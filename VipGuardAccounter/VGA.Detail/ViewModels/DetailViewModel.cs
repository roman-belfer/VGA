using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Enums;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Repositories;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;

namespace VGA.Detail.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        public DetailViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _repository = Container.Resolve<IBodyguardRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            //_eventAggregator.GetEvent<DataEvents.DetailEvent>().Subscribe(OnDetail, ThreadOption.UIThread);
        }

        public DelegateCommand EditCommand
        {
            get { return new DelegateCommand(OnEdit); }
        }

        private void OnEdit()
        {
            _navigator.Edit();

            //_eventAggregator.GetEvent<DataEvents.EditEvent>().Publish(new EditModel(_bodyguard.ID, EditModeEnum.EditEmployee));
        }

        private void OnDetail(uint id)
        {
            Task.Run(async () => await DetailAsync(id));
        }

        private async Task DetailAsync(uint id)
        {
            var detailDto = await _repository.GetDetail(id);

            //Bodyguard = BodyguardModel.ConvertToModel(detailDto);
        }
    }
}
