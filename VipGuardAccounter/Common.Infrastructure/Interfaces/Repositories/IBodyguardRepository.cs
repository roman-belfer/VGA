using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces.Repositories
{
    public interface IBodyguardRepository
    {
        Task<IEnumerable<BodyguardDto>> GetBodyguardsCollection(SearchBodyguardsParameters searchParams);
        Task<BodyguardDto> GetBodyguardDetails(uint ID);
    }
}
