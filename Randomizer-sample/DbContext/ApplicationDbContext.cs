using Microsoft.EntityFrameworkCore;
using OTM_sample.Model;

namespace OTM_sample.DbContext;
public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Quote> Quotes { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Quote>();

    }
}
