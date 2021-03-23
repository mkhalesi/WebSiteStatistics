using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class CurrentVisitorDTO
    {
        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public string CountryName { get; set; }
        public string OsName { get; set; }
    }
}
