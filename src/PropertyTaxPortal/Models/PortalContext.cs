using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        { }

        public PortalContext()
        {
        }

        public DbSet<Address> AddressList { get; set; }
        public DbSet<FactoredBaseValue> FactoredBaseValue { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__ptp_connect"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
             
        }
    }
}