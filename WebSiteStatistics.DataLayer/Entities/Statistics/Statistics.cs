
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.Entities.Common;

namespace WebSiteStatistics.DataLayer.Entities.Statistics
{
    public class Statistics : BaseEntity
    {
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string UserOs { get; set; }
        public string Referer { get; set; }
        public string PageViewed { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
