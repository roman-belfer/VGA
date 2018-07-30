using Common.Infrastructure.Interfaces.Views;
using System.Windows.Controls;
using VGA.Index.ViewModels;

namespace VGA.Index.Views
{
    public partial class IndexView : UserControl, IIndexView
    {
        public IndexView()
        {
            InitializeComponent();

            DataContext = new IndexViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
