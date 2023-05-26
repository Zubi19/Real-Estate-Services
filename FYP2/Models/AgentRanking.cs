using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class AgentRanking
    {
        public  int GetRanking(string id)
        {
            SqlCommand cmd = new SqlCommand("getscore", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@agentid", id);
            object score = cmd.ExecuteScalar();
            if (score != DBNull.Value)
               return Convert.ToInt32(score);
            else
                return 0;
            
        }
        public int AgentrRankExist(string id)
        {
            SqlCommand cmd = new SqlCommand("checkagentrank", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@agentid", id);
            object score = cmd.ExecuteScalar();
            if (score != DBNull.Value)
                return Convert.ToInt32(score);
            else
                return 0;

        }

        public void updaterank(string id, int rank)
        {
            SqlCommand cmd = new SqlCommand("updatescore", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@score", rank);
            cmd.ExecuteReader();
        }
        public void insertrank(string id, int rank)
        {
            SqlCommand cmd = new SqlCommand("insertscore", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@score", rank);
            cmd.ExecuteReader();
        }
        public List<ShowAgentVariables> GetAgent()
        {
           
                List<ShowAgentVariables> agentlist = new List<ShowAgentVariables>();

                SqlCommand cmd = new SqlCommand("ShowAgentRank", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@area", a.SelectArea);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();


                sd.Fill(dt);


                foreach (DataRow dr in dt.Rows)
                {
                    agentlist.Add(
                        new ShowAgentVariables
                        {
                            Id = Convert.ToInt32(dr["AgentId"]),
                            Name = Convert.ToString(dr["Agent_name"]),
                            ContactNo = Convert.ToString(dr["Agent_contact"]),
                            area = Convert.ToString(dr["Areas"]),
                            email = Convert.ToString(dr["Agent_email"]),
                            type = Convert.ToString(dr["Agent_type"]),
                            //For pic
                            ContentType = dr["ContentType"].ToString(),
                            Data = (byte[])dr["Data"],
                            imageName = dr["Name"].ToString(),


                        });
                }
                return agentlist;
            
        }
    }
}