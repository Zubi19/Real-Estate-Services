using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FYP2.Reminder
{
    public class EmailScheduler
    {
        public static string date ="2018-04-07";
        public static string time = "19:23";
        static int i = 0;
       // public static IScheduler _scheduler;
        public static void Start()
        {
            string dtbuilder="0 ";
            char dt = time[time.Length - 2];
            if (dt == '0')
                dtbuilder = dtbuilder + time[time.Length-1];
            else
                dtbuilder = dtbuilder + time[time.Length - 2] + time[time.Length - 1];
            dt = time[time.Length - 5];
            if (dt == '0')
                dtbuilder = dtbuilder + " " + time[time.Length - 4] ;
            else
                dtbuilder = dtbuilder + " " + time[time.Length - 5] + time[time.Length - 4];
            dt = date[date.Length - 2];
            if (dt == '0')
                dtbuilder = dtbuilder + " " + date[date.Length - 1];
            else
                dtbuilder=dtbuilder+" "+date[date.Length-2]+date[date.Length-1];
            dt = date[date.Length - 5];
            if(dt=='0')
                dtbuilder = dtbuilder + " " + date[date.Length - 4];
            else
                dtbuilder = dtbuilder + " " + date[date.Length - 5] + date[date.Length - 4];
            dtbuilder = dtbuilder + " ? *";
            // 0 42 8 4 27 ? *
            try
            {
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<Logging>()
                    .WithIdentity("emailJob" + Convert.ToString(i), "emailtrigger" + Convert.ToString(GlobalVariable.EmailCounter))
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithCronSchedule(dtbuilder)
                       //.WithDescription("Once")
                       //       .WithSimpleSchedule(x => x
                       //        .WithIntervalInSeconds(60)
                       //        .RepeatForever())
                             //.StartAt(DateBuilder.DateOf(2, 35, 00, 27, 4, 2017))
                             //.EndAt(DateBuilder.DateOf(2, 36, 00, 27, 4, 2017))
                             .Build();
                    //.StartAt(DateBuilder.DateOf(12, 43, 00, 26, 2, 2015))
                    //.WithDailyTimeIntervalSchedule
                    //(s =>
                    //    s.WithIntervalInSeconds(5)
                    //        .OnEveryDay()
                    //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 15))
                    //)
                    //.Build();

                sched.ScheduleJob(job, trigger);
                GlobalVariable.EmailCounter++;
            }
                
            catch (ArgumentException e)
            {
               // Log.Error(e);
            }
        }
    }
    public class Logging : IJob
    {

        public static string Subject { get; set; }
        public static string Message { get; set; }
        public static string mailto { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string email_from = "gharbanaopk@gmail.com";
        public string pass_from = "gharbanaopk123";
        public string path;
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
                Credentials = new NetworkCredential("suparco_nasa@yahoo.com", ""),
                EnableSsl = true
            };

            //return smtp_yahoo;
            //return smtp_hotmail;
            return smtp_gmail;
        }

        public void Execute(Quartz.IJobExecutionContext context)
        {
           
           
                MailMessage mail = new MailMessage();

                SmtpClient smtp = get_smtp();

                //---------mailmessage configuration
                mail.From = new MailAddress(email_from);
                mail.To.Add(mailto);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = Message;

                //---------attachment
                if (path != null)
                {
                    Attachment attach = new Attachment(path);
                    mail.Attachments.Add(attach);
                }

                //---------send
                smtp.Send(mail);

            
           
        }
       
        }
    
}