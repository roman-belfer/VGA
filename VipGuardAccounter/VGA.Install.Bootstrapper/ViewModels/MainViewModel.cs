using VGA.Install.Bootstrapper.Interfaces;
using System;

namespace VGA.Install.Bootstrapper.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public event Action Cancel;

        private IInstallViewModel _currentViewModel;
        public IInstallViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != null)
                {
                    _currentViewModel.ViewModelChanged -= CurrentViewModelChanged;
                    _currentViewModel.Canceled -= Canceled;
                }
                if (value != null)
                {
                    value.ViewModelChanged += CurrentViewModelChanged;
                    value.Canceled += Canceled;
                    value.Showed();
                }
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        private void CurrentViewModelChanged(IInstallViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        private void Canceled()
        {
            Cancel?.Invoke();
        }
    }
}