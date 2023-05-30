using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using DbEntity;
using Microsoft.Extensions.Options;

namespace BackServer.Contexts
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<HeadingOne> HeadingsOne { get; set; }
        public DbSet<HeadingTwo> HeadingsTwo { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
    }
}