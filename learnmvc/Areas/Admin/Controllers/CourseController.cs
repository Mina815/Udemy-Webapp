using learnmvc.DataAccess;
using learnmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnmvc.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> kolo = _db.courses;
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
        public IActionResult Create(Course item)
        {
            if (ModelState.IsValid)
            {
                _db.courses.Add(item);
                _db.SaveChanges();
                TempData["success"] = " Course Created Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var item = _db.courses.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course item)
        {
            if (ModelState.IsValid)
            {
                _db.courses.Update(item);
                _db.SaveChanges();
                TempData["success"] = " Course Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            var item = _db.courses.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourse(int? id)
        {
            var item = _db.courses.Find(id);
            if (item == null) return NotFound();

            _db.courses.Remove(item);
            _db.SaveChanges();
            TempData["success"] = " Course Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
