using DangerSwap.Models;
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
    }
}
