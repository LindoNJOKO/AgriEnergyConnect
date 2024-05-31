using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Models;

public partial class AgriEnergyConnectDbContext : DbContext
{
    public AgriEnergyConnectDbContext()
    {
    }

    public AgriEnergyConnectDbContext(DbContextOptions<AgriEnergyConnectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-II9F0GR5\\MSSQLSERVER01;Initial Catalog=AgriEnergyConnectDb; Encrypt=False; Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF12A412A92");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.City)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.EmployeePassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PK__Farmer__731B88E8639621F4");

            entity.ToTable("Farmer");

            entity.Property(e => e.FarmerId).HasColumnName("FarmerID");
            entity.Property(e => e.City)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.FarmerPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Login__1788CCACAACE2C4B");

            entity.ToTable("Login");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA1260388932EA93");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.FarmerId).HasColumnName("FarmerID");
            entity.Property(e => e.PostDes)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Farmer).WithMany(p => p.Posts)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK__Posts__FarmerID__3F466844");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED933079BB");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Catergry)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.FarmerId).HasColumnName("FarmerID");
            entity.Property(e => e.PoductName)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.ProductDes)
                .HasMaxLength(225)
                .IsUnicode(false);

            entity.HasOne(d => d.Farmer).WithMany(p => p.Products)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK__Products__Farmer__4222D4EF");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Register__1788CCACCE54BE73");

            entity.ToTable("Register");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.City)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(225)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
