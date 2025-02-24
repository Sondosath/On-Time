using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sunday_Model_in_MVC_.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<User1> User1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BJH6DOD;Database=Orange;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3213E83F083621A1");

            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Courses)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("courses");
            entity.Property(e => e.Instructer)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instructer");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User1__3214EC0789EF8E65");

            entity.ToTable("User1");

            entity.HasIndex(e => e.Email, "UQ__User1__A9D10534669BD832").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
