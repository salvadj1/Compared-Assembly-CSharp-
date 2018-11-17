using System;

namespace Facepunch.Load
{
	// Token: 0x0200026D RID: 621
	[Flags]
	public enum TaskStatus : byte
	{
		// Token: 0x04000B83 RID: 2947
		Pending = 1,
		// Token: 0x04000B84 RID: 2948
		Downloading = 2,
		// Token: 0x04000B85 RID: 2949
		Complete = 4
	}
}
