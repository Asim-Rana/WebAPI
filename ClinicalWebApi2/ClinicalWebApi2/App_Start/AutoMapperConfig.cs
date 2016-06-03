using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ClinicalWebApi2.Profiles;

namespace ClinicalWebApi2.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppointmentProfile());
                cfg.AddProfile(new DiseaseProfile());
                cfg.AddProfile(new DoctorProfile());
                cfg.AddProfile(new MedicineProfile());
                cfg.AddProfile(new PatientHistoryProfile());
                cfg.AddProfile(new PatientProfile());
                cfg.AddProfile(new AssistantProfile());
            });
        }
    }
}