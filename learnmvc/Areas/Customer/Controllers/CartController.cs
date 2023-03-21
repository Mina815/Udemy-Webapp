using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace learnmvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public int OrderTotal { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var Claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _UnitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == Claim.Value,
                includeProperties: "Product")
            };
            foreach(var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPrice(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.CartTotal += cart.Count * cart.Price;
            }
            return View(ShoppingCartVM);
        }
        private double GetPrice(double quantity, double price,double price50, double price100)
        {
            if (quantity <= 50) return price;
            else {
                if (quantity <= 100) return price50;
                return price100;
            }
        }

    }
}
