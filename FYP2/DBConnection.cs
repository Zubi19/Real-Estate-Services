using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2
{
    public class DBConnection
    {
        private static SqlConnection SqlConnection;

        public static SqlConnection GetConnection()
        {
            if (SqlConnection == null)
            {
                SqlConnection = new SqlConnection(@"Data Source=(local)\sqlexpress;Initial Catalog=FYP;Integrated Security=SSPI;MultipleActiveResultSets=true");
                SqlConnection.Open();
            }
            return SqlConnection;
        }
        //(local)\sqlexpress
        //DESKTOP-OL2ASPL
        public static SqlConnection CloseConnection()
        {
            SqlConnection.Close();
            return SqlConnection;
        }
    }
}