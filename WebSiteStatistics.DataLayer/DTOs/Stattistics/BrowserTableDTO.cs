using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class BrowserTableDTO
    {
        private string _BrowserIcon;
        public string BrowserIcon
        {
            get { return _BrowserIcon; }
            set
            {
                switch (value)
                {
                    case "InternetExplorer":

                        _BrowserIcon = "internet-explorer";
                        break;
                    case "IE":
                        _BrowserIcon = "internet-explorer";
                        break;
                    case "Unknown":
                        _BrowserIcon = "question-circle";
                        break;
                    case "Mozilla":
                        _BrowserIcon = "firefox";
                        break;
                    default:
                        _BrowserIcon = value;
                        break;
                }
            }
        }
        public string BrowserName { get; set; }
        public int BrowserViewCount { get; set; }
        public int TottalVisits { get; set; }
    }
}
