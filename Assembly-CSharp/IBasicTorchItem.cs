using System;
using UnityEngine;

// Token: 0x02000674 RID: 1652
public interface IBasicTorchItem : global::IHeldItem, global::IInventoryItem
{
	// Token: 0x17000ADB RID: 2779
	// (get) Token: 0x060038BD RID: 14525
	// (set) Token: 0x060038BE RID: 14526
	bool isLit { get; set; }

	// Token: 0x060038BF RID: 14527
	void Ignite();

	// Token: 0x060038C0 RID: 14528
	void Extinguish();

	// Token: 0x17000ADC RID: 2780
	// (get) Token: 0x060038C1 RID: 14529
	// (set) Token: 0x060038C2 RID: 14530
	GameObject light { get; set; }
}
