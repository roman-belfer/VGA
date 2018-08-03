using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces
{
    public interface IBodyguardRepository
    {
        Task<IEnumerable<BodyguardDto>> GetBodyguardsCollection(SearchParameters searchParams);
        Task<BodyguardDetailDto> GetBodyguardDetails(uint ID);
    }
}
