using System;

namespace Facepunch.Procedural
{
	// Token: 0x020005A2 RID: 1442
	[Flags]
	public enum ClockStatus : byte
	{
		// Token: 0x0400192D RID: 6445
		Elapsed = 1,
		// Token: 0x0400192E RID: 6446
		WillElapse = 2,
		// Token: 0x0400192F RID: 6447
		DidElapse = 3,
		// Token: 0x04001930 RID: 6448
		Negative = 4,
		// Token: 0x04001931 RID: 6449
		Unset = 0
	}
}
