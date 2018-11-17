using System;

// Token: 0x02000152 RID: 338
public struct RepairEvent
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00028F80 File Offset: 0x00027180
	public IDMain beneficiary
	{
		get
		{
			return (!this.receiver) ? null : this.receiver.idMain;
		}
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00028FA4 File Offset: 0x000271A4
	public override string ToString()
	{
		return string.Format("[RepairEvent: beneficiary={0} givenAmount={1} usedAmount={5} status={2} doner={3} receiver={4}]", new object[]
		{
			this.beneficiary,
			this.givenAmount,
			this.status,
			this.doner,
			this.receiver,
			this.usedAmount
		});
	}

	// Token: 0x040006A3 RID: 1699
	public IDBase doner;

	// Token: 0x040006A4 RID: 1700
	public TakeDamage receiver;

	// Token: 0x040006A5 RID: 1701
	public float givenAmount;

	// Token: 0x040006A6 RID: 1702
	public float usedAmount;

	// Token: 0x040006A7 RID: 1703
	public RepairStatus status;
}
