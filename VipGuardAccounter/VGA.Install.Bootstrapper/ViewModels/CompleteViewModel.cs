using Aurora.Install.Bootstrapper.Helpers;
using Aurora.Install.Bootstrapper.Interfaces;
using Aurora.Install.Bootstrapper.Models;
using System;
using System.IO;
using System.Windows.Input;

namespace Aurora.Install.Bootstrapper.ViewModels
{
    class CompleteViewModel : ViewModelBase, IInstallViewModel
    {
        private static CompleteViewModel _instance;
        public static CompleteViewModel Instance => _instance ?? (_instance = new CompleteViewModel());

        public bool Run { get; set; }

        public bool CanRun => !BootstrapperModel.Instance.ForUninstall && !IsCanceled;

        public bool IsCanceled => BootstrapperModel.Instance.IsCanceled;

        public IInstallerView View { get; }

        public event Action<IInstallViewModel> ViewModelChanged;

        public event Action Canceled;

        public ICommand FinishCommand { get; private set; }

        private CompleteViewModel()
        {
            FinishCommand = new RelayCommand(FinishCommand_Execute);
            Run = true;
            View = ViewHelper.ResolveView(ViewHelper.CompleteDlgName);
            View.ViewModel = this;
        }

        private void FinishCommand_Execute()
        {
            if (!IsCanceled)
            {
                TryRunApplication();
            }

            Canceled?.Invoke();
        }

        private void TryRunApplication()
        {
            var manufacturerFolder = BootstrapperModel.Instance.ManufacturerFolderPath;
            var clientFolderPath = Path.Combine(manufacturerFolder, BootstrapperModel.Instance.ApplicationName);

            if (!CanRun) return;

            if (Run)
            {
                ProcessHelper.RunProcess(clientFolderPath, "Aurora HDR 2018.exe");
            }
        }
        
        public void Showed()
        { }
    }
}