using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class CoverTypeRepositry : Repositry<CoverType>, ICoverTypeRepositry
    {

        private AppDbContext _db;
        public CoverTypeRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }



        public void update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
