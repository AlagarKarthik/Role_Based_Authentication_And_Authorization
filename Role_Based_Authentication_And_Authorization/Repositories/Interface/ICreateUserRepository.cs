using Role_Based_Authentication_And_Authorization.Models.Domain;

namespace Role_Based_Authentication_And_Authorization.Repositories.Interface
{
    public interface ICreateUserRepository
    {
        Task<User> CreateAsync(User usr);
    }
}
