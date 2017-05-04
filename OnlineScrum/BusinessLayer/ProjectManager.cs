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
                            return insertProject.ScrumMaster + "does not exist";
                        insertProject.DevTeam = String.Join(",", project.DevTeamList);
                        foreach(var dev in insertProject.DevTeam.Split(new char[] { ',' }))
                        {
                            if (String.IsNullOrEmpty(dev)) continue; 
                            if (!UserManager.CheckExistingEmail(dev))
                                return dev + "does not exist";
                        }
                        context.Projects.Add(insertProject);
                        context.SaveChanges();
                        return "";
                    }
                }
                catch (Exception e)
                {
                    using (StreamWriter sw = File.AppendText(".\\log.txt"))
                    {
                        sw.Write("RegisterUser\t");
                        sw.Write(e.GetBaseException());
                        sw.Write('\t');
                        sw.WriteLine(e.Message);
                    }
                    return "Database error";
                }
            }
        }

        public static Project getProjectByEmail(string email)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var projects = (from proj in context.Projects
                                       select proj).ToList();
                    Project project = null;
                    foreach(var proj in projects)
                    {
                        if (proj.ScrumMaster == email || proj.DevTeam.Contains(email))
                            project = proj;
                    }
                    
                    //Project retProj = new Project
                    //               {
                    //                   DevTeam = project.DevTeam,
                    //                   ScrumMaster = project.ScrumMaster,
                    //                   Name = project.Name,
                    //                   ProjectID = project.ProjectID
                    //               };

                    return project;
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = File.AppendText(".\\log.txt"))
                {
                    sw.Write("getProjectByEmail\t");
                    sw.Write(e.GetBaseException());
                    sw.Write('\t');
                    sw.WriteLine(e.Message);
                }
                return null;
            }
        }
    }
}