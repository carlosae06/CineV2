using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BackEnd.Models;

namespace BackEnd.Data
{
    public partial class CineContext : DbContext
    {
        public CineContext()
        {
        }

        public CineContext(DbContextOptions<CineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Butaca> Butacas { get; set; } = null!;
        public virtual DbSet<Categori> Categoris { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Control> Controls { get; set; } = null!;
        public virtual DbSet<Funsion> Funsions { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Sala> Salas { get; set; } = null!;
        public virtual DbSet<Seccion> Seccions { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Vent> Vents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Butaca>(entity =>
            {
                entity.ToTable("Butaca");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.Butacas)
                    .HasForeignKey(d => d.IdSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Butaca__IdSeccio__5CD6CB2B");
            });

            modelBuilder.Entity<Categori>(entity =>
            {
                entity.ToTable("Categori");

                entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF299DA624")
                    .IsUnique();

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.UserName, "UQ__Cliente__C9F2845649B1DD17")
                    .IsUnique();

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CLIENTE')");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idpersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__Idperso__44FF419A");
            });

            modelBuilder.Entity<Control>(entity =>
            {
                entity.ToTable("Control");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.Controls)
                    .HasForeignKey(d => d.IdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Control__IdTicke__6C190EBB");
            });

            modelBuilder.Entity<Funsion>(entity =>
            {
                entity.ToTable("Funsion");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Horario)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.Funsions)
                    .HasForeignKey(d => d.IdPelicula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Funsion__IdPelic__5441852A");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.Funsions)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Funsion__IdSala__534D60F1");
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.ToTable("Pelicula");

                entity.Property(e => e.Clasificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Director)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Duracion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaEstreno).HasColumnType("date");

                entity.Property(e => e.Idioma)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Protagonista)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Sinopsis)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pelicula__IdCate__4CA06362");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("persona");

                entity.HasIndex(e => e.Ci, "UQ__persona__32149A5A97B6DE7D")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ci)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.HasIndex(e => e.Cod, "UQ__Rol__C1FE6AA831EF3581")
                    .IsUnique();

                entity.Property(e => e.Cod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.ToTable("Sala");

                entity.HasIndex(e => e.Cod, "UQ__Sala__C1FE6AA89D1EAEEB")
                    .IsUnique();

                entity.Property(e => e.Cod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.ToTable("Seccion");

                entity.HasIndex(e => e.Cod, "UQ__Seccion__C1FE6AA8CDAE6925")
                    .IsUnique();

                entity.Property(e => e.Cod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.Seccions)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seccion__IdSala__59063A47");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.Cod, "UQ__Ticket__C1FE6AA877DB3856")
                    .IsUnique();

                entity.Property(e => e.Cod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TipoPago)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdButacaNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdButaca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdButaca__68487DD7");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdClient__656C112C");

                entity.HasOne(d => d.IdFunsionNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdFunsion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdFunsio__66603565");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Ticket__IdUsuari__6477ECF3");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdVenta__6754599E");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F28456D91374AB")
                    .IsUnique();

                entity.Property(e => e.Correo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Password)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__idRol__3F466844");

                entity.HasOne(d => d.IdpersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Idpersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__Idperso__403A8C7D");
            });

            modelBuilder.Entity<Vent>(entity =>
            {
                entity.ToTable("Vent");

                entity.HasIndex(e => e.NumDocumento, "UQ__Vent__11150A80CF795D72")
                    .IsUnique();

                entity.HasIndex(e => e.Telefono, "UQ__Vent__4EC504804F04A1CA")
                    .IsUnique();

                entity.Property(e => e.Complemento)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Documento)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
