using System;
using System.Collections.Generic;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class BobEffectStack : IDisposable
{
	// Token: 0x06001616 RID: 5654 RVA: 0x00053124 File Offset: 0x00051324
	public bool IsForkOf(BobEffectStack stack)
	{
		return this.owner != null && this.owner == stack;
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x00053140 File Offset: 0x00051340
	public bool CreateInstance(BobEffect effect)
	{
		BobEffect.Data data;
		if (effect && effect.Create(out data))
		{
			this.data.Add(data);
			foreach (BobEffectStack bobEffectStack in this.forks)
			{
				bobEffectStack.data.Add(data.Clone());
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x000531D8 File Offset: 0x000513D8
	private void RunSim(ref int i, ref Vector3G force, ref Vector3G torque)
	{
		while (i < this.dataCount)
		{
			this.ctx.data = this.data[i];
			switch (this.ctx.data.effect.Simulate(ref this.ctx))
			{
			case BOBRES.CONTINUE:
				force.x += this.ctx.data.force.x;
				force.y += this.ctx.data.force.y;
				force.z += this.ctx.data.force.z;
				torque.x += this.ctx.data.torque.x;
				torque.y += this.ctx.data.torque.y;
				torque.z += this.ctx.data.torque.z;
				break;
			case BOBRES.EXIT:
				if (!this.isFork)
				{
					int index = i++;
					this.RunSim(ref i, ref force, ref torque);
					if (this.ctx.data != null)
					{
						if (this.ctx.data.effect != null)
						{
							this.ctx.data.effect.Destroy(ref this.ctx.data);
						}
						this.data.RemoveAt(index);
						foreach (BobEffectStack bobEffectStack in this.forks)
						{
							bobEffectStack.data.RemoveAt(index);
						}
					}
					else
					{
						this.data.RemoveAt(index);
					}
				}
				return;
			case BOBRES.ERROR:
				Debug.LogError("Error with effect", this.ctx.data.effect);
				break;
			}
			i++;
		}
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x00053424 File Offset: 0x00051624
	public void Simulate(ref double dt, ref Vector3G force, ref Vector3G torque)
	{
		this.dataCount = this.data.Count;
		if (this.dataCount > 0)
		{
			int num = 0;
			this.ctx.dt = dt;
			this.RunSim(ref num, ref force, ref torque);
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x00053468 File Offset: 0x00051668
	private void DestroyAllEffects()
	{
		foreach (BobEffect.Data data in this.data)
		{
			this.ctx.data = data;
			if (this.ctx.data.effect)
			{
				this.ctx.data.effect.Destroy(ref this.ctx.data);
			}
		}
		this.ctx.data = null;
		this.data.Clear();
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x00053524 File Offset: 0x00051724
	public void Dispose()
	{
		if (!this.isFork)
		{
			foreach (BobEffectStack bobEffectStack in this.forks)
			{
				bobEffectStack.DestroyAllEffects();
			}
		}
		else
		{
			this.DestroyAllEffects();
			this.owner.forks.Remove(this);
			this.owner = null;
			this.isFork = false;
		}
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000535C0 File Offset: 0x000517C0
	public BobEffectStack Fork()
	{
		BobEffectStack bobEffectStack = new BobEffectStack();
		bobEffectStack.isFork = true;
		bobEffectStack.owner = ((!this.isFork) ? this : this.owner);
		bobEffectStack.owner.forks.Add(bobEffectStack);
		foreach (BobEffect.Data data in bobEffectStack.owner.data)
		{
			bobEffectStack.data.Add(data.Clone());
		}
		return bobEffectStack;
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00053674 File Offset: 0x00051874
	public void Join()
	{
		if (this.isFork)
		{
			this.dataCount = this.data.Count;
			for (int i = 0; i < this.dataCount; i++)
			{
				this.owner.data[i].CopyDataTo(this.data[i]);
			}
		}
	}

	// Token: 0x04000B29 RID: 2857
	private List<BobEffect.Data> data = new List<BobEffect.Data>();

	// Token: 0x04000B2A RID: 2858
	private List<BobEffectStack> forks = new List<BobEffectStack>();

	// Token: 0x04000B2B RID: 2859
	private BobEffectStack owner;

	// Token: 0x04000B2C RID: 2860
	private int dataCount;

	// Token: 0x04000B2D RID: 2861
	private bool isFork;

	// Token: 0x04000B2E RID: 2862
	private BobEffect.Context ctx;
}
