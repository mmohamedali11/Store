using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;


namespace Presentation
{
    [ApiController]                          // Makes this a REST API controller
    [Route("api/[controller]")]             // URL will be api/orders
    public class OrdersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        // Constructor - gets service manager injected
        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]                           // Handles POST requests to api/orders
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                // Call service to create the order - returns domain model directly
                var order = await _serviceManager.orderService.CreateOrderAsync(createOrderDto);

                // Return 201 Created with the order data (domain model gets serialized to JSON)
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                // If something goes wrong, return error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]                    // Handles GET requests to api/orders/123
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                // Call service to get the order - returns domain model directly
                var order = await _serviceManager.orderService.GetOrderByIdAsync(id);

                if (order == null)
                {
                    return NotFound();       // Return 404 if order doesn't exist
                }

                return Ok(order);            // Return 200 OK with order data (domain model)
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}