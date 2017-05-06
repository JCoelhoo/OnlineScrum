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
                        returnSprint.Items = sprintResult.Items;
                        //returnSprint.ItemsList = sprintResult.Items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    return returnSprint;
                }
            } catch (Exception e)
            {
                SharedManager.Log(e, "GetSprintFromID");
                return null;
            }
        }

        public static string AddItem(Sprint sprint, Item item)
        {
            if (item == null)
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
                            //TODO check dates of start and finish
                            var number = (sprint.Items == null) 
                                ? 1
                                : (sprint.Items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).Count()+1;
                            if (number == -1) return SharedManager.DatabaseError;
                            item.ItemNumber = number;
                            context.Items.Add(item);
                            context.SaveChanges();
                            //FIXME sprintID is attributed when Add()
                            AddItemToSprint(sprint.SprintID, item.ItemID);
                            dbTransaction.Commit();

                            return "";
                        }
                        catch (Exception e)
                        {
                            dbTransaction.Rollback();
                            SharedManager.Log(e, "AddItem");
                            return SharedManager.DatabaseError;
                        }
                    }
                }
            }
        }

        private static string AddItemToSprint(int sprintID, int itemID)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var sprintReturn = (from sprint in context.Sprints
                                where sprint.SprintID == sprintID
                                select sprint).FirstOrDefault();
                    sprintReturn.Items += "," + itemID;
                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddItemToSprint");
                return SharedManager.DatabaseError;
            }
        }

        //private static int GetNewItemNumber()
        //{
        //    try
        //    {
        //        using (var context = new DatabaseContext())
        //        {
        //            var count = context.Items.Count();
        //            if (count == 0)
        //                return 1;

        //            return count + 1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        SharedManager.Log(e, "GetNewItemNumber");
        //        return -1;
        //    }
        //}
        public static List<Item> GetItemsFromSprint(string items)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var itemList = items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var itemRet = (from item in context.Items
                                   where itemList.Contains(item.ItemID.ToString())
                                   select item).ToList();

                    return itemRet;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddItemToSprint");
                return new List<Item>() { };
            }
        }
    }

    
}