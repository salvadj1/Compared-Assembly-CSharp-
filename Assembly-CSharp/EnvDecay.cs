using System;

// Token: 0x02000545 RID: 1349
public class EnvDecay : IDLocal
{
	// Token: 0x06002D0E RID: 11534 RVA: 0x000A88BC File Offset: 0x000A6ABC
	public void Awake()
	{
		base.enabled = false;
	}

	// Token: 0x040016D6 RID: 5846
	public float decayMultiplier = 1f;

	// Token: 0x040016D7 RID: 5847
	public bool ambientDecay;

	// Token: 0x040016D8 RID: 5848
	protected float lastDecayThink;

	// Token: 0x040016D9 RID: 5849
	protected global::TakeDamage _takeDamage;

	// Token: 0x040016DA RID: 5850
	protected global::DeployableObject _deployable;
}
