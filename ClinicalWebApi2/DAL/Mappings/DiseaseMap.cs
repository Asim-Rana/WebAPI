using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace ClinicalWebApi2.DAL.Mappings
{
    public class DiseaseMap : ClassMap<Disease>
    {
        public DiseaseMap()
        {
            Id(x => x.Id);
            Map(x=>x.Value);
            References(x=>x.history);
        }
    }
}
