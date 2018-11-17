using System;
using UnityEngine;

namespace Facepunch.Load.Downloaders
{
	// Token: 0x02000292 RID: 658
	public sealed class WWWDispatch : IDownloaderDispatch
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x00058A14 File Offset: 0x00056C14
		public void BindLoader(Loader loader)
		{
			this.coroutineRunner.Retain();
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00058A24 File Offset: 0x00056C24
		public void UnbindLoader(Loader loader)
		{
			this.coroutineRunner.Release();
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00058A34 File Offset: 0x00056C34
		public IDownloader CreateDownloaderForJob(Job job)
		{
			return new WWWDispatch.Downloader(this);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00058A3C File Offset: 0x00056C3C
		public void DeleteDownloader(Job job, IDownloader idownloader)
		{
			if (idownloader is WWWDispatch.Downloader)
			{
				WWWDispatch.Downloader downloader = (WWWDispatch.Downloader)idownloader;
				downloader.Job = null;
				downloader.Download = null;
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00058A6C File Offset: 0x00056C6C
		private void DownloadBegin(WWWDispatch.Downloader downloader, Job job)
		{
			downloader.Job = job;
			downloader.Download = new WWW(job.Path);
			job.OnDownloadingBegin(downloader);
			if (downloader.Download.isDone)
			{
				this.DownloadFinished(downloader);
			}
			else
			{
				this.coroutineRunner.Install(WWWDispatch.Downloader.DownloaderRoutineCallback, downloader, downloader.Download, true);
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00058AD0 File Offset: 0x00056CD0
		private void DownloadFinished(WWWDispatch.Downloader downloader)
		{
			downloader.Job.OnDownloadingComplete();
		}

		// Token: 0x04000C71 RID: 3185
		private readonly Utility.ReferenceCountedCoroutine.Runner coroutineRunner = new Utility.ReferenceCountedCoroutine.Runner("WWWDispatch");

		// Token: 0x02000293 RID: 659
		private class Downloader : IDownloader
		{
			// Token: 0x0600178C RID: 6028 RVA: 0x00058AE0 File Offset: 0x00056CE0
			public Downloader(WWWDispatch dispatch)
			{
				this.Dispatch = dispatch;
			}

			// Token: 0x0600178E RID: 6030 RVA: 0x00058B04 File Offset: 0x00056D04
			public void BeginJob(Job job)
			{
				this.Dispatch.DownloadBegin(this, job);
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x00058B14 File Offset: 0x00056D14
			public float GetDownloadProgress(Job job)
			{
				return (this.Download != null) ? this.Download.progress : 0f;
			}

			// Token: 0x06001790 RID: 6032 RVA: 0x00058B44 File Offset: 0x00056D44
			public AssetBundle GetLoadedAssetBundle(Job job)
			{
				return this.Download.assetBundle;
			}

			// Token: 0x06001791 RID: 6033 RVA: 0x00058B54 File Offset: 0x00056D54
			public void OnJobCompleted(Job job)
			{
				if (this.Job == job)
				{
					this.Download.Dispose();
					this.Download = null;
					this.Job = null;
				}
			}

			// Token: 0x06001792 RID: 6034 RVA: 0x00058B7C File Offset: 0x00056D7C
			private static bool DownloaderRoutine(ref object yieldInstruction, ref object tag)
			{
				WWWDispatch.Downloader downloader = (WWWDispatch.Downloader)tag;
				yieldInstruction = downloader.Download;
				if (downloader.Download.isDone)
				{
					downloader.Dispatch.DownloadFinished(downloader);
					return false;
				}
				return true;
			}

			// Token: 0x04000C72 RID: 3186
			public readonly WWWDispatch Dispatch;

			// Token: 0x04000C73 RID: 3187
			public WWW Download;

			// Token: 0x04000C74 RID: 3188
			public Job Job;

			// Token: 0x04000C75 RID: 3189
			public static readonly Utility.ReferenceCountedCoroutine.Callback DownloaderRoutineCallback = new Utility.ReferenceCountedCoroutine.Callback(WWWDispatch.Downloader.DownloaderRoutine);
		}
	}
}
