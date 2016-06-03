using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DTO
{
    public class AppointmentDTO
    {
        public string Id { get; set; }
        public System.DateTime Date { get; set; }

        public bool IsProcessed { get; set; }

        public int Token { get; set; }

        public DoctorDTO doctor { get; set; }

        public PatientDTO patient { get; set; }
    }
}
