using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class SubDetailsDTO
    {
        public int Today { get; set; }
        public int LastDay { get; set; }
        public int ThisMonth { get; set; }
        public int ThisYear { get; set; }
        public string PeakDate { get; set; }
        public string LowDate { get; set; }
    }
}
