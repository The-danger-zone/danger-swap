using DangerSwap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.DbContexts
{
    public class DangerSwapContext : IdentityDbContext<User>
    {
        public DangerSwapContext(DbContextOptions<DangerSwapContext> options)
            : base(options)
        { }
        public override DbSet<User> Users { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<Rate> Rate { get; set; } = null!;
        public DbSet<TransactionCurrency> TransactionCurrencies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            builder.Entity<User>().HasData(new User
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = false,
                Password = "Admin123#",
                PasswordHash = "AQAAAAEAACcQAAAAEEuLD1dFigkTjmZRnQbEut6oXO0tE8rYl2tavqD8oqZceKgJxp35 + mGldf80qWJrHA ==",
                SecurityStamp = string.Empty
            });

        }
    }
}
