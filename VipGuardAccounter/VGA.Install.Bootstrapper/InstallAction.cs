using System;

namespace VGA.Install.Bootstrapper
{
    public abstract class InstallAction
    {
        public event Action<InstallAction, string> MessageChagned;
        public event Action<InstallAction, int?> ProgressChagned;

        public abstract string ActionDescription { get; }

        protected void OnMessageChanged(string message)
        {
            MessageChagned?.Invoke(this, message);
        }

        protected void OnProgressChanged(int? progress)
        {
            ProgressChagned?.Invoke(this, progress);
        }

        public abstract void Invoke();
    }
}