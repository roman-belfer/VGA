using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.Menu.ViewModels;

namespace VGA.Menu.Views
{
    public partial class MenuView : BaseView<MenuViewModel>, IMenuView
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}
