using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaCreativa.Models;

public partial class BdprestamosContext : DbContext
{
    public BdprestamosContext()
    {
    }

    public BdprestamosContext(DbContextOptions<BdprestamosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.NumeroSerie);

            entity.ToTable("Equipo");

            entity.Property(e => e.NumeroSerie)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("numSerie");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nMarca");
            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nomEquipo");

            entity.HasOne(d => d.Nombre_de_la_Marca).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.NombreMarca)
                .HasConstraintName("FK_Marca");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.NombreMarca);

            entity.ToTable("Marca");

            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomMarca");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Exactitud).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TipoH)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Persona).HasName("PK__Prestamo__BDC5F71CCF3BBCCF");

            entity.Property(e => e.Persona)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nomEquipo");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomMarca");

            entity.HasOne(d => d.Nombre_de_la_Marca).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.NombreMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prestamos__nomMa__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
