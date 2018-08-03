using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using System;
using VGA.Detail.ViewModels;

namespace VGA.Detail.Views
{
    public partial class DetailView : BaseView<DetailViewModel>, IDetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }
    }
}
