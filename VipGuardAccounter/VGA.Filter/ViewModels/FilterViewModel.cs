using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Events;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Repositories;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;

namespace VGA.Filter.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        #region Fields

        private readonly INavigator _navigator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDictionaryRepository _dictionaryRepository;

        private List<ValueModel> _heightCollection;
        private List<ValueModel> _weightCollection;
        private List<ValueModel> _expirienceCollection;
        private List<ValueModel> _militaryCollection;
        private List<ValueModel> _handgunCollection;
        private List<ValueModel> _handgunBulletCollection;
        private List<ValueModel> _rifleCollection;
        private List<ValueModel> _rifleBulletCollection;

        #endregion

        public FilterViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;
            _dictionaryRepository = Container.Resolve<IDictionaryRepository>();

            InitCollections();
        }

        #region Properties

        public List<ValueModel> HeightCollection
        {
            get { return _heightCollection; }
            set
            {
                if (_heightCollection != value)
                {
                    _heightCollection = value;
                    OnPropertyChanged(nameof(HeightCollection));
                }
            }
        }

        public List<ValueModel> WeightCollection
        {
            get { return _weightCollection; }
            set
            {
                if (_weightCollection != value)
                {
                    _weightCollection = value;
                    OnPropertyChanged(nameof(WeightCollection));
                }
            }
        }

        public List<ValueModel> ExpirienceCollection
        {
            get { return _expirienceCollection; }
            set
            {
                if (_expirienceCollection != value)
                {
                    _expirienceCollection = value;
                    OnPropertyChanged(nameof(ExpirienceCollection));
                }
            }
        }

        public List<ValueModel> MilitaryCollection
        {
            get { return _militaryCollection; }
            set
            {
                if (_militaryCollection != value)
                {
                    _militaryCollection = value;
                    OnPropertyChanged(nameof(MilitaryCollection));
                }
            }
        }

        public List<ValueModel> HandgunCollection
        {
            get { return _handgunCollection; }
            set
            {
                if (_handgunCollection != value)
                {
                    _handgunCollection = value;
                    OnPropertyChanged(nameof(HandgunCollection));
                }
            }
        }

        public List<ValueModel> HandgunBulletCollection
        {
            get { return _handgunBulletCollection; }
            set
            {
                if (_handgunBulletCollection != value)
                {
                    _handgunBulletCollection = value;
                    OnPropertyChanged(nameof(HandgunBulletCollection));
                }
            }
        }

        public List<ValueModel> RifleCollection
        {
            get { return _rifleCollection; }
            set
            {
                if (_rifleCollection != value)
                {
                    _rifleCollection = value;
                    OnPropertyChanged(nameof(RifleCollection));
                }
            }
        }

        public List<ValueModel> RifleBulletCollection
        {
            get { return _rifleBulletCollection; }
            set
            {
                if (_rifleBulletCollection != value)
                {
                    _rifleBulletCollection = value;
                    OnPropertyChanged(nameof(RifleBulletCollection));
                }
            }
        }

        #endregion

        public DelegateCommand SearchCommand
        {
            get { return new DelegateCommand(OnSearch); }
        }

        private void OnSearch()
        {
            _navigator.Index();

            //TODO Set parameters for the search
            _eventAggregator.GetEvent<SearchEvents.SearchParameterEvent>().Publish(new SearchParameters());
        }

        private void InitCollections()
        {
            HeightCollection = _dictionaryRepository.GetHeightCollection().ToList();
            WeightCollection = _dictionaryRepository.GetWeightCollection().ToList();
            ExpirienceCollection = _dictionaryRepository.GetExpirienceCollection().ToList();
            MilitaryCollection = _dictionaryRepository.GetMilitaryCollection().ToList();
            HandgunCollection = _dictionaryRepository.GetHandgunCollection().ToList();
            HandgunBulletCollection = _dictionaryRepository.GetHandgunBulletCollection().ToList();
            RifleCollection = _dictionaryRepository.GetRifleCollection().ToList();
            RifleBulletCollection = _dictionaryRepository.GetRifleBulletCollection().ToList();
        }
    }
}
