using Microsoft.EntityFrameworkCore;
using Role_Based_Authentication_And_Authorization.Data;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAscyn()
        {
            return await dbContext.Categories.ToListAsync();

        }

    }
}
