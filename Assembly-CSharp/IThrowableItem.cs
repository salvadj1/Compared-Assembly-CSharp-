using System;

// Token: 0x020005E8 RID: 1512
public interface IThrowableItem : IHeldItem, IInventoryItem, IWeaponItem
{
	// Token: 0x06003624 RID: 13860
	void BeginHoldingBack();

	// Token: 0x06003625 RID: 13861
	void EndHoldingBack();

	// Token: 0x17000ABE RID: 2750
	// (get) Token: 0x06003626 RID: 13862
	float heldThrowStrength { get; }

	// Token: 0x17000ABF RID: 2751
	// (get) Token: 0x06003627 RID: 13863
	// (set) Token: 0x06003628 RID: 13864
	float holdingStartTime { get; set; }

	// Token: 0x17000AC0 RID: 2752
	// (get) Token: 0x06003629 RID: 13865
	// (set) Token: 0x0600362A RID: 13866
	bool holdingBack { get; set; }

	// Token: 0x17000AC1 RID: 2753
	// (get) Token: 0x0600362B RID: 13867
	// (set) Token: 0x0600362C RID: 13868
	float minReleaseTime { get; set; }
}
