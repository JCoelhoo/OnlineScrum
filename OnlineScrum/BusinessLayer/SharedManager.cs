using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OnlineScrum.Models;

namespace OnlineScrum.BusinessLayer
{
    public class SharedManager
    {
        public static DateTime DailyMeetingTime { get; set; } =
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

        public static bool RepeatMethod { get; set; }
        public static int MeetingInterval { get; set; } = 1;


        public static string DatabaseError { get { return DatabaseError; } }

        public static void Log(Exception e, string function)
        {
            using (var sw = File.AppendText(Path.Combine(HttpContext.Current.Server.MapPath("~"), "log.txt")))
            {
                sw.Write(DateTime.Now);
                sw.Write("  ----  ");
                sw.WriteLine(function);
                sw.Write(e.GetBaseException());
                sw.Write(" ----> ");
                sw.WriteLine(e.Message);
                sw.Write("\n\n");
            }
        }

        public static List<string> SplitString(string s)
        {
            return s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        //TODO MeetingInterval
        public static void DailyScrumMeeting(Project project, Sprint sprint, bool timeTrigger = true)
        {
            if (!timeTrigger && RepeatMethod)
            {
                return;
            }
            var meetings = MeetingManager.GetMeetingsByEmail(project.ScrumMaster, sprint.SprintID);
            foreach (var member in SharedManager.SplitString(project.DevTeam))
            {
                var tomorrow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0).AddDays(1);
                if (meetings.Count(
                        m => m.Time == tomorrow
                             && m.Developer == member) != 0) continue;
                var meeting = new Meeting
                {
                    Developer = member,
                    Location = sprint.MeetingLocation,
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0).AddDays(1),
                    ScrumMaster = project.ScrumMaster,
                    SprintID = sprint.SprintID
                };
                MeetingManager.AddMeeting(meeting, sprint.SprintID);
            }
            RepeatMethod = true;
        }
    }

    public static class Extension
    {
        public static int BusinessDaysUntil(this DateTime firstDay, DateTime lastDay)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;



            return businessDays;
        }
    }
}