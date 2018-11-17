using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000294 RID: 660
	public class Group : IDownloadTask
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00058BC0 File Offset: 0x00056DC0
		string IDownloadTask.Name
		{
			get
			{
				return this.jobDesc;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00058BC8 File Offset: 0x00056DC8
		int IDownloadTask.ByteLength
		{
			get
			{
				return this.ByteLength;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00058BD0 File Offset: 0x00056DD0
		public Loader Loader
		{
			get
			{
				return this._op.Loader;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00058BE0 File Offset: 0x00056DE0
		public int ByteLengthDownloaded
		{
			get
			{
				int num = 0;
				foreach (Job job in this.Jobs)
				{
					num += job.ByteLengthDownloaded;
				}
				return num;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00058C18 File Offset: 0x00056E18
		public TaskStatus TaskStatus
		{
			get
			{
				TaskStatus result = TaskStatus.Complete;
				foreach (Job job in this.Jobs)
				{
					if (job.TaskStatus == TaskStatus.Downloading)
					{
						return TaskStatus.Downloading;
					}
					if (job.TaskStatus == TaskStatus.Pending)
					{
						result = TaskStatus.Pending;
					}
				}
				return result;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00058C64 File Offset: 0x00056E64
		public TaskStatusCount TaskStatusCount
		{
			get
			{
				TaskStatusCount result = default(TaskStatusCount);
				foreach (Job job in this.Jobs)
				{
					ref TaskStatusCount ptr = ref result;
					TaskStatus taskStatus;
					TaskStatus status = taskStatus = job.TaskStatus;
					int num = ptr[taskStatus];
					result[status] = num + 1;
				}
				return result;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00058CC0 File Offset: 0x00056EC0
		public string ContextualDescription
		{
			get
			{
				TaskStatusCount taskStatusCount = this.TaskStatusCount;
				TaskStatusCount? taskStatusCount2 = this.lastStatusCount;
				TaskStatusCount taskStatusCount3 = (taskStatusCount2 == null) ? taskStatusCount : taskStatusCount2.Value;
				if (this.lastStatusCount == null || taskStatusCount3.Pending != taskStatusCount.Pending || taskStatusCount3.Complete != taskStatusCount.Complete || taskStatusCount3.Downloading != taskStatusCount.Downloading)
				{
					this.lastStatusCount = new TaskStatusCount?(taskStatusCount);
					switch ((byte)(taskStatusCount.TaskStatus & (TaskStatus.Pending | TaskStatus.Downloading | TaskStatus.Complete)))
					{
					case 1:
						this.lastDescriptiveText = string.Format("{0} pending", taskStatusCount.Pending);
						break;
					case 2:
						this.lastDescriptiveText = string.Format("{0} downloading", taskStatusCount.Downloading);
						break;
					case 3:
						this.lastDescriptiveText = string.Format("{0} pending, {1} downloading", taskStatusCount.Pending, taskStatusCount.Downloading);
						break;
					case 4:
						this.lastDescriptiveText = string.Format("{0} complete", taskStatusCount.Complete);
						break;
					case 5:
						this.lastDescriptiveText = string.Format("{0} pending, {1} complete", taskStatusCount.Pending, taskStatusCount.Downloading);
						break;
					case 6:
						this.lastDescriptiveText = string.Format("{0} downloading, {1} complete", taskStatusCount.Downloading, taskStatusCount.Complete);
						break;
					case 7:
						this.lastDescriptiveText = string.Format("{0} pending, {1} downloading, {2} complete", taskStatusCount.Pending, taskStatusCount.Downloading, taskStatusCount.Complete);
						break;
					default:
						throw new ArgumentException("TaskStatus");
					}
				}
				return this.lastDescriptiveText;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00058EB4 File Offset: 0x000570B4
		public float PercentDone
		{
			get
			{
				return (float)((double)this.ByteLengthDownloaded / (double)this.ByteLength);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00058EC8 File Offset: 0x000570C8
		public int Count
		{
			get
			{
				return this.Jobs.Length;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00058ED4 File Offset: 0x000570D4
		public int Done
		{
			get
			{
				int num = 0;
				foreach (Job job in this.Jobs)
				{
					if (job.TaskStatus == TaskStatus.Complete)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00058F14 File Offset: 0x00057114
		public void Initialize()
		{
			this.ByteLength = 0;
			foreach (Job job in this.Jobs)
			{
				this.ByteLength += job.Item.ByteLength;
			}
			int num = this.Jobs.Length;
			if (num != 0)
			{
				if (num != 1)
				{
					this.jobDesc = string.Format("{0} bundles", this.Jobs.Length);
				}
				else
				{
					this.jobDesc = "1 bundle";
				}
			}
			else
			{
				this.jobDesc = "No bundles";
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00058FBC File Offset: 0x000571BC
		internal void GetArrays(out AssetBundle[] assetBundles, out Item[] items)
		{
			items = new Item[this.Jobs.Length];
			assetBundles = new AssetBundle[this.Jobs.Length];
			for (int i = 0; i < this.Jobs.Length; i++)
			{
				assetBundles[i] = this.Jobs[i].AssetBundle;
				items[i] = this.Jobs[i].Item;
			}
		}

		// Token: 0x04000C76 RID: 3190
		[NonSerialized]
		public Operation _op;

		// Token: 0x04000C77 RID: 3191
		[NonSerialized]
		public Job[] Jobs;

		// Token: 0x04000C78 RID: 3192
		[NonSerialized]
		public int ByteLength;

		// Token: 0x04000C79 RID: 3193
		[NonSerialized]
		private string jobDesc;

		// Token: 0x04000C7A RID: 3194
		[NonSerialized]
		private string lastDescriptiveText;

		// Token: 0x04000C7B RID: 3195
		[NonSerialized]
		private TaskStatusCount? lastStatusCount;
	}
}
