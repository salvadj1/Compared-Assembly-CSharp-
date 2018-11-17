using System;

// Token: 0x020005B5 RID: 1461
public abstract class BasicHealthKit<T> : InventoryItem<T> where T : BasicHealthKitDataBlock
{
	// Token: 0x060034F4 RID: 13556 RVA: 0x000C1568 File Offset: 0x000BF768
	protected BasicHealthKit(T db) : base(db)
	{
	}
}
