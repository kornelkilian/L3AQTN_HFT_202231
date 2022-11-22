using System;
using System.Collections.Generic;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Logic;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace L3AQTN_HFT_202231.Test
{
    [TestFixture]
    public class BusLogicTests
    {
        public BusLogicTests()
        {
        }

		[Test]
        public void GetAverageByBrand_CallWithMockedRepo_ReturnsAverage()
        {
            // Arrange GIVEN

            var brand = new Brand() { Id = 1, Name = "Citrom" };
            var car = new Bus() { Id = 1, BrandId = 1, Model = "C4", Price = 1000 };
            var car2 = new Bus() { Id = 2, BrandId = 1, Model = "C5", Price = 1200 };

            var carList = new List<Bus> { car, car2 };

            var carRepoMock = new Mock<IRepository<Bus>>(MockBehavior.Strict);
            var brandRepoMock = new Mock<IRepository<Brand>>();

            carRepoMock.Setup(repo => repo.ReadAll())
                       .Returns(() => carList);

            var logic = new BusLogic(carRepoMock.Object, brandRepoMock.Object);

            // Act WHEN
            var result2 = logic.GetAverageByBrand().ToList();

            // Assert THEN
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.Count, Is.EqualTo(1), $"{nameof(result2)}.{nameof(result2.Count)} is not proper");
            Assert.That(result2.First().AveragePrice, Is.EqualTo(1100));
            Assert.That(result2.First().AveragePrice, Is.Not.EqualTo(1000));


        }
    }
}

