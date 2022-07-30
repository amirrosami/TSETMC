using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Domain.Entities.App;

namespace Tsetmc.Persistence.Contexts
{
    public partial class TSETMCContext : DbContext,Iunitofwork
    {
        public TSETMCContext()
        {
        }

        public TSETMCContext(DbContextOptions<TSETMCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GetHoldersUpdateList> GetHoldersUpdateLists { get; set; } = null!;
        public virtual DbSet<Market> Markets { get; set; } = null!;
        public virtual DbSet<MarketHistory> MarketHistories { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Sector> Sectors { get; set; } = null!;
        public virtual DbSet<ShareHolder> ShareHolders { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StocksHistory> StocksHistories { get; set; } = null!;
        public virtual DbSet<Stockswillupdate> Stockswillupdates { get; set; } = null!;

        public int savechanges()
        {
            return base.SaveChanges();
        }

        public Task<int> savechangesAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken); 
        }

        public int TruncateTable()
        {
            return base.Database.ExecuteSqlRaw("truncate Table Stock_History");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TSETMC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1256_CI_AS");

            modelBuilder.Entity<GetHoldersUpdateList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetHoldersUpdateList");
                entity.Property(e => e.HistDate).HasMaxLength(8);

                entity.Property(e => e.StockInternalId).HasMaxLength(50);
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.MarketName).HasMaxLength(100);

                entity.Property(e => e.MarketNo).HasMaxLength(20);
            });

            modelBuilder.Entity<MarketHistory>(entity =>
            {
                entity.ToTable("MarketHistory");

                entity.Property(e => e.HistDate).HasMaxLength(8);

                entity.Property(e => e.StatusId).HasMaxLength(50);

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.MarketHistories)
                    .HasForeignKey(d => d.MarketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MarketHis__Marke__36B12243");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonCode).HasMaxLength(450);

                entity.Property(e => e.PersonName).HasMaxLength(100);

                entity.Property(e => e.PersonType).HasMaxLength(50);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.Property(e => e.SectorName).HasMaxLength(100);

                entity.Property(e => e.SectorNo).HasMaxLength(50);
            });

            modelBuilder.Entity<ShareHolder>(entity =>
            {
                entity.Property(e => e.HistDate).HasMaxLength(8);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ShareHolders)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__ShareHold__Perso__30F848ED");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasIndex(e => e.StockInternalId, "UQ__Stocks__182CA84D303FE3FE")
                    .IsUnique();

                entity.HasIndex(e => e.StockNo, "UQ__Stocks__2C8517D03579A8A0")
                    .IsUnique();

                entity.HasIndex(e => e.StockName, "UQ__Stocks__3156311908601D14")
                    .IsUnique();

                entity.HasIndex(e => e.StockSymbol, "UQ__Stocks__E2FE09934CB87F72")
                    .IsUnique();

                entity.Property(e => e.StockBvol).HasColumnName("StockBVol");

                entity.Property(e => e.StockEps).HasColumnName("StockEPS");

                entity.Property(e => e.StockInternalId).HasMaxLength(50);

                entity.Property(e => e.StockName).HasMaxLength(200);

                entity.Property(e => e.StockSymbol).HasMaxLength(50);

                entity.HasOne(d => d.StockSector)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StockSectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stocks__StockSec__2E1BDC42");
            });

            modelBuilder.Entity<StocksHistory>(entity =>
            {
                entity.ToTable("Stocks_History");

                entity.Property(e => e.BuyCountI).HasColumnName("Buy_CountI");

                entity.Property(e => e.BuyCountN).HasColumnName("Buy_CountN");

                entity.Property(e => e.BuyIvolume).HasColumnName("Buy_IVolume");

                entity.Property(e => e.BuyNvolume).HasColumnName("Buy_NVolume");

                entity.Property(e => e.HistDate).HasMaxLength(8);

                entity.Property(e => e.SellCountI).HasColumnName("Sell_CountI");

                entity.Property(e => e.SellCountN).HasColumnName("Sell_CountN");

                entity.Property(e => e.SellIvolume).HasColumnName("Sell_IVolume");

                entity.Property(e => e.SellNvolume).HasColumnName("Sell_NVolume");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StocksHistories)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stocks_Hi__Stock__33D4B598");
            });

            modelBuilder.Entity<Stockswillupdate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("stockswillupdate");

                entity.Property(e => e.HistDate).HasMaxLength(8);

                entity.Property(e => e.I)
                    .HasMaxLength(50)
                    .HasColumnName("i");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
