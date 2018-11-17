using System;
using System.Collections.Generic;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class BobEffectStack : IDisposable
{
	// Token: 0x0600176A RID: 5994 RVA: 0x000574CC File Offset: 0x000556CC
	public bool IsForkOf(global::BobEffectStack stack)
	{
		return this.owner != null && this.owner == stack;
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000574E8 File Offset: 0x000556E8
	public bool CreateInstance(global::BobEffect effect)
	{
		global::BobEffect.Data data;
		if (effect && effect.Create(out data))
		{
			this.data.Add(data);
			foreach (global::BobEffectStack bobEffectStack in this.forks)
			{
				bobEffectStack.data.Add(data.Clone());
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x00057580 File Offset: 0x00055780
	private void RunSim(ref int i, ref Vector3G force, ref Vector3G torque)
	{
		while (i < this.dataCount)
		{
			this.ctx.data = this.data[i];
			switch (this.ctx.data.effect.Simulate(ref this.ctx))
			{
			case global::BOBRES.CONTINUE:
				force.x += this.ctx.data.force.x;
				force.y += this.ctx.data.force.y;
				force.z += this.ctx.data.force.z;
				torque.x += this.ctx.data.torque.x;
				torque.y += this.ctx.data.torque.y;
				torque.z += this.ctx.data.torque.z;
				break;
			case global::BOBRES.EXIT:
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
						foreach (global::BobEffectStack bobEffectStack in this.forks)
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
			case global::BOBRES.ERROR:
				Debug.LogError("Error with effect", this.ctx.data.effect);
				break;
			}
			i++;
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000577CC File Offset: 0x000559CC
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

	// Token: 0x0600176E RID: 5998 RVA: 0x00057810 File Offset: 0x00055A10
	private void DestroyAllEffects()
	{
		foreach (global::BobEffect.Data data in this.data)
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

	// Token: 0x0600176F RID: 5999 RVA: 0x000578CC File Offset: 0x00055ACC
	public void Dispose()
	{
		if (!this.isFork)
		{
			foreach (global::BobEffectStack bobEffectStack in this.forks)
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

	// Token: 0x06001770 RID: 6000 RVA: 0x00057968 File Offset: 0x00055B68
	public global::BobEffectStack Fork()
	{
		global::BobEffectStack bobEffectStack = new global::BobEffectStack();
		bobEffectStack.isFork = true;
		bobEffectStack.owner = ((!this.isFork) ? this : this.owner);
		bobEffectStack.owner.forks.Add(bobEffectStack);
		foreach (global::BobEffect.Data data in bobEffectStack.owner.data)
		{
			bobEffectStack.data.Add(data.Clone());
		}
		return bobEffectStack;
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x00057A1C File Offset: 0x00055C1C
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

	// Token: 0x04000C4C RID: 3148
	private List<global::BobEffect.Data> data = new List<global::BobEffect.Data>();

	// Token: 0x04000C4D RID: 3149
	private List<global::BobEffectStack> forks = new List<global::BobEffectStack>();

	// Token: 0x04000C4E RID: 3150
	private global::BobEffectStack owner;

	// Token: 0x04000C4F RID: 3151
	private int dataCount;

	// Token: 0x04000C50 RID: 3152
	private bool isFork;

	// Token: 0x04000C51 RID: 3153
	private global::BobEffect.Context ctx;
}
