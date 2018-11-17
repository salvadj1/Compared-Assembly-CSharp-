using System;
using UnityEngine;

// Token: 0x020006AA RID: 1706
public interface ITorchItem : global::IHeldItem, global::IInventoryItem, global::IThrowableItem, global::IWeaponItem
{
	// Token: 0x17000B40 RID: 2880
	// (get) Token: 0x06003A0E RID: 14862
	bool isLit { get; }

	// Token: 0x17000B41 RID: 2881
	// (get) Token: 0x06003A0F RID: 14863
	// (set) Token: 0x06003A10 RID: 14864
	float realThrowTime { get; set; }

	// Token: 0x17000B42 RID: 2882
	// (get) Token: 0x06003A11 RID: 14865
	// (set) Token: 0x06003A12 RID: 14866
	float realIgniteTime { get; set; }

	// Token: 0x17000B43 RID: 2883
	// (get) Token: 0x06003A13 RID: 14867
	// (set) Token: 0x06003A14 RID: 14868
	float forceSecondaryTime { get; set; }

	// Token: 0x17000B44 RID: 2884
	// (get) Token: 0x06003A15 RID: 14869
	// (set) Token: 0x06003A16 RID: 14870
	GameObject light { get; set; }

	// Token: 0x06003A17 RID: 14871
	void Ignite();

	// Token: 0x06003A18 RID: 14872
	void Extinguish();
}
