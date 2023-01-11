using learnmvc.DataAccess;
using learnmvc.DataAccess.Repositry;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CoverTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> kolo = _UnitOfWork.CoverType.GetAll();
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
                _UnitOfWork.CoverType.Add(item);
                _UnitOfWork.Save();
                TempData["success"] = " CoverType Created Successfully";
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
        public IActionResult Edit(CoverType item)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.update(item);
                _UnitOfWork.Save();
                TempData["success"] = " CoverType Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoverType(int? id)
        {
            var item = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id); 
            if (item == null) return NotFound();

            _UnitOfWork.CoverType.Remove(item);
            _UnitOfWork.Save();
            TempData["success"] = " CoverType Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
