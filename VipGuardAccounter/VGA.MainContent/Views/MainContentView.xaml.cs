using System.Windows;
using VGA.MainContent.ViewModels;

namespace VGA.MainContent.Views
{
    public partial class MainContentView : Window
    {
        public MainContentView()
        {
            InitializeComponent();

            DataContext = new MainContentViewModel();
        }
    }
}
