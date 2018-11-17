using System;

// Token: 0x02000734 RID: 1844
public class Ragdoll : global::Character
{
	// Token: 0x06003D3B RID: 15675 RVA: 0x000DB208 File Offset: 0x000D9408
	public Ragdoll() : this(0)
	{
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x000DB214 File Offset: 0x000D9414
	protected Ragdoll(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x000DB220 File Offset: 0x000D9420
	protected new void Awake()
	{
		base.LoadTraitMapNonNetworked();
		base.Awake();
	}

	// Token: 0x04001F48 RID: 8008
	[NonSerialized]
	public IDMain sourceMain;
}
