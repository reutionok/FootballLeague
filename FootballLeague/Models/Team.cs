using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string LeagueName { get; set; }
        public int Goals { get; set; }
        public int MissedGoals { get; set; }
   

    }
}
