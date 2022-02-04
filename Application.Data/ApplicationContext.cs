using Application.Data.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<ApplicantDTO> Applicant { get; set; }
        public DbSet<RequestedCreditDTO> RequestedCredit { get; set; }
        public DbSet<ApplicationDTO> Application { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> context):base (context)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicantDTO>().ToTable("Applicant").Property(f=>f.Id).ValueGeneratedOnAdd();
            builder.Entity<RequestedCreditDTO>().ToTable("RequestedCredit").Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Entity<ApplicationDTO>().ToTable("Application").Property(f => f.Id).ValueGeneratedOnAdd();

        }
    }
}