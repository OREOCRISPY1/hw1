using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCrew
    {
        public String Department { get; set; }
        public String Job { get; set; }
        public int CrewId { get; set; }
        public int MovieId { get; set; }
        public Crew Crew { get; set; }
        public Movie Movie { get; set; }
    }
}
