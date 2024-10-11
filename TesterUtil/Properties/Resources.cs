using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TesterUtil.Properties
{
	// Token: 0x02000004 RID: 4
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00004701 File Offset: 0x00002901
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000470C File Offset: 0x0000290C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TesterUtil.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000476B File Offset: 0x0000296B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000025 RID: 37
		private static ResourceManager resourceMan;

		// Token: 0x04000026 RID: 38
		private static CultureInfo resourceCulture;
	}
}
