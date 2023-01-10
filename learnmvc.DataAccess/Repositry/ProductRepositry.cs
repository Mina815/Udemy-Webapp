using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class ProductRepositry : Repositry<Product>, IProductRepositry
    {

        private AppDbContext _db;
        public ProductRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }



        public void update(Product obj)
        {
            var ObjFromDB = _db.Products.FirstOrDefault(u =>u.Id == obj.Id);
            if (ObjFromDB != null)
            {
                ObjFromDB.Name= obj.Name;
                ObjFromDB.ISBN= obj.ISBN;
                ObjFromDB.Price= obj.Price;
                ObjFromDB.Price50= obj.Price50;
                ObjFromDB.Price100= obj.Price100;
                ObjFromDB.Author= obj.Author;
                ObjFromDB.Decribtion= obj.Decribtion;
                ObjFromDB.CategoryId= obj.CategoryId;
                ObjFromDB.CoverTypeId= obj.CoverTypeId;
                if(obj.ImageUrl !=null)
                {
                    ObjFromDB.ImageUrl= obj.ImageUrl;
                }

            }
        }
    }
}
