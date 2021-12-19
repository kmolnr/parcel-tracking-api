using Microsoft.EntityFrameworkCore;
using ParcelTracking.EFCore.Models;

namespace ParcelTracking.EFCore
{
    public class ParcelTrackingDbContext : DbContext
    {
        public ParcelTrackingDbContext(DbContextOptions<ParcelTrackingDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Parcel> Parcel { get; set; }

        public DbSet<ParcelItem> ParcelItem { get; set; }

        public DbSet<ParcelStatus> ParcelStatus { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<ParcelParcelStatus> ParcelParcelStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Parcels)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(u => u.UserId);

            modelBuilder.Entity<Parcel>()
                .Property(p => p.Code)
                .HasMaxLength(8);

            modelBuilder.Entity<Parcel>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<Parcel>()
                .HasMany(p => p.ParcelItems)
                .WithOne(i => i.Parcel)
                .HasForeignKey(i => i.ParcelId)
                .HasPrincipalKey(p => p.ParcelId);

            modelBuilder.Entity<ParcelItem>()
                .HasOne(i => i.Product)
                .WithMany(p => p.ParcelItems)
                .HasForeignKey(i => i.ProductId)
                .HasPrincipalKey(p => p.ProductId);

            modelBuilder.Entity<ParcelStatus>()
                .HasIndex(s => s.Code)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<ParcelParcelStatus>()
                .HasKey(pps => new { pps.ParcelId, pps.ParcelStatusId });

            modelBuilder.Entity<ParcelParcelStatus>()
                .HasOne(pps => pps.ParcelStatus)
                .WithMany(ps => ps.ParcelParcelStatus)
                .HasForeignKey(pps => pps.ParcelStatusId);

            modelBuilder.Entity<ParcelParcelStatus>()
                .HasOne(pps => pps.Parcel)
                .WithMany(p => p.ParcelParcelStatus)
                .HasForeignKey(pps => pps.ParcelId);

            this.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            // SHA-2 password encryption recommended.

            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserId = 1, Name = "user1", Password = "user1", Role = "user" },
                    new User { UserId = 2, Name = "user2", Password = "user2", Role = "user" }
                );

            modelBuilder.Entity<Parcel>()
                .HasData(
                    new Parcel { ParcelId = 1, Code = "QWER0001", UserId = 1 },
                    new Parcel { ParcelId = 2, Code = "QWER0002", UserId = 1 },
                    new Parcel { ParcelId = 3, Code = "QWER0003", UserId = 2 },
                    new Parcel { ParcelId = 4, Code = "QWER0004", UserId = 2 }
                );

            modelBuilder.Entity<ParcelItem>()
                .HasData(
                    new ParcelItem { ParcelItemId = 1, ParcelId = 1, ProductId = 1 },
                    new ParcelItem { ParcelItemId = 2, ParcelId = 2, ProductId = 2 },
                    new ParcelItem { ParcelItemId = 3, ParcelId = 3, ProductId = 3 },
                    new ParcelItem { ParcelItemId = 4, ParcelId = 4, ProductId = 4 },
                    new ParcelItem { ParcelItemId = 5, ParcelId = 4, ProductId = 4 }
                );

            modelBuilder.Entity<ParcelStatus>()
                .HasData(
                    new ParcelStatus { ParcelStatusId = 1, Code = "WfPU", Description = "Waiting for Pick Up", HungarianDescription = "Csomag a feladónál. Futárra vár." },
                    new ParcelStatus { ParcelStatusId = 2, Code = "PU", Description = "Picked Up", HungarianDescription = "Csomag a futárnál. Depóba tart." },
                    new ParcelStatus { ParcelStatusId = 3, Code = "ID", Description = "In Deposit", HungarianDescription = "Depóban van. Kiszállításra vár." },
                    new ParcelStatus { ParcelStatusId = 4, Code = "OD", Description = "On Delivery", HungarianDescription = "Kiszállítás alatt áll. Célba tart." },
                    new ParcelStatus { ParcelStatusId = 5, Code = "DD", Description = "Delivered", HungarianDescription = "Kiszállítva." }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { ProductId = 1, Name = "Fender Stratocaster" },
                    new Product { ProductId = 2, Name = "Gibson Les Paul" },
                    new Product { ProductId = 3, Name = "Fender Telecaster" },
                    new Product { ProductId = 4, Name = "Gibson SG Standard" }
                );

            modelBuilder
                .Entity<ParcelParcelStatus>()
                .HasData(
                    new { ParcelId = 1, ParcelStatusId = 1 },
                    new { ParcelId = 2, ParcelStatusId = 2 },
                    new { ParcelId = 3, ParcelStatusId = 3 },
                    new { ParcelId = 4, ParcelStatusId = 4 },
                    new { ParcelId = 1, ParcelStatusId = 5 },
                    new { ParcelId = 2, ParcelStatusId = 1 },
                    new { ParcelId = 3, ParcelStatusId = 2 },
                    new { ParcelId = 4, ParcelStatusId = 3 }
                );
        }
    }
}
