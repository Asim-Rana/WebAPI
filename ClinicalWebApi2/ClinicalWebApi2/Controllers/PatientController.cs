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
    public class PatientController : ApiController
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize(Roles="Admin")]
        // GET api/Patient
        public IQueryable<PatientDTO> GetPatients()
        {
            try
            {
                return _patientService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "Admin,Patient")]
        // GET api/Patient/5
        [ResponseType(typeof(PatientDTO))]
        public IHttpActionResult GetPatient(string id)
        {
            PatientDTO patient = _patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        [Authorize(Roles = "Admin,Patient")]
        [ResponseType(typeof(PatientDTO))]
        public IHttpActionResult GetPatientByPhone(string phone)
        {
            PatientDTO patient = _patientService.GetByPhone(phone);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        // POST api/Patient
        [ResponseType(typeof(Patient))]
        public IHttpActionResult Post(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(!_patientService.Create(patient))
                {
                    return BadRequest("Error in creation.Please try again later");
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        [Authorize(Roles = "Patient")]
        // PUT api/Patient/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(string id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != patient.Id)
            //{
            //    return BadRequest();
            //}
            try
            {
                int result = _patientService.Update(id, patient);
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

        [Authorize(Roles = "Admin,Patient")]
        // DELETE api/Patient/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                int result = _patientService.Delete(id);
                if (result == 0)
                {
                    return NotFound();
                }
                else if (result == 1)
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
        private bool PatientExists(string id)
        {
            return _patientService.GetById(id) != null;
        }
    }
}
