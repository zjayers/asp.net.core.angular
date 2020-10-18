using asp.net.core.angular.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.angular.Persistence {
    public class VegaDbContext : DbContext {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options) { }

        public DbSet<Make> Makes { get; set; }

    }
}