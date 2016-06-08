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
    public interface IDoctorService
    {
        IList<DoctorDTO> GetAll();
        DoctorDTO GetById(string id);
        bool Create(Doctor doctor);
        int Update(string id ,Doctor doctor);
        int Delete(string id);
        DoctorDTO GetByPhone(string phone);
    }
    public class DoctorService : IDoctorService
    {
        private IRepository<Doctor> _doctorRepository;
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
        public DoctorService(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public bool Create(Doctor doctor)
        {
            return _doctorRepository.Create(doctor);
        }

        public int Delete(string id)
        {
            Doctor doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return 0;
            }
            doctor.RemoveAppointments();
            doctor.RemovePatientHistories();
            if(!_doctorRepository.Delete(id))
            {
                return 1;
            }
            return 2;
        }

        public IList<DoctorDTO> GetAll()
        {
            IList<Doctor> docList = _doctorRepository.GetAll().ToList();
            IList<DoctorDTO> docDTOList = new List<DoctorDTO>();
            foreach (var doc in docList)
            {
                docDTOList.Add(Mapper.Map<Doctor, DoctorDTO>(doc));
            }
            return docDTOList;
        }

        public DoctorDTO GetById(string id)
        {
            Doctor doc = _doctorRepository.GetById(id);
            return Mapper.Map<Doctor, DoctorDTO>(doc);
        }

        public int Update(string id , Doctor doctor)
        {
            Doctor doc = _doctorRepository.GetById(id);
            if (doc == null)
            {
                return 0;
            }
            doc.Name = doctor.Name;
            doc.Phone = doctor.Phone;
            doc.Speciality = doctor.Speciality;
            doc.Education = doctor.Education;
            doc.Address = doctor.Address;
            doc.Gender = doctor.Gender;
            if(!_doctorRepository.Update(doc))
            {
                return 1;
            }
            return 2;
        }
        public DoctorDTO GetByPhone(string phone)
        {
            Doctor doc = _doctorRepository.GetEntityByPhone(ExpressionFilter.CreateContainsExpression<Doctor>(phone));
            return Mapper.Map<Doctor, DoctorDTO>(doc);
        }
    }
}
