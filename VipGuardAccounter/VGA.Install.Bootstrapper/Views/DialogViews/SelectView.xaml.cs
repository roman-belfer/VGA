using VGA.Install.Bootstrapper.Helpers;
using VGA.Install.Bootstrapper.Interfaces;

namespace VGA.Install.Bootstrapper.Views.DialogViews
{
    /// <summary>
    /// Interaction logic for SelectView.xaml
    /// </summary>
    public partial class SelectView : IInstallerView
    {
        public IInstallerView NextView => ViewHelper.ResolveView(ViewHelper.InstallDlgName);

        public IInstallerView PrevView => null;

        public bool SupportCancel => true;

        public bool SupportInstall => true;

        public IInstallViewModel ViewModel
        {
            get
            {
                return DataContext as IInstallViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        public SelectView()
        {
            InitializeComponent();
        }
    }
}