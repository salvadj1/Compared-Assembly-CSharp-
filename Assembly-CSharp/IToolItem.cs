using System;

// Token: 0x020006A8 RID: 1704
public interface IToolItem : global::IInventoryItem
{
	// Token: 0x17000B3C RID: 2876
	// (get) Token: 0x06003A03 RID: 14851
	bool canWork { get; }

	// Token: 0x06003A04 RID: 14852
	void StartWork();

	// Token: 0x06003A05 RID: 14853
	void CancelWork();

	// Token: 0x06003A06 RID: 14854
	void CompleteWork();

	// Token: 0x17000B3D RID: 2877
	// (get) Token: 0x06003A07 RID: 14855
	float workDuration { get; }
}
