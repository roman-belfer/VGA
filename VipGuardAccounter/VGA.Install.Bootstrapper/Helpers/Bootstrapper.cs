using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using Aurora.Install.Bootstrapper.Helpers;
using Aurora.Install.Bootstrapper.Models;
using Aurora.Install.Bootstrapper.ViewModels;
using Aurora.Install.Bootstrapper.Views;
using System;
using Aurora.Install.Bootstrapper.Common.Localizer;

namespace Aurora.Install.Bootstrapper
{
    public class Bootstrapper : BootstrapperApplication
    {
        protected override void Run()
        {
            ViewHelper.Init();

            BootstrapperModel.Instance.Bootstrapper = this;

            ProcessHelper.KillProcess("Aurora");

            //if (Environment.OSVersion.Version.Major < 10)
            //{
            //    MessageBox.Show("Application is available just on Windows 10 and higher!", "Warning!");
            //    ProcessHelper.KillProcess();
            //}

            Engine.Detect();

            if (Command.Display == Display.Embedded)
            {
                Engine.Plan(Command.Action);
                Engine.Apply(IntPtr.Zero);
            }
            else
            {
                var viewModel = new MainViewModel { CurrentViewModel = SelectViewModel.Instance };

                var view = new BootstrapperView();
                viewModel.Cancel += view.Close;
                view.DataContext = viewModel;
                view.ShowDialog();
            }

            Engine.Quit(0);
        }
    }
}