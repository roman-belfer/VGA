using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using VGA.Index.Models;

namespace VGA.Index.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<IndexModel> _bodyguardCollection;

        private DelegateCommand<uint?> _detailCommand;

        public IndexViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _repository = Container.Resolve<IBodyguardRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<SearchEvents.SearchParameterEvent>().Subscribe(OnSearch, ThreadOption.UIThread);
        }

        public ObservableCollection<IndexModel> BodyguardCollection
        {
            get { return _bodyguardCollection; }
            set
            {
                if (_bodyguardCollection != value)
                {
                    _bodyguardCollection = value;
                    OnPropertyChanged(nameof(BodyguardCollection));
                }
            }
        }

        public DelegateCommand<uint?> DetailCommand =>
            _detailCommand ?? (_detailCommand = new DelegateCommand<uint?>(OnDetail));

        private void OnSearch(SearchParameters searchParams)
        {
            var bodyguardsCollection = _repository.GetBodyguardsCollection(searchParams);
            var models = IndexModel.ConvertFromDto(bodyguardsCollection);
            BodyguardCollection = new ObservableCollection<IndexModel>(models.OrderByDescending(x => x.Rate));
        }

        private void OnDetail(uint? id)
        {
            if (id.HasValue)
            {
                _navigator.Detail();
                _eventAggregator.GetEvent<DataEvents.DetailEvent>().Publish(id.Value);
            }
        }
    }
}
