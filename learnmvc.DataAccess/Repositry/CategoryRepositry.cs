using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class CategoryRepositry : Repositry<Category>, ICategoryRepositry
    {

        private AppDbContext _db;
        public CategoryRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
