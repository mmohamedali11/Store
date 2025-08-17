
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string userEnail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {

            Id = Guid.NewGuid();
            UserEnail = userEnail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEnail { get; set; }

        public Address  ShippingAddress { get; set; }

        //Order Items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); //Navigation property

        //DelviryMethod
        public DeliveryMethod DeliveryMethod { get; set; } //Navigation Property
        public int? DeliveryMethodId { get; set; }//Fk

        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        //Sub Total
        public decimal SubTotal { get; set; }

        //Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //Payment 
        public string PaymentIntentId { get; set; }



    }
}
