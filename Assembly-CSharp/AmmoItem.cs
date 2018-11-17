using System;

// Token: 0x020005AF RID: 1455
public abstract class AmmoItem<T> : InventoryItem<T> where T : AmmoItemDataBlock
{
	// Token: 0x060034D9 RID: 13529 RVA: 0x000C1248 File Offset: 0x000BF448
	protected AmmoItem(T db) : base(db)
	{
	}
}
