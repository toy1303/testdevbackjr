using System;
using System.Collections.Generic;
using APIDevBACK.DtoSalida;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIDevBACK.Modelo;

public partial class TestDevContext : DbContext
{
    private readonly IConfiguration _configuration;
    public TestDevContext()
    {
    }
    public TestDevContext(DbContextOptions<TestDevContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<CcRiacatArea> CcRiacatAreas { get; set; }

    public virtual DbSet<CcUser> CcUsers { get; set; }

    public virtual DbSet<Ccloglogin> Ccloglogins { get; set; }

    public DbSet<DtoRepporte> DtoRepporte { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DBconection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public List<DtoRepporte> ReporteHorasTrabajdas()
    {
        return this.Set<DtoRepporte>()
            .FromSqlRaw("EXEC ReporteHorasTrabajdas")
            .ToList();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CcRiacatArea>(entity =>
        {
            entity.HasKey(e => e.Idarea).HasName("PK__ccRIACat__05D2C2926FC4EE84");

            entity.ToTable("ccRIACat_Areas");

            entity.Property(e => e.Idarea).HasColumnName("IDArea");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CcUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__ccUsers__206A9DF881D10BAA");

            entity.ToTable("ccUsers");

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.FCreate)
                .HasColumnType("datetime")
                .HasColumnName("fCreate");
            entity.Property(e => e.Idarea).HasColumnName("IDArea");
            entity.Property(e => e.LastLoginAttempt).HasColumnType("datetime");
            entity.Property(e => e.TipoUserId).HasColumnName("TipoUser_id");

            entity.HasOne(d => d.IdareaNavigation).WithMany(p => p.CcUsers)
                .HasForeignKey(d => d.Idarea)
                .HasConstraintName("FK_ccUsers_ccRIACat_Areas");
        });

        modelBuilder.Entity<Ccloglogin>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ccloglog__2D21E3B63AFFA715");

            entity.ToTable("ccloglogin");

            entity.Property(e => e.LogId).HasColumnName("Log_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Ccloglogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ccloglogin_ccUsers");
        });
        modelBuilder.Entity<DtoRepporte>(entity =>
        {
            entity.HasNoKey(); // Marca como tipo sin clave
            entity.ToView(null); // Si no está asociado a una vista específica
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
