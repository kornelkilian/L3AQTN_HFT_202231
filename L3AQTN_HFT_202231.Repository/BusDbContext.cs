using L3AQTN_HFT_202231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;

namespace L3AQTN_HFT_202231.Repository
{
	public class BusDbContext : DbContext
	{
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Brand> Brands { get; set; }

        private object DbCreationLock = new object();
        public BusDbContext()
        {
            lock (DbCreationLock)
            {
                this.Database.EnsureCreated();
            }
        }

        public BusDbContext(DbContextOptions<BusDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               // optionsBuilder
                    //.UseLazyLoadingProxies()
                   // .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarDb.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var brand = new Brand() { Id = 1, Name = "Citrom" };
            var bus = new Bus() { Id = 1, BrandId = 1, Model = "C4", Price = 1000 };
            var bus2 = new Bus() { Id = 2, BrandId = 1, Model = "C5", Price = 1200 };
            var owner = new Owner() { Id = 1, Name = "Gyula" };

            modelBuilder.Entity<Bus>(entity =>
                            entity.HasOne(bus => bus.Brand)
                            .WithMany(brand => brand.Buses)
                            .HasForeignKey(bus => bus.BrandId)
                            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Brand>().HasData(brand);
            modelBuilder.Entity<Bus>().HasData(bus, bus2);
            modelBuilder.Entity<Owner>().HasData(owner);
        }

       

	}
}

