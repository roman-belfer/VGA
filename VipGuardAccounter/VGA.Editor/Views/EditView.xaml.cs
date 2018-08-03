using System;
using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.Editor.ViewModels;

namespace VGA.Editor.Views
{
    public partial class EditView : BaseView<EditViewModel>, IEditView
    {
        public EditView()
        {
            InitializeComponent();
        }
    }
}
