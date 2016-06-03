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
    public class PatientHistoryController : ApiController
    {
        private IPatientHistoryService _patientHistoryService;

        public PatientHistoryController(IPatientHistoryService patientHistoryService)
        {
            _patientHistoryService = patientHistoryService;
        }

        [Authorize(Roles="Admin,Assistant")]
        // GET api/PatientHistory
        public IQueryable<PatientHistoryDTO> GetPatientHistories()
        {
            try
            {
                return _patientHistoryService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = "Admin,Assistant")]
        // GET api/PatientHistory/5
        [ResponseType(typeof(PatientHistoryDTO))]
        public IHttpActionResult GetPatientHistory(string id)
        {
            PatientHistoryDTO patientHistory = _patientHistoryService.GetById(id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            return Ok(patientHistory);
        }
        [Authorize(Roles = "Patient")]
        [ResponseType(typeof(PatientHistoryDTO))]
        public IHttpActionResult GetPatientHistoryByPatient(string id)
        {
            IList<PatientHistoryDTO> patHistory = _patientHistoryService.GetByPatient(id);
            if (patHistory == null || patHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(patHistory);
        }
        [Authorize(Roles = "Doctor")]
        [ResponseType(typeof(PatientHistoryDTO))]
        public IHttpActionResult GetPatientHistoryByDoctor(string id)
        {
            IList<PatientHistoryDTO> patHistory = _patientHistoryService.GetByDoctor(id);
            if (patHistory == null || patHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(patHistory);
        }
        [Authorize(Roles = "Assistant,Admin")]
        // POST api/PatientHistory
        [ResponseType(typeof(PatientHistory))]
        public IHttpActionResult Post([FromBody]List<PatientHistory> patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _patientHistoryService.Create(patientHistory);
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }
            return Created("DefaultApi", patientHistory);
        }
        [Authorize(Roles = "Admin,Assistant")]
        // PUT api/PatientHistory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatientHistory(string id, PatientHistory patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != patientHistory.Id)
            //{
            //    return BadRequest();
            //}
            if (!PatientHistoryExists(id))
            {
                return NotFound();
            }
            try
            {
                _patientHistoryService.Update(patientHistory);
            }
            catch (Exception)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/PatientHistory/5
        //[ResponseType(typeof(PatientHistory))]
        //public IHttpActionResult Delete(int id)
        //{
        //    PatientHistory patientHistory = _patientHistoryService.GetById(id);
        //    if (patientHistory == null)
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        if(!_patientHistoryService.Delete(id , patientHistory))
        //        {
        //            return BadRequest("Error in deletion. Please try again later");
        //        }
        //    }
        //    catch (Exception w)
        //    {
        //        return InternalServerError(w);
        //    }

        //    return Ok(patientHistory);
        //}
        [Authorize(Roles = "Admin,Assistant")]
        public IHttpActionResult DeleteAfterDate(DateTime date)
        {
            try
            {
                if (!_patientHistoryService.DeleteAfterDate(date))
                {
                    return NotFound();
                }
            }
            catch (Exception w)
            {
                return InternalServerError(w);
            }

            return Ok();
        }
        private bool PatientHistoryExists(string id)
        {
            return _patientHistoryService.GetById(id) != null;
        }
    }
}
