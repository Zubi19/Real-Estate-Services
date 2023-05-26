using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP2.Models
{
    public class UserRegistration
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tel { get; set; }
        public UserRegistration(string name,string email,string pass, string tel)
        {

            this.name = name;
            this.email = email;
            this.password = pass;
            this.tel = tel;
        }
        public static void enterdata(UserRegistration a)
        {
            SqlCommand sc = new SqlCommand("UserRegiste", DBConnection.GetConnection());
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.AddWithValue("@username", a.name);

            sc.Parameters.AddWithValue("@useremail", a.email);
            sc.Parameters.AddWithValue("@tel", a.tel);
            sc.Parameters.AddWithValue("@Password", a.password);

            
            
            sc.ExecuteNonQuery();
        }
    }
}