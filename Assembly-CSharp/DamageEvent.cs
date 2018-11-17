using System;

// Token: 0x0200017A RID: 378
public struct DamageEvent
{
	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002CC8C File Offset: 0x0002AE8C
	public BodyPart bodyPart
	{
		get
		{
			return this.victim.bodyPart;
		}
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0002CC9C File Offset: 0x0002AE9C
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

	// Token: 0x040007A4 RID: 1956
	public global::DamageBeing attacker;

	// Token: 0x040007A5 RID: 1957
	public global::DamageBeing victim;

	// Token: 0x040007A6 RID: 1958
	public global::TakeDamage sender;

	// Token: 0x040007A7 RID: 1959
	public global::LifeStatus status;

	// Token: 0x040007A8 RID: 1960
	public global::DamageTypeFlags damageTypes;

	// Token: 0x040007A9 RID: 1961
	public float amount;

	// Token: 0x040007AA RID: 1962
	public object extraData;
}
