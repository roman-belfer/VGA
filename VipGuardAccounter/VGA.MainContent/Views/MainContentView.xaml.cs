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

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal 
                        ? WindowState.Maximized 
                        : WindowState.Normal;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
