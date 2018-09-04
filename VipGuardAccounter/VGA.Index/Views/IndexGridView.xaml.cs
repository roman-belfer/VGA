using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VGA.Index.ViewModels;

namespace VGA.Index.Views
{
    public partial class IndexGridView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<ItemViewModel>),
                typeof(IndexGridView), new PropertyMetadata());

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(ItemViewModel),
                typeof(IndexGridView), new PropertyMetadata());

        public static readonly DependencyProperty IsDataLoadingProperty =
            DependencyProperty.Register("IsDataLoading", typeof(bool),
                typeof(IndexGridView), new PropertyMetadata());

        public ObservableCollection<ItemViewModel> ItemsSource
        {
            get { return (ObservableCollection<ItemViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ItemViewModel SelectedItem
        {
            get { return (ItemViewModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

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
