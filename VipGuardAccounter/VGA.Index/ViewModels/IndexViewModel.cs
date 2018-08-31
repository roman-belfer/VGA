﻿using Common.Infrastructure;
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

namespace VGA.Index.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IBodyguardRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        private bool _isDataLoading;
        private ItemViewModel _selectedBodyguard;
        private ObservableCollection<ItemViewModel> _bodyguardCollection;

        public IndexViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
            _repository = Container.Resolve<IBodyguardRepository>();
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<SearchEvents.SearchBodyguardsEvent>().Subscribe(OnSearch, ThreadOption.UIThread);

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

        public ItemViewModel SelectedBodyguard
        {
            get { return _selectedBodyguard; }
            set
            {
                if (_selectedBodyguard != value)
                {
                    _selectedBodyguard = value;
                    OnPropertyChanged(nameof(SelectedBodyguard));
                }
            }
        }

        public ObservableCollection<ItemViewModel> BodyguardCollection
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

        public DelegateCommand ItemChangedCommand
        {
            get { return new DelegateCommand(OnItemChanged); }
        }

        public DelegateCommand<int?> DetailCommand
        {
            get { return new DelegateCommand<int?>(OnDetail); }
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
            BodyguardCollection.FirstOrDefault(x => x.ID == id).IsReadOnly = false;
        }

        private void OnDelete(uint? id)
        {
            var item = BodyguardCollection.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                BodyguardCollection.Remove(item);
            }
        }

        private void OnSearch(SearchBodyguardsParameters searchParams)
        {
            IsDataLoading = true;

            Task.Run(async () => await SearchAsync(searchParams));
        }

        private async Task SearchAsync(SearchBodyguardsParameters searchParams)
        {
            var bodyguardsCollection = await _repository.GetCollection(searchParams);
            var models = ItemViewModel.ConvertFromDto(bodyguardsCollection);
            BodyguardCollection = new ObservableCollection<ItemViewModel>(models);

            await Task.Delay(4000);

            IsDataLoading = false;
        }

        private void OnDetail(int? id)
        {
            if (id.HasValue)
            {
                _navigator.Detail();
                _eventAggregator.GetEvent<DataEvents.DetailEvent>().Publish(id.Value);
            }
        }

        private void OnItemChanged()
        {
            Task.Run(async () =>
            {
                var item = ItemViewModel.ConvertToDto(SelectedBodyguard);
                await _repository.Edit(item);
            });
        }
    }
}
