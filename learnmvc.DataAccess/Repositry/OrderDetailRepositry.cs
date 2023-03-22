using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class OrderDetailRepositry : Repositry<OrderDetail>, IOrderDetailRepositry
	{

        private AppDbContext _db;
        public OrderDetailRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
