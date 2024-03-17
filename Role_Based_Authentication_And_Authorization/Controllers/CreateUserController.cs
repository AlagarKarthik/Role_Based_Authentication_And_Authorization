using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Models.Dto;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {

        private readonly ICreateUserRepository userLoginRepository;

        public CreateUserController(ICreateUserRepository userLoginRepository)
        {
            this.userLoginRepository = userLoginRepository;
        }

        //public CreateUserController(ApplicationDbContext applicationDbContext) 
        //{
        //    _authenticateContext = applicationDbContext;
        //}

        //[HttpPost("Authenticate")]

        //public async Task<IActionResult> LoginAuth([FromBody] CreateUserRequestDto requestDto)
        //{
        //    if(requestDto == null)
        //    {
        //        return BadRequest();
        //    }

        //    var user = await _authenticateContext.Users.FirstOrDefaultAsync(x => x.UserName == requestDto.UserName 
        //    && x.Password == requestDto.Password);

        //    if (user == null)
        //    {
        //        return NotFound(new { Message = "User not found"});

        //    }
        //    else
        //    {
        //        return Ok(new 
        //        {
        //            Message = "Login Succeed"
        //        });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> SaveUser([FromBody] CreateUserRequestDto request)
        {
            // Here mapping DTO to Domain model
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password,
                Role = request.Role,
            };

            await userLoginRepository.CreateAsync(user);


            // Here Domain model to DTO
            var response = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Role = user.Role,

            };
            return Ok(response);
        }
    }
}
