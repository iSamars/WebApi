using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace DockerSqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = new SqliteConnectionStringBuilder { DataSource = "Database.db" }.ToString();

            optionsBuilder.UseSqlite(new SqliteConnection(connString));
        }

        public DbSet<Product> Products { get; set; }
    }
}