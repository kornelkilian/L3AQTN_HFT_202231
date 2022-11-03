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

        public DbSet<Owner> Owners { get; set; }

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
            //s
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                     .UseInMemoryDatabase("BusDB")
                    .UseLazyLoadingProxies();
                    ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var owner = new Owner() { Id = 1, Name = "Gyula" };
            var brand = new Brand() { Id = 1, Name = "Citrom" };
            var bus = new Bus() { Id = 1, BrandId = 1, Model = "C4",OwnerId=1, Price = 1000 };
            var bus2 = new Bus() { Id = 2, BrandId = 1, Model = "C5",OwnerId=1, Price = 1200 };

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

