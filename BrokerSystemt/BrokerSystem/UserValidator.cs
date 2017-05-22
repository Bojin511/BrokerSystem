using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace Utils
{
    public class UserValidator
    {
        public static int getUserByUsername(string Username, string Password)
        {
            //XDocument xdoc = XDocument.Load(@"../../Users.xml");
            //var query = from usr in xdoc.Descendants("USER")
            //            where
            //            usr.Element("ROLE").Value == "System Administrator"
            //            && usr.Element("USERNAME").Value == Username
            //            && usr.Element("PASSWORD").Value == Password
            //            select usr;
            //return query.Count();


            string query = @"select * from Users where username = '" + Username + "' AND password = '" + Password + "' AND role = 'broker'";
            SqlConnection Cn = new SqlConnection(ConfigurationManager.ConnectionStrings["xmlDBOrigConnectionString1"].ConnectionString);
            SqlCommand Com = new SqlCommand(query, Cn);
            Cn.Open();
            SqlDataReader reader = Com.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
            }
            Cn.Close();
            return i;
        }


    }
}

