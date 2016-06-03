using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ClinicalWebApi2.BusinessEntities;
using ClinicalWebApi2.DTO;

namespace ClinicalWebApi2.Profiles
{
    public class PatientProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Patient, PatientDTO>();
        }
    }
}