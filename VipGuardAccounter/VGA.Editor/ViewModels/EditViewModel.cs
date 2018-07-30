using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using VGA.Editor.Models;

namespace VGA.Editor.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private BodyguardModel _bodyguard;

        public EditViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _repository = Container.Resolve<IBodyguardRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<DataEvents.EditEvent>().Subscribe(OnEdit, ThreadOption.UIThread);
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

        public DelegateCommand SaveCommand
        {
            get { return new DelegateCommand(OnSave); }
        }

        public DelegateCommand CancelCommand
        {
            get { return new DelegateCommand(OnCancel); }
        }

        private void OnCancel()
        {
            _navigator.Back();
        }

        private void OnSave()
        {
            //TO DO: Saving Logic

            _navigator.Back();
        }

        private void OnEdit(uint? id)
        {
            if (id.HasValue)
            {
                var detailDto = _repository.GetBodyguardDetails(id.Value);

                Bodyguard = BodyguardModel.ConvertToModel(detailDto);
            }
            else
            {
                Bodyguard = new BodyguardModel();
            }
        }
    }
}
