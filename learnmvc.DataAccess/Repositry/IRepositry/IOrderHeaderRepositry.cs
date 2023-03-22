using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry.IRepositry
{
    public interface IOrderHeaderRepositry : IRepositry<OrderHeader>
    {
        
        void update(OrderHeader obj);
        void UpdateStatus(int id , string orderStatus, string? paymentStatus  = null);
    }
}
