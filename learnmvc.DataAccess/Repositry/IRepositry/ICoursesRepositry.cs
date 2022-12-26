using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry.IRepositry
{
    public interface ICoursesRepositry : IRepositry<Course>
    {
        void update(Course obj);
        void save();
    }
}
