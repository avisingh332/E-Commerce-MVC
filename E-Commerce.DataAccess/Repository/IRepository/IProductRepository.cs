using E_Commerce.Models.Models;


namespace E_Commerce.DataAccess.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
         void Update(Product obj);
    }
}
