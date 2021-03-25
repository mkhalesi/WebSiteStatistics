using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebSiteStatistics.Application.Services.Interfaces;
using WebSiteStatistics.Web.Models;

namespace WebSiteStatistics.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;
        public HomeController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }
        public async Task<IActionResult> Index()
        {
            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            await statisticsService.AddUserToStatistic(remoteIpAddress);
            return View();
        }
    }
}
