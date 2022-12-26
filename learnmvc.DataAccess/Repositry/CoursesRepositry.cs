using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class CoursesRepositry : Repositry<Course>, ICoursesRepositry
    {

        private AppDbContext _db;
        public CoursesRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void save()
        {
            _db.SaveChanges();
        }

        public void update(Course obj)
        {
            _db.courses.Update(obj);
        }
    }
}
