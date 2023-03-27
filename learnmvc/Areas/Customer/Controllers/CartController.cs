using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Models.ViewModels;
using learnmvc.Utility;
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
                includeProperties: "Product"),
                OrderHeader = new ()
            };
            foreach(var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPrice(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Count * cart.Price;
            }
            return View(ShoppingCartVM);
        }
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var Claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _UnitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == Claim.Value,
				includeProperties: "Product"),
				OrderHeader = new()
			};
            ShoppingCartVM.OrderHeader.ApplicationUser = _UnitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == Claim.Value);
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

			foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPrice(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += cart.Count * cart.Price;
			}
				return View(ShoppingCartVM);
		}

		[HttpPost]
		[ActionName("Summary")]
		[ValidateAntiForgeryToken]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var Claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.ListCart = _UnitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == Claim.Value,
				includeProperties: "Product");
			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = Claim.Value;

			
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPrice(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += cart.Count * cart.Price;
			}
			_UnitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
			_UnitOfWork.Save();
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count,
				};
				_UnitOfWork.OrderDetail.Add(orderDetail);
				_UnitOfWork.Save();

			}
			_UnitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
			_UnitOfWork.Save();
			return RedirectToAction("Index","Home");
		}
		public IActionResult Plus(int cartID) 
        {
            var cart = _UnitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartID);
			_UnitOfWork.ShoppingCart.IncrementCount(cart, 1);
			_UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
		public IActionResult Minus(int cartID)
		{
			var cart = _UnitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartID);
			_UnitOfWork.ShoppingCart.DecrementCount(cart, 1);
            if(cart.Count < 1)
            {
                _UnitOfWork.ShoppingCart.Remove(cart);
            }
			_UnitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int cartID)
		{
			var cart = _UnitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartID);
            _UnitOfWork.ShoppingCart.Remove(cart);
			_UnitOfWork.Save();
			return RedirectToAction(nameof(Index));
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
