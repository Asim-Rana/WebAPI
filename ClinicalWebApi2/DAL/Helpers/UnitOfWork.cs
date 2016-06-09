using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace ClinicalWebApi2.DAL.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;
        private static object lockedObject = new object();

        private ISession Session { get; set; }

        static UnitOfWork()
        {
            var MappingAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("DAL")).ToList();
            lock (lockedObject)
            {
                _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("connstring")).ShowSql())
                        .CurrentSessionContext("web")
                        .Mappings(m => MappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                        .ExposeConfiguration(config => new SchemaUpdate(config).Execute(true, true))
                        .BuildSessionFactory(); 
            }
        }
        public static ISession CurrentSession
        {
            get
            {
                if (CurrentSessionContext.HasBind(_sessionFactory))

                    return _sessionFactory.GetCurrentSession();

                ISession session = _sessionFactory.OpenSession();

                CurrentSessionContext.Bind(session);

                return session;

            }
        }
        public static void CloseSession()
        {
            
            if (_sessionFactory == null)
            {
                return;
            }
            if (CurrentSessionContext.HasBind(_sessionFactory))
            {

                ISession session = CurrentSessionContext.Unbind(_sessionFactory);

                session.Clear();
                session.Close();

            } 
            
        }
        public void BeginTransaction()
        {
            
            try
            {
                Session = CurrentSession;
                Session.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            } 
           
        }

        public void Commit()
        {
            
            Session = CurrentSession;
            _transaction = Session.Transaction;
            if (_transaction != null && _transaction.IsActive)
            {
                try
                {
                    Session.FlushMode = FlushMode.Commit;
                    _transaction.Commit();
                }
                catch (Exception)
                {
                    _transaction.Rollback();
                    throw;
                }
                finally
                {
                    CloseSession();
                }
            } 
            
        }
    }
}
