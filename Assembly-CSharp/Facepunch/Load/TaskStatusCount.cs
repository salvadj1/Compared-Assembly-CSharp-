using System;

namespace Facepunch.Load
{
	// Token: 0x020002A2 RID: 674
	public struct TaskStatusCount
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0005A704 File Offset: 0x00058904
		public int Total
		{
			get
			{
				return this.Pending + this.Downloading + this.Complete;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0005A71C File Offset: 0x0005891C
		public int Remaining
		{
			get
			{
				return this.Pending + this.Downloading;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0005A72C File Offset: 0x0005892C
		public float PercentComplete
		{
			get
			{
				return (this.Complete != 0) ? ((float)((double)this.Remaining / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0005A754 File Offset: 0x00058954
		public float PercentPending
		{
			get
			{
				return (this.Pending != 0) ? ((float)((double)this.Pending / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005A77C File Offset: 0x0005897C
		public float PercentDownloading
		{
			get
			{
				return ((float)this.Downloading != 0f) ? ((float)((double)this.Downloading / (double)this.Total)) : 0f;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005A7AC File Offset: 0x000589AC
		public bool CompletelyPending
		{
			get
			{
				return this.Pending > 0 && this.Downloading == 0 && this.Complete == 0;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0005A7E0 File Offset: 0x000589E0
		public bool CompletelyDownloading
		{
			get
			{
				return this.Downloading > 0 && this.Pending == 0 && this.Complete == 0;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005A814 File Offset: 0x00058A14
		public bool CompletelyComplete
		{
			get
			{
				return this.Complete > 0 && this.Downloading == 0 && this.Pending == 0;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005A848 File Offset: 0x00058A48
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

		// Token: 0x170006C9 RID: 1737
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

		// Token: 0x04000CAC RID: 3244
		public int Pending;

		// Token: 0x04000CAD RID: 3245
		public int Downloading;

		// Token: 0x04000CAE RID: 3246
		public int Complete;

		// Token: 0x04000CAF RID: 3247
		public static readonly TaskStatusCount OnePending = new TaskStatusCount
		{
			Pending = 1
		};

		// Token: 0x04000CB0 RID: 3248
		public static readonly TaskStatusCount OneDownloading = new TaskStatusCount
		{
			Downloading = 1
		};

		// Token: 0x04000CB1 RID: 3249
		public static readonly TaskStatusCount OneComplete = new TaskStatusCount
		{
			Complete = 1
		};
	}
}
