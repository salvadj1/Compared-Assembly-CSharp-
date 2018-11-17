using System;
using UnityEngine;

// Token: 0x0200067F RID: 1663
public abstract class ConsumableItem<T> : global::InventoryItem<T> where T : global::ConsumableDataBlock
{
	// Token: 0x06003911 RID: 14609 RVA: 0x000CA278 File Offset: 0x000C8478
	protected ConsumableItem(T db) : base(db)
	{
	}

	// Token: 0x06003912 RID: 14610 RVA: 0x000CA284 File Offset: 0x000C8484
	public bool GetCookableInfo(out int consumeCount, out global::ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
	{
		burnTemp = this.datablock.burnTemp;
		cookTempMin = this.datablock.cookHeatRequirement;
		cookedVersion = this.datablock.cookedVersion;
		if (this.datablock.cookable && cookedVersion)
		{
			cookedCount = (consumeCount = Mathf.Min(2, base.uses));
			return consumeCount > 0;
		}
		cookedCount = (consumeCount = 0);
		return false;
	}
}
