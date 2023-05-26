using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class Admin
    {
        public string uname { get; set; }
        public string pass { get; set; }
        public string area { get; set; }
        public string block { get; set; }

        public void EnterAreas()
        {
            List<string> names = block.Split(',').ToList<string>();
            int i = names.Count;
            SqlCommand sc = new SqlCommand("AdminEnterArea", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@area", area);





            sc.ExecuteNonQuery();
            for (int j = 0; j < i; j++)
            {
                SqlCommand scc = new SqlCommand("AdminEnterBlock", DBConnection.GetConnection());
                scc.CommandType = CommandType.StoredProcedure;


                scc.Parameters.AddWithValue("@blockname", names[j]);



                scc.ExecuteNonQuery();

            }
        }
        public void EnterBlocks()
        {
            List<string> names = block.Split(',').ToList<string>();
            int i = names.Count;
            //SqlCommand sc = new SqlCommand("AdminEnterArea", DBConnection.GetConnection());
            //sc.CommandType = CommandType.StoredProcedure;
            //sc.Parameters.AddWithValue("@area", area);





            //sc.ExecuteNonQuery();
            for (int j = 0; j < i; j++)
            {
                SqlCommand scc = new SqlCommand("AdminEnterBlocks", DBConnection.GetConnection());
                scc.CommandType = CommandType.StoredProcedure;

                scc.Parameters.AddWithValue("@areaid", area);
                scc.Parameters.AddWithValue("@blockname", names[j]);



                scc.ExecuteNonQuery();

            }
        }
        public bool Verify()
        {
            SqlCommand sc = new SqlCommand("Loginadmin", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;


            sc.Parameters.AddWithValue("@uname", this.uname);
            // sc.Parameters.AddWithValue("@Password", a.Pass);

            SqlDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == this.pass)
                {

                    
                    return true;
                }
            }
            return false;
        }
    }
}