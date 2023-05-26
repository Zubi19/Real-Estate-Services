using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP2.Models;

namespace FYP2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {if(Session["Login"]=="admin")
            return View();
        return RedirectToAction("Login", "Login");
        }
        public ActionResult EnterArea()
        {
            if (Session["Login"] == "admin")
                return View();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult EnterBlock()
        {
            if (Session["Login"] == "admin")
                return View();
            return RedirectToAction("Login", "Login");
        }
        Admin a = new Admin();
        [HttpPost]
        public ActionResult AdminEnterArea()
        {
            a.area = Request["name"];
            a.block = Request["block"];
            a.EnterAreas();

            return View("EnterArea");
           
        }
        public ActionResult AdminEnterBlock()
        {
            a.area = Request["area"];
            a.block = Request["block"];
            a.EnterBlocks();

            return View("EnterArea");

        }
    }
}