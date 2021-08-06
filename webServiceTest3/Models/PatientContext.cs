using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webServiceTest3.Models
{
    public class PatientContext : DbContext
    {
        //public PatientContext(DbContextOptions<PatientContext> options) 
        //    : base(options)
        //{

        //}

        public DbSet<Patient> PatientData { get; set; }
        public DbSet<Log> LogData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Patient>(p =>
            //{
            //    p.HasKey(pat => pat.Id);

            //});
            //
            modelBuilder.Entity<Log>(p =>
            {
                p.HasKey(pat => pat.Id);
            });
        }

        public PatientContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID =admin1;Password=admin1;Server=localhost;Port=5432;Database=patients;");
        }
    }
}
