using System;

// Token: 0x020005B9 RID: 1465
public abstract class BloodDrawItem<T> : InventoryItem<T> where T : BloodDrawDatablock
{
	// Token: 0x06003503 RID: 13571 RVA: 0x000C1694 File Offset: 0x000BF894
	protected BloodDrawItem(T db) : base(db)
	{
	}
}
