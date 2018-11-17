using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public abstract class StateInterpolator<T> : StateInterpolator
{
	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00078CA0 File Offset: 0x00076EA0
	public int bufferCapacity
	{
		get
		{
			return this.tbuffer.Length;
		}
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x00078CAC File Offset: 0x00076EAC
	protected void Awake()
	{
		this.tbuffer = new TimeStamped<T>[this._bufferCapacity];
		for (int i = 0; i < this._bufferCapacity; i++)
		{
			this.tbuffer[i].index = i;
		}
	}

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00078CF4 File Offset: 0x00076EF4
	public new double storedDuration
	{
		get
		{
			return (this.len >= 2) ? (this.tbuffer[this.tbuffer[0].index].timeStamp - this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp) : 0.0;
		}
	}

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x06001ECB RID: 7883 RVA: 0x00078D68 File Offset: 0x00076F68
	public new double oldestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[0].index].timeStamp : double.NegativeInfinity;
		}
	}

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00078DB0 File Offset: 0x00076FB0
	public new double newestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp : double.PositiveInfinity;
		}
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x00078E00 File Offset: 0x00077000
	public new void Clear()
	{
		if (this.len > 0)
		{
			if (ReferenceTypeHelper<T>.TreatAsReferenceHolder)
			{
				for (int i = 0; i < this.len; i++)
				{
					this.tbuffer[this.tbuffer[i].index].value = default(T);
				}
			}
			this.len = 0;
		}
	}

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x06001ECE RID: 7886 RVA: 0x00078E6C File Offset: 0x0007706C
	protected sealed override double __newestTimeStamp
	{
		get
		{
			return this.newestTimeStamp;
		}
	}

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x06001ECF RID: 7887 RVA: 0x00078E74 File Offset: 0x00077074
	protected sealed override double __oldestTimeStamp
	{
		get
		{
			return this.oldestTimeStamp;
		}
	}

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00078E7C File Offset: 0x0007707C
	protected sealed override double __storedDuration
	{
		get
		{
			return this.storedDuration;
		}
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00078E84 File Offset: 0x00077084
	protected sealed override void __Clear()
	{
		this.Clear();
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00078E8C File Offset: 0x0007708C
	public override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		throw new NotImplementedException("The thing using this has not implemented a way to take pos, rot to " + typeof(T));
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00078EA8 File Offset: 0x000770A8
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

	// Token: 0x06001ED4 RID: 7892 RVA: 0x000790E4 File Offset: 0x000772E4
	public virtual void SetGoals(ref T state, ref double timeStamp)
	{
		this.Push(ref state, ref timeStamp);
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x000790F0 File Offset: 0x000772F0
	public void SetGoals(ref TimeStamped<T> state)
	{
		T value = state.value;
		double timeStamp = state.timeStamp;
		this.SetGoals(ref value, ref timeStamp);
	}

	// Token: 0x04000EDC RID: 3804
	protected TimeStamped<T>[] tbuffer;
}
