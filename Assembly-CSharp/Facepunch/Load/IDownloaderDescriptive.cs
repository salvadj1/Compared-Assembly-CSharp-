using System;

namespace Facepunch.Load
{
	// Token: 0x02000264 RID: 612
	public interface IDownloaderDescriptive : IDownloader
	{
		// Token: 0x06001659 RID: 5721
		bool DescribeProgress(Job job, ref string lastString);
	}
}
