using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace ClinicalWebApi2.DAL.Mappings
{
    public class PatientHistoryMap : ClassMap<PatientHistory>
    {
        public PatientHistoryMap()
        {
            Id(x => x.Id);
            Map(x => x.Date).CustomSqlType("datetime").Not.Nullable();
            References(x => x.patient);
            References(x => x.doctor);
            HasMany(x=>x.diseases).Cascade.AllDeleteOrphan().LazyLoad().Inverse();
            HasMany(x => x.medicines).Cascade.AllDeleteOrphan().LazyLoad().Inverse();
        }
    }
}
