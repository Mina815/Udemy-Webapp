using learnmvc.DataAccess.Repositry;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace learnmvc.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
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
			if(User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_User_Indi))
				orderHeaders = _UnitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
			else
			{
				var ClaimIdentity = (ClaimsIdentity)User.Identity;
				var Claim = ClaimIdentity.FindFirst(ClaimTypes.NameIdentifier);
				orderHeaders = _UnitOfWork.OrderHeader.GetAll(u=>u.ApplicationUserId == Claim.Value,includeProperties: "ApplicationUser");
			}
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
