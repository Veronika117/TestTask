using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackOfficeSystems.API.Models
{
    public class Order
    {
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public DateTime TimeOrdered { get; set; }
        public int Id { get; set; }
    }
}