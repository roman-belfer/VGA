using Common.Infrastructure.Interfaces.Views;
using System.Windows.Controls;
using VGA.Menu.ViewModels;

namespace VGA.Menu.Views
{
    public partial class MenuView : UserControl, IMenuView
    {
        public MenuView()
        {
            InitializeComponent();

            DataContext = new MenuViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
