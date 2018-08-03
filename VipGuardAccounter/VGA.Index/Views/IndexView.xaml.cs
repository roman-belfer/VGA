using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.Index.ViewModels;

namespace VGA.Index.Views
{
    public partial class IndexView : BaseView<IndexViewModel>, IIndexView
    {
        public IndexView()
        {
            InitializeComponent();            
        }
    }
}
