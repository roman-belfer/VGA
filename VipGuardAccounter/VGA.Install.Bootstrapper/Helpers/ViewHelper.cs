using Aurora.Install.Bootstrapper.Interfaces;
using Aurora.Install.Bootstrapper.Views.DialogViews;
using System.Collections.Generic;

namespace Aurora.Install.Bootstrapper.Helpers
{
    internal static class ViewHelper
    {
        private static Dictionary<string, IInstallerView> _views;

        public const string SelectComponentsDlgName = "SelectComponentsDlgName";
        public const string InstallDlgName = "InstallDlgName";
        public const string CompleteDlgName = "CompleteDlgName";

        public static void Init()
        {
            _views = new Dictionary<string, IInstallerView>();
            RegisterView(SelectComponentsDlgName, new SelectView());
            RegisterView(InstallDlgName, new ProgressView());
            RegisterView(CompleteDlgName, new CompleteView());
        }

        public static void RegisterView(string name, IInstallerView view)
        {
            if (!_views.ContainsKey(name))
            {
                _views.Add(name, view);
            }
        }

        public static IInstallerView ResolveView(string name)
        {
            IInstallerView result;

            _views.TryGetValue(name, out result);

            return result;
        }

    }
}
