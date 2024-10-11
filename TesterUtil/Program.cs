using System;
using System.Windows.Forms;

namespace TesterUtil
{
	// Token: 0x02000003 RID: 3
	internal static class Program
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000046E6 File Offset: 0x000028E6
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
