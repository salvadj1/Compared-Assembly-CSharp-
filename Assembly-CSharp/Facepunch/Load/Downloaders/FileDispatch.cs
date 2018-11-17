using System;
using System.IO;
using UnityEngine;

namespace Facepunch.Load.Downloaders
{
	// Token: 0x0200025D RID: 605
	public sealed class FileDispatch : IDownloaderDispatch
	{
		// Token: 0x06001628 RID: 5672 RVA: 0x0005452C File Offset: 0x0005272C
		public void BindLoader(Loader loader)
		{
			this.wwwFallback.BindLoader(loader);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005453C File Offset: 0x0005273C
		public void UnbindLoader(Loader loader)
		{
			this.wwwFallback.BindLoader(loader);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0005454C File Offset: 0x0005274C
		public IDownloader CreateDownloaderForJob(Job job)
		{
			if (File.Exists(job.Path))
			{
				FileInfo fileInfo = new FileInfo(job.Path);
				bool flag = fileInfo.Length == (long)job.ByteLength;
				if (flag)
				{
					AssetBundle assetBundle = AssetBundle.CreateFromFile(Path.GetFullPath(job.Path).Replace('\\', '/'));
					if (assetBundle)
					{
						return new FileDispatch.Downloader
						{
							bundle = assetBundle
						};
					}
				}
			}
			Debug.LogWarning("Missing Bundle " + job.Path);
			if (job.ContentType != ContentType.Assets || job.TypeOfAssets == typeof(NavMesh))
			{
				return this.wwwFallback.CreateDownloaderForJob(job);
			}
			return null;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00054604 File Offset: 0x00052804
		public void DeleteDownloader(Job job, IDownloader downloader)
		{
			if (!(downloader is FileDispatch.Downloader))
			{
				this.wwwFallback.DeleteDownloader(job, downloader);
			}
		}

		// Token: 0x04000B4C RID: 2892
		private WWWDispatch wwwFallback = new WWWDispatch();

		// Token: 0x0200025E RID: 606
		private class Downloader : IDownloader
		{
			// Token: 0x0600162D RID: 5677 RVA: 0x00054628 File Offset: 0x00052828
			public void BeginJob(Job job)
			{
				job.OnDownloadingBegin(this);
				job.OnDownloadingComplete();
			}

			// Token: 0x0600162E RID: 5678 RVA: 0x00054638 File Offset: 0x00052838
			public float GetDownloadProgress(Job job)
			{
				return 0f;
			}

			// Token: 0x0600162F RID: 5679 RVA: 0x00054640 File Offset: 0x00052840
			public AssetBundle GetLoadedAssetBundle(Job job)
			{
				return this.bundle;
			}

			// Token: 0x06001630 RID: 5680 RVA: 0x00054648 File Offset: 0x00052848
			public void OnJobCompleted(Job job)
			{
				this.bundle = null;
			}

			// Token: 0x04000B4D RID: 2893
			public AssetBundle bundle;
		}
	}
}
