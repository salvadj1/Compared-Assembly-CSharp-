using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facepunch.Progress
{
	// Token: 0x020001BF RID: 447
	public sealed class ProgressBar
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x000316A0 File Offset: 0x0002F8A0
		public void Add(IProgress IProgress)
		{
			if (object.ReferenceEquals(IProgress, null))
			{
				return;
			}
			this.List.Add(IProgress);
			this.count++;
			this.denom += 1f;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000316DC File Offset: 0x0002F8DC
		public void AddMultiple<T>(IEnumerable<T> collection) where T : IProgress
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00031740 File Offset: 0x0002F940
		public void Clear()
		{
			this.bonus = (this.denom = 0f);
			this.List.Clear();
			this.count = 0;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00031774 File Offset: 0x0002F974
		public void Clean()
		{
			float num;
			this.Update(out num);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003178C File Offset: 0x0002F98C
		public bool Update(out float progress)
		{
			if (this.count == 0)
			{
				progress = 0f;
				return false;
			}
			float num = 0f;
			int i = 0;
			int num2 = this.count;
			int num3 = num2 - 1;
			while (i < num2)
			{
				float num4;
				if (this.List[num3].Poll(out num4) && num4 < 1f)
				{
					num += num4;
				}
				else
				{
					if (--this.count <= 0)
					{
						this.Clear();
						progress = 1f;
						return true;
					}
					this.bonus += 1f;
					this.List.RemoveAt(num3);
				}
				i++;
				num3--;
			}
			if ((progress = (num + this.bonus) / this.denom) > 1f)
			{
				progress = 1f;
			}
			return true;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00031878 File Offset: 0x0002FA78
		public void Add(AsyncOperation Progress)
		{
			if (!object.ReferenceEquals(Progress, null))
			{
				this.Add(new ProgressBar.AsyncOperationProgress(Progress));
			}
		}

		// Token: 0x04000787 RID: 1927
		private readonly List<IProgress> List = new List<IProgress>();

		// Token: 0x04000788 RID: 1928
		private float bonus;

		// Token: 0x04000789 RID: 1929
		private float denom;

		// Token: 0x0400078A RID: 1930
		private int count;

		// Token: 0x020001C0 RID: 448
		private struct AsyncOperationProgress : IProgress
		{
			// Token: 0x06000C73 RID: 3187 RVA: 0x00031898 File Offset: 0x0002FA98
			public AsyncOperationProgress(AsyncOperation aop)
			{
				this.aop = aop;
			}

			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000318A4 File Offset: 0x0002FAA4
			public float progress
			{
				get
				{
					return (this.aop != null && !this.aop.isDone) ? (this.aop.progress * 0.999f) : 1f;
				}
			}

			// Token: 0x0400078B RID: 1931
			public readonly AsyncOperation aop;
		}
	}
}
