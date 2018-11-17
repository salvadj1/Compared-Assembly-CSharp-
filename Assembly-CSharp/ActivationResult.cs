using System;

// Token: 0x020004F1 RID: 1265
public enum ActivationResult
{
	// Token: 0x04001556 RID: 5462
	Success,
	// Token: 0x04001557 RID: 5463
	Fail_Busy,
	// Token: 0x04001558 RID: 5464
	Fail_Broken,
	// Token: 0x04001559 RID: 5465
	Fail_Access,
	// Token: 0x0400155A RID: 5466
	Fail_Redundant,
	// Token: 0x0400155B RID: 5467
	Fail_BadToggle,
	// Token: 0x0400155C RID: 5468
	Fail_RequiresInstigator,
	// Token: 0x0400155D RID: 5469
	Error_Implementation,
	// Token: 0x0400155E RID: 5470
	Error_Destroyed
}
