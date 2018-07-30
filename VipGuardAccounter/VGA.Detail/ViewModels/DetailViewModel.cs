using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Prism.Events;
using VGA.Detail.Models;

namespace VGA.Detail.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private BodyguardModel _bodyguard;

        public DetailViewModel()
        {
            _repository = Container.Resolve<IBodyguardRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<DataEvents.DetailEvent>().Subscribe(OnDetail, ThreadOption.UIThread);
        }

        public BodyguardModel Bodyguard
        {
            get { return _bodyguard; }
            set
            {
                if (_bodyguard != value)
                {
                    _bodyguard = value;
                    OnPropertyChanged(nameof(Bodyguard));
                }
            }
        }

        private void OnDetail(uint id)
        {
            var detailDto = _repository.GetBodyguardDetails(id);

            Bodyguard = BodyguardModel.ConvertToModel(detailDto);
        }
    }
}
