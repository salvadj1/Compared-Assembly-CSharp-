using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public abstract class StateInterpolator<T> : global::StateInterpolator
{
	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x0600222A RID: 8746 RVA: 0x0007E09C File Offset: 0x0007C29C
	public int bufferCapacity
	{
		get
		{
			return this.tbuffer.Length;
		}
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x0007E0A8 File Offset: 0x0007C2A8
	protected void Awake()
	{
		this.tbuffer = new global::TimeStamped<T>[this._bufferCapacity];
		for (int i = 0; i < this._bufferCapacity; i++)
		{
			this.tbuffer[i].index = i;
		}
	}

	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x0600222C RID: 8748 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
	public new double storedDuration
	{
		get
		{
			return (this.len >= 2) ? (this.tbuffer[this.tbuffer[0].index].timeStamp - this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp) : 0.0;
		}
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x0600222D RID: 8749 RVA: 0x0007E164 File Offset: 0x0007C364
	public new double oldestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[0].index].timeStamp : double.NegativeInfinity;
		}
	}

	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x0600222E RID: 8750 RVA: 0x0007E1AC File Offset: 0x0007C3AC
	public new double newestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp : double.PositiveInfinity;
		}
	}

	// Token: 0x0600222F RID: 8751 RVA: 0x0007E1FC File Offset: 0x0007C3FC
	public new void Clear()
	{
		if (this.len > 0)
		{
			if (global::ReferenceTypeHelper<T>.TreatAsReferenceHolder)
			{
				for (int i = 0; i < this.len; i++)
				{
					this.tbuffer[this.tbuffer[i].index].value = default(T);
				}
			}
			this.len = 0;
		}
	}

	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06002230 RID: 8752 RVA: 0x0007E268 File Offset: 0x0007C468
	protected sealed override double __newestTimeStamp
	{
		get
		{
			return this.newestTimeStamp;
		}
	}

	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x06002231 RID: 8753 RVA: 0x0007E270 File Offset: 0x0007C470
	protected sealed override double __oldestTimeStamp
	{
		get
		{
			return this.oldestTimeStamp;
		}
	}

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x06002232 RID: 8754 RVA: 0x0007E278 File Offset: 0x0007C478
	protected sealed override double __storedDuration
	{
		get
		{
			return this.storedDuration;
		}
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x0007E280 File Offset: 0x0007C480
	protected sealed override void __Clear()
	{
		this.Clear();
	}

	// Token: 0x06002234 RID: 8756 RVA: 0x0007E288 File Offset: 0x0007C488
	public override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		throw new NotImplementedException("The thing using this has not implemented a way to take pos, rot to " + typeof(T));
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
	protected void Push(ref T state, ref double timeStamp)
	{
		int num = this.tbuffer.Length;
		if (this.len < num)
		{
			for (int i = 0; i < this.len; i++)
			{
				int index = this.tbuffer[i].index;
				if (this.tbuffer[index].timeStamp < timeStamp)
				{
					for (int j = this.len; j > i; j--)
					{
						this.tbuffer[j].index = this.tbuffer[j - 1].index;
					}
					this.tbuffer[i].index = this.len;
					this.tbuffer[this.len++].Set(ref state, ref timeStamp);
					return;
				}
				if (this.tbuffer[index].timeStamp == timeStamp)
				{
					this.tbuffer[index].Set(ref state, ref timeStamp);
					return;
				}
			}
			this.tbuffer[this.len].index = this.len;
			this.tbuffer[this.len++].Set(ref state, ref timeStamp);
		}
		else
		{
			for (int k = 0; k < num; k++)
			{
				int index2 = this.tbuffer[k].index;
				if (this.tbuffer[index2].timeStamp < timeStamp)
				{
					int index3 = this.tbuffer[num - 1].index;
					for (int l = num - 1; l > k; l--)
					{
						this.tbuffer[l].index = this.tbuffer[l - 1].index;
					}
					this.tbuffer[k].index = index3;
					this.tbuffer[index3].Set(ref state, ref timeStamp);
					return;
				}
				if (this.tbuffer[index2].timeStamp == timeStamp)
				{
					this.tbuffer[index2].Set(ref state, ref timeStamp);
					return;
				}
			}
		}
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x0007E4E0 File Offset: 0x0007C6E0
	public virtual void SetGoals(ref T state, ref double timeStamp)
	{
		this.Push(ref state, ref timeStamp);
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x0007E4EC File Offset: 0x0007C6EC
	public void SetGoals(ref global::TimeStamped<T> state)
	{
		T value = state.value;
		double timeStamp = state.timeStamp;
		this.SetGoals(ref value, ref timeStamp);
	}

	// Token: 0x04001042 RID: 4162
	protected global::TimeStamped<T>[] tbuffer;
}
