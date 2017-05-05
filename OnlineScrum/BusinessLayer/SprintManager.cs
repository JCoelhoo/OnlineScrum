using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineScrum.BusinessLayer
{
    public class SprintManager
    {
        public static Sprint GetSprintFromID(int id)
        {
            try
            {
                var returnSprint = new Sprint();
                using(var context = new DatabaseContext())
                {
                    var sprintResult = (from sprint in context.Sprints
                                      where sprint.SprintID == id
                                      select sprint).FirstOrDefault();
                    if (sprintResult != null)
                    {
                        returnSprint.SprintID = sprintResult.SprintID;
                        returnSprint.FinishDate = sprintResult.FinishDate;
                        returnSprint.StartDate = sprintResult.StartDate;
                        returnSprint.SprintNumber = sprintResult.SprintNumber;
                        returnSprint.SprintName = sprintResult.SprintName;
                    }
                    return returnSprint;
                }
            } catch (Exception e)
            {
                SharedManager.Log(e, "GetSprintFromID");
                return null;
            }
        }
    }
}