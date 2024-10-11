using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace TesterUtil
{
	// Token: 0x02000002 RID: 2
	public partial class Form1 : Form
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
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
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021E8 File Offset: 0x000003E8
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

		// Token: 0x06000003 RID: 3 RVA: 0x00002236 File Offset: 0x00000436
		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002239 File Offset: 0x00000439
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000223C File Offset: 0x0000043C
		private void btn_QA_Click(object sender, EventArgs e)
		{
			this.Tester_Usage = "QA";
			File.WriteAllText(this.LastClothoFile, this.Clotho_Path);
			this.MoveDir(this.ResultDir, this.BackupDir);
			string directoryName = Path.GetDirectoryName(this.Clotho_Path);
			Directory.SetCurrentDirectory(directoryName);
			this.LaunchExternalProgram(this.Clotho_Path);
			base.TopMost = false;
			this.Util_Logger();
			this.KillExternalProgram(this.Clotho_Path);
			this.KillExternalProgram(this.CloseLotFailWarning);
			base.WindowState = FormWindowState.Normal;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022CC File Offset: 0x000004CC
		private void btn_NPI_Click(object sender, EventArgs e)
		{
			this.Tester_Usage = "NPI";
			File.WriteAllText(this.LastClothoFile, this.Clotho_Path);
			this.MoveDir(this.ResultDir, this.BackupDir);
			string directoryName = Path.GetDirectoryName(this.Clotho_Path);
			Directory.SetCurrentDirectory(directoryName);
			this.ip_addr = this.Auto_IpAddr_Update();
			this.lbl_ipaddr.Text = "Tester IP Address = " + this.ip_addr.ToString();
			this.lbl_ipaddr.BackColor = Color.LightBlue;
			this.LaunchExternalProgram(this.Clotho_Path);
			base.TopMost = false;
			this.Util_Logger();
			this.KillExternalProgram(this.Clotho_Path);
			this.KillExternalProgram(this.CloseLotFailWarning);
			base.WindowState = FormWindowState.Normal;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002399 File Offset: 0x00000599
		private void btn_PM_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Coming Soon... ");
			this.Tester_Usage = "PM";
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023B4 File Offset: 0x000005B4
		private void Util_Logger()
		{
			bool flag = false;
			string text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
			string text2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
			string text3 = "0";
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
			text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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
						text2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
						this.StopTime = DateTime.Now;
						text3 = this.StopTime.Subtract(this.StartTime).TotalMinutes.ToString("0.##");
						flag = true;
						break;
					}
				}
			}
			bool flag13 = !flag2;
			if (flag13)
			{
				this.Write2File = string.Concat(new string[]
				{
					this.TesterID,
					",",
					this.TesterIPAddress,
					",",
					this.Tester_Usage,
					",",
					text,
					",",
					text2,
					",",
					text3
				});
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
					text2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
					this.StopTime = DateTime.Now;
					text3 = this.StopTime.Subtract(this.StartTime).TotalMinutes.ToString("0.##");
					this.Write2File = string.Concat(new string[]
					{
						this.TesterID,
						",",
						this.TesterIPAddress,
						",",
						this.Tester_Usage,
						",",
						text,
						",",
						text2,
						",",
						text3
					});
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

		// Token: 0x06000009 RID: 9 RVA: 0x000028C4 File Offset: 0x00000AC4
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
			IPHostEntry hostByName = Dns.GetHostByName(hostName);
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

		// Token: 0x0600000A RID: 10 RVA: 0x00002ADC File Offset: 0x00000CDC
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

		// Token: 0x0600000B RID: 11 RVA: 0x00002B28 File Offset: 0x00000D28
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

		// Token: 0x0600000C RID: 12 RVA: 0x00002B74 File Offset: 0x00000D74
		private void MoveDir(string srcDir, string destDir)
		{
			string[] files = Directory.GetFiles(srcDir);
			foreach (string text in files)
			{
				string fileName = Path.GetFileName(text);
				File.Move(text, destDir + fileName);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002BB8 File Offset: 0x00000DB8
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void LaunchExternalProgram(string ProgramPath)
		{
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
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002D68 File Offset: 0x00000F68
		private void KillExternalProgram(string ProgramPath)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ProgramPath);
			foreach (Process process in Process.GetProcesses())
			{
				bool flag = process.ProcessName == fileNameWithoutExtension;
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

		// Token: 0x06000011 RID: 17 RVA: 0x00002E10 File Offset: 0x00001010
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002FF4 File Offset: 0x000011F4
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

		// Token: 0x06000013 RID: 19 RVA: 0x00003534 File Offset: 0x00001734
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

		// Token: 0x06000014 RID: 20 RVA: 0x000035A8 File Offset: 0x000017A8
		private void btn_Clotho01_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[0];
			this.btn_Clotho01.BackColor = Color.Green;
			this.btn_Clotho02.BackColor = Color.Snow;
			this.btn_Clotho03.BackColor = Color.Snow;
			this.btn_Clotho04.BackColor = Color.Snow;
			this.btn_Clotho05.BackColor = Color.Snow;
			this.btn_Clotho06.BackColor = Color.Snow;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000362C File Offset: 0x0000182C
		private void btn_Clotho02_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[1];
			this.btn_Clotho01.BackColor = Color.Snow;
			this.btn_Clotho02.BackColor = Color.Green;
			this.btn_Clotho03.BackColor = Color.Snow;
			this.btn_Clotho04.BackColor = Color.Snow;
			this.btn_Clotho05.BackColor = Color.Snow;
			this.btn_Clotho06.BackColor = Color.Snow;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000036B0 File Offset: 0x000018B0
		private void btn_Clotho03_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[2];
			this.btn_Clotho01.BackColor = Color.Snow;
			this.btn_Clotho02.BackColor = Color.Snow;
			this.btn_Clotho03.BackColor = Color.Green;
			this.btn_Clotho04.BackColor = Color.Snow;
			this.btn_Clotho05.BackColor = Color.Snow;
			this.btn_Clotho06.BackColor = Color.Snow;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003734 File Offset: 0x00001934
		private void btn_Clotho04_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[3];
			this.btn_Clotho01.BackColor = Color.Snow;
			this.btn_Clotho02.BackColor = Color.Snow;
			this.btn_Clotho03.BackColor = Color.Snow;
			this.btn_Clotho04.BackColor = Color.Green;
			this.btn_Clotho05.BackColor = Color.Snow;
			this.btn_Clotho06.BackColor = Color.Snow;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000037B8 File Offset: 0x000019B8
		private void btn_Clotho05_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[4];
			this.btn_Clotho01.BackColor = Color.Snow;
			this.btn_Clotho02.BackColor = Color.Snow;
			this.btn_Clotho03.BackColor = Color.Snow;
			this.btn_Clotho04.BackColor = Color.Snow;
			this.btn_Clotho05.BackColor = Color.Green;
			this.btn_Clotho06.BackColor = Color.Snow;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000383C File Offset: 0x00001A3C
		private void btn_Clotho06_Click(object sender, EventArgs e)
		{
			this.Clotho_Path = this.ChosenClotho[5];
			this.btn_Clotho01.BackColor = Color.Snow;
			this.btn_Clotho02.BackColor = Color.Snow;
			this.btn_Clotho03.BackColor = Color.Snow;
			this.btn_Clotho04.BackColor = Color.Snow;
			this.btn_Clotho05.BackColor = Color.Snow;
			this.btn_Clotho06.BackColor = Color.Green;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000038BE File Offset: 0x00001ABE
		private void SetProgressLabel(string message, int progress)
		{
			this.lbl_message.Text = message + progress.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000038DC File Offset: 0x00001ADC
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

		// Token: 0x0600001C RID: 28 RVA: 0x000039D4 File Offset: 0x00001BD4
		private void lbl_message_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000039D7 File Offset: 0x00001BD7
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x04000001 RID: 1
		private string LastClothoFile = "C:\\Avago.ATF.Common.x64\\LastClothoFile.init";

		// Token: 0x04000002 RID: 2
		private string Clotho_Path = "C:\\Avago.ATF.4.0.0\\System\\Avago.ATF.UIs.exe";

		// Token: 0x04000003 RID: 3
		private string LatestInstalledClotho = "";

		// Token: 0x04000004 RID: 4
		private int LatestClotho = 0;

		// Token: 0x04000005 RID: 5
		private int ReadCurrentClotho = 0;

		// Token: 0x04000006 RID: 6
		private string[] ChosenClotho = new string[]
		{
			"",
			"",
			"",
			"",
			"",
			"",
			""
		};

		// Token: 0x04000007 RID: 7
		private string ResultDir = "C:\\Avago.ATF.Common\\Results\\";

		// Token: 0x04000008 RID: 8
		private string BackupDir = "C:\\Avago.ATF.Common\\Results.Backup\\";

		// Token: 0x04000009 RID: 9
		private string CloseLotFailWarning = "C:\\Avago.ATF.Common.x64\\CloseLotFailWarning.exe";

		// Token: 0x0400000A RID: 10
		private string TesterIPAddress = "1.1.1.1";

		// Token: 0x0400000B RID: 11
		private string TesterID = "Default";

		// Token: 0x0400000C RID: 12
		private string Tester_Usage = "Default";

		// Token: 0x0400000D RID: 13
		private string LocalFilePath = "C:\\Avago.ATF.Common.x64\\00_TesterUtil\\";

		// Token: 0x0400000E RID: 14
		private string LocalNiMaxReport = "C:\\Avago.ATF.Common.x64\\00_NI-MAX_Report\\";

		// Token: 0x0400000F RID: 15
		private string NiMaxReportFileName = "_NiMaxReport.html";

		// Token: 0x04000010 RID: 16
		private DateTime StartTime = DateTime.Now;

		// Token: 0x04000011 RID: 17
		private DateTime StopTime = DateTime.Now;

		// Token: 0x04000012 RID: 18
		private string Write2File = "";

		// Token: 0x04000013 RID: 19
		private int count = 0;

		// Token: 0x04000014 RID: 20
		private string ip_addr = "NA";
	}
}
