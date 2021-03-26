using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteStatistics.Application.Services.Interfaces;
using WebSiteStatistics.DataLayer.DTOs.Stattistics;

namespace WebSiteStatistics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        #region constructor
        private readonly IStatisticsService statisticsService;
        public DashBoardController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        #endregion

        #region index - Home page
        public async Task<IActionResult> Index()
        {
            var statisticsForHome = await statisticsService.GetStatisticsDetails();
            var browserTable = await statisticsService.getBrowserTable();
            var osTable = await statisticsService.GetOsTable();
            var referrer = await statisticsService.GetReferrer();
            var pageView = await statisticsService.GetPageViewDetails();

            var homeDetails = new HomePageDTO
            {
                Statistics = statisticsForHome,
                BrowserTables = browserTable,
                osTables = osTable,
                pageViews = pageView,
                referrers = referrer,
            };
            return View(homeDetails);
        }
        #endregion

        #region blocked Ip Index
        [HttpGet]
        public async Task<IActionResult> BlockedIps()
        {
            var blockedIps = await statisticsService.GetAllBlockedIp();
            return View(blockedIps);
        }
        #endregion

        #region add ip to Blocked Ip
        [HttpPost]
        public async Task<JsonResult> AddIpToBlockedIp(string IpAddress)
        {
            var result = await statisticsService.AddIpToBlockedIp(IpAddress);
            if (result == false) return null;
            return Json(result);
        }
        #endregion

        #region delete blocked ip
        [HttpPost]
        public async Task<JsonResult> DeleteBlockedIp(long Id)
        {
            var result = await statisticsService.DeleteBlockedIp(Id);
            if (result == false) return null;
            return Json(result);
        }
        #endregion

        #region Country Table
        public async Task<IActionResult> CountryTable()
        {
            var details = await statisticsService.GetCountryTable();
            return View(details);
        }
        #endregion

        #region City Table
        public async Task<IActionResult> CityTable()
        {
            var details = await statisticsService.GetCityTable();
            return View(details);
        }
        #endregion

        #region Chart
        public IActionResult Chart()
        {
            return View();
        }
        #endregion

        #region Request For user Os Data 
        [HttpGet]
        public async Task<JsonResult> RequestUserOsData()
        {
            var results = await statisticsService.GetDataForRequestUserOS();
            return Json(results);
        }
        #endregion

        #region Request For user Browser Data 
        [HttpGet]
        public async Task<JsonResult> RequestUserBrowserData()
        {
            var results = await statisticsService.GetDataForRequestUserBrowser();
            return Json(results);
        }
        #endregion

        #region Request For Country visit Data
        [HttpGet]
        public async Task<JsonResult> RequestVisitorsCountryData()
        {
            var results = await statisticsService.GetDataForRequestCountry();
            return Json(results);
        }
        #endregion
    }
}
