using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace ClinicalWebApi2.DAL.Mappings
{
    public class DoctorMap : ClassMap<Doctor>
    {
        public DoctorMap()
        {
            Id(x => x.Id);
            Map(x=>x.Name).Length(40).Not.Nullable();
            Map(x=>x.Education).Not.Nullable();
            Map(x=>x.Speciality).Not.Nullable();
            Map(x=>x.Phone).Not.Nullable().Unique();
            Map(x => x.Address).Not.Nullable();
            Map(x => x.Gender).Not.Nullable();
            HasMany(x=>x.AssignedAppointments).Cascade.All().LazyLoad().Inverse();
            HasMany(x=>x.ExaminedPatients).Cascade.All().LazyLoad().Inverse();
        }
    }
}
