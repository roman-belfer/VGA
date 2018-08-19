using System;
using System.ComponentModel;

namespace VGA.Install.Bootstrapper.Interfaces
{
    public interface IInstallViewModel : INotifyPropertyChanged
    {
        IInstallerView View { get; }
        event Action<IInstallViewModel> ViewModelChanged;
        event Action Canceled;
        void Showed();
    }
}