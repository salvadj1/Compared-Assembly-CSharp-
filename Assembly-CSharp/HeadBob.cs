using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x0200057D RID: 1405
public sealed class HeadBob : MonoBehaviour, global::ICameraFX
{
	// Token: 0x06002E16 RID: 11798 RVA: 0x000AE5B8 File Offset: 0x000AC7B8
	void global::ICameraFX.PreCull()
	{
		int num = (this.cfg.antiOutputs != null && this.allowAntiOutputs) ? this.cfg.antiOutputs.Length : 0;
		bool flag = this.viewModel;
		Transform transform = (!flag) ? null : this.viewModel.transform;
		if (flag && this.added)
		{
			for (int i = num - 1; i >= 0; i--)
			{
				this.cfg.antiOutputs[i].Subtract(transform);
			}
		}
		this.Advance(Time.deltaTime);
		this.preCullLP = this.offset;
		this.preCullLR = this.rotationOffset;
		base.transform.localPosition += this.preCullLP;
		base.transform.localEulerAngles += this.preCullLR;
		num = ((this.cfg.antiOutputs != null && this.allowAntiOutputs) ? this.cfg.antiOutputs.Length : 0);
		if (flag)
		{
			this.added = true;
			for (int j = num - 1; j >= 0; j--)
			{
				this.cfg.antiOutputs[j].Add(transform, ref this.preCullLP, ref this.preCullLR);
			}
		}
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x000AE720 File Offset: 0x000AC920
	void global::ICameraFX.PostRender()
	{
		base.transform.localPosition -= this.preCullLP;
		base.transform.localEulerAngles -= this.preCullLR;
		if (this.added)
		{
			bool flag = this.viewModel;
			int num = (this.cfg.antiOutputs != null && this.allowAntiOutputs) ? this.cfg.antiOutputs.Length : 0;
			if (flag)
			{
				Transform transform = this.viewModel.transform;
				for (int i = num - 1; i >= 0; i--)
				{
					this.cfg.antiOutputs[i].Subtract(transform);
				}
			}
			this.added = false;
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x000AE7EC File Offset: 0x000AC9EC
	void global::ICameraFX.OnViewModelChange(global::ViewModel viewModel)
	{
		if (this.viewModel != viewModel)
		{
			this._viewModelPositionScalar = 1f;
			this._viewModelRotationScalar = 1f;
			Transform transform;
			if (this.viewModel)
			{
				transform = this.viewModel.transform;
				this.viewModel.headBob = null;
			}
			else
			{
				transform = base.transform;
			}
			Transform transform2;
			if (viewModel)
			{
				viewModel.headBob = this;
				transform2 = viewModel.transform;
			}
			else
			{
				transform2 = base.transform;
			}
			this.viewModel = viewModel;
			if (!transform)
			{
			}
			if (!transform2)
			{
			}
		}
	}

	// Token: 0x170009DE RID: 2526
	// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000AE89C File Offset: 0x000ACA9C
	public float globalScalar
	{
		get
		{
			return this._globalScalar;
		}
	}

	// Token: 0x170009DF RID: 2527
	// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000AE8A4 File Offset: 0x000ACAA4
	public float globalPositionScalar
	{
		get
		{
			return this._globalPositionScalar;
		}
	}

	// Token: 0x170009E0 RID: 2528
	// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000AE8AC File Offset: 0x000ACAAC
	public float globalRotationScalar
	{
		get
		{
			return this._globalRotationScalar;
		}
	}

	// Token: 0x170009E1 RID: 2529
	// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000AE8B4 File Offset: 0x000ACAB4
	public double positionScalar
	{
		get
		{
			return global::HeadBob.bob_scale * global::HeadBob.bob_scale_linear * (double)this._globalScalar * (double)this._globalPositionScalar * (double)this._viewModelPositionScalar * (double)this._aimPositionScalar;
		}
	}

	// Token: 0x170009E2 RID: 2530
	// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000AE8E4 File Offset: 0x000ACAE4
	public double rotationScalar
	{
		get
		{
			return global::HeadBob.bob_scale * global::HeadBob.bob_scale_angular * (double)this._globalScalar * (double)this._globalRotationScalar * (double)this._viewModelRotationScalar * (double)this._aimRotationScalar;
		}
	}

	// Token: 0x170009E3 RID: 2531
	// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000AE914 File Offset: 0x000ACB14
	// (set) Token: 0x06002E1F RID: 11807 RVA: 0x000AE91C File Offset: 0x000ACB1C
	public float viewModelPositionScalar
	{
		get
		{
			return this._viewModelPositionScalar;
		}
		set
		{
			this._viewModelPositionScalar = value;
		}
	}

	// Token: 0x170009E4 RID: 2532
	// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000AE928 File Offset: 0x000ACB28
	// (set) Token: 0x06002E21 RID: 11809 RVA: 0x000AE930 File Offset: 0x000ACB30
	public float viewModelRotationScalar
	{
		get
		{
			return this._viewModelRotationScalar;
		}
		set
		{
			this._viewModelRotationScalar = value;
		}
	}

	// Token: 0x170009E5 RID: 2533
	// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000AE93C File Offset: 0x000ACB3C
	// (set) Token: 0x06002E23 RID: 11811 RVA: 0x000AE944 File Offset: 0x000ACB44
	public float aimPositionScalar
	{
		get
		{
			return this._aimPositionScalar;
		}
		set
		{
			this._aimPositionScalar = value;
		}
	}

	// Token: 0x170009E6 RID: 2534
	// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000AE950 File Offset: 0x000ACB50
	// (set) Token: 0x06002E25 RID: 11813 RVA: 0x000AE958 File Offset: 0x000ACB58
	public float aimRotationScalar
	{
		get
		{
			return this._aimRotationScalar;
		}
		set
		{
			this._aimRotationScalar = value;
		}
	}

	// Token: 0x170009E7 RID: 2535
	// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000AE964 File Offset: 0x000ACB64
	// (set) Token: 0x06002E27 RID: 11815 RVA: 0x000AE97C File Offset: 0x000ACB7C
	public bool allow
	{
		get
		{
			return this._allow && base.enabled;
		}
		set
		{
			this._allow = value;
			if (value)
			{
				this._wasForbidden = false;
				base.enabled = true;
			}
		}
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x000AE99C File Offset: 0x000ACB9C
	private void OnEnable()
	{
		if (this.allowOnEnable)
		{
			this._allow = true;
		}
		this._wasForbidden = false;
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x000AE9B8 File Offset: 0x000ACBB8
	private void OnDisable()
	{
		if (this.awake)
		{
			if (this.forceForbidOnDisable && this._allow)
			{
				base.enabled = true;
				this._wasForbidden = base.enabled;
				if (this._wasForbidden)
				{
					this._allow = false;
					return;
				}
			}
			this.allowFractionNormalized = 0f;
			this.allowValue = 0f;
			base.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x000AEA34 File Offset: 0x000ACC34
	private void OnDestroy()
	{
		this.forceForbidOnDisable = false;
	}

	// Token: 0x06002E2B RID: 11819 RVA: 0x000AEA40 File Offset: 0x000ACC40
	private void Awake()
	{
		this.awake = true;
		this.working.stack = new global::BobEffectStack();
		this.predicted.stack = this.working.stack.Fork();
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x000AEA80 File Offset: 0x000ACC80
	private void OnLocallyAppended(IDMain main)
	{
		if (!this._motor)
		{
			this._motor = main.GetRemote<global::CCMotor>();
		}
	}

	// Token: 0x170009E8 RID: 2536
	// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000AEAA0 File Offset: 0x000ACCA0
	private Vector3 offset
	{
		get
		{
			double num = (double)this.allowValue * this.positionScalar;
			Vector3 result;
			result.x = (float)(this.raw_pos.x * num);
			result.y = (float)(this.raw_pos.y * num);
			result.z = (float)(this.raw_pos.z * num);
			return result;
		}
	}

	// Token: 0x170009E9 RID: 2537
	// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000AEAFC File Offset: 0x000ACCFC
	private Vector3 rotationOffset
	{
		get
		{
			double num = (double)this.allowValue * this.rotationScalar;
			Vector3 result;
			result.x = (float)(this.raw_rot.x * num);
			result.y = (float)(this.raw_rot.y * num);
			result.z = (float)(this.raw_rot.z * num);
			return result;
		}
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x000AEB58 File Offset: 0x000ACD58
	private void Solve(ref global::HeadBob.Weight weight, ref double dt)
	{
		Vector3G vector3G;
		vector3G.x = dt * this.groundLocalVelocity.x * (double)this.cfg.forceSpeedMultiplier.x;
		vector3G.y = dt * this.groundLocalVelocity.y * (double)this.cfg.forceSpeedMultiplier.y;
		vector3G.z = dt * this.groundLocalVelocity.z * (double)this.cfg.forceSpeedMultiplier.z;
		Vector3G fE = weight.position.fE;
		Vector3G fE2 = weight.rotation.fE;
		weight.position.fE = default(Vector3G);
		weight.rotation.fE = default(Vector3G);
		if (this.anyAdditionalCurves)
		{
			int i = 0;
			while (i < this.additionalCurveCount)
			{
				global::BobForceCurve bobForceCurve = this.cfg.additionalCurves[i];
				double num;
				switch (bobForceCurve.source)
				{
				case global::BobForceCurveSource.LocalMovementMagnitude:
					num = this.groundLocalVelocityMag;
					break;
				case global::BobForceCurveSource.LocalMovementX:
					num = this.groundLocalVelocity.x;
					break;
				case global::BobForceCurveSource.LocalMovementY:
					num = this.groundLocalVelocity.y;
					break;
				case global::BobForceCurveSource.LocalMovementZ:
					num = this.groundLocalVelocity.z;
					break;
				case global::BobForceCurveSource.WorldMovementMagnitude:
					num = this.groundWorldVelocityMag;
					break;
				case global::BobForceCurveSource.WorldMovementX:
					num = this.groundWorldVelocity.x;
					break;
				case global::BobForceCurveSource.WorldMovementY:
					num = this.groundWorldVelocity.y;
					break;
				case global::BobForceCurveSource.WorldMovementZ:
					num = this.groundWorldVelocity.z;
					break;
				case global::BobForceCurveSource.LocalVelocityMagnitude:
					num = this.localVelocityMag;
					break;
				case global::BobForceCurveSource.LocalVelocityX:
					num = this.localVelocity.x;
					break;
				case global::BobForceCurveSource.LocalVelocityY:
					num = this.localVelocity.y;
					break;
				case global::BobForceCurveSource.LocalVelocityZ:
					goto IL_204;
				case global::BobForceCurveSource.WorldVelocityMagnitude:
					num = this.worldVelocityMag;
					break;
				case global::BobForceCurveSource.WorldVelocityX:
					num = this.worldVelocity.x;
					break;
				case global::BobForceCurveSource.WorldVelocityY:
					num = this.worldVelocity.y;
					break;
				case global::BobForceCurveSource.WorldVelocityZ:
					num = this.worldVelocity.z;
					break;
				case global::BobForceCurveSource.RotationMagnitude:
					num = this.localAngularVelocityMag;
					break;
				case global::BobForceCurveSource.RotationPitch:
					num = this.localAngularVelocity.x;
					break;
				case global::BobForceCurveSource.RotationYaw:
					num = this.localAngularVelocity.y;
					break;
				case global::BobForceCurveSource.RotationRoll:
					num = this.localAngularVelocity.z;
					break;
				case global::BobForceCurveSource.TurnMagnitude:
					num = this.groundLocalAngularVelocityMag;
					break;
				case global::BobForceCurveSource.TurnPitch:
					num = this.groundLocalAngularVelocity.x;
					break;
				case global::BobForceCurveSource.TurnYaw:
					num = this.groundLocalAngularVelocity.y;
					break;
				case global::BobForceCurveSource.TurnRoll:
					num = this.groundLocalAngularVelocity.z;
					break;
				default:
					goto IL_204;
				}
				IL_2DF:
				global::BobForceCurveTarget target = bobForceCurve.target;
				if (target == global::BobForceCurveTarget.Position || target != global::BobForceCurveTarget.Rotation)
				{
					bobForceCurve.Calculate(ref weight.additionalPositions[i], ref num, ref dt, ref weight.position.fE);
				}
				else
				{
					bobForceCurve.Calculate(ref weight.additionalPositions[i], ref num, ref dt, ref weight.rotation.fE);
				}
				i++;
				continue;
				IL_204:
				num = this.localVelocity.z;
				goto IL_2DF;
			}
		}
		if (this.cfg.impulseForceSmooth > 0f)
		{
			Vector3G.SmoothDamp(ref weight.position.fI, ref this.impulseForce.accel, ref weight.position.fIV, this.cfg.impulseForceSmooth, this.cfg.impulseForceMaxChangeAcceleration, ref dt);
		}
		else
		{
			weight.position.fI = this.impulseForce.accel;
		}
		if (this.cfg.angleImpulseForceSmooth > 0f)
		{
			Vector3G.SmoothDamp(ref weight.rotation.fI, ref this.impulseTorque.accel, ref weight.rotation.fIV, this.cfg.angleImpulseForceSmooth, this.cfg.angleImpulseForceMaxChangeAcceleration, ref dt);
		}
		else
		{
			weight.rotation.fI = this.impulseTorque.accel;
		}
		weight.position.fE.x = weight.position.fE.x + (this.inputForce.x + weight.position.fI.x * (double)this.cfg.impulseForceScale.x);
		weight.position.fE.y = weight.position.fE.y + (this.inputForce.y + weight.position.fI.y * (double)this.cfg.impulseForceScale.y);
		weight.position.fE.z = weight.position.fE.z + (this.inputForce.z + weight.position.fI.z * (double)this.cfg.impulseForceScale.z);
		weight.rotation.fE.x = weight.rotation.fE.x + weight.rotation.fI.x * (double)this.cfg.angularImpulseForceScale.x;
		weight.rotation.fE.y = weight.rotation.fE.y + weight.rotation.fI.y * (double)this.cfg.angularImpulseForceScale.y;
		weight.rotation.fE.z = weight.rotation.fE.z + weight.rotation.fI.z * (double)this.cfg.angularImpulseForceScale.z;
		Vector3G value = weight.position.value;
		value.x /= (double)this.cfg.elipsoidRadii.x;
		value.y /= (double)this.cfg.elipsoidRadii.y;
		value.z /= (double)this.cfg.elipsoidRadii.z;
		double num2 = value.x * value.x + value.y * value.y + value.z * value.z;
		if (num2 > 1.0)
		{
			num2 = 1.0 / Math.Sqrt(num2);
			value.x *= num2;
			value.y *= num2;
			value.z *= num2;
		}
		value.x *= (double)this.cfg.elipsoidRadii.x;
		value.y *= (double)this.cfg.elipsoidRadii.y;
		value.z *= (double)this.cfg.elipsoidRadii.z;
		weight.stack.Simulate(ref dt, ref weight.position.fE, ref weight.rotation.fE);
		weight.position.acceleration.x = weight.position.fE.x - fE.x + (value.x * (double)(-(double)this.cfg.springConstant.x) - weight.position.velocity.x * (double)this.cfg.springDampen.x) * (double)this.cfg.weightMass;
		weight.position.acceleration.y = weight.position.fE.y - fE.y + (value.y * (double)(-(double)this.cfg.springConstant.y) - weight.position.velocity.y * (double)this.cfg.springDampen.y) * (double)this.cfg.weightMass;
		weight.position.acceleration.z = weight.position.fE.z - fE.z + (value.z * (double)(-(double)this.cfg.springConstant.z) - weight.position.velocity.z * (double)this.cfg.springDampen.z) * (double)this.cfg.weightMass;
		weight.position.velocity.x = weight.position.velocity.x + weight.position.acceleration.x * dt;
		weight.position.velocity.y = weight.position.velocity.y + weight.position.acceleration.y * dt;
		weight.position.velocity.z = weight.position.velocity.z + weight.position.acceleration.z * dt;
		if (!float.IsInfinity(this.cfg.maxVelocity.x))
		{
			if (weight.position.velocity.x < (double)(-(double)this.cfg.maxVelocity.x))
			{
				weight.position.value.x = weight.position.value.x - (double)this.cfg.maxVelocity.x * dt;
			}
			else if (weight.position.velocity.x > (double)this.cfg.maxVelocity.x)
			{
				weight.position.value.x = weight.position.value.x + (double)this.cfg.maxVelocity.x * dt;
			}
			else
			{
				weight.position.value.x = weight.position.value.x + weight.position.velocity.x * dt;
			}
		}
		else
		{
			weight.position.value.x = weight.position.value.x + weight.position.velocity.x * dt;
		}
		if (!float.IsInfinity(this.cfg.maxVelocity.y))
		{
			if (weight.position.velocity.y < (double)(-(double)this.cfg.maxVelocity.y))
			{
				weight.position.value.y = weight.position.value.y - (double)this.cfg.maxVelocity.y * dt;
			}
			else if (weight.position.velocity.y > (double)this.cfg.maxVelocity.y)
			{
				weight.position.value.y = weight.position.value.y + (double)this.cfg.maxVelocity.y * dt;
			}
			else
			{
				weight.position.value.y = weight.position.value.y + weight.position.velocity.y * dt;
			}
		}
		else
		{
			weight.position.value.y = weight.position.value.y + weight.position.velocity.y * dt;
		}
		if (!float.IsInfinity(this.cfg.maxVelocity.z))
		{
			if (weight.position.velocity.z < (double)(-(double)this.cfg.maxVelocity.z))
			{
				weight.position.value.z = weight.position.value.z - (double)this.cfg.maxVelocity.z * dt;
			}
			else if (weight.position.velocity.z > (double)this.cfg.maxVelocity.z)
			{
				weight.position.value.z = weight.position.value.z + (double)this.cfg.maxVelocity.z * dt;
			}
			else
			{
				weight.position.value.z = weight.position.value.z + weight.position.velocity.z * dt;
			}
		}
		else
		{
			weight.position.value.z = weight.position.value.z + weight.position.velocity.z * dt;
		}
		weight.rotation.acceleration.x = weight.rotation.fE.x - fE2.x + (weight.rotation.value.x * (double)(-(double)this.cfg.angularSpringConstant.x) - weight.rotation.velocity.x * (double)this.cfg.angularSpringDampen.x) * (double)this.cfg.angularWeightMass;
		weight.rotation.acceleration.y = weight.rotation.fE.y - fE2.y + (weight.rotation.value.y * (double)(-(double)this.cfg.angularSpringConstant.y) - weight.rotation.velocity.y * (double)this.cfg.angularSpringDampen.y) * (double)this.cfg.angularWeightMass;
		weight.rotation.acceleration.z = weight.rotation.fE.z - fE2.z + (weight.rotation.value.z * (double)(-(double)this.cfg.angularSpringConstant.z) - weight.rotation.velocity.z * (double)this.cfg.angularSpringDampen.z) * (double)this.cfg.angularWeightMass;
		weight.rotation.velocity.x = weight.rotation.velocity.x + weight.rotation.acceleration.x * dt;
		weight.rotation.velocity.y = weight.rotation.velocity.y + weight.rotation.acceleration.y * dt;
		weight.rotation.velocity.z = weight.rotation.velocity.z + weight.rotation.acceleration.z * dt;
		weight.rotation.value.x = weight.rotation.value.x + weight.rotation.velocity.x * dt;
		weight.rotation.value.y = weight.rotation.value.y + weight.rotation.velocity.y * dt;
		weight.rotation.value.z = weight.rotation.value.z + weight.rotation.velocity.z * dt;
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x000AFA28 File Offset: 0x000ADC28
	private int Step(float dt)
	{
		int num = 0;
		int num2 = 0;
		this.timeSolve += (double)dt;
		double num3 = ((double)this.cfg.solveRate >= 0.0) ? (1.0 / (double)this.cfg.solveRate) : (1.0 / -(double)this.cfg.solveRate);
		double num4 = ((double)this.cfg.intermitRate != 0.0) ? (((double)this.cfg.intermitRate >= 0.0) ? (1.0 / (double)this.cfg.intermitRate) : (1.0 / -(double)this.cfg.intermitRate)) : 0.0;
		if (double.IsInfinity(num3) || num3 == 0.0)
		{
			num3 = this.timeSolve;
		}
		bool flag = num4 > num3;
		double num5 = num3 * (double)this.cfg.timeScale;
		if (this.timeSolve >= num3)
		{
			do
			{
				this.timeSolve -= num3;
				if (flag)
				{
					this.timeIntermit -= num3;
					if (this.timeIntermit < 0.0)
					{
						this.intermitStart = this.working;
					}
				}
				this.Solve(ref this.working, ref num5);
				if (flag && this.timeIntermit < 0.0)
				{
					this.intermitNext = this.working;
					this.intermitFraction = (this.timeIntermit + num3) / num3;
					this.timeIntermit += num4;
					num2++;
				}
				num++;
			}
			while (this.timeSolve >= num3);
		}
		if (flag)
		{
			if (num2 > 0)
			{
				if (this.simStep)
				{
					Vector3G.Lerp(ref this.intermitStart.position.value, ref this.intermitNext.position.value, ref this.intermitFraction, ref this.raw_pos);
					Vector3G.Lerp(ref this.intermitStart.rotation.value, ref this.intermitNext.rotation.value, ref this.intermitFraction, ref this.raw_rot);
					this.CheckDeadZone();
				}
				else
				{
					this.raw_pos = this.intermitNext.position.value;
					this.raw_rot = this.intermitNext.rotation.value;
					this.CheckDeadZone();
				}
			}
			return num2;
		}
		if (this.simStep)
		{
			this.working.CopyTo(ref this.predicted);
			this.Solve(ref this.predicted, ref num5);
			num = -(num + 1);
			double num6 = this.timeSolve / num3;
			Vector3G.Lerp(ref this.working.position.value, ref this.predicted.position.value, ref num6, ref this.raw_pos);
			Vector3G.Lerp(ref this.working.rotation.value, ref this.predicted.rotation.value, ref num6, ref this.raw_rot);
			this.CheckDeadZone();
		}
		else
		{
			this.raw_pos = this.working.position.value;
			this.raw_rot = this.working.rotation.value;
			this.CheckDeadZone();
		}
		return num;
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x000AFD84 File Offset: 0x000ADF84
	private void CheckDeadZone()
	{
		if (this.raw_pos.x >= (double)(-(double)this.cfg.positionDeadzone.x) && this.raw_pos.x < (double)this.cfg.positionDeadzone.x)
		{
			this.raw_pos.x = 0.0;
		}
		if (this.raw_pos.y >= (double)(-(double)this.cfg.positionDeadzone.y) && this.raw_pos.y < (double)this.cfg.positionDeadzone.y)
		{
			this.raw_pos.y = 0.0;
		}
		if (this.raw_pos.z >= (double)(-(double)this.cfg.positionDeadzone.z) && this.raw_pos.z < (double)this.cfg.positionDeadzone.z)
		{
			this.raw_pos.z = 0.0;
		}
		if (this.raw_rot.x >= (double)(-(double)this.cfg.rotationDeadzone.x) && this.raw_rot.x < (double)this.cfg.rotationDeadzone.x)
		{
			this.raw_rot.x = 0.0;
		}
		if (this.raw_rot.y >= (double)(-(double)this.cfg.rotationDeadzone.y) && this.raw_rot.y < (double)this.cfg.rotationDeadzone.y)
		{
			this.raw_rot.y = 0.0;
		}
		if (this.raw_rot.z >= (double)(-(double)this.cfg.rotationDeadzone.z) && this.raw_rot.z < (double)this.cfg.rotationDeadzone.z)
		{
			this.raw_rot.z = 0.0;
		}
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x000AFF9C File Offset: 0x000AE19C
	private static void DrawForceLine(Vector3 posdir, Vector3 force, Vector3 radii, Vector3 k, float boxDim)
	{
		Vector3 vector = Vector3.Scale(radii, posdir);
		Vector3 vector2 = vector * 2f;
		float num = Vector3.Dot(force, posdir) / (Vector3.Dot(posdir, radii) * Vector3.Dot(posdir, k));
		if (num < 0f)
		{
			num = -num;
			vector = -vector;
			vector2 = -vector2;
			posdir = -posdir;
		}
		Vector3 vector3 = vector + (vector2 - vector) * num;
		Color color = Gizmos.color;
		Gizmos.color = color * new Color(1f, 1f, 1f, 0.5f);
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(posdir), new Vector3(1f, 1f, 1f));
		float num2 = Vector3.Dot(posdir, vector);
		float num3 = Vector3.Dot(posdir, vector2) - num2;
		float num4 = Vector3.Dot(posdir, vector3) - num2;
		Gizmos.DrawWireCube(new Vector3(0f, 0f, num2 + num3 / 2f), new Vector3(boxDim, boxDim, num3));
		Gizmos.DrawWireCube(new Vector3(0f, 0f, -(num2 + num3 / 2f)), new Vector3(boxDim, boxDim, num3));
		Gizmos.color = color;
		Gizmos.DrawCube(new Vector3(0f, 0f, num2 + num4 / 2f), new Vector3(boxDim, boxDim, num4));
		Gizmos.matrix = matrix;
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x000B0124 File Offset: 0x000AE324
	private static void DrawForceAxes(Vector3 force, Vector3 radii, Vector3 k, float boxDim)
	{
		Color color = Gizmos.color;
		Gizmos.color = color * Color.red;
		global::HeadBob.DrawForceLine(Vector3.right, force, radii, k, boxDim);
		Gizmos.color = color * Color.green;
		global::HeadBob.DrawForceLine(Vector3.up, force, radii, k, boxDim);
		Gizmos.color = color * Color.blue;
		global::HeadBob.DrawForceLine(Vector3.forward, force, radii, k, boxDim);
		Gizmos.color = color;
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x000B0198 File Offset: 0x000AE398
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = ((!base.transform.parent) ? base.transform : base.transform.parent).localToWorldMatrix;
		Gizmos.DrawLine(Vector3.zero, this.offset);
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix *= Matrix4x4.Scale(this.cfg.elipsoidRadii);
		Gizmos.DrawWireSphere(Vector3.zero, 1f);
		Gizmos.matrix = matrix;
		Gizmos.color = new Color(1f, 1f, 1f, 0.8f);
		global::HeadBob.DrawForceAxes(this.working.position.acceleration.f, this.cfg.elipsoidRadii, this.cfg.springConstant, 0.2f);
		Gizmos.color = Color.white;
		global::HeadBob.DrawForceAxes(this.working.position.acceleration.f, this.cfg.elipsoidRadii, this.cfg.maxVelocity, 0.1f);
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x000B02B8 File Offset: 0x000AE4B8
	private void PushPosition()
	{
		this.worldToLocal.f = this.otherParent.worldToLocalMatrix;
		this.localToWorld.f = this.otherParent.localToWorldMatrix;
		global::HeadBob.VectorStamp vectorStamp;
		vectorStamp.timeStamp = Time.time;
		vectorStamp.valid = true;
		global::Character character;
		Vector3 eulerAngles;
		Vector3 vector;
		if (this._motor && (character = (this._motor.idMain as global::Character)))
		{
			eulerAngles = character.eyesAngles.eulerAngles;
			vector = character.eyesOrigin;
		}
		else
		{
			eulerAngles = this.otherParent.eulerAngles;
			vector = this.otherParent.position;
		}
		vectorStamp.vector.x = (double)vector.x;
		vectorStamp.vector.y = (double)vector.y;
		vectorStamp.vector.z = (double)vector.z;
		global::HeadBob.VectorStamp vectorStamp2;
		vectorStamp2.vector.x = (double)eulerAngles.x;
		vectorStamp2.vector.y = (double)eulerAngles.y;
		vectorStamp2.vector.z = (double)eulerAngles.z;
		vectorStamp2.timeStamp = Time.time;
		vectorStamp2.valid = true;
		if (this.lastPosition.valid && this.lastPosition.timeStamp != vectorStamp.timeStamp)
		{
			double num = 1.0 / (double)(vectorStamp.timeStamp - this.lastPosition.timeStamp);
			this.worldVelocity.x = (vectorStamp.vector.x - this.lastPosition.vector.x) * num;
			this.worldVelocity.y = (vectorStamp.vector.y - this.lastPosition.vector.y) * num;
			this.worldVelocity.z = (vectorStamp.vector.z - this.lastPosition.vector.z) * num;
			Matrix4x4G.Mult3x3(ref this.worldVelocity, ref this.worldToLocal, ref this.localVelocity);
		}
		this.impulseForce.Sample(ref this.localVelocity, vectorStamp.timeStamp);
		this.lastPosition = vectorStamp;
		if (this.lastRotation.valid && this.lastRotation.timeStamp != vectorStamp2.timeStamp)
		{
			double num2 = 1.0 / (double)(vectorStamp2.timeStamp - this.lastRotation.timeStamp);
			Precise.DeltaAngle(ref this.lastRotation.vector.x, ref vectorStamp2.vector.x, ref this.localAngularVelocity.x);
			Precise.DeltaAngle(ref this.lastRotation.vector.y, ref vectorStamp2.vector.y, ref this.localAngularVelocity.y);
			Precise.DeltaAngle(ref this.lastRotation.vector.z, ref vectorStamp2.vector.z, ref this.localAngularVelocity.z);
			this.localAngularVelocity.x = this.localAngularVelocity.x * num2;
			this.localAngularVelocity.y = this.localAngularVelocity.y * num2;
			this.localAngularVelocity.z = this.localAngularVelocity.z * num2;
		}
		this.impulseTorque.Sample(ref this.localAngularVelocity, vectorStamp2.timeStamp);
		this.lastRotation = vectorStamp2;
		this.localVelocityMag = Math.Sqrt(this.localVelocity.x * this.localVelocity.x + this.localVelocity.y * this.localVelocity.y + this.localVelocity.z * this.localVelocity.z);
		this.worldVelocityMag = Math.Sqrt(this.worldVelocity.x * this.worldVelocity.x + this.worldVelocity.y * this.worldVelocity.y + this.worldVelocity.z * this.worldVelocity.z);
		this.localAngularVelocityMag = Math.Sqrt(this.localAngularVelocity.x * this.localAngularVelocity.x + this.localAngularVelocity.y * this.localAngularVelocity.y + this.localAngularVelocity.z * this.localAngularVelocity.z);
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x000B0710 File Offset: 0x000AE910
	private void GatherInfo(global::CCMotor motor)
	{
		if (motor.isGrounded && !motor.isSliding)
		{
			this.groundLocalVelocity = this.localVelocity;
			this.groundWorldVelocity = this.worldVelocity;
			this.groundLocalAngularVelocity = this.localAngularVelocity;
			this.groundLocalVelocityMag = this.localVelocityMag;
			this.groundWorldVelocityMag = this.worldVelocityMag;
			this.groundLocalAngularVelocityMag = this.localAngularVelocityMag;
		}
		else
		{
			this.groundLocalVelocity = default(Vector3G);
			this.groundWorldVelocity = default(Vector3G);
			this.groundLocalAngularVelocity = default(Vector3G);
			this.groundLocalVelocityMag = 0.0;
			this.groundWorldVelocityMag = 0.0;
			this.groundLocalAngularVelocityMag = 0.0;
		}
		this.inputForce.x = (double)motor.input.moveDirection.x;
		this.inputForce.y = (double)motor.input.moveDirection.y;
		this.inputForce.z = (double)motor.input.moveDirection.z;
		Matrix4x4G.Mult3x3(ref this.inputForce, ref this.worldToLocal, ref this.inputForce);
		this.inputForce.x = this.inputForce.x * (double)this.cfg.inputForceMultiplier.x;
		this.inputForce.y = this.inputForce.y * (double)this.cfg.inputForceMultiplier.y;
		this.inputForce.z = this.inputForce.z * (double)this.cfg.inputForceMultiplier.z;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x000B08B0 File Offset: 0x000AEAB0
	private bool CheckChanges(bool hasMotor, Transform parent)
	{
		if (this.hadMotor != hasMotor || this.otherParent != parent)
		{
			this.hadMotor = hasMotor;
			this.groundLocalVelocity = default(Vector3G);
			this.groundWorldVelocity = default(Vector3G);
			this.localVelocity = default(Vector3G);
			this.worldVelocity = default(Vector3G);
			this.impulseForce = default(global::HeadBob.VectorAccelSampler);
			this.impulseTorque = default(global::HeadBob.VectorAccelSampler);
			this.lastPosition = default(global::HeadBob.VectorStamp);
			this.otherParent = parent;
			this.raw_pos = default(Vector3G);
			this.raw_rot = default(Vector3G);
			global::BobEffectStack stack = this.predicted.stack;
			this.predicted = default(global::HeadBob.Weight);
			this.predicted.stack = stack;
			stack = this.working.stack;
			this.working = default(global::HeadBob.Weight);
			this.working.stack = stack;
			return true;
		}
		return false;
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x000B09C8 File Offset: 0x000AEBC8
	private bool Advance(float dt)
	{
		bool flag;
		if (this._motor)
		{
			flag = this.CheckChanges(true, this._motor.idMain.transform);
			this.PushPosition();
			this.GatherInfo(this._motor);
		}
		else
		{
			flag = this.CheckChanges(false, base.transform.parent ?? base.transform);
			this.PushPosition();
		}
		if (this.cfg.additionalCurves != null)
		{
			this.anyAdditionalCurves = ((this.additionalCurveCount = this.cfg.additionalCurves.Length) > 0);
		}
		else
		{
			this.additionalCurveCount = 0;
			this.anyAdditionalCurves = false;
		}
		if (this.anyAdditionalCurves)
		{
			Array.Resize<Vector3G>(ref this.working.additionalPositions, this.additionalCurveCount);
			Array.Resize<Vector3G>(ref this.predicted.additionalPositions, this.additionalCurveCount);
		}
		if (this._allow)
		{
			if (this.allowFractionNormalized < 1f)
			{
				int length = this.cfg.allowCurve.length;
				if ((float)length == 0f)
				{
					this.allowFractionNormalized = 1f;
					this.allowValue = 1f;
				}
				else
				{
					this.allowFractionNormalized += dt / (float)length;
					if (this.allowFractionNormalized >= 1f)
					{
						this.allowFractionNormalized = 1f;
						this.allowValue = 1f;
					}
					else
					{
						this.allowValue = this.cfg.allowCurve.Evaluate(this.allowFractionNormalized * (float)length);
					}
				}
				flag = true;
			}
		}
		else
		{
			if (this.allowFractionNormalized > 0f)
			{
				int length2 = this.cfg.forbidCurve.length;
				if ((float)length2 == 0f)
				{
					this.allowFractionNormalized = 0f;
					this.allowValue = 0f;
				}
				else
				{
					this.allowFractionNormalized -= dt / (float)length2;
					if (this.allowFractionNormalized <= 0f)
					{
						this.allowFractionNormalized = 0f;
						this.allowValue = 0f;
					}
					else
					{
						this.allowValue = 1f - this.cfg.forbidCurve.Evaluate((1f - this.allowFractionNormalized) * (float)length2);
					}
				}
				flag = true;
			}
			if (this._wasForbidden && this.allowFractionNormalized == 0f)
			{
				base.enabled = false;
			}
		}
		return this.Step(dt) != 0 || flag;
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x000B0C54 File Offset: 0x000AEE54
	private void LateUpdate()
	{
		if (!base.camera)
		{
			if (this.Advance(Time.deltaTime))
			{
				base.transform.localPosition = this.offset;
				base.transform.localEulerAngles = this.rotationOffset;
			}
		}
		else if (!this._allow && this._mount && !this._mount.isActiveMount)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x000B0CDC File Offset: 0x000AEEDC
	public bool AddEffect(global::BobEffect effect)
	{
		return this.working.stack.CreateInstance(effect);
	}

	// Token: 0x0400183C RID: 6204
	public global::BobConfiguration cfg;

	// Token: 0x0400183D RID: 6205
	[SerializeField]
	private global::CCMotor _motor;

	// Token: 0x0400183E RID: 6206
	[SerializeField]
	private global::CameraMount _mount;

	// Token: 0x0400183F RID: 6207
	[SerializeField]
	private float _globalScalar = 1f;

	// Token: 0x04001840 RID: 6208
	[SerializeField]
	private float _globalPositionScalar = 1f;

	// Token: 0x04001841 RID: 6209
	[SerializeField]
	private float _globalRotationScalar = 1f;

	// Token: 0x04001842 RID: 6210
	private static double bob_scale = 1.0;

	// Token: 0x04001843 RID: 6211
	private static double bob_scale_linear = 1.0;

	// Token: 0x04001844 RID: 6212
	private static double bob_scale_angular = 1.0;

	// Token: 0x04001845 RID: 6213
	private float _viewModelPositionScalar = 1f;

	// Token: 0x04001846 RID: 6214
	private float _viewModelRotationScalar = 1f;

	// Token: 0x04001847 RID: 6215
	private float _aimPositionScalar = 1f;

	// Token: 0x04001848 RID: 6216
	private float _aimRotationScalar = 1f;

	// Token: 0x04001849 RID: 6217
	public bool simStep = true;

	// Token: 0x0400184A RID: 6218
	public bool allowOnEnable = true;

	// Token: 0x0400184B RID: 6219
	public bool forceForbidOnDisable;

	// Token: 0x0400184C RID: 6220
	public bool allowAntiOutputs;

	// Token: 0x0400184D RID: 6221
	private Transform otherParent;

	// Token: 0x0400184E RID: 6222
	private global::ViewModel viewModel;

	// Token: 0x0400184F RID: 6223
	private Matrix4x4G worldToLocal;

	// Token: 0x04001850 RID: 6224
	private Matrix4x4G localToWorld;

	// Token: 0x04001851 RID: 6225
	private Vector3G localVelocity;

	// Token: 0x04001852 RID: 6226
	private Vector3G worldVelocity;

	// Token: 0x04001853 RID: 6227
	private Vector3G groundLocalVelocity;

	// Token: 0x04001854 RID: 6228
	private Vector3G groundWorldVelocity;

	// Token: 0x04001855 RID: 6229
	private Vector3G localAngularVelocity;

	// Token: 0x04001856 RID: 6230
	private Vector3G groundLocalAngularVelocity;

	// Token: 0x04001857 RID: 6231
	private double localVelocityMag;

	// Token: 0x04001858 RID: 6232
	private double worldVelocityMag;

	// Token: 0x04001859 RID: 6233
	private double groundLocalVelocityMag;

	// Token: 0x0400185A RID: 6234
	private double groundWorldVelocityMag;

	// Token: 0x0400185B RID: 6235
	private double localAngularVelocityMag;

	// Token: 0x0400185C RID: 6236
	private double groundLocalAngularVelocityMag;

	// Token: 0x0400185D RID: 6237
	private Vector3G inputForce;

	// Token: 0x0400185E RID: 6238
	private Vector3G raw_pos;

	// Token: 0x0400185F RID: 6239
	private Vector3G raw_rot;

	// Token: 0x04001860 RID: 6240
	private double timeSolve;

	// Token: 0x04001861 RID: 6241
	private double timeIntermit;

	// Token: 0x04001862 RID: 6242
	private int additionalCurveCount;

	// Token: 0x04001863 RID: 6243
	private global::HeadBob.Weight working;

	// Token: 0x04001864 RID: 6244
	private global::HeadBob.Weight predicted;

	// Token: 0x04001865 RID: 6245
	private global::HeadBob.Weight intermitStart;

	// Token: 0x04001866 RID: 6246
	private global::HeadBob.Weight intermitNext;

	// Token: 0x04001867 RID: 6247
	private double intermitFraction;

	// Token: 0x04001868 RID: 6248
	private global::HeadBob.VectorAccelSampler impulseForce;

	// Token: 0x04001869 RID: 6249
	private global::HeadBob.VectorAccelSampler impulseTorque;

	// Token: 0x0400186A RID: 6250
	private global::HeadBob.VectorStamp lastPosition;

	// Token: 0x0400186B RID: 6251
	private global::HeadBob.VectorStamp lastRotation;

	// Token: 0x0400186C RID: 6252
	private float allowFractionNormalized;

	// Token: 0x0400186D RID: 6253
	private float allowValue;

	// Token: 0x0400186E RID: 6254
	private Vector3 preCullLP;

	// Token: 0x0400186F RID: 6255
	private Vector3 preCullLR;

	// Token: 0x04001870 RID: 6256
	private bool anyAdditionalCurves;

	// Token: 0x04001871 RID: 6257
	private bool _allow;

	// Token: 0x04001872 RID: 6258
	private bool awake;

	// Token: 0x04001873 RID: 6259
	private bool added;

	// Token: 0x04001874 RID: 6260
	private bool hadMotor;

	// Token: 0x04001875 RID: 6261
	private bool _wasForbidden;

	// Token: 0x0200057E RID: 1406
	private struct Weight
	{
		// Token: 0x06002E3B RID: 11835 RVA: 0x000B0CF0 File Offset: 0x000AEEF0
		public void CopyTo(ref global::HeadBob.Weight other)
		{
			if (other.additionalPositions != this.additionalPositions && this.additionalPositions != null)
			{
				Array.Copy(this.additionalPositions, other.additionalPositions, this.additionalPositions.Length);
			}
			other.rotation = this.rotation;
			other.position = this.position;
			if (other.stack != null && other.stack.IsForkOf(this.stack))
			{
				other.stack.Join();
			}
		}

		// Token: 0x04001876 RID: 6262
		public global::HeadBob.Weight.Element position;

		// Token: 0x04001877 RID: 6263
		public global::HeadBob.Weight.Element rotation;

		// Token: 0x04001878 RID: 6264
		public Vector3G[] additionalPositions;

		// Token: 0x04001879 RID: 6265
		public global::BobEffectStack stack;

		// Token: 0x0200057F RID: 1407
		public struct Element
		{
			// Token: 0x0400187A RID: 6266
			public Vector3G value;

			// Token: 0x0400187B RID: 6267
			public Vector3G velocity;

			// Token: 0x0400187C RID: 6268
			public Vector3G acceleration;

			// Token: 0x0400187D RID: 6269
			public Vector3G fI;

			// Token: 0x0400187E RID: 6270
			public Vector3G fE;

			// Token: 0x0400187F RID: 6271
			public Vector3G fIV;
		}
	}

	// Token: 0x02000580 RID: 1408
	private struct VectorStamp
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x000B0D78 File Offset: 0x000AEF78
		public double AddDifference(ref global::HeadBob.VectorStamp previous, ref Vector3G difference)
		{
			if (previous.valid && previous.timeStamp != this.timeStamp)
			{
				double num = 1.0 / (double)(this.timeStamp - previous.timeStamp);
				difference.x += num * (this.vector.x - previous.vector.x);
				difference.y += num * (this.vector.y - previous.vector.y);
				difference.z += num * (this.vector.z - previous.vector.z);
				return 1.0;
			}
			return 0.0;
		}

		// Token: 0x04001880 RID: 6272
		public Vector3G vector;

		// Token: 0x04001881 RID: 6273
		public float timeStamp;

		// Token: 0x04001882 RID: 6274
		public bool valid;
	}

	// Token: 0x02000581 RID: 1409
	private struct VectorAccelSampler
	{
		// Token: 0x06002E3D RID: 11837 RVA: 0x000B0E40 File Offset: 0x000AF040
		public void Sample(ref Vector3G v, float timeStamp)
		{
			if (this.sample1.timeStamp < timeStamp)
			{
				this.sample2 = this.sample1;
			}
			if (this.sample0.timeStamp < timeStamp)
			{
				this.sample1 = this.sample0;
			}
			this.sample0.vector = v;
			this.sample0.timeStamp = timeStamp;
			this.sample0.valid = true;
			Vector3G vector3G = default(Vector3G);
			double num = this.sample0.AddDifference(ref this.sample1, ref vector3G) + this.sample0.AddDifference(ref this.sample2, ref vector3G);
			if (num != 0.0)
			{
				num = 1.0 / num;
				this.accel.x = vector3G.x * num;
				this.accel.y = vector3G.y * num;
				this.accel.z = vector3G.z * num;
			}
		}

		// Token: 0x04001883 RID: 6275
		public global::HeadBob.VectorStamp sample0;

		// Token: 0x04001884 RID: 6276
		public global::HeadBob.VectorStamp sample1;

		// Token: 0x04001885 RID: 6277
		public global::HeadBob.VectorStamp sample2;

		// Token: 0x04001886 RID: 6278
		public Vector3G accel;
	}
}
