using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using Common.MVVM;
using Prism.Commands;

namespace VGA.Menu.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;

        private DelegateCommand _backCommand;

        public MenuViewModel()
        {
            _navigator = Container.Resolve<INavigator>();
        }

        public DelegateCommand BackCommand =>
            _backCommand ?? (_backCommand = new DelegateCommand(OnBack));

        private void OnBack()
        {
            _navigator.Back();
        }
    }
}
