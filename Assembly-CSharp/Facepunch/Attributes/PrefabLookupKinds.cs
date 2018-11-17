using System;

namespace Facepunch.Attributes
{
	// Token: 0x02000478 RID: 1144
	[Flags]
	public enum PrefabLookupKinds
	{
		// Token: 0x040012F0 RID: 4848
		Controllable = 4,
		// Token: 0x040012F1 RID: 4849
		Character = 6,
		// Token: 0x040012F2 RID: 4850
		NetMain = 7,
		// Token: 0x040012F3 RID: 4851
		NGC = 8,
		// Token: 0x040012F4 RID: 4852
		Net = 15,
		// Token: 0x040012F5 RID: 4853
		Bundled = 16,
		// Token: 0x040012F6 RID: 4854
		All = 31
	}
}
