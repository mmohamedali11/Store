using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }





        public string UserEnail { get; set; }

        public AddressDto ShippingAddress { get; set; }

        //Order Items
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); //Navigation property

        //DelviryMethod
        public string DeliveryMethod { get; set; }
        

        public string PaymentStatus { get; set; } 

        //Sub Total
        public decimal SubTotal { get; set; }

        //Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //Payment 
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
