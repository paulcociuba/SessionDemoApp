using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SessionDemoApp.Controllers
{
    public class ValuesController : ApiController
    {
        // GET: api/Values
        public IEnumerable<string> Get()
        {
            //create a date-time object
            DateTime nowDate = DateTime.Now;

            return new string[] { nowDate.ToLongDateString(), nowDate.ToLongTimeString() };
        }

        public string Post([FromBody]string dateParam)
        {
            //create a date-time object
            DateTime nowDate = DateTime.Now;

            //define a string to contain the return value
            string returnString = String.Empty;

            switch (dateParam)
            {
                case "0":
                    //return the entire generated time string
                    returnString = nowDate.ToShortDateString() + " / " + nowDate.ToLongTimeString();
                    break;
                case "1":
                    //return the date of month
                    returnString = "Day of month: " + nowDate.Date.Day.ToString();
                    break;
                case "2":
                    //return the month of the year
                    returnString = "Month of year: " + nowDate.Month.ToString();
                    break;
                case "3":
                    //return the year
                    returnString = "Year: " + nowDate.Year.ToString();
                    break;
                case "4":
                    //return the hour of the day
                    returnString = "Hour: " + nowDate.Hour.ToString(); 
                    break;
                case "5":
                    //return the minute of the hour
                    returnString = "Minutes passed the hour: " + nowDate.Minute.ToString();
                    break;
                case "6":
                    //return the seconds of the minute
                    returnString = "Seconds passed the minute: " + nowDate.Second.ToString();
                    break;
                default:
                    returnString = "Unknown value";
                    break;
            }

            //return what ever  we populated
            return returnString;
        }

    }
}
