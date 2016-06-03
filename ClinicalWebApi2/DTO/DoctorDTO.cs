using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DTO
{
    public class DoctorDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Education { get; set; }

        public string Speciality { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
