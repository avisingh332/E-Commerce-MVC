using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.DataAccess.Data;


namespace E_Commerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; } 
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);   
        }
        public void Save()
        {
           _db.SaveChanges();
        }

    }
}
