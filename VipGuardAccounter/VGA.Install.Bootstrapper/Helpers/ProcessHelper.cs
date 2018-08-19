using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace Aurora.Install.Bootstrapper.Helpers
{
    public static class ProcessHelper
    {
        private static readonly Process[] Processes;

        static ProcessHelper()
        {
            Processes = Process.GetProcesses();
        }

        public static void RunProcess(string folderPath, string processName)
        {
            var processPath = Path.Combine(folderPath, @processName);
            var processInfo = new ProcessStartInfo(processPath)
            {
                WorkingDirectory = folderPath
            };

            try
            {
                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void KillProcess(string processName)
        {
            var process = Processes.FirstOrDefault(x => x.ProcessName == @processName);
            process?.Kill();
        }

        // Kills current process
        public static void KillProcess()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void SetAutoLaunchProcess(string folderPath, string processName, string keyName)
        {
            var processPath = Path.Combine(folderPath, @processName);
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key == null) return;

                    key.DeleteValue(keyName, false);
                    key.SetValue(keyName, processPath);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void DeleteAutoLaunchProcess(string keyName)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key?.DeleteValue(keyName, false);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
