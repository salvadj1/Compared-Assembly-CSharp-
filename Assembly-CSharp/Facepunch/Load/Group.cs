using System;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000261 RID: 609
	public class Group : IDownloadTask
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00054818 File Offset: 0x00052A18
		string IDownloadTask.Name
		{
			get
			{
				return this.jobDesc;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00054820 File Offset: 0x00052A20
		int IDownloadTask.ByteLength
		{
			get
			{
				return this.ByteLength;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00054828 File Offset: 0x00052A28
		public Loader Loader
		{
			get
			{
				return this._op.Loader;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00054838 File Offset: 0x00052A38
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

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00054870 File Offset: 0x00052A70
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

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000548BC File Offset: 0x00052ABC
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

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00054918 File Offset: 0x00052B18
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

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00054B0C File Offset: 0x00052D0C
		public float PercentDone
		{
			get
			{
				return (float)((double)this.ByteLengthDownloaded / (double)this.ByteLength);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00054B20 File Offset: 0x00052D20
		public int Count
		{
			get
			{
				return this.Jobs.Length;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00054B2C File Offset: 0x00052D2C
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

		// Token: 0x0600164A RID: 5706 RVA: 0x00054B6C File Offset: 0x00052D6C
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

		// Token: 0x0600164B RID: 5707 RVA: 0x00054C14 File Offset: 0x00052E14
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

		// Token: 0x04000B53 RID: 2899
		[NonSerialized]
		public Operation _op;

		// Token: 0x04000B54 RID: 2900
		[NonSerialized]
		public Job[] Jobs;

		// Token: 0x04000B55 RID: 2901
		[NonSerialized]
		public int ByteLength;

		// Token: 0x04000B56 RID: 2902
		[NonSerialized]
		private string jobDesc;

		// Token: 0x04000B57 RID: 2903
		[NonSerialized]
		private string lastDescriptiveText;

		// Token: 0x04000B58 RID: 2904
		[NonSerialized]
		private TaskStatusCount? lastStatusCount;
	}
}
