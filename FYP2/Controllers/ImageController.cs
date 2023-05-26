using FYP.Models;
using FYP2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP2.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Image()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Image(HttpPostedFileBase postedFile)
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


            return View();
        }
        public FileResult DownloadFile()
        {
            byte[] bytes;
            string fileName, contentType;
            //string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * FROM tblImages ";
                //cmd.CommandText = "SELECT Name, Data, ContentType FROM tblImages WHERE Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", fileId);
                cmd.Connection = DBConnection.GetConnection();
                //con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                //con.Close();
            }


            return File(bytes, contentType, fileName);
        }
        private static List<ImageModel> GetFiles()
        {
            List<ImageModel> files = new List<ImageModel>();
            //string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM tblImages"))
            {
                cmd.Connection = DBConnection.GetConnection();
                //con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        files.Add(new ImageModel
                        {
                            //Id = Convert.ToInt32(sdr["Id"]),
                            Name = sdr["Name"].ToString()
                        });
                    }
                }
                //        con.Close();
            }

            return files;
        }
    }
}