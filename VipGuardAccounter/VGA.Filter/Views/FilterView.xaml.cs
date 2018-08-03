using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.Filter.ViewModels;

namespace VGA.Filter.Views
{
    public partial class FilterView : BaseView<FilterViewModel>, IFilterView
    {
        public FilterView()
        {
            InitializeComponent();
        }
    }
}
