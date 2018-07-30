using Common.Infrastructure.Interfaces.Views;
using System.Windows.Controls;
using VGA.Editor.ViewModels;

namespace VGA.Editor.Views
{
    public partial class EditView : UserControl, IEditView
    {
        public EditView()
        {
            InitializeComponent();

            DataContext = new EditViewModel();
        }

        public IView Previous { get; set; }
        public IView Next { get; set; }
    }
}
