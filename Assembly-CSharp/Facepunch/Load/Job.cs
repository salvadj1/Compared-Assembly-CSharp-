using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x0200029A RID: 666
	public sealed class Job : IDownloadTask
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x0005902C File Offset: 0x0005722C
		public Job()
		{
			this.TaskStatus = TaskStatus.Pending;
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0005903C File Offset: 0x0005723C
		int IDownloadTask.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00059040 File Offset: 0x00057240
		int IDownloadTask.Done
		{
			get
			{
				return (this.TaskStatus != TaskStatus.Complete) ? 0 : 1;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00059058 File Offset: 0x00057258
		TaskStatusCount IDownloadTask.TaskStatusCount
		{
			get
			{
				switch (this.TaskStatus)
				{
				case TaskStatus.Pending:
					return TaskStatusCount.OnePending;
				case TaskStatus.Downloading:
					return TaskStatusCount.OneDownloading;
				case TaskStatus.Complete:
					return TaskStatusCount.OneComplete;
				}
				throw new ArgumentOutOfRangeException("TaskStatus");
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000590A8 File Offset: 0x000572A8
		public Loader Loader
		{
			get
			{
				return this._op.Loader;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000590B8 File Offset: 0x000572B8
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x000590C0 File Offset: 0x000572C0
		public Group Group { get; internal set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000590CC File Offset: 0x000572CC
		public string Name
		{
			get
			{
				return this.Item.Name;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000590DC File Offset: 0x000572DC
		public string Path
		{
			get
			{
				return this.Item.Path;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000590EC File Offset: 0x000572EC
		public int ByteLength
		{
			get
			{
				return this.Item.ByteLength;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000590FC File Offset: 0x000572FC
		public ContentType ContentType
		{
			get
			{
				return this.Item.ContentType;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0005910C File Offset: 0x0005730C
		public Type TypeOfAssets
		{
			get
			{
				return this.Item.TypeOfAssets;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0005911C File Offset: 0x0005731C
		public int ByteLengthDownloaded
		{
			get
			{
				return Mathf.FloorToInt(this.PercentDone * (float)this.ByteLength);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00059134 File Offset: 0x00057334
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x0005913C File Offset: 0x0005733C
		internal AssetBundle AssetBundle { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00059148 File Offset: 0x00057348
		public float PercentDone
		{
			get
			{
				switch (this.TaskStatus)
				{
				case TaskStatus.Pending:
					return 0f;
				case TaskStatus.Downloading:
					return this.downloader.GetDownloadProgress(this);
				case TaskStatus.Complete:
					return 1f;
				}
				throw new ArgumentOutOfRangeException("TaskStatus");
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0005919C File Offset: 0x0005739C
		public string ContextualDescription
		{
			get
			{
				switch (this.TaskStatus)
				{
				case TaskStatus.Pending:
					return "Pending";
				case TaskStatus.Downloading:
					return (!this.hasDescriptiveDownloader || !this.descriptiveDownloader.DescribeProgress(this, ref this.lastDescriptiveString)) ? "Downloading" : (this.lastDescriptiveString ?? string.Empty);
				case TaskStatus.Complete:
					return "Complete";
				}
				throw new ArgumentOutOfRangeException("TaskStatus");
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00059224 File Offset: 0x00057424
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x0005922C File Offset: 0x0005742C
		public TaskStatus TaskStatus { get; private set; }

		// Token: 0x060017C5 RID: 6085 RVA: 0x00059238 File Offset: 0x00057438
		public void OnDownloadingBegin(IDownloader downloader)
		{
			this.downloader = downloader;
			this.lastDescriptiveString = null;
			this.hasDescriptiveDownloader = ((this.descriptiveDownloader = (downloader as IDownloaderDescriptive)) != null);
			this.TaskStatus = TaskStatus.Downloading;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00059278 File Offset: 0x00057478
		public void OnDownloadingComplete()
		{
			this.TaskStatus = TaskStatus.Complete;
			IDownloader downloader = this.downloader;
			this.downloader = null;
			this.descriptiveDownloader = null;
			this.hasDescriptiveDownloader = false;
			this.Loader.OnJobCompleted(this, downloader);
			if (!object.ReferenceEquals(this.Tag, null))
			{
				Debug.LogWarning("Clearing tag manually ( should have been done by the IDownloader during the OnJobComplete callback )");
			}
			this.Tag = null;
		}

		// Token: 0x04000C81 RID: 3201
		[NonSerialized]
		public Operation _op;

		// Token: 0x04000C82 RID: 3202
		[NonSerialized]
		public Item Item;

		// Token: 0x04000C83 RID: 3203
		private IDownloaderDescriptive descriptiveDownloader;

		// Token: 0x04000C84 RID: 3204
		private bool hasDescriptiveDownloader;

		// Token: 0x04000C85 RID: 3205
		private string lastDescriptiveString;

		// Token: 0x04000C86 RID: 3206
		public object Tag;

		// Token: 0x04000C87 RID: 3207
		private IDownloader downloader;
	}
}
