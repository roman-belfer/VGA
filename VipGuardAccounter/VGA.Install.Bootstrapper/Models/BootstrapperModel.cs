using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace Aurora.Install.Bootstrapper.Models
{
    internal class BootstrapperModel
    {
        #region Properties

        public bool ForUninstall { get; set; }

        public bool IsInstalled { get; private set; }

        public bool IsCanceled { get; private set; }

        public bool IsNewerVersionInstalled { get; private set; }

        public bool IsPreviousVersionInstalled { get; private set; }

        public X509Certificate2 Certificate { get; set; }

        public ICollection<InstallAction> AdditionalActions { get; private set; }

        private static BootstrapperModel _instance;
        public static BootstrapperModel Instance => _instance ?? (_instance = new BootstrapperModel());

        private BootstrapperApplication _bootstrapper;
        public BootstrapperApplication Bootstrapper
        {
            get { return _bootstrapper; }
            set
            {
                if (_bootstrapper != null)
                {
                    _bootstrapper.DetectBegin -= OnDetectBegin;
                    _bootstrapper.Error -= OnBootstrapperError;
                    _bootstrapper.DetectRelatedBundle -= OnDetectRelatedBundle;
                }

                _bootstrapper = value;

                if (_bootstrapper == null) return;

                _bootstrapper.DetectBegin += OnDetectBegin;
                _bootstrapper.Error += OnBootstrapperError;
                _bootstrapper.DetectRelatedBundle += OnDetectRelatedBundle;
            }
        }

        public string ManufacturerFolderPath
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("BurnVariable")
                    ? Bootstrapper.Engine.FormatString("[BurnVariable]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("BurnVariable"))
                {
                    Bootstrapper.Engine.StringVariables["BurnVariable"] = value;
                }
            }
        }

        public string PathToImages
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("PathToImages")
                    ? Bootstrapper.Engine.FormatString("[PathToImages]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("PathToImages"))
                {
                    Bootstrapper.Engine.StringVariables["PathToImages"] = value;
                }
            }
        }

        public string PathToWorkingDirectory
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("PathToWorkingDirectory")
                    ? Bootstrapper.Engine.FormatString("[PathToWorkingDirectory]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("PathToWorkingDirectory"))
                {
                    Bootstrapper.Engine.StringVariables["PathToWorkingDirectory"] = value;
                }
            }
        }

        public string ServerAddress
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("ServerAddress")
                    ? Bootstrapper.Engine.FormatString("[ServerAddress]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("ServerAddress"))
                {
                    Bootstrapper.Engine.StringVariables["ServerAddress"] = value;
                }
            }
        }

        public string IdentityDnsValue
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("IdentityDnsValue")
                    ? Bootstrapper.Engine.FormatString("[IdentityDnsValue]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("IdentityDnsValue"))
                {
                    Bootstrapper.Engine.StringVariables["IdentityDnsValue"] = value;
                }
            }
        }

        public string PathToClientImages
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("PathToClientImages")
                    ? Bootstrapper.Engine.FormatString("[PathToClientImages]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("PathToClientImages"))
                {
                    Bootstrapper.Engine.StringVariables["PathToClientImages"] = value;
                }
            }
        }

        public string PathToClientWorkingDirectory
        {
            get
            {
                return Bootstrapper.Engine.StringVariables.Contains("PathToClientWorkingDirectory")
                    ? Bootstrapper.Engine.FormatString("[PathToClientWorkingDirectory]") : string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                if (Bootstrapper.Engine.StringVariables.Contains("PathToClientWorkingDirectory"))
                {
                    Bootstrapper.Engine.StringVariables["PathToClientWorkingDirectory"] = value;
                }
            }
        }

        public string ApplicationName => Bootstrapper.Engine.StringVariables["ApplicationName"];

        #endregion

        private BootstrapperModel()
        {
            AdditionalActions = new List<InstallAction>();
        }

        #region Methods

        public void InstallCertificate(X509Certificate2 certificate, X509Store store)
        {
            if (certificate == null) return;

            store.Open(OpenFlags.ReadWrite);

            var collection = store.Certificates.Find(X509FindType.FindByThumbprint, certificate.Thumbprint, false);
            if (collection.Count == 0)
            {
                store.Add(certificate);
            }

            store.Close();
        }

        public void PlanInstall()
        {
            Bootstrapper.Engine.Plan(LaunchAction.Install);
        }

        public void PlanUnistall()
        {
            ForUninstall = true;
            Bootstrapper.Engine.Plan(LaunchAction.Uninstall);
        }

        public void PlanRepair()
        {
            Bootstrapper.Engine.Plan(LaunchAction.Repair);
        }

        public void ApplyPlan()
        {
            Bootstrapper.Engine.Apply(IntPtr.Zero);
        }

        private void OnBootstrapperError(object sender, ErrorEventArgs e)
        {
            IsCanceled = true;
        }

        private void OnDetectRelatedBundle(object sender, DetectRelatedBundleEventArgs e)
        {
            IsPreviousVersionInstalled = true;
            
            if (e.Version.CompareTo(Assembly.GetEntryAssembly().GetName().Version) >= 0)
            {
                IsNewerVersionInstalled = true;
            }
        }

        private void OnDetectBegin(object sender, DetectBeginEventArgs e)
        {
            IsInstalled = e.Installed;
        }

        #endregion
    }
}