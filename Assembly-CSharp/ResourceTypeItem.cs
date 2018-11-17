using System;
using UnityEngine;

// Token: 0x020005E3 RID: 1507
public abstract class ResourceTypeItem<T> : InventoryItem<T> where T : ResourceTypeItemDataBlock
{
	// Token: 0x06003612 RID: 13842 RVA: 0x000C394C File Offset: 0x000C1B4C
	protected ResourceTypeItem(T db) : base(db)
	{
	}

	// Token: 0x17000ABD RID: 2749
	// (get) Token: 0x06003613 RID: 13843 RVA: 0x000C3958 File Offset: 0x000C1B58
	public bool flammable
	{
		get
		{
			return this.datablock.flammable;
		}
	}

	// Token: 0x06003614 RID: 13844 RVA: 0x000C396C File Offset: 0x000C1B6C
	public bool GetCookableInfo(out int consumeCount, out ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
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

	// Token: 0x04001AA0 RID: 6816
	protected float _lastUseTime;
}
