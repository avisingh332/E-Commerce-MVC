using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace E_Commerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            _categoryRepository = new Lazy<ICategoryRepository>(new CategoryRepository(_db));
            _productRepository = new Lazy<IProductRepository>(new ProductRepository(_db));  
        }

        public ICategoryRepository Category
        {
            get { return _categoryRepository.Value; }
        }
        public IProductRepository Product
        {
            get { return _productRepository.Value;}
        }

        public void Save()
        {
           _db.SaveChanges();
        }

    }
}
