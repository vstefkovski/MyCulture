using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCulture.Domain.DomainModels;
using MyCulture.Domain.Identity;
using System;

namespace MyCulture.Repository
{
    public class ApplicationDbContext:IdentityDbContext<MyCultureApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public virtual DbSet<Culture> Cultures { get; set; }
        public virtual  DbSet<CultureCart> CultureCarts { get; set; }
        public virtual  DbSet<CultureInCultureCart> CultureInCultureCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Culture>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<CultureCart>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<CultureInCultureCart>()
                .HasKey(r => new { r.CultureId, r.CultureCartId });
            builder.Entity<CultureInCultureCart>()
               .HasOne(r => r.Culture)
               .WithMany(r => r.CultureInCultureCarts)
               .HasForeignKey(r => r.CultureCartId);
            builder.Entity<CultureInCultureCart>()
               .HasOne(r => r.CultureCart)
               .WithMany(r => r.CultureInCultureCarts)
               .HasForeignKey(r => r.CultureId);
            builder.Entity<CultureCart>()
               .HasOne<MyCultureApplicationUser>(r => r.Owner)
               .WithOne(r => r.UserCart)
               .HasForeignKey<CultureCart>(r => r.OwnerId);
            builder.Entity<CultureInOrder>()
               .HasKey(r => new { r.CultureId, r.OrderId });
            builder.Entity<CultureInOrder>()
              .HasOne(r => r.SelectedCulture)
              .WithMany(r => r.Orders)
              .HasForeignKey(r => r.CultureId);
            builder.Entity<CultureInOrder>()
                .HasOne(r => r.UserOrder)
                .WithMany(r => r.Cultures)
                .HasForeignKey(r => r.OrderId);

        }
    }
}
