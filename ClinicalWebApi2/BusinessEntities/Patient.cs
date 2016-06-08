using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class Patient
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Your Name is invalid.")]
        [DataType(DataType.Text)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Contact No")]
        [RegularExpression(@"^(\+92)\d{3}\d{7}$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        public virtual string Phone { get; set; }

        [Required(ErrorMessage = "Please Enter Your Age")]
        [Range(1, 120)]
        public virtual int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [DataType(DataType.Text)]
        public virtual string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Your Gender")]
        [DataType(DataType.Text)]
        public virtual string Gender { get; set; }

        public virtual IList<PatientHistory> CheckupHistory
        {
            get; set;
        }
        public virtual IList<Appointment> appointments
        {
            get; set;
        }

        public Patient()
        {
            CheckupHistory = new List<PatientHistory>();
            appointments = new List<Appointment>();
        }
        public virtual void RemoveAppointments()
        {
            appointments.Clear();
        }
        public virtual void RemovePatientHistories()
        {
            CheckupHistory.Clear();
        }
    }
}
