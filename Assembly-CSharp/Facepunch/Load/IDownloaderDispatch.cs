using System;

namespace Facepunch.Load
{
	// Token: 0x02000298 RID: 664
	public interface IDownloaderDispatch
	{
		// Token: 0x060017AE RID: 6062
		void BindLoader(Loader loader);

		// Token: 0x060017AF RID: 6063
		void UnbindLoader(Loader loader);

		// Token: 0x060017B0 RID: 6064
		IDownloader CreateDownloaderForJob(Job job);

		// Token: 0x060017B1 RID: 6065
		void DeleteDownloader(Job job, IDownloader downloader);
	}
}
