using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Views;
using Common.MVVM;
using Prism.Events;

namespace VGA.MainContent.ViewModels
{
    public class MainContentViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IEventAggregator _eventAggregator;
        
        private bool _isViewChanged;
        private IView _tempView;
        private IView _currentView;

        public MainContentViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            MenuView = _navigator.GetMenuView();

            Init();
        }

        #region Properties

        public object MenuView { get; }

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

        private void Init()
        {
            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Subscribe(OnNavigateView, ThreadOption.UIThread);
        }

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
