using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Constructor - gets dependencies injected
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;  // For database operations
            _mapper = mapper;          // For converting between DTOs and domain models
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            // Step 1: Create Address first (we need its ID for the order)
            var address = new Address
            {
                Name = createOrderDto.Address,        // Direct property access
                Country = createOrderDto.Country,         // Direct property access  
                City = createOrderDto.City,               // Direct property access
                MobileNumber = createOrderDto.MobileNumber // Direct property access
            };

            // Save address to database and get its ID
            var addressRepo = _unitOfWork.GetRepository<Address, int>();
            await addressRepo.AddAsync(address);                    // Add to memory
            await _unitOfWork.SaveChangesAsync();                  // Save to database to get ID

            // Step 2: Process each item in the order
            var orderItems = new List<Domain.Models.OrderItem>();   // Use full namespace to avoid confusion
            decimal total = 0;
            var productRepo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in createOrderDto.Items)              // Loop through Items instead of OrderItems
            {
                // Get product from database to get current price
                var product = await productRepo.GetAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product {item.ProductId} not found");
                }

                // Create order item with current product price
                var orderItem = new Domain.Models.OrderItem         // Use full namespace
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.price                          // Use current price from database
                };

                orderItems.Add(orderItem);
                total += orderItem.Price * orderItem.Quantity;    // Add to total
            }

            // Step 3: Create the order
            var order = new Order
            {
                OrderNumber = "ORD" + DateTime.Now.Ticks,         // Simple order number generation
                CustomerName = createOrderDto.CustomerName,
                AddressId = address.Id,                           // Link to the address we just saved
                Total = total,
                OrderItems = orderItems
            };

            // Step 4: Save order to database
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            await orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // Step 5: Return the domain model directly (no DTO conversion)
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var order = await orderRepo.GetAsync(orderId);
            return order;  // Return domain model directly
        }
    }
}