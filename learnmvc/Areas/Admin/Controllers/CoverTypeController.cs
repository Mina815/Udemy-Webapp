using learnmvc.DataAccess;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly AppDbContext _UnitOfWork;
        public CoverTypeController(AppDbContext UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> kolo = _UnitOfWork.CoverTypes;
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
        public IActionResult Create(CoverType item)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverTypes.Add(item);
                _UnitOfWork.SaveChanges();
                TempData["success"] = " CoverType Created Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var item = _UnitOfWork.CoverTypes.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

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
                TempData["success"] = " CoverType Edited Successfully";
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
            TempData["success"] = " CoverType Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
