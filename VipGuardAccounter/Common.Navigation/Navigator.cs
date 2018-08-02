using Common.Infrastructure;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Views;
using Prism.Events;

namespace Common.Navigation
{
    public class Navigator : INavigator
    {
        private readonly IEventAggregator _eventAggregator;

        private IView _currentView;

        private IView _startMenuView;
        private IView _filterView;
        private IView _indexView;
        private IView _detailView;
        private IView _editView;

        public Navigator()
        {
            _eventAggregator = EventContainer.EventInstance.EventAggregator;
        }

        public void Launch()
        {
            LaunchMain();

            LaunchFirstView();
        }

        public void StartMenu()
        {
            _startMenuView = _startMenuView ?? Resolve<IStartMenuView>();

            UpdateView(_startMenuView);

            RaiseViewChanged();
        }

        public void Index()
        {
            _indexView = _indexView ?? Resolve<IIndexView>();

            UpdateView(_indexView);

            RaiseViewChanged();
        }

        public void Filter()
        {
            _filterView = _filterView ?? Resolve<IFilterView>();

            UpdateView(_filterView);

            RaiseViewChanged();
        }

        public void Detail()
        {
            _detailView = _detailView ?? Resolve<IDetailView>();

            UpdateView(_detailView);

            RaiseViewChanged();
        }

        public void Edit()
        {
            _editView = _editView ?? Resolve<IEditView>();

            UpdateView(_editView);

            RaiseViewChanged();
        }

        public void Back()
        {
            _currentView = _currentView?.Previous;

            RaiseViewChanged();
        }

        public object GetMenuView()
        {
            return Resolve<IMenuView>();
        }

        public bool IsFirstViewActive()
        {
            return _currentView?.Previous != null;
        }

        private void RaiseViewChanged()
        {
            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Publish(_currentView);
        }

        private void UpdateView(IView view)
        {
            view.Previous = _currentView;
            _currentView = view;
        }

        private void LaunchFirstView()
        {
            StartMenu();
        }

        private void LaunchMain()
        {
            Launch<IMainModule>();
        }

        private void Launch<T>() where T : IModule
        {
            Resolve<T>()?.Launch();
        }

        private T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
