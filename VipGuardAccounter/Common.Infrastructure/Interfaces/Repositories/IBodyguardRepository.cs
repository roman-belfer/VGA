using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces
{
    public interface IBodyguardRepository
    {
        Task<IEnumerable<BodyguardDto>> GetCollection(SearchBodyguardsParameters searchParams);
        Task<BodyguardDto> GetBodyguardDetails(uint ID);
        Task Edit(BodyguardDto item);
    }
}
