using Microsoft.EntityFrameworkCore;
using ReviewApiApp;
using ReviewApiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContexts
{
    public class ApiReviewDbContext:DbContext
    {
        public ApiReviewDbContext(DbContextOptions<ApiReviewDbContext> options):base(options) 
        {
            
        }
        public DbSet<Production> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Production>().HasData(new Production()
            {
                Id=1,
                Name="anything about production world.."
            });

            modelBuilder.Entity<Brand>().HasData(new Brand()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                ProductionId = 1,
            });

            // It is another way to applying seeding concept
         //  helperFolder.SeedData(modelBuilder);
        }
    }
}
