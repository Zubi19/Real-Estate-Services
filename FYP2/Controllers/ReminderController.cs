using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using FYP2.Reminder;

namespace FYP2.Controllers
{
    public class ReminderController : Controller
    {
        // GET: Reminder
        public ActionResult ReminderEmail()
        {


            return PartialView("ReminderEmail");
        }
        public ActionResult ReminderSMS()
        {


            return PartialView("ReminderSMS");
        }
        public ActionResult Sendemail(FormCollection FC, HttpPostedFileBase postedFile, string agentemail)
        {
            EmailScheduler e = new EmailScheduler();
            Logging  em = new Logging();
            EmailScheduler.date = FC["date"];
            EmailScheduler.time = FC["time"];
            Logging.Subject = FC["subject"];
            Logging.Message = FC["message"];
            em.file = postedFile;
            Logging.mailto = agentemail;
            EmailScheduler.Start();
            //em.email_send(agentemail, em.Subject, em.Message);
            return PartialView("ReminderEmail");
        }
        public ActionResult Sendsms(FormCollection FC, string AgentNumber)
        {
            Chal sms = new Chal();
            SMSScheduler s = new SMSScheduler();

            Chal.message = FC["message"];
            Chal.custNum = AgentNumber;
            SMSScheduler.date = FC["date"];
            SMSScheduler.time = FC["time"];
            //Send_Message.SendMessage(AgentNumber);
            SMSScheduler.Start();
            return PartialView("ReminderSMS");
        }
    }
}