using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WikiPex.Models
{
    public partial class sistem21_wikiapexContext : DbContext
    {
        public sistem21_wikiapexContext()
        {
        }

        public sistem21_wikiapexContext(DbContextOptions<sistem21_wikiapexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Habilidades> Habilidades { get; set; } = null!;
        public virtual DbSet<Personajes> Personajes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Habilidades>(entity =>
            {
                entity.HasKey(e => e.IdHabilidades)
                    .HasName("PRIMARY");

                entity.ToTable("habilidades");

                entity.Property(e => e.IdHabilidades).HasColumnType("int(11)");

                entity.Property(e => e.DescripcionDefinitiva).HasColumnType("mediumtext");

                entity.Property(e => e.DescripcionPasiva).HasColumnType("mediumtext");

                entity.Property(e => e.DescripcionTactica).HasColumnType("mediumtext");

                entity.Property(e => e.NombreDefinitiva).HasMaxLength(99);

                entity.Property(e => e.NombrePasiva).HasMaxLength(99);

                entity.Property(e => e.NombreTactica).HasMaxLength(99);
            });

            modelBuilder.Entity<Personajes>(entity =>
            {
                entity.ToTable("personajes");

                entity.HasIndex(e => e.IdHabilidad, "fkIdHabilidad_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Edad)
                    .HasMaxLength(45)
                    .HasColumnName("edad")
                    .HasDefaultValueSql("'Desconocido'");

                entity.Property(e => e.Frase)
                    .HasColumnType("mediumtext")
                    .HasColumnName("frase");

                entity.Property(e => e.Historia).HasColumnName("historia");

                entity.Property(e => e.IdHabilidad).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .HasDefaultValueSql("'Desconocido'");

                entity.Property(e => e.Nombrereal)
                    .HasMaxLength(45)
                    .HasColumnName("nombrereal")
                    .HasDefaultValueSql("'Desconocido'");

                entity.Property(e => e.Planetanatal)
                    .HasMaxLength(45)
                    .HasColumnName("planetanatal")
                    .HasDefaultValueSql("'Desconocido'");

                entity.Property(e => e.Seudonimo)
                    .HasMaxLength(45)
                    .HasColumnName("seudonimo");

                entity.Property(e => e.Url)
                    .HasMaxLength(45)
                    .HasColumnName("url");

                entity.HasOne(d => d.IdHabilidadNavigation)
                    .WithMany(p => p.Personajes)
                    .HasForeignKey(d => d.IdHabilidad)
                    .HasConstraintName("fkIdHabilidad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
