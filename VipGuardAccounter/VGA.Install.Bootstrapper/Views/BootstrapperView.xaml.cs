using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Aurora.Install.Bootstrapper.Views
{
    /// <summary>
    /// Interaction logic for BootstrapperView.xaml
    /// </summary>
    public partial class BootstrapperView
    {
        public BootstrapperView()
        {
            InitializeComponent();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}