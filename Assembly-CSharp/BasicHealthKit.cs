using System;

// Token: 0x02000673 RID: 1651
public abstract class BasicHealthKit<T> : global::InventoryItem<T> where T : global::BasicHealthKitDataBlock
{
	// Token: 0x060038BC RID: 14524 RVA: 0x000C97C4 File Offset: 0x000C79C4
	protected BasicHealthKit(T db) : base(db)
	{
	}
}
