using learnmvc.DataAccess;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace learnmvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CompanyController(IUnitOfWork UnitOfWork)
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
            Company Company = new();
            if (id == null || id == 0){
                
                return View(Company);

            }
            else{
                //Update Product
                Company = _UnitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
                return View(Company);
            }
          
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company item)
        {

            if (ModelState.IsValid)
            {
               

                if (item.Id == 0)
                {
                    _UnitOfWork.Company.Add(item);
                    _UnitOfWork.Save();
                    TempData["success"] = " Company Added Successfully";
                }
                else { 
                    _UnitOfWork.Company.update(item);        
                    _UnitOfWork.Save();
                    TempData["success"] = " Company Updated Successfully";
                
                }

                return RedirectToAction("Index");
            }
            return View(item);
        }
      

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _UnitOfWork.Company.GetAll();
            return Json(new {data = CompanyList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return Json(new {success= false, message = "Error while deleting"});
            }

            _UnitOfWork.Company.Remove(item);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            
        }
        #endregion
    }
}
