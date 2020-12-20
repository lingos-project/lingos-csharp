using Lingos.Common;
using Microsoft.EntityFrameworkCore;

namespace Lingos.Source.Postgres
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
            const string host = "localhost";
            const string port = "5432";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql($"USER ID={user};Password={pass};Host={host};Port={port};Database={database}")
                    .UseSnakeCaseNamingConvention();
            }
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