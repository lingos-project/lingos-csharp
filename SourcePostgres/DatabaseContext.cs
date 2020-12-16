using Common;
using Microsoft.EntityFrameworkCore;

namespace SourcePostgres
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Key> Keys { get; set; }
        public DbSet<Locale> Locales { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Translation> Translations { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string user = "user";
            const string pass = "password";
            const string database = "database";
            optionsBuilder
                .UseNpgsql($"USER ID={user};Password={pass};Host=localhost;Port=5432;Database={database}")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(t => new {t.KeyName, t.ScopeName, t.LocaleName, t.Variant});
                entity.Property(t => t.Variant).HasDefaultValue("none");
            });
        }
    }
}