using System.Collections.Generic;
using System.Linq.Expressions;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Repositories;
using System.Linq;
using System;
using ClinicalWebApi2.DAL.Helpers;
using ClinicalWebApi2.DTO;
using AutoMapper;
using System.Web;

namespace ClinicalWebApi2.DAL.Services
{
    public interface IPatientHistoryService
    {
        IList<PatientHistoryDTO> GetAll();
        PatientHistoryDTO GetById(string id);
        bool Create(List<PatientHistory> patientHistory);
        void Update(PatientHistory patientHistory);
        bool Delete(string id , PatientHistory patientHistory);
        IList<PatientHistoryDTO> GetByPatient(string id);
        IList<PatientHistoryDTO> GetByDoctor(string id);
        bool DeleteAfterDate(DateTime date);
    }
    public class PatientHistoryService : IPatientHistoryService
    {
        private IRepository<PatientHistory> _patientHistoryRepository;
        private IRepository<Patient> _patientRepository;
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

        public PatientHistoryService(IRepository<PatientHistory> patientHistoryRepository, IRepository<Patient> patientRepository , IRepository<Doctor> doctorRepository)
        {
            _patientHistoryRepository = patientHistoryRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }
        public bool Create(List<PatientHistory> patientHistory)
        {
            foreach(var history in patientHistory)
            {
                history.Id = Guid.NewGuid().ToString();
                foreach(var disease in history.diseases)
                {
                    disease.Id = Guid.NewGuid().ToString();
                    disease.history = history;
                }
                foreach (var medicine in history.medicines)
                {
                    medicine.Id = Guid.NewGuid().ToString();
                    medicine.history = history;
                }
                if (!_patientHistoryRepository.Create(history))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(string id , PatientHistory patientHistory)
        {
            patientHistory.RemoveDoctor();
            patientHistory.RemovePatient();
            patientHistory.RemoveDisease();
            patientHistory.RemoveMedicine();
            return _patientHistoryRepository.Delete(id);
        }

        public IList<PatientHistoryDTO> GetAll()
        {
            IList<PatientHistory> patHistList = _patientHistoryRepository.GetAll().ToList();
            IList<PatientHistoryDTO> patHistDTOList = new List<PatientHistoryDTO>();
            foreach (var pat in patHistList)
            {
                patHistDTOList.Add(Mapper.Map<PatientHistory, PatientHistoryDTO>(pat));
            }
            return patHistDTOList;
        }

        public PatientHistoryDTO GetById(string id)
        {
            PatientHistory pat = _patientHistoryRepository.GetById(id);
            return Mapper.Map<PatientHistory, PatientHistoryDTO>(pat);
        }

        public void Update(PatientHistory patientHistory)
        {
            _patientHistoryRepository.Update(patientHistory);
        }
        public IList<PatientHistoryDTO> GetByPatient(string id)
        {
            var patHistories = _patientHistoryRepository.GetAll();
            if (patHistories == null || patHistories.Count() == 0)
            {
                return null;
            }
            var patHistList = patHistories.Where(x => x != null  && x.patient.Id.Equals(id)).ToList();
            if (patHistList == null || patHistList.Count() == 0)
            {
                return null;
            }
            IList<PatientHistoryDTO> patHistDTOList = new List<PatientHistoryDTO>();
            foreach (var pat in patHistList)
            {
                patHistDTOList.Add(Mapper.Map<PatientHistory, PatientHistoryDTO>(pat));
            }
            return patHistDTOList;
        }
        public IList<PatientHistoryDTO> GetByDoctor(string id)
        {
            var patHistories = _patientHistoryRepository.GetAll();
            if(patHistories == null || patHistories.Count() == 0)
            {
                return null;
            }
            var patHistList = patHistories.Where(x => x != null  && x.doctor.Id.Equals(id)).ToList();
            if (patHistList == null || patHistList.Count() == 0)
            {
                return null;
            }
            IList<PatientHistoryDTO> patHistDTOList = new List<PatientHistoryDTO>();
            foreach (var pat in patHistList)
            {
                patHistDTOList.Add(Mapper.Map<PatientHistory, PatientHistoryDTO>(pat));
            }
            return patHistDTOList;
        }
        public bool DeleteAfterDate(DateTime date)
        {
            IList<PatientHistory> history = GetHistoriesAfterDate(date);
            if(history == null || history.Count == 0)
            {
                return false;
            }
            for(int i = history.Count-1; i >= 0; i--)
            {
                Delete(history.ElementAt(i).Id, history.ElementAt(i));
            }
            return true;
        }
        public IList<PatientHistory> GetHistoriesAfterDate(DateTime date)
        {
            var patHistory = _patientHistoryRepository.GetAll();
            return patHistory.Where(x=>x!= null && x.Date.Date.CompareTo(date) >= 0).ToList();
        }
    }
}