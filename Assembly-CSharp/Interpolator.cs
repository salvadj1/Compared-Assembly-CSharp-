using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002BC RID: 700
[RequireComponent(typeof(uLinkNetworkView))]
public class Interpolator : IDLocal, IIDLocalInterpolator
{
	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x060018EE RID: 6382 RVA: 0x000621E4 File Offset: 0x000603E4
	IDLocal IIDLocalInterpolator.self
	{
		get
		{
			return this;
		}
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x000621E8 File Offset: 0x000603E8
	internal static void SyncronizeAll()
	{
		Interpolator.Interpolators.UpdateAll();
	}

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000621F0 File Offset: 0x000603F0
	// (set) Token: 0x060018F1 RID: 6385 RVA: 0x000621F8 File Offset: 0x000603F8
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
					this._running = Interpolator.Interpolators.SetEnabled(this);
				}
				else
				{
					this._running = !Interpolator.Interpolators.SetDisabled(this);
				}
			}
		}
	}

	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00062248 File Offset: 0x00060448
	// (set) Token: 0x060018F3 RID: 6387 RVA: 0x00062250 File Offset: 0x00060450
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

	// Token: 0x060018F4 RID: 6388 RVA: 0x0006225C File Offset: 0x0006045C
	protected void OnDestroy()
	{
		this._destroying = true;
		if (this._running)
		{
			Interpolator.Interpolators.SetDisabled(this);
			this._running = false;
		}
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x0006228C File Offset: 0x0006048C
	public virtual void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		if (!this.initialized)
		{
			base.transform.position = pos;
			if (this.idMain is Character)
			{
				Angle2 eyesAngles = Angle2.LookDirection(rot * Vector3.forward);
				eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
				((Character)this.idMain).eyesAngles = eyesAngles;
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
		if (this.idMain is Character)
		{
			this.fromRot = ((Character)this.idMain).eyesAngles.quat;
		}
		else
		{
			this.fromRot = base.transform.rotation;
		}
		this.lerpStartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x00062380 File Offset: 0x00060580
	protected virtual void Syncronize()
	{
		float num = (Time.realtimeSinceStartup - this.lerpStartTime) / Interpolation.@struct.totalDelaySecondsF;
		Vector3 vector = Vector3.Lerp(this.fromPos, this.targetPos, num);
		Quaternion quaternion = Quaternion.Slerp(this.fromRot, this.targetRot, num);
		if (this.idMain is Character)
		{
			Character character = (Character)this.idMain;
			character.origin = vector;
			Angle2 eyesAngles = Angle2.LookDirection(quaternion * Vector3.forward);
			eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
			character.eyesAngles = eyesAngles;
		}
		else
		{
			base.transform.position = vector;
			base.transform.rotation = quaternion;
		}
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x00062440 File Offset: 0x00060640
	virtual IDMain get_idMain()
	{
		return base.idMain;
	}

	// Token: 0x04000D45 RID: 3397
	[NonSerialized]
	private Vector3 targetPos;

	// Token: 0x04000D46 RID: 3398
	[NonSerialized]
	private Vector3 fromPos;

	// Token: 0x04000D47 RID: 3399
	[NonSerialized]
	private Quaternion targetRot;

	// Token: 0x04000D48 RID: 3400
	[NonSerialized]
	private Quaternion fromRot;

	// Token: 0x04000D49 RID: 3401
	[NonSerialized]
	private float lerpStartTime;

	// Token: 0x04000D4A RID: 3402
	[NonSerialized]
	private bool initialized;

	// Token: 0x04000D4B RID: 3403
	[NonSerialized]
	private bool _running;

	// Token: 0x04000D4C RID: 3404
	[NonSerialized]
	private bool _destroying;

	// Token: 0x020002BD RID: 701
	private static class Interpolators
	{
		// Token: 0x060018F9 RID: 6393 RVA: 0x00062460 File Offset: 0x00060660
		public static void UpdateAll()
		{
			if (Interpolator.Interpolators.iterating)
			{
				return;
			}
			HashSet<Interpolator> hashSet;
			if (Interpolator.Interpolators.swapped)
			{
				hashSet = Interpolator.Interpolators.hashset2;
			}
			else
			{
				hashSet = Interpolator.Interpolators.hashset1;
			}
			try
			{
				Interpolator.Interpolators.iterating = true;
				foreach (Interpolator interpolator in hashSet)
				{
					try
					{
						interpolator.Syncronize();
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
				}
			}
			finally
			{
				if (Interpolator.Interpolators.caughtIterating)
				{
					Interpolator.Interpolators.swapped = !Interpolator.Interpolators.swapped;
					if (Interpolator.Interpolators.swapped)
					{
						Interpolator.Interpolators.hashset1.Clear();
					}
					else
					{
						Interpolator.Interpolators.hashset2.Clear();
					}
				}
				Interpolator.Interpolators.iterating = false;
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00062574 File Offset: 0x00060774
		public static bool SetEnabled(Interpolator interpolator)
		{
			if (!Interpolator.Interpolators.iterating)
			{
				return ((!Interpolator.Interpolators.swapped) ? Interpolator.Interpolators.hashset1 : Interpolator.Interpolators.hashset2).Add(interpolator);
			}
			if (Interpolator.Interpolators.caughtIterating)
			{
				return ((!Interpolator.Interpolators.swapped) ? Interpolator.Interpolators.hashset2 : Interpolator.Interpolators.hashset1).Add(interpolator);
			}
			HashSet<Interpolator> hashSet;
			HashSet<Interpolator> hashSet2;
			if (Interpolator.Interpolators.swapped)
			{
				hashSet = Interpolator.Interpolators.hashset2;
				hashSet2 = Interpolator.Interpolators.hashset1;
			}
			else
			{
				hashSet = Interpolator.Interpolators.hashset1;
				hashSet2 = Interpolator.Interpolators.hashset2;
			}
			if (hashSet.Contains(interpolator))
			{
				return false;
			}
			Interpolator.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Add(interpolator);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00062620 File Offset: 0x00060820
		public static bool SetDisabled(Interpolator interpolator)
		{
			if (!Interpolator.Interpolators.iterating)
			{
				return ((!Interpolator.Interpolators.swapped) ? Interpolator.Interpolators.hashset1 : Interpolator.Interpolators.hashset2).Remove(interpolator);
			}
			if (Interpolator.Interpolators.caughtIterating)
			{
				return ((!Interpolator.Interpolators.swapped) ? Interpolator.Interpolators.hashset2 : Interpolator.Interpolators.hashset1).Remove(interpolator);
			}
			HashSet<Interpolator> hashSet;
			HashSet<Interpolator> hashSet2;
			if (Interpolator.Interpolators.swapped)
			{
				hashSet = Interpolator.Interpolators.hashset2;
				hashSet2 = Interpolator.Interpolators.hashset1;
			}
			else
			{
				hashSet = Interpolator.Interpolators.hashset1;
				hashSet2 = Interpolator.Interpolators.hashset2;
			}
			if (!hashSet.Contains(interpolator))
			{
				return false;
			}
			Interpolator.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Remove(interpolator);
		}

		// Token: 0x04000D4D RID: 3405
		private static readonly HashSet<Interpolator> hashset1 = new HashSet<Interpolator>();

		// Token: 0x04000D4E RID: 3406
		private static readonly HashSet<Interpolator> hashset2 = new HashSet<Interpolator>();

		// Token: 0x04000D4F RID: 3407
		private static bool swapped;

		// Token: 0x04000D50 RID: 3408
		private static bool iterating;

		// Token: 0x04000D51 RID: 3409
		private static bool caughtIterating;
	}
}
