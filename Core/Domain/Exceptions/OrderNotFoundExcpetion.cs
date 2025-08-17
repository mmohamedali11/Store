using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundExcpetion(Guid id ) : NotFoundException($"Order With id {id} is Not Found")
    {
    }
}
