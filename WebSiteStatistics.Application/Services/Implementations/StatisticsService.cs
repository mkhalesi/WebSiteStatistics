using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAParser;
using WebSiteStatistics.DataLayer.DTOs.Stattistics;
using WebSiteStatistics.DataLayer.Entities.Statistics;
using WebSiteStatistics.DataLayer.Repository;
using WebSiteStatistics.Application.Utilities.Date;
using System.Xml;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebSiteStatistics.Application.Services.Interfaces
{
    public class StatisticsService : IStatisticsService
    {
        #region constructor
        private IGenericRepository<Statistics> statisticsRepository;
        private IGenericRepository<BlockedIp> blockedIpRepository;
        private IGenericRepository<Country> countryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<StatisticsService> _logger;
        private readonly HttpClient _httpClient;
        public StatisticsService(IHttpContextAccessor httpContextAccessor,
            IGenericRepository<Statistics> statisticsRepository,
            IGenericRepository<BlockedIp> blockedIpRepository,
            IGenericRepository<Country> countryRepository,
            ILogger<StatisticsService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            this.statisticsRepository = statisticsRepository;
            this.blockedIpRepository = blockedIpRepository;
            this.countryRepository = countryRepository;
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
            _logger = logger;
        }
        #endregion

        #region add user to Statistic Entity
        public async Task AddUserToStatistic(string ip)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var blockedIP =  blockedIpRepository.GetQuery().AsQueryable()
                .Where(p => !p.IsDelete).ToList();

            if (!blockedIP.Any(ip => ip.IpAddress.Equals(GetIPAddress())))
            {
                var statistic = new Statistics();
                statistic.IpAddress = GetIPAddress();
                statistic.UserOs = GetUserOS(httpContext.Request.Headers["User-Agent"].ToString());
                statistic.PageViewed = httpContext.Request.Path;
                statistic.Referer = httpContext.Request.Headers["Referer"].ToString() ?? "Direct";
                statistic.UserAgent = httpContext.Request.Headers["User-Agent"].ToString();
                statistic.DateStamp = DateTime.Now;
          
                await statisticsRepository.AddEntity(statistic);
                await statisticsRepository.SaveChanges();
                await GetLocation();

                #region Get Country Visitors
                // Get Country Visitors
                //my ipStack Website Details = access_key = 74c3142e849d032aa8238f174421a424
                //https://ipstack.com/quickstart get access_key

                //XmlDocument xdoc = new XmlDocument();
                //xdoc.LoadXml("https://api.ipstack.com/" + GetIPAddress() + "?access_key=74c3142e849d032aa8238f174421a424");
                //var country = xdoc.Descendants("Response").Select(c => new
                //{
                //    IpAddress = c.Element("IP")?.Value,
                //    CountryCode = c.Element("CountryCode")?.Value,
                //    CountryName = c.Element("CountryName")?.Value,
                //    RegionCode = c.Element("RegionCode")?.Value,
                //    RegionName = c.Element("RegionName")?.Value,
                //    City = c.Element("City")?.Value,
                //    ZipCode = c.Element("ZipCode")?.Value,
                //    TimeZone = c.Element("TimeZone")?.Value,
                //    Latitude = c.Element("Latitude")?.Value,
                //    Longitude = c.Element("Longitude")?.Value,
                //    MetroCode = c.Element("MetroCode")?.Value,
                //});

                //var countryData = country.FirstChild();

                ////Check If The Country Is already in database or not
                //var countries = countryRepository
                //    .GetQuery().Any(c => c.CountryCode.Equals(countryData.CountryCode));

                //if (countries)
                //{
                //    //then Update the ViewCount
                //    Country currentCountry = countryRepository.GetQuery()
                //        .First(cc => cc.CountryCode.Equals(countryData.CountryCode));
                //    currentCountry.ViewCount++;
                //    await countryRepository.SaveChanges();
                //}
                //else
                //{
                //    //then add this Country To Database
                //    var newCountry = new Country()
                //    {
                //        CountryCode = countryData.CountryCode,
                //        CountryName = countryData.CountryName,
                //        Latitude = countryData.Latitude,
                //        Longitude = countryData.Longitude,
                //        ViewCount = 1
                //    };
                //    await countryRepository.AddEntity(newCountry);
                //    await countryRepository.SaveChanges();
                //}
                #endregion
            }
        }

        #region freeGeoip
        public async Task<GeoInfo> GetLocation()
        {
            // GetIPAddress()
            var response = await _httpClient.GetAsync("http://api.ipstack.com/" + "31.58.171.206" + "?access_key=74c3142e849d032aa8238f174421a424");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonDeserialize =  JsonConvert.DeserializeObject<GeoInfo>(json);

                var countries = countryRepository.GetQuery().AsQueryable()
                    .Any(c => c.CountryCode.Equals(jsonDeserialize.CountryCode));

                if (countries)
                {
                    //then Update the ViewCount
                    Country currentCountry = countryRepository.GetQuery()
                        .First(cc => cc.CountryCode.Equals(jsonDeserialize.CountryCode));
                    currentCountry.ViewCount++;
                    await countryRepository.SaveChanges();
                }
                else
                {
                    //then add this Country To Database
                    var newCountry = new Country()
                    {
                        CountryCode = jsonDeserialize.CountryCode,
                        CountryName = jsonDeserialize.CountryName,
                        Latitude = jsonDeserialize.Latitude,
                        Longitude = jsonDeserialize.Longitude,
                        City = jsonDeserialize.City,
                        RegionName = jsonDeserialize.RegionName,
                        RegionCode = jsonDeserialize.RegionCode,
                        ViewCount = 1
                    };
                    await countryRepository.AddEntity(newCountry);
                    await countryRepository.SaveChanges();
                }
                return jsonDeserialize;
            }
            return null;
        }
        public class GeoInfo
        {
            [JsonProperty("country_code")]
            public string CountryCode { get; set; }

            [JsonProperty("country_name")]
            public string CountryName { get; set; }

            [JsonProperty("region_code")]
            public string RegionCode { get; set; }

            [JsonProperty("region_name")]
            public string RegionName { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("zip_code")]
            public string ZipCode { get; set; }

            [JsonProperty("latitude")]
            public string Latitude { get; set; }

            [JsonProperty("longitude")]
            public string Longitude { get; set; }
        }
        #endregion

        private static string GetUserOS(string userAgent)
        {
            // get a parser with the embedded regex patterns
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c.OS.Family;
        }

        private string GetIPAddress()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //string ipAddress = httpContext.Features.Get<IServerVariablesFeature>()["HTTP_X_FORWARDED_FOR"];
            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return httpContext.Features.Get<IServerVariablesFeature>()["REMOTE_ADDR"];
        }
        #endregion

        #region get current visitor
        public async Task<CurrentVisitorDTO> GetCurrentVisitor()
        {
            string remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            }
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            var Cv = new CurrentVisitorDTO()
            {
                IpAddress = remoteIpAddress,
                Browser = c.UA.Family,
                OsName = c.OS.Family,
            };
            return Cv;
        }

        #endregion

        #region get SubDetail For Sidebar
        public async Task<SubDetailsDTO> GetSubDetail()
        {
            var stat = await statisticsRepository.GetQuery().AsQueryable()
                .AsNoTracking().ToListAsync();
            var subdetails = new SubDetailsDTO
            {
                Today = stat.Count(d => d.DateStamp.Day == DateTime.Now.Day),
                LastDay = stat.Count(d => d.DateStamp.Day == DateTime.Now.Day - 1),
                ThisMonth = stat.Count(m => m.DateStamp.Month == DateTime.Now.Month),
                ThisYear = stat.Count(y => y.DateStamp.Year == DateTime.Now.Year),
                PeakDate = stat.GroupBy(x => x.DateStamp.ToShamsiDate()).OrderByDescending(grouping => grouping.Count()).First().Key.ToString(),
                LowDate = stat.GroupBy(x => x.DateStamp.ToShamsiDate()).OrderByDescending(grouping => grouping.Count()).Last().Key.ToString(),
            };
            return subdetails;
        }
        #endregion


        #region get Statistics for home
        public async Task<StatisticsDTO> GetStatisticsDetails()
        {
            var stat = await statisticsRepository.GetQuery().AsQueryable()
                .ToListAsync();
            StatisticsDTO svm = new StatisticsDTO()
            {
                //Online Users Can't Work on Localhost
                OnlineUsers = (int)_httpContextAccessor.HttpContext.User.Identities.Count(),
                TodayVisits = stat.Count(ss => ss.DateStamp.Day == DateTime.Now.Day),
                TotallVisits = stat.Count,
                UniquVisitors = stat.GroupBy(ta => ta.IpAddress).Select(ta => ta.Key).Count(),
            };
            return svm;
        }
        #endregion

        #region get Browser Detail for Table
        public async Task<List<BrowserTableDTO>> getBrowserTable()
        {
            var tottal = statisticsRepository.GetQuery().Count();

            var btv = await statisticsRepository.GetQuery()
                .GroupBy(ua => new { ua.UserAgent })
                .OrderByDescending(g => g.Count())
                .Select(g => new BrowserTableDTO()
                {
                    BrowserIcon = g.Key.UserAgent,
                    BrowserName = g.Key.UserAgent,
                    BrowserViewCount = g.Count(),
                    TottalVisits = tottal
                }).ToListAsync();

            foreach (var item in btv)
            {
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(item.BrowserName);
                item.BrowserName = c.UA.Family;
                item.BrowserIcon = c.UA.Family;
            }
            return btv;
        }
        #endregion

        #region get OS detail for Table 
        public async Task<List<OsTableDTO>> GetOsTable()
        {
            var tottal = statisticsRepository.GetQuery().Count();
            var osDetails = await statisticsRepository.GetQuery()
                .GroupBy(ua => new { ua.UserOs })
                .OrderByDescending(g => g.Count())
                .Select(g => new OsTableDTO()
                {
                    OsIcon = g.Key.UserOs,
                    OsName = g.Key.UserOs,
                    OsViewCount = g.Count(),
                    TottalVisits = tottal
                })
                .ToListAsync();

            return osDetails;
        }
        #endregion

        #region get Referrer for Home
        public async Task<List<ReferrerDTO>> GetReferrer()
        {
            var statistics = await statisticsRepository.GetQuery()
                .ToListAsync();
            var ur = statistics
                .GroupBy(r => new { r.Referer })
                .OrderByDescending(r => r.Count())
                .Select(r => new ReferrerDTO()
                {
                    ReferrerUrl = r.Key.Referer,
                    ReferrerCount = r.Count()
                }).ToList();

            foreach (var st in statistics)
            {
                st.Referer = GetHostName(st.Referer);
            }
            return ur;
        }
        #endregion

        #region get Page View Details
        public async Task<List<PageViewDTO>> GetPageViewDetails()
        {
            var stPage = await statisticsRepository.GetQuery().ToListAsync();

            var pv = stPage.GroupBy(r => new { r.PageViewed })
                .OrderByDescending(r => r.Count())
                .Select(r => new PageViewDTO()
                {
                    PageUrl = r.Key.PageViewed,
                    PageViewCount = r.Count()
                }).ToList();

            foreach (var statisticse in stPage)
            {
                statisticse.PageViewed = NormalizePageName(statisticse.PageViewed);
            }

            return pv;
        }
        #endregion


        #region add ip to BlockedIp 
        public async Task<bool> AddIpToBlockedIp(string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrWhiteSpace(ipAddress))
            {
                var blockIp = new BlockedIp()
                {
                    IpAddress = ipAddress,
                };
                await blockedIpRepository.AddEntity(blockIp);
                await blockedIpRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region delete ip from BlockedIp
        public async Task<bool> DeleteBlockedIp(long blockedId)
        {
            var blockIp = await blockedIpRepository.GetQuery().AsQueryable()
                .Where(p => p.Id == blockedId && !p.IsDelete).FirstOrDefaultAsync();
            if (blockIp != null)
            {
                blockIp.IsDelete = true;
                await blockedIpRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region get all BlockedIp
        public async Task<List<BlockedIpDTO>> GetAllBlockedIp()
        {
            var blockedips = await blockedIpRepository.GetQuery().AsNoTracking()
            .Where(p => !p.IsDelete)
            .Select(p => new BlockedIpDTO
            {
                Id = p.Id,
                IpAddress = p.IpAddress,
            })
            .ToListAsync();
            return blockedips;
        }
        #endregion

        #region Normalize Page Name
        private string NormalizePageName(string PageName)
        {
            if (PageName == "/")
            {
                return "/Home";
            }
            else
            {
                return PageName.Remove(0, 0);
            }
        }
        #endregion

        #region get Host Name
        private string GetHostName(string url)
        {
            if (url == "")
            {
                url = "Direct";
            }
            if (url != "Direct")
            {
                Uri uri = new Uri(url);
                return uri.Host;
            }
            else
            {
                return url;
            }
        }

        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (statisticsRepository != null)
                await statisticsRepository.DisposeAsync();

            if (blockedIpRepository != null)
                await blockedIpRepository.DisposeAsync();

            if (countryRepository != null)
                await countryRepository.DisposeAsync();
        }
        #endregion

    }
}
