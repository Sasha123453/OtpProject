using Microsoft.EntityFrameworkCore;

namespace OtpProject.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<OtpModel> OtpModels { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }  
    }
}
