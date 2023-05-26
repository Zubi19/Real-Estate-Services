using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class AreaBlock
    {

        List<AreaBlock> BlockList = new List<AreaBlock>();
        public String Block { get; set; }


        AreaBlock ab = null;
        public List<AreaBlock> GetBlock()
        {
            SqlCommand cmd = new SqlCommand("Select * from tblBlock", DBConnection.GetConnection());

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {

                ab = new AreaBlock();
                ab.Block = Convert.ToString(rd.GetSqlValue(1));
                BlockList.Add(ab);


            }
            rd.Close();
            return BlockList;
        }


        public static DataTable TableBlock()
        {
            SqlCommand cmd = new SqlCommand("Select * from tblBlock", DBConnection.GetConnection());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            sda.Dispose();
            return dt;
        }
    }
}