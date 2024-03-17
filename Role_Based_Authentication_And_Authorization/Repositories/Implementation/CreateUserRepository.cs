using Role_Based_Authentication_And_Authorization.Data;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Repositories.Implementation
{
    public class CreateUserRepository : ICreateUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CreateUserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User usr)
        {
            await dbContext.Users.AddAsync(usr);
            await dbContext.SaveChangesAsync();
            return usr;
        }
    }
}
