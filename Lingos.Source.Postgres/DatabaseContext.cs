using System.Collections.Generic;
using Lingos.Common;
using Lingos.Core;
using Lingos.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lingos.Source.Postgres
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Key> Keys { get; set; }
        public DbSet<Locale> Locales { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Translation> Translations { get; set; }

        private readonly Config _config;

        public DatabaseContext(Config config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Dictionary<string, object> config = _config.Plugins["sourcePostgres"];
            string user = config.GetString("user");
            string pass = config.GetString("password");
            string database = config.GetString("database");
            string host = config.GetString("host");
            string port = config.GetString("port");
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