﻿using OnlineScrum.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
                        var insertUser = new User { Username = user.Username, Password = Hash(user.Password), Email = user.Email, Role = user.Role};
                        context.Users.Add(insertUser);
                        context.SaveChanges();
                        return "";
                    }
                }
                catch (Exception e)
                {
                    SharedManager.Log(e, "RegisterUser");
                    return SharedManager.DatabaseError;
                }
            }
        }

        public static bool CheckExistingEmail(string email)
        {
            try
            {

                using (var context = new DatabaseContext())
                {
                    var userEmails = from user in context.Users
                                           select user.Email;

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

                    SharedManager.Log(e, "CheckExistingEmail");
                    return false;
            }
        }

        public static LoginStatus Login(Login log)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var userSet = from user in context.Users
                                           select new
                                           {
                                               user.Email,
                                               user.Password,
                                               user.Role
                                           };

                   
                    foreach (var record in userSet)
                    {
                        var result_password = Hash(log.Password);

                        if (record.Email.Equals(log.Email) && result_password.Equals(record.Password))
                        {
                            if (record.Role == "Developer" || record.Role =="ScrumMaster")
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
                SharedManager.Log(e, "Login");
                return LoginStatus.DBFail;
            }
        }

        public static User GetUserByEmail(string Email)
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
                SharedManager.Log(e, "GetUserByUsername");
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