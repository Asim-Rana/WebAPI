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
    public interface IAssistantService
    {
        IList<AssistantDTO> GetAll();
        AssistantDTO GetById(string id);
        bool Create(Assistant assistant);
        int Update(string id, Assistant assistant);
        int Delete(string id);
        AssistantDTO GetByPhone(string phone);
    }
    public class AssistantService : IAssistantService
    {
        private IRepository<Assistant> _assistantRepository;
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
        public AssistantService(IRepository<Assistant> assistantRepository)
        {
            _assistantRepository = assistantRepository;
        }
        public bool Create(Assistant assistant)
        {
            return _assistantRepository.Create(assistant);
        }

        public int Delete(string id)
        {
            Assistant assistant = _assistantRepository.GetById(id);
            if (assistant == null)
            {
                return 0;
            }
            if (!_assistantRepository.Delete(id))
            {
                return 1;
            }
            return 2;
        }

        public IList<AssistantDTO> GetAll()
        {
            IList<Assistant> asstList = _assistantRepository.GetAll().ToList();
            IList<AssistantDTO> asstDTOList = new List<AssistantDTO>();
            foreach (var asst in asstList)
            {
                asstDTOList.Add(Mapper.Map<Assistant, AssistantDTO>(asst));
            }
            return asstDTOList;
        }

        public AssistantDTO GetById(string id)
        {
            Assistant asst = _assistantRepository.GetById(id);
            return Mapper.Map<Assistant, AssistantDTO>(asst);
        }

        public int Update(string id, Assistant assistant)
        {
            Assistant asst = _assistantRepository.GetById(id);
            if (asst == null)
            {
                return 0;
            }
            asst.Name = assistant.Name;
            asst.Phone = assistant.Phone;
            asst.Age = assistant.Age;
            asst.Education = assistant.Education;
            asst.Address = assistant.Address;
            if (!_assistantRepository.Update(asst))
            {
                return 1;
            }
            return 2;
        }
        public AssistantDTO GetByPhone(string phone)
        {
            Assistant asst = _assistantRepository.GetEntityByPhone(ExpressionFilter.CreateContainsExpression<Assistant>(phone));
            return Mapper.Map<Assistant, AssistantDTO>(asst);
        }
    }
}
