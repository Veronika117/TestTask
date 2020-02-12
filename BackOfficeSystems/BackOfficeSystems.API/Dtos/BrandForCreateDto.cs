using System.ComponentModel.DataAnnotations;

namespace BackOfficeSystems.API.Dtos
{
    public class BrandForCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}