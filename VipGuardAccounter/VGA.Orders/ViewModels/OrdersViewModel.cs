using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace VGA.Orders.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        //private readonly IOrdersRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private bool _isDataLoading;
        private ObservableCollection<ItemViewModel> _ordersCollection;

        public OrdersViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            //_repository = Container.Resolve<IOrdersRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<SearchEvents.SearchOrdersEvent>().Subscribe(OnSearch, ThreadOption.UIThread);

            InitCollection();
        }

        public bool IsDataLoading
        {
            get { return _isDataLoading; }
            set
            {
                if (_isDataLoading != value)
                {
                    _isDataLoading = value;
                    OnPropertyChanged(nameof(IsDataLoading));
                }
            }
        }

        public ObservableCollection<ItemViewModel> OrdersCollection
        {
            get { return _ordersCollection; }
            set
            {
                if (_ordersCollection != value)
                {
                    _ordersCollection = value;
                    OnPropertyChanged(nameof(OrdersCollection));
                }
            }
        }

        public DelegateCommand<uint?> DetailCommand
        {
            get { return new DelegateCommand<uint?>(OnDetail); }
        }

        public DelegateCommand<uint?> EditCommand
        {
            get { return new DelegateCommand<uint?>(OnEdit); }
        }

        public DelegateCommand<uint?> DeleteCommand
        {
            get { return new DelegateCommand<uint?>(OnDelete); }
        }

        private void InitCollection()
        {
            OnSearch(null);
        }

        private void OnEdit(uint? id)
        {
           // OrdersCollection.FirstOrDefault(x => x.ID == id).IsReadOnly = false;
        }

        private void OnDelete(uint? id)
        {
            var item = OrdersCollection.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                OrdersCollection.Remove(item);
            }
        }

        private void OnSearch(SearchOrdersParameters searchParams)
        {
            IsDataLoading = true;

           // Task.Run(async () => await SearchAsync(searchParams));
        }

        private async Task SearchAsync(SearchOrdersParameters searchParams)
        {
            //var bodyguardsCollection = await _repository.GetBodyguardsCollection(searchParams);
            //var models = ItemViewModel.ConvertFromDto(bodyguardsCollection);
            //BodyguardCollection = new ObservableCollection<ItemViewModel>(models.OrderByDescending(x => x.Rate));

            await Task.Delay(4000);

            IsDataLoading = false;
        }

        private void OnDetail(uint? id)
        {
            //if (id.HasValue)
            //{
            //    _navigator.Detail();
            //    _eventAggregator.GetEvent<DataEvents.DetailEvent>().Publish(id.Value);
            //}
        }
    }
}
