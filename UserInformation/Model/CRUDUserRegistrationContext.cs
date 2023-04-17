using Microsoft.EntityFrameworkCore;

namespace UserInformation.Model
{
    public class CRUDUserRegistrationContext : DbContext
    {
        public CRUDUserRegistrationContext(DbContextOptions<CRUDUserRegistrationContext> options) : base(options) 
        {

        }

        public DbSet<UserRegistration> userregistration { get; set; }
        public DbSet<UserRegistrationDTO> UserRegistrationDTO { get; set; }
    }
}
