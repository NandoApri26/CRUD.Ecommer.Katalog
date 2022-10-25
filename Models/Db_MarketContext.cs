using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Floram.Models.Entities;

namespace Floram.Models
{
    public partial class Db_MarketContext : DbContext
    {
        public Db_MarketContext()
        {
        }

        public Db_MarketContext(DbContextOptions<Db_MarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Barang> Barangs { get; set; } = null!;
        public virtual DbSet<ItemTransaksi> ItemTransakses { get; set; } = null!;
        public virtual DbSet<Karyawan> Karyawans { get; set; } = null!;
        public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
        public virtual DbSet<Pembeli> Pembelis { get; set; } = null!;
        public virtual DbSet<Penjual> Penjuals { get; set; } = null!;
        public virtual DbSet<Transaksi> Transakses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost; Port=5432; Database=Db_Market; User Id=postgres; Password=Deasi26#;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barang>(entity =>
            {
                entity.HasIndex(e => e.IdPenjual, "IX_Barangs_IdPenjual");

                entity.HasIndex(e => e.PembeliIdPembeli, "IX_Barangs_PembeliIdPembeli");

                entity.Property(e => e.Kode).HasMaxLength(10);

                entity.Property(e => e.Nama).HasMaxLength(100);

                entity.HasOne(d => d.IdPenjualNavigation)
                    .WithMany(p => p.Barangs)
                    .HasForeignKey(d => d.IdPenjual);

                entity.HasOne(d => d.PembeliIdPembeliNavigation)
                    .WithMany(p => p.Barangs)
                    .HasForeignKey(d => d.PembeliIdPembeli);
            });

            modelBuilder.Entity<ItemTransaksi>(entity =>
            {
                entity.ToTable("ItemTransaksis");

                entity.HasIndex(e => e.IdBarang, "IX_ItemTransaksis_IdBarang");

                entity.HasIndex(e => e.IdTransaksi, "IX_ItemTransaksis_IdTransaksi");

                entity.HasOne(d => d.IdBarangNavigation)
                    .WithMany(p => p.ItemTransaksis)
                    .HasForeignKey(d => d.IdBarang);

                entity.HasOne(d => d.IdTransaksiNavigation)
                    .WithMany(p => p.ItemTransaksis)
                    .HasForeignKey(d => d.IdTransaksi);
            });

            modelBuilder.Entity<Karyawan>(entity =>
            {
                entity.HasKey(e => e.IdKaryawan);

                entity.Property(e => e.NamaKaryawan).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.Posisi).HasMaxLength(25);

                entity.Property(e => e.Username).HasMaxLength(30);
            });

            modelBuilder.Entity<Keranjang>(entity =>
            {
                entity.HasIndex(e => e.IdBarang, "IX_Keranjangs_IdBarang");

                entity.HasIndex(e => e.IdUser, "IX_Keranjangs_IdUser");

                entity.HasOne(d => d.IdBarangNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdBarang);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdUser);
            });

            modelBuilder.Entity<Pembeli>(entity =>
            {
                entity.HasKey(e => e.IdPembeli);

                entity.HasIndex(e => e.IdUser, "IX_Pembelis_IdUser");

                entity.Property(e => e.Alamat).HasMaxLength(100);

                entity.Property(e => e.Nama).HasMaxLength(25);

                entity.Property(e => e.Negara).HasMaxLength(25);

                entity.Property(e => e.NoHp).HasMaxLength(12);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Pembelis)
                    .HasForeignKey(d => d.IdUser);
            });

            modelBuilder.Entity<Penjual>(entity =>
            {
                entity.HasIndex(e => e.IdUser, "IX_Penjuals_IdUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Penjuals)
                    .HasForeignKey(d => d.IdUser);
            });

            modelBuilder.Entity<Transaksi>(entity =>
            {
                entity.ToTable("Transaksis");

                entity.HasIndex(e => e.IdUser, "IX_Transaksis_IdUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Transaksis)
                    .HasForeignKey(d => d.IdUser);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
