using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace TesterUtil.Properties
{
	// Token: 0x02000005 RID: 5
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00004774 File Offset: 0x00002974
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000027 RID: 39
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
