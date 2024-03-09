using E_Commerce.Models.Models;


namespace E_Commerce.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
    