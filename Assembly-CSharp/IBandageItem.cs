using System;

// Token: 0x020005B2 RID: 1458
public interface IBandageItem : IHeldItem, IInventoryItem
{
	// Token: 0x17000A5F RID: 2655
	// (get) Token: 0x060034E1 RID: 13537
	// (set) Token: 0x060034E2 RID: 13538
	float bandageStartTime { get; set; }

	// Token: 0x17000A60 RID: 2656
	// (get) Token: 0x060034E3 RID: 13539
	// (set) Token: 0x060034E4 RID: 13540
	bool lastFramePrimary { get; set; }

	// Token: 0x17000A61 RID: 2657
	// (get) Token: 0x060034E5 RID: 13541
	// (set) Token: 0x060034E6 RID: 13542
	float lastBandageTime { get; set; }
}
