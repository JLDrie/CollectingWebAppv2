using Microsoft.EntityFrameworkCore;
using CollectingWebApp.Models;
using System.Collections.Generic;

namespace CollectingWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }
        public DbSet<CollectingWebApp.Models.Category> Category { get; set; }
        public DbSet<CollectingWebApp.Models.Collection> Collection { get; set; }
        public DbSet<CollectingWebApp.Models.Object> Object { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Collection>()
                .HasOne(o => o.Category)
                .WithMany(v => v.Collections)
                .HasForeignKey(o => o.CategoryId); ;

            modelBuilder.Entity<Models.Object>()
                .HasOne(o => o.Collection)
                .WithMany(v => v.Objects)
                .HasForeignKey(o => o.CollectionId); ;
        }
    }
}
