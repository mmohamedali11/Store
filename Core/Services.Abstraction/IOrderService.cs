using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        
        Task<OrderResultDto> GetOrderByIdAsync(Guid Id);
        Task<IEnumerable<OrderResultDto>> GetOrderByUserEmailAsync(string userEmail);

        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();



    }
}
