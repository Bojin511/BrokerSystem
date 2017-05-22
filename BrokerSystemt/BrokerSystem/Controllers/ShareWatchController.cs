using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;
using BrokerSystem.ViewModels;
using DotNet.Highcharts.Options;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

namespace BrokerSystem.Controllers
{
    public class ShareWatchController : Controller
    {
        private xmlDBOrigEntities1 db = new xmlDBOrigEntities1();
        private ShareWatchViewModel vm = new ShareWatchViewModel();

        public ActionResult Index()
        {
            var company = db.companies;
            
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetTitle(new Title
            {
                Text = "Please select a company!"
            });

            var shareid = db.shares.Where(x => x.company_id == 1).Select(x => x.company_id).FirstOrDefault();
            var sharedate = db.shares_prices.Where(x => x.share_id == shareid).Select(c => c.time_start).ToArray();
            var shareprices = db.shares_prices.Where(c => c.share_id == shareid).Select(c => c.price).ToArray();

            Object[] dbpricetoarray = new object[shareprices.Length];
            string[] dbdatetoarray = new string[sharedate.Length];

            for (int i = 0; i < shareprices.Length; i++)
            {
                dbpricetoarray[i] = Convert.ChangeType(shareprices[i], typeof(object));
            }

            for (int i = 0; i < dbdatetoarray.Length; i++)
            {
                dbdatetoarray[i] = sharedate[i].ToShortDateString();
            }

            var stockchart = new Highcharts("chart")
            .InitChart(new Chart
            {
                ZoomType = ZoomTypes.X,
                SpacingRight = 5
            })
            .SetTitle(new Title
            {
                Text = "Share Price vs Time"
            })
            .SetSubtitle(new Subtitle
            {
                Text = "Click and drag in the plot area to zoom in"
            })
            .SetXAxis(new XAxis
            {
                Categories = dbdatetoarray,
                TickInterval = 10
            })
            .SetSeries(new Series
            {
                Data = new Data(dbpricetoarray),
            });


            //vm.TablesViewModel.COMPANy = _company;
            vm.CompanyViewModel.COMPANY_ID = 1;
            vm.StockChart.Highcharts = stockchart;

            return View(vm);
        }

    }
}
