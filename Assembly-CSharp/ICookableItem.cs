using System;

// Token: 0x020005CC RID: 1484
public interface ICookableItem : IInventoryItem
{
	// Token: 0x06003590 RID: 13712
	bool GetCookableInfo(out int consumeCount, out ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp);
}
