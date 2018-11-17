using System;

// Token: 0x02000705 RID: 1797
public interface ICarriableTrans
{
	// Token: 0x06003BE5 RID: 15333
	void OnAddedToCarrier(global::TransCarrier carrier);

	// Token: 0x06003BE6 RID: 15334
	void OnDroppedFromCarrier(global::TransCarrier carrier);
}
