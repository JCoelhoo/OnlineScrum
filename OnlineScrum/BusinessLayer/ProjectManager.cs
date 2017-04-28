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
        public static string AddProject(Project project)
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
                        //var sha = new SHA1CryptoServiceProvider();
                        //var password = Encoding.ASCII.GetBytes(lecturer.Password);    
                        //lecturer.Password = Encoding.Default.GetString(sha.ComputeHash(password));
                        var insertProject = new Project { Name = project.Name, ScrumMaster = "jk" };
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
    }
}