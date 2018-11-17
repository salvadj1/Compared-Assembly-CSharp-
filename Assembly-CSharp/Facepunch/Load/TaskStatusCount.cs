using System;

namespace Facepunch.Load
{
	// Token: 0x0200026E RID: 622
	public struct TaskStatusCount
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000562BC File Offset: 0x000544BC
		public int Total
		{
			get
			{
				return this.Pending + this.Downloading + this.Complete;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000562D4 File Offset: 0x000544D4
		public int Remaining
		{
			get
			{
				return this.Pending + this.Downloading;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000562E4 File Offset: 0x000544E4
		public float PercentComplete
		{
			get
			{
				return (this.Complete != 0) ? ((float)((double)this.Remaining / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0005630C File Offset: 0x0005450C
		public float PercentPending
		{
			get
			{
				return (this.Pending != 0) ? ((float)((double)this.Pending / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00056334 File Offset: 0x00054534
		public float PercentDownloading
		{
			get
			{
				return ((float)this.Downloading != 0f) ? ((float)((double)this.Downloading / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00056364 File Offset: 0x00054564
		public bool CompletelyPending
		{
			get
			{
				return this.Pending > 0 && this.Downloading == 0 && this.Complete == 0;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00056398 File Offset: 0x00054598
		public bool CompletelyDownloading
		{
			get
			{
				return this.Downloading > 0 && this.Pending == 0 && this.Complete == 0;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000563CC File Offset: 0x000545CC
		public bool CompletelyComplete
		{
			get
			{
				return this.Complete > 0 && this.Downloading == 0 && this.Pending == 0;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00056400 File Offset: 0x00054600
		public TaskStatus TaskStatus
		{
			get
			{
				if (this.Pending > 0)
				{
					if (this.Downloading > 0)
					{
						if (this.Complete > 0)
						{
							return TaskStatus.Pending | TaskStatus.Downloading | TaskStatus.Complete;
						}
						return TaskStatus.Pending | TaskStatus.Downloading;
					}
					else
					{
						if (this.Complete > 0)
						{
							return TaskStatus.Pending | TaskStatus.Complete;
						}
						return TaskStatus.Pending;
					}
				}
				else if (this.Downloading > 0)
				{
					if (this.Complete > 0)
					{
						return TaskStatus.Downloading | TaskStatus.Complete;
					}
					return TaskStatus.Downloading;
				}
				else
				{
					if (this.Complete > 0)
					{
						return TaskStatus.Complete;
					}
					return (TaskStatus)0;
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		public int this[TaskStatus status]
		{
			get
			{
				switch (status)
				{
				case TaskStatus.Pending:
					return this.Pending;
				case TaskStatus.Downloading:
					return this.Downloading;
				case TaskStatus.Pending | TaskStatus.Downloading:
					return this.Pending + this.Downloading;
				case TaskStatus.Complete:
					return this.Complete;
				case TaskStatus.Pending | TaskStatus.Complete:
					return this.Pending + this.Complete;
				case TaskStatus.Downloading | TaskStatus.Complete:
					return this.Pending + this.Downloading;
				case TaskStatus.Pending | TaskStatus.Downloading | TaskStatus.Complete:
					return this.Pending + this.Downloading + this.Complete;
				default:
					return 0;
				}
			}
			set
			{
				switch (status)
				{
				case TaskStatus.Pending:
					this.Pending = value;
					break;
				case TaskStatus.Downloading:
					this.Downloading = value;
					break;
				case TaskStatus.Pending | TaskStatus.Downloading:
					this.Pending = value;
					this.Downloading = value;
					break;
				case TaskStatus.Complete:
					this.Complete = value;
					break;
				case TaskStatus.Pending | TaskStatus.Complete:
					this.Pending = value;
					this.Complete = value;
					break;
				case TaskStatus.Downloading | TaskStatus.Complete:
					this.Complete = value;
					this.Downloading = value;
					break;
				case TaskStatus.Pending | TaskStatus.Downloading | TaskStatus.Complete:
					this.Downloading = value;
					this.Pending = value;
					this.Complete = value;
					break;
				}
			}
		}

		// Token: 0x04000B86 RID: 2950
		public int Pending;

		// Token: 0x04000B87 RID: 2951
		public int Downloading;

		// Token: 0x04000B88 RID: 2952
		public int Complete;

		// Token: 0x04000B89 RID: 2953
		public static readonly TaskStatusCount OnePending = new TaskStatusCount
		{
			Pending = 1
		};

		// Token: 0x04000B8A RID: 2954
		public static readonly TaskStatusCount OneDownloading = new TaskStatusCount
		{
			Downloading = 1
		};

		// Token: 0x04000B8B RID: 2955
		public static readonly TaskStatusCount OneComplete = new TaskStatusCount
		{
			Complete = 1
		};
	}
}
