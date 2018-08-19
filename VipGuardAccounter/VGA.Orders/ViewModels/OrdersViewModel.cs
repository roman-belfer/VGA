using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Repositories;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace VGA.Orders.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IOrdersRepository _repository;
        private readonly IBodyguardRepository _bodyguardsRepository;
        private readonly IEventAggregator _eventAggregator;

        private bool _isDataLoading;
        private ObservableCollection<ItemViewModel> _ordersCollection;

        public OrdersViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _repository = Container.Resolve<IOrdersRepository>();
            _bodyguardsRepository = Container.Resolve<IBodyguardRepository>();
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

            Task.Run(async () => await SearchAsync(searchParams));
        }

        private async Task SearchAsync(SearchOrdersParameters searchParams)
        {
            var ordersCollection = await _repository.GetOrdersCollection(searchParams);
            var bodyguards = await _bodyguardsRepository.GetBodyguardsCollection(null);
            var models = ItemViewModel.ConvertFromDto(ordersCollection, bodyguards.ToDictionary(o => o.ID, o => o.FullName));
            OrdersCollection = new ObservableCollection<ItemViewModel>(models);

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
