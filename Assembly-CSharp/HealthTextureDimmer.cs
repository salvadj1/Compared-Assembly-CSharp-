using System;

// Token: 0x020004DE RID: 1246
[Obsolete("Use HealthDimmer as a private field, and call UpdateHeathAmount on that instead of using this component")]
public class HealthTextureDimmer : IDLocal
{
	// Token: 0x06002ACD RID: 10957 RVA: 0x000AAE3C File Offset: 0x000A903C
	public void UpdateHealthAmount(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x06002ACE RID: 10958 RVA: 0x000AAE4C File Offset: 0x000A904C
	protected void OnPoolRetire()
	{
		this.healthDimmer.Reset();
	}

	// Token: 0x04001755 RID: 5973
	[NonSerialized]
	private HealthDimmer healthDimmer;
}
