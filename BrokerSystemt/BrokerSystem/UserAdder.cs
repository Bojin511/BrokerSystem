using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;


namespace Utils
{
    public class UserAdder
    {
        public static void insertUser(string username, string password, string firstname, string lastname, string email, string DOB, string role)
        {
            //XDocument xdoc = XDocument.Load(@"../../Users.xml");
            //XElement xUsr = new XElement("USER",
            //                    new XElement("USERNAME", username),
            //                    new XElement("PASSWORD", password),
            //                    new XElement("FIRST_NAME", firstname),
            //                    new XElement("LAST_NAME", lastname),
            //                    new XElement("EMAIL", email),
            //                    new XElement("DOB", DOB),
            //                    new XElement("ROLE", role));

            //xdoc.Element("USERS").Add(xUsr);
            //xdoc.Save(@"../../Users.xml");


            string query = @"insert into Users values ('" + username + "', '" + password + "', '" + firstname + "', '" + lastname + "', '" + email + "', '" + DOB + "', '" + role + "')";
            SqlConnection Cn = new SqlConnection(ConfigurationManager.ConnectionStrings["xmlDBOrigConnectionString1"].ConnectionString);
            SqlCommand Com = new SqlCommand(query, Cn);
            Cn.Open();
            int res = Com.ExecuteNonQuery();
            Cn.Close();


        }
    }
}
