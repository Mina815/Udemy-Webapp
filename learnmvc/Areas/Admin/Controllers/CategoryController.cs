using learnmvc.DataAccess;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {
        private readonly AppDbContext _UnitOfWork;
        public CategoryController(AppDbContext UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> kolo = _UnitOfWork.Categories;
            return View(kolo);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category item)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Categories.Add(item);
                _UnitOfWork.SaveChanges();
                TempData["success"] = " Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var item = _UnitOfWork.Categories.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category item)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Categories.Update(item);
                _UnitOfWork.SaveChanges();
                TempData["success"] = " Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.Categories.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var item = _UnitOfWork.Categories.Find(id);
            if (item == null) return NotFound();

            _UnitOfWork.Categories.Remove(item);
            _UnitOfWork.SaveChanges();
            TempData["success"] = " Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
