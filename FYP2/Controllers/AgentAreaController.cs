using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP2.Controllers
{
    public class AgentAreaControllerController : Controller
    {
        // GET: AgentArea
        //public ActionResult AgentArea()
        //{
        //    AgentArea a = new AgentArea();
        //    List<AgentArea> Li = new List<AgentArea>();

        //    Li = a.GetArea();
        //    ViewBag.AgentArea = new SelectList(a.GetArea(), "Area");
        //    ViewData["Person"] = Li;
        //    return View();

        //}

        public ActionResult GetBlocks(int AreaID)
        {
            AgentArea a = new AgentArea();
            DataTable dt = new DataTable();
            dt = a.GetBlockByArea(AreaID);

            var blocks = new List<string>();
            foreach (DataRow item in dt.Rows)
            {
                blocks.Add(Convert.ToString(item["BlockName"]));
            }
            
            //blocks.Add("Block 1");
            //blocks.Add("Block 12");
            //blocks.Add("Block 13");
            //blocks.Add("Block 14");
            return Json(blocks, JsonRequestBehavior.AllowGet);
            //return Json(Li, JsonRequestBehavior.AllowGet);
        
        }
    }
}