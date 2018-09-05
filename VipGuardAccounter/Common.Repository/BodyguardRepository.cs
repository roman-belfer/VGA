using Common.Infrastructure.DataModels;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces.Repositories;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class BodyguardRepository : IBodyguardRepository
    {
        private readonly IEventAggregator _eventAggregator;
        private IEnumerable<BodyguardDto> _bodyguardCollection;

        public BodyguardRepository()
        {
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _bodyguardCollection = new List<BodyguardDto>()
            {
                new BodyguardDto(0, "Иван", "Иванович", "Иванов", "0670050102"),
                new BodyguardDto(1, "Петр", "Петрович", "Петров", "0670158512"),
                new BodyguardDto(2, "Елена", "Дмитриевна", "Иванова", "0670042589", false),
                new BodyguardDto(3, "Александр", "Александрович", "Александров", "0674851152"),
                new BodyguardDto(4, "Сергей", "Сергеевич", "Сергеев", "0670545587")
            };
        }

        public Task<BodyguardDto> GetDetail(uint ID)
        {
            return Task.Run(() => _bodyguardCollection.FirstOrDefault(x => x.ID == ID));
        }

        public Task<IEnumerable<BodyguardDto>> GetCollection(SearchBodyguardsParameters searchParams)
        {
            return Task.Run(() => _bodyguardCollection);
        }

        public void Save(BodyguardDto dto)
        {
            Task.Run(() =>
            {
                var existDto = _bodyguardCollection.FirstOrDefault(x => x.ID == dto.ID);
                if (existDto != null)
                {
                    var index = _bodyguardCollection.ToList().IndexOf(existDto);
                    _bodyguardCollection.ToList().Remove(existDto);
                    _bodyguardCollection.ToList().Insert(index, dto);
                }
                else
                {
                    _bodyguardCollection.ToList().Add(dto);
                }

                _eventAggregator.GetEvent<RepositoryEvents.BodyguardRepositoryChanged>().Publish();
            });
        }

        public void Delete(uint id)
        {
            Task.Run(() =>
            {
                var existDto = _bodyguardCollection.FirstOrDefault(x => x.ID == id);
                if (existDto != null)
                {
                    _bodyguardCollection.ToList().Remove(existDto);
                }

                _eventAggregator.GetEvent<RepositoryEvents.BodyguardRepositoryChanged>().Publish();
            });
        }
    }
}
