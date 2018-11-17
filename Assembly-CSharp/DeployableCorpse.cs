using System;

// Token: 0x0200064B RID: 1611
public class DeployableCorpse : IDMain
{
	// Token: 0x0600381E RID: 14366 RVA: 0x000CE158 File Offset: 0x000CC358
	public DeployableCorpse() : this(0)
	{
	}

	// Token: 0x0600381F RID: 14367 RVA: 0x000CE164 File Offset: 0x000CC364
	protected DeployableCorpse(IDFlags flags) : base(flags)
	{
	}

	// Token: 0x04001C3C RID: 7228
	private float lifeTime = 300f;
}
