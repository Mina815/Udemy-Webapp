using learnmvc.DataAccess;
using learnmvc.DataAccess.Repositry;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _UnitOfWork.Category.GetAll();
            return View(CategoryList);
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
                _UnitOfWork.Category.Add(item);
                _UnitOfWork.Save();
                TempData["success"] = " Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var item = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
                _UnitOfWork.Category.update(item);
                _UnitOfWork.Save();
                TempData["success"] = " Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var item = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id ==id);
            if (item == null) return NotFound();

            _UnitOfWork.Category.Remove(item);
            _UnitOfWork.Save();
            TempData["success"] = " Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
