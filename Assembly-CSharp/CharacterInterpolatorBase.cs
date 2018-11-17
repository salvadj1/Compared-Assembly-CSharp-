using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000103 RID: 259
public abstract class CharacterInterpolatorBase : IDLocalCharacterAddon, IIDLocalInterpolator
{
	// Token: 0x060006A4 RID: 1700 RVA: 0x0001E840 File Offset: 0x0001CA40
	internal CharacterInterpolatorBase(IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001E860 File Offset: 0x0001CA60
	IDMain IIDLocalInterpolator.idMain
	{
		get
		{
			return this.idMain;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001E868 File Offset: 0x0001CA68
	IDLocal IIDLocalInterpolator.self
	{
		get
		{
			return this;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060006A7 RID: 1703
	protected abstract double __storedDuration { get; }

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060006A8 RID: 1704
	protected abstract double __oldestTimeStamp { get; }

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060006A9 RID: 1705
	protected abstract double __newestTimeStamp { get; }

	// Token: 0x060006AA RID: 1706
	protected abstract void __Clear();

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001E86C File Offset: 0x0001CA6C
	public double storedDuration
	{
		get
		{
			return this.__storedDuration;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001E874 File Offset: 0x0001CA74
	public double oldestTimeStamp
	{
		get
		{
			return this.__oldestTimeStamp;
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001E87C File Offset: 0x0001CA7C
	public double newestTimeStamp
	{
		get
		{
			return this.__newestTimeStamp;
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0001E884 File Offset: 0x0001CA84
	protected override void OnAddonAwake()
	{
		CharacterInterpolatorTrait trait = base.idMain.GetTrait<CharacterInterpolatorTrait>();
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

	// Token: 0x060006AF RID: 1711 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
	public void Clear()
	{
		this.__Clear();
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
	internal static void SyncronizeAll()
	{
		CharacterInterpolatorBase.Interpolators.UpdateAll();
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001E8E8 File Offset: 0x0001CAE8
	// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
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
					this._running = CharacterInterpolatorBase.Interpolators.SetEnabled(this);
				}
				else
				{
					this._running = !CharacterInterpolatorBase.Interpolators.SetDisabled(this);
				}
			}
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001E940 File Offset: 0x0001CB40
	// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001E948 File Offset: 0x0001CB48
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

	// Token: 0x060006B5 RID: 1717 RVA: 0x0001E954 File Offset: 0x0001CB54
	protected void OnDestroy()
	{
		this._destroying = true;
		if (this._running)
		{
			CharacterInterpolatorBase.Interpolators.SetDisabled(this);
			this._running = false;
		}
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0001E984 File Offset: 0x0001CB84
	public virtual void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		if (!this.initialized)
		{
			base.transform.position = pos;
			if (base.idMain is Character)
			{
				Angle2 eyesAngles = Angle2.LookDirection(rot * Vector3.forward);
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
		if (base.idMain is Character)
		{
			this.fromRot = base.idMain.eyesAngles.quat;
		}
		else
		{
			this.fromRot = base.transform.rotation;
		}
		this.lerpStartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0001EA70 File Offset: 0x0001CC70
	protected virtual void Syncronize()
	{
		float num = (Time.realtimeSinceStartup - this.lerpStartTime) / Interpolation.@struct.totalDelaySecondsF;
		Vector3 vector = Vector3.Lerp(this.fromPos, this.targetPos, num);
		Quaternion quaternion = Quaternion.Slerp(this.fromRot, this.targetRot, num);
		if (base.idMain is Character)
		{
			Character idMain = base.idMain;
			idMain.origin = vector;
			Angle2 eyesAngles = Angle2.LookDirection(quaternion * Vector3.forward);
			eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
			idMain.eyesAngles = eyesAngles;
		}
		else
		{
			base.transform.position = vector;
			base.transform.rotation = quaternion;
		}
	}

	// Token: 0x040004FC RID: 1276
	protected const int kDefaultBufferCapacity = 32;

	// Token: 0x040004FD RID: 1277
	private const IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake;

	// Token: 0x040004FE RID: 1278
	[NonSerialized]
	private Vector3 targetPos;

	// Token: 0x040004FF RID: 1279
	[NonSerialized]
	private Vector3 fromPos;

	// Token: 0x04000500 RID: 1280
	[NonSerialized]
	private Quaternion targetRot;

	// Token: 0x04000501 RID: 1281
	[NonSerialized]
	private Quaternion fromRot;

	// Token: 0x04000502 RID: 1282
	[NonSerialized]
	private float lerpStartTime;

	// Token: 0x04000503 RID: 1283
	[NonSerialized]
	private bool initialized;

	// Token: 0x04000504 RID: 1284
	[NonSerialized]
	protected int _bufferCapacity = 32;

	// Token: 0x04000505 RID: 1285
	[NonSerialized]
	protected bool extrapolate;

	// Token: 0x04000506 RID: 1286
	[NonSerialized]
	protected float allowableTimeSpan = 0.1f;

	// Token: 0x04000507 RID: 1287
	[NonSerialized]
	protected int len;

	// Token: 0x04000508 RID: 1288
	[NonSerialized]
	private bool _running;

	// Token: 0x04000509 RID: 1289
	[NonSerialized]
	private bool _destroying;

	// Token: 0x02000104 RID: 260
	private static class Interpolators
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001EB40 File Offset: 0x0001CD40
		public static void UpdateAll()
		{
			if (CharacterInterpolatorBase.Interpolators.iterating)
			{
				return;
			}
			HashSet<CharacterInterpolatorBase> hashSet;
			if (CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset2;
			}
			else
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset1;
			}
			try
			{
				CharacterInterpolatorBase.Interpolators.iterating = true;
				foreach (CharacterInterpolatorBase characterInterpolatorBase in hashSet)
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
				if (CharacterInterpolatorBase.Interpolators.caughtIterating)
				{
					CharacterInterpolatorBase.Interpolators.swapped = !CharacterInterpolatorBase.Interpolators.swapped;
					if (CharacterInterpolatorBase.Interpolators.swapped)
					{
						CharacterInterpolatorBase.Interpolators.hashset1.Clear();
					}
					else
					{
						CharacterInterpolatorBase.Interpolators.hashset2.Clear();
					}
				}
				CharacterInterpolatorBase.Interpolators.iterating = false;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001EC54 File Offset: 0x0001CE54
		public static bool SetEnabled(CharacterInterpolatorBase interpolator)
		{
			if (!CharacterInterpolatorBase.Interpolators.iterating)
			{
				return ((!CharacterInterpolatorBase.Interpolators.swapped) ? CharacterInterpolatorBase.Interpolators.hashset1 : CharacterInterpolatorBase.Interpolators.hashset2).Add(interpolator);
			}
			if (CharacterInterpolatorBase.Interpolators.caughtIterating)
			{
				return ((!CharacterInterpolatorBase.Interpolators.swapped) ? CharacterInterpolatorBase.Interpolators.hashset2 : CharacterInterpolatorBase.Interpolators.hashset1).Add(interpolator);
			}
			HashSet<CharacterInterpolatorBase> hashSet;
			HashSet<CharacterInterpolatorBase> hashSet2;
			if (CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset2;
				hashSet2 = CharacterInterpolatorBase.Interpolators.hashset1;
			}
			else
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset1;
				hashSet2 = CharacterInterpolatorBase.Interpolators.hashset2;
			}
			if (hashSet.Contains(interpolator))
			{
				return false;
			}
			CharacterInterpolatorBase.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Add(interpolator);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001ED00 File Offset: 0x0001CF00
		public static bool SetDisabled(CharacterInterpolatorBase interpolator)
		{
			if (!CharacterInterpolatorBase.Interpolators.iterating)
			{
				return ((!CharacterInterpolatorBase.Interpolators.swapped) ? CharacterInterpolatorBase.Interpolators.hashset1 : CharacterInterpolatorBase.Interpolators.hashset2).Remove(interpolator);
			}
			if (CharacterInterpolatorBase.Interpolators.caughtIterating)
			{
				return ((!CharacterInterpolatorBase.Interpolators.swapped) ? CharacterInterpolatorBase.Interpolators.hashset2 : CharacterInterpolatorBase.Interpolators.hashset1).Remove(interpolator);
			}
			HashSet<CharacterInterpolatorBase> hashSet;
			HashSet<CharacterInterpolatorBase> hashSet2;
			if (CharacterInterpolatorBase.Interpolators.swapped)
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset2;
				hashSet2 = CharacterInterpolatorBase.Interpolators.hashset1;
			}
			else
			{
				hashSet = CharacterInterpolatorBase.Interpolators.hashset1;
				hashSet2 = CharacterInterpolatorBase.Interpolators.hashset2;
			}
			if (!hashSet.Contains(interpolator))
			{
				return false;
			}
			CharacterInterpolatorBase.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Remove(interpolator);
		}

		// Token: 0x0400050A RID: 1290
		private static readonly HashSet<CharacterInterpolatorBase> hashset1 = new HashSet<CharacterInterpolatorBase>();

		// Token: 0x0400050B RID: 1291
		private static readonly HashSet<CharacterInterpolatorBase> hashset2 = new HashSet<CharacterInterpolatorBase>();

		// Token: 0x0400050C RID: 1292
		private static bool swapped;

		// Token: 0x0400050D RID: 1293
		private static bool iterating;

		// Token: 0x0400050E RID: 1294
		private static bool caughtIterating;
	}
}
