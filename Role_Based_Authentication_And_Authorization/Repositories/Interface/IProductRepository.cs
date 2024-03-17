using Role_Based_Authentication_And_Authorization.Models.Domain;

namespace Role_Based_Authentication_And_Authorization.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);

        Task<IEnumerable<Product>> GetAllAscyn();
    }
}
