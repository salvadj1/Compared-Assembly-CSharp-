using System;

// Token: 0x020001F4 RID: 500
public enum UseCheck : sbyte
{
	// Token: 0x0400086E RID: 2158
	Success = 1,
	// Token: 0x0400086F RID: 2159
	OutOfOrder = -128,
	// Token: 0x04000870 RID: 2160
	BadUser,
	// Token: 0x04000871 RID: 2161
	BadConfiguration
}
