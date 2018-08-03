using Common.Infrastructure.Interfaces.Views;
using Common.MVVM;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Common.UI
{
    public abstract class BaseView<T> : UserControl where T : BaseViewModel
    {
        private Action _completeHideAction;

        private DoubleAnimation _showAnimation;
        private DoubleAnimation _hideAnimation;

        public BaseView()
        {
            DataContext = Activator.CreateInstance(typeof(T));
            Opacity = 0;

            _showAnimation = new DoubleAnimation()
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };

            _hideAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            _hideAnimation.Completed += OnHideCompleted;
        }
        
        public IView Previous { get; set; }
        public IView Next { get; set; }

        public void ShowView()
        {
            BeginAnimation(OpacityProperty, _showAnimation);
        }

        public void HideView(Action completeAction)
        {
            _completeHideAction = completeAction;

            BeginAnimation(OpacityProperty, _hideAnimation);
        }

        private void OnHideCompleted(object sender, EventArgs e)
        {
            _completeHideAction?.Invoke();
            _completeHideAction = null;
        }
    }
}
