using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.DTOs.Stattistics;

namespace WebSiteStatistics.Application.Services.Interfaces
{
    public interface IStatisticsService : IAsyncDisposable
    {
        #region Add User to Statistic Entity
        void AddUserToStatistic(string ip);
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

        #region get OS detail for Table 
        Task<List<OsTableDTO>> GetOsTable();
        #endregion

        #region get Referrer for Home
        Task<List<ReferrerDTO>> GetReferrer();
        #endregion

        #region get Page View Details
        Task<List<PageViewDTO>> GetPageViewDetails();
        #endregion

        #region add ip to BlockedIp 
        Task AddIpToBlockedIp(string ipAddress);
        #endregion

        #region delete ip from BlockedIp
        Task DeleteBlockedIp(long blockedId);
        #endregion

        #region get all BlockedIp
        Task<List<BlockedIpDTO>> GetAllBlockedIp();
        #endregion

    }
}
