using OnlineScrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

                    if (sprintResult == null)
                        return returnSprint;

                    returnSprint.SprintID = sprintResult.SprintID;
                    returnSprint.FinishDate = sprintResult.FinishDate;
                    returnSprint.StartDate = sprintResult.StartDate;
                    returnSprint.SprintNumber = sprintResult.SprintNumber;
                    returnSprint.SprintName = sprintResult.SprintName;
                    returnSprint.Items = sprintResult.Items;
                    //returnSprint.ItemsList = sprintResult.Items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
                                : (SharedManager.SplitString(sprint.Items)).Count()+1;
                            if (number == -1) return SharedManager.DatabaseError;
                            //check assignedto member of sprint
                            item.ItemNumber = number;
                            item.ItemStatus = "Developing";
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
                    if (sprintReturn == null)
                        return "Sprint does not exist";

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
                    var itemList = SharedManager.SplitString(items);
                    var itemRet = (from item in context.Items
                                   where itemList.Contains(item.ItemID.ToString())
                                   select item).ToList();

                    return itemRet;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "GetItemsFromSprint");
                return new List<Item>() { };
            }
        }

        public static string AddMeetingToSprint(string meetingID, int sprintID)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var sprintResult = (from sprint in context.Sprints
                                        where sprint.SprintID == sprintID
                                        select sprint).First();

                    sprintResult.Meetings = (sprintResult.Meetings == "") ? meetingID : ',' + sprintResult.Meetings;
                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "AddMeetingToSprint");
                return SharedManager.DatabaseError;
            }

        }

        public static string ChangeStatus(Item item)
        {

            try
            {
                using (var context = new DatabaseContext())
                {
                    var itemRet = (from it in context.Items
                        where it.ItemID == item.ItemID
                        select it).First();

                    if (item.ItemStatus == "Closed")
                        itemRet.DateClosed = DateTime.Now;
                    itemRet.ItemStatus = item.ItemStatus;
                    context.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "Change_Status");
                return SharedManager.DatabaseError;
            }
        }


        //TODO check itemid matches user
        public static Item Add_Notes(int itemID, string note)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var itemRet = (from it in context.Items
                        where it.ItemID == itemID
                        select it).First();

                    itemRet.ItemNotes += ".NOTE_SEPARATOR." + note;
                    context.SaveChanges();

                    return itemRet;
                }
            }
            catch (Exception e)
            {
                SharedManager.Log(e, "Add_Notes");
                return null;
            }
        }
    }

    
}