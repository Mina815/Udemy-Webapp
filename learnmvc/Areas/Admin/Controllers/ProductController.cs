using learnmvc.DataAccess;
using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using learnmvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace learnmvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _hostEnvironment = hostEnvironment;
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

            if (id == null || id == 0){
                
                return View(ProductVM);

            }
            else{
                //Update Product
                ProductVM.Product = _UnitOfWork.Product.GetFirstOrDefault(u=>u.Id == id);
                return View(ProductVM);
            }
          
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM item, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                String WWWRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    //Creating name for image
                    String fileName = Guid.NewGuid().ToString();
                    var Uploads = Path.Combine(WWWRootPath, @"Images\Product");
                    var Extension = Path.GetExtension(file.FileName);

                    //check if there is an img exists to remove it for Update
                    if(item.Product.ImageUrl!= null)
                    {
                        var OldImgPath = Path.Combine(WWWRootPath,item.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(OldImgPath))
                        {
                            System.IO.File.Delete(OldImgPath);
                        }
                    }
                    using(var fileStream = new FileStream(Path.Combine(Uploads,fileName + Extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    item.Product.ImageUrl = @"\Images\Product\" + fileName + Extension;
                };

                if (item.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(item.Product);
                    _UnitOfWork.Save();
                    TempData["success"] = " Product Added Successfully";
                }
                else { 
                    _UnitOfWork.Product.update(item.Product);        
                    _UnitOfWork.Save();
                    TempData["success"] = " Product Updated Successfully";
                
                }

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
        

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = _UnitOfWork.Product.GetAll(includeProperties: "Category,CoverType,");
            return Json(new {data = ProductList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var item = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return Json(new {success= false, message = "Error while deleting"});
            }
            //deleting the image
            if (item.ImageUrl != null)
            {
                var OldImgPath = Path.Combine(_hostEnvironment.WebRootPath, item.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(OldImgPath))
                {
                    System.IO.File.Delete(OldImgPath);
                }
            }

            _UnitOfWork.Product.Remove(item);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            
        }
        #endregion
    }
}
