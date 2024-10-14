using Catalog.API.Contract.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Provider.Database
{
    public class CatalogDbContext :DbContext
    {
        public static CatalogDbContext Create(ConnectionHelper connectionHelper)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionHelper.GetConnectionString());
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            IMongoDatabase database = new MongoClient(settings).GetDatabase(connectionHelper.GetDatabaseName());
            return new(new DbContextOptionsBuilder<CatalogDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
        }

        public CatalogDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToCollection("products");
        }
    }
}
