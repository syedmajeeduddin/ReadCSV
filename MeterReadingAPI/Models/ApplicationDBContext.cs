using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MeterReadingAPI.Models
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) 
            : base(options)
        {

        }
        //public ApplicationDBContext()
        //{ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Does nothing
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();


        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterRead> MeterReads { get; set; }
    }
}
