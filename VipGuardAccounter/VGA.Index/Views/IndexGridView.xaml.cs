using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VGA.Index.Models;

namespace VGA.Index.Views
{
    public partial class IndexGridView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<IndexModel>),
                typeof(IndexGridView), new PropertyMetadata());

        public ObservableCollection<IndexModel> ItemsSource
        {
            get { return (ObservableCollection<IndexModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty IsDataLoadingProperty =
            DependencyProperty.Register("IsDataLoading", typeof(bool),
                typeof(IndexGridView), new PropertyMetadata());

        public bool IsDataLoading
        {
            get { return (bool)GetValue(IsDataLoadingProperty); }
            set { SetValue(IsDataLoadingProperty, value); }
        }

        public static readonly DependencyProperty DetailCommandProperty =
            DependencyProperty.Register("DetailCommand", typeof(DelegateCommand<uint?>),
                typeof(IndexGridView), new PropertyMetadata());

        public DelegateCommand<uint?> DetailCommand
        {
            get { return (DelegateCommand<uint?>)GetValue(DetailCommandProperty); }
            set { SetValue(DetailCommandProperty, value); }
        }

        public IndexGridView()
        {
            InitializeComponent();
        }
    }
}
