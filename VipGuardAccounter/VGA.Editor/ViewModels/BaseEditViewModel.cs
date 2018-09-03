using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;

namespace VGA.Editor.ViewModels
{
    public abstract class BaseEditViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;

        private bool _isActive;

        public BaseEditViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        public DelegateCommand SaveCommand
        {
            get { return new DelegateCommand(OnSave); }
        }

        public DelegateCommand CancelCommand
        {
            get { return new DelegateCommand(OnCancel); }
        }

        public virtual void Edit(uint id) { IsActive = true; }
        public virtual void Create() { IsActive = true; }

        protected virtual void OnSave()
        {
            OnCancel();
        }

        private void OnCancel()
        {
            _navigator.Back();
            IsActive = false;
        }
    }
}
