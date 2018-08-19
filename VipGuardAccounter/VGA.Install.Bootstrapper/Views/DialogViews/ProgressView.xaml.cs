using Aurora.Install.Bootstrapper.Helpers;
using Aurora.Install.Bootstrapper.Interfaces;

namespace Aurora.Install.Bootstrapper.Views.DialogViews
{
    /// <summary>
    /// Interaction logic for ProgressView.xaml
    /// </summary>
    public partial class ProgressView : IInstallerView
    {
        public IInstallerView NextView => ViewHelper.ResolveView(ViewHelper.CompleteDlgName);

        public IInstallerView PrevView => ViewHelper.ResolveView(ViewHelper.SelectComponentsDlgName);

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
                DataContext = value;
            }
        }

        public ProgressView()
        {
            InitializeComponent();
        }
    }
}