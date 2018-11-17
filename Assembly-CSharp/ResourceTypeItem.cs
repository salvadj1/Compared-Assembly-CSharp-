using System;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
public abstract class ResourceTypeItem<T> : global::InventoryItem<T> where T : global::ResourceTypeItemDataBlock
{
	// Token: 0x060039DA RID: 14810 RVA: 0x000CBBA8 File Offset: 0x000C9DA8
	protected ResourceTypeItem(T db) : base(db)
	{
	}

	// Token: 0x17000B33 RID: 2867
	// (get) Token: 0x060039DB RID: 14811 RVA: 0x000CBBB4 File Offset: 0x000C9DB4
	public bool flammable
	{
		get
		{
			return this.datablock.flammable;
		}
	}

	// Token: 0x060039DC RID: 14812 RVA: 0x000CBBC8 File Offset: 0x000C9DC8
	public bool GetCookableInfo(out int consumeCount, out global::ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
	{
		burnTemp = 999999999;
		cookTempMin = this.datablock.cookHeatRequirement;
		cookedVersion = this.datablock.cookedVersion;
		if (!this.datablock.cookable || !cookedVersion)
		{
			cookedCount = (consumeCount = 0);
			return false;
		}
		consumeCount = Mathf.Min(2, base.uses);
		cookedCount = consumeCount * Random.Range(this.datablock.numToGiveCookedMin, this.datablock.numToGiveCookedMax + 1);
		if (cookedCount == 0)
		{
			consumeCount = 0;
			return false;
		}
		return true;
	}

	// Token: 0x04001C71 RID: 7281
	protected float _lastUseTime;
}
