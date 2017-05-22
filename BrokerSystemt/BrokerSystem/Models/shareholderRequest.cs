using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrokerSystem.Models
{
    public class shareholderRequest 
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string shareholderName { get; set; }
        public string stockEx { get; set; }
        public string transactionType { get; set; }
        public int shareAmount { get; set; }
    }
}
