using Common.Infrastructure.DataModels;
using System.Data.Entity;

namespace Common.Repository
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<BodyguardDto> Bodyguards { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
    }
}
