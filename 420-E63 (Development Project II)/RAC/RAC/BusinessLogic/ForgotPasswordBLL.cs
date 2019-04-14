using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Models;
using System.Text;

namespace RAC.BusinessLogic {
    public class ForgotPasswordBLL {
        /*
         * The email key is setup using the ValidationKeyBLL class
         * method named SetupEmailKey.
         * The remaining logic for this class is for checking changing the password. 
         */
        public static EmailNotificationErr SendResetPasswordEmail(string email, string url) {
            var n = new Notification();

            if (string.IsNullOrWhiteSpace(email))
            {
                return EmailNotificationErr.InvalidInput;
            }
            User cand = CandidateBLL.GetUser(email);
            if (cand == null) { 
                return EmailNotificationErr.InvalidInput;
            }
            int userId= cand.Id;
            
            int ret = n.sendEmail(email, (int)Messages.FORGOT_PASSWORD,
                    userId, url); // email confirmation
            if (ret == -1)
            {
                //error sending email
                return EmailNotificationErr.EmailSendFailed;
            }
            return EmailNotificationErr.Success;
        }
        public static EmailNotificationErr UpdatePassword(string email, string code, string newPass) {

            User cand = CandidateBLL.GetUser(email);
            if (cand == null || String.IsNullOrWhiteSpace(newPass) || String.IsNullOrWhiteSpace(code) || String.IsNullOrWhiteSpace(email)) {
                return EmailNotificationErr.InvalidInput;
            }

            int userId = cand.Id;
            EmailNotificationErr validCode = ValidationKeyBLL.IsLinkValid(userId, code);

            EmailNotificationErr returned = EmailNotificationErr.Success;
            try
            {
                if (validCode == EmailNotificationErr.Success)
                {
                    // email and code are valid, now we change the password
                    byte[] saltByte = CandidateBLL.GenerateSalt();
                    string hashedPassword = CandidateBLL.HashPassword(newPass, saltByte);
                    cand.Password = hashedPassword;
                    cand.Salt = Encoding.Default.GetString(saltByte);
                    DbContext.Context.SaveChanges();
                }
                else
                {
                    returned = validCode;
                }
            }
            catch {
                returned = EmailNotificationErr.DatabasConsistencyError;
            }
            // look at candidate BLL line 47 for hashing
            return returned;
        }
    }
}