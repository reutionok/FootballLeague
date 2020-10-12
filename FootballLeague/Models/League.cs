using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class League
    {
        public string Name { get; set; }
        public List<Match> Matches { get; set; }

    }
}
