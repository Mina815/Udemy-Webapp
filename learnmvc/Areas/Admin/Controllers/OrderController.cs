using learnmvc.DataAccess.Repositry;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace learnmvc.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _UnitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult GetAll(string status)
		{
			IEnumerable<OrderHeader> orderHeaders;
			orderHeaders = _UnitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                default:
                    break;
            }

            return Json(new { data = orderHeaders });
		}
	}
}
