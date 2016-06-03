using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace ClinicalWebApi2.DAL.Mappings
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Length(40).Not.Nullable();
            Map(x => x.Phone).Not.Nullable().Unique();
            Map(x => x.Age).Not.Nullable();
            Map(x => x.Address).Not.Nullable();
            HasMany(x => x.appointments).Cascade.AllDeleteOrphan().LazyLoad().Inverse(); 
            HasMany(x=>x.CheckupHistory).Cascade.AllDeleteOrphan().LazyLoad().Inverse(); 
        }
    }
}
