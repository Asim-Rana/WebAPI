using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class Medicine
    {
        public virtual string Id { get; set; }
        public virtual string Value { get; set; }
        public virtual PatientHistory history { get; set; }
        public virtual void RemoveMedicine()
        {
            history.medicines.Remove(this);
            history = null;
        }

    }
}
