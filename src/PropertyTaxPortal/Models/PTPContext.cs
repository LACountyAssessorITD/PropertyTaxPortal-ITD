using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class PTPContext : DbContext
    {
        public PTPContext(DbContextOptions<PTPContext> options) : base(options)
        { }

        public DbSet<FAQ> faq { get; set; }

        public DbSet<NEWS> News { get; set; }

        public DbSet<Category> category { get; set; }

        public DbSet<RefCode> refcode { get; set; }
  
        public DbSet<EmailTrackingCount> emailTrackingCount { get; set; }

        public DbSet<Subjects> subjects { get; set; }

        public DbSet<States> states { get; set; }

        public DbSet<ImageModel> imageModel { get; set; }
    }
}
