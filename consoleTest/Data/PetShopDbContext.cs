using consoleTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetShop.Models;
using System;
using System.Collections.Generic;

namespace PetShopConsole.Data;

public partial class PetShopDbContext : DbContext
{
    private DbSet<Booking> bookings;

    public PetShopDbContext()
    {
    }

    public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
        : base(options)
    {
    }
    public List<Invoice> Invoices { get; set; } = new();

    public virtual DbSet<Booking> Bookings { get => bookings; set => bookings = value; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server =localhost; database = PetShopDB;uid=sa;pwd=123;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACDAD3F337F");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingTime).HasColumnType("datetime");
            entity.Property(e => e.PetId).HasColumnName("PetID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Pet).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__Bookings__PetID__4CA06362");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Servic__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserID__37A5467C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFAB584A38");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserID__2F10007B");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CDA194D40");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__31EC6D26");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__32E0915F");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.PetId).HasName("PK__Pets__48E53802F0F60A1F");

            entity.Property(e => e.PetId).HasColumnName("PetID");
            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Species).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Pets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pets__UserID__4BAC3F29");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDA8AFF526");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA76B0E17A");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE21E28B5");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534BB43D40D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
