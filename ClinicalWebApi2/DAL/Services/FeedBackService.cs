using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DAL.Repositories;

namespace DAL.Services
{
    public interface IFeedBackService
    {
        IList<FeedBack> GetAll();
        FeedBack GetById(string id);
        bool Create(FeedBack feedBack);
        bool Update(List<FeedBack> feedback);
        int Delete(string id);
        bool Reset();
    }
    public class FeedBackService : IFeedBackService
    {
        private IRepository<FeedBack> _feedBackRepository;
        public FeedBackService(IRepository<FeedBack> feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }
        public bool Create(FeedBack feedBack)
        {
            feedBack.Excellent = 0;
            feedBack.Good = 0;
            feedBack.Satisfactory = 0;
            feedBack.DontKnow = 0;
            feedBack.Id = Guid.NewGuid().ToString();
            return _feedBackRepository.Create(feedBack);
        }

        public int Delete(string id)
        {
            FeedBack feedback = _feedBackRepository.GetById(id);
            if (feedback == null)
            {
                return 0;
            }
            if (!_feedBackRepository.Delete(id))
            {
                return 1;
            }
            return 2;
        }

        public IList<FeedBack> GetAll()
        {
            return _feedBackRepository.GetAll().ToList();
        }

        public FeedBack GetById(string id)
        {
            return _feedBackRepository.GetById(id); 
        }

        public bool Reset()
        {
            try
            {
                IList<FeedBack> feedBackList = _feedBackRepository.GetAll().ToList();
                foreach (var feedback in feedBackList)
                {
                    feedback.Excellent = 0;
                    feedback.Good = 0;
                    feedback.Satisfactory = 0;
                    feedback.DontKnow = 0;
                    _feedBackRepository.Update(feedback);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(List<FeedBack> feedback)
        {
            try
            {
                foreach (var feb in feedback)
                {
                    FeedBack fb = _feedBackRepository.GetById(feb.Id);
                    if (fb != null)
                    {
                        fb.Excellent = fb.Excellent + feb.Excellent;
                        fb.Good = fb.Good + feb.Good;
                        fb.Satisfactory = fb.Satisfactory + feb.Satisfactory;
                        fb.DontKnow = fb.DontKnow + feb.DontKnow;
                        _feedBackRepository.Update(fb);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
    }
}
