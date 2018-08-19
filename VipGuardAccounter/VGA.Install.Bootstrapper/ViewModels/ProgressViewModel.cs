using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using Aurora.Install.Bootstrapper.Helpers;
using Aurora.Install.Bootstrapper.Interfaces;
using Aurora.Install.Bootstrapper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Aurora.Install.Bootstrapper.ViewModels
{
    class ProgressViewModel : ViewModelBase, IInstallViewModel
    {
        #region Fields

        private Dictionary<string, int> _executingPackageOrderIndex;

        private int _progressPhases;
        private int _progress;
        private int _cacheProgress;
        private int _executeProgress;
        private string _message;

        #endregion

        #region Properties

        #region Singleton

        private static ProgressViewModel _instance;
        public static ProgressViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProgressViewModel();
                return _instance;
            }
        }

        #endregion

        public IInstallerView View { get; private set; }

        public event Action<IInstallViewModel> ViewModelChanged;

        public event Action Canceled;

        private bool _isProgressIndeterminate;
        public bool IsProgressIndeterminate
        {
            get { return _isProgressIndeterminate; }
            set
            {
                _isProgressIndeterminate = value;
                OnPropertyChanged("IsProgressIndeterminate");
            }
        }

        public bool ProgressEnabled
        {
            get { return true; }
        }

        public int Progress
        {
            get
            {
                return this._progress;
            }

            set
            {
                if (this._progress != value)
                {
                    this._progress = value;
                    OnPropertyChanged("Progress");
                }
            }
        }

        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                if (this._message != value)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        #endregion

        private ProgressViewModel()
        {
            IsProgressIndeterminate = true;
            Message = "Initializing...";

            this._executingPackageOrderIndex = new Dictionary<string, int>();
            BootstrapperModel.Instance.Bootstrapper.ExecuteMsiMessage += this.ExecuteMsiMessage;
            BootstrapperModel.Instance.Bootstrapper.ExecuteProgress += this.ApplyExecuteProgress;
            BootstrapperModel.Instance.Bootstrapper.PlanBegin += this.PlanBegin;
            BootstrapperModel.Instance.Bootstrapper.PlanPackageComplete += this.PlanPackageComplete;
            BootstrapperModel.Instance.Bootstrapper.Progress += this.ApplyProgress;
            BootstrapperModel.Instance.Bootstrapper.CacheAcquireProgress += this.CacheAcquireProgress;
            BootstrapperModel.Instance.Bootstrapper.CacheComplete += this.CacheComplete;
            BootstrapperModel.Instance.Bootstrapper.ApplyComplete += this.ApplyComplete;
            View = ViewHelper.ResolveView(ViewHelper.InstallDlgName);
            View.ViewModel = this;
        }

        #region Methods

        private void AfterCompleteAction()
        {
            foreach (InstallAction action in BootstrapperModel.Instance.AdditionalActions)
            {
                try
                {
                    action.MessageChagned += InstallAction_MessageChanged;
                    action.ProgressChagned += InstallAction_ProgressChagned;

                    action.Invoke();

                    action.MessageChagned -= InstallAction_MessageChanged;
                    action.ProgressChagned -= InstallAction_ProgressChagned;
                }
                catch (Exception exc)
                {
                    string errorMessage = FillStringFromException(exc, null);
                    BootstrapperModel.Instance.Bootstrapper.Engine.Log(LogLevel.Error, errorMessage);
                }
            }

        }

        private string FillStringFromException(Exception exc, string parent)
        {
            string result = "";
            if (exc == null)
            {
                return result;
            }

            if (parent == null)
            {
                result += DateTime.Now + "\n";

                result += "\tStack Trace:\n" + exc.StackTrace;

                result += "\n\n\tMessage:\n\t\t";

                result += exc.Message + "\n";

                parent = "\t";
            }
            else
            {
                result += parent + "\t" + exc.Message + "\n";
            }

            result += FillStringFromException(exc.InnerException, (parent ?? "") + "\t");

            return result;
        }

        private void OnCanceled()
        {
            Canceled?.Invoke();
        }

        public void Showed()
        {
            BootstrapperModel.Instance.ApplyPlan();
            OnPropertyChanged("ProgressEnabled");
        }

        #endregion

        #region Event Handlers

        private void InstallAction_ProgressChagned(InstallAction sender, int? progress)
        {
            if (progress.HasValue)
            {
                Progress = progress.Value;
                IsProgressIndeterminate = false;
            }
            else
                IsProgressIndeterminate = true;
        }

        private void InstallAction_MessageChanged(InstallAction sender, string message)
        {
            Message = message;
        }

        private void PlanBegin(object sender, PlanBeginEventArgs e)
        {
            lock (this)
            {
                this._progressPhases = 1;
                this._executingPackageOrderIndex.Clear();
            }
        }

        private void PlanPackageComplete(object sender, PlanPackageCompleteEventArgs e)
        {
            if (ActionState.None != e.Execute)
            {
                lock (this)
                {
                    Debug.Assert(!this._executingPackageOrderIndex.ContainsKey(e.PackageId));
                    this._executingPackageOrderIndex.Add(e.PackageId, this._executingPackageOrderIndex.Count);
                }
            }
        }

        private void ExecuteMsiMessage(object sender, ExecuteMsiMessageEventArgs e)
        {
            lock (this)
            {
                if (e.MessageType == InstallMessage.ActionStart)
                {
                    this.Message = e.Message;
                }

                e.Result = Result.Ok;
            }
        }

        private void ApplyProgress(object sender, ProgressEventArgs e)
        {
            lock (this)
            {
                e.Result = Result.Ok;
            }
        }

        private void CacheAcquireProgress(object sender, CacheAcquireProgressEventArgs e)
        {
            lock (this)
            {
                this._cacheProgress = e.OverallPercentage;
                if (_progressPhases != 0)
                {
                    this.Progress = (this._cacheProgress + this._executeProgress) / this._progressPhases;
                    IsProgressIndeterminate = false;
                }
                e.Result = Result.Ok;
            }
        }

        private void CacheComplete(object sender, CacheCompleteEventArgs e)
        {
            lock (this)
            {
                this._cacheProgress = 100;
                if (_progressPhases != 0)
                {
                    this.Progress = (this._cacheProgress + this._executeProgress) / this._progressPhases;
                    IsProgressIndeterminate = false;
                }
            }
        }

        private void ApplyExecuteProgress(object sender, ExecuteProgressEventArgs e)
        {
            lock (this)
            {

                this._executeProgress = e.OverallPercentage;
                this.Progress = (this._cacheProgress + this._executeProgress) / 2;
                IsProgressIndeterminate = false;

                if (BootstrapperModel.Instance.Bootstrapper.Command.Display == Display.Embedded)
                {
                    BootstrapperModel.Instance.Bootstrapper.Engine.SendEmbeddedProgress(e.ProgressPercentage, this.Progress);
                }

                e.Result = Result.Ok;
            }
        }

        private void ApplyComplete(object sender, ApplyCompleteEventArgs e)
        {
            AfterCompleteAction();
            ViewModelChanged?.Invoke(CompleteViewModel.Instance);
        }

        #endregion
    }
}