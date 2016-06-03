using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Helpers;
using ClinicalWebApi2.DAL.Repositories;
using ClinicalWebApi2.DTO;

namespace ClinicalWebApi2.DAL.Services
{
    public interface IPatientService
    {
        IList<PatientDTO> GetAll();
        PatientDTO GetById(string id);
        bool Create(Patient patient);
        int Update(string id , Patient patient);
        int Delete(string id);
        PatientDTO GetByPhone(string phone);
    }
    public class PatientService : IPatientService
    {
        private IRepository<Patient> _patientRepository;
        private IMapper _mapper = null;
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = HttpContext.Current.Application["Configuration"] as MapperConfiguration;
                    _mapper = config.CreateMapper();
                }
                return _mapper;
            }
        }
        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public bool Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }

        public int Delete(string id)
        {
            Patient patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                return 0;
            }
            patient.RemoveAppointments();
            patient.RemovePatientHistories();
            if (!_patientRepository.Delete(id))
            {
                return 1;
            }
            return 2;
        }

        public IList<PatientDTO> GetAll()
        {
            IList<Patient> patList = _patientRepository.GetAll().ToList();
            IList<PatientDTO> patDTOList = new List<PatientDTO>();
            foreach(var pat in patList)
            {
                patDTOList.Add(Mapper.Map<Patient, PatientDTO>(pat));
            }        
            return patDTOList;
        }

        public PatientDTO GetById(string id)
        {
            Patient pat = _patientRepository.GetById(id);
            return Mapper.Map<Patient, PatientDTO>(pat);
        }

        public int Update(string id , Patient patient)
        {
            Patient pat = _patientRepository.GetById(id);
            if (pat == null)
            {
                return 0;
            }
            pat.Age = patient.Age;
            pat.Name = patient.Name;
            pat.Phone = patient.Phone;
            pat.Address = patient.Address;
            if(!_patientRepository.Update(pat))
            {
                return 1;
            }
            return 2;
        }
        public PatientDTO GetByPhone(string phone)
        {
            Patient pat = _patientRepository.GetEntityByPhone(ExpressionFilter.CreateContainsExpression<Patient>(phone));
            return Mapper.Map<Patient, PatientDTO>(pat);
        }
    }
}
