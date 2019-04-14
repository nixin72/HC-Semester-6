using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Models;
using System.ComponentModel;

namespace RAC.BusinessLogic {
    public class ConfirmEmailBLL {
        /*
         * Contains the logic for handling email verification and confirmation when a candidate registers. 
         */
        
        public static EmailNotificationErr ValidateEmail(int userId, string key) {
            EmailNotificationErr returned = ValidationKeyBLL.IsLinkValid(userId, key);
            if (returned == EmailNotificationErr.Success) {
                Candidate person = (Candidate)DbContext.Context.User.Find(userId);
                //update person's emailValidated column in DB
                person.IsAccountValidated = true;
                //DbContext.Context.User.Add(person);
                DbContext.Context.SaveChanges();
                
                //Sending notification to RACAdvisor that they the user has started working on their RAC Request
                var n = new Notification();
                Notification.CreateNotification(person.Id, Messages.NEW_RAC_STARTED);
            }
            return returned;
        }        

        public static EmailNotificationErr resendNotification(User usedCand, string url) {
            var n = new Notification();string to = usedCand.Email;
            EmailNotificationErr confirmed = EmailNotificationErr.Success;
            try {
                int ret = n.sendEmail(to, (int)Messages.CANDIDATE_CONFIRM_EMAIL, usedCand.Id, url); // email confirmation
                if (ret == -1) {
                    confirmed = EmailNotificationErr.EmailSendFailed;
                }
            }
            catch {
                confirmed = EmailNotificationErr.DatabasConsistencyError;
            }
            return confirmed;
        }
    }
}