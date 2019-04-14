using System;
using System.Net.Mail;
using RAC.RACModels;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Hosting;
using System.Configuration;

namespace RAC.BusinessLogic {
	
    
	public class Notification {
        private const string DEFAULT_STRING = "";

		public Candidate getCandidate(string email) {
			Candidate c = new Candidate();
			return c;
		}

		public ContentSpecialist getContentSpecialist(string email) {
			ContentSpecialist c = new ContentSpecialist();
			return c;
		}

		public string getSubject(string from, string to, int msg) {
			return "";
		}
        private static string emailToHtml(string email)
        {
            string start = "<html><head><style>body {background-image: " +
                            "url('../images/topbar-bg.gif'), " +
                            "url('../images/bg.gif');" +
                            "background-position: left top, left top;background-repeat: repeat-x, repeat;font-family: Verdana, sans-serif;font-size: 14px;}p {font-size: 14px;line-height: 1.7;}h1, h2, h3 {	/* color: #daa728; */	color: #bf8100;}.content {	background-color: #fff;	margin:10px;padding: 10px;}hr {border-top: 1px solid #d6d4d0;}</style></head><body><div class='content'><h1>Recognition of Acquired Competencies<h1><hr/><p>";
            string end = "</p></div></body></html>";
            return start + email + end;
        }
        public static int sendEmailWithMessage(string to, string message, string subject) {
            try
            {
                message = emailToHtml(message);
                string from = ConfigurationManager.AppSettings.Get("NoReply");
                MailMessage mail = new MailMessage(from, to);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = ConfigurationManager.AppSettings.Get("SmtpHost");
                mail.Subject = subject;
                try
                {
                    mail.Body = message;
                    string emailStr = "\r\n TO:" + to + "\r\n FROM" + from + "\r\n MESSAGE:"
                        + mail.Body + "\r\n --------------------------------------- \r\n";
                    saveEmailToFile(emailStr);
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }
                catch
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }

            return 0;
        }

        
        public static void saveEmailToFile(string emailStr) {
            string path = HostingEnvironment.MapPath("~/App_Data/Emails.txt");
            if (!File.Exists(path) && path != null) {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(emailStr);
                tw.Close();
            }
            else if (File.Exists(path)) {
                using (var tw = new StreamWriter(path, true)) {
                    tw.WriteLine(emailStr);
                    tw.Close();
                }
            }
        }


	    public static void CreateNotification(int candidateId, Messages message)
	    {
	        DbContext.Context.RACNotification.Add(new RACNotification()
	        {
                CandidateId = candidateId,
                NotificationType = (short) message,
                Time = DateTime.Now
	        });

	        DbContext.Context.SaveChanges();
	    }

	    public static void DeleteNotification(int id)
	    {
	        RACNotification notification = DbContext.Context.RACNotification.FirstOrDefault(s => s.Id == id);
	        if (notification != null) 
	        {
	            DbContext.Context.RACNotification.Remove(notification);
	        }
	        DbContext.Context.SaveChanges();
	    }
	}
    
}