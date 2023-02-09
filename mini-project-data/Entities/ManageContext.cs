using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mini_project_data.Entities;

public partial class ManageContext : DbContext
{
    public ManageContext()
    {
    }

    public ManageContext(DbContextOptions<ManageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Weather> Weathers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;user id=root;password=martialemblem152;port=3306;database=manage;ConvertZeroDateTime=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.Phone, "Phone").IsUnique();

            entity.HasIndex(e => e.RoleId, "User_Role_Id_fk");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(4000);
            entity.Property(e => e.Phone).HasMaxLength(10);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("User_Role_Id_fk");
        });

        modelBuilder.Entity<Weather>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Weather");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.Property(e => e.City).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
