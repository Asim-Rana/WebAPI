[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ClinicalWebApi2.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ClinicalWebApi2.App_Start.NinjectWebCommon), "Stop")]

namespace ClinicalWebApi2.App_Start
{
    using System;
    using System.Web;
    using ClinicalWebApi2.BusinessEntities;
    using ClinicalWebApi2.DAL.Helpers;
    using ClinicalWebApi2.DAL.Repositories;
    using ClinicalWebApi2.DAL.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IDoctorService>().To<DoctorService>();
            kernel.Bind<IPatientService>().To<PatientService>();
            kernel.Bind<IAppointmentService>().To<AppointmentService>();
            kernel.Bind<IAssistantService>().To<AssistantService>();
            kernel.Bind<IPatientHistoryService>().To<PatientHistoryService>();
            kernel.Bind<IRepository<Doctor>>().To<Repository<Doctor>>();
            kernel.Bind<IRepository<Patient>>().To<Repository<Patient>>();
            kernel.Bind<IRepository<Appointment>>().To<Repository<Appointment>>();
            kernel.Bind<IRepository<PatientHistory>>().To<Repository<PatientHistory>>();
            kernel.Bind<IRepository<Assistant>>().To<Repository<Assistant>>();
        }        
    }
}
