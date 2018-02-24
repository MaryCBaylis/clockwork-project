using System;
using System.Collections.Generic;
namespace Clockwork.API.Helpers
{
    public class TimeZoneHelper
    {
        Dictionary<string, string> timeZones;

        public TimeZoneHelper() {
            timeZones = new Dictionary<string, string>();

            //The 39 local times obtained from https://www.timeanddate.com/time/current-number-time-zones.html
            //This will change after daylight savings time starts!
            timeZones.Add("Pacific/Kiritimati", "LINT (Kirimati)");
            timeZones.Add("Pacific/Chatham", "CHADT (Chatham Islands)");
            timeZones.Add("Pacific/Auckland", "NZDT (Auckland)");
            timeZones.Add("Asia/Anadyr", "ANAT (Anadyr)");
            timeZones.Add("Australia/Melbourne", "AEDT (Melbourne)");
            timeZones.Add("Australia/Adelaide", "ACDT (Adelaide)");
            timeZones.Add("Australia/Brisbane", "AEST (Brisbane)");
            timeZones.Add("Australia/Darwin", "ACST (Darwin)");
            timeZones.Add("Asia/Tokyo", "JST (Tokyo)");
            timeZones.Add("Australia/Eucla", "ACWST (Eucla)");
            timeZones.Add("Asia/Pyongyang", "PYT (Pyongyang)");
            timeZones.Add("Asia/Shanghai", "CST (Beijing");
            timeZones.Add("Asia/Jakarta", "WIB (Jakarta)");
            timeZones.Add("Asia/Yangon", "MMT (Yangon)");
            timeZones.Add("Asia/Dhaka", "BST (Dhaka)");
            timeZones.Add("Asia/Kathmandu", "IST (New Delhi)");
            timeZones.Add("Asia/Tashkent", "UZT (Tashkent)");
            timeZones.Add("Asia/Kabul", "AFT (Kabul)");
            timeZones.Add("Asia/Dubai", "GST (Dubai)");
            timeZones.Add("Asia/Tehran", "IRST (Tehran)");
            timeZones.Add("Europe/Moscow", "MSK (Moscow)");
            timeZones.Add("Africa/Cairo", "EET (Cairo)");
            timeZones.Add("Europe/Brussels", "CET (Brussels)");
            timeZones.Add("Europe/London", "GMT (London)");
            timeZones.Add("Atlantic/Cape_Verde", "CVT (Praia");
            timeZones.Add("Atlantic/South_Georgia", "GST (King Edward Point)");
            timeZones.Add("America/Santiago", "ART (Buenos Aires");
            timeZones.Add("America/St_Johns", "NST (St. John's");
            timeZones.Add("America/Caracas", "VET (Caracas)");
            timeZones.Add("America/New_York", "EST (NewYork)");
            timeZones.Add("America/Mexico_City", "CST (Mexico City");
            timeZones.Add("America/Phoenix", "MST (Calgary)");
            timeZones.Add("America/Los_Angeles", "PST (Los Angeles");
            timeZones.Add("America/Anchorage", "AKST (Anchorage)");


        }

        //const Dictionary<string, string> TimeZones = new Dictionary<string, string>()

        //public static string GetTimeZoneList() {
        //    return "hi";
        //}
    }
}
