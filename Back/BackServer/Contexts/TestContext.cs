using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using DbEntity;

namespace BackServer.Contexts
{
    public class TestContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly bool _useProfileDbConnection;
        
        public TestContext(IConfiguration configuration, bool useProfile = false)
        {
            this._configuration = configuration;
            this._useProfileDbConnection = useProfile;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(this._configuration.GetConnectionString("DefaultConnection"));
            DbConnection connection = this._useProfileDbConnection ? (DbConnection) new ProfiledDbConnection((DbConnection) npgsqlConnection, (IDbProfiler) MiniProfiler.Current) : (DbConnection) npgsqlConnection;
            options.UseNpgsql(connection);
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