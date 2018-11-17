using System;
using UnityEngine;

namespace Facepunch.Load.Downloaders
{
	// Token: 0x0200025F RID: 607
	public sealed class WWWDispatch : IDownloaderDispatch
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x0005466C File Offset: 0x0005286C
		public void BindLoader(Loader loader)
		{
			this.coroutineRunner.Retain();
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0005467C File Offset: 0x0005287C
		public void UnbindLoader(Loader loader)
		{
			this.coroutineRunner.Release();
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0005468C File Offset: 0x0005288C
		public IDownloader CreateDownloaderForJob(Job job)
		{
			return new WWWDispatch.Downloader(this);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00054694 File Offset: 0x00052894
		public void DeleteDownloader(Job job, IDownloader idownloader)
		{
			if (idownloader is WWWDispatch.Downloader)
			{
				WWWDispatch.Downloader downloader = (WWWDispatch.Downloader)idownloader;
				downloader.Job = null;
				downloader.Download = null;
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000546C4 File Offset: 0x000528C4
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

		// Token: 0x06001637 RID: 5687 RVA: 0x00054728 File Offset: 0x00052928
		private void DownloadFinished(WWWDispatch.Downloader downloader)
		{
			downloader.Job.OnDownloadingComplete();
		}

		// Token: 0x04000B4E RID: 2894
		private readonly Utility.ReferenceCountedCoroutine.Runner coroutineRunner = new Utility.ReferenceCountedCoroutine.Runner("WWWDispatch");

		// Token: 0x02000260 RID: 608
		private class Downloader : IDownloader
		{
			// Token: 0x06001638 RID: 5688 RVA: 0x00054738 File Offset: 0x00052938
			public Downloader(WWWDispatch dispatch)
			{
				this.Dispatch = dispatch;
			}

			// Token: 0x0600163A RID: 5690 RVA: 0x0005475C File Offset: 0x0005295C
			public void BeginJob(Job job)
			{
				this.Dispatch.DownloadBegin(this, job);
			}

			// Token: 0x0600163B RID: 5691 RVA: 0x0005476C File Offset: 0x0005296C
			public float GetDownloadProgress(Job job)
			{
				return (this.Download != null) ? this.Download.progress : 0f;
			}

			// Token: 0x0600163C RID: 5692 RVA: 0x0005479C File Offset: 0x0005299C
			public AssetBundle GetLoadedAssetBundle(Job job)
			{
				return this.Download.assetBundle;
			}

			// Token: 0x0600163D RID: 5693 RVA: 0x000547AC File Offset: 0x000529AC
			public void OnJobCompleted(Job job)
			{
				if (this.Job == job)
				{
					this.Download.Dispose();
					this.Download = null;
					this.Job = null;
				}
			}

			// Token: 0x0600163E RID: 5694 RVA: 0x000547D4 File Offset: 0x000529D4
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

			// Token: 0x04000B4F RID: 2895
			public readonly WWWDispatch Dispatch;

			// Token: 0x04000B50 RID: 2896
			public WWW Download;

			// Token: 0x04000B51 RID: 2897
			public Job Job;

			// Token: 0x04000B52 RID: 2898
			public static readonly Utility.ReferenceCountedCoroutine.Callback DownloaderRoutineCallback = new Utility.ReferenceCountedCoroutine.Callback(WWWDispatch.Downloader.DownloaderRoutine);
		}
	}
}
