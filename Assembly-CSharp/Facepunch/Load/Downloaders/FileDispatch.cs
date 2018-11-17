using System;
using System.IO;
using UnityEngine;

namespace Facepunch.Load.Downloaders
{
	// Token: 0x02000290 RID: 656
	public sealed class FileDispatch : IDownloaderDispatch
	{
		// Token: 0x0600177C RID: 6012 RVA: 0x000588D4 File Offset: 0x00056AD4
		public void BindLoader(Loader loader)
		{
			this.wwwFallback.BindLoader(loader);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000588E4 File Offset: 0x00056AE4
		public void UnbindLoader(Loader loader)
		{
			this.wwwFallback.BindLoader(loader);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000588F4 File Offset: 0x00056AF4
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

		// Token: 0x0600177F RID: 6015 RVA: 0x000589AC File Offset: 0x00056BAC
		public void DeleteDownloader(Job job, IDownloader downloader)
		{
			if (!(downloader is FileDispatch.Downloader))
			{
				this.wwwFallback.DeleteDownloader(job, downloader);
			}
		}

		// Token: 0x04000C6F RID: 3183
		private WWWDispatch wwwFallback = new WWWDispatch();

		// Token: 0x02000291 RID: 657
		private class Downloader : IDownloader
		{
			// Token: 0x06001781 RID: 6017 RVA: 0x000589D0 File Offset: 0x00056BD0
			public void BeginJob(Job job)
			{
				job.OnDownloadingBegin(this);
				job.OnDownloadingComplete();
			}

			// Token: 0x06001782 RID: 6018 RVA: 0x000589E0 File Offset: 0x00056BE0
			public float GetDownloadProgress(Job job)
			{
				return 0f;
			}

			// Token: 0x06001783 RID: 6019 RVA: 0x000589E8 File Offset: 0x00056BE8
			public AssetBundle GetLoadedAssetBundle(Job job)
			{
				return this.bundle;
			}

			// Token: 0x06001784 RID: 6020 RVA: 0x000589F0 File Offset: 0x00056BF0
			public void OnJobCompleted(Job job)
			{
				this.bundle = null;
			}

			// Token: 0x04000C70 RID: 3184
			public AssetBundle bundle;
		}
	}
}
