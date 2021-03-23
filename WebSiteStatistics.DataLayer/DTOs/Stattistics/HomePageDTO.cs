using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class HomePageDTO
    {
        public StatisticsDTO Statistics { get; set; }
        public List<BrowserTableDTO> BrowserTables { get; set; }
        public List<OsTableDTO> osTables { get; set; }
        public List<ReferrerDTO> referrers { get; set; }
        public List<PageViewDTO> pageViews { get; set; }
    }
}
