using System.Data.Entity;
using Teleperformance.Model;

namespace Teleperformance.Data
{
    public class TeleContext : DbContext
    {
        public TeleContext() : base("default")
        {
        }

        public DbSet<Inbound> Inbounds { get; set; }
        public DbSet<Outbound> Outbounds { get; set; }
    }
}
