using System;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public interface IBasicTorchItem : IHeldItem, IInventoryItem
{
	// Token: 0x17000A65 RID: 2661
	// (get) Token: 0x060034F5 RID: 13557
	// (set) Token: 0x060034F6 RID: 13558
	bool isLit { get; set; }

	// Token: 0x060034F7 RID: 13559
	void Ignite();

	// Token: 0x060034F8 RID: 13560
	void Extinguish();

	// Token: 0x17000A66 RID: 2662
	// (get) Token: 0x060034F9 RID: 13561
	// (set) Token: 0x060034FA RID: 13562
	GameObject light { get; set; }
}
