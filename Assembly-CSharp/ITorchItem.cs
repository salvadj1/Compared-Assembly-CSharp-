using System;
using UnityEngine;

// Token: 0x020005EC RID: 1516
public interface ITorchItem : IHeldItem, IInventoryItem, IThrowableItem, IWeaponItem
{
	// Token: 0x17000ACA RID: 2762
	// (get) Token: 0x06003646 RID: 13894
	bool isLit { get; }

	// Token: 0x17000ACB RID: 2763
	// (get) Token: 0x06003647 RID: 13895
	// (set) Token: 0x06003648 RID: 13896
	float realThrowTime { get; set; }

	// Token: 0x17000ACC RID: 2764
	// (get) Token: 0x06003649 RID: 13897
	// (set) Token: 0x0600364A RID: 13898
	float realIgniteTime { get; set; }

	// Token: 0x17000ACD RID: 2765
	// (get) Token: 0x0600364B RID: 13899
	// (set) Token: 0x0600364C RID: 13900
	float forceSecondaryTime { get; set; }

	// Token: 0x17000ACE RID: 2766
	// (get) Token: 0x0600364D RID: 13901
	// (set) Token: 0x0600364E RID: 13902
	GameObject light { get; set; }

	// Token: 0x0600364F RID: 13903
	void Ignite();

	// Token: 0x06003650 RID: 13904
	void Extinguish();
}
