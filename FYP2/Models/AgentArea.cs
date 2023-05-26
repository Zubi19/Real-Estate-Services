using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class AgentArea
    {

        List<AgentArea> AreaList = new List<AgentArea>();
        public String Area { get; set; }


        AgentArea a = null;
        //public List<AgentArea> GetArea()
        //{
        //    SqlCommand cmd = new SqlCommand("Select * from tblAreas", DBConnection.GetConnection());

        //    SqlDataReader rd = cmd.ExecuteReader();
        //    while (rd.Read())
        //    {

        //        a = new AgentArea();
        //        a.Area = Convert.ToString(rd.GetSqlValue(1));
        //        AreaList.Add(a);


        //    }
        //    rd.Close();
        //    return AreaList;
        //}


        public DataTable GetBlockByArea(int AreaID)
        {
            SqlCommand cmd = new SqlCommand(string.Format("Select * from tblBlock where Area_Id = {0}", AreaID), DBConnection.GetConnection());

             SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            sda.Dispose();
            return dt;
        }

        public static DataTable TableAea()
        {
            SqlCommand cmd = new SqlCommand("GetArea", DBConnection.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            sda.Dispose();
            return dt;
        }
    }
}