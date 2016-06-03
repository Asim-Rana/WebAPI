using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Helpers;
using ClinicalWebApi2.DAL.Services;
using ClinicalWebApi2.DTO;
using ClinicalWebApi2.Filters;
using Ninject;

namespace ClinicalWebApi2.Controllers
{
    [NHSessionManagement]
    public class AppointmentController : ApiController
    {
        private IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles="Admin,Assistant")]
        // GET api/Appointment
        public IQueryable<AppointmentDTO> GetAppointments()
        {
            try
            {
                return _appointmentService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = "Admin,Assistant")]
        // GET api/Appointment/5
        [ResponseType(typeof(AppointmentDTO))]
        public IHttpActionResult Get(string id)
        {
            AppointmentDTO appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
        [Authorize(Roles = "Doctor")]
        [ResponseType(typeof(AppointmentDTO))]
        public IHttpActionResult GetAppointmentByDoctor(string id)
        {
            IList<AppointmentDTO> appointment = _appointmentService.GetAppointment(id, 1);
            if (appointment == null || appointment.Count == 0)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
        [Authorize(Roles = "Patient")]
        [ResponseType(typeof(AppointmentDTO))]
        public IHttpActionResult GetAppointmentByPatient(string id)
        {
            IList<AppointmentDTO> appointment = _appointmentService.GetAppointment(id, 3);
            if (appointment == null || appointment.Count == 0)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
        [Authorize(Roles = "Assistant")]
        [ResponseType(typeof(AppointmentDTO))]
        public IHttpActionResult GetAppointmentByToken(int id)
        {
            IList<AppointmentDTO> appointment = _appointmentService.GetAppointment(id, 2);
            if (appointment == null || appointment.Count == 0)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
        [Authorize(Roles = "Patient")]
        // POST api/Appointment
        [ResponseType(typeof(AppointmentDTO))]
        public IHttpActionResult Post(Appointment appointment , string docId , string patId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int res = _appointmentService.Create(appointment, docId, patId);
                if (res == 2)
                {
                    return BadRequest("No empty slot Found, Plz Select another date");
                }
                else if (res == 3)
                {
                    return BadRequest("No such doctor or patient is available for appointment");
                }
                else if (res == 4)
                {
                    return BadRequest("You cannot take more than one appointment on same day");
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w);
            }
            AppointmentDTO appDto = _appointmentService.ConvertDTO(appointment);
            return CreatedAtRoute("DefaultApi", new { id = appDto.Id }, appDto);
        }
        [Authorize(Roles = "Assistant,Doctor")]
        // PUT api/Appointment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointmentStatus(string id)
        {
            try
            {
                int result = _appointmentService.Update(id);
                if(result == 0)
                {
                    return NotFound();
                }
                else if (result == 1)
                {
                    return BadRequest("Error in updation. Please try again later");
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize(Roles = "Admin,Assistant")]
        // DELETE api/Appointment/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                int result = _appointmentService.Delete(id);
                if(result == 0)
                {
                    return NotFound();
                }
                else if (result == 1)
                {
                    return BadRequest("Error in deletion. Please try again later");
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w);
            }

            return Ok();
        }
        private bool AppointmentExists(string id)
        {
            return _appointmentService.GetById(id) != null;
        }
    }
}
