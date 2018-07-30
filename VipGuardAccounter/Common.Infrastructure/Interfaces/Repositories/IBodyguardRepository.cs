using Common.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Interfaces
{
    public interface IBodyguardRepository
    {
        IEnumerable<BodyguardDto> GetBodyguardsCollection(SearchParameters searchParams);
        BodyguardDetailDto GetBodyguardDetails(uint ID);
    }
}
