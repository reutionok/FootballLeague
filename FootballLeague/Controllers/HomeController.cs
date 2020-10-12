using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FootballLeague.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetLeagues()
        {
            JSONReadWrite readWrite = new JSONReadWrite();
            List<League> leagues = JsonConvert.DeserializeObject<List<League>>(readWrite.Read("en.1.json", "data"));
            leagues.AddRange(JsonConvert.DeserializeObject<List<League>>(readWrite.Read("en.2.json", "data")));
            // leagues.AddRange(JsonConvert.DeserializeObject<List<League>>(readWrite.Read("en.3.json", "data")));
            return View(leagues);

        }
    }
}
