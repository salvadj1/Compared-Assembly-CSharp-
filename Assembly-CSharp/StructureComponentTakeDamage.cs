using System;

// Token: 0x02000493 RID: 1171
public class StructureComponentTakeDamage : ProtectionTakeDamage
{
	// Token: 0x060029B6 RID: 10678 RVA: 0x000A3760 File Offset: 0x000A1960
	protected override LifeStatus Hurt(ref DamageEvent damage)
	{
		return base.Hurt(ref damage);
	}
}
