using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        private string[] ChosenClotho = new string[] { "", "", "", "", "", "", "" };
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

        public Form1()
        {
            this.InitializeComponent();
            bool flag = Directory.Exists(this.LocalFilePath);
            bool flag2 = !flag;
            if (flag2)
            {
                Directory.CreateDirectory(this.LocalFilePath);
            }
            bool flag3 = Directory.Exists(this.LocalNiMaxReport);
            bool flag4 = !flag3;
            if (flag4)
            {
                Directory.CreateDirectory(this.LocalNiMaxReport);
            }
            this.Clotho_Path = this.CheckLastClotho();
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

        // Token: 0x06000010 RID: 16 RVA: 0x00002DBC File Offset: 0x00000FBC
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

        private string CheckLastClotho()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\");
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            int num = 0;
            string[] array = new string[]
            {
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            };
            foreach (DirectoryInfo directoryInfo2 in directories)
            {
                bool flag = directoryInfo2.Name.StartsWith("Avago.ATF.") & !directoryInfo2.Name.Contains("Common");
                if (flag)
                {
                    string text = directoryInfo2.FullName + "\\System\\Avago.ATF.UIs.exe";
                    bool flag2 = File.Exists(text);
                    if (flag2)
                    {
                        array[num] = FileVersionInfo.GetVersionInfo(text).ProductVersion;
                        this.ChosenClotho[num] = text;
                        string value = array[num].Replace(".", "").Remove(3);
                        this.ReadCurrentClotho = Convert.ToInt32(value);
                        bool flag3 = num == 0;
                        if (flag3)
                        {
                            this.LatestClotho = this.ReadCurrentClotho;
                            this.LatestInstalledClotho = text;
                        }
                        else
                        {
                            bool flag4 = this.ReadCurrentClotho > this.LatestClotho;
                            if (flag4)
                            {
                                this.LatestClotho = this.ReadCurrentClotho;
                                this.LatestInstalledClotho = text;
                            }
                        }
                        num++;
                    }
                }
            }
            bool flag5 = File.Exists(this.LastClothoFile);
            string text2;
            if (flag5)
            {
                string[] array3 = File.ReadAllLines(this.LastClothoFile);
                text2 = array3[0];
                bool flag6 = !File.Exists(text2);
                if (flag6)
                {
                    text2 = this.LatestInstalledClotho;
                }
                bool flag7 = !text2.Contains("Avago.ATF.UIs.exe");
                if (flag7)
                {
                    text2 = this.LatestInstalledClotho;
                }
            }
            else
            {
                text2 = this.LatestInstalledClotho;
            }
            return text2;
        }

        private void Display_Clotho_Version()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\");
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            int num = 0;
            string[] array = new string[]
            {
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            };
            foreach (DirectoryInfo directoryInfo2 in directories)
            {
                bool flag = directoryInfo2.Name.StartsWith("Avago.ATF.") & !directoryInfo2.Name.Contains("Common");
                if (flag)
                {
                    string text = directoryInfo2.FullName + "\\System\\Avago.ATF.UIs.exe";
                    bool flag2 = File.Exists(text);
                    if (flag2)
                    {
                        array[num] = FileVersionInfo.GetVersionInfo(text).ProductVersion;
                        this.ChosenClotho[num] = text;
                        string value = array[num].Replace(".", "").Remove(3);
                        this.ReadCurrentClotho = Convert.ToInt32(value);
                        bool flag3 = num == 0;
                        if (flag3)
                        {
                            this.LatestClotho = this.ReadCurrentClotho;
                            this.LatestInstalledClotho = text;
                        }
                        else
                        {
                            bool flag4 = this.ReadCurrentClotho > this.LatestClotho;
                            if (flag4)
                            {
                                this.LatestClotho = this.ReadCurrentClotho;
                                this.LatestInstalledClotho = text;
                            }
                        }
                        num++;
                    }
                }
            }
            bool flag5 = File.Exists(this.LastClothoFile);
            string text2;
            if (flag5)
            {
                string[] array3 = File.ReadAllLines(this.LastClothoFile);
                text2 = array3[0];
                bool flag6 = !File.Exists(text2);
                if (flag6)
                {
                    text2 = this.LatestInstalledClotho;
                }
            }
            else
            {
                text2 = this.LatestInstalledClotho;
            }
            for (int j = 0; j < num; j++)
            {
                bool flag7 = j == 0;
                if (flag7)
                {
                    this.btn_Clotho01.Text = "Clotho v." + array[j];
                    this.btn_Clotho01.Visible = true;
                    bool flag8 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag8)
                    {
                        this.btn_Clotho01.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag9 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag9)
                        {
                            this.btn_Clotho01.BackColor = Color.Green;
                        }
                    }
                }
                bool flag10 = j == 1;
                if (flag10)
                {
                    this.btn_Clotho02.Text = "Clotho v." + array[j];
                    this.btn_Clotho02.Visible = true;
                    bool flag11 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag11)
                    {
                        this.btn_Clotho02.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag12 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag12)
                        {
                            this.btn_Clotho02.BackColor = Color.Green;
                        }
                    }
                }
                bool flag13 = j == 2;
                if (flag13)
                {
                    this.btn_Clotho03.Text = "Clotho v." + array[j];
                    this.btn_Clotho03.Visible = true;
                    bool flag14 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag14)
                    {
                        this.btn_Clotho03.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag15 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag15)
                        {
                            this.btn_Clotho03.BackColor = Color.Green;
                        }
                    }
                }
                bool flag16 = j == 3;
                if (flag16)
                {
                    this.btn_Clotho04.Text = "Clotho v." + array[j];
                    this.btn_Clotho04.Visible = true;
                    bool flag17 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag17)
                    {
                        this.btn_Clotho04.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag18 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag18)
                        {
                            this.btn_Clotho04.BackColor = Color.Green;
                        }
                    }
                }
                bool flag19 = j == 4;
                if (flag19)
                {
                    this.btn_Clotho05.Text = "Clotho v." + array[j];
                    this.btn_Clotho05.Visible = true;
                    bool flag20 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag20)
                    {
                        this.btn_Clotho05.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag21 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag21)
                        {
                            this.btn_Clotho05.BackColor = Color.Green;
                        }
                    }
                }
                bool flag22 = j == 5;
                if (flag22)
                {
                    this.btn_Clotho06.Text = "Clotho v." + array[j];
                    this.btn_Clotho06.Visible = true;
                    bool flag23 = FileVersionInfo.GetVersionInfo(text2).ProductVersion == array[j];
                    if (flag23)
                    {
                        this.btn_Clotho06.BackColor = Color.Green;
                    }
                    else
                    {
                        bool flag24 = this.Clotho_Path == this.ChosenClotho[j];
                        if (flag24)
                        {
                            this.btn_Clotho06.BackColor = Color.Green;
                        }
                    }
                }
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
            if (sender == btn_Clotho01)
                this.Clotho_Path = this.ChosenClotho[0];
            else if (sender == btn_Clotho02)
                this.Clotho_Path = this.ChosenClotho[1];
            else if (sender == btn_Clotho03)
                this.Clotho_Path = this.ChosenClotho[2];
            else if (sender == btn_Clotho04)
                this.Clotho_Path = this.ChosenClotho[3];
            else if (sender == btn_Clotho05)
                this.Clotho_Path = this.ChosenClotho[4];
            else if (sender == btn_Clotho06)
                this.Clotho_Path = this.ChosenClotho[5];

            foreach (Button _obj in new object[] { btn_Clotho01, btn_Clotho02, btn_Clotho03, btn_Clotho04, btn_Clotho05, btn_Clotho06 })
            {
                if (sender == _obj)
                    _obj.BackColor = Color.Green;
                else
                    _obj.BackColor = Color.Snow;
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