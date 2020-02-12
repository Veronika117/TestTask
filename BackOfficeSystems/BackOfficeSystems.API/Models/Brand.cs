using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeSystems.API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}