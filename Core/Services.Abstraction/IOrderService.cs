using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto);     // Return domain model directly
        Task<Order?> GetOrderByIdAsync(int orderId);                     // Return domain model directly
    }
}