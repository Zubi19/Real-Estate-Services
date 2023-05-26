using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class LoginModel 
    {
        public int AgentId;
        public string Email { get; set; }
        public string Pass { get; set; }
        //LoginModel m = null;
        public bool Verify()
        {
            SqlCommand sc = new SqlCommand("Loginall", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;


            sc.Parameters.AddWithValue("@UserEmail", this.Email);
            // sc.Parameters.AddWithValue("@Password", a.Pass);

            SqlDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            if(dt.Rows.Count>0)
            {
                if(dt.Rows[0][0].ToString()==this.Pass)
                {
                    
                    //List<LoginModel> Li = new List<LoginModel>();
                    //m = new LoginModel();
                     GetUserDetail();
                    return true;
                }
            }
            return false;
        }
        public static int id { get; set; }
        public static string name { get; set; }
        public static string email { get; set; }
        public  string tel{get;set;}
        List<LoginModel> userlist = new List<LoginModel>();
       
        public void GetUserDetail()
        {
            email = this.Email;

            SqlCommand cmd = new SqlCommand("userdetail", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@email", this.Email);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {

               // a = new LoginModel();
                id = Convert.ToInt32(rd.GetInt32(0));
                name = Convert.ToString(rd.GetSqlValue(1));
               // a.Email = Convert.ToString(rd.GetSqlValue(2));
                tel = Convert.ToString(rd.GetSqlValue(3));
                //userlist.Add(m);

                GlobalVariables.tel = this.tel;
            }
           // DBConnection.CloseConnection();
            //return userlist;
        }
        public bool AgentVerify()
        {
            SqlCommand sc = new SqlCommand("LoginAgent", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;


            sc.Parameters.AddWithValue("@AgentEmail", this.Email);
            // sc.Parameters.AddWithValue("@Password", a.Pass);

            SqlDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == this.Pass)
                {
                    id = Convert.ToInt32(dt.Rows[0][1].ToString());
                    email = this.Email;
                    
                    //List<LoginModel> Li = new List<LoginModel>();
                    //LoginModel m = new LoginModel();
                    GetAgent(this.Email);

                    return true;
                }
            }
            return false;
        }
        public void GetAgent(string email)
        {
            

            SqlCommand cmd = new SqlCommand("agenttel", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {

                //// a = new LoginModel();
                //id = Convert.ToInt32(rd.GetInt32(0));
                //name = Convert.ToString(rd.GetSqlValue(1));
                //// a.Email = Convert.ToString(rd.GetSqlValue(2));
                //tel = Convert.ToString(rd.GetSqlValue(3));
                ////userlist.Add(m);

                GlobalVariables.tel = Convert.ToString(rd["Agent_contact"]);
            }
            // DBConnection.CloseConnection();
            //return userlist;
        }
        public string address { get; set; }
        public int fess { get; set; }
        
        public int experience { get; set; }
        public string description { get; set; }
        public string Area1 { get; set; }
        public string Area2 { get; set; }
        public string Area3 { get; set; }
        public string Buy { get; set; }
        public string Contractor { get; set; }
        public string Other { get; set; }
        //List<LoginModel> AgentList = new List<LoginModel>();
        //public List<LoginDetailVariables> GetAgenDetails();
        public List<LoginDetailVariables> GetAgentDetail()
        {
            //id = AgentId;
            List<LoginDetailVariables> agentlist = new List<LoginDetailVariables>();

            SqlCommand cmd = new SqlCommand("ShowAgentDetail", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@id", AgentId);
            SqlDataReader rd = cmd.ExecuteReader();
            //while (rd.Read())
           
            while (rd.Read())
            {
                
                agentlist.Add(
                        new LoginDetailVariables
                        {
                //a = new LoginModel();
                            id = Convert.ToInt32(rd["Agent_id"]),
                            Email = Convert.ToString(rd["Agent_email"]),
                            name = Convert.ToString(rd["Agent_name"]),
                            tel = Convert.ToString(rd["Agent_contact"]),
                            address = Convert.ToString(rd["Agent_address"]),
                            fess = Convert.ToInt32(rd["Agent_fees"]),
                            experience = Convert.ToInt32(rd["Agent_experience"]),
                            description = Convert.ToString(rd["Agent_description"]),
                            imageName = Convert.ToString(rd["Name"]),
                            ContentType = Convert.ToString(rd["ContentType"]),
                Data = (byte[])rd["Data"],
                            Area1 = Convert.ToString(rd["Areas"]),
                            Type = Convert.ToString(rd["Agent_type"]),
                            //reviews=Convert.ToString(rd["Reviews"])
                        });

            }
            return agentlist;
        }
    }
}