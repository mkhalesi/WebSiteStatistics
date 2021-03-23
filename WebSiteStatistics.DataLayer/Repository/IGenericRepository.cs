using System;
using System.Linq;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.Entities.Common;

namespace WebSiteStatistics.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEntityById(long entityId);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntity(long entityId);
        Task SaveChanges();
    }
}