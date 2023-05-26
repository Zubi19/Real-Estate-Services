using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP2.Models;
namespace FYP2.Controllers
{
    public class AgentDeatailsController : Controller
    {
        // GET: AgentDeatails
        public ActionResult AgentDetails(FormCollection fc)
        {
            string a = fc["star"];
            return View("AgentDetail");
        }

        //public ActionResult GetMap(string locURL)
        //{
        //    ShowAgentVariables model = new ShowAgentVariables();
        //    model.MapUrl = locURL;
        //    return View(model);
        //}
        //public ActionResult GetPic(string locPic)
        //{
        //    ShowAgentVariables model = new ShowAgentVariables();
        //    model.PicPath = locPic;
        //    return View(model);
        //}
    }
}