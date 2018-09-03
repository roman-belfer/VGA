using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrderDto>> GetOrdersCollection(SearchOrdersParameters searchParams);
        Task<OrderDto> GetOrderDetails(uint ID);
    }
}
