using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VGA.Orders.ViewModels;

namespace VGA.Orders.Views
{
    public partial class OrdersGridView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<ItemViewModel>),
                typeof(OrdersGridView), new PropertyMetadata());

        public ObservableCollection<ItemViewModel> ItemsSource
        {
            get { return (ObservableCollection<ItemViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty IsDataLoadingProperty =
            DependencyProperty.Register("IsDataLoading", typeof(bool),
                typeof(OrdersGridView), new PropertyMetadata());

        public bool IsDataLoading
        {
            get { return (bool)GetValue(IsDataLoadingProperty); }
            set { SetValue(IsDataLoadingProperty, value); }
        }

        public static readonly DependencyProperty DetailCommandProperty =
            DependencyProperty.Register("DetailCommand", typeof(DelegateCommand<uint?>),
                typeof(OrdersGridView), new PropertyMetadata());

        public DelegateCommand<uint?> DetailCommand
        {
            get { return (DelegateCommand<uint?>)GetValue(DetailCommandProperty); }
            set { SetValue(DetailCommandProperty, value); }
        }

        public OrdersGridView()
        {
            InitializeComponent();
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
