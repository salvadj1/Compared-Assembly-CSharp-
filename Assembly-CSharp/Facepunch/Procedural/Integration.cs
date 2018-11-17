using System;

namespace Facepunch.Procedural
{
	// Token: 0x020005A1 RID: 1441
	[Flags]
	public enum Integration : byte
	{
		// Token: 0x04001928 RID: 6440
		Stationary = 1,
		// Token: 0x04001929 RID: 6441
		Moved = 2,
		// Token: 0x0400192A RID: 6442
		MovedDestination = 3,
		// Token: 0x0400192B RID: 6443
		Ahead = 4
	}
}
