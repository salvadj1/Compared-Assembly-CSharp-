using System;

namespace Facepunch.Load
{
	// Token: 0x02000262 RID: 610
	public interface IDownloadTask
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600164C RID: 5708
		int ByteLength { get; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600164D RID: 5709
		int ByteLengthDownloaded { get; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600164E RID: 5710
		float PercentDone { get; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600164F RID: 5711
		TaskStatus TaskStatus { get; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001650 RID: 5712
		string Name { get; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001651 RID: 5713
		string ContextualDescription { get; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001652 RID: 5714
		int Count { get; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001653 RID: 5715
		int Done { get; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001654 RID: 5716
		TaskStatusCount TaskStatusCount { get; }
	}
}
