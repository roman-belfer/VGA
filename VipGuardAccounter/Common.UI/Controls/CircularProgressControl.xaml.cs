using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.UI.Controls
{
    public partial class CircularProgressControl : UserControl
    {
        public static readonly DependencyProperty ColorProperty = 
            DependencyProperty.Register("Color", typeof(Brush),
                typeof(CircularProgressControl), new PropertyMetadata());

        public new static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool),
                typeof(CircularProgressControl), new PropertyMetadata());

        public static readonly DependencyProperty LoadTextProperty =
            DependencyProperty.Register("LoadText", typeof(string),
                typeof(CircularProgressControl), new PropertyMetadata());

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public string LoadText
        {
            get { return (string)GetValue(LoadTextProperty); }
            set { SetValue(LoadTextProperty, value); }
        }

        public CircularProgressControl()
        {
            InitializeComponent();
        }
    }
}
