using Common.MVVM;
using Prism.Commands;
using System;
using System.Windows.Media.Imaging;

namespace VGA.StartMenu.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
    {
        private string _title;
        private bool _isEnabled;
        private BitmapSource _image;
        private Action _itemAction;

        public MenuItemViewModel(
            string title, string image, Action itemAction, bool isEnabled = true)
        {
            _title = title;
            _image = new BitmapImage(new Uri(image));
            _itemAction = itemAction;
            _isEnabled = isEnabled;
        }

        public DelegateCommand ItemCommand
        {
            get { return new DelegateCommand(_itemAction); }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public BitmapSource Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
    }
}
