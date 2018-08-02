using Common.Infrastructure.Interfaces.Views;
using System.Windows.Controls;
using VGA.StartMenu.ViewModels;

namespace VGA.StartMenu.Views
{
    public partial class StartMenuView : UserControl, IStartMenuView
    {
        public StartMenuView()
        {
            InitializeComponent();

            DataContext = new StartMenuViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
