using System;

// Token: 0x020005BC RID: 1468
public interface IBowWeaponItem : IHeldItem, IInventoryItem, IWeaponItem
{
	// Token: 0x17000A6A RID: 2666
	// (get) Token: 0x06003506 RID: 13574
	// (set) Token: 0x06003507 RID: 13575
	bool arrowDrawn { get; set; }

	// Token: 0x17000A6B RID: 2667
	// (get) Token: 0x06003508 RID: 13576
	// (set) Token: 0x06003509 RID: 13577
	bool tired { get; set; }

	// Token: 0x17000A6C RID: 2668
	// (get) Token: 0x0600350A RID: 13578
	// (set) Token: 0x0600350B RID: 13579
	float completeDrawTime { get; set; }

	// Token: 0x17000A6D RID: 2669
	// (get) Token: 0x0600350C RID: 13580
	// (set) Token: 0x0600350D RID: 13581
	int currentArrowID { get; set; }

	// Token: 0x0600350E RID: 13582
	IInventoryItem FindAmmo();

	// Token: 0x0600350F RID: 13583
	void ArrowReportMiss(ArrowMovement arrow);

	// Token: 0x06003510 RID: 13584
	void ArrowReportHit(IDMain hitMain, ArrowMovement arrow);

	// Token: 0x06003511 RID: 13585
	void MakeReadyIn(float delay);
}
