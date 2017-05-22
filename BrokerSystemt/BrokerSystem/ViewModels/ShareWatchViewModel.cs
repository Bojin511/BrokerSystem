using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;

namespace BrokerSystem.ViewModels
{
    public class ShareWatchViewModel
    {
        public TablesViewModel TablesViewModel { get; set; }
        public CompanyViewModel CompanyViewModel { get; set; }        
        public StockChart StockChart { get; set; }
        public ShareholderViewModel ShareholderViewModel { get; set; }

        public ShareWatchViewModel()
        {
            TablesViewModel = new TablesViewModel();
            CompanyViewModel = new CompanyViewModel();
            ShareholderViewModel = new ShareholderViewModel();
            StockChart = new StockChart();
        }

    }

    public class TablesViewModel
    {
        //public IQueryable<BROKER_SHARE_WATCH> BROKER_SHARE_WATCH { get; set; }
        //public IQueryable<COMPANy> COMPANy { get; set; }
        //public IQueryable<SHARE_HOLDER_BROKER> SHAER_HOLDER_BROKER { get; set; }
    }

    public class CompanyViewModel
    {
        //public COMPANy COMPANy { get; set; }
        public int COMPANY_ID { get; set; }
    }

    public class ShareholderViewModel
    {
        //public SHARE_HOLDER_BROKER SHARE_HOLDER_BROKER { get; set; }
        public int SHARE_HOLDER_ID { get; set; }
    }

    public class StockChart
    {
        public Highcharts Highcharts { get; set; }
    }


}