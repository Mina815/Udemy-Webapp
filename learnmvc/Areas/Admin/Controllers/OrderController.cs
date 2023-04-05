using learnmvc.DataAccess.Repositry;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult GetAll()
		{
			IEnumerable<OrderHeader> orderHeaders;
			orderHeaders = _UnitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
			return Json(new { data = orderHeaders });
		}
	}
}
