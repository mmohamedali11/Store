using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order : BaseEntity<int>
    {
        public string OrderNumber { get; set; } = string.Empty;
        public Decimal Total { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public string CustomerName { get; set; } = string.Empty;
        public Address Address { get; set; }

        public int AddressId { get; set; }



    }
}
