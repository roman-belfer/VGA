using Common.Infrastructure.Interfaces.Views;
using System.Windows.Controls;
using VGA.Detail.ViewModels;

namespace VGA.Detail.Views
{
    public partial class DetailView : UserControl, IDetailView
    {
        public DetailView()
        {
            InitializeComponent();

            DataContext = new DetailViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
