using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
    }
}