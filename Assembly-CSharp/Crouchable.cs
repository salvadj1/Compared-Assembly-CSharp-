using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class Crouchable : IDLocalCharacter
{
	// Token: 0x17000269 RID: 617
	// (get) Token: 0x0600093D RID: 2365 RVA: 0x00027240 File Offset: 0x00025440
	protected CharacterCrouchTrait crouchTrait
	{
		get
		{
			if (!this.didCrouchTraitTest)
			{
				this._crouchTrait = base.GetTrait<CharacterCrouchTrait>();
				this.didCrouchTraitTest = true;
			}
			return this._crouchTrait;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x0600093E RID: 2366 RVA: 0x00027274 File Offset: 0x00025474
	protected AnimationCurve crouchCurve
	{
		get
		{
			return this.crouchTrait.crouchCurve;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x0600093F RID: 2367 RVA: 0x00027284 File Offset: 0x00025484
	protected float crouchToSpeedFraction
	{
		get
		{
			return this.crouchTrait.crouchToSpeedFraction;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000940 RID: 2368 RVA: 0x00027294 File Offset: 0x00025494
	public new Crouchable crouchable
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000941 RID: 2369 RVA: 0x00027298 File Offset: 0x00025498
	public new bool crouched
	{
		get
		{
			return this.crouchUnits < 0f;
		}
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x000272A8 File Offset: 0x000254A8
	public void ApplyCrouchOffset(ref CCTotem.PositionPlacement placement)
	{
		float num = placement.bottom.y + base.initialEyesOffsetY;
		float num2 = placement.originalTop.y - num;
		float num3 = placement.top.y - num2;
		float num4 = num3 - num;
		this.crouchUnits = ((!Mathf.Approximately(num4, 0f)) ? num4 : 0f);
		base.idMain.InvalidateEyesOffset();
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00027314 File Offset: 0x00025514
	public void LocalPlayerUpdateCrouchState(ref Crouchable.CrouchState incoming, ref bool crouchFlag, ref bool crouchBlockFlag, ref Crouchable.Smoothing smoothing)
	{
		double num = (double)base.initialEyesOffsetY;
		double num2 = (double)incoming.BottomY + num;
		double num3 = (double)(incoming.BottomY + incoming.InitialStandingHeight);
		double num4 = num3 - num2;
		double num5 = (double)incoming.TopY - num4;
		double num6 = num5 - (double)incoming.BottomY;
		double target = num6 - num;
		this.crouchUnits = smoothing.CatchUp(target);
		base.idMain.InvalidateEyesOffset();
		if (incoming.CrouchBlocked)
		{
			crouchBlockFlag = true;
			crouchFlag = true;
		}
		else
		{
			crouchBlockFlag = false;
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00027398 File Offset: 0x00025598
	public void LocalPlayerUpdateCrouchState(CCMotor ccmotor, ref bool crouchFlag, ref bool crouchBlockFlag, ref Crouchable.Smoothing smoothing)
	{
		Crouchable.CrouchState crouchState;
		crouchState.CrouchBlocked = ccmotor.isCrouchBlocked;
		CCTotem.PositionPlacement? lastPositionPlacement = ccmotor.LastPositionPlacement;
		CCTotem.PositionPlacement positionPlacement = (lastPositionPlacement == null) ? new CCTotem.PositionPlacement(base.origin, base.origin, base.origin, ccmotor.ccTotemPole.MaximumHeight) : lastPositionPlacement.Value;
		crouchState.BottomY = positionPlacement.bottom.y;
		crouchState.TopY = positionPlacement.top.y;
		crouchState.InitialStandingHeight = positionPlacement.originalHeight;
		this.LocalPlayerUpdateCrouchState(ref crouchState, ref crouchFlag, ref crouchBlockFlag, ref smoothing);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00027434 File Offset: 0x00025634
	protected internal void ApplyCrouch(ref Vector3 localPosition)
	{
		localPosition.y += this.crouchUnits;
	}

	// Token: 0x04000668 RID: 1640
	private const double kSmoothInterval = 0.0032239760652016921;

	// Token: 0x04000669 RID: 1641
	private const double kSmoothDamp = 0.5;

	// Token: 0x0400066A RID: 1642
	private const double kSmoothDampInput = 0.0;

	// Token: 0x0400066B RID: 1643
	private const double kSmoothStiffness = 5.0;

	// Token: 0x0400066C RID: 1644
	[NonSerialized]
	private CharacterCrouchTrait _crouchTrait;

	// Token: 0x0400066D RID: 1645
	[NonSerialized]
	private bool didCrouchTraitTest;

	// Token: 0x0400066E RID: 1646
	[NonSerialized]
	private float crouchUnits;

	// Token: 0x0400066F RID: 1647
	[NonSerialized]
	private float crouchTime;

	// Token: 0x02000143 RID: 323
	public struct CrouchState
	{
		// Token: 0x04000670 RID: 1648
		public bool CrouchBlocked;

		// Token: 0x04000671 RID: 1649
		public float BottomY;

		// Token: 0x04000672 RID: 1650
		public float TopY;

		// Token: 0x04000673 RID: 1651
		public float InitialStandingHeight;
	}

	// Token: 0x02000144 RID: 324
	public struct Smoothing
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x0002744C File Offset: 0x0002564C
		public void Reset()
		{
			this = default(Crouchable.Smoothing);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00027468 File Offset: 0x00025668
		public void Solve()
		{
			this.A = this.T;
			this.V = (this.Z = (this.Y = 0.0));
			this.I = true;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000274AC File Offset: 0x000256AC
		public void AddSeconds(double elapsedSeconds)
		{
			if (elapsedSeconds > 0.0)
			{
				this.Z += elapsedSeconds;
				this.Y += elapsedSeconds;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000274DC File Offset: 0x000256DC
		public float CatchUp(double target)
		{
			double num;
			if (!this.I)
			{
				this.Y = (this.V = (this.Z = 0.0));
				this.I = true;
				this.A = target;
				this.T = target;
				num = target;
			}
			else
			{
				if (this.Z > 0.0)
				{
					this.V += (target - this.T) / this.Z * 0.0;
					this.Z = 0.0;
				}
				double num2 = 0.0032239760652016921;
				double num3 = this.Y;
				if (num3 >= num2)
				{
					double a = this.A;
					this.T = target;
					double num4 = a - target;
					double num5 = this.V;
					double num6 = 0.5;
					double num7 = 5.0;
					do
					{
						double num8 = num4;
						num4 += num5 * num2;
						double num9 = -num4 * num7 - num6 * num5;
						num4 += num9 * num2;
						num5 = (num4 - num8) / num2;
					}
					while ((num3 -= num2) >= num2);
					this.A = target + num4;
					this.V = num5;
					this.Y = num3;
				}
				num = ((num3 >= 1.4012984643248171E-45) ? (this.A + this.V * num3) : this.A);
			}
			return (num >= 1.4012984643248171E-45 || num <= -1.4012984643248171E-45) ? ((float)num) : 0f;
		}

		// Token: 0x04000674 RID: 1652
		private bool I;

		// Token: 0x04000675 RID: 1653
		private double T;

		// Token: 0x04000676 RID: 1654
		private double A;

		// Token: 0x04000677 RID: 1655
		private double V;

		// Token: 0x04000678 RID: 1656
		private double Y;

		// Token: 0x04000679 RID: 1657
		private double Z;
	}
}
