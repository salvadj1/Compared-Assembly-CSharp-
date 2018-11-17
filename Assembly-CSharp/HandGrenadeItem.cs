using System;

// Token: 0x02000687 RID: 1671
public abstract class HandGrenadeItem<T> : global::ThrowableItem<T> where T : global::HandGrenadeDataBlock
{
	// Token: 0x06003920 RID: 14624 RVA: 0x000CA884 File Offset: 0x000C8A84
	protected HandGrenadeItem(T db) : base(db)
	{
	}
}
