using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TesterUtil
{
    public partial class Form1 : Form
    {
        private bool IsRun { get; set; } = false;
        private string LastClothoFile = "C:\\Avago.ATF.Common.x64\\LastClothoFile.init";
        private string Clotho_Path = "C:\\Avago.ATF.4.0.0\\System\\Avago.ATF.UIs.exe";
        private string LatestInstalledClotho = "";
        private int LatestClotho = 0;
        private int ReadCurrentClotho = 0;
        private string ResultDir = @"C:\Avago.ATF.Common\Results\";
        private string BackupDir = @"C:\Avago.ATF.Common\Results.Backup\";
        private string CloseLotFailWarning = @"C:\Avago.ATF.Common.x64\CloseLotFailWarning.exe";
        private string TesterIPAddress = "1.1.1.1";
        private string TesterID = "Default";
        private string Tester_Usage = "Default";
        private string LocalFilePath = @"C:\Avago.ATF.Common.x64\00_TesterUtil\";
        private string LocalNiMaxReport = @"C:\Avago.ATF.Common.x64\00_NI-MAX_Report\";
        private string NiMaxReportFileName = "_NiMaxReport.html";
        private DateTime StartTime = DateTime.Now;
        private DateTime StopTime = DateTime.Now;
        private string Write2File = "";
        private int count = 0;
        private string ip_addr = "NA";
        private List<(string version, string fullpath)> ClothoList = new List<(string version, string fullpath)>();
        private int clothoIndex = 0;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Program.WM_SHOWFIRSTINSTANCE)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Program.ShowWindow(Handle, Program.SW_RESTORE);
                }
                Program.SetForegroundWindow(Handle);
            }
            base.WndProc(ref m);
        }

        public Form1()
        {
            this.InitializeComponent();
            this.Text = Program.STR_FORMNAME;

            if (!Directory.Exists(this.LocalFilePath)) Directory.CreateDirectory(this.LocalFilePath);
            if (!Directory.Exists(this.LocalNiMaxReport)) Directory.CreateDirectory(this.LocalNiMaxReport);

            GetClothoList();
            CheckLastClotho();
            this.Display_Clotho_Version();
            this.MoveDir(this.ResultDir, this.BackupDir);

            Application.ApplicationExit += (s, e) =>
            {
                if (IsRun)
                    this.KillExternalProgram(this.Clotho_Path, this.CloseLotFailWarning);
            };
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            for (int i = 1; i <= this.count; i++)
            {
                bool cancellationPending = backgroundWorker.CancellationPending;
                if (cancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                backgroundWorker.ReportProgress(i);
            }
        }

        private void btn_QA_Click(object sender, EventArgs e)
        {
            RunClotho("QA");
        }

        private void btn_NPI_Click(object sender, EventArgs e)
        {
            RunClotho("NPI");
        }

        private async void RunClotho(string tester_usage)
        {
            if (IsRun) return;

            this.Tester_Usage = tester_usage;
            File.WriteAllText(this.LastClothoFile, this.Clotho_Path);
            this.MoveDir(this.ResultDir, this.BackupDir);
            string directoryName = Path.GetDirectoryName(this.Clotho_Path);
            Directory.SetCurrentDirectory(directoryName);

            this.ip_addr = this.Auto_IpAddr_Update();
            this.lbl_ipaddr.Text = "Tester IP Address = " + this.ip_addr.ToString();
            this.lbl_ipaddr.BackColor = Color.LightBlue;

            await this.LaunchExternalProgram(this.Clotho_Path);
            base.TopMost = false;
            this.Util_Logger();
            this.KillExternalProgram(this.Clotho_Path, this.CloseLotFailWarning);
            base.WindowState = FormWindowState.Normal;
        }

        private void btn_PM_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon... ");
            this.Tester_Usage = "PM";
        }

        private void Util_Logger()
        {
            bool flag = false;
            string starttime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string endtime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string usagetime = "0";
            bool flag2 = false;
            string url = Path.GetDirectoryName(this.Clotho_Path) + "\\Configuration\\ATFConfig.xml";
            XmlTextReader xmlTextReader = new XmlTextReader(url);
            while (xmlTextReader.Read())
            {
                XmlNodeType nodeType = xmlTextReader.NodeType;
                XmlNodeType xmlNodeType = nodeType;
                if (xmlNodeType == XmlNodeType.Element)
                {
                    bool flag3 = xmlTextReader.Name == "ConfigItem";
                    if (flag3)
                    {
                        xmlTextReader.MoveToNextAttribute();
                        bool flag4 = xmlTextReader.Value == "IPAddress";
                        if (flag4)
                        {
                            xmlTextReader.MoveToNextAttribute();
                            xmlTextReader.MoveToNextAttribute();
                            this.TesterIPAddress = xmlTextReader.Value;
                        }
                        bool flag5 = xmlTextReader.Value == "TesterID";
                        if (flag5)
                        {
                            xmlTextReader.MoveToNextAttribute();
                            xmlTextReader.MoveToNextAttribute();
                            this.TesterID = xmlTextReader.Value;
                        }
                    }
                }
            }
            xmlTextReader.Close();
            starttime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            this.StartTime = DateTime.Now;
            bool flag6 = true;
            while (!flag)
            {
                Thread.Sleep(1000);
                this.count++;
                bool flag7 = this.CheckExternalProgram(this.Clotho_Path);
                bool flag8 = !flag7;
                if (flag8)
                {
                    flag2 = true;
                    break;
                }
                bool flag9 = this.CheckFolderNonEmpty("C:\\Avago.ATF.Common\\Results");
                while (flag9)
                {
                    Thread.Sleep(1000);
                    flag7 = this.CheckExternalProgram(this.Clotho_Path);
                    bool flag10 = !flag7;
                    if (flag10)
                    {
                        flag = true;
                        flag2 = true;
                        break;
                    }
                    bool flag11 = this.CheckFolderEmpty("C:\\Avago.ATF.Common\\Results");
                    bool flag12 = flag11;
                    if (flag12)
                    {
                        endtime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        this.StopTime = DateTime.Now;
                        usagetime = this.StopTime.Subtract(this.StartTime).TotalMinutes.ToString("0.##");
                        flag = true;
                        break;
                    }
                }
            }
            bool flag13 = !flag2;
            if (flag13)
            {
                this.Write2File = string.Join(",",
                    this.TesterID,
                    this.TesterIPAddress,
                    this.Tester_Usage,
                    starttime,
                    endtime,
                    usagetime
                );
                string contents = "Tester ID,IP Address,Usage,Start Time,Stop Time,Usage Time (Minutes)\r\n";
                string text4 = this.LocalFilePath + "TU_IP" + this.TesterIPAddress + ".csv";
                bool flag14 = !File.Exists(text4);
                if (flag14)
                {
                    File.WriteAllText(text4, contents);
                    File.AppendAllText(text4, this.Write2File + "\r\n");
                }
                else
                {
                    try
                    {
                        File.AppendAllText(text4, this.Write2File + "\r\n");
                    }
                    catch
                    {
                        string path = text4.Replace(".csv", "_temp.csv");
                        File.WriteAllText(path, contents);
                        File.AppendAllText(path, this.Write2File + "\r\n");
                    }
                }
            }
            else
            {
                bool flag15 = flag6;
                if (flag15)
                {
                    endtime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    this.StopTime = DateTime.Now;
                    usagetime = this.StopTime.Subtract(this.StartTime).TotalMinutes.ToString("0.##");
                    this.Write2File = string.Join(",",
                        this.TesterID,
                        this.TesterIPAddress,
                        this.Tester_Usage,
                        starttime,
                        endtime,
                        usagetime
                    );
                    string contents2 = "Tester ID,IP Address,Usage,Start Time,Stop Time,Usage Time (Minutes)\r\n";
                    string text5 = this.LocalFilePath + "TU_IP" + this.TesterIPAddress + ".csv";
                    bool flag16 = !File.Exists(text5);
                    if (flag16)
                    {
                        File.WriteAllText(text5, contents2);
                        File.AppendAllText(text5, this.Write2File + "\r\n");
                    }
                    else
                    {
                        try
                        {
                            File.AppendAllText(text5, this.Write2File + "\r\n");
                        }
                        catch
                        {
                            string path2 = text5.Replace(".csv", "_temp.csv");
                            File.WriteAllText(path2, contents2);
                            File.AppendAllText(path2, this.Write2File + "\r\n");
                        }
                    }
                }
            }
        }

        private string Auto_IpAddr_Update()
        {
            bool flag = true;
            string text = "C:\\Avago.ATF.4.0.0\\System\\Configuration\\ATFConfig.xml";
            string text2 = "10.10.10.10";
            string text3 = "10.1.1.1";
            string result = "error";
            text = Path.GetDirectoryName(this.Clotho_Path) + "\\Configuration\\ATFConfig.xml";
            XmlTextReader xmlTextReader = new XmlTextReader(text);
            while (xmlTextReader.Read())
            {
                XmlNodeType nodeType = xmlTextReader.NodeType;
                XmlNodeType xmlNodeType = nodeType;
                if (xmlNodeType == XmlNodeType.Element)
                {
                    bool flag2 = xmlTextReader.Name == "ConfigItem";
                    if (flag2)
                    {
                        xmlTextReader.MoveToNextAttribute();
                        bool flag3 = xmlTextReader.Value == "IPAddress";
                        if (flag3)
                        {
                            xmlTextReader.MoveToNextAttribute();
                            xmlTextReader.MoveToNextAttribute();
                            text2 = xmlTextReader.Value;
                        }
                    }
                }
            }
            xmlTextReader.Close();
            string hostName = Dns.GetHostName();
            IPHostEntry hostByName = Dns.GetHostEntry(hostName);
            foreach (IPAddress ipaddress in hostByName.AddressList)
            {
                string a = ipaddress.ToString();
                bool flag4 = a == text2;
                if (flag4)
                {
                    flag = false;
                    result = text2;
                }
            }
            bool flag5 = flag;
            if (flag5)
            {
                foreach (IPAddress ipaddress2 in hostByName.AddressList)
                {
                    bool flag6 = ipaddress2.ToString().StartsWith("10.");
                    if (flag6)
                    {
                        text3 = ipaddress2.ToString();
                    }
                }
            }
            bool flag7 = false;
            foreach (string text4 in File.ReadLines(text))
            {
                bool flag8 = text4.Contains("IPAddress") && text4.Contains(text2);
                if (flag8)
                {
                    flag7 = true;
                }
            }
            bool flag9 = flag7 && flag;
            if (flag9)
            {
                string text5 = File.ReadAllText(text);
                text5 = text5.Replace(text2, text3);
                File.WriteAllText(text, text5);
                result = text3;
            }
            return result;
        }

        private bool CheckFolderEmpty(string path)
        {
            bool flag = string.IsNullOrEmpty(path);
            if (flag)
            {
                throw new ArgumentNullException("path");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            bool exists = directoryInfo.Exists;
            if (exists)
            {
                return directoryInfo.GetFileSystemInfos().Length == 0;
            }
            throw new DirectoryNotFoundException();
        }

        private bool CheckFolderNonEmpty(string path)
        {
            bool flag = string.IsNullOrEmpty(path);
            if (flag)
            {
                throw new ArgumentNullException("path");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            bool exists = directoryInfo.Exists;
            if (exists)
            {
                return directoryInfo.GetFileSystemInfos().Length != 0;
            }
            throw new DirectoryNotFoundException();
        }

        private void MoveDir(string srcDir, string destDir)
        {
            string[] files = Directory.GetFiles(srcDir);
            try
            {
                foreach (string text in files)
                {
                    string fileName = Path.GetFileName(text);
                    File.Move(text, destDir + fileName);
                }
            }
            catch
            {
            }
        }

        private string GetTesterIp()
        {
            string url = Path.GetDirectoryName(this.Clotho_Path) + "\\Configuration\\ATFConfig.xml";
            XmlTextReader xmlTextReader = new XmlTextReader(url);
            while (xmlTextReader.Read())
            {
                XmlNodeType nodeType = xmlTextReader.NodeType;
                XmlNodeType xmlNodeType = nodeType;
                if (xmlNodeType == XmlNodeType.Element)
                {
                    bool flag = xmlTextReader.Name == "ConfigItem";
                    if (flag)
                    {
                        xmlTextReader.MoveToNextAttribute();
                        bool flag2 = xmlTextReader.Value == "IPAddress";
                        if (flag2)
                        {
                            xmlTextReader.MoveToNextAttribute();
                            xmlTextReader.MoveToNextAttribute();
                            this.TesterIPAddress = xmlTextReader.Value;
                        }
                        bool flag3 = xmlTextReader.Value == "TesterID";
                        if (flag3)
                        {
                            xmlTextReader.MoveToNextAttribute();
                            xmlTextReader.MoveToNextAttribute();
                            this.TesterID = xmlTextReader.Value;
                        }
                    }
                }
            }
            xmlTextReader.Close();
            return this.TesterIPAddress;
        }

        private async Task LaunchExternalProgram(string ProgramPath)
        {
            IsRun = true;

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ProgramPath);
            foreach (Process process in Process.GetProcesses())
            {
                bool flag = process.ProcessName == fileNameWithoutExtension;
                if (flag)
                {
                    process.Kill();
                    Thread.Sleep(100);
                }
            }

            await Task.Run(() =>
            {
                Process process2 = new Process();
                try
                {
                    process2.StartInfo.UseShellExecute = false;
                    process2.StartInfo.FileName = ProgramPath;
                    process2.StartInfo.CreateNoWindow = true;
                    process2.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    process2.WaitForExit();
                    process2.Dispose();
                    IsRun = false;
                }
            });
        }

        private void KillExternalProgram(params string[] ProgramPaths)
        {
            var filenameswoExtension = ProgramPaths.Select(ProgramPathspath => Path.GetFileNameWithoutExtension(ProgramPathspath));
            var processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                bool flag = filenameswoExtension.Any(v => v == process.ProcessName);
                if (flag)
                {
                    process.Kill();
                }
            }

            Thread.Sleep(100);
        }

        private bool CheckExternalProgram(string ProgramPath)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ProgramPath);
            bool result = false;
            foreach (Process process in Process.GetProcesses())
            {
                bool flag = process.ProcessName == fileNameWithoutExtension;
                if (flag)
                {
                    result = true;
                }
            }
            return result;
        }

        private void GetClothoList()
        {
            var directories = Directory.GetDirectories(@"C:\")
            .Where(dir => Regex.IsMatch(dir, @"Avago.ATF.\d+.\d+.\d+"))
            .ToList();

            foreach (var dir in directories)
            {
                string _clotho_exe = Path.Combine(dir, @"System\Avago.ATF.UIs.exe");
                bool flag2 = File.Exists(_clotho_exe);
                if (flag2)
                {
                    var _version = FileVersionInfo.GetVersionInfo(_clotho_exe).ProductVersion;
                    ClothoList.Add((_version, _clotho_exe));

                    this.ReadCurrentClotho = Convert.ToInt32(Regex.Replace(_version, @"[^0-9]", ""));
                    if (this.ReadCurrentClotho > this.LatestClotho)
                    {
                        this.LatestClotho = this.ReadCurrentClotho;
                        this.LatestInstalledClotho = _clotho_exe;
                    }
                }
            }
        }

        private void CheckLastClotho()
        {
            string text2;
            if (File.Exists(this.LastClothoFile))
            {
                text2 = File.ReadAllLines(this.LastClothoFile)[0];

                if (!File.Exists(text2)) text2 = this.LatestInstalledClotho;
                if (!text2.Contains("Avago.ATF.UIs.exe")) text2 = this.LatestInstalledClotho;
            }
            else
            {
                text2 = this.LatestInstalledClotho;
            }

            clothoIndex = ClothoList.FindIndex(s => s.fullpath == text2);
            Clotho_Path = text2;
        }

        private void Display_Clotho_Version()
        {
            int _idx = 0;

            foreach (var dir in ClothoList)
            {
                Button button = new Button
                {
                    Text = $"Clotho v.{dir.version}",
                    Width = 267,
                    Height = 29,
                    Tag = dir.fullpath,
                    BackColor = clothoIndex == _idx ? Color.Green : Color.Snow,
                    Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold)
                };

                button.Click += btn_Clotho01_Click;
                buttonPanel.Controls.Add(button);
                _idx += 1;
            }
        }

        private void clearFolder(string FolderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(FolderName);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                fileInfo.Delete();
            }
            foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
            {
                this.clearFolder(directoryInfo2.FullName);
                directoryInfo2.Delete();
            }
        }

        private void btn_Clotho01_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int buttonIndex = buttonPanel.Controls.GetChildIndex(clickedButton);

                string folderPath = clickedButton.Tag.ToString();

                this.Clotho_Path = folderPath;

                for (int i = 0; i < buttonPanel.Controls.Count; i++)
                {
                    buttonPanel.Controls[i].BackColor = i == buttonIndex ? Color.Green : Color.Snow;
                }
            }
        }

        private void SetProgressLabel(string message, int progress)
        {
            this.lbl_message.Text = message + progress.ToString();
        }

        private void btn_Inst_Click(object sender, EventArgs e)
        {
            this.btn_Inst.BackColor = Color.Yellow;
            bool flag = Directory.Exists(this.LocalNiMaxReport);
            string text = "C:\\Program Files (x86)\\National Instruments\\MAX\\";
            string str = "IP" + this.GetTesterIp();
            bool flag2 = !flag;
            if (flag2)
            {
                Directory.CreateDirectory(this.LocalNiMaxReport);
            }
            else
            {
                this.clearFolder(this.LocalNiMaxReport);
            }
            Directory.SetCurrentDirectory(text);
            string text2 = this.LocalNiMaxReport + str + this.NiMaxReportFileName;
            string fileName = text + "nimax.exe";
            string arguments = "/report:fileName=\"" + text2 + "\";reportType=\"custom\";Silent=\"true\"";
            Process.Start(fileName, arguments);
            for (int i = 0; i < 120; i++)
            {
                Thread.Sleep(1000);
                bool flag3 = File.Exists(text2);
                if (flag3)
                {
                    this.btn_Inst.BackColor = Color.Green;
                    break;
                }
            }
        }
    }
}