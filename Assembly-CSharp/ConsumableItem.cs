using System;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public abstract class ConsumableItem<T> : InventoryItem<T> where T : ConsumableDataBlock
{
	// Token: 0x06003549 RID: 13641 RVA: 0x000C201C File Offset: 0x000C021C
	protected ConsumableItem(T db) : base(db)
	{
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x000C2028 File Offset: 0x000C0228
	public bool GetCookableInfo(out int consumeCount, out ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
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
