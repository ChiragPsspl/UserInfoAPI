using Microsoft.EntityFrameworkCore;

namespace UserInformation.Model
{
    public class CRUDLogContext : DbContext
    {
        public CRUDLogContext(DbContextOptions<CRUDLogContext> options) : base(options) 
        {
            
        }

        public DbSet<LogInfo> LogInfo { get; set; }
    }
}
