using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class Appointment
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Appointment Date")]
        [DataType(DataType.Date)]
        public virtual System.DateTime Date { get; set; }

        public virtual bool IsProcessed { get; set; }

        public virtual int Token { get; set; }

        public virtual Doctor doctor { get; set; }

        public virtual Patient patient { get; set; }
        public virtual void AddDoctor(Doctor doc)
        {
            doc.AssignedAppointments.Add(this);
            doctor = doc;
        }
        public virtual void AddPatient(Patient pat)
        {
            pat.appointments.Add(this);
            patient = pat;
        }
        public virtual void RemoveDoctor()
        {
            doctor.AssignedAppointments.Remove(this);
            doctor = null;
        }
        public virtual void RemovePatient()
        {
            patient.appointments.Remove(this);
            patient = null;
        }
    }
}
