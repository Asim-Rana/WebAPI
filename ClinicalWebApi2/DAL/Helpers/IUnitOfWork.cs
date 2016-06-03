using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DAL.Helpers
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
    }
}
