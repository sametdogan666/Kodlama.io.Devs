using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Technology> Technologies { get; set; }
        public ProgrammingLanguage()
        {

        }

        public ProgrammingLanguage(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}
