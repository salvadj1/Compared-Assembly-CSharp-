using System;

// Token: 0x0200054E RID: 1358
public class StructureComponentTakeDamage : global::ProtectionTakeDamage
{
	// Token: 0x06002D68 RID: 11624 RVA: 0x000AB4F8 File Offset: 0x000A96F8
	protected override global::LifeStatus Hurt(ref global::DamageEvent damage)
	{
		return base.Hurt(ref damage);
	}
}
