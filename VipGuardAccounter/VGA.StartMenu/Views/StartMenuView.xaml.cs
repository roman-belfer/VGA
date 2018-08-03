using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.StartMenu.ViewModels;

namespace VGA.StartMenu.Views
{
    public partial class StartMenuView : BaseView<StartMenuViewModel>, IStartMenuView
    {
        public StartMenuView()
        {
            InitializeComponent();
        }
    }
}
