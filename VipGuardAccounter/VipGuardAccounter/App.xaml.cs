using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Interfaces.Repositories;
using Common.Infrastructure.Interfaces.Views;
using Common.Navigation;
using Common.Repository;
using System.Windows;
using VGA.Detail.Views;
using VGA.Editor.Views;
using VGA.Filter.Views;
using VGA.Index.Views;
using VGA.MainContent;
using VGA.Menu.Views;
using VGA.Orders.Views;
using VGA.StartMenu.Views;

namespace Vipsky
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Configure();

            Container.Resolve<INavigator>().Launch();
        }

        private void Configure()
        {
            Container.Register<INavigator, Navigator>();
            Container.Register<IMainModule, MainModule>();

            Container.Register<IDictionaryRepository, DictionaryRepository>();
            Container.Register<IBodyguardRepository, BodyguardRepository>();
            Container.Register<IOrdersRepository, OrdersRepository>();

            Container.Register<IDetailView, DetailView>();
            Container.Register<IFilterView, FilterView>();
            Container.Register<IIndexView, IndexView>();
            Container.Register<IMenuView, MenuView>();
            Container.Register<IEditView, EditView>();
            Container.Register<IStartMenuView, StartMenuView>();
            Container.Register<IOrdersView, OrdersView>();
        }
    }
}
