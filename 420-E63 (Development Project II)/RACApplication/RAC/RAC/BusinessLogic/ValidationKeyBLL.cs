using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Models;
using System.IO;

namespace RAC.BusinessLogic {
    public static class ValidationKeyBLL {
        /*
         * This file contaains logic for creating unique keys to make sure 
         * email links sent to the user are real.
         */
        private const int MINUTES_BETWEEN = 1;

        public static string GenerateKey() {
            int length = 12;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static EmailNotificationErr AddEntry(int userId, string key, bool enableTimerCheck = true) {
            // this adds the entry to the database for that user for an email key
            // first check if an entry exists for that user, if so deletes it

            EmailNotificationErr returned = EmailNotificationErr.Success;
            ValidationKey emailExisting = DbContext.Context.ValidationKeys.Find(userId);
            bool timeCheck = true;
            
            DateTime compared = DateTime.Now.AddMinutes(-MINUTES_BETWEEN);

            if (emailExisting != null && enableTimerCheck)
            {
                if (emailExisting.DateAdded <= compared)
                {
                    // if the last was done less than a minute ago do not send
                    DbContext.Context.ValidationKeys.Remove(emailExisting);
                    DbContext.Context.SaveChanges();
                }
                else
                {
                    // people can only request an email validation every MINUTES_BETWEEN minutes
                    returned = EmailNotificationErr.RequestTooSoon;
                    timeCheck = false;
                }
            }
            else if (!enableTimerCheck) {
                // make sure an entry with that ID doesnt exist
                try
                {
                    DbContext.Context.ValidationKeys.Remove(
                        DbContext.Context.ValidationKeys.Find(userId));
                }
                catch {
                    // means none exist with this ID so all is good
                }
            }
            if (timeCheck) {
                // add entry
                ValidationKey emailKey = new ValidationKey();
                emailKey.Id = userId;
                emailKey.UniqueKey = key;
                emailKey.DateAdded = DateTime.Now;
                try {
                    // this used to be a work around and can now be changed to a normal DB call
                    //string insertCmd = "SET IDENTITY_INSERT ValidationKeys ON; Insert into ValidationKeys (Id, UniqueKey,DateAdded ) "
                    //  + "Values(" + emailKey.Id + ",\'" + emailKey.UniqueKey + "\',\'" + emailKey.DateAdded.ToString() + "\')";
                    // DbContext.Context.Database.ExecuteSqlCommand(insertCmd); // hardcode sql

                    DbContext.Context.ValidationKeys.Add(emailKey);
                    DbContext.Context.SaveChanges();
                }
                catch (Exception ex) {
                    try
                    {
                        using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                        {
                            file.WriteLine(ex.ToString());
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Error could not be logged: " + e.ToString());
                    }

                    returned = EmailNotificationErr.DatabaseErr;
                }
            }

            return returned;
        }
        /*
         * done after email sent to make sure the link was ok
         */
        public static EmailNotificationErr IsLinkValid(int userId, string key) {
            // only use key and if it exists, confirm
            EmailNotificationErr returned = EmailNotificationErr.DatabasConsistencyError;
            try {
                // validate user is in db
                Candidate person = (Candidate)DbContext.Context.User.Find(userId);
                if (person != null) {
                    // validata string and ids match
                    ValidationKey emailVal = DbContext.Context.ValidationKeys.Find(userId);

                    if (emailVal != null && emailVal.UniqueKey.Equals(key)) {
                        // SUCCESS PATH
                        // remove row in db
                        DbContext.Context.ValidationKeys.Remove(emailVal);                        
                        returned = EmailNotificationErr.Success;
                    }
                    else {
                        // the key doesnt match the key in the databse
                        returned = EmailNotificationErr.DatabasConsistencyError;
                    }
                }
                else {
                    // person is null meaning no person has this id
                    returned = EmailNotificationErr.DatabasConsistencyError;
                }
            }
            catch {
                returned = EmailNotificationErr.DatabaseErr;
            }

            return returned;
        }

        public static string SetupEmailKey(int userId, bool enableTimerCheck = true)
        {
            // this method creates the database entry to validate that the confirm email
            // and forgot password links are valid

            // the enableTimerCheck variable enables and disables the functionality that 
            // makes sure emails are not set back to back. this is disabled for testing mostly

            string key = GenerateKey();
            try
            {
                EmailNotificationErr result = AddEntry(userId, key, enableTimerCheck);
                if (result != EmailNotificationErr.Success)
                {
                    // return description of returned enum                    
                    return EnumToString(result);
                }
            }
            catch
            {
                return EnumToString(EmailNotificationErr.DatabaseErr);
            }
            return key;
        }

        public static string EnumToString(EmailNotificationErr enumParam)
        {
            string[] names = { "Success",
                "Error. The email confirmation cannot be sent within x minutes of the last one.",//MINUTES_BETWEEN 
                "Error. Something went wrong on our end.",
                "Error. The parameters entered did not match the database." };
            try
            {
                return names[(int)enumParam];
            }
            catch
            {
                return "Enum paarsing error.";
            }
        }
    }
}