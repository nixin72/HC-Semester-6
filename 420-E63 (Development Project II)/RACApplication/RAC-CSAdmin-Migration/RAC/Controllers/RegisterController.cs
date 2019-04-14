using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace RAC.Controllers {
	public class RegisterController {
		public static void sendConfirmationEmail() {
			MailMessage mail = new MailMessage("dumaresq.philip@cegep-heritage.qc.ca", "dumaresq.philip@cegep-heritage.qc.ca");
			SmtpClient client = new SmtpClient();
			client.Port = 25;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Host = "hcmail.cegep-heritage.qc.ca";
			mail.Subject = "test";
			mail.Body = "This is a test.";
			client.Send(mail);

			/*
			 * https://stackoverflow.com/questions/9201239/send-e-mail-via-smtp-using-c-sharp
			 * 
				using System.Net.Mail;
				using System.Text;

				...

				// Command line argument must the the SMTP host.
				SmtpClient client = new SmtpClient();
				client.Port = 587;
				client.Host = "smtp.gmail.com";
				client.EnableSsl = true;
				client.Timeout = 10000;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Credentials = new System.Net.NetworkCredential("user@gmail.com","password");

				MailMessage mm = new MailMessage("donotreply@domain.com", "sendtomyemail@domain.co.uk", "test", "test");
				mm.BodyEncoding = UTF8Encoding.UTF8;
				mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

				client.Send(mm);
			 
			 */
		}

	}
}