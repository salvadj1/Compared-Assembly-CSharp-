using System;

// Token: 0x02000670 RID: 1648
public class Ragdoll : Character
{
	// Token: 0x06003947 RID: 14663 RVA: 0x000D2828 File Offset: 0x000D0A28
	public Ragdoll() : this(0)
	{
	}

	// Token: 0x06003948 RID: 14664 RVA: 0x000D2834 File Offset: 0x000D0A34
	protected Ragdoll(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x06003949 RID: 14665 RVA: 0x000D2840 File Offset: 0x000D0A40
	protected new void Awake()
	{
		base.LoadTraitMapNonNetworked();
		base.Awake();
	}

	// Token: 0x04001D50 RID: 7504
	[NonSerialized]
	public IDMain sourceMain;
}
