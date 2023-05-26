using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace FYP2.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string email_from = "gharbanaopk@gmail.com";
        public string pass_from = "gharbanaopk123";
        public string path;
        //123mailling@gmail.com
        //abdullah1995
        public System.Net.Mail.SmtpClient get_smtp()
        {
            System.Net.Mail.SmtpClient smtp_gmail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("gharbanaopk@gmail.com", "gharbanaopk123"),
                EnableSsl = true
            };

            SmtpClient smtp_hotmail = new SmtpClient("smtp.live.com", 587)
            {
                Credentials = new NetworkCredential("abdullahbaig123@outlook.com", "asd123asd"),
                EnableSsl = true
            };

            SmtpClient smtp_yahoo = new SmtpClient("smtp.mail.yahoo.com", 465)
            {
                Credentials = new NetworkCredential("username@yahoo.com", "password"),
                EnableSsl = true
            };

            return smtp_gmail;
            //return smtp_hotmail;
            //return smtp_yahoo;
        }

        public void email_send(string mail_to, string subject, string bodytext)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtp = get_smtp();

                //---------mailmessage configuration
                mail.From = new MailAddress(email_from);
                mail.To.Add(mail_to);
                mail.Subject = subject;
                //mail.IsBodyHtml = true;
                //bodytext.Replace()

                mail.Body = bodytext + "\r\n" + "Reply To: " + LoginModel.email;

                //---------attachment
                if (path != null)
                {
                    Attachment attach = new Attachment(path);
                    mail.Attachments.Add(attach);
                }

                //---------send
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
            }
        }

        //public string file_picker()
        //{
        //    //-----------file picker
        //    OpenFileDialog file = new OpenFileDialog();
        //    if (file.ShowDialog() == DialogResult.OK)
        //    {
        //        return path = file.FileName;
        //    }
        //    else return path = null;
        //}
    }
}