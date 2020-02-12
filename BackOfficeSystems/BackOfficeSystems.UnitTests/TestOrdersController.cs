using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Controllers;
using BackOfficeSystems.API.Data;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BackOfficeSystems.UnitTests
{
    [TestFixture]
    public class TestOrdersController
    {
        private Mock<IOrderRepository> orderRepositoryMock;

        private OrdersController subject;

        [SetUp]
        public void SetUp()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();

            subject = new OrdersController(orderRepositoryMock.Object);
        }

        [Test]
        public async Task Test_GetBrands_ShouldReturnListOfOrders()
        {
            //arrange
            var orders = new List<Order>
            {
                new Order
                {
                    BrandId = 1,
                    TimeOrdered = DateTime.Now,
                    Quantity = 1
                }
            };

            orderRepositoryMock.Setup(p => p.GetOrders()).Returns(Task.FromResult(orders));

            //act
            var result = await subject.GetOrders();
            var OkActionResult = result as OkObjectResult;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(OkActionResult);
            Assert.AreEqual(orders, OkActionResult.Value);
        }

        [Test]
        public async Task Test_AddOrders_ShouldAddOrdersSuccessfully()
        {
            //arrange
            var orders = new List<OrderToCreateDto>
            {
                new OrderToCreateDto
                {
                     BrandId = 1,
                     Quantity = 1,
                     TimeOrdered = DateTime.Now
                }
            };

            //act
            var result = await subject.AddOrders(orders);

            var codeResult = result as StatusCodeResult;

            //assert
            Assert.IsInstanceOf<StatusCodeResult>(codeResult);
            Assert.AreEqual(codeResult.StatusCode, 201);
            orderRepositoryMock.Verify(x => x.AddOrders(orders), Times.Once);
        }

        [Test]
        public async Task Test_GetBrandItemsQuantity_ShouldReturnListOfBrandItemsQuantity()
        {
            //arrange
            IEnumerable<BrandQuantity> brandItemsQuantityList = new List<BrandQuantity>
            {
                new BrandQuantity
                {
                    BrandName = "Test Brand",
                    Quantity = 1
                }
            };

            orderRepositoryMock.Setup(p => p.GetBrandItemsQuantity()).Returns(Task.FromResult(brandItemsQuantityList));

            //act
            var result = await subject.GetBrandItemsQuantity();
            var OkActionResult = result as OkObjectResult;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(OkActionResult);
            Assert.AreEqual(brandItemsQuantityList, OkActionResult.Value);
        }

        [Test]
        public async Task Test_AddOrdersFromFile_ShouldAddOrdersSuccessfully()
        {
            //arrange
            var fileInfo = new OrderFromFileDto
            {
                FilePath = "TestPath"
            };

            //act
            var result = await subject.AddOrders(fileInfo);

            var codeResult = result as StatusCodeResult;

            //assert
            Assert.IsInstanceOf<StatusCodeResult>(codeResult);
            Assert.AreEqual(codeResult.StatusCode, 201);
            orderRepositoryMock.Verify(x => x.AddOrders(fileInfo), Times.Once);
        }
    }
}
