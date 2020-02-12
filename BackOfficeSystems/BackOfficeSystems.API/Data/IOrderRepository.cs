using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;

namespace BackOfficeSystems.API.Data
{
    public interface IOrderRepository
    {
        Task AddOrders(List<OrderToCreateDto> orders);

        Task AddOrders(OrderFromFileDto orderFromFileDto);

        Task<List<Order>> GetOrders();

        Task<IEnumerable<BrandQuantity>> GetBrandItemsQuantity();
    }
}
