using System;

// Token: 0x0200043B RID: 1083
public enum ActivationResult
{
	// Token: 0x040013D3 RID: 5075
	Success,
	// Token: 0x040013D4 RID: 5076
	Fail_Busy,
	// Token: 0x040013D5 RID: 5077
	Fail_Broken,
	// Token: 0x040013D6 RID: 5078
	Fail_Access,
	// Token: 0x040013D7 RID: 5079
	Fail_Redundant,
	// Token: 0x040013D8 RID: 5080
	Fail_BadToggle,
	// Token: 0x040013D9 RID: 5081
	Fail_RequiresInstigator,
	// Token: 0x040013DA RID: 5082
	Error_Implementation,
	// Token: 0x040013DB RID: 5083
	Error_Destroyed
}
