using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace ClinicalWebApi2.DAL.Mappings
{
    public class AppointmentMap : ClassMap<Appointment>
    {
        public AppointmentMap()
        {
            Id(x => x.Id);
            Map(x => x.Date).CustomSqlType("datetime").Not.Nullable();
            Map(x => x.IsProcessed);
            Map(x => x.Token).Not.Nullable();
            References(x => x.patient);
            References(x => x.doctor);
        }
    }
}
