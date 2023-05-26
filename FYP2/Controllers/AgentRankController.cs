using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP2.Controllers
{
    public class AgentRankController : Controller
    {
        // GET: AgentRank
        public ActionResult ShowAgentRank()
        {
            if (Session["Login"] == "ok")
            {
                AgentRanking a = new AgentRanking();

                return View(a.GetAgent());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }
        public ActionResult AgentDetails(int AgentID)
        {FYP2.Controllers.Variables.showotheragent=false;
            ShowRateReview b = new ShowRateReview();
            LoginModel lm = new LoginModel();
            lm.AgentId = AgentID;
            //  var a = lm.GetAgentDetail();

            return RedirectToAction("AgentDetails", "ShowAgent", new { AgentID = AgentID });
        }
    }
}