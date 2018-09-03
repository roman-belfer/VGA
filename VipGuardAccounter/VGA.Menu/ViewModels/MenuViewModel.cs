using System;
using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Enums;
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

        private bool _isSearchEnabled;
        private bool _isHomeEnabled;
        private bool _isEmployeesEnabled;
        private bool _isOrdersEnabled;

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

        public bool IsSearchEnabled
        {
            get { return _isSearchEnabled; }
            set
            {
                if (_isSearchEnabled != value)
                {
                    _isSearchEnabled = value;
                    OnPropertyChanged(nameof(IsSearchEnabled));
                }
            }
        }

        public bool IsEmployeesEnabled
        {
            get { return _isEmployeesEnabled; }
            set
            {
                if (_isEmployeesEnabled != value)
                {
                    _isEmployeesEnabled = value;
                    OnPropertyChanged(nameof(IsEmployeesEnabled));
                }
            }
        }

        public bool IsOrdersEnabled
        {
            get { return _isOrdersEnabled; }
            set
            {
                if (_isOrdersEnabled != value)
                {
                    _isOrdersEnabled = value;
                    OnPropertyChanged(nameof(IsOrdersEnabled));
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

        public DelegateCommand EmployeesCommand
        {
            get { return new DelegateCommand(OnEmployees); }
        }

        public DelegateCommand OrdersCommand
        {
            get { return new DelegateCommand(OnOrders); }
        }

        public DelegateCommand SearchCommand
        {
            get { return new DelegateCommand(OnSearch); }
        }

        public DelegateCommand CreateEmployeeCommand
        {
            get { return new DelegateCommand(OnCreateEmployee); }
        }

        public DelegateCommand CreateOrderCommand
        {
            get { return new DelegateCommand(OnCreateOrder); }
        }

        private void Init()
        {
            _eventAggregator.GetEvent<NavigationEvents.NavigateViewEvent>().Subscribe(OnNavigateView, ThreadOption.UIThread);
        }

        private void OnNavigateView(IView view)
        {
            IsHomeEnabled = !_navigator.IsFirstViewActive();
            IsSearchEnabled = _navigator.IsSearchAvailable();
            IsEmployeesEnabled = _navigator.IsOrdersActive();
            IsOrdersEnabled = _navigator.IsEmployeesActive();
        }

        private void OnHome()
        {
            _navigator.StartMenu();
        }

        private void OnBack()
        {
            _navigator.Back();
        }

        private void OnEmployees()
        {
            _navigator.Index();
        }

        private void OnOrders()
        {
            _navigator.Orders();
        }

        private void OnSearch()
        {
            _navigator.Filter();
        }

        private void OnCreateEmployee()
        {
            _navigator.Edit();

            _eventAggregator.GetEvent<DataEvents.EditEvent>().Publish(new EditModel(EditModeEnum.NewEmployee));
        }

        private void OnCreateOrder()
        {
            _navigator.Edit();

            _eventAggregator.GetEvent<DataEvents.EditEvent>().Publish(new EditModel(EditModeEnum.NewOrder));
        }
    }
}
