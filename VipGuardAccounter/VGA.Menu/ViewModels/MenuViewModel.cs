using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Views;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;

namespace VGA.Menu.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IEventAggregator _eventAggregator;

        private bool _isHomeEnabled;

        public MenuViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            Init();
        }

        public bool IsHomeEnabled
        {
            get { return _isHomeEnabled; }
            set
            {
                if (_isHomeEnabled != value)
                {
                    _isHomeEnabled = value;
                    OnPropertyChanged(nameof(IsHomeEnabled));
                }
            }
        }

        public DelegateCommand BackCommand
        {
            get { return new DelegateCommand(OnBack); }
        }

        public DelegateCommand HomeCommand
        {
            get { return new DelegateCommand(OnHome); }
        }

        private void Init()
        {
            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Subscribe(OnNavigateView, ThreadOption.UIThread);
        }

        private void OnNavigateView(IView view)
        {
            IsHomeEnabled = !_navigator.IsFirstViewActive();
        }

        private void OnHome()
        {
            _navigator.StartMenu();
        }

        private void OnBack()
        {
            _navigator.Back();
        }
    }
}
