using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Views;
using Prism.Events;

namespace VGA.MainContent.ViewModels
{
    public class MainContentViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IEventAggregator _eventAggregator;

        private bool _isMenuVisible;
        private object _menuView;
        private IView _currentView;

        public MainContentViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            Init();
        }

        public bool IsMenuVisible
        {
            get { return _isMenuVisible; }
            set
            {
                if (_isMenuVisible != value)
                {
                    _isMenuVisible = value;
                    OnPropertyChanged(nameof(IsMenuVisible));
                }
            }
        }

        public object MenuView
        {
            get { return _menuView; }
            set
            {
                if (_menuView != value)
                {
                    _menuView = value;
                    OnPropertyChanged(nameof(MenuView));
                }
            }
        }

        public IView CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        private void Init()
        {
            MenuView = _navigator.GetMenuView();

            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Subscribe(OnNavigateView, ThreadOption.UIThread);
        }

        private void OnNavigateView(IView view)
        {
            CurrentView = view;

            IsMenuVisible = _navigator.IsFirstViewActive();
        }
    }
}
