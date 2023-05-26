using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace FYP2.Reminder
{
    public class SMSScheduler
    {
        public static string date = "2018-04-07";
        public static string time = "19:23";
        static int i = 0;
       
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

                IJobDetail job = JobBuilder.Create<Chal>()
                    .WithIdentity("smsJob" + Convert.ToString(i), "smstrigger" + Convert.ToString(GlobalVariable.SMSCounter))
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
                GlobalVariable.SMSCounter++;
            }
            catch (ArgumentException e)
            {
               // Log.Error(e);
            }
        }
        }
    }
    public class Chal : IJob
    {
        private const string MessagesUrlPath = "services/api/messaging";
        static string txtIPAddress = "192.168.8.101";
        static string txtPort = "1688";
        public static string custNum;
        public static string message;
        protected static string ConstructBaseUri()
        {
            UriBuilder uriBuilder = new UriBuilder("http", txtIPAddress, Convert.ToInt32(txtPort));
            return uriBuilder.ToString();
        }


        public void  Execute(Quartz.IJobExecutionContext context)
        {

            SendMessage();
            


        }
        public static async Task SendMessage()
        {
            using (var client = new HttpClient())
            {
                string url = ConstructBaseUri();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("to", custNum));
                postData.Add(new KeyValuePair<string, string>("message", message));
                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(MessagesUrlPath, content);

            }

        }
    }
   
