using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP2.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult History()
        {
            if (Session["Login"] == "ok")
            {
                History h = new History();
                h.getAgentid();
                var a = h.GetAgent();
                //h.agentDetail();
                return View(a);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }
        public ActionResult HistoryDetails(int AgentID)
        {
            FYP2.Controllers.Variables.showotheragent = false;
            ShowRateReview b = new ShowRateReview();
            LoginModel lm = new LoginModel();
            lm.AgentId = AgentID;
            var a = lm.GetAgentDetail();

            return View("HistoryReview", a);

        }
    }
}