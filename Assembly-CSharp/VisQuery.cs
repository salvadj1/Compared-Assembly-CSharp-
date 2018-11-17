using System;
using UnityEngine;

// Token: 0x02000471 RID: 1137
public class VisQuery : ScriptableObject
{
	// Token: 0x060027C1 RID: 10177 RVA: 0x00090748 File Offset: 0x0008E948
	private void Enter(global::VisNode a, global::VisNode b)
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

	// Token: 0x060027C2 RID: 10178 RVA: 0x000907B0 File Offset: 0x0008E9B0
	private void Exit(global::VisNode a, global::VisNode b)
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

	// Token: 0x060027C3 RID: 10179 RVA: 0x00090818 File Offset: 0x0008EA18
	private bool Try(global::VisNode self, global::VisNode instigator)
	{
		global::Vis.Mask traitMask = self.traitMask;
		global::Vis.Mask traitMask2 = instigator.traitMask;
		return this.evaluation.Pass(traitMask, traitMask2);
	}

	// Token: 0x040012E0 RID: 4832
	[SerializeField]
	protected global::VisEval evaluation;

	// Token: 0x040012E1 RID: 4833
	[SerializeField]
	protected global::VisAction[] actions;

	// Token: 0x040012E2 RID: 4834
	[SerializeField]
	protected bool nonInstance;

	// Token: 0x02000472 RID: 1138
	public enum TryResult
	{
		// Token: 0x040012E4 RID: 4836
		Outside,
		// Token: 0x040012E5 RID: 4837
		Enter,
		// Token: 0x040012E6 RID: 4838
		Stay,
		// Token: 0x040012E7 RID: 4839
		Exit
	}

	// Token: 0x02000473 RID: 1139
	public class Instance
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x00090840 File Offset: 0x0008EA40
		internal Instance(global::VisQuery outer, ref int bit)
		{
			this.outer = outer;
			this.applicable = new global::HSet<global::VisNode>();
			this.bit = 1L << (bit & 31);
			this.bitNumber = (byte)bit;
			bit++;
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00090878 File Offset: 0x0008EA78
		public int count
		{
			get
			{
				return this.num;
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x00090880 File Offset: 0x0008EA80
		public bool Fits(global::VisNode other)
		{
			return this.applicable.Contains(other);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00090890 File Offset: 0x0008EA90
		public void ExecuteEnter(global::VisNode self, global::VisNode other)
		{
			if (this.execNum++ == 0 || !this.outer.nonInstance)
			{
				this.outer.Enter(self, other);
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000908D0 File Offset: 0x0008EAD0
		public void ExecuteExit(global::VisNode self, global::VisNode other)
		{
			if (--this.execNum == 0 || !this.outer.nonInstance)
			{
				this.outer.Exit(self, other);
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00090910 File Offset: 0x0008EB10
		public void Execute(global::VisQuery.TryResult res, global::VisNode self, global::VisNode other)
		{
			switch (res)
			{
			case global::VisQuery.TryResult.Enter:
				this.ExecuteEnter(self, other);
				break;
			case global::VisQuery.TryResult.Exit:
				this.ExecuteExit(self, other);
				break;
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00090958 File Offset: 0x0008EB58
		public global::VisQuery.TryResult TryAdd(global::VisNode self, global::VisNode other)
		{
			if (!this.outer.Try(self, other))
			{
				return this.TryRemove(self, other);
			}
			if (this.applicable.Add(other))
			{
				this.num++;
				return global::VisQuery.TryResult.Enter;
			}
			return global::VisQuery.TryResult.Stay;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000909A4 File Offset: 0x0008EBA4
		public global::VisQuery.TryResult TryRemove(global::VisNode self, global::VisNode other)
		{
			if (this.applicable.Remove(other))
			{
				this.num--;
				return global::VisQuery.TryResult.Exit;
			}
			return global::VisQuery.TryResult.Outside;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000909D4 File Offset: 0x0008EBD4
		public void Clear(global::VisNode self)
		{
			while (--this.num >= 0)
			{
				global::HSetIter<global::VisNode> enumerator = this.applicable.GetEnumerator();
				enumerator.MoveNext();
				global::VisNode other = enumerator.Current;
				enumerator.Dispose();
				this.TryRemove(self, other);
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00090A2C File Offset: 0x0008EC2C
		public bool IsActive(long mask)
		{
			return (mask & this.bit) == this.bit;
		}

		// Token: 0x040012E8 RID: 4840
		public readonly global::VisQuery outer;

		// Token: 0x040012E9 RID: 4841
		private readonly global::HSet<global::VisNode> applicable;

		// Token: 0x040012EA RID: 4842
		private readonly long bit;

		// Token: 0x040012EB RID: 4843
		private readonly byte bitNumber;

		// Token: 0x040012EC RID: 4844
		private int num;

		// Token: 0x040012ED RID: 4845
		private int execNum;
	}
}
