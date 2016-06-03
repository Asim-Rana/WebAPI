using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class Disease
    {
        public virtual string Id { get; set; }
        public virtual string Value { get; set; }
        public virtual PatientHistory history { get; set; }
        public virtual void RemoveHistory()
        {
            history.diseases.Remove(this);
            history = null;
        }
        
    }
}
