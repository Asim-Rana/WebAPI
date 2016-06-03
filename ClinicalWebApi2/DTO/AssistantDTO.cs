using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalWebApi2.DTO
{
    public class AssistantDTO
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Education { get; set; }

        public virtual int Age { get; set; }

        public virtual string Phone { get; set; }
    }
}
