using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineScrum.BusinessLayer
{
    public class UserManager
    {
        public enum LoginStatus { RegularUser, Fail, DBFail };

        public static string RegisterUser(Register user)
        {
            if (user == null)
            {
                return "Error when adding. Please try again";
            }
            else if (CheckExistingEmail(user.Email))
            {
                return "Email already in use";
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
                        var insertUser = new User { Username = user.Username, Password = Hash(user.Password), Email = user.Email, Role = -1 };
                        context.Users.Add(insertUser);
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

        public static bool CheckExistingEmail(string email)
        {
            try
            {

                using (var context = new DatabaseContext())
                {
                    var userEmails = from student in context.Users
                                           select student.Email;

                    foreach (var Email in userEmails)
                    {
                        if (Email.Equals(email))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = File.AppendText(".\\log.txt"))
                {
                    sw.Write("CheckExistingEmail\t");
                    sw.Write(e.GetBaseException());
                    sw.Write('\t');
                    sw.WriteLine(e.Message);
                }
                return false;
            }
        }

        public static LoginStatus Login(Login log)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var userSet = from student in context.Users
                                           select new
                                           {
                                               student.Email,
                                               student.Password,
                                               student.Role
                                           };

                   
                    foreach (var record in userSet)
                    {
                        var result_password = Hash(log.Password);

                        if (record.Email.Equals(log.Email) && result_password.Equals(record.Password))
                        {
                            if (record.Role == 0)
                            {
                                return LoginStatus.RegularUser;
                            }

                            return LoginStatus.RegularUser;
                        }
                    }

                    return LoginStatus.Fail;
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = File.AppendText(".\\log.txt"))
                {
                    sw.Write("Login");
                    sw.Write('\t');
                    sw.Write(e.GetBaseException());
                    sw.Write('\t');
                    sw.WriteLine(e.Message);
                }
                return LoginStatus.DBFail;
            }
        }

        public static User getUserByEmail(string Email)
        {
            try
            {
                var returnedUser = new User();
                using (var context = new DatabaseContext())
                {
                    var userResult = (from user in context.Users
                                          where user.Email == Email
                                          select user).FirstOrDefault();
                    if (userResult != null)
                    {
                        returnedUser.Email = userResult.Email;
                        returnedUser.Username = userResult.Username;
                        returnedUser.Password = userResult.Password;
                        returnedUser.Role = userResult.Role;

                    }
                    return returnedUser;
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = File.AppendText(".\\log.txt"))
                {
                    sw.Write("getUserByUsername\t");
                    sw.Write(e.GetBaseException());
                    sw.Write('\t');
                    sw.WriteLine(e.Message);
                }
                return null;
            }

        }

        public static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}