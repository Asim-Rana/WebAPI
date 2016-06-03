using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DTO
{
    public class PatientHistoryDTO
    {
        public string Id { get; set; }
        public System.DateTime Date { get; set; }

        public IList<DiseaseDTO> diseases { get; set; }
        public IList<MedicineDTO> medicines { get; set; }
        public DoctorDTO doctor { get; set; }
        public PatientDTO patient { get; set; }
    }
}
