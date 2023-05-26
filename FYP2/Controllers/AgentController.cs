using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;
using System.IO;
using System.Data.SqlClient;
using FYP2.Models;

namespace FYP2.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult AgentRegister()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(FormCollection fc, HttpPostedFileBase postedFile)
        {



            AgentRegisteration a = new AgentRegisteration((fc["name"]), fc["email"], (fc["password"]), Convert.ToString(fc["tel"]), 
                (fc["address"]), Convert.ToInt32(fc["area1"]), Convert.ToInt32(fc["area2"]), Convert.ToInt32(fc["area3"]),fc["selectblock"],fc["selectblock2"],fc["selectblock3"], Convert.ToInt32(fc["fees"]), Convert.ToInt32(fc["experience"]),
                fc["Property"], fc["Contractor"], fc["other"], fc["description"]);
            string[] selectedblocks = fc["selectblock"].Split(new char[] { ',' });
            string selected = fc["selectblock2"];
            string[] selectedblocks2 = null;
            if (selected != null)
            {
                 selectedblocks2 = fc["selectblock2"].Split(new char[] { ',' });
            }
            string selectedbl = fc["selectblock3"];
            string[] selectedblocks3 = null;
            if (selectedbl != null)
            {
                selectedblocks3 = fc["selectblock3"].Split(new char[] { ',' });
            }
                //string[] selectedblocks3 = fc["selectblock3"].Split(new char[] { ',' });
            a.enterdata(a,postedFile,selectedblocks,selectedblocks2,selectedblocks3);
            //Image(postedFile);
            return View();

        }
        [HttpPost]
        public void Image(HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            //string constr = DBConnection.GetConnection();
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            string query = "INSERT INTO tblImages (Name,ContentType, Data) VALUES (@Name, @ContentType, @Data)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = DBConnection.GetConnection();
                cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                cmd.Parameters.AddWithValue("@Data", bytes);
                //con.Open();
                cmd.ExecuteNonQuery();
                //con.Close();
            }


            //return View();
        }
    }
}