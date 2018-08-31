using Common.Infrastructure.DataModels;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace Common.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base(
            new SQLiteConnection() { ConnectionString = @"Data Source=.\VipSkyDB.db" },
            true)
        { }
        
        public DbSet<BodyguardDto> Bodyguards { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
    }
}
