using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(string id);
        IList<T> GetAppointment<U>(U id , int criteria);
        T GetEntityByPhone(Expression<Func<T, bool>> expression);
    }
}
