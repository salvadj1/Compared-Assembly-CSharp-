using System;

namespace Facepunch.Attributes
{
	// Token: 0x020003CB RID: 971
	[Flags]
	public enum PrefabLookupKinds
	{
		// Token: 0x0400118A RID: 4490
		Controllable = 4,
		// Token: 0x0400118B RID: 4491
		Character = 6,
		// Token: 0x0400118C RID: 4492
		NetMain = 7,
		// Token: 0x0400118D RID: 4493
		NGC = 8,
		// Token: 0x0400118E RID: 4494
		Net = 15,
		// Token: 0x0400118F RID: 4495
		Bundled = 16,
		// Token: 0x04001190 RID: 4496
		All = 31
	}
}
