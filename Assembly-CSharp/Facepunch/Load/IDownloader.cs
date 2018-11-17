using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000296 RID: 662
	public interface IDownloader
	{
		// Token: 0x060017A9 RID: 6057
		void BeginJob(Job job);

		// Token: 0x060017AA RID: 6058
		float GetDownloadProgress(Job job);

		// Token: 0x060017AB RID: 6059
		AssetBundle GetLoadedAssetBundle(Job job);

		// Token: 0x060017AC RID: 6060
		void OnJobCompleted(Job job);
	}
}
