using System;

// Token: 0x0200066D RID: 1645
public abstract class AmmoItem<T> : global::InventoryItem<T> where T : global::AmmoItemDataBlock
{
	// Token: 0x060038A1 RID: 14497 RVA: 0x000C94A4 File Offset: 0x000C76A4
	protected AmmoItem(T db) : base(db)
	{
	}
}
