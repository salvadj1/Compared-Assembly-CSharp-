using System;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class VisQuery : ScriptableObject
{
	// Token: 0x0600245F RID: 9311 RVA: 0x0008B34C File Offset: 0x0008954C
	private void Enter(VisNode a, VisNode b)
	{
		IDMain idMain = a.idMain;
		IDMain instigator = (!this.nonInstance) ? b.idMain : null;
		for (int i = 0; i < this.actions.Length; i++)
		{
			if (this.actions[i])
			{
				this.actions[i].Accomplish(idMain, instigator);
			}
		}
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x0008B3B4 File Offset: 0x000895B4
	private void Exit(VisNode a, VisNode b)
	{
		IDMain idMain = a.idMain;
		IDMain instigator = (!this.nonInstance) ? b.idMain : null;
		for (int i = 0; i < this.actions.Length; i++)
		{
			if (this.actions[i])
			{
				this.actions[i].UnAcomplish(idMain, instigator);
			}
		}
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x0008B41C File Offset: 0x0008961C
	private bool Try(VisNode self, VisNode instigator)
	{
		Vis.Mask traitMask = self.traitMask;
		Vis.Mask traitMask2 = instigator.traitMask;
		return this.evaluation.Pass(traitMask, traitMask2);
	}

	// Token: 0x0400117A RID: 4474
	[SerializeField]
	protected VisEval evaluation;

	// Token: 0x0400117B RID: 4475
	[SerializeField]
	protected VisAction[] actions;

	// Token: 0x0400117C RID: 4476
	[SerializeField]
	protected bool nonInstance;

	// Token: 0x020003C5 RID: 965
	public enum TryResult
	{
		// Token: 0x0400117E RID: 4478
		Outside,
		// Token: 0x0400117F RID: 4479
		Enter,
		// Token: 0x04001180 RID: 4480
		Stay,
		// Token: 0x04001181 RID: 4481
		Exit
	}

	// Token: 0x020003C6 RID: 966
	public class Instance
	{
		// Token: 0x06002462 RID: 9314 RVA: 0x0008B444 File Offset: 0x00089644
		internal Instance(VisQuery outer, ref int bit)
		{
			this.outer = outer;
			this.applicable = new HSet<VisNode>();
			this.bit = 1L << (bit & 31);
			this.bitNumber = (byte)bit;
			bit++;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x0008B47C File Offset: 0x0008967C
		public int count
		{
			get
			{
				return this.num;
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0008B484 File Offset: 0x00089684
		public bool Fits(VisNode other)
		{
			return this.applicable.Contains(other);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0008B494 File Offset: 0x00089694
		public void ExecuteEnter(VisNode self, VisNode other)
		{
			if (this.execNum++ == 0 || !this.outer.nonInstance)
			{
				this.outer.Enter(self, other);
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0008B4D4 File Offset: 0x000896D4
		public void ExecuteExit(VisNode self, VisNode other)
		{
			if (--this.execNum == 0 || !this.outer.nonInstance)
			{
				this.outer.Exit(self, other);
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0008B514 File Offset: 0x00089714
		public void Execute(VisQuery.TryResult res, VisNode self, VisNode other)
		{
			switch (res)
			{
			case VisQuery.TryResult.Enter:
				this.ExecuteEnter(self, other);
				break;
			case VisQuery.TryResult.Exit:
				this.ExecuteExit(self, other);
				break;
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0008B55C File Offset: 0x0008975C
		public VisQuery.TryResult TryAdd(VisNode self, VisNode other)
		{
			if (!this.outer.Try(self, other))
			{
				return this.TryRemove(self, other);
			}
			if (this.applicable.Add(other))
			{
				this.num++;
				return VisQuery.TryResult.Enter;
			}
			return VisQuery.TryResult.Stay;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0008B5A8 File Offset: 0x000897A8
		public VisQuery.TryResult TryRemove(VisNode self, VisNode other)
		{
			if (this.applicable.Remove(other))
			{
				this.num--;
				return VisQuery.TryResult.Exit;
			}
			return VisQuery.TryResult.Outside;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0008B5D8 File Offset: 0x000897D8
		public void Clear(VisNode self)
		{
			while (--this.num >= 0)
			{
				HSetIter<VisNode> enumerator = this.applicable.GetEnumerator();
				enumerator.MoveNext();
				VisNode other = enumerator.Current;
				enumerator.Dispose();
				this.TryRemove(self, other);
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0008B630 File Offset: 0x00089830
		public bool IsActive(long mask)
		{
			return (mask & this.bit) == this.bit;
		}

		// Token: 0x04001182 RID: 4482
		public readonly VisQuery outer;

		// Token: 0x04001183 RID: 4483
		private readonly HSet<VisNode> applicable;

		// Token: 0x04001184 RID: 4484
		private readonly long bit;

		// Token: 0x04001185 RID: 4485
		private readonly byte bitNumber;

		// Token: 0x04001186 RID: 4486
		private int num;

		// Token: 0x04001187 RID: 4487
		private int execNum;
	}
}
