using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class ShowRateReview
    {
        public int rating { get; set; }
        public string reviews { get; set; }

        public static int GetRating(int id)
        {
            SqlCommand cmd = new SqlCommand("ShowRating", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@agentid", id);
            object rating = cmd.ExecuteScalar();
            if (rating != DBNull.Value)
                return Convert.ToInt32(rating);
            else
                return 0;
            
        }

        public static int ratingcount(int id)
        {
            SqlCommand cmd = new SqlCommand("maxrating", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@agentid", id);
            int ratingcount = (int)cmd.ExecuteScalar();
            return ratingcount;
        }
        public static DataTable GetReview(int id)
        {
            ArrayList rowList = new ArrayList();
            SqlCommand cmd = new SqlCommand("ShowReview", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@agentid", id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            sda.Dispose();
            return dt;
        }
    }
}