using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facepunch.Progress
{
	// Token: 0x020001EF RID: 495
	public sealed class ProgressBar
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003558C File Offset: 0x0003378C
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

		// Token: 0x06000DAE RID: 3502 RVA: 0x000355C8 File Offset: 0x000337C8
		public void AddMultiple<T>(IEnumerable<T> collection) where T : IProgress
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003562C File Offset: 0x0003382C
		public void Clear()
		{
			this.bonus = (this.denom = 0f);
			this.List.Clear();
			this.count = 0;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00035660 File Offset: 0x00033860
		public void Clean()
		{
			float num;
			this.Update(out num);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00035678 File Offset: 0x00033878
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

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00035764 File Offset: 0x00033964
		public void Add(AsyncOperation Progress)
		{
			if (!object.ReferenceEquals(Progress, null))
			{
				this.Add(new ProgressBar.AsyncOperationProgress(Progress));
			}
		}

		// Token: 0x0400089B RID: 2203
		private readonly List<IProgress> List = new List<IProgress>();

		// Token: 0x0400089C RID: 2204
		private float bonus;

		// Token: 0x0400089D RID: 2205
		private float denom;

		// Token: 0x0400089E RID: 2206
		private int count;

		// Token: 0x020001F0 RID: 496
		private struct AsyncOperationProgress : IProgress
		{
			// Token: 0x06000DB3 RID: 3507 RVA: 0x00035784 File Offset: 0x00033984
			public AsyncOperationProgress(AsyncOperation aop)
			{
				this.aop = aop;
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00035790 File Offset: 0x00033990
			public float progress
			{
				get
				{
					return (this.aop != null && !this.aop.isDone) ? (this.aop.progress * 0.999f) : 1f;
				}
			}

			// Token: 0x0400089F RID: 2207
			public readonly AsyncOperation aop;
		}
	}
}
