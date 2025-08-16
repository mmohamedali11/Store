using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationEciption(IEnumerable<String> errors) : Exception("Validation Errors")
    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}
