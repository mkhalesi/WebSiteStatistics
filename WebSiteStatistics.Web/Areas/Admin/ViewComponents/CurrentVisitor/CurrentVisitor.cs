using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteStatistics.Application.Services.Interfaces;

namespace WebSiteStatistics.Web.Areas.Admin.ViewComponents.CurrentVisitor
{
    public class CurrentVisitor : ViewComponent
    {
        private readonly IStatisticsService statisticsService;
        public CurrentVisitor(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.currentVisitor = await statisticsService.GetCurrentVisitor();
            return View(viewName: "CurrentVisitor");
        }
    }
}
