using System;

// Token: 0x02000642 RID: 1602
public interface ICarriableTrans
{
	// Token: 0x060037F9 RID: 14329
	void OnAddedToCarrier(TransCarrier carrier);

	// Token: 0x060037FA RID: 14330
	void OnDroppedFromCarrier(TransCarrier carrier);
}
