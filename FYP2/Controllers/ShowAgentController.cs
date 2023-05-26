using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP2.Models;
using FYP2.NaiveBayes;

namespace FYP2.Controllers
{
    public class ShowAgentController : Controller
    {
        // GET: ShowAgent
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public ActionResult ShowAgent()
        {
            //if (Session["Login"] == "ok")
            //{
                
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");
            //}
           
        }
        
        
        [HttpPost]
        public ActionResult ShowAgent(FormCollection fc)
        {
            ShowAgent a = new ShowAgent(Convert.ToInt32(fc["selectarea"]),fc["selectblock"], fc["selecttype"]);
            Variables.agentarea = Convert.ToInt32(fc["selectarea"]);
            Variables.type = fc["Select Type"];
            ModelState.Clear();
           
            return View(a.GetAgent(a));

        }
        public ActionResult OtherAgent(FormCollection fc, int id)
        {
            ShowAgent a = new ShowAgent(Variables.agentarea, fc["selectblock"], fc["selecttype"]);
            Variables.id = id;
            ModelState.Clear();

            return View("ShowAgent",a.GetAgent(a));

        }
        public ActionResult AgentDetails(int AgentID)
        {
            if (Session["Login"] == "ok")
            {
                
                ShowRateReview b = new ShowRateReview();
                LoginModel lm = new LoginModel();
                lm.AgentId = AgentID;
                var a = lm.GetAgentDetail();

                return View("AgentDetails", a);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult AgentDetail(int AgentID)
        {
            if (Session["Login"] == "ok")
            {
                FYP2.Controllers.Variables.showotheragent = true;
                ShowRateReview b = new ShowRateReview();
                LoginModel lm = new LoginModel();
                lm.AgentId = AgentID;
                var a = lm.GetAgentDetail();

                return View("AgentDetails", a);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ShowAgents()
        {
            return View();
        }
        public ActionResult RateReview(FormCollection fc, string id)
        {
            string commentvalue;
            Words a = new Words();
            int star = Convert.ToInt32(fc["star"]);
            RateReview r = new RateReview();
           
                r.rating = star;
            r.review = fc["review"];
            r.insert(id);
            double probvalue = a.start(r.review);
           if(probvalue>=0)
           {
               commentvalue = "positive";
           }
            else
           {
               commentvalue = "negative";
           }
            
            AgentRanking ar = new AgentRanking();
            int rankexist = ar.AgentrRankExist(id);
            if(rankexist==0)
            {
                if (commentvalue == "positive")
                    ar.insertrank(id, 1);
                else
                    ar.insertrank(id, -1);
            }
            else
            {
                int rank = ar.GetRanking(id);
                if (commentvalue == "positive")
                    rank++;
                else
                    rank--;
                ar.updaterank(id, rank);
            }
            AgentDetails(Convert.ToInt32(id));
            return View("AgentDetails");
        }

        public PartialViewResult SendEmail()   /////partrial view
        {

            return PartialView("SendEmail");
        }
        public ActionResult email(FormCollection FC, HttpPostedFileBase postedFile, string agentemail)
        {
            EmailModel em = new EmailModel();

            em.Subject = FC["subject"];
            em.Message = FC["message"];
            em.file = postedFile;
            em.email_send(agentemail, em.Subject, em.Message);

            return PartialView("SendEmail");
        }
    }
}