using System;

// Token: 0x0200017C RID: 380
public struct RepairEvent
{
	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002CCFC File Offset: 0x0002AEFC
	public IDMain beneficiary
	{
		get
		{
			return (!this.receiver) ? null : this.receiver.idMain;
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0002CD20 File Offset: 0x0002AF20
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

	// Token: 0x040007B2 RID: 1970
	public IDBase doner;

	// Token: 0x040007B3 RID: 1971
	public global::TakeDamage receiver;

	// Token: 0x040007B4 RID: 1972
	public float givenAmount;

	// Token: 0x040007B5 RID: 1973
	public float usedAmount;

	// Token: 0x040007B6 RID: 1974
	public global::RepairStatus status;
}
