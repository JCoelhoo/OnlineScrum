using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace OnlineScrum.BusinessLayer
{
    public class ProjectManager
    {
        public static string AddProject(Project project, string scrumMaster)
        {
            //TODO: fix id
            //TODO SM not in dev
            if (project == null)
            {
                return "Error when adding. Please try again";
            }
            else
            {
                try
                {
                    using (var context = new DatabaseContext())
                    {
                        var insertProject = new Project { Name = project.Name, ScrumMaster = scrumMaster, DevTeam = project.DevTeam };
                        if (!UserManager.CheckExistingEmail(insertProject.ScrumMaster))
                            return insertProject.ScrumMaster + " does not exist";
                        insertProject.DevTeam = String.Join(",", project.DevTeamList.Where(s => !String.IsNullOrWhiteSpace(s)));
                        foreach (var dev in SharedManager.SplitString(insertProject.DevTeam))
                        {
                            //if (String.IsNullOrEmpty(dev)) continue; 
                            if (scrumMaster == dev)
                                return dev + " is the Scrum Master";
                            if (!UserManager.CheckExistingEmail(dev))
                                return dev + " does not exist";
                        }
                        context.Projects.Add(insertProject);
                        context.SaveChanges();
                        return "";
                    }
                }
                catch (Exception e)
                {
                    SharedManager.Log(e, "AddProject");
                    return SharedManager.DatabaseError;
                }
            }
        }

        public static Project GetProjectByEmail(string email)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var projects = (from proj in context.Projects
                                    select proj).ToList();
                    Project project = null;
                    foreach (var proj in projects)
                    {
                        if (proj.ScrumMaster == email || proj.DevTeam.Contains(email))
                            project = proj;
                    }

                    return project;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetProjectByEmail");
                return null;
            }
        }

        public static string AddSprint(Sprint sprint, Project proj)
        {
            if (sprint == null)
            {
                return "Error when adding. Please try again";
            }
            else
            {

                using (var context = new DatabaseContext())
                {
                    using (var dbTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //var sha = new SHA1CryptoServiceProvider();
                            //var password = Encoding.ASCII.GetBytes(lecturer.Password);    
                            //lecturer.Password = Encoding.Default.GetString(sha.ComputeHash(password));C:\Users\João\Desktop\OnlineScrum\OnlineScrum\BusinessLayer\UserManager.cs
                            //TODO check dates of start and finish
                            var number = (proj.Sprints == null)
                                ? 1
                                : (SharedManager.SplitString(proj.Sprints)).Count() + 1;
                            sprint.SprintNumber = number;
                            context.Sprints.Add(sprint);
                            context.SaveChanges();
                            //FIXME sprintID is attributed when Add()
                            AddSprintToProject(proj.ProjectID, sprint.SprintID);
                            dbTransaction.Commit();

                            return "";
                        }
                        catch (Exception e)
                        {
                            dbTransaction.Rollback();
                            SharedManager.Log(e, "AddSprint");
                            return SharedManager.DatabaseError;
                        }
                    }
                }
            }
        }

        //private static int GetNewSprintNumber()
        //{
        //    try {
        //        using (var context = new DatabaseContext())
        //        {
        //            var count = context.Sprints.Count();
        //            if (count == 0)
        //                return 1;

        //            return count+1; 
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        SharedManager.Log(e, "GetSprintNumber");
        //        return -1;
        //    }
        //}

        public static string AddSprintToProject(int projectID, int sprintID)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var proj = (from project in context.Projects
                                where project.ProjectID == projectID
                                select project).FirstOrDefault();
                    proj.Sprints += "," + sprintID;
                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddSprintToProject");
                return SharedManager.DatabaseError;
            }
        }

        public static List<Sprint> GetSprintFromProject(string sprintString)
        {
            try
            {
                if (sprintString == null) return new List<Sprint>();
                var sprints = SharedManager.SplitString(sprintString);
                var returnSprint = new Project();
                using (var context = new DatabaseContext())
                {
                    var sprintList = (from sprint in context.Sprints
                                      where sprints.Contains<string>(sprint.SprintID.ToString())
                                      select sprint).ToList<Sprint>();

                    return sprintList;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetSprintFromProject");
                return new List<Sprint>();
            }
        }

        //if memeber exists or differnet
        public static string AddMember(string member, int projectID)
        {
            try
            {
                if (member == null) return "Email cannot be null";
                using (var context = new DatabaseContext())
                {
                    var proj = (from project in context.Projects
                                where project.ProjectID == projectID
                                select project).First();
                    if (proj == null)
                    {
                        return "Project not found";
                    }

                    if (SharedManager.SplitString(proj.DevTeam).Contains(member) || proj.ScrumMaster == member)
                    {
                        return "Member already exists";
                    }

                    if (!UserManager.CheckExistingEmail(member))
                        return member + " does not exist";

                    proj.DevTeam += "," + member;
                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetSprintFromProject");
                return SharedManager.DatabaseError;
            }
        }
    }
}