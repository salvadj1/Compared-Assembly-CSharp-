using System;

namespace Facepunch.Procedural
{
	// Token: 0x020004E6 RID: 1254
	[Flags]
	public enum Integration : byte
	{
		// Token: 0x0400176B RID: 5995
		Stationary = 1,
		// Token: 0x0400176C RID: 5996
		Moved = 2,
		// Token: 0x0400176D RID: 5997
		MovedDestination = 3,
		// Token: 0x0400176E RID: 5998
		Ahead = 4
	}
}
