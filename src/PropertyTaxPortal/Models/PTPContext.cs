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

        public PTPContext()
        {
        }

        public DbSet<FAQ> faq { get; set; }

        public DbSet<Category> category { get; set; }

        public DbSet<RefCode> refcode { get; set; }

        public DbSet<EmailTrackingCount> emailTrackingCount { get; set; }

        public DbSet<Subjects> subjects { get; set; }

        public DbSet<States> states { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<News> News { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__ptp_connect"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");
            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("newsID");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .IsUnicode(false);

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .IsUnicode(false);

                entity.Property(e => e.Caption)
                    .HasColumnName("caption")
                    .IsUnicode(false);

                entity.Property(e => e.EndOn)
                    .IsRequired()
                    .HasColumnName("endOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.NewsDate)
                    .IsRequired()
                    .HasColumnName("newsDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SOrder).HasColumnName("sOrder");

                entity.Property(e => e.StartOn)
                    .IsRequired()
                    .HasColumnName("startOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsUnicode(false);

                entity.Property(e => e.UploadId).HasColumnName("uploadId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                            .HasMaxLength(15)
                            .IsUnicode(false);
                entity.Property(e => e.Active).HasColumnName("active");
            });
        }
    }
}