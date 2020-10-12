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
       
    }
}
