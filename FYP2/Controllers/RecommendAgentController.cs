using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FYP2.Controllers
{
    public class RecommendAgentController : Controller
    {
        // GET: RecommendAgent
        public ActionResult RecommendAgent()
        {
            if (Session["Login"] == "ok")
            {
                return View();
            }
            else 
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult GetAgent(FormCollection fc)
        {
            RecommendedAgent ra = new RecommendedAgent();
            ra.area = fc["selectarea"];
            ra.type = fc["selecttype"];
            ra.OverallAgentRating();
            ra.VectorFormation();
            //ra.getRecommendAgent();
            return View("RecommendAgent",ra.getRecommendAgent(ra.getAgentId));
        }
        public ActionResult AgentDetails(int AgentID)
        {
            FYP2.Controllers.Variables.showotheragent = false;
            ShowRateReview b = new ShowRateReview();
            LoginModel lm = new LoginModel();
            lm.AgentId = AgentID;
          //  var a = lm.GetAgentDetail();

            return RedirectToAction("AgentDetails", "ShowAgent", new { AgentID = AgentID });
        }
    }
}