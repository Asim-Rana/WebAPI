using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ClinicalWebApi2.DAL.Helpers;
using Ninject;

namespace ClinicalWebApi2.Filters
{
    public class NHSessionManagementAttribute : ActionFilterAttribute
    {
        public override bool AllowMultiple { get { return false; } }
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            UnitOfWork.BeginTransaction();
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            if (actionExecutedContext.Exception == null)
                UnitOfWork.Commit();
            
        }
    }
}