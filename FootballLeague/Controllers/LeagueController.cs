using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FootballLeague.Controllers
{
    public class LeagueController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }



        public IActionResult GetBestDay()
        {
            JSONReadWrite readWrite = new JSONReadWrite();
            string[] files = new string[] { "en.1.json", "en.2.json", "en.3.json" };
            List<League> leagues = new List<League>();
            List<Match> matches = new List<Match>();
            List<Match> bestDays = new List<Match>();
            for (int l = 0; l < 3; l++)
            {
                leagues = JsonConvert.DeserializeObject<List<League>>(readWrite.Read(files[l], "data"));

                for (int j = 0; j < 1; j++)
                {

                    for (int i = 0; i < leagues[j].Matches.Count; i++)
                    {
                        matches.Add(new Match { leagueName=leagues[j].Name, Round=leagues[j].Matches[i].Round, Date= leagues[j].Matches[i].Date, Team1= leagues[j].Matches[i].Team1, Team2= leagues[j].Matches[i].Team2, Score= leagues[j].Matches[i].Score });
                    }
                }
                bestDays.AddRange(matches.Where(m => m.Score.Ft[0] + m.Score.Ft[1] == matches.Max(m => m.Score.Ft[0] + m.Score.Ft[1])));
                matches.Clear();
            }

                return View(bestDays);
        }

        public IActionResult GetBestAttackTeam()
        {
            JSONReadWrite readWrite = new JSONReadWrite();
            string[] files = new string[] { "en.1.json", "en.2.json", "en.3.json" };
            List<League> leagues = new List<League>();
            List<Match> matches = new List<Match>();
            List<Team> teams = new List<Team>();
            List<Team> bestTeams = new List<Team>();

            for (int l = 0; l < 3; l++)
            {
                leagues = JsonConvert.DeserializeObject<List<League>>(readWrite.Read(files[l], "data"));

                for (int j = 0; j < 1; j++)
                {

                    for (int i = 0; i < leagues[j].Matches.Count; i++)
                    {
                        matches.Add(leagues[j].Matches[i]);
                    }
                }

                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team1);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[0];
                        newTeam.MissedGoals = item.Score.Ft[1];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }
                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team2);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[1];
                        newTeam.MissedGoals = item.Score.Ft[0];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }

                bestTeams.AddRange(teams.Where(t => t.Goals == teams.Max(t => t.Goals))
                    .Select(t => new Team { Name = t.Name, LeagueName = t.LeagueName }));
                teams.Clear();
                matches.Clear();
            }
            return View("GetBestTeam", bestTeams);
        }

        public IActionResult GetBestProtectionTeam()
        {
            JSONReadWrite readWrite = new JSONReadWrite();
            string[] files = new string[] { "en.1.json", "en.2.json", "en.3.json" };
            List<League> leagues = new List<League>();
            List<Match> matches = new List<Match>();
            List<Team> teams = new List<Team>();
            List<Team> bestTeams = new List<Team>();

            for (int l = 0; l < 3; l++)
            {
                leagues = JsonConvert.DeserializeObject<List<League>>(readWrite.Read(files[l], "data"));

                for (int j = 0; j < 1; j++)
                {

                    for (int i = 0; i < leagues[j].Matches.Count; i++)
                    {
                        matches.Add(leagues[j].Matches[i]);
                    }
                }

                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team1);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[0];
                        newTeam.MissedGoals = item.Score.Ft[1];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }
                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team2);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[1];
                        newTeam.MissedGoals = item.Score.Ft[0];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }

                bestTeams.AddRange(teams.Where(t => t.MissedGoals == teams.Min(t => t.MissedGoals))
                    .Select(t => new Team { Name = t.Name, LeagueName = t.LeagueName }));
                teams.Clear();
                matches.Clear();
            }
            return View("GetBestTeam", bestTeams);
        }

        public IActionResult GetBestTeam()
        {
            JSONReadWrite readWrite = new JSONReadWrite();
            string[] files = new string[] { "en.1.json", "en.2.json", "en.3.json" };
            List<League> leagues = new List<League>();
            List<Match> matches = new List<Match>();
            List<Team> teams = new List<Team>();
            List<Team> bestTeams = new List<Team>();
            List<Team> tempTeams = new List<Team>();

            for (int l = 0; l < 3; l++)
            {
                leagues = JsonConvert.DeserializeObject<List<League>>(readWrite.Read(files[l], "data"));

                for (int j = 0; j < 1; j++)
                {

                    for (int i = 0; i < leagues[j].Matches.Count; i++)
                    {
                        matches.Add(leagues[j].Matches[i]);
                    }
                }

                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team1);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[0];
                        newTeam.MissedGoals = item.Score.Ft[1];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }
                foreach (var item in matches)
                {
                    var currentTeam = teams.Find(t => t.Name == item.Team2);
                    if (currentTeam != null)
                    {
                        currentTeam.Goals += item.Score.Ft[0];
                        currentTeam.MissedGoals += item.Score.Ft[1];
                        currentTeam.LeagueName = leagues[0].Name;
                    }
                    else
                    {

                        var newTeam = new Team();
                        newTeam.Name = item.Team1;
                        newTeam.Goals = item.Score.Ft[1];
                        newTeam.MissedGoals = item.Score.Ft[0];
                        newTeam.LeagueName = leagues[0].Name;
                        teams.Add(newTeam);
                    }
                }
                
                tempTeams.AddRange(teams.Where(t => t.Goals - t.MissedGoals == teams.Max(t => t.Goals - t.MissedGoals))
                    .Select(t=> new Team { Name = t.Name, LeagueName = t.LeagueName, Goals=t.Goals, MissedGoals=t.MissedGoals}));
                bestTeams.AddRange(tempTeams.Where(t => t.Goals == tempTeams.Max(t => t.Goals)));
             
                teams.Clear();
                matches.Clear();
                tempTeams.Clear();
            }
                return View(bestTeams);
            }
        }
    }

    

