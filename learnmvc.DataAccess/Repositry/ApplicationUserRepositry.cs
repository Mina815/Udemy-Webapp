using learnmvc.DataAccess.Repositry.IRepositry;
using learnmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry
{
    public class ApplicationUserRepositry : Repositry<ApplicationUser>, IApplicationUserRepositry
    {

        private AppDbContext _db;
        public ApplicationUserRepositry(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
