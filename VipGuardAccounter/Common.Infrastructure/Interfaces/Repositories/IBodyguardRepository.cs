using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces.Repositories
{
    public interface IBodyguardRepository
    {
        Task<IEnumerable<BodyguardDto>> GetCollection(SearchBodyguardsParameters searchParams);
        Task<BodyguardDto> GetDetail(uint ID);
    }
}
