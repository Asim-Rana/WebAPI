using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.Filters;
using DAL.Services;

namespace ClinicalWebApi2.Controllers
{
    [NHSessionManagement]
    [RoutePrefix("FeedBack")]
    public class FeedBackController : ApiController
    {
        private IFeedBackService _feedBackService;

        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        [Authorize(Roles = "Admin")]
        // GET api/feedback
        public IQueryable<FeedBack> GetFeedBacks()
        {
            try
            {
                return _feedBackService.GetAll().AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Authorize(Roles = "Admin")]

        [ResponseType(typeof(FeedBack))]
        public IHttpActionResult GetFeedBack(string id)
        {
            try
            {
                FeedBack feedBack = _feedBackService.GetById(id);
                if (feedBack == null)
                {
                    return NotFound();
                }

                return Ok(feedBack);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(FeedBack))]
        public IHttpActionResult Post(FeedBack feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _feedBackService.Create(feedback);
            }
            catch (Exception w)
            {
                return InternalServerError(w.InnerException != null ? w.InnerException : w);
            }

            return CreatedAtRoute("DefaultApi", new { id = feedback.Id }, feedback);
        }
        [Authorize(Roles = "Patient")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedBack(List<FeedBack> feedbacks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = _feedBackService.Update(feedbacks);
                if (result == false)
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
                int result = _feedBackService.Delete(id);
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
        [Authorize(Roles = "Admin")]
        [Route("Reset")]
        public IHttpActionResult ResetFeedBacks()
        {
            if(_feedBackService.Reset())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
