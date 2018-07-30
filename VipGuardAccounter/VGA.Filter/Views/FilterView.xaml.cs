using Common.Infrastructure.Interfaces.Views;
using VGA.Filter.ViewModels;
using System.Windows.Controls;

namespace VGA.Filter.Views
{
    public partial class FilterView : UserControl, IFilterView
    {
        public FilterView()
        {
            InitializeComponent();

            DataContext = new FilterViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
