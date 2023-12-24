using Beneficiary.Service.Entities;
using Microsoft.EntityFrameworkCore;


namespace Beneficiary.Service.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base (options)
        {
            
        }

        public DbSet<Insured> Insuranceds { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceInsured> InsuredInsurances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsuranceInsured>()
                .HasKey(ii => new { ii.InsuredId, ii.InsuranceId });

            modelBuilder.Entity<InsuranceInsured>()
                .HasOne(ii => ii.Insured)
                .WithMany(i => i.InsuredInsurances)
                .HasForeignKey(ii => ii.InsuredId);

            modelBuilder.Entity<InsuranceInsured>()
                .HasOne(ii => ii.Insurance)
                .WithMany(i => i.InsuredInsurances)
                .HasForeignKey(ii => ii.InsuranceId);
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
