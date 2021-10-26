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
        public virtual DbSet<UserModel> UserModel { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        //{
        //   if (!optionsBuilder.IsConfigured)
        //   {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-R5S2I97;Database=gym;Trusted_Connection=True;");
        //    }
        //}

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
                // entity.HasKey(e => e.Id).HasName("Id");
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
                //entity.HasKey(e => e.Id).HasName("Id");
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.videopath)
                .HasColumnName("videopath")
                    .HasMaxLength(512);

                entity.Property(e => e.videodescription)
                   .HasColumnName("videodescription")
                   .HasMaxLength(1024);
            });
        }
    }
}
