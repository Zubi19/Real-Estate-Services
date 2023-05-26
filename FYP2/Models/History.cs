using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class History
    {
        
        ArrayList aid = new ArrayList();
        ArrayList rate = new ArrayList();
        ArrayList review = new ArrayList();
        ArrayList date = new ArrayList();
        ArrayList id = new ArrayList();
        int a = 0;
       
        public void getAgentid()
        {
            SqlCommand cmd = new SqlCommand("ShowHistory", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", LoginModel.id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["AgentId"]);
                id.Add(a);
                

            }
            
        }
        int[] getAgentHistory;
        public List<ShowAgentVariables> GetAgent()
        {
            getAgentHistory = id.ToArray(typeof(int)) as int[];
                List<ShowAgentVariables> agentlist = new List<ShowAgentVariables>();
                DataTable d = new DataTable();
                d.Columns.Add("AgentId");
                for (int i = 0; i < getAgentHistory.Length; i++)
                {

                    d.Rows.Add(getAgentHistory[i]);
                }
                SqlCommand cmd = new SqlCommand("ShowAgent1", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sp = cmd.Parameters.AddWithValue("@List", d);
                sp.SqlDbType = SqlDbType.Structured;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();


                sd.Fill(dt);


                foreach (DataRow dr in dt.Rows)
                {
                    agentlist.Add(
                        new ShowAgentVariables
                        {
                            Id = Convert.ToInt32(dr["Agent_id"]),
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
        public  DataTable agentDetail(int agid)
        {
            var items = new List<KeyValuePair<int, int>>();
            int a;
            getAgentHistory = id.ToArray(typeof(int)) as int[];
            List<HistoryVariable> agentlist = new List<HistoryVariable>();
            DataTable d = new DataTable();
            d.Columns.Add("AgentId");
            for (int i = 0; i < getAgentHistory.Length; i++)
            {

                d.Rows.Add(getAgentHistory[i]);
            }
            SqlCommand cmd = new SqlCommand("HistoryDetail", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@id", agid);
             cmd.Parameters.AddWithValue("@uid", LoginModel.id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();


            sd.Fill(dt);
            return dt;
             //foreach (DataRow dr in dt.Rows)
             //{
             //    aid.Add(Convert.ToInt32(dr["AgentId"]));
             //    aid.Add(Convert.ToInt32(dr["Rating"]));
             //    aid.Add(Convert.ToString(dr["Reviews"]));
             //    aid.Add(Convert.ToDateTime(dr["Date"]));

                    
             //   }
                
            }
        }
    }
