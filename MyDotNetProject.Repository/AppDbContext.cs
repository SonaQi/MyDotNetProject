using Microsoft.EntityFrameworkCore;
using MyDotNetProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>(b =>
            {
                b.ToTable("test")
                .HasKey(x => x.id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
