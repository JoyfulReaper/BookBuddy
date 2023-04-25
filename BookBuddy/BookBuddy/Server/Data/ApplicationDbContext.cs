using Microsoft.EntityFrameworkCore;

namespace BookBuddy.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var softDeletedEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is Book && e.State == EntityState.Deleted);

        foreach (var entry in softDeletedEntries)
        {
            entry.State = EntityState.Modified;
            entry.CurrentValues["IsDeleted"] = true;
        }

        return base.SaveChanges();
    }
}
