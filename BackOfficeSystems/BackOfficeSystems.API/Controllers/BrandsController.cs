using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Data;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository repo;

        public BrandsController(IBrandRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await repo.GetBrands();

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await repo.GetBrand(id);

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrands(List<BrandForCreateDto> brands)
        {
            await repo.AddBrands(brands);

            return StatusCode(201);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(BrandForDeleteDto brandForDelete)
        {
            var result = await repo.DeleteBrand(brandForDelete);
            if(result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Exception.GetType() == typeof(ArgumentNullException) 
                                    ? "No item with this id found" : "Failed to delete item");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditBrand(Brand brand)
        {
            var result = await repo.UpdateBrand(brand);
            if(result.IsSuccess)
            {
                return Ok();
            }
            else
            {   
                return BadRequest("Failed to edit item");
            }
        }
    }
}