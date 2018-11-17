using System;

// Token: 0x02000670 RID: 1648
public interface IBandageItem : global::IHeldItem, global::IInventoryItem
{
	// Token: 0x17000AD5 RID: 2773
	// (get) Token: 0x060038A9 RID: 14505
	// (set) Token: 0x060038AA RID: 14506
	float bandageStartTime { get; set; }

	// Token: 0x17000AD6 RID: 2774
	// (get) Token: 0x060038AB RID: 14507
	// (set) Token: 0x060038AC RID: 14508
	bool lastFramePrimary { get; set; }

	// Token: 0x17000AD7 RID: 2775
	// (get) Token: 0x060038AD RID: 14509
	// (set) Token: 0x060038AE RID: 14510
	float lastBandageTime { get; set; }
}
