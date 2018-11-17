using System;

// Token: 0x0200067A RID: 1658
public interface IBowWeaponItem : global::IHeldItem, global::IInventoryItem, global::IWeaponItem
{
	// Token: 0x17000AE0 RID: 2784
	// (get) Token: 0x060038CE RID: 14542
	// (set) Token: 0x060038CF RID: 14543
	bool arrowDrawn { get; set; }

	// Token: 0x17000AE1 RID: 2785
	// (get) Token: 0x060038D0 RID: 14544
	// (set) Token: 0x060038D1 RID: 14545
	bool tired { get; set; }

	// Token: 0x17000AE2 RID: 2786
	// (get) Token: 0x060038D2 RID: 14546
	// (set) Token: 0x060038D3 RID: 14547
	float completeDrawTime { get; set; }

	// Token: 0x17000AE3 RID: 2787
	// (get) Token: 0x060038D4 RID: 14548
	// (set) Token: 0x060038D5 RID: 14549
	int currentArrowID { get; set; }

	// Token: 0x060038D6 RID: 14550
	global::IInventoryItem FindAmmo();

	// Token: 0x060038D7 RID: 14551
	void ArrowReportMiss(global::ArrowMovement arrow);

	// Token: 0x060038D8 RID: 14552
	void ArrowReportHit(IDMain hitMain, global::ArrowMovement arrow);

	// Token: 0x060038D9 RID: 14553
	void MakeReadyIn(float delay);
}
