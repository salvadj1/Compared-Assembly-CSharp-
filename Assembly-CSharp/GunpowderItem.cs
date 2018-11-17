using System;

// Token: 0x02000685 RID: 1669
public abstract class GunpowderItem<T> : global::InventoryItem<T> where T : global::GunpowderDataBlock
{
	// Token: 0x0600391F RID: 14623 RVA: 0x000CA878 File Offset: 0x000C8A78
	protected GunpowderItem(T db) : base(db)
	{
	}
}
