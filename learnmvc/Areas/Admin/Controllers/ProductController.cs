using learnmvc.DataAccess;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _UnitOfWork;
        public ProductController(AppDbContext UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> kolo = _UnitOfWork.Products;
            return View(kolo);
        }
       
        //GET
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create Product

            }
            else
            {
                //Update Product

            }
          
            return View(item);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType item)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverTypes.Update(item);
                _UnitOfWork.SaveChanges();
                TempData["success"] = " product Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.CoverTypes.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoverType(int? id)
        {
            var item = _UnitOfWork.CoverTypes.FirstOrDefault(u => u.Id == id); 
            if (item == null) return NotFound();

            _UnitOfWork.CoverTypes.Remove(item);
            _UnitOfWork.SaveChanges();
            TempData["success"] = " product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
