using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.DTOs.Stattistics;
using WebSiteStatistics.DataLayer.Entities.Statistics;

namespace WebSiteStatistics.Application.Services.Interfaces
{
    public interface IStatisticsService : IAsyncDisposable
    {
        #region Add User to Statistic Entity
        Task AddUserToStatistic(string ip);
        #endregion

        #region get Current Visitor
        Task<CurrentVisitorDTO> GetCurrentVisitor();
        #endregion

        #region get SubDetail For Sidebar
        Task<SubDetailsDTO> GetSubDetail();
        #endregion


        #region get Statistics for home
        Task<StatisticsDTO> GetStatisticsDetails();
        #endregion

        #region get Browser Detail for Table
        Task<List<BrowserTableDTO>> getBrowserTable();
        #endregion

        #region  get Data for Request user Browser 
        Task<RequestBrowserData[]> GetDataForRequestUserBrowser();
        #endregion

        #region get OS detail for Table 
        Task<List<OsTableDTO>> GetOsTable();
        #endregion

        #region  get Data for Request user Os 
        Task<RequestOSData[]> GetDataForRequestUserOS();
        #endregion

        #region get Referrer for Home
        Task<List<ReferrerDTO>> GetReferrer();
        #endregion

        #region get Page View Details
        Task<List<PageViewDTO>> GetPageViewDetails();
        #endregion

        #region add ip to BlockedIp 
        Task<bool> AddIpToBlockedIp(string ipAddress);
        #endregion

        #region delete ip from BlockedIp
        Task<bool> DeleteBlockedIp(long blockedId);
        #endregion

        #region get all BlockedIp
        Task<List<BlockedIpDTO>> GetAllBlockedIp();
        #endregion

        // Countries
        #region get detail for Country Table
        Task<List<Country>> GetCountryTable();
        #endregion

        #region get detail for City Table
        Task<List<Country>> GetCityTable();
        #endregion

  

    }
}
