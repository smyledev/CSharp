using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data 
{
    public class IPInfoDbContext : DbContext
    {
        public IPInfoDbContext (DbContextOptions<IPInfoDbContext> options) : base(options) {}
        public DbSet<IPInfo> IPsInfo { get; set; }
    }
}
