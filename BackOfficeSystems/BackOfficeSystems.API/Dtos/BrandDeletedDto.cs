using System;

namespace BackOfficeSystems.API.Dtos
{
    public class BrandDeletedDto
    {
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
    }
}