using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteStatistics.Application.Services.Interfaces;

namespace WebSiteStatistics.Web.Areas.Admin.ViewComponents.SubDetails
{
    public class SubDetails : ViewComponent
    {
        private readonly IStatisticsService statisticsService;
        public SubDetails(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.subDetails = await statisticsService.GetSubDetail();
            return View(viewName: "SubDetails");
        }
    }
}
