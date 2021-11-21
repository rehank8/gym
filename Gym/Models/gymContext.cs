using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gym.Models
{
    public partial class gymContext : DbContext
    {

        public gymContext(DbContextOptions<gymContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Certifications> Certifications { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Event> Event { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-R5S2I97;Database=gym;Trusted_Connection=True;MultipleActiveResultSets=true");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Certifications>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.imagepath)
                .HasColumnName("imagepath")
                    .HasMaxLength(512);

                entity.Property(e => e.imagedescription)
                    .HasColumnName("imagedescription")
                    .HasMaxLength(1024);

            });

            modelBuilder.Entity<Videos>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.videopath)
                .HasColumnName("videopath")
                    .HasMaxLength(512);

                entity.Property(e => e.videodescription)
                   .HasColumnName("videodescription")
                   .HasMaxLength(1024);
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.UserId)
                .HasColumnName("UserId");

                entity.Property(e => e.UserName)
                   .HasColumnName("UserName")
                   .HasMaxLength(100);

                entity.Property(e => e.IPAddress)
                   .HasColumnName("IPAddress")
                   .HasMaxLength(200);

                entity.Property(e => e.Action)
                  .HasColumnName("Action")
                  .HasMaxLength(512);

                entity.Property(e => e.CreatedDate)
                  .HasColumnName("CreatedDate");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.UserId)
                .HasColumnName("UserId");

                entity.Property(e => e.UserName)
                   .HasColumnName("UserName")
                   .HasMaxLength(100);

                entity.Property(e => e.Amount)
                   .HasColumnName("Amount")
                   .HasColumnType("Decimal");

                entity.Property(e => e.CreatedDate)
                  .HasColumnName("CreatedDate");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.EventName)
                .HasColumnName("EventName");

                entity.Property(e => e.Description)
                   .HasColumnName("Description");

                entity.Property(e => e.EventDateTime)
                   .HasColumnName("EventDateTime");

                entity.Property(e => e.EventImagePath)
                  .HasColumnName("EventImagePath");

                entity.Property(e => e.EventImagePath)
                  .HasColumnName("EventImagePath");
            });
        }
    }
}
