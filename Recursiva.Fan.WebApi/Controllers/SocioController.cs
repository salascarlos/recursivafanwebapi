using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recursiva.Fan.WebApi.Data;
using Recursiva.Fan.WebApi.Models;

namespace Recursiva.Fan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioController : ControllerBase
    {
        private readonly FanContext _fanContext;

        public SocioController(FanContext fanContext)
        {
            _fanContext = fanContext;
        }

        [HttpGet]
        [Route("Quantity")]
        public IActionResult GetSociosQuantity()
        {
            int quantity = _fanContext.Socios.Local.Count();
            return Ok(quantity);
        }

        [HttpGet]
        [Route("Average/{team}")]
        public IActionResult GetSociosAverage(string team)
        {
            double average = _fanContext.Socios.Local.Where(x => x.Team.Contains(team)).Average(y => y.Age);
            return Ok(average);
        }

        [HttpGet]
        [Route("Married/{quantity}")]
        public IActionResult GetSociosMarried(int quantity)
        {
            var socios = _fanContext.Socios.Local.Where(x => x.Status.Contains("Casado") && x.Level.Contains("Universitario")).Take(quantity).OrderBy(y => y.Age).Select(x => new { Name = x.Name, Age = x.Age, Team = x.Team }).ToList();
            return Ok(socios);
        }

        [HttpGet]
        [Route("MostCommon/{team}/{quantity}")]
        public IActionResult GetSociosNameMostCommon(string team, int quantity)
        {
            var names = _fanContext.Socios.Local.Where(x => x.Team.Contains(team)).GroupBy(x => x.Name).Take(quantity).Select(g => new { Name = g.Key, Count = g.Count() });
            return Ok(names);
        }

        [HttpGet]
        [Route("Teams")]
        public IActionResult GetTeamData()
        {
            var teams = _fanContext.Socios.Local.GroupBy(x => x.Team).Select(g => new { Name = g.Key, Count = g.Count(), avgAge = g.Average(p => p.Age), maxAge = g.Max(p => p.Age), minAge = g.Min(p => p.Age) }).OrderByDescending(x => x.Count);
            return Ok(teams);
        }





    }
}
