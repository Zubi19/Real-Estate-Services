using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class Blog1Controller : Controller
    {
        // GET: Blog1
        public ActionResult Blog1()
        {
            ViewData["Countries"] = new List<string>{
                "PAKISTAN",
                    "india",
                    "bANGLADESH"
                };
            return View();
        }
    }
}