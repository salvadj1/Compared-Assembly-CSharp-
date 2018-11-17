using System;

// Token: 0x020005EA RID: 1514
public interface IToolItem : IInventoryItem
{
	// Token: 0x17000AC6 RID: 2758
	// (get) Token: 0x0600363B RID: 13883
	bool canWork { get; }

	// Token: 0x0600363C RID: 13884
	void StartWork();

	// Token: 0x0600363D RID: 13885
	void CancelWork();

	// Token: 0x0600363E RID: 13886
	void CompleteWork();

	// Token: 0x17000AC7 RID: 2759
	// (get) Token: 0x0600363F RID: 13887
	float workDuration { get; }
}
