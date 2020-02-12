using System;

namespace BackOfficeSystems.API.Dtos
{
    public class OrderToCreateDto
    {
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeOrdered { get; set; }
    }
}