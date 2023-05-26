using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class Profile
    {
        public List<ShowAgentVariables> ShowHistory( )
        {

            List<ShowAgentVariables> agentlist = new List<ShowAgentVariables>();

            SqlCommand cmd = new SqlCommand("ShowHistory", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", LoginModel.id);
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