using IWshRuntimeLibrary;
using Squirrel;
using System;
using System.Deployment.Application;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TesterUtil
{
    internal static class Program
    {
        public const int WM_SHOWFIRSTINSTANCE = 0x0400;
        private static Mutex mutex;
        public const int SW_RESTORE = 9;

        public static readonly Version AppVersion = new Version(1, 0, 21, 0);
        public static string STR_FORMNAME = "";

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void CreateShortcutIfMissing()
        {
            string shortcutName = "Clotho Master.lnk";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, shortcutName);

            if (System.IO.File.Exists(shortcutPath))
                return;

            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string appExe;

            if (ApplicationDeployment.IsNetworkDeployed &&
                AppDomain.CurrentDomain.SetupInformation?.ActivationArguments?.ActivationData != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length > 0)
            {
                appExe = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
            }
            else
            {
                appExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
            }

            if (string.IsNullOrEmpty(appExe))
                appExe = appPath;

            WshShell shell = new WshShell();
            object shortcutObj = shell.CreateShortcut(shortcutPath);
            IWshShortcut shortcut = (IWshShortcut)shortcutObj;
            //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = appExe;
            shortcut.WorkingDirectory = Path.GetDirectoryName(appExe);
            shortcut.Description = "Clotho Master";
            shortcut.Save();
        }

        public static string GetClickOnceVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ToString();
            }
            else
            {
                return AppVersion.ToString();
            }
        }

        [STAThread]
        private static void Main()
        {
            //CreateShortcutIfMissing();
            STR_FORMNAME = $"WSD Tester Utilization v.{AppVersion.ToString()} (Auto IP Address Update)";

            bool isNewInstance;
            mutex = new Mutex(true, "UniqueProgramMutexName", out isNewInstance);

            if (isNewInstance)
            {
                SquirrelAwareApp.HandleEvents(onAppUpdate: Form1.OnAppUpdate, onAppUninstall: Form1.OnAppUninstall, onInitialInstall: Form1.OnInitialInstall);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = new Form1();
                mainForm.Load += (sender, e) => mainForm.BringToFront();
                Application.Run(mainForm);
            }
            else
            {
                IntPtr hWnd = FindWindow(null, STR_FORMNAME);
                if (hWnd != IntPtr.Zero)
                {
                    PostMessage(hWnd, WM_SHOWFIRSTINSTANCE, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }
    }
}