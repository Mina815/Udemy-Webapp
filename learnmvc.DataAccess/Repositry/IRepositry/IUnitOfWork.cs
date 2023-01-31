using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry.IRepositry
{
    public interface IUnitOfWork
    {
        ICategoryRepositry Category { get; }
        ICoverTypeRepositry CoverType { get; }
        IProductRepositry Product { get; }
        ICompanyRepositry Company { get; }
        IShoppingCartRepositry ShoppingCart { get; }
        IApplicationUserRepositry ApplicationUser { get; }
        void Save();
    }
}
