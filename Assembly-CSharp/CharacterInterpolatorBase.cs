using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000122 RID: 290
public abstract class CharacterInterpolatorBase : global::IDLocalCharacterAddon, IIDLocalInterpolator
{
	// Token: 0x06000776 RID: 1910 RVA: 0x00021414 File Offset: 0x0001F614
	internal CharacterInterpolatorBase(global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x00021434 File Offset: 0x0001F634
	IDMain IIDLocalInterpolator.idMain
	{
		get
		{
			return this.idMain;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06000778 RID: 1912 RVA: 0x0002143C File Offset: 0x0001F63C
	IDLocal IIDLocalInterpolator.self
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000779 RID: 1913
	protected abstract double __storedDuration { get; }

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x0600077A RID: 1914
	protected abstract double __oldestTimeStamp { get; }

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x0600077B RID: 1915
	protected abstract double __newestTimeStamp { get; }

	// Token: 0x0600077C RID: 1916
	protected abstract void __Clear();

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x0600077D RID: 1917 RVA: 0x00021440 File Offset: 0x0001F640
	public double storedDuration
	{
		get
		{
			return this.__storedDuration;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x0600077E RID: 1918 RVA: 0x00021448 File Offset: 0x0001F648
	public double oldestTimeStamp
	{
		get
		{
			return this.__oldestTimeStamp;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x0600077F RID: 1919 RVA: 0x00021450 File Offset: 0x0001F650
	public double newestTimeStamp
	{
		get
		{
			return this.__newestTimeStamp;
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00021458 File Offset: 0x0001F658
	protected override void OnAddonAwake()
	{
		global::CharacterInterpolatorTrait trait = base.idMain.GetTrait<global::CharacterInterpolatorTrait>();
		if (trait)
		{
			if (trait.bufferCapacity > 0)
			{
				this._bufferCapacity = trait.bufferCapacity;
			}
			this.extrapolate = trait.allowExtrapolation;
			this.allowableTimeSpan = trait.allowableTimeSpan;
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x000214AC File Offset: 0x0001F6AC
	public void Clear()
	{
		this.__Clear();
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x000214B4 File Offset: 0x0001F6B4
	internal static void SyncronizeAll()
	{
		global::CharacterInterpolatorBase.Interpolators.UpdateAll();
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000783 RID: 1923 RVA: 0x000214BC File Offset: 0x0001F6BC
	// (set) Token: 0x06000784 RID: 1924 RVA: 0x000214C4 File Offset: 0x0001F6C4
	public bool running
	{
		get
		{
			return this._running;
		}
		set
		{
			if (this._destroying)
			{
				value = false;
			}
			if (this._running != value)
			{
				if (value)
				{
					this._running = global::CharacterInterpolatorBase.Interpolators.SetEnabled(this);
				}
				else
				{
					this._running = !global::CharacterInterpolatorBase.Interpolators.SetDisabled(this);
				}
			}
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000785 RID: 1925 RVA: 0x00021514 File Offset: 0x0001F714
	// (set) Token: 0x06000786 RID: 1926 RVA: 0x0002151C File Offset: 0x0001F71C
	[Obsolete("Use .running for interpolators", true)]
	public bool enabled
	{
		get
		{
			return this.running;
		}
		set
		{
			this.running = value;
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00021528 File Offset: 0x0001F728
	protected void OnDestroy()
	{
		this._destroying = true;
		if (this._running)
		{
			global::CharacterInterpolatorBase.Interpolators.SetDisabled(this);
			this._running = false;
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00021558 File Offset: 0x0001F758
	public virtual void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		if (!this.initialized)
		{
			base.transform.position = pos;
			if (base.idMain is global::Character)
			{
				global::Angle2 eyesAngles = global::Angle2.LookDirection(rot * Vector3.forward);
				eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
				base.idMain.eyesAngles = eyesAngles;
			}
			else
			{
				base.transform.rotation = rot;
			}
			this.initialized = true;
		}
		this.targetPos = pos;
		this.targetRot = rot;
		this.fromPos = base.transform.position;
		if (base.idMain is global::Character)
		{
			this.fromRot = base.idMain.eyesAngles.quat;
		}
		else
		{
			this.fromRot = base.transform.rotation;
		}
		this.lerpStartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00021644 File Offset: 0x0001F844
	protected virtual void Syncronize()
	{
		float num = (Time.realtimeSinceStartup - this.lerpStartTime) / global::Interpolation.@struct.totalDelaySecondsF;
		Vector3 vector = Vector3.Lerp(this.fromPos, this.targetPos, num);
		Quaternion quaternion = Quaternion.Slerp(this.fromRot, this.targetRot, num);
		if (base.idMain is global::Character)
		{
			global::Character idMain = base.idMain;
			idMain.origin = vector;
			global::Angle2 eyesAngles = global::Angle2.LookDirection(quaternion * Vector3.forward);
			eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
			idMain.eyesAngles = eyesAngles;
		}
		else
		{
			base.transform.position = vector;
			base.transform.rotation = quaternion;
		}
	}

	// Token: 0x040005C7 RID: 1479
	protected const int kDefaultBufferCapacity = 32;

	// Token: 0x040005C8 RID: 1480
	private const global::IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake;

	// Token: 0x040005C9 RID: 1481
	[NonSerialized]
	private Vector3 targetPos;

	// Token: 0x040005CA RID: 1482
	[NonSerialized]
	private Vector3 fromPos;

	// Token: 0x040005CB RID: 1483
	[NonSerialized]
	private Quaternion targetRot;

	// Token: 0x040005CC RID: 1484
	[NonSerialized]
	private Quaternion fromRot;

	// Token: 0x040005CD RID: 1485
	[NonSerialized]
	private float lerpStartTime;

	// Token: 0x040005CE RID: 1486
	[NonSerialized]
	private bool initialized;

	// Token: 0x040005CF RID: 1487
	[NonSerialized]
	protected int _bufferCapacity = 32;

	// Token: 0x040005D0 RID: 1488
	[NonSerialized]
	protected bool extrapolate;

	// Token: 0x040005D1 RID: 1489
	[NonSerialized]
	protected float allowableTimeSpan = 0.1f;

	// Token: 0x040005D2 RID: 1490
	[NonSerialized]
	protected int len;

	// Token: 0x040005D3 RID: 1491
	[NonSerialized]
	private bool _running;

	// Token: 0x040005D4 RID: 1492
	[NonSerialized]
	private bool _destroying;

	// Token: 0x02000123 RID: 291
	private static class Interpolators
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00021714 File Offset: 0x0001F914
		public static void UpdateAll()
		{
			if (global::CharacterInterpolatorBase.Interpolators.iterating)
			{
				return;
			}
			HashSet<global::CharacterInterpolatorBase> hashSet;
			if (global::CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset2;
			}
			else
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset1;
			}
			try
			{
				global::CharacterInterpolatorBase.Interpolators.iterating = true;
				foreach (global::CharacterInterpolatorBase characterInterpolatorBase in hashSet)
				{
					try
					{
						characterInterpolatorBase.Syncronize();
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
				}
			}
			finally
			{
				if (global::CharacterInterpolatorBase.Interpolators.caughtIterating)
				{
					global::CharacterInterpolatorBase.Interpolators.swapped = !global::CharacterInterpolatorBase.Interpolators.swapped;
					if (global::CharacterInterpolatorBase.Interpolators.swapped)
					{
						global::CharacterInterpolatorBase.Interpolators.hashset1.Clear();
					}
					else
					{
						global::CharacterInterpolatorBase.Interpolators.hashset2.Clear();
					}
				}
				global::CharacterInterpolatorBase.Interpolators.iterating = false;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00021828 File Offset: 0x0001FA28
		public static bool SetEnabled(global::CharacterInterpolatorBase interpolator)
		{
			if (!global::CharacterInterpolatorBase.Interpolators.iterating)
			{
				return ((!global::CharacterInterpolatorBase.Interpolators.swapped) ? global::CharacterInterpolatorBase.Interpolators.hashset1 : global::CharacterInterpolatorBase.Interpolators.hashset2).Add(interpolator);
			}
			if (global::CharacterInterpolatorBase.Interpolators.caughtIterating)
			{
				return ((!global::CharacterInterpolatorBase.Interpolators.swapped) ? global::CharacterInterpolatorBase.Interpolators.hashset2 : global::CharacterInterpolatorBase.Interpolators.hashset1).Add(interpolator);
			}
			HashSet<global::CharacterInterpolatorBase> hashSet;
			HashSet<global::CharacterInterpolatorBase> hashSet2;
			if (global::CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset2;
				hashSet2 = global::CharacterInterpolatorBase.Interpolators.hashset1;
			}
			else
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset1;
				hashSet2 = global::CharacterInterpolatorBase.Interpolators.hashset2;
			}
			if (hashSet.Contains(interpolator))
			{
				return false;
			}
			global::CharacterInterpolatorBase.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Add(interpolator);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000218D4 File Offset: 0x0001FAD4
		public static bool SetDisabled(global::CharacterInterpolatorBase interpolator)
		{
			if (!global::CharacterInterpolatorBase.Interpolators.iterating)
			{
				return ((!global::CharacterInterpolatorBase.Interpolators.swapped) ? global::CharacterInterpolatorBase.Interpolators.hashset1 : global::CharacterInterpolatorBase.Interpolators.hashset2).Remove(interpolator);
			}
			if (global::CharacterInterpolatorBase.Interpolators.caughtIterating)
			{
				return ((!global::CharacterInterpolatorBase.Interpolators.swapped) ? global::CharacterInterpolatorBase.Interpolators.hashset2 : global::CharacterInterpolatorBase.Interpolators.hashset1).Remove(interpolator);
			}
			HashSet<global::CharacterInterpolatorBase> hashSet;
			HashSet<global::CharacterInterpolatorBase> hashSet2;
			if (global::CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset2;
				hashSet2 = global::CharacterInterpolatorBase.Interpolators.hashset1;
			}
			else
			{
				hashSet = global::CharacterInterpolatorBase.Interpolators.hashset1;
				hashSet2 = global::CharacterInterpolatorBase.Interpolators.hashset2;
			}
			if (!hashSet.Contains(interpolator))
			{
				return false;
			}
			global::CharacterInterpolatorBase.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Remove(interpolator);
		}

		// Token: 0x040005D5 RID: 1493
		private static readonly HashSet<global::CharacterInterpolatorBase> hashset1 = new HashSet<global::CharacterInterpolatorBase>();

		// Token: 0x040005D6 RID: 1494
		private static readonly HashSet<global::CharacterInterpolatorBase> hashset2 = new HashSet<global::CharacterInterpolatorBase>();

		// Token: 0x040005D7 RID: 1495
		private static bool swapped;

		// Token: 0x040005D8 RID: 1496
		private static bool iterating;

		// Token: 0x040005D9 RID: 1497
		private static bool caughtIterating;
	}
}
