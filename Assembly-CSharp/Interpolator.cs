using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F9 RID: 761
[RequireComponent(typeof(uLinkNetworkView))]
public class Interpolator : IDLocal, IIDLocalInterpolator
{
	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00066B58 File Offset: 0x00064D58
	IDLocal IIDLocalInterpolator.self
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x00066B5C File Offset: 0x00064D5C
	internal static void SyncronizeAll()
	{
		global::Interpolator.Interpolators.UpdateAll();
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00066B64 File Offset: 0x00064D64
	// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00066B6C File Offset: 0x00064D6C
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
					this._running = global::Interpolator.Interpolators.SetEnabled(this);
				}
				else
				{
					this._running = !global::Interpolator.Interpolators.SetDisabled(this);
				}
			}
		}
	}

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00066BBC File Offset: 0x00064DBC
	// (set) Token: 0x06001A83 RID: 6787 RVA: 0x00066BC4 File Offset: 0x00064DC4
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

	// Token: 0x06001A84 RID: 6788 RVA: 0x00066BD0 File Offset: 0x00064DD0
	protected void OnDestroy()
	{
		this._destroying = true;
		if (this._running)
		{
			global::Interpolator.Interpolators.SetDisabled(this);
			this._running = false;
		}
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x00066C00 File Offset: 0x00064E00
	public virtual void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		if (!this.initialized)
		{
			base.transform.position = pos;
			if (this.idMain is global::Character)
			{
				global::Angle2 eyesAngles = global::Angle2.LookDirection(rot * Vector3.forward);
				eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
				((global::Character)this.idMain).eyesAngles = eyesAngles;
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
		if (this.idMain is global::Character)
		{
			this.fromRot = ((global::Character)this.idMain).eyesAngles.quat;
		}
		else
		{
			this.fromRot = base.transform.rotation;
		}
		this.lerpStartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x00066CF4 File Offset: 0x00064EF4
	protected virtual void Syncronize()
	{
		float num = (Time.realtimeSinceStartup - this.lerpStartTime) / global::Interpolation.@struct.totalDelaySecondsF;
		Vector3 vector = Vector3.Lerp(this.fromPos, this.targetPos, num);
		Quaternion quaternion = Quaternion.Slerp(this.fromRot, this.targetRot, num);
		if (this.idMain is global::Character)
		{
			global::Character character = (global::Character)this.idMain;
			character.origin = vector;
			global::Angle2 eyesAngles = global::Angle2.LookDirection(quaternion * Vector3.forward);
			eyesAngles.pitch = Mathf.DeltaAngle(0f, eyesAngles.pitch);
			character.eyesAngles = eyesAngles;
		}
		else
		{
			base.transform.position = vector;
			base.transform.rotation = quaternion;
		}
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x00066DB4 File Offset: 0x00064FB4
	virtual IDMain get_idMain()
	{
		return base.idMain;
	}

	// Token: 0x04000E80 RID: 3712
	[NonSerialized]
	private Vector3 targetPos;

	// Token: 0x04000E81 RID: 3713
	[NonSerialized]
	private Vector3 fromPos;

	// Token: 0x04000E82 RID: 3714
	[NonSerialized]
	private Quaternion targetRot;

	// Token: 0x04000E83 RID: 3715
	[NonSerialized]
	private Quaternion fromRot;

	// Token: 0x04000E84 RID: 3716
	[NonSerialized]
	private float lerpStartTime;

	// Token: 0x04000E85 RID: 3717
	[NonSerialized]
	private bool initialized;

	// Token: 0x04000E86 RID: 3718
	[NonSerialized]
	private bool _running;

	// Token: 0x04000E87 RID: 3719
	[NonSerialized]
	private bool _destroying;

	// Token: 0x020002FA RID: 762
	private static class Interpolators
	{
		// Token: 0x06001A89 RID: 6793 RVA: 0x00066DD4 File Offset: 0x00064FD4
		public static void UpdateAll()
		{
			if (global::Interpolator.Interpolators.iterating)
			{
				return;
			}
			HashSet<global::Interpolator> hashSet;
			if (global::Interpolator.Interpolators.swapped)
			{
				hashSet = global::Interpolator.Interpolators.hashset2;
			}
			else
			{
				hashSet = global::Interpolator.Interpolators.hashset1;
			}
			try
			{
				global::Interpolator.Interpolators.iterating = true;
				foreach (global::Interpolator interpolator in hashSet)
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
				if (global::Interpolator.Interpolators.caughtIterating)
				{
					global::Interpolator.Interpolators.swapped = !global::Interpolator.Interpolators.swapped;
					if (global::Interpolator.Interpolators.swapped)
					{
						global::Interpolator.Interpolators.hashset1.Clear();
					}
					else
					{
						global::Interpolator.Interpolators.hashset2.Clear();
					}
				}
				global::Interpolator.Interpolators.iterating = false;
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00066EE8 File Offset: 0x000650E8
		public static bool SetEnabled(global::Interpolator interpolator)
		{
			if (!global::Interpolator.Interpolators.iterating)
			{
				return ((!global::Interpolator.Interpolators.swapped) ? global::Interpolator.Interpolators.hashset1 : global::Interpolator.Interpolators.hashset2).Add(interpolator);
			}
			if (global::Interpolator.Interpolators.caughtIterating)
			{
				return ((!global::Interpolator.Interpolators.swapped) ? global::Interpolator.Interpolators.hashset2 : global::Interpolator.Interpolators.hashset1).Add(interpolator);
			}
			HashSet<global::Interpolator> hashSet;
			HashSet<global::Interpolator> hashSet2;
			if (global::Interpolator.Interpolators.swapped)
			{
				hashSet = global::Interpolator.Interpolators.hashset2;
				hashSet2 = global::Interpolator.Interpolators.hashset1;
			}
			else
			{
				hashSet = global::Interpolator.Interpolators.hashset1;
				hashSet2 = global::Interpolator.Interpolators.hashset2;
			}
			if (hashSet.Contains(interpolator))
			{
				return false;
			}
			global::Interpolator.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Add(interpolator);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00066F94 File Offset: 0x00065194
		public static bool SetDisabled(global::Interpolator interpolator)
		{
			if (!global::Interpolator.Interpolators.iterating)
			{
				return ((!global::Interpolator.Interpolators.swapped) ? global::Interpolator.Interpolators.hashset1 : global::Interpolator.Interpolators.hashset2).Remove(interpolator);
			}
			if (global::Interpolator.Interpolators.caughtIterating)
			{
				return ((!global::Interpolator.Interpolators.swapped) ? global::Interpolator.Interpolators.hashset2 : global::Interpolator.Interpolators.hashset1).Remove(interpolator);
			}
			HashSet<global::Interpolator> hashSet;
			HashSet<global::Interpolator> hashSet2;
			if (global::Interpolator.Interpolators.swapped)
			{
				hashSet = global::Interpolator.Interpolators.hashset2;
				hashSet2 = global::Interpolator.Interpolators.hashset1;
			}
			else
			{
				hashSet = global::Interpolator.Interpolators.hashset1;
				hashSet2 = global::Interpolator.Interpolators.hashset2;
			}
			if (!hashSet.Contains(interpolator))
			{
				return false;
			}
			global::Interpolator.Interpolators.caughtIterating = true;
			hashSet2.UnionWith(hashSet);
			return hashSet2.Remove(interpolator);
		}

		// Token: 0x04000E88 RID: 3720
		private static readonly HashSet<global::Interpolator> hashset1 = new HashSet<global::Interpolator>();

		// Token: 0x04000E89 RID: 3721
		private static readonly HashSet<global::Interpolator> hashset2 = new HashSet<global::Interpolator>();

		// Token: 0x04000E8A RID: 3722
		private static bool swapped;

		// Token: 0x04000E8B RID: 3723
		private static bool iterating;

		// Token: 0x04000E8C RID: 3724
		private static bool caughtIterating;
	}
}
