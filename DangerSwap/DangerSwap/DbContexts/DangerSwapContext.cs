using DangerSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.DbContexts
{
    public class DangerSwapContext : DbContext
    {
        public DangerSwapContext(DbContextOptions<DangerSwapContext> options) 
            : base(options)
        { }
        public DbSet<User> Users { get; set; } = null!;
    }
}
