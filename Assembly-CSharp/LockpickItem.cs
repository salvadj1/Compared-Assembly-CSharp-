using System;

// Token: 0x02000699 RID: 1689
public abstract class LockpickItem<T> : global::InventoryItem<T> where T : global::LockpickItemDataBlock
{
	// Token: 0x060039C1 RID: 14785 RVA: 0x000CB7BC File Offset: 0x000C99BC
	protected LockpickItem(T db) : base(db)
	{
	}
}
