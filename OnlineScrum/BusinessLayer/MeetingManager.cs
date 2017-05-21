using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineScrum.BusinessLayer
{
    public class MeetingManager
    {
        public static string AddMeeting(Meeting meeting, int sprintID)
        {
            if (meeting == null)
            {
                return "Error when adding. Please try again";
            }
            else
            {
                try
                {
                    meeting.SprintID = sprintID;
                    using (var context = new DatabaseContext())
                    {
                        if (!UserManager.CheckExistingEmail(meeting.ScrumMaster))
                            return meeting.ScrumMaster + " does not exist";
                        if (!UserManager.CheckExistingEmail(meeting.Developer))
                            return meeting.Developer + " does not exist";

                        if (meeting.Time < DateTime.Now)
                        {
                            return "Time for meeting invalid";
                        }
                        if (meeting.Time.DayOfWeek == DayOfWeek.Saturday || meeting.Time.DayOfWeek == DayOfWeek.Sunday)
                            return "Not a week day";

                        context.Meetings.Add(meeting);
                        context.SaveChanges();
                        return "";
                    }
                }
                catch (Exception e)
                {
                    SharedManager.Log(e, "AddMeeting");
                    return SharedManager.DatabaseError;
                }
            }
        }

        public static List<Meeting> GetMeetingsByEmail(string email, int sprintID)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var meetingList = (from meeting in context.Meetings
                                       where (meeting.Developer == email || meeting.ScrumMaster == email) && meeting.SprintID == sprintID
                                       select meeting).ToList();

                    return meetingList;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetMeetingsByEmail");
                return new List<Meeting> { };
            }
        }

        public static string AddMeetingQuestions(int id, Meeting meeting)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var meetingRet = (from meet in context.Meetings
                                       where (meet.MeetingID == meeting.MeetingID) && meet.SprintID == id
                                       select meet).First();

                    meetingRet.TodayQuestion = meeting.TodayQuestion;
                    meetingRet.YesterdayQuestion = meeting.YesterdayQuestion;
                    meetingRet.ObstaclesQuestion = meeting.ObstaclesQuestion;

                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddMeetingQuestions");
                return SharedManager.DatabaseError;
            }
        }

    }
}