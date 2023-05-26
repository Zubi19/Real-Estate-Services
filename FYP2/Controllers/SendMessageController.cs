using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP2.Controllers
{
    public class SendMessageController : Controller
    {
        // GET: SendMessage
        public ActionResult Index()
        {
            
            
                return PartialView("SendMessage");
                   
        }

        public ActionResult sendmessage(FormCollection FC, string AgentNumber)
        {
            Send_Message sms = new Send_Message();


            Send_Message.Message = FC["message"];
            Send_Message.SendMessage(AgentNumber);

            return PartialView("SendMessage");
        }
    }
}