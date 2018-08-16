using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class BodyguardRepository : IBodyguardRepository
    {
        private List<BodyguardDetailDto> _bodyguardCollection;

        public BodyguardRepository()
        {
            _bodyguardCollection = new List<BodyguardDetailDto>()
            {
                new BodyguardDetailDto()
                {
                    BaseInfo = new BodyguardDto(0, "Иван", "Иванович", "Иванов", 3, "0670050102")
                },
                new BodyguardDetailDto
                {
                    BaseInfo = new BodyguardDto(1, "Петр", "Петрович", "Петров", 5, "0670158512")
                },
                new BodyguardDetailDto
                {
                    BaseInfo = new BodyguardDto(2, "Дмитрий", "Дмитриевич", "Дмитриев", 5, "0670042589")
                },
                new BodyguardDetailDto
                {
                    BaseInfo = new BodyguardDto(3, "Александр", "Александрович", "Александров", 4, "0674851152")
                },
                new BodyguardDetailDto
                {
                    BaseInfo = new BodyguardDto(4, "Сергей", "Сергеевич", "Сергеев", 3, "0670545587")
                },
            };
        }

        public Task<BodyguardDetailDto> GetBodyguardDetails(uint ID)
        {
            return Task.Run(() => _bodyguardCollection.FirstOrDefault(x => x.BaseInfo.ID == ID));
        }

        public Task<IEnumerable<BodyguardDto>> GetBodyguardsCollection(SearchBodyguardsParameters searchParams)
        {
            return Task.Run(() => _bodyguardCollection.Select(x => x.BaseInfo));
        }
    }
}
