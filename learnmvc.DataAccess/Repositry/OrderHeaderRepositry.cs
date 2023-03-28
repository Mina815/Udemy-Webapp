using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class OrderHeaderRepositry : Repositry<OrderHeader>, IOrderHeaderRepositry
	{

        private AppDbContext _db;
        public OrderHeaderRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var order = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(order != null)
            {
                order.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    order.PaymentStatus = paymentStatus;
                }
            }
		}
		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var order = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
			order.SessionId = sessionId;
            order.PaymentIntentId = paymentIntentId;

		}
	}
}
