using System;

namespace Facepunch.Load
{
	// Token: 0x02000265 RID: 613
	public interface IDownloaderDispatch
	{
		// Token: 0x0600165A RID: 5722
		void BindLoader(Loader loader);

		// Token: 0x0600165B RID: 5723
		void UnbindLoader(Loader loader);

		// Token: 0x0600165C RID: 5724
		IDownloader CreateDownloaderForJob(Job job);

		// Token: 0x0600165D RID: 5725
		void DeleteDownloader(Job job, IDownloader downloader);
	}
}
