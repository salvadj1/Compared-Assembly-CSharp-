using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public abstract class CharacterInterpolatorBase<T> : global::CharacterInterpolatorBase
{
	// Token: 0x06000791 RID: 1937 RVA: 0x00021A3C File Offset: 0x0001FC3C
	protected CharacterInterpolatorBase() : this(false, global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake)
	{
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00021A48 File Offset: 0x0001FC48
	protected CharacterInterpolatorBase(bool customPusher) : this(customPusher, global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake)
	{
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00021A54 File Offset: 0x0001FC54
	protected CharacterInterpolatorBase(global::IDLocalCharacterAddon.AddonFlags addonFlags) : this(false, addonFlags)
	{
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00021A60 File Offset: 0x0001FC60
	protected CharacterInterpolatorBase(bool customPusher, global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | (global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake))
	{
		this.customPusher = customPusher;
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06000795 RID: 1941 RVA: 0x00021A74 File Offset: 0x0001FC74
	public int bufferCapacity
	{
		get
		{
			return this.tbuffer.Length;
		}
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00021A80 File Offset: 0x0001FC80
	protected override void OnAddonPostAwake()
	{
		this.tbuffer = new global::TimeStamped<T>[(this._bufferCapacity > 0) ? this._bufferCapacity : 32];
		this._bufferCapacity = this.tbuffer.Length;
		for (int i = 0; i < this._bufferCapacity; i++)
		{
			this.tbuffer[i].index = i;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06000797 RID: 1943 RVA: 0x00021AE8 File Offset: 0x0001FCE8
	public new double storedDuration
	{
		get
		{
			return (this.len >= 2) ? (this.tbuffer[this.tbuffer[0].index].timeStamp - this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp) : 0.0;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06000798 RID: 1944 RVA: 0x00021B5C File Offset: 0x0001FD5C
	public new double oldestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[0].index].timeStamp : double.NegativeInfinity;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000799 RID: 1945 RVA: 0x00021BA4 File Offset: 0x0001FDA4
	public new double newestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp : double.PositiveInfinity;
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00021BF4 File Offset: 0x0001FDF4
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

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x0600079B RID: 1947 RVA: 0x00021C60 File Offset: 0x0001FE60
	protected sealed override double __newestTimeStamp
	{
		get
		{
			return this.newestTimeStamp;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x0600079C RID: 1948 RVA: 0x00021C68 File Offset: 0x0001FE68
	protected sealed override double __oldestTimeStamp
	{
		get
		{
			return this.oldestTimeStamp;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x0600079D RID: 1949 RVA: 0x00021C70 File Offset: 0x0001FE70
	protected sealed override double __storedDuration
	{
		get
		{
			return this.storedDuration;
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00021C78 File Offset: 0x0001FE78
	protected sealed override void __Clear()
	{
		this.Clear();
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00021C80 File Offset: 0x0001FE80
	public override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		throw new NotImplementedException("The thing using this has not implemented a way to take pos, rot to " + typeof(T));
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00021C9C File Offset: 0x0001FE9C
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

	// Token: 0x060007A1 RID: 1953 RVA: 0x00021ED8 File Offset: 0x000200D8
	protected virtual bool CustomPusher(ref T state, ref double timeStamp)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00021EE0 File Offset: 0x000200E0
	public void SetGoals(ref T state, ref double timeStamp)
	{
		if (this.customPusher)
		{
			double num = timeStamp;
			T t = state;
			if (this.CustomPusher(ref t, ref num))
			{
				this.Push(ref t, ref num);
			}
		}
		else
		{
			this.Push(ref state, ref timeStamp);
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00021F28 File Offset: 0x00020128
	public void SetGoals(ref global::TimeStamped<T> state)
	{
		double timeStamp = state.timeStamp;
		T value = state.value;
		this.SetGoals(ref value, ref timeStamp);
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00021F50 File Offset: 0x00020150
	public new global::CharacterInterpolatorBase interpolator
	{
		get
		{
			return this;
		}
	}

	// Token: 0x040005DC RID: 1500
	private const global::IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake;

	// Token: 0x040005DD RID: 1501
	[NonSerialized]
	protected global::TimeStamped<T>[] tbuffer;

	// Token: 0x040005DE RID: 1502
	private readonly bool customPusher;
}
