using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class BodyguardRepository : IBodyguardRepository
    {
        private IEnumerable<BodyguardDto> _bodyguardCollection;

        public BodyguardRepository()
        {
            _bodyguardCollection = new List<BodyguardDto>()
            {
                new BodyguardDto(0, "Иван", "Иванович", "Иванов", "0670050102"),
                new BodyguardDto(1, "Петр", "Петрович", "Петров", "0670158512"),
                new BodyguardDto(2, "Елена", "Дмитриевна", "Иванова", "0670042589", false),
                new BodyguardDto(3, "Александр", "Александрович", "Александров", "0674851152"),
                new BodyguardDto(4, "Сергей", "Сергеевич", "Сергеев", "0670545587")
            };
        }

        public Task<BodyguardDto> GetBodyguardDetails(uint ID)
        {
            return Task.Run(() => _bodyguardCollection.FirstOrDefault(x => x.ID == ID));
        }

        public Task<IEnumerable<BodyguardDto>> GetBodyguardsCollection(SearchBodyguardsParameters searchParams)
        {
            return Task.Run(() => _bodyguardCollection);
        }
    }
}
