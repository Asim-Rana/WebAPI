using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.BusinessEntities
{
    public class PatientHistory
    {
        public virtual string Id { get; set; }

        [Required]
        public virtual System.DateTime Date { get; set; }

        public virtual IList<Disease> diseases { get; protected set; }
        public virtual IList<Medicine> medicines { get; protected set; }
        public virtual Doctor doctor { get; set; }
        public virtual Patient patient { get; set; }
        //public virtual void AddDoctor(Doctor doc)
        //{
        //    doc.ExaminedPatients.Add(this);
        //    doctor = doc;
        //}
        //public virtual void AddPatient(Patient pat)
        //{
        //    pat.CheckupHistory.Add(this);
        //    patient = pat;
        //}
        public virtual void RemoveDoctor()
        {
            doctor.ExaminedPatients.Remove(this);
            doctor = null;
        }
        public virtual void RemovePatient()
        {
            patient.CheckupHistory.Remove(this);
            patient = null;
        }
        public PatientHistory()
        {
            diseases = new List<Disease>();
            medicines = new List<Medicine>();
        }
        //public virtual void AddMedicine(List<Medicine> med)
        //{
        //    foreach(var m in med)
        //    { 
        //        medicines.Add(m);
        //        m.history = this;
        //    }
        //}
        //public virtual void AddDisease(List<Disease> dis)
        //{
        //    foreach(var d in dis)
        //    { 
        //        diseases.Add(d);
        //        d.history = this;
        //    }
        //}
        public virtual void RemoveMedicine()
        {
            medicines.Clear();
        }
        public virtual void RemoveDisease()
        {
            diseases.Clear();
        }
    }
}
