using Microsoft.AspNetCore.Identity;

namespace Role_Based_Authentication_And_Authorization.Repositories.Interface
{
    public interface IJwtTokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
