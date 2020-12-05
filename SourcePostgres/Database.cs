using Common;
using Microsoft.EntityFrameworkCore;

namespace SourcePostgres
{
    public class Database : DbContext
    {
        public DbSet<Locale> Locales { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string user = "user";
            const string pass = "password";
            const string database = "database";
            optionsBuilder.UseNpgsql($"USER ID={user};Password={pass};Host=localhost;Port=5432;Database={database}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>()
                .HasKey(t => new {t.Key, t.LocaleName, t.ScopeName, t.Variant});
            modelBuilder.Entity<Translation>()
                .Property(t => t.Variant)
                .HasDefaultValue("none");
        }
    }
}