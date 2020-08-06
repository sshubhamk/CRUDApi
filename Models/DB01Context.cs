using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDApi.Models
{
    public partial class DB01Context : DbContext
    {
        public DB01Context()
        {
        }

        public DB01Context(DbContextOptions<DB01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SHUBHAM\\MSSQLSERVER01;Database=DB01;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.email)
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.mobilenumber).HasColumnName("mobilenumber");

                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .IsUnicode(false);

                entity.Property(e => e.roletype)
                    .HasColumnName("roletype")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
