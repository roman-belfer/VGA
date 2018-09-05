using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> GetCollection(SearchOrdersParameters searchParams);
        Task<OrderDto> GetDetail(uint ID);
    }
}
