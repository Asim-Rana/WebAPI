using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DTO;
using ClinicalWebApi2.Filters;
using ClinicalWebApi2.DAL.Services;

namespace ClinicalWebApi2.Controllers
{
    [NHSessionManagement]
    public class AssistantController : ApiController
    {

        private IAssistantService _assistantService;

        public AssistantController(IAssistantService assistantService)
        {
            _assistantService = assistantService;
        }

        [Authorize(Roles = "Admin")]
        public IQueryable<AssistantDTO> GetAssistants()
        {
            try
            {
                return _assistantService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Authorize(Roles = "Admin,Assistant")]
        [ResponseType(typeof(AssistantDTO))]
        public IHttpActionResult GetAssistant(string id)
        {
            try
            {
                AssistantDTO assistant = _assistantService.GetById(id);
                if (assistant == null)
                {
                    return NotFound();
                }

                return Ok(assistant);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin,Assistant")]
        [ResponseType(typeof(AssistantDTO))]
        public IHttpActionResult GetAssistantByPhone(string phone)
        {
            try
            {
                IQueryable<AssistantDTO> assistants = GetAssistants();
                if (assistants == null || assistants.Count() == 0)
                {
                    return NotFound();
                }
                var assistant = assistants.Where(x => x.Phone.Equals(phone)).Where(x => x != null).FirstOrDefault();
                if (assistant == null)
                {
                    return NotFound();
                }

                return Ok(assistant);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Assistant))]
        public IHttpActionResult Post(Assistant assistant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _assistantService.Create(assistant);
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }

            return CreatedAtRoute("DefaultApi", new { id = assistant.Id }, assistant);
        }

        [Authorize(Roles = "Assistant")]
        public IHttpActionResult PutAssistant(string id, Assistant assistant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                int result = _assistantService.Update(id, assistant);
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
        public IHttpActionResult Delete(string id)
        {
            try
            {
                int result = _assistantService.Delete(id);
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
        private bool AssistantExists(string id)
        {
            return _assistantService.GetById(id) != null;
        }
    }
}
