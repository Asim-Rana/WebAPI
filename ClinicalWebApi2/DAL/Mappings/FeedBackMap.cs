using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicalWebApi2.BusinessEntities;
using FluentNHibernate.Mapping;

namespace DAL.Mappings
{
    class FeedBackMap : ClassMap<FeedBack>
    {
        public FeedBackMap()
        {
            Id(x => x.Id);
            Map(x=>x.Question).Not.Nullable().Length(100);
            Map(x=>x.Excellent).Not.Nullable();
            Map(x => x.Good).Not.Nullable();
            Map(x => x.Satisfactory).Not.Nullable();
            Map(x => x.DontKnow).Not.Nullable();
        }
    }
}
