using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackOfficeSystems.API.Dtos;
using BackOfficeSystems.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.API.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext context;
        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddOrders(List<OrderToCreateDto> orders)
        {
            var ordersToAdd = new List<Order>();
            foreach (var order in orders)
            {
                ordersToAdd.Add(new Order
                {
                    Quantity = order.Quantity,
                    BrandId = order.BrandId,
                    TimeOrdered = order.TimeOrdered
                });
            }

            await context.Orders.AddRangeAsync(ordersToAdd);

            context.SaveChanges();
        }

        public async Task AddOrders(OrderFromFileDto orderFromFileDto)
        {
            var rows = System.IO.File.ReadLines(orderFromFileDto.FilePath).ToList();

            var headers = new[] { "time_recived", "quantity", "brand_id" };

            var headersRow = rows[0].Split('\t').Select(x => x.ToLower()).ToArray();

            var timeRecivedId = Array.IndexOf(headers, "time_recived");
            var quantityId = Array.IndexOf(headers, "quantity");
            var brandIdId = Array.IndexOf(headers, "brand_id");

            var orders = new List<Order>();
            foreach (var row in rows)
            {
                var arr = row.Split('\t');

                if (string.IsNullOrEmpty(row)) continue;

                if (row == rows[0]) continue;

                var item = new Order();
                DateTime.TryParse(arr[timeRecivedId], out DateTime time);
                int.TryParse(arr[quantityId], out int quantity);
                int.TryParse(arr[brandIdId], out int brandId);

                item.TimeOrdered = time;
                item.Quantity = quantity;
                item.BrandId = brandId;

                orders.Add(item);
            }

            await context.Orders.AddRangeAsync(orders);

            context.SaveChanges();
        }

        public async Task<IEnumerable<BrandQuantity>> GetBrandItemsQuantity()
        {
            var brands = await context.Brands.ToListAsync();
            var orders = await context.Orders.ToListAsync();
            var quantity = brands.GroupJoin(orders, b => b.Id, o => o.BrandId, (br, ord) => new BrandQuantity
            {
                BrandName = br.Name,
                Quantity = ord.Sum(c => c.Quantity)
            });

            return quantity;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await context.Orders.ToListAsync();

            return orders;
        }
    }
}
