using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class Crouchable : global::IDLocalCharacter
{
	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002AFBC File Offset: 0x000291BC
	protected global::CharacterCrouchTrait crouchTrait
	{
		get
		{
			if (!this.didCrouchTraitTest)
			{
				this._crouchTrait = base.GetTrait<global::CharacterCrouchTrait>();
				this.didCrouchTraitTest = true;
			}
			return this._crouchTrait;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002AFF0 File Offset: 0x000291F0
	protected AnimationCurve crouchCurve
	{
		get
		{
			return this.crouchTrait.crouchCurve;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002B000 File Offset: 0x00029200
	protected float crouchToSpeedFraction
	{
		get
		{
			return this.crouchTrait.crouchToSpeedFraction;
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002B010 File Offset: 0x00029210
	public new global::Crouchable crouchable
	{
		get
		{
			return this;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002B014 File Offset: 0x00029214
	public new bool crouched
	{
		get
		{
			return this.crouchUnits < 0f;
		}
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0002B024 File Offset: 0x00029224
	public void ApplyCrouchOffset(ref global::CCTotem.PositionPlacement placement)
	{
		float num = placement.bottom.y + base.initialEyesOffsetY;
		float num2 = placement.originalTop.y - num;
		float num3 = placement.top.y - num2;
		float num4 = num3 - num;
		this.crouchUnits = ((!Mathf.Approximately(num4, 0f)) ? num4 : 0f);
		base.idMain.InvalidateEyesOffset();
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0002B090 File Offset: 0x00029290
	public void LocalPlayerUpdateCrouchState(ref global::Crouchable.CrouchState incoming, ref bool crouchFlag, ref bool crouchBlockFlag, ref global::Crouchable.Smoothing smoothing)
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

	// Token: 0x06000A6A RID: 2666 RVA: 0x0002B114 File Offset: 0x00029314
	public void LocalPlayerUpdateCrouchState(global::CCMotor ccmotor, ref bool crouchFlag, ref bool crouchBlockFlag, ref global::Crouchable.Smoothing smoothing)
	{
		global::Crouchable.CrouchState crouchState;
		crouchState.CrouchBlocked = ccmotor.isCrouchBlocked;
		global::CCTotem.PositionPlacement? lastPositionPlacement = ccmotor.LastPositionPlacement;
		global::CCTotem.PositionPlacement positionPlacement = (lastPositionPlacement == null) ? new global::CCTotem.PositionPlacement(base.origin, base.origin, base.origin, ccmotor.ccTotemPole.MaximumHeight) : lastPositionPlacement.Value;
		crouchState.BottomY = positionPlacement.bottom.y;
		crouchState.TopY = positionPlacement.top.y;
		crouchState.InitialStandingHeight = positionPlacement.originalHeight;
		this.LocalPlayerUpdateCrouchState(ref crouchState, ref crouchFlag, ref crouchBlockFlag, ref smoothing);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0002B1B0 File Offset: 0x000293B0
	protected internal void ApplyCrouch(ref Vector3 localPosition)
	{
		localPosition.y += this.crouchUnits;
	}

	// Token: 0x04000777 RID: 1911
	private const double kSmoothInterval = 0.0032239760652016921;

	// Token: 0x04000778 RID: 1912
	private const double kSmoothDamp = 0.5;

	// Token: 0x04000779 RID: 1913
	private const double kSmoothDampInput = 0.0;

	// Token: 0x0400077A RID: 1914
	private const double kSmoothStiffness = 5.0;

	// Token: 0x0400077B RID: 1915
	[NonSerialized]
	private global::CharacterCrouchTrait _crouchTrait;

	// Token: 0x0400077C RID: 1916
	[NonSerialized]
	private bool didCrouchTraitTest;

	// Token: 0x0400077D RID: 1917
	[NonSerialized]
	private float crouchUnits;

	// Token: 0x0400077E RID: 1918
	[NonSerialized]
	private float crouchTime;

	// Token: 0x0200016D RID: 365
	public struct CrouchState
	{
		// Token: 0x0400077F RID: 1919
		public bool CrouchBlocked;

		// Token: 0x04000780 RID: 1920
		public float BottomY;

		// Token: 0x04000781 RID: 1921
		public float TopY;

		// Token: 0x04000782 RID: 1922
		public float InitialStandingHeight;
	}

	// Token: 0x0200016E RID: 366
	public struct Smoothing
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B1C8 File Offset: 0x000293C8
		public void Reset()
		{
			this = default(global::Crouchable.Smoothing);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B1E4 File Offset: 0x000293E4
		public void Solve()
		{
			this.A = this.T;
			this.V = (this.Z = (this.Y = 0.0));
			this.I = true;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002B228 File Offset: 0x00029428
		public void AddSeconds(double elapsedSeconds)
		{
			if (elapsedSeconds > 0.0)
			{
				this.Z += elapsedSeconds;
				this.Y += elapsedSeconds;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002B258 File Offset: 0x00029458
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

		// Token: 0x04000783 RID: 1923
		private bool I;

		// Token: 0x04000784 RID: 1924
		private double T;

		// Token: 0x04000785 RID: 1925
		private double A;

		// Token: 0x04000786 RID: 1926
		private double V;

		// Token: 0x04000787 RID: 1927
		private double Y;

		// Token: 0x04000788 RID: 1928
		private double Z;
	}
}
