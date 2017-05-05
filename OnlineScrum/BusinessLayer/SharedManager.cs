using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineScrum.BusinessLayer
{
    public class SharedManager
    {
        public static string DatabaseError { get { return "Database Error"; } }

        public static void Log(Exception e, string function)
        {
            using (StreamWriter sw = File.AppendText(Path.Combine(HttpContext.Current.Server.MapPath("~"), "log.txt")))
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
    }
}