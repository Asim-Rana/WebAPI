using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class FeedBack
    {
        public virtual string Id { get; set; }
        [Required(ErrorMessage = "Question is required")]
        public virtual string Question { get; set; }
        public virtual int Excellent { get; set; }
        public virtual int Satisfactory { get; set; }
        public virtual int Good { get; set; }
        public virtual int DontKnow { get; set; }
    }
}
