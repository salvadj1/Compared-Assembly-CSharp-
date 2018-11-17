using System;

// Token: 0x020005C7 RID: 1479
public abstract class GunpowderItem<T> : InventoryItem<T> where T : GunpowderDataBlock
{
	// Token: 0x06003557 RID: 13655 RVA: 0x000C261C File Offset: 0x000C081C
	protected GunpowderItem(T db) : base(db)
	{
	}
}
