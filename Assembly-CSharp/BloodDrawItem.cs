using System;

// Token: 0x02000677 RID: 1655
public abstract class BloodDrawItem<T> : global::InventoryItem<T> where T : global::BloodDrawDatablock
{
	// Token: 0x060038CB RID: 14539 RVA: 0x000C98F0 File Offset: 0x000C7AF0
	protected BloodDrawItem(T db) : base(db)
	{
	}
}
