using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet= _db.Set<T>();
            //_db.Categories is now equal to dbSet
            //so instead of using _db.Categories.Add we can now use dbSet.Add()
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            T obj = dbSet.FirstOrDefault(filter);
            return obj;
        }

        public IEnumerable<T> GetALL()
        {
            return dbSet.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
