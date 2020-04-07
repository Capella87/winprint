﻿using System;
using System.Diagnostics;
using WixSharp;

namespace WinPrintInstaller {
    class Install {
        public static readonly Guid UpgradeCode = new Guid("{0002A500-0000-0000-C000-000000000046}");
        public static readonly Guid ProductCode = new Guid("{0002A501-0000-0000-C000-000000000046}");

        static void Main() {
            const string sourceBaseDir = @"..\..\release";
            const string outDir = @"..\..\install";
            var versionFile = $"{sourceBaseDir}\\WinPrint.Core.dll";
            Debug.WriteLine($"version path: {versionFile}");
            var info = FileVersionInfo.GetVersionInfo(versionFile);

            var feature = new Feature(new Id("winprint"));

            var project = new Project(info.ProductName, new EnvironmentVariable("PATH", "[INSTALLDIR]") { Part = EnvVarPart.last }) {

                RegValues = new[] {
                    new RegValue(feature, RegistryHive.LocalMachine, $@"Software\{info.CompanyName}\{info.ProductName}", "Telemetry", "[TELEMETRY]") {
                        Win64 = true,
                        // https://github.com/oleg-shilo/wixsharp/issues/818#issuecomment-597058371
                        AttributesDefinition = "Type=integer"
                    }
                },

                Dirs = new[] {
                    new Dir(feature, $"%ProgramFiles%\\{info.CompanyName}\\{info.ProductName}",
                        new Files(@"*.dll"),
                        new Files(@"*.deps.json"),
                        new Files(@"*.runtimeconfig.json"),
                        new File(new Id("winprintgui_exe"), @"winprintgui.exe",
                            new FileShortcut("winprint", "INSTALLDIR") { AttributesDefinition = "Advertise=yes"} ),
                        new ExeFileShortcut("Uninstall winprint", "[System64Folder]msiexec.exe", "/x [ProductCode]")),
                    new Dir(feature, $"%AppData%\\{info.CompanyName}\\{info.ProductName}"),
                    new Dir(feature, $"%ProgramMenu%\\{info.CompanyName}\\{info.ProductName}",
                        new ExeFileShortcut("WinPrint", "[INSTALLDIR]winprintgui.exe", arguments: ""))
                 },

                //Binaries = new[] {
                //},

                //Actions = new[] {
                //    new InstalledFileAction("winprintgui_exe", "")
                //    {
                //        Step = Step.InstallFinalize,
                //        When = When.After,
                //        Return = Return.asyncNoWait,
                //        Execute = Execute.immediate,
                //        Impersonate = true,
                //        //Condition = Feature.BeingInstall(),
                //    }
                //},

                Properties = new[]{
                    //setting property to be used in install condition
                    new Property("ALLUSERS", "1"),
                    new Property("TELEMETRY", "1"),
                }
            };

            // See Core.Models.
            project.GUID = ProductCode;
            project.UpgradeCode = UpgradeCode;
            project.SourceBaseDir = sourceBaseDir;
            project.OutDir = outDir;

            project.Version = new Version(info.ProductVersion); //new Version("2.0.1.10040"); 
            project.MajorUpgrade = new MajorUpgrade {
                Schedule = UpgradeSchedule.afterInstallInitialize,
                AllowSameVersionUpgrades = true,
                DowngradeErrorMessage = "A later version of [ProductName] is already installed. Setup will now exit."
            };
            project.Platform = Platform.x64;

            //project.LicenceFile = "license.rtf";

            project.ControlPanelInfo.Comments = $"winprint by Charlie Kindel";
            project.ControlPanelInfo.Readme = "https://tig.github.io/winprint";
            project.ControlPanelInfo.HelpLink = "https://tig.github.io/winprint";
            project.ControlPanelInfo.UrlInfoAbout = "https://tig.github.io/winprint";
            project.ControlPanelInfo.UrlUpdateInfo = "https://tig.github.io/winprint";
            project.ControlPanelInfo.Manufacturer = info.CompanyName;
            project.ControlPanelInfo.InstallLocation = "[INSTALLDIR]";
            project.ControlPanelInfo.NoModify = true;
            //project.ControlPanelInfo.NoRepair = true,
            //project.ControlPanelInfo.NoRemove = true,
            //project.ControlPanelInfo.SystemComponent = true, //if set will not be shown in Control Panel

            project.PreserveTempFiles = true;

            //project.UI = WUI.WixUI_ProgressOnly;

            //project.RemoveDialogsBetween(NativeDialogs.WelcomeDlg, NativeDialogs.);

            //project.SetNetFxPrerequisite("NETFRAMEWORK20='#1'");

            project.EmbeddedUI = new EmbeddedAssembly(System.Reflection.Assembly.GetExecutingAssembly().Location);
            project.PreserveTempFiles = true;

            project.CAConfigFile = "CustomAction.config";
            project.BuildMsi();
        }
    }
}
