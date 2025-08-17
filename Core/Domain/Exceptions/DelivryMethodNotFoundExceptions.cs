using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DelivryMethodNotFoundExceptions(int id ) : NotFoundException($"Delivry Method With Id {id} Is Not Found")
    {
    }
}
