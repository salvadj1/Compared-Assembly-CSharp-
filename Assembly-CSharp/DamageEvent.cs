using System;

// Token: 0x02000150 RID: 336
public struct DamageEvent
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00028F10 File Offset: 0x00027110
	public BodyPart bodyPart
	{
		get
		{
			return this.victim.bodyPart;
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00028F20 File Offset: 0x00027120
	public override string ToString()
	{
		return string.Format("{{attacker={3}, victim={0}, amount={1}, status={2}, sender={4}}}", new object[]
		{
			this.victim,
			this.amount,
			this.status,
			this.attacker,
			this.sender
		});
	}

	// Token: 0x04000695 RID: 1685
	public DamageBeing attacker;

	// Token: 0x04000696 RID: 1686
	public DamageBeing victim;

	// Token: 0x04000697 RID: 1687
	public TakeDamage sender;

	// Token: 0x04000698 RID: 1688
	public LifeStatus status;

	// Token: 0x04000699 RID: 1689
	public DamageTypeFlags damageTypes;

	// Token: 0x0400069A RID: 1690
	public float amount;

	// Token: 0x0400069B RID: 1691
	public object extraData;
}
