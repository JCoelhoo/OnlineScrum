using System;
using System.Collections.Generic;
using System.Linq;
using OnlineScrum.Models;

namespace OnlineScrum.BusinessLayer
{
    public class ProjectManager
    {
        public static string AddProject(Project project, string scrumMaster, out Project pr)
        {
            pr = new Project();
            if (project == null)
            {
                return "Error when adding. Please try again";
            }
            try
            {
                using (var context = new DatabaseContext())
                {
                    var proj = (from p in context.Projects where p.Name == project.Name select p).FirstOrDefault();
                    if (proj != null)
                        return project.Name + " is already being used";
                    scrumMaster = project.ScrumMaster ?? scrumMaster;
                    var insertProject = new Project
                    {
                        Name = project.Name,
                        ScrumMaster = scrumMaster,
                        DevTeam = project.DevTeam,
                        Description = project.Description
                    };

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
                    pr = insertProject;
                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddProject");
                return SharedManager.DatabaseError;
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

        public static string ChangeSprintInItem(List<SprintItem> item, int projectID)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    foreach (var i in item)
                    {
                        var p = (from proj in context.Projects
                               where proj.ProjectID == projectID
                               select proj).FirstOrDefault();

                        var s = (from sprint in context.Sprints
                            where sprint.SprintName == i.Sprint && p.Sprints.Contains(sprint.SprintID + ",")
                                 select sprint).FirstOrDefault();
                        if (s == null) continue;
                        if (!SharedManager.SplitString(s.Items).Contains(i.Item))
                        {
                            var it = SprintManager.GetItemFromID(Convert.ToInt32(i.Item));
                            if (it.SprintlessProjectID == 0)
                            {
                                SprintManager.AddItem(s, it);
                            }
                            else
                            {
                                SprintManager.ChangeItem(s, it);
                            }
                        }
                    }
                    context.SaveChanges();
                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "ChangeSprintInItem");
                return SharedManager.DatabaseError;
            }
        }

        public static string AddSprint(Sprint sprint, Project proj)
        {
            if (sprint == null)
            {
                return "Error when adding. Please try again";
            }
            using (var context = new DatabaseContext())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var b = (from s in context.Sprints
                                 where sprint.SprintName == s.SprintName
                                 select s).FirstOrDefault();
                        if (b != null)
                        {
                            return "A Sprint already exists with that name";
                        }
                        //var sha = new SHA1CryptoServiceProvider();
                        //var password = Encoding.ASCII.GetBytes(lecturer.Password);    
                        //lecturer.Password = Encoding.Default.GetString(sha.ComputeHash(password));C:\Users\João\Desktop\OnlineScrum\OnlineScrum\BusinessLayer\UserManager.cs
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
                    proj.Sprints += sprintID+",";
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

        public static Project GetProjectByID(int projectID, string email)
        {
            return GetProjectsByEmail(email).FirstOrDefault(m => m.ProjectID == projectID);
        }

        public static List<Sprint> GetSprintFromProject(string sprintString)
        {
            try
            {
                if (sprintString == null) return new List<Sprint>();
                var sprints = SharedManager.SplitString(sprintString);
                using (var context = new DatabaseContext())
                {
                    var sprintList = context.Sprints.Where(m => sprints.Any(m2 => m2 == m.SprintID.ToString())).ToList();

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

                    if (UserManager.GetUserByEmail(member).Role == "ScrumMaster")
                        return member + " is a Scrum Master";

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

        public static List<Item> GetSprintlessItems(Project project)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var items = context.Items.Where(m => m.SprintlessProjectID == project.ProjectID).ToList();
                    return items;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetSprintlessItems");
                return new List<Item>();
            }
        }

        public static List<Project> GetProjectsByEmail(string userEmail)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var projects = (from proj in context.Projects
                        select proj).ToList();

                    return projects.Where(m => SharedManager.SplitString(m.DevTeam).Contains(userEmail) || m.ScrumMaster == userEmail).ToList();
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetProjectsByEmail");
                return new List<Project>();
            }
        }
    }
}