using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task SetAsync(string Key, object value, TimeSpan duration);
        Task<string?> GetAsync(string Key);

    }
}
