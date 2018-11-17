using System;

// Token: 0x020005C9 RID: 1481
public abstract class HandGrenadeItem<T> : ThrowableItem<T> where T : HandGrenadeDataBlock
{
	// Token: 0x06003558 RID: 13656 RVA: 0x000C2628 File Offset: 0x000C0828
	protected HandGrenadeItem(T db) : base(db)
	{
	}
}
