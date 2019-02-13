using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class PTPContext: DbContext
    {
        public PTPContext(DbContextOptions<PTPContext> options):base(options)
        { }

        public DbSet<FAQ> faq { get; set; }
    }
}
