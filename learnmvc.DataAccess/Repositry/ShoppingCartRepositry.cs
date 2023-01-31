using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class ShoppingCartRepositry : Repositry<ShoppingCart>, IShoppingCartRepositry
    {

        private AppDbContext _db;
        public ShoppingCartRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
