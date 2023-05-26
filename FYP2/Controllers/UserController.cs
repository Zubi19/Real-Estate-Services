using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP2.Models;

namespace FYP2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {


            UserRegistration a = new UserRegistration((fc["name"]), fc["email"], fc["password"], (fc["contact"]));
            UserRegistration.enterdata(a);
            return View();

        }
    }
}