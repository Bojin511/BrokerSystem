using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;

namespace BrokerSystem.Controllers
{
    public class ChartController : Controller
    {
        //
        // GET: /Chart/

        public ActionResult Index()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new Series
                {
                    Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                });

            return View(chart);
        }

    }
}
