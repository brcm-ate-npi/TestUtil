using System;
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

        public static readonly Version AppVersion = new Version(1, 0, 7, 0);
        public static readonly string STR_FORMNAME = $"WSD Tester Utilization v.{AppVersion.ToString()} (Auto IP Address Update)";

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        private static void Main()
        {
            bool isNewInstance;
            mutex = new Mutex(true, "UniqueProgramMutexName", out isNewInstance);

            if (isNewInstance)
            {
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