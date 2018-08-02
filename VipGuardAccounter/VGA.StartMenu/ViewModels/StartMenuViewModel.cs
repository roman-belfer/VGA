using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using System;
using System.Collections.ObjectModel;

namespace VGA.StartMenu.ViewModels
{
    public class StartMenuViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;

        private ObservableCollection<MenuItemViewModel> _menuItemsCollection;

        public StartMenuViewModel()
        {
            _navigator = Container.Resolve<INavigator>();

            InitCollection();
        }

        public ObservableCollection<MenuItemViewModel> MenuItemsCollection
        {
            get { return _menuItemsCollection; }
            set
            {
                if (_menuItemsCollection != value)
                {
                    _menuItemsCollection = value;
                    OnPropertyChanged(nameof(MenuItemsCollection));
                }
            }
        }

        private void InitCollection()
        {
            MenuItemsCollection = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel("Bodyguards", "pack://application:,,,/VGA.StartMenu;component/Resources/bodyguards.jpg", OnBodyguards),
                new MenuItemViewModel("Helicopters", "pack://application:,,,/VGA.StartMenu;component/Resources/helicopters.jpg", OnHelicopters, false),
                new MenuItemViewModel("Jets", "pack://application:,,,/VGA.StartMenu;component/Resources/jets.png", OnJets, false),
                new MenuItemViewModel("Auto", "pack://application:,,,/VGA.StartMenu;component/Resources/auto.jpg", OnAuto, false)
            };
        }

        private void OnAuto()
        {
            throw new NotImplementedException();
        }

        private void OnJets()
        {
            throw new NotImplementedException();
        }

        private void OnHelicopters()
        {
            throw new NotImplementedException();
        }

        private void OnBodyguards()
        {
            _navigator.Filter();
        }
    }
}
