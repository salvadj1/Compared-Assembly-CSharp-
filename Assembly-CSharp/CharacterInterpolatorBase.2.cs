using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public abstract class CharacterInterpolatorBase<T> : CharacterInterpolatorBase
{
	// Token: 0x060006BF RID: 1727 RVA: 0x0001EE68 File Offset: 0x0001D068
	protected CharacterInterpolatorBase() : this(false, IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake)
	{
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0001EE74 File Offset: 0x0001D074
	protected CharacterInterpolatorBase(bool customPusher) : this(customPusher, IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake)
	{
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0001EE80 File Offset: 0x0001D080
	protected CharacterInterpolatorBase(IDLocalCharacterAddon.AddonFlags addonFlags) : this(false, addonFlags)
	{
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0001EE8C File Offset: 0x0001D08C
	protected CharacterInterpolatorBase(bool customPusher, IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | (IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake))
	{
		this.customPusher = customPusher;
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
	public int bufferCapacity
	{
		get
		{
			return this.tbuffer.Length;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0001EEAC File Offset: 0x0001D0AC
	protected override void OnAddonPostAwake()
	{
		this.tbuffer = new TimeStamped<T>[(this._bufferCapacity > 0) ? this._bufferCapacity : 32];
		this._bufferCapacity = this.tbuffer.Length;
		for (int i = 0; i < this._bufferCapacity; i++)
		{
			this.tbuffer[i].index = i;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001EF14 File Offset: 0x0001D114
	public new double storedDuration
	{
		get
		{
			return (this.len >= 2) ? (this.tbuffer[this.tbuffer[0].index].timeStamp - this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp) : 0.0;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001EF88 File Offset: 0x0001D188
	public new double oldestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[0].index].timeStamp : double.NegativeInfinity;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
	public new double newestTimeStamp
	{
		get
		{
			return (this.len != 0) ? this.tbuffer[this.tbuffer[this.len - 1].index].timeStamp : double.PositiveInfinity;
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0001F020 File Offset: 0x0001D220
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

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001F08C File Offset: 0x0001D28C
	protected sealed override double __newestTimeStamp
	{
		get
		{
			return this.newestTimeStamp;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001F094 File Offset: 0x0001D294
	protected sealed override double __oldestTimeStamp
	{
		get
		{
			return this.oldestTimeStamp;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001F09C File Offset: 0x0001D29C
	protected sealed override double __storedDuration
	{
		get
		{
			return this.storedDuration;
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
	protected sealed override void __Clear()
	{
		this.Clear();
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0001F0AC File Offset: 0x0001D2AC
	public override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		throw new NotImplementedException("The thing using this has not implemented a way to take pos, rot to " + typeof(T));
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0001F0C8 File Offset: 0x0001D2C8
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

	// Token: 0x060006CF RID: 1743 RVA: 0x0001F304 File Offset: 0x0001D504
	protected virtual bool CustomPusher(ref T state, ref double timeStamp)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0001F30C File Offset: 0x0001D50C
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

	// Token: 0x060006D1 RID: 1745 RVA: 0x0001F354 File Offset: 0x0001D554
	public void SetGoals(ref TimeStamped<T> state)
	{
		double timeStamp = state.timeStamp;
		T value = state.value;
		this.SetGoals(ref value, ref timeStamp);
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001F37C File Offset: 0x0001D57C
	public new CharacterInterpolatorBase interpolator
	{
		get
		{
			return this;
		}
	}

	// Token: 0x04000511 RID: 1297
	private const IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake;

	// Token: 0x04000512 RID: 1298
	[NonSerialized]
	protected TimeStamped<T>[] tbuffer;

	// Token: 0x04000513 RID: 1299
	private readonly bool customPusher;
}
