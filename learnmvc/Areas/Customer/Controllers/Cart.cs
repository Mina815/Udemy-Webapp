using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class Cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
