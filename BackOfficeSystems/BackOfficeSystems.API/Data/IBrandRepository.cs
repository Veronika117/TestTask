using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;

namespace BackOfficeSystems.API.Data
{
    public interface IBrandRepository
    {
        Task AddBrands(List<BrandForCreateDto> brands);

        Task<List<Brand>> GetBrands();

        Task<Brand> GetBrand(int id);

        Task<BrandUpdatedDto> UpdateBrand(Brand brand);

        Task<BrandDeletedDto> DeleteBrand(BrandForDeleteDto brandForDelete);
    }
}