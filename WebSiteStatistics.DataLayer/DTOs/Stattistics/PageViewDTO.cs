using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class PageViewDTO
    {
        public string PageUrl { get; set; }
        public int PageViewCount { get; set; }
    }
}