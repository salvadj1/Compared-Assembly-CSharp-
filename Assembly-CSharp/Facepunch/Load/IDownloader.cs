using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000263 RID: 611
	public interface IDownloader
	{
		// Token: 0x06001655 RID: 5717
		void BeginJob(Job job);

		// Token: 0x06001656 RID: 5718
		float GetDownloadProgress(Job job);

		// Token: 0x06001657 RID: 5719
		AssetBundle GetLoadedAssetBundle(Job job);

		// Token: 0x06001658 RID: 5720
		void OnJobCompleted(Job job);
	}
}
