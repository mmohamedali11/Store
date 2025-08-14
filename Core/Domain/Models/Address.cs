using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Address : BaseEntity<int>

    {
        public string Name { get; set; }
        public string Country { get; set; }

        public String City { get; set; }
        public string MobileNumber { get; set; }
    }
}
