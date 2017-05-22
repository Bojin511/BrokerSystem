using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace Utils
{
    public class UserLoginLogger
    {
        private static void logAttempt(String username, String attempt)
        {
            //string loginLogFile = "../../loginLog.xml";
            //XDocument xdoc = XDocument.Load(@loginLogFile);

            //XElement xLog = new XElement("LOGIN",
            //            new XElement("USERNAME", username),
            //            new XElement("ATTEMPT", attempt));

            //xdoc.Element("LOGINS").Add(xLog);
            //xdoc.Save(@loginLogFile);

            string query = @"insert into loginLog values ('" + username + "', '" + attempt + "')";
            SqlConnection Cn = new SqlConnection(ConfigurationManager.ConnectionStrings["xmlDBOrigConnectionString1"].ConnectionString);
            SqlCommand Com = new SqlCommand(query, Cn);
            Cn.Open();
            int res = Com.ExecuteNonQuery();
            Cn.Close();

        }

        public static void logSuccess(String username)
        {
            logAttempt(username, "Success");
        }

        public static void logFailed(String username)
        {
            logAttempt(username, "Failed");
        }
    }
}
