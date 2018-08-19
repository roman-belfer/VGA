using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using VGA.Install.Bootstrapper.Helpers;
using VGA.Install.Bootstrapper.Models;
using VGA.Install.Bootstrapper.ViewModels;
using VGA.Install.Bootstrapper.Views;
using System;

namespace VGA.Install.Bootstrapper
{
    public class Bootstrapper : BootstrapperApplication
    {
        protected override void Run()
        {
            ViewHelper.Init();

            BootstrapperModel.Instance.Bootstrapper = this;

            ProcessHelper.KillProcess("VipSky");

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