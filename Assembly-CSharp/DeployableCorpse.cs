using System;

// Token: 0x0200070E RID: 1806
public class DeployableCorpse : IDMain
{
	// Token: 0x06003C0A RID: 15370 RVA: 0x000D6A08 File Offset: 0x000D4C08
	public DeployableCorpse() : this(0)
	{
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x000D6A14 File Offset: 0x000D4C14
	protected DeployableCorpse(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x04001E31 RID: 7729
	private float lifeTime = 300f;
}
