using learnmvc.DataAccess.Repositry.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepositry(_db);
            CoverType = new CoverTypeRepositry(_db);
            Product = new ProductRepositry(_db);
        }
        public ICategoryRepositry Category { get; private set; }
        public ICoverTypeRepositry CoverType { get; private set; }
        public IProductRepositry Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
