using System;

namespace Facepunch.Load
{
	// Token: 0x020002A1 RID: 673
	[Flags]
	public enum TaskStatus : byte
	{
		// Token: 0x04000CA9 RID: 3241
		Pending = 1,
		// Token: 0x04000CAA RID: 3242
		Downloading = 2,
		// Token: 0x04000CAB RID: 3243
		Complete = 4
	}
}
