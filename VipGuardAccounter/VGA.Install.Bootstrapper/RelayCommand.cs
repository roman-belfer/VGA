using VGA.Install.Bootstrapper.ViewModels;
using System;
using System.Windows.Input;

namespace VGA.Install.Bootstrapper
{
    internal class RelayCommand : ViewModelBase, ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute)
            : this(execute, null)
        { }

        public bool CanExecute(object parameter)
        {
            var canExecute = true;
            if (_canExecute != null)
            {
                canExecute = _canExecute();
            }
            return canExecute;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}