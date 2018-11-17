using System;

// Token: 0x0200048A RID: 1162
public class EnvDecay : IDLocal
{
	// Token: 0x0600295C RID: 10588 RVA: 0x000A24C0 File Offset: 0x000A06C0
	public void Awake()
	{
		base.enabled = false;
	}

	// Token: 0x04001540 RID: 5440
	public float decayMultiplier = 1f;

	// Token: 0x04001541 RID: 5441
	public bool ambientDecay;

	// Token: 0x04001542 RID: 5442
	protected float lastDecayThink;

	// Token: 0x04001543 RID: 5443
	protected TakeDamage _takeDamage;

	// Token: 0x04001544 RID: 5444
	protected DeployableObject _deployable;
}
