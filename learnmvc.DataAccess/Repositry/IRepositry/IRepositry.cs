using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.DataAccess.Repositry.IRepositry
{
    public interface IRepositry<T> where T : class
    {
        IEnumerable<T> GetAll(String? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter , String? includeProperties = null);


    }
}
