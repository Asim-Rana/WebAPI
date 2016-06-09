using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClinicalWebApi2.BusinessEntities
{
    public class Doctor
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Your Name is invalid.")]
        [DataType(DataType.Text)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Education")]
        [DataType(DataType.Text)]
        public virtual string Education { get; set; }

        [Required(ErrorMessage = "Please Enter Your Speciality")]
        [DataType(DataType.Text)]
        public virtual string Speciality { get; set; }

        [Required(ErrorMessage = "Please Enter Your Contact No")]
        [RegularExpression(@"^(\+92)\d{3}\d{7}$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        public virtual string Phone { get; set; }
        [DataType(DataType.Text)]
        public virtual string Gender { get; set; }
        public virtual IList<Appointment> AssignedAppointments { get; protected set; }
        public virtual IList<PatientHistory> ExaminedPatients { get; protected set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [DataType(DataType.Text)]
        public virtual string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Experience")]
        [DataType(DataType.Text)]
        public virtual string Experience { get; set; }

        [Required(ErrorMessage = "Please Enter Your Training")]
        [DataType(DataType.Text)]
        public virtual string Training { get; set; }
        public Doctor()
        {
            AssignedAppointments = new List<Appointment>();
            ExaminedPatients = new List<PatientHistory>();
        }
        public virtual void RemoveAppointments()
        {
            for (int i = AssignedAppointments.Count - 1; i >= 0; i--)
            {
                AssignedAppointments.ElementAt(i).doctor = null;
                AssignedAppointments.RemoveAt(i);
            }
        }
        public virtual void RemovePatientHistories()
        {
            for (int i = ExaminedPatients.Count - 1; i >= 0; i--)
            {
                ExaminedPatients.ElementAt(i).doctor = null;
                ExaminedPatients.RemoveAt(i);
            }
        }
    }
}
