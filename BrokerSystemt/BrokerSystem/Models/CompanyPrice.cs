using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrokerSystem.Models
{
    public class CompanyPrice 
    {

            public string name { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public int ? issued { get; set; }
            public DateTime ? shares_date_start { get; set; }
            public DateTime ? shares_date_end { get; set; }
            public Decimal ? price { get; set; }
            public DateTime ? price_time_start { get; set; }
            public DateTime ? price_time_end { get; set; }
            public string symbol { get; set; }





    }
}
