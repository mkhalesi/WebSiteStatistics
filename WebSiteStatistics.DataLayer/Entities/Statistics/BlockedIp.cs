
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.Entities.Common;

namespace WebSiteStatistics.DataLayer.Entities.Statistics
{
    public class BlockedIp : BaseEntity
    {
        public string IpAddress { get; set; }
    }
}
