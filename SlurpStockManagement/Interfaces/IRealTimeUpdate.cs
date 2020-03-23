using System.Collections.Generic;
using System.Threading.Tasks;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IRealTimeUpdate
    {
        Task Send(List<CoffeeOrderItem> order);
    }
}
