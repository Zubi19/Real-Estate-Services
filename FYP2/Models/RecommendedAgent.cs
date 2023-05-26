using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class RecommendedAgent
    {
        public string area;
        public string type;
       // public int rating=4;

        Dictionary<int,string> area1=new Dictionary<int,string>();
        Dictionary<int, string> area2 = new Dictionary<int, string>();
        Dictionary<int, string> area3 = new Dictionary<int, string>();
        Dictionary<int, string> type1 = new Dictionary<int, string>();
        Dictionary<int, string> type2 = new Dictionary<int, string>();
        Dictionary<int, string> type3 = new Dictionary<int, string>();

        Dictionary<int, int> agentrate = new Dictionary<int, int>();
        ArrayList agentarea=new ArrayList();
        ArrayList agenttype = new ArrayList();
        ArrayList agentrateid = new ArrayList();
        ArrayList agentid = new ArrayList();
        int[] array;
        //ArrayList agent = new ArrayList();
        //public string[] agentarea;
        //public string[] agenttype;
        //public int [] agentrate;
        public void OverallAgentRating()
        {
            SqlCommand cmd = new SqlCommand("OverallRating", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            int a;
            int b;

            sd.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                a=Convert.ToInt32(dr["id"]);
                b=Convert.ToInt32(dr["rating"]);
                agentrateid.Add(a);
                agentrate.Add(a, b);
                //agentrate.Add(dr[""]);
                
            }
             array = agentrateid.ToArray(typeof(int)) as int[];
            GetAgent(array);
        }
        public void GetAgent(int[] id)
        {
            int a;
            int previousagentid=0;
            int previousagentid2=-1;
            string previousarea="";
            string previousarea2="";
            string previoustype="";
            string previoustype2="";
            string previousarea3 = "";
            string previoustype3 = "";
            string previoustype4 = "";
            string b;
            string c;
            DataTable d = new DataTable();
            d.Columns.Add("AgentId");
            for(int i=0;i<id.Length;i++)
            {
                
                d.Rows.Add(id[i]);
            }
            SqlCommand cmd = new SqlCommand("AgentListRecomenAlgo", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp=cmd.Parameters.AddWithValue("@List", d);
            sp.SqlDbType = SqlDbType.Structured;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable();


            sd.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["AgentId"]);
                b=dr["AgentArea"].ToString();
                
               
                 if(a==previousagentid2 &&  b!=previousarea3 && b!=previousarea2)
                {
                    area1.Add(a, b);
                     previousarea3=b;
                }
                 else    if(a==previousagentid && b!=previousarea2 && b!=previousarea && b!=previousarea3)
                {
                    area2.Add(a, b);
                    previousagentid2 = a;
                    previousarea2 = b;
                    
                }
                else if(a!=previousagentid && a!=previousagentid2)
                {
                    area3.Add(a, b);
                    previousagentid = a;
                    previousarea = b;
                }
               
            }
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["AgentId"]);
                c = dr["Agent_type"].ToString();


                if (previousagentid2 == a && previoustype3 != c && c != previoustype2 && c!= previoustype&&c!=previoustype4 )
                 {
                     if (!type1.ContainsKey(a))
                {
                    type1.Add(a, c);
                    previoustype3 = c;
                }
                 }
                else if (previousagentid == a && previoustype2 != c && c != previoustype && c != previoustype3 )
                {
                    if (!type2.ContainsKey(a))
                    {
                        type2.Add(a, c);
                        previousagentid2 = previousagentid;
                        previoustype2 = previoustype;
                        previoustype4 = c;
                    }
                }
                else if(a != previousagentid && a != previousagentid2)
                {
                    type3.Add(a, c);
                    previousagentid = a;
                    previoustype = c;
                }
            }
        }
        int[] VectAgent=new int[6];
        int[] VectUser=new int[6];
        int arraysizr = 0;
        public void VectorFormation()
        {

            for(int i=0;i<array.Length;i++)
            {
                if(agentrate.ContainsKey(array[i]))
                {
                    int value =agentrate[array[i]];
                    if(value>=60)
                    {
                        VectAgent[0] = 1;
                        VectUser[0] = 1;
                        arraysizr = 1;
                    }
                    else
                    {
                        VectAgent[0] = 0;
                        VectUser[0] = 1;
                        VectAgent[1] = 1;
                        VectUser[1] = 0;
                        arraysizr = 2;
                    }
                }
                if(area3.ContainsKey(array[i]))
                {
                    string value2;
                    string value3;
                    string value1 = area3[array[i]];
                    area2.TryGetValue(array[i],out  value2);
                    area1.TryGetValue(array[i],out  value3);
                    if(value1==area||value2==area||value3==area)
                    {
                        if(arraysizr==2)
                        {
                            VectAgent[2] = 1;
                            VectUser[2] = 1;
                            arraysizr = 3;
                        }
                        else 
                        {
                            VectUser[1] = 1;
                            VectAgent[1] = 1;
                            arraysizr = 2;
                        }
                    }
                    else 
                    {
                        
                    if(arraysizr==2)
                    {
                        VectUser[3] = 0;
                        VectAgent[3] = 1;
                        VectAgent[2] = 0;
                        VectUser[2] = 1;
                        arraysizr = 4;
                    }
                    else
                    {
                        VectUser[1] = 0;
                        VectAgent[1] = 1;
                        VectAgent[2] = 0;
                        VectUser[2] = 1;
                        arraysizr = 3;
                    }
                    }
                }
                if (type3.ContainsKey(array[i]))
                {
                    string value2;
                    string value3;
                    string value1 = type3[array[i]];
                    type2.TryGetValue(array[i], out  value2);
                    type1.TryGetValue(array[i], out  value3);
                    if (value1 == type || value2 == type || (type=="Other"&& value3!=null))
                    {
                        if (arraysizr==4)
                        {
                            VectAgent[4] = 1;
                            VectUser[4] = 1;
                            arraysizr = 5;
                        }
                        else if(arraysizr==3)
                        {
                            VectUser[3] = 1;
                            VectAgent[3] = 1;
                            arraysizr = 4;
                        }
                        else 
                        {
                            VectUser[2] = 1;
                            VectAgent[2] = 1;
                            arraysizr = 3;
                        }
                    }
                    else
                    {

                        if (arraysizr==4)
                        {
                            VectUser[4] = 0;
                            VectAgent[4] = 1;
                            VectAgent[5] = 0;
                            VectUser[5] = 1;
                            arraysizr = 6;
                        }
                        else if(arraysizr==3)
                        {
                            VectUser[3] = 0;
                            VectAgent[3] = 1;
                            VectAgent[4] = 0;
                            VectUser[4] = 1;
                            arraysizr = 5;
                        }
                        else
                        {
                            VectUser[2] = 0;
                            VectAgent[2] = 1;
                            VectAgent[3] = 0;
                            VectUser[3] = 1;
                            arraysizr = 4;
                        }
                    }
                }
                ApplyingCosineSimilarity(array[i]);
            }
        }
       public ArrayList  getAgentId = new ArrayList();
        int[] getRecommendAgentId;
        public void ApplyingCosineSimilarity(int id)
        {
            int ab=0;
            double amod=0;
            double bmod=0;
            int VAlength = VectAgent.Length;
            int VUlength = VectUser.Length;

            if (arraysizr == 3)
            {
                 ab = (VectAgent[0] * VectUser[0]) + (VectAgent[1] * VectUser[1]) + (VectAgent[2] * VectUser[2]);

                 amod = VectAgent[0] * VectAgent[0] + VectAgent[1] * VectAgent[1] + VectAgent[2] * VectAgent[2];
                 bmod = VectUser[0] * VectUser[0] + VectUser[1] * VectUser[1] + VectUser[2] * VectUser[2];
            
            }
            else if(arraysizr== 4)
            {
                 ab = (VectAgent[0] * VectUser[0]) + (VectAgent[1] * VectUser[1]) + (VectAgent[2] * VectUser[2]) + (VectAgent[3] * VectUser[3]);
                 amod = VectAgent[0] * VectAgent[0] + VectAgent[1] * VectAgent[1] + VectAgent[2] * VectAgent[2] + VectAgent[3] * VectAgent[3];
                 bmod = VectUser[0] * VectUser[0] + VectUser[1] * VectUser[1] + VectUser[2] * VectUser[2] + VectUser[3] * VectUser[3];
            
            }
            else if(arraysizr==5)
            {
                 ab = (VectAgent[0] * VectUser[0]) + (VectAgent[1] * VectUser[1]) + (VectAgent[2] * VectUser[2]) + (VectAgent[3] * VectUser[3]) + (VectAgent[4] * VectUser[4]);
                 amod = VectAgent[0] * VectAgent[0] + VectAgent[1] + VectAgent[1] + VectAgent[2] + VectAgent[2]+VectAgent[3] * VectAgent[3]+VectAgent[4] * VectAgent[4];
                 bmod = VectUser[0] * VectUser[0] + VectUser[1] * VectUser[1] + VectUser[2] * VectUser[2]+VectUser[3] * VectUser[3]+VectUser[4] * VectUser[4];
            
            }
            else if(arraysizr==6)
            {
                 ab = (VectAgent[0] * VectUser[0]) + (VectAgent[1] * VectUser[1]) + (VectAgent[2] * VectUser[2]) + (VectAgent[3] * VectUser[3]) + (VectAgent[4] * VectUser[4]) + (VectAgent[5] * VectUser[5]);
                 amod = VectAgent[0] * VectAgent[0] + VectAgent[1] + VectAgent[1] + VectAgent[2] + VectAgent[2]+VectAgent[3] * VectAgent[3]+VectAgent[4] * VectAgent[4]+VectAgent[5] * VectAgent[5];
                 bmod = VectUser[0] * VectUser[0] + VectUser[1] * VectUser[1] + VectUser[2] * VectUser[2]+VectUser[3] * VectUser[3]+VectUser[4] * VectUser[4]+VectUser[5] * VectUser[5];
            
            }
            amod = Math.Sqrt(amod);
            bmod = Math.Sqrt(bmod);
            double result=ab/(amod*bmod);
            if (result > .8)
                getAgentId.Add(id);
            
            
        }
        public List<ShowAgentVariables> getRecommendAgent(ArrayList a)
        {
            getRecommendAgentId = getAgentId.ToArray(typeof(int)) as int[];
            List<ShowAgentVariables> agentlist = new List<ShowAgentVariables>();
            DataTable d = new DataTable();
            d.Columns.Add("AgentId");
            for (int i = 0; i < getRecommendAgentId.Length; i++)
            {

                d.Rows.Add(getRecommendAgentId[i]);
            }
            SqlCommand cmd = new SqlCommand("GetRecommendAgent", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp=cmd.Parameters.AddWithValue("@List", d);
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
                            ContactNo=Convert.ToString(dr["Agent_contact"]),
                            area = Convert.ToString(dr["Areas"]),
                            email=Convert.ToString(dr["Agent_email"]),
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