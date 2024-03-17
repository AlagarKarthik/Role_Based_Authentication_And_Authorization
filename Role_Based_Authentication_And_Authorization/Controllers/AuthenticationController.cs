using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Authentication_And_Authorization.Models.Dto;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
       
        private readonly IJwtTokenRepository jwtTokenRepository;

        public AuthenticationController(UserManager<IdentityUser> userManager, IJwtTokenRepository jwtTokenRepository)
        {
            this._userManager = userManager;
            this.jwtTokenRepository = jwtTokenRepository;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser is not null)
            {
                var checkpasswordResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkpasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    //create JWT Token and Response

                    var jwtTkn = jwtTokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtTkn

                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");

            return ValidationProblem(ModelState);

        }

        [HttpPost]
        [Route("adduser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto request)
        {
            // create the identityuser object

            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()

            };


            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                if (request.Role == "Admin")
                {
                    identityResult = await _userManager.AddToRoleAsync(user, "Admin");
                }

                else if (request.Role == "Manager")
                {
                    identityResult = await _userManager.AddToRoleAsync(user, "Manager");
                }
                else if (request.Role == "Reviewer")
                {
                    identityResult = await _userManager.AddToRoleAsync(user, "Reviewer");
                }

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
