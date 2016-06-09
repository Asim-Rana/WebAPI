using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace ClinicalWebApi2.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository()
        {
            if(Session == null)
            { 
                Session = UnitOfWork.CurrentSession;
            }
        }

        protected ISession Session;

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public T GetById(string id)
        {
            return Session.Get<T>(id);
        }

        public bool Create(T entity)
        {
            try
            {
                Session.Save(entity);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(T entity)
        {
            try
            {
                Session.Update(entity);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                Session.Delete(Session.Load<T>(id));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public IList<T> GetAppointment<U>(U id, int criteria)
        {
            IQuery q = null;
            switch (criteria)
            {
                case 1:
                    try
                    {
                        q = Session.CreateQuery("from Appointment where doctor_id='" + id + "'");
                    }
                    catch (Exception)
                    {
                       return null;
                    }
                    break;
                case 2:
                    try
                    {
                        q = Session.CreateQuery("from Appointment where token=" + id);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    break;
                case 3:
                    try
                    {
                        q = Session.CreateQuery("from Appointment where patient_id='" + id + "'");
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    break;
            }
             
            return q.List<T>();
        }
        public T GetEntityByPhone(Expression<Func<T, bool>> expression)
        {
            return Session.QueryOver<T>().Where(expression).SingleOrDefault();
        }
    }
}
