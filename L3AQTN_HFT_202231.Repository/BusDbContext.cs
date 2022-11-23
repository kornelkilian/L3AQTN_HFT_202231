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
                     .UseLazyLoadingProxies()
                     .UseInMemoryDatabase("BusDB")
                    ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var owner = new Owner() { Id = 1, Name = "Gyula",ZIPCode=1111 };
            var owner2 = new Owner() { Id = 2, Name = "Tibor",ZIPCode=1212 };
            var brand = new Brand() { Id = 1, Name = "Mercedes" };
            var brand2 = new Brand() { Id = 2, Name = "BMW" };
            var bus = new Bus() { Id = 1, BrandId = 1, Model = "C4",OwnerId=1, Price = 1000 };
            var bus2 = new Bus() { Id = 2, BrandId = 1, Model = "C5",OwnerId=1, Price = 1200 };
            var bus3 = new Bus() { Id = 3, BrandId = 2, Model = "A2", OwnerId =2, Price = 2500 };

            modelBuilder.Entity<Bus>(entity =>
                            entity.HasOne(bus => bus.Brand)
                            .WithMany(brand => brand.Buses)
                            .HasForeignKey(bus => bus.BrandId)
                            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Bus>(entity =>
                          entity.HasOne(bus => bus.Owner)
                          .WithMany(owner => owner.Buses)
                          .HasForeignKey( bus=> bus.OwnerId)
                          .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Brand>(entity =>
                        entity.HasMany(brand => brand.Buses)
                        .WithOne(x=>x.Brand)
                        .HasForeignKey(x => x.BrandId)
                        .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Owner>(entity =>
              entity.HasMany(o => o.Buses)
              .WithOne(bus => bus.Owner)
              .HasForeignKey(a => a.OwnerId)
              .OnDelete(DeleteBehavior.ClientSetNull));



            modelBuilder.Entity<Brand>().HasData(brand,brand2);
            modelBuilder.Entity<Bus>().HasData(bus, bus2,bus3);
            modelBuilder.Entity<Owner>().HasData(owner,owner2);
        }

       

	}
}

