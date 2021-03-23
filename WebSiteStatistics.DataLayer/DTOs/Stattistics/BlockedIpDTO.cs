using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatistics.DataLayer.DTOs.Stattistics
{
    public class BlockedIpDTO
    {
        public long Id { get; set; }
        public string IpAddress { get; set; }
    }
}
