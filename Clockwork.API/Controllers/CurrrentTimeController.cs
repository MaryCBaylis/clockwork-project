﻿using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;

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
        public IActionResult GetAll()
        {
            var db = new ClockworkContext();
 
            return Ok(db.CurrentTimeQueries);
        }

    }
}
