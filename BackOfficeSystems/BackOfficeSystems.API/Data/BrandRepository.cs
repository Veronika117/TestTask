using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.API.Data
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext context;
        public BrandRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddBrands(List<BrandForCreateDto> brands)
        {
            var newBrands = new List<Brand>(brands.Count);

            foreach(var brand in brands)
            {
                newBrands.Add(new Brand { Name = brand.Name });
            }

            await context.Brands.AddRangeAsync(newBrands);

            context.SaveChanges();
        }

        public async Task<BrandDeletedDto> DeleteBrand(BrandForDeleteDto brandForDelete)
        {
            var brandDeletedDto = new BrandDeletedDto();
            try
            {
                var brandToDelete = await context.Brands.FirstOrDefaultAsync(x => x.Id == brandForDelete.Id);
                context.Brands.Remove(brandToDelete);
                context.SaveChanges();
                
                brandDeletedDto.IsSuccess = true;
            }
            catch(Exception ex)
            {
               brandDeletedDto.Exception = ex;
            }

            return brandDeletedDto;
        }

        public async Task<Brand> GetBrand(int id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            return brand;
        }

        public async Task<List<Brand>> GetBrands()
        {
            var brands = await context.Brands.ToListAsync();

            return brands;
        }

        public async Task<BrandUpdatedDto> UpdateBrand(Brand brand)
        {
            var brandUpdatedDto = new BrandUpdatedDto();
            var existingBrand = await context.Brands.FirstOrDefaultAsync(x => x.Id == brand.Id);
            try
            {
                context.Brands.Update(brand);
                context.SaveChanges();

                brandUpdatedDto.IsSuccess = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                brandUpdatedDto.Exception = ex;
            }

            return brandUpdatedDto;
        }
    }
}