using System;

// Token: 0x020005DB RID: 1499
public abstract class LockpickItem<T> : InventoryItem<T> where T : LockpickItemDataBlock
{
	// Token: 0x060035F9 RID: 13817 RVA: 0x000C3560 File Offset: 0x000C1760
	protected LockpickItem(T db) : base(db)
	{
	}
}
