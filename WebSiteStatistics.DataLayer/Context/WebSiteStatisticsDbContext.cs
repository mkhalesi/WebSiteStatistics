
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteStatistics.DataLayer.Entities.Statistics;

namespace WebSiteStatistics.DataLayer.Context
{
    public class WebSiteStatisticsDbContext : DbContext
    {
        public WebSiteStatisticsDbContext(DbContextOptions<WebSiteStatisticsDbContext> options) : base(options){}

        #region dbset
        public DbSet<Country> Country { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<BlockedIp> BlockedIp { get; set; }
        #endregion

        #region on model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }
}
