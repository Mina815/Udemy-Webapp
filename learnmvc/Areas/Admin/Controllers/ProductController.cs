using learnmvc.DataAccess;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace learnmvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            
            return View();
        }
       
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM ProductVM = new()
            {
                Product = new(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(i=>new SelectListItem
                {
                    Text= i.Name,
                    Value= i.Id.ToString(),
                }),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(i=> new SelectListItem
                {
                    Text= i.Name,
                    Value= i.Id.ToString(),
                }),
            };           
            //IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            // IEnumerable<SelectListItem> CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                //return View(Product);
                return View(ProductVM);

            }
            else
            {
                //Update Product

            }
          
            return View(ProductVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM item, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_UnitOfWork.Product.Update(item);
                _UnitOfWork.Save();
                TempData["success"] = " product Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        ////GET
        //public IActionResult Delete(int? id)
        //{
        //    var item = _UnitOfWork.CoverTypes.FirstOrDefault(u => u.Id == id);
        //    if (item == null) return NotFound();

        //    return View(item);
        //}
        ////POST
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteCoverType(int? id)
        //{
        //    var item = _UnitOfWork.CoverTypes.FirstOrDefault(u => u.Id == id); 
        //    if (item == null) return NotFound();

        //    _UnitOfWork.CoverTypes.Remove(item);
        //    _UnitOfWork.SaveChanges();
        //    TempData["success"] = " product Deleted Successfully";
        //    return RedirectToAction("Index");
        //}
    }
}
