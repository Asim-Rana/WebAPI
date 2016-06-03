using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Services;
using ClinicalWebApi2.DTO;
using ClinicalWebApi2.Filters;

namespace ClinicalWebApi2.Controllers
{
    [NHSessionManagement]
    public class DoctorController : ApiController
    {
        
        private IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Admin")]
        // GET api/doctor
        public IQueryable<DoctorDTO> GetDoctors()
        {
            try
            {
                return _doctorService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        // GET api/doctor/5
        [ResponseType(typeof(DoctorDTO))]
        public IHttpActionResult GetDoctor(string id)
        {
            try
            {
                DoctorDTO doctor = _doctorService.GetById(id);
                if (doctor == null)
                {
                    return NotFound();
                }

                return Ok(doctor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin,Doctor")]
        [ResponseType(typeof(DoctorDTO))]
        public IHttpActionResult GetDoctorByPhone(string phone)
        {
            try
            {
                IQueryable<DoctorDTO> doctors = GetDoctors();
                if (doctors == null || doctors.Count() == 0)
                {
                    return NotFound();
                }
                var doctor = doctors.Where(x => x.Phone.Equals(phone)).Where(x => x != null).FirstOrDefault();
                if (doctor == null)
                {
                    return NotFound();
                }

                return Ok(doctor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST api/doctor
        [Authorize(Roles="Admin")]
        [ResponseType(typeof(Doctor))]
        public IHttpActionResult Post(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _doctorService.Create(doctor);
            }
            catch (Exception w)
            { 
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }

            return CreatedAtRoute("DefaultApi", new { id = doctor.Id }, doctor);
        }

        [Authorize(Roles = "Doctor")]
        // PUT api/doctor/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDoctor(string id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != doctor.Id)
            //{
            //    return BadRequest();
            //}
            try
            {
                int result = _doctorService.Update(id, doctor);
                if (result == 0)
                {
                    return NotFound();
                }
                else if (result == 1)
                {
                    return BadRequest("Error in updation.Please try again later");
                }
                
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        // DELETE api/doctor/5
        [ResponseType(typeof(Doctor))]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                int result = _doctorService.Delete(id);
                if (result == 0)
                {
                    return NotFound();
                }
                else if(result == 1)
                {
                    return BadRequest("Error in deletion.Please try again later");
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w);
            }

            return Ok();
        }
        private bool DoctorExists(string id)
        {
            return _doctorService.GetById(id) != null;
        }
    }
}
