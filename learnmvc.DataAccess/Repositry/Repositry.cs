using learnmvc.DataAccess.Repositry.IRepositry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class Repositry<T> : IRepositry<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public Repositry(AppDbContext db)
        {
            _db = db;
            //_db.Products.Include(u => u.Category).Include(u => u.CoverType);
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, String? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null) query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query= query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, String? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();

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
