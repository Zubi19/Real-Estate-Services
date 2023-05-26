using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class AgentRegisteration
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public List<int> area1=new List<int>();// { get; set; }
        //public string area2 { get; set; }
        //public string area3 { get; set; }
        public List<string> AgentType = new List<string>();
        public List<string> Block = new List<string>();
        public int fee { get; set; }
        public int experience { get; set; }
        //public string property { get; set; }
        //public string contractor { get; set; }
        //public string other { get; set; }
        public string description { get; set; }
        public List<ImageModel> image = new List<ImageModel>();
        
        public AgentRegisteration(string name, string email, string password, string tel, string address, int area1, int area2, int area3,string selectblock,string selectblock2, string selectblock3, int fee, int experience, string property, string contractor, string other, string description)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.tel = tel;
            this.address = address;
            this.area1.Add(area1);
            this.area1.Add(area2);
            this.area1.Add(area3);
            this.Block.Add(selectblock);
            this.Block.Add(selectblock2);
            this.Block.Add(selectblock3);
            this.fee = fee;
            this.experience = experience;


            //this.other = other;
            this.description = description;
            if(string.IsNullOrEmpty(property) )
            {
               //this.AgentType.Add(property) = "off";
            }
            else 
            {
                this.AgentType.Add("Property");
            }
            if (contractor == null)
            {
                
            }
            else
            {
                this.AgentType.Add("Contractor");
            }
            if (other == "")
            {

            }
            else
            {
                this.AgentType.Add(other);
            }
            //this.image.Add(imagename);
           
        }
        public void EnterAgentArea(AgentRegisteration a, string[] block, string[] block2, string[] block3)
        {
            
            for (int j = 0; j < area1.Count;j++ )
            {
                if(a.area1[j]!=0)
                {
                SqlCommand sc = new SqlCommand("AgentEnterAreas", DBConnection.GetConnection());
                sc.CommandType = CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@area", a.area1[j]);
                sc.ExecuteNonQuery();
                EnterAgentBlock(a, j,block,block2,block3);
                }
                
            }
                
        }
        int blockcount = 0;
       
        public void EnterAgentBlock(AgentRegisteration a,int j,string[] block,string[] block2,string[] block3)
        {
            if (j == 0)
            {

                for (int i = 0; i < block.Length; i++)
                {
                    if (!string.IsNullOrEmpty(block[i]))
                    {
                        SqlCommand sc = new SqlCommand("AgentEnterBlock", DBConnection.GetConnection());
                        sc.CommandType = CommandType.StoredProcedure;
                        sc.Parameters.AddWithValue("@blockname", block[i]);
                        sc.Parameters.AddWithValue("@area", a.area1[j]);
                        // sc.Parameters.AddWithValue("@blockcount", blockcount);
                        sc.ExecuteNonQuery();
                    }
                }
            }
            if(j==1)
            {
                for (int i = 0; i < block2.Length; i++)
                {
                    if (!string.IsNullOrEmpty(block2[i]))
                    {
                        SqlCommand sc = new SqlCommand("AgentEnterBlock", DBConnection.GetConnection());
                        sc.CommandType = CommandType.StoredProcedure;
                        sc.Parameters.AddWithValue("@blockname", block2[i]);
                        sc.Parameters.AddWithValue("@area", a.area1[j]);
                        // sc.Parameters.AddWithValue("@blockcount", blockcount);
                        sc.ExecuteNonQuery();
                    }
                }
            }
            if(j==2)
            {
                for (int i = 0; i < block3.Length; i++)
                {
                    if (!string.IsNullOrEmpty(block3[i]))
                    {
                        SqlCommand sc = new SqlCommand("AgentEnterBlock", DBConnection.GetConnection());
                        sc.CommandType = CommandType.StoredProcedure;
                        sc.Parameters.AddWithValue("@blockname", block3[i]);
                        sc.Parameters.AddWithValue("@area", a.area1[j]);
                        // sc.Parameters.AddWithValue("@blockcount", blockcount);
                        sc.ExecuteNonQuery();
                    }
                }
            }
        }
        public void EnterAgentType(AgentRegisteration a)
        {

            for (int i = 0; i < AgentType.Count; i++)
            {
                SqlCommand sc = new SqlCommand("AgentType", DBConnection.GetConnection());
                sc.CommandType = CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@type", a.AgentType[i]);
                sc.ExecuteNonQuery();
            }

        }
        public void enterdata(AgentRegisteration a, HttpPostedFileBase postedFile, string[] block,string[] block2,string[] block3)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            SqlCommand sc = new SqlCommand("AgentRegister", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@Agent_name", a.name);

            sc.Parameters.AddWithValue("@Agent_email", a.email);
            sc.Parameters.AddWithValue("@Password", a.password);

            sc.Parameters.AddWithValue("@Agent_contact", a.tel);
            sc.Parameters.AddWithValue("@Agent_address", a.address);
           
            sc.Parameters.AddWithValue("@Agent_fees", a.fee);
            sc.Parameters.AddWithValue("@Agent_experience", a.experience);
          
            sc.Parameters.AddWithValue("@Agent_description", a.description);
            sc.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
            sc.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
            sc.Parameters.AddWithValue("@Data", bytes);
            sc.ExecuteNonQuery();
            EnterAgentArea(a,block,block2,block3);
            
            EnterAgentType(a);
            
        }

        private void EnterAgentArea()
        {
            throw new NotImplementedException();
        }
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
        public static DataTable Areas()
        {
            SqlCommand sc = new SqlCommand("select Areas from tblAreas", DBConnection.GetConnection());

            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            return dt;
        }
    }
}