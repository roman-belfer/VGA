using VGA.Install.Bootstrapper.Helpers;
using VGA.Install.Bootstrapper.Interfaces;
using VGA.Install.Bootstrapper.Models;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace VGA.Install.Bootstrapper.ViewModels
{
    class SelectViewModel : ViewModelBase, IInstallViewModel
    {
        public event Action<IInstallViewModel> ViewModelChanged;

        public event Action Canceled;

        public event Action Finished;

        #region Properties

        private static SelectViewModel _instance;
        public static SelectViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SelectViewModel();
                return _instance;
            }
        }

        private BootstrapperModel _model;
        public BootstrapperModel Model
        {
            get { return _model; }
        }

        public bool IsInstalled
        {
            get { return _model.IsInstalled; }
        }

        public bool IsNewerVersionInstalled
        {
            get { return _model.IsNewerVersionInstalled; }
        }

        public string InstallFolderPath
        {
            get { return _model.ManufacturerFolderPath; }
            set
            {
                _model.ManufacturerFolderPath = value;
                OnPropertyChanged("InstallFolderPath");
            }
        }

        public IInstallerView View { get; private set; }

        public ICommand InstallCommand { get; private set; }

        public ICommand UnistallCommand { get; private set; }

        public ICommand RepairCommand { get; private set; }

        public ICommand SelectFolderCommand { get; private set; }

        #endregion

        public SelectViewModel()
        {
            _model = BootstrapperModel.Instance;

            InitializeCommands();
            View = ViewHelper.ResolveView(ViewHelper.SelectComponentsDlgName);
            View.ViewModel = this;
        }

        private void InitializeCommands()
        {
            InstallCommand = new RelayCommand(InstallCommand_Execute, InstallCommand_CanExecute) { Text = "Установить" };
            UnistallCommand = new RelayCommand(UnistallCommand_Execute, UnistallCommand_CanExecute) { Text = "Удалить" };
            RepairCommand = new RelayCommand(RepairCommand_Execute, RepairCommand_CanExecute) { Text = "Изменить" };
            SelectFolderCommand = new RelayCommand(SelectFolderCommand_Execute);
        }

        #region Command Executors

        private void InstallCommand_Execute()
        {
            if (IsNewerVersionInstalled)
            {
                ProcessHelper.KillProcess();
            }
            else
            {
                _model.PlanInstall();
                OnChangeViewModel(ProgressViewModel.Instance);
            }
        }
        private bool InstallCommand_CanExecute()
        {
            if (IsNewerVersionInstalled)
            {
                (InstallCommand as RelayCommand).Text = "OK";
            }
            else if (_model.IsPreviousVersionInstalled)
            {
                (InstallCommand as RelayCommand).Text = "Обновить";
            }

            return !IsInstalled;
        }


        private void UnistallCommand_Execute()
        {
            _model.PlanUnistall();
            OnChangeViewModel(ProgressViewModel.Instance);
        }
        private bool UnistallCommand_CanExecute()
        {
            return IsInstalled;
        }

        private void RepairCommand_Execute()
        {
            _model.PlanRepair();
            OnChangeViewModel(ProgressViewModel.Instance);
        }
        private bool RepairCommand_CanExecute()
        {
            return IsInstalled;
        }

        private void SelectFolderCommand_Execute()
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                InstallFolderPath = dlg.SelectedPath;
            }
        }

        #endregion

        private void OnChangeViewModel(IInstallViewModel viewModel)
        {
            ViewModelChanged?.Invoke(viewModel);
        }

        public void Showed()
        {
            OnPropertyChanged(string.Empty);
        }

        private void OnFinished()
        {
            Finished?.Invoke();
        }
    }
}