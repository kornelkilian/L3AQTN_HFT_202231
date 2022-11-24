using System;
using System.Collections.Generic;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Logic;
using Moq;
using NUnit.Framework;
using System.Linq;
using Microsoft.Identity.Client;
using L3AQTN_HFT_202231.Repository;

namespace L3AQTN_HFT_202231.Test
{
    
    [TestFixture]
    public class BusLogicTests
    {
        BusLogic logic;
        Mock<IRepository<Bus>> mockBusRepo;
        Mock<IRepository<Brand>> mockBrandRepo;
        Mock<IRepository<Owner>> mockOwnerRepo;

     


        Brand bmw;
        
        public BusLogicTests()
        {
        }

        [SetUp]
        public void Init()
        {
             bmw = new Brand() { Id = 1, Name = "BMW" };
           // placeholder = new Brand() { Id = 2, Name = "PLACE" };
            mockBusRepo = new Mock<IRepository<Bus>>();
            mockBusRepo.Setup(m => m.ReadAll()).Returns(new List<Bus>()
            {

                new Bus(){Id=1,BrandId=1,Model="MOCK",Price=1000,OwnerId=1},
                 new Bus(){Id=3,BrandId=2,Model="MOCK",Price=5000,OwnerId=1 },
                new Bus(){Id=2,BrandId=1,Model="MOCK2",Price=2000,OwnerId=2}

            }.AsQueryable()) ;

            mockOwnerRepo = new Mock<IRepository<Owner>>();
            mockOwnerRepo.Setup(m => m.ReadAll()).Returns(new List<Owner>()
            {

                new Owner(){Id=1,Name="Gyula",ZIPCode=1111,HasMustache=true},
                new Owner(){Id=2,Name="Feri",ZIPCode=1212,HasMustache=true}


            }.AsQueryable());

            mockBrandRepo = new Mock<IRepository<Brand>>();
            mockBrandRepo.Setup(m => m.ReadAll()).Returns(new List<Brand>()
            {
                new Brand(){Name="BMW",Id=1,Country="GER"},
                new Brand(){Name="Mercedes",Id=2,Country="GER"}


            }.AsQueryable());
            logic =new BusLogic(mockBusRepo.Object,mockOwnerRepo.Object,mockBrandRepo.Object);
           
        }

		[Test]
         public void CreateBusWithCorrectModel()
        {
            var b = new Bus() { Model = "GOLF",Id=1 };

            logic.Create(b);

            mockBusRepo.Verify(_ => _.Create(b), Times.Once);
        }

        //CREATE TESTS
        [Test]
        public void CreateBusWithIncorrectModel()
        {
            //Modell név nem lehet 2 karakter alatt.
            var b = new Bus() { Model = "A" };
            try
            {
                logic.Create(b);
            }
            catch 
            {

                
            }

            mockBusRepo.Verify(r => r.Create(b), Times.Never);
        }
        [Test]
        public void CreateBusWithZeroPrice()
        {
            var b = new Bus() {Model="AAA", Price = 0 };
            try
            {
                logic.Create(b);
            }
            catch 
            {

                
            }
            mockBusRepo.Verify(r => r.Create(b), Times.Never);
        }

        //OTHER TESTS
        [Test]
        public void DeleteBusTest()
        {
            var b = new Bus() { Model = "GOLF", Id = 15 };

            logic.Create(b);

            logic.Delete(15);

            mockBusRepo.Verify(_ => _.Delete(15), Times.Once);
        }

        [Test]
        public void UpdateBus()
        {

            Bus bus = new Bus()
            {
                Model = "Old"
            };
            logic.Create(bus);
            bus.Model = "New";

            logic.Update(bus);

            mockBusRepo.Verify(_ => _.Update(bus), Times.Once);

        }

        //NON-CRUD TESTS

        [Test]
        public void AvgPriceByBrandCountry()
        {
            var c = new BusDbContext();

            IRepository<Bus> repo = new BusRepository(c);
            IRepository<Brand> brandRepo = new BrandRepository(c);
            IRepository<Owner> ownerRepo = new OwnerRepository(c);


            var logc = new BusLogic(repo, ownerRepo, brandRepo);

            double? avg = logc.GetAvaragePriceByBrandCountry("GER");
            Assert.That(avg, Is.EqualTo((double)4700/3));
        }
        [Test]
        public void AvgPriceByOwner()
        {
            //LazyLoading nem működött a Mockkal, ezért a DbContext adataival faket csináltam.
            var c = new BusDbContext();

            IRepository<Bus> repo = new BusRepository(c);
            IRepository<Brand> brandRepo = new BrandRepository(c);
            IRepository<Owner> ownerRepo = new OwnerRepository(c);


            var logc = new BusLogic(repo, ownerRepo, brandRepo);

           

            double? avg = logc.GetAvaragePriceByOwner("Gyula");
            Assert.That(avg, Is.EqualTo(1100));
        }

        [Test]
        public void HighestPriceByBrand()
        {
            //LazyLoading nem működött a Mockkal
            var c = new BusDbContext();

            IRepository<Bus> repo = new BusRepository(c);
            IRepository<Brand> brandRepo = new BrandRepository(c);
            IRepository<Owner> ownerRepo = new OwnerRepository(c);


            var logc = new BusLogic(repo, ownerRepo, brandRepo);
            
            double? h = logc.HighestPriceByBrand("Mercedes");
            Assert.That(h, Is.EqualTo(1200));
        }

        [Test]
        public void GetBusCountByMustache()
        {
            var b=logic.GetBusCountByMustache();

            var c = b.Count();

            Assert.That(c == 2);
        }
        [Test]
        public void BusesByZipCode()
        {
            var c = new BusDbContext();

            IRepository<Bus> repo = new BusRepository(c);
            IRepository<Brand> brandRepo = new BrandRepository(c);
            IRepository<Owner> ownerRepo = new OwnerRepository(c);


            var logc = new BusLogic(repo, ownerRepo, brandRepo);

            var a = logc.BusesByZIPCode(1111);
            bool helper = true;
            foreach (Bus bus in a)
            {
                if (bus.Owner.ZIPCode!=1111)
                {
                    helper = false;
                }

            }
            Assert.That(helper == true);
        }

        [Test]
        public void BusesWithMustacheOwners()
        {
            var c = new BusDbContext();

            IRepository<Bus> repo = new BusRepository(c);
            IRepository<Brand> brandRepo = new BrandRepository(c);
            IRepository<Owner> ownerRepo = new OwnerRepository(c);


            var logc = new BusLogic(repo, ownerRepo, brandRepo);

            var result = logc.BusesWithMustacheOwners();
            bool helper = true;
            foreach (Bus bus in result)
            {
                if (bus.Owner.HasMustache==false)
                {
                    helper = false;
                }
            }

            Assert.That(helper==true);

        }
    }
}

