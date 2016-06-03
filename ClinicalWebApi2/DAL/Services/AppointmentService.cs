using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Repositories;
using ClinicalWebApi2.DTO;

namespace ClinicalWebApi2.DAL.Services
{
    public interface IAppointmentService
    {
        IList<AppointmentDTO> GetAll();
        AppointmentDTO GetById(string id);
        int Create(Appointment appointment, string docId, string patId);
        int Update(string id);
        int Delete(string id);
        IList<AppointmentDTO> GetAppointment<T>(T id , int criteria);
        AppointmentDTO ConvertDTO(Appointment appointment);
    }
    public class AppointmentService : IAppointmentService
    {
        private IRepository<Appointment> _appointmentRepository;
        private IRepository<Doctor> _doctorRepository;
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
        public AppointmentService(IRepository<Appointment> appointmentRepository , IRepository<Doctor> doctorRepository , IRepository<Patient> patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }
        public int Create(Appointment appointment, string docId, string patId)
        {
            int res = MakeAppointmentSchedule(appointment, docId, patId);
            if (res == 1)
            {
                appointment.Id = Guid.NewGuid().ToString();
                _appointmentRepository.Create(appointment);
            }
            return res;
        }

        public int Delete(string id)
        {
            Appointment appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return 0;
            }
            appointment.RemoveDoctor();
            appointment.RemovePatient();
            if(!_appointmentRepository.Delete(id))
            {
                return 1;
            }
            return 2;
        }

        public IList<AppointmentDTO> GetAll()
        {
            IList<Appointment> appList = _appointmentRepository.GetAll().ToList();
            IList<AppointmentDTO> appDTOList = new List<AppointmentDTO>();
            foreach (var app in appList)
            {
                appDTOList.Add(Mapper.Map<Appointment, AppointmentDTO>(app));
            }
            return appDTOList;
        }

        public AppointmentDTO GetById(string id)
        {
            Appointment app = _appointmentRepository.GetById(id);
            return Mapper.Map<Appointment, AppointmentDTO>(app);
        }
        public AppointmentDTO ConvertDTO(Appointment appointment)
        {
            return Mapper.Map<Appointment, AppointmentDTO>(appointment);
        }
        public int Update(string id)
        {
            Appointment app = _appointmentRepository.GetById(id);
            if(app == null)
            {
                return 0;
            }
            app.IsProcessed = true;
            if(!_appointmentRepository.Update(app))
            {
                return 1;
            }
            return 2;
        }
        int MakeAppointmentSchedule(Appointment appointment, string docId, string patId)
        {
            var appList = _appointmentRepository.GetAll()
                .Where(x => x != null && x.Date.Date.CompareTo(appointment.Date.Date) == 0)
                .ToList();
            if (appList.Any(x => x.patient.Id.Equals(patId)))
            {
                return 4;
            }
            Appointment recentAppointment = appList.Where(x=>x != null && x.doctor.Equals(docId)).OrderByDescending(x=>x.Date.TimeOfDay).FirstOrDefault();

            DateTime d;
            if (recentAppointment != null)
            {
                d = new DateTime(recentAppointment.Date.Year , recentAppointment.Date.Month , recentAppointment.Date.Day , recentAppointment.Date.Hour , recentAppointment.Date.Minute , recentAppointment.Date.Second);
                d = d.AddMinutes(15);
                DateTime lastSlot = new DateTime(d.Year, d.Month, d.Day, 22 , 0 , 0);
                if (d.CompareTo(lastSlot) > 0)
                {
                    return 2;
                }
                appointment.Date = d;
                appointment.IsProcessed = false;
                appointment.Token = recentAppointment.Token + 1;
            }
            else
            {
                d = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, 18, 0, 0);
                appointment.Date = d;
                appointment.IsProcessed = false;
                appointment.Token = 1;
            }
            var doctor = _doctorRepository.GetById(docId);
            var patient = _patientRepository.GetById(patId);
            if(doctor == null || patient == null)
            {
                return 3;
            }
            appointment.AddDoctor(doctor);
            appointment.AddPatient(patient);
            return 1;
        }
        public IList<AppointmentDTO> GetAppointment<T>(T id , int criteria)
        {
            IList<Appointment> appList = _appointmentRepository.GetAppointment(id, criteria);
            IList<AppointmentDTO> appDTOList = new List<AppointmentDTO>();
            foreach (var app in appList)
            {
                appDTOList.Add(Mapper.Map<Appointment, AppointmentDTO>(app));
            }
            return appDTOList;
        }
    }
}
