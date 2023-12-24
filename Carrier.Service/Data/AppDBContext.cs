using Carrier.Service.Entities;
using Microsoft.EntityFrameworkCore;


namespace Carrier.Service.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base (options)
        {
            
        }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    }
}
