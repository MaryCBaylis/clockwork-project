using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using NodaTime;
using TimeZoneNames;

namespace Clockwork.API.Controllers
{

    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult Get()
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
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
        public IActionResult getTimezones()
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
            return Ok(regions);
        }

        public string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

    }
}
