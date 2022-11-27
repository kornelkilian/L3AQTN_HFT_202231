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

        public BusLogicTests()
        {
        }

        [SetUp]
        public void Init()
        {
            var brands = new List<Brand>()
            {
                new Brand {Id=1, Name="BMW",Country="GER"},
                new Brand {Id=2,Name="Mercedes",Country="GER"}
            }.AsQueryable();

            var owners = new List<Owner>()
            {
                 new Owner(){Id=1,Name="Gyula",ZIPCode=1111,HasMustache=true},
                new Owner(){Id=2,Name="Feri",ZIPCode=1212,HasMustache=true},
            }.AsQueryable();

            var buses1 = new List<Bus>()
            {
                new Bus(){Id=1,BrandId=1,Model="MOCK0",Price=1000,OwnerId=1,Brand=brands.First(),Owner=owners.First()},
                new Bus(){Id=3,BrandId=1,Model="MOCK1",Price=5000,OwnerId=1,Brand=brands.First(),Owner=owners.First() },
               


            }.AsQueryable();

            var buses2 = new List<Bus>()
            {
                new Bus(){Id=4,BrandId=2,Model="MOCK2",Price=2000,OwnerId=2,Brand=brands.Last(),Owner=owners.Last()}


            }.AsQueryable();
            buses1.First().Brand = brands.First();
            buses1.First().Owner = owners.First();
            foreach (var item in buses1)
            {
                brands.First().Buses.Add(item);
                item.Brand.Equals(brands.First());
                owners.First().Buses.Add(item);
                item.Owner.Equals(owners.First());
            }

            foreach (var item in buses2)
            {
                brands.Last().Buses.Add(item);
                item.Brand.Equals(brands.Last());

                owners.Last().Buses.Add(item);
                item.Owner.Equals(owners.Last());


            }
          

            mockBusRepo = new Mock<IRepository<Bus>>();
            mockBusRepo.Setup(t => t.ReadAll()).Returns(buses1.Concat(buses2));

            mockBrandRepo = new Mock<IRepository<Brand>>();
            mockBrandRepo.Setup(t => t.ReadAll()).Returns(brands);

            mockOwnerRepo = new Mock<IRepository<Owner>>();
            mockOwnerRepo.Setup(t => t.ReadAll()).Returns(owners);

        
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
          
            double? avg = logic.GetAvaragePriceByBrandCountry("GER");
            Assert.That(avg, Is.EqualTo((double)8000/3));
        }
        [Test]
        public void AvgPriceByOwner()
        {
           
            double? avg = logic.GetAvaragePriceByOwner("Gyula");
            Assert.That(avg, Is.EqualTo(3000));
        }

        [Test]
        public void HighestPriceByBrand()
        {
         
            double? h = logic.HighestPriceByBrand("Mercedes");
            Assert.That(h, Is.EqualTo(2000));
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
           
            var a = logic.BusesByZIPCode(1111);
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
          
            var result = logic.BusesWithMustacheOwners();
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

