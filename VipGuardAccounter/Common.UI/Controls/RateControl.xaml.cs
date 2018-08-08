using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Common.UI.Controls
{
    public partial class RateControl : UserControl
    {
        public RateControl()
        {
            InitializeComponent();

            RateCollection = new ObservableCollection<bool>();
            for (int i = 0; i < 5; i++) RateCollection.Add(false);
        }

        public static readonly DependencyProperty RateProperty =
            DependencyProperty.Register("Rate", typeof(int),
                typeof(RateControl), new PropertyMetadata(OnRateChanged));

        public static readonly DependencyProperty RateCollectionProperty =
            DependencyProperty.Register("RateCollection", typeof(ObservableCollection<bool>),
                typeof(RateControl), new PropertyMetadata());

        public int Rate
        {
            get { return (int)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }

        public ObservableCollection<bool> RateCollection
        {
            get { return (ObservableCollection<bool>)GetValue(RateCollectionProperty); }
            set { SetValue(RateCollectionProperty, value); }
        }

        private static void OnRateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rate = (int)e.NewValue;
            if (d is RateControl rateControl && rate > 0 && rate <= 5)
            {
                for (int i = 0; i < rate; i++) rateControl.RateCollection[i] = true;
            }
        }
    }
}
