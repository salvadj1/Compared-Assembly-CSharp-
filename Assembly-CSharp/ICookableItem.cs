using System;

// Token: 0x0200068A RID: 1674
public interface ICookableItem : global::IInventoryItem
{
	// Token: 0x06003958 RID: 14680
	bool GetCookableInfo(out int consumeCount, out global::ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp);
}
