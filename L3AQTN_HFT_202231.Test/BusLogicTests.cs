using System;
using System.Collections.Generic;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Logic;
using Moq;
using NUnit.Framework;
using System.Linq;
using Microsoft.Identity.Client;

namespace L3AQTN_HFT_202231.Test
{
    
    [TestFixture]
    public class BusLogicTests
    {
        BusLogic logic;
        Mock<IRepository<Bus>> mockBusRepo;
        Brand bmw;
        Brand placeholder;
        public BusLogicTests()
        {
        }

        [SetUp]
        public void Init()
        {
             bmw = new Brand() { Id = 1, Name = "BMW" };
            placeholder = new Brand() { Id = 2, Name = "PLACE" };
            mockBusRepo = new Mock<IRepository<Bus>>();
            mockBusRepo.Setup(m => m.ReadAll()).Returns(new List<Bus>()
            {

                new Bus(){Id=1,BrandId=1,Model="MOCK",Price=1000,OwnerId=10,Brand=bmw },
                 new Bus(){Id=3,BrandId=2,Model="MOCK",Price=5000,OwnerId=10,Brand=placeholder },
                new Bus(){Id=2,BrandId=1,Model="MOCK2",Price=2000,OwnerId=10,Brand=bmw }

            }.AsQueryable()) ;
            logic=new BusLogic(mockBusRepo.Object);
           
        }

		[Test]
         public void CreateBusWithCorrectModel()
        {
            var b = new Bus() { Model = "GOLF",Id=1 };

            logic.Create(b);

            mockBusRepo.Verify(_ => _.Create(b), Times.Once);
        }
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

        [Test]
        public void AvgPriceByBrandTest()
        {
           
            double? avg = logic.GetAvaragePriceByBrand(bmw);
            Assert.That(avg, Is.EqualTo(1500));
        }
        [Test]
        public void AvgPriceByModel()
        {

            double? avg = logic.GetAvaragePriceByModel("MOCK");
            Assert.That(avg, Is.EqualTo(3000));
        }

    }
}

