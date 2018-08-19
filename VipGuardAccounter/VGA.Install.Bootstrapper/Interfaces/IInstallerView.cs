namespace VGA.Install.Bootstrapper.Interfaces
{
    public interface IInstallerView
    {
        bool SupportCancel { get; }
        bool SupportInstall { get; }
        IInstallerView NextView { get; }
        IInstallerView PrevView { get; }
        IInstallViewModel ViewModel { get; set; }
    }
}