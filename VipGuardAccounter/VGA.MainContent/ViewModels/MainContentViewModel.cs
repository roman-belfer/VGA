using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Views;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;

namespace VGA.MainContent.ViewModels
{
    public class MainContentViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IEventAggregator _eventAggregator;
        
        private bool _isViewChanged;
        private bool _isHomeVisible;
        private bool _isMenuVisible;
        private object _menuView;
        private IView _currentView;

        public MainContentViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            Init();
        }

        #region Properties

        public bool IsViewChanged
        {
            get { return _isViewChanged; }
            set
            {
                if (_isViewChanged != value)
                {
                    _isViewChanged = value;
                    OnPropertyChanged(nameof(IsViewChanged));
                }
            }
        }

        public bool IsHomeVisible
        {
            get { return _isHomeVisible; }
            set
            {
                if (_isHomeVisible != value)
                {
                    _isHomeVisible = value;
                    OnPropertyChanged(nameof(IsHomeVisible));
                }
            }
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

        #endregion

        public DelegateCommand HomeCommand
        {
            get { return new DelegateCommand(OnHome); }
        }

        private void Init()
        {
            MenuView = _navigator.GetMenuView();

            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Subscribe(OnNavigateView, ThreadOption.UIThread);
        }

        private IView _tempView;
        private void OnNavigateView(IView view)
        {
            _tempView = view;
            if (CurrentView != null)
            {
                CurrentView?.HideView(OnHideCompleted);
            }
            else
            {
                OnHideCompleted();
            }
            
            //IsMenuVisible = CurrentView.Previous != null;
            IsHomeVisible = !_navigator.IsFirstViewActive();
        }

        private void OnHideCompleted()
        {
            CurrentView = _tempView;
            CurrentView.ShowView();
        }

        private void OnHome()
        {
            _navigator.StartMenu();
        }
    }
}
