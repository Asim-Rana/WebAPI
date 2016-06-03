using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class Assistant
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Your Name is invalid.")]
        [DataType(DataType.Text)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Education")]
        [DataType(DataType.Text)]
        public virtual string Education { get; set; }

        [Required(ErrorMessage = "Please Enter Your Age")]
        [Range(1, 120)]
        public virtual int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Your Contact No")]
        [RegularExpression(@"^(\+92)\d{3}\d{7}$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        public virtual string Phone { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [DataType(DataType.Text)]
        public virtual string Address { get; set; }
    }
}
