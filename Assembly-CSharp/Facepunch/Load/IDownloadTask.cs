using System;

namespace Facepunch.Load
{
	// Token: 0x02000295 RID: 661
	public interface IDownloadTask
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060017A0 RID: 6048
		int ByteLength { get; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060017A1 RID: 6049
		int ByteLengthDownloaded { get; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060017A2 RID: 6050
		float PercentDone { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060017A3 RID: 6051
		TaskStatus TaskStatus { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060017A4 RID: 6052
		string Name { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060017A5 RID: 6053
		string ContextualDescription { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060017A6 RID: 6054
		int Count { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060017A7 RID: 6055
		int Done { get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060017A8 RID: 6056
		TaskStatusCount TaskStatusCount { get; }
	}
}
