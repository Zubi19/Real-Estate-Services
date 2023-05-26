using FYP.Models;
using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AgentLogin()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string submit)
        {
            switch (submit)
            {
                case "agent":
                    // delegate sending to another controller action
                    return (agent());
                case "user":
                    // call another action to perform the cancellation
                    return (user());
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return (View());
            }
            
        }
        LoginModel lm = new LoginModel();
        private ActionResult user()
        {
            lm.Email = Request["email"];
            lm.Pass = Request["pass"];
            bool chk = lm.Verify();
            if (chk == true)
            {
                Session["Login"] = "ok";
                return RedirectToAction("Home", "Home");
            }
                return View();
            
        }
        private ActionResult agent()
        {
            lm.Email = Request["email"];
            lm.Pass = Request["pass"];
            bool chk = lm.AgentVerify();
            if (chk == true)
            {
                Session["Login"] = "ok";
                return RedirectToAction("Home", "Home");
            }
                return View();
            
        }
        Admin admin = new Admin();
        public ActionResult AdminLogin()
        {
            admin.uname = Request["uname"];
            admin.pass = Request["pass"];
            bool chk=admin.Verify();
            if(chk==true)
            {
                Session["Login"] = "admin";
                return RedirectToAction("Admin", "Admin");
            }
            return View();


        }
    }
}