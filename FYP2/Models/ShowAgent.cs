
//using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;


namespace FYP2.Models
{
    public class ShowAgent
    {
        public int SelectArea { get; set; }
        public int AreaID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int id { get; set; }
        public string SelectType { get; set; }
        public string SelectBlock { get; set; }
       
        public ShowAgent(int Selectarea, string selectblock,string Selecttype )
        {
            this.SelectArea = Selectarea;
            this.SelectType = Selecttype;
            this.SelectBlock = selectblock;
          
        }
        public DataTable GetArea(ShowAgent a)
        {
            SqlCommand sc = new SqlCommand("SearchAgent", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@area", a.SelectArea);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            return dt;
        }
        

        public List<ShowAgentVariables> GetAgent(ShowAgent a)
        {
            if ((a.SelectType == "Select Type"|| a.SelectType==null) && a.SelectBlock==null)
            {
                List<ShowAgentVariables> agentlist = new List<ShowAgentVariables>();
                FYP2.Controllers.Variables.type = "";
                SqlCommand cmd = new SqlCommand("SearchAgent", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@area", a.SelectArea);
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
            else if(a.SelectType=="Select Type" && a.SelectBlock!=null)
            {
                FYP2.Controllers.Variables.type = "";
                
                List<ShowAgentVariables> studentlist = new List<ShowAgentVariables>();

                SqlCommand cmd = new SqlCommand("SearchAgentwithBlock", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@area", a.SelectArea);
                //cmd.Parameters.AddWithValue("@locationurl", a.SelectArea);
                cmd.Parameters.AddWithValue("@block", a.SelectBlock);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();


                sd.Fill(dt);


                foreach (DataRow dr in dt.Rows)
                {
                    studentlist.Add(
                        
                        new ShowAgentVariables
                        {
                            
                            
                            Id = Convert.ToInt32(dr["Agent_id"]),
                            Name = Convert.ToString(dr["Agent_name"]),
                           ContactNo = Convert.ToString(dr["Agent_contact"]),
                            Description = Convert.ToString(dr["Agent_description"]),
                            email = Convert.ToString(dr["Agent_email"]),
                           // MapUrl = Convert.ToString(dr["Show_Location"]),
                            PicPath = Convert.ToString(dr["Picture_Path"]), /////picture path sai ni dya database main,,,agent infi ke table main  dekhyega ,,picture path kesy dengy
                            area = Convert.ToString(dr["Areas"]),
                            block = Convert.ToString(dr["BlockName"]),
                            ContentType = dr["ContentType"].ToString(),
                            Data = (byte[])dr["Data"],
                            imageName = dr["Name"].ToString(),
                            type = Convert.ToString(dr["Agent_type"]),
                            
                        });
                }
                return studentlist;
            
            }
            else if(a.SelectType=="Property" || a.SelectType=="Contractor")
            {
                
                List<ShowAgentVariables> studentlist = new List<ShowAgentVariables>();
                
                SqlCommand cmd = new SqlCommand("SearchAgentwithType", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@area", a.SelectArea);
                cmd.Parameters.AddWithValue("@block", a.SelectBlock);
                cmd.Parameters.AddWithValue("@type", a.SelectType);


                //SqlCommand cmd = new SqlCommand(string.Format("select info.Agent_id,info.Agent_name,info.Agent_contact,  info.Agent_description,info.Show_Location,info.Picture_Path,area.Areas,block.BlockName from tblAgent_Info info,tblBlock block,tblAreas area where info.Agent_ID in (select Agent_ID from tblAgent_Location loc where loc.Agent_Loc_ID in (select ID from tblBlocks_by_Area where AreaId={0} and BlockId in ({1}))) and block.BlockId in ({1}) and area.Area_Id={0}", a.SelectArea, blocks), DBConnection.GetConnection());
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();


                sd.Fill(dt);


                foreach (DataRow dr in dt.Rows)
                {
                    studentlist.Add(
                        new ShowAgentVariables
                        {

                            Id = Convert.ToInt32(dr["Agent_id"]),
                            Name = Convert.ToString(dr["Agent_name"]),
                            ContactNo = Convert.ToString(dr["Agent_contact"]),
                            Description = Convert.ToString(dr["Agent_description"]),
                            MapUrl = Convert.ToString(dr["Show_Location"]),
                            email = Convert.ToString(dr["Agent_email"]),
                            //PicPath = Convert.ToString(dr["Picture_Path"]), /////picture path sai ni dya database main,,,agent infi ke table main  dekhyega ,,picture path kesy dengy
                            area = Convert.ToString(dr["Areas"]),
                            block = Convert.ToString(dr["BlockName"]),
                            type=Convert.ToString(dr["Agent_type"]),

                            ContentType = dr["ContentType"].ToString(),
                            Data = (byte[])dr["Data"],
                            imageName = dr["Name"].ToString(),
                           

                        });
                }
                return studentlist;
            
            }
            else
            {

                List<ShowAgentVariables> studentlist = new List<ShowAgentVariables>();
                
                SqlCommand cmd = new SqlCommand("SearchAgentwithTypeOther", DBConnection.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@area", a.SelectArea);
                cmd.Parameters.AddWithValue("@block", a.SelectBlock);
                cmd.Parameters.AddWithValue("@type", a.SelectType);


                //SqlCommand cmd = new SqlCommand(string.Format("select info.Agent_id,info.Agent_name,info.Agent_contact,  info.Agent_description,info.Show_Location,info.Picture_Path,area.Areas,block.BlockName from tblAgent_Info info,tblBlock block,tblAreas area where info.Agent_ID in (select Agent_ID from tblAgent_Location loc where loc.Agent_Loc_ID in (select ID from tblBlocks_by_Area where AreaId={0} and BlockId in ({1}))) and block.BlockId in ({1}) and area.Area_Id={0}", a.SelectArea, blocks), DBConnection.GetConnection());
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();


                sd.Fill(dt);


                foreach (DataRow dr in dt.Rows)
                {
                    studentlist.Add(
                        new ShowAgentVariables
                        {

                            Id = Convert.ToInt32(dr["Agent_id"]),
                            Name = Convert.ToString(dr["Agent_name"]),
                            ContactNo = Convert.ToString(dr["Agent_contact"]),
                            Description = Convert.ToString(dr["Agent_description"]),
                            MapUrl = Convert.ToString(dr["Show_Location"]),
                            email = Convert.ToString(dr["Agent_email"]),
                            //PicPath = Convert.ToString(dr["Picture_Path"]), /////picture path sai ni dya database main,,,agent infi ke table main  dekhyega ,,picture path kesy dengy
                            area = Convert.ToString(dr["Areas"]),
                            block = Convert.ToString(dr["BlockName"]),
                            type = Convert.ToString(dr["Agent_type"]),

                            ContentType = dr["ContentType"].ToString(),
                            Data = (byte[])dr["Data"],
                            imageName = dr["Name"].ToString(),


                        });
                }
                return studentlist;

            }
        }

        
    }
}