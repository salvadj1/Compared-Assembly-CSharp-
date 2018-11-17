using System;

namespace Facepunch.Procedural
{
	// Token: 0x020004E7 RID: 1255
	[Flags]
	public enum ClockStatus : byte
	{
		// Token: 0x04001770 RID: 6000
		Elapsed = 1,
		// Token: 0x04001771 RID: 6001
		WillElapse = 2,
		// Token: 0x04001772 RID: 6002
		DidElapse = 3,
		// Token: 0x04001773 RID: 6003
		Negative = 4,
		// Token: 0x04001774 RID: 6004
		Unset = 0
	}
}
