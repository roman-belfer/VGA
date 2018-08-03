using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using VGA.Detail.Models;

namespace VGA.Detail.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private BodyguardModel _bodyguard;

        public DetailViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
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

        public DelegateCommand EditCommand
        {
            get { return new DelegateCommand(OnEdit); }
        }

        private void OnEdit()
        {
            _navigator.Edit();

            _eventAggregator.GetEvent<DataEvents.EditEvent>().Publish(_bodyguard.ID);
        }

        private void OnDetail(uint id)
        {
            Task.Run(async () => await DetailAsync(id));
        }

        private async Task DetailAsync(uint id)
        {
            var detailDto = await _repository.GetBodyguardDetails(id);

            Bodyguard = BodyguardModel.ConvertToModel(detailDto);
        }
    }
}
