using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000267 RID: 615
	public sealed class Job : IDownloadTask
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x00054C84 File Offset: 0x00052E84
		public Job()
		{
			this.TaskStatus = TaskStatus.Pending;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00054C94 File Offset: 0x00052E94
		int IDownloadTask.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00054C98 File Offset: 0x00052E98
		int IDownloadTask.Done
		{
			get
			{
				return (this.TaskStatus != TaskStatus.Complete) ? 0 : 1;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00054CB0 File Offset: 0x00052EB0
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

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00054D00 File Offset: 0x00052F00
		public Loader Loader
		{
			get
			{
				return this._op.Loader;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00054D10 File Offset: 0x00052F10
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x00054D18 File Offset: 0x00052F18
		public Group Group { get; internal set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00054D24 File Offset: 0x00052F24
		public string Name
		{
			get
			{
				return this.Item.Name;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x00054D34 File Offset: 0x00052F34
		public string Path
		{
			get
			{
				return this.Item.Path;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00054D44 File Offset: 0x00052F44
		public int ByteLength
		{
			get
			{
				return this.Item.ByteLength;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00054D54 File Offset: 0x00052F54
		public ContentType ContentType
		{
			get
			{
				return this.Item.ContentType;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00054D64 File Offset: 0x00052F64
		public Type TypeOfAssets
		{
			get
			{
				return this.Item.TypeOfAssets;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00054D74 File Offset: 0x00052F74
		public int ByteLengthDownloaded
		{
			get
			{
				return Mathf.FloorToInt(this.PercentDone * (float)this.ByteLength);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00054D8C File Offset: 0x00052F8C
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x00054D94 File Offset: 0x00052F94
		internal AssetBundle AssetBundle { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00054DA0 File Offset: 0x00052FA0
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x00054DF4 File Offset: 0x00052FF4
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

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00054E7C File Offset: 0x0005307C
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x00054E84 File Offset: 0x00053084
		public TaskStatus TaskStatus { get; private set; }

		// Token: 0x06001671 RID: 5745 RVA: 0x00054E90 File Offset: 0x00053090
		public void OnDownloadingBegin(IDownloader downloader)
		{
			this.downloader = downloader;
			this.lastDescriptiveString = null;
			this.hasDescriptiveDownloader = ((this.descriptiveDownloader = (downloader as IDownloaderDescriptive)) != null);
			this.TaskStatus = TaskStatus.Downloading;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00054ED0 File Offset: 0x000530D0
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

		// Token: 0x04000B5E RID: 2910
		[NonSerialized]
		public Operation _op;

		// Token: 0x04000B5F RID: 2911
		[NonSerialized]
		public Item Item;

		// Token: 0x04000B60 RID: 2912
		private IDownloaderDescriptive descriptiveDownloader;

		// Token: 0x04000B61 RID: 2913
		private bool hasDescriptiveDownloader;

		// Token: 0x04000B62 RID: 2914
		private string lastDescriptiveString;

		// Token: 0x04000B63 RID: 2915
		public object Tag;

		// Token: 0x04000B64 RID: 2916
		private IDownloader downloader;
	}
}
