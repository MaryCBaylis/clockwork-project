using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Web;

namespace Clockwork.API.Controllers
{

    public class CurrentTimeController : Controller
    {

        // GET api/currenttime/{timeZoneId}
        [Route("api/[controller]/{timeZoneId}")]
        [HttpGet]
        public IActionResult Get(string timeZoneId)
        {
            timeZoneId = HttpUtility.UrlDecode(timeZoneId);

            var utcTime = DateTime.UtcNow;
            var userTime = ConvertDateToTimeZone(utcTime, timeZoneId);
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = userTime,
                TimeZone = timeZoneId
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }
            }

            return Ok(returnVal);
        }

        //GET api/currenttime/requests
        [Route("api/[controller]/requests")]
        [HttpGet]
        public IActionResult All()
        {
            var db = new ClockworkContext();
 
            return Ok(db.CurrentTimeQueries);
        }

        //GET api/currenttime/timezones
        [Route("api/[controller]/timezones")]
        [HttpGet]
        public IActionResult GetTimezones()
        {
            Dictionary<string, Dictionary<string, string>> regions = new Dictionary<string, Dictionary<string, string>>();

            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                var regionName = z.Id.Split("/")[0];
                if (!regions.ContainsKey(regionName))
                {
                    regions.Add(
                        regionName, new Dictionary<string, string>()
                    );
                }
                if (!regions[regionName].ContainsKey(z.StandardName))
                {
                    regions[regionName].Add(
                        z.StandardName, z.Id
                    );
                }

            }

            var result = new
            {
                regions = regions,
                serverInfo = new {
                    id = TimeZoneInfo.Local.Id,
                    time = DateTime.Now
                }
            };
            return Ok(result);
        }

        //GET api/currenttime/query/{currentimequeryid}
        [Route("api/[controller]/query/{currenttimequeryid}")]
        [HttpGet]
        public IActionResult GetQuery(string currentTimeQueryId)
        {
            CurrentTimeQuery timeQuery;
            using (var db = new ClockworkContext())
            {
                timeQuery = db.CurrentTimeQueries.Find(Int32.Parse(currentTimeQueryId));
            }

            return Ok(timeQuery);
        }

        private DateTime ConvertDateToTimeZone(DateTime userTime, string timeZoneId) 
        {
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime result = TimeZoneInfo.ConvertTimeFromUtc(userTime, timeInfo);

            return result;
        }
    }
}
