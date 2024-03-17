using Microsoft.EntityFrameworkCore;
using Role_Based_Authentication_And_Authorization.Data;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAscyn()
        {
            return await dbContext.Products.ToListAsync();
        }
    }
}
