using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    //generic interface repository 
    public interface IRepository<T> where T : class
    {
        //T- any class
        T GetFirstOrDefault(Expression<Func<T, bool>> filter,string? includeProperties = null, bool tracked = true);// _db.Categories.FirstOrDefault(u => u.Id == id) its in this format
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);//collection of entites
    }
}
