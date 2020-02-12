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
    public class TestBrandsController
    {
        private Mock<IBrandRepository> brandRepositoryMock;

        private BrandsController subject;

        [SetUp]
        public void SetUp()
        {
            brandRepositoryMock = new Mock<IBrandRepository>();

            subject = new BrandsController(brandRepositoryMock.Object);
        }

        [Test]
        public async Task Test_GetBrands_ShouldReturnListOfBrands()
        {
            //arrange
            var brands = new List<Brand>
            {
                new Brand { Name="TestBrand" }
            };

            brandRepositoryMock.Setup(p => p.GetBrands()).Returns(Task.FromResult(brands));

            //act
            var result = await subject.GetBrands();
            var OkActionResult = result as OkObjectResult;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(OkActionResult);
            Assert.AreEqual(brands, OkActionResult.Value);
        }

        [Test]
        public async Task Test_GetBrand_ShouldReturnBrand()
        {
            //arrange
            var id = 1;
            var brand = new Brand { Name = "TestBrand" };

            brandRepositoryMock.Setup(p => p.GetBrand(id)).Returns(Task.FromResult(brand));

            //act
            var result = await subject.GetBrand(id);
            var OkActionResult = result as OkObjectResult;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(OkActionResult);
            Assert.AreEqual(brand, OkActionResult.Value);
        }

        [Test]
        public async Task Test_AddBrands_ShouldAddBrandsSuccessfully()
        {
            //arrange
            var brands = new List<BrandForCreateDto>
            {
                new BrandForCreateDto { Name="TestBrand" }
            };

            //act
            var result = await subject.AddBrands(brands);
            
            var codeResult = result as StatusCodeResult;

            //assert
            Assert.IsInstanceOf<StatusCodeResult>(codeResult);
            Assert.AreEqual(codeResult.StatusCode, 201);
            brandRepositoryMock.Verify(x => x.AddBrands(brands), Times.Once);
        }
        
        [Test]
        public async Task Test_DeleteBrand_ShouldDeleteSuccessfully()
        {
            //arrange
            var brandForDelete = new BrandForDeleteDto {Id = 1};
            var resultModel = new BrandDeletedDto { IsSuccess = true };
            brandRepositoryMock.Setup(p => p.DeleteBrand(brandForDelete)).Returns(Task.FromResult(resultModel));

            //act
            var result = await subject.DeleteBrand(brandForDelete);
            var OkActionResult = result as OkResult;

            //assert
            Assert.IsInstanceOf<OkResult>(OkActionResult);
            brandRepositoryMock.Verify(x => x.DeleteBrand(brandForDelete), Times.Once);
        }
        
        [Test]
        public async Task Test_DeleteBrand_ThrowsNullArgumentException()
        {
            //arrange
            var brandForDelete = new BrandForDeleteDto {Id = 1};
            var resultModel = new BrandDeletedDto { IsSuccess = false, Exception = new ArgumentNullException() };
            brandRepositoryMock.Setup(p => p.DeleteBrand(brandForDelete)).Returns(Task.FromResult(resultModel));

            //act
            var result = await subject.DeleteBrand(brandForDelete);
            var badRequest = result as BadRequestObjectResult;

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequest);
            brandRepositoryMock.Verify(x => x.DeleteBrand(brandForDelete), Times.Once);
        }
        
        [Test]
        public async Task Test_DeleteBrand_ThrowsException()
        {
            //arrange
            var brandForDelete = new BrandForDeleteDto {Id = 1};
            var resultModel = new BrandDeletedDto { IsSuccess = false, Exception = new Exception() };
            brandRepositoryMock.Setup(p => p.DeleteBrand(brandForDelete)).Returns(Task.FromResult(resultModel));

            //act
            var result = await subject.DeleteBrand(brandForDelete);
            var badRequest = result as BadRequestObjectResult;

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequest);
            brandRepositoryMock.Verify(x => x.DeleteBrand(brandForDelete), Times.Once);
        }
        
        [Test]
        public async Task Test_EditBrand_ShouldEditSuccessfully()
        {
            //arrange
            var brandForEdit = new Brand
            {
                Id = 1,
                Name = "Test Brand"
            };
            var resultModel = new BrandUpdatedDto() { IsSuccess = true };
            brandRepositoryMock.Setup(p => p.UpdateBrand(brandForEdit)).Returns(Task.FromResult(resultModel));

            //act
            var result = await subject.EditBrand(brandForEdit);
            var OkActionResult = result as OkResult;

            //assert
            Assert.IsInstanceOf<OkResult>(OkActionResult);
            brandRepositoryMock.Verify(x => x.UpdateBrand(brandForEdit), Times.Once);
        }
        
        [Test]
        public async Task Test_EditBrand_ThrowsException()
        {
            //arrange
            var brandForEdit = new Brand
            {
                Id = 1,
                Name = "Test Brand"
            };
            var resultModel = new BrandUpdatedDto { IsSuccess = false, Exception = new Exception() };
            brandRepositoryMock.Setup(p => p.UpdateBrand(brandForEdit)).Returns(Task.FromResult(resultModel));

            //act
            var result = await subject.EditBrand(brandForEdit);
            var badRequest = result as BadRequestObjectResult;

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(badRequest);
            brandRepositoryMock.Verify(x => x.UpdateBrand(brandForEdit), Times.Once);
        }
    }
}
