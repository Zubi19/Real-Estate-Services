using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class RateReview
    {
        public int rating { get; set; }
        public string review { get; set; }

        public void insert(string id)
        {
            SqlCommand sc = new SqlCommand("RateReview", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@Rate", rating);

            sc.Parameters.AddWithValue("@Review", review);
            sc.Parameters.AddWithValue("@userid", LoginModel.id);
            sc.Parameters.AddWithValue("@agentid", id);
            sc.Parameters.AddWithValue("@datetime", DateTime.Now);
            sc.ExecuteNonQuery();
        }
    }
}