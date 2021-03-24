
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.Entities.Common;

namespace WebSiteStatistics.DataLayer.Entities.Statistics
{
    public class Country : BaseEntity
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int ViewCount { get; set; }
    }
}
