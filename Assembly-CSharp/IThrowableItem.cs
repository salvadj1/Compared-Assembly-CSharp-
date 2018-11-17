using System;

// Token: 0x020006A6 RID: 1702
public interface IThrowableItem : global::IHeldItem, global::IInventoryItem, global::IWeaponItem
{
	// Token: 0x060039EC RID: 14828
	void BeginHoldingBack();

	// Token: 0x060039ED RID: 14829
	void EndHoldingBack();

	// Token: 0x17000B34 RID: 2868
	// (get) Token: 0x060039EE RID: 14830
	float heldThrowStrength { get; }

	// Token: 0x17000B35 RID: 2869
	// (get) Token: 0x060039EF RID: 14831
	// (set) Token: 0x060039F0 RID: 14832
	float holdingStartTime { get; set; }

	// Token: 0x17000B36 RID: 2870
	// (get) Token: 0x060039F1 RID: 14833
	// (set) Token: 0x060039F2 RID: 14834
	bool holdingBack { get; set; }

	// Token: 0x17000B37 RID: 2871
	// (get) Token: 0x060039F3 RID: 14835
	// (set) Token: 0x060039F4 RID: 14836
	float minReleaseTime { get; set; }
}
