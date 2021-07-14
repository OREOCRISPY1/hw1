using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Crew
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Gender { get; set; }
        public String TmdbUrl { get; set; }
        public String ProfilePath { get; set; }
        public ICollection<MovieCrew> MovieCrews { get; set; }
    }
}
