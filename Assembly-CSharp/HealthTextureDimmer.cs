using System;

// Token: 0x02000599 RID: 1433
[Obsolete("Use HealthDimmer as a private field, and call UpdateHeathAmount on that instead of using this component")]
public class HealthTextureDimmer : IDLocal
{
	// Token: 0x06002E7F RID: 11903 RVA: 0x000B2BD4 File Offset: 0x000B0DD4
	public void UpdateHealthAmount(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x000B2BE4 File Offset: 0x000B0DE4
	protected void OnPoolRetire()
	{
		this.healthDimmer.Reset();
	}

	// Token: 0x04001912 RID: 6418
	[NonSerialized]
	private global::HealthDimmer healthDimmer;
}
