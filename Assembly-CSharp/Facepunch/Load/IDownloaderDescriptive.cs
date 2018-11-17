using System;

namespace Facepunch.Load
{
	// Token: 0x02000297 RID: 663
	public interface IDownloaderDescriptive : IDownloader
	{
		// Token: 0x060017AD RID: 6061
		bool DescribeProgress(Job job, ref string lastString);
	}
}
