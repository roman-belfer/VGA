using VGA.Install.Bootstrapper.Interfaces;
using System;

namespace VGA.Install.Bootstrapper.Views.DialogViews
{
    /// <summary>
    /// Interaction logic for CompleteView.xaml
    /// </summary>
    public partial class CompleteView : IInstallerView
    {
        public IInstallerView NextView => null;

        public IInstallerView PrevView => null;

        public bool SupportCancel => false;

        public bool SupportInstall => false;

        public IInstallViewModel ViewModel
        {
            get
            {
                return DataContext as IInstallViewModel;
            }
            set
            {
                Dispatcher.Invoke(new Action(() => DataContext = value));
            }
        }

        public CompleteView()
        {
            InitializeComponent();
        }
    }
}