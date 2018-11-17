using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
public sealed class HeadBob : MonoBehaviour, ICameraFX
{
	// Token: 0x06002A64 RID: 10852 RVA: 0x000A6820 File Offset: 0x000A4A20
	void ICameraFX.PreCull()
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

	// Token: 0x06002A65 RID: 10853 RVA: 0x000A6988 File Offset: 0x000A4B88
	void ICameraFX.PostRender()
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

	// Token: 0x06002A66 RID: 10854 RVA: 0x000A6A54 File Offset: 0x000A4C54
	void ICameraFX.OnViewModelChange(ViewModel viewModel)
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

	// Token: 0x1700096E RID: 2414
	// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000A6B04 File Offset: 0x000A4D04
	public float globalScalar
	{
		get
		{
			return this._globalScalar;
		}
	}

	// Token: 0x1700096F RID: 2415
	// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000A6B0C File Offset: 0x000A4D0C
	public float globalPositionScalar
	{
		get
		{
			return this._globalPositionScalar;
		}
	}

	// Token: 0x17000970 RID: 2416
	// (get) Token: 0x06002A69 RID: 10857 RVA: 0x000A6B14 File Offset: 0x000A4D14
	public float globalRotationScalar
	{
		get
		{
			return this._globalRotationScalar;
		}
	}

	// Token: 0x17000971 RID: 2417
	// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000A6B1C File Offset: 0x000A4D1C
	public double positionScalar
	{
		get
		{
			return HeadBob.bob_scale * HeadBob.bob_scale_linear * (double)this._globalScalar * (double)this._globalPositionScalar * (double)this._viewModelPositionScalar * (double)this._aimPositionScalar;
		}
	}

	// Token: 0x17000972 RID: 2418
	// (get) Token: 0x06002A6B RID: 10859 RVA: 0x000A6B4C File Offset: 0x000A4D4C
	public double rotationScalar
	{
		get
		{
			return HeadBob.bob_scale * HeadBob.bob_scale_angular * (double)this._globalScalar * (double)this._globalRotationScalar * (double)this._viewModelRotationScalar * (double)this._aimRotationScalar;
		}
	}

	// Token: 0x17000973 RID: 2419
	// (get) Token: 0x06002A6C RID: 10860 RVA: 0x000A6B7C File Offset: 0x000A4D7C
	// (set) Token: 0x06002A6D RID: 10861 RVA: 0x000A6B84 File Offset: 0x000A4D84
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

	// Token: 0x17000974 RID: 2420
	// (get) Token: 0x06002A6E RID: 10862 RVA: 0x000A6B90 File Offset: 0x000A4D90
	// (set) Token: 0x06002A6F RID: 10863 RVA: 0x000A6B98 File Offset: 0x000A4D98
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

	// Token: 0x17000975 RID: 2421
	// (get) Token: 0x06002A70 RID: 10864 RVA: 0x000A6BA4 File Offset: 0x000A4DA4
	// (set) Token: 0x06002A71 RID: 10865 RVA: 0x000A6BAC File Offset: 0x000A4DAC
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

	// Token: 0x17000976 RID: 2422
	// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000A6BB8 File Offset: 0x000A4DB8
	// (set) Token: 0x06002A73 RID: 10867 RVA: 0x000A6BC0 File Offset: 0x000A4DC0
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

	// Token: 0x17000977 RID: 2423
	// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000A6BCC File Offset: 0x000A4DCC
	// (set) Token: 0x06002A75 RID: 10869 RVA: 0x000A6BE4 File Offset: 0x000A4DE4
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

	// Token: 0x06002A76 RID: 10870 RVA: 0x000A6C04 File Offset: 0x000A4E04
	private void OnEnable()
	{
		if (this.allowOnEnable)
		{
			this._allow = true;
		}
		this._wasForbidden = false;
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x000A6C20 File Offset: 0x000A4E20
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

	// Token: 0x06002A78 RID: 10872 RVA: 0x000A6C9C File Offset: 0x000A4E9C
	private void OnDestroy()
	{
		this.forceForbidOnDisable = false;
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x000A6CA8 File Offset: 0x000A4EA8
	private void Awake()
	{
		this.awake = true;
		this.working.stack = new BobEffectStack();
		this.predicted.stack = this.working.stack.Fork();
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x000A6CE8 File Offset: 0x000A4EE8
	private void OnLocallyAppended(IDMain main)
	{
		if (!this._motor)
		{
			this._motor = main.GetRemote<CCMotor>();
		}
	}

	// Token: 0x17000978 RID: 2424
	// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000A6D08 File Offset: 0x000A4F08
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

	// Token: 0x17000979 RID: 2425
	// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000A6D64 File Offset: 0x000A4F64
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

	// Token: 0x06002A7D RID: 10877 RVA: 0x000A6DC0 File Offset: 0x000A4FC0
	private void Solve(ref HeadBob.Weight weight, ref double dt)
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
				BobForceCurve bobForceCurve = this.cfg.additionalCurves[i];
				double num;
				switch (bobForceCurve.source)
				{
				case BobForceCurveSource.LocalMovementMagnitude:
					num = this.groundLocalVelocityMag;
					break;
				case BobForceCurveSource.LocalMovementX:
					num = this.groundLocalVelocity.x;
					break;
				case BobForceCurveSource.LocalMovementY:
					num = this.groundLocalVelocity.y;
					break;
				case BobForceCurveSource.LocalMovementZ:
					num = this.groundLocalVelocity.z;
					break;
				case BobForceCurveSource.WorldMovementMagnitude:
					num = this.groundWorldVelocityMag;
					break;
				case BobForceCurveSource.WorldMovementX:
					num = this.groundWorldVelocity.x;
					break;
				case BobForceCurveSource.WorldMovementY:
					num = this.groundWorldVelocity.y;
					break;
				case BobForceCurveSource.WorldMovementZ:
					num = this.groundWorldVelocity.z;
					break;
				case BobForceCurveSource.LocalVelocityMagnitude:
					num = this.localVelocityMag;
					break;
				case BobForceCurveSource.LocalVelocityX:
					num = this.localVelocity.x;
					break;
				case BobForceCurveSource.LocalVelocityY:
					num = this.localVelocity.y;
					break;
				case BobForceCurveSource.LocalVelocityZ:
					goto IL_204;
				case BobForceCurveSource.WorldVelocityMagnitude:
					num = this.worldVelocityMag;
					break;
				case BobForceCurveSource.WorldVelocityX:
					num = this.worldVelocity.x;
					break;
				case BobForceCurveSource.WorldVelocityY:
					num = this.worldVelocity.y;
					break;
				case BobForceCurveSource.WorldVelocityZ:
					num = this.worldVelocity.z;
					break;
				case BobForceCurveSource.RotationMagnitude:
					num = this.localAngularVelocityMag;
					break;
				case BobForceCurveSource.RotationPitch:
					num = this.localAngularVelocity.x;
					break;
				case BobForceCurveSource.RotationYaw:
					num = this.localAngularVelocity.y;
					break;
				case BobForceCurveSource.RotationRoll:
					num = this.localAngularVelocity.z;
					break;
				case BobForceCurveSource.TurnMagnitude:
					num = this.groundLocalAngularVelocityMag;
					break;
				case BobForceCurveSource.TurnPitch:
					num = this.groundLocalAngularVelocity.x;
					break;
				case BobForceCurveSource.TurnYaw:
					num = this.groundLocalAngularVelocity.y;
					break;
				case BobForceCurveSource.TurnRoll:
					num = this.groundLocalAngularVelocity.z;
					break;
				default:
					goto IL_204;
				}
				IL_2DF:
				BobForceCurveTarget target = bobForceCurve.target;
				if (target == BobForceCurveTarget.Position || target != BobForceCurveTarget.Rotation)
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

	// Token: 0x06002A7E RID: 10878 RVA: 0x000A7C90 File Offset: 0x000A5E90
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

	// Token: 0x06002A7F RID: 10879 RVA: 0x000A7FEC File Offset: 0x000A61EC
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

	// Token: 0x06002A80 RID: 10880 RVA: 0x000A8204 File Offset: 0x000A6404
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

	// Token: 0x06002A81 RID: 10881 RVA: 0x000A838C File Offset: 0x000A658C
	private static void DrawForceAxes(Vector3 force, Vector3 radii, Vector3 k, float boxDim)
	{
		Color color = Gizmos.color;
		Gizmos.color = color * Color.red;
		HeadBob.DrawForceLine(Vector3.right, force, radii, k, boxDim);
		Gizmos.color = color * Color.green;
		HeadBob.DrawForceLine(Vector3.up, force, radii, k, boxDim);
		Gizmos.color = color * Color.blue;
		HeadBob.DrawForceLine(Vector3.forward, force, radii, k, boxDim);
		Gizmos.color = color;
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x000A8400 File Offset: 0x000A6600
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = ((!base.transform.parent) ? base.transform : base.transform.parent).localToWorldMatrix;
		Gizmos.DrawLine(Vector3.zero, this.offset);
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix *= Matrix4x4.Scale(this.cfg.elipsoidRadii);
		Gizmos.DrawWireSphere(Vector3.zero, 1f);
		Gizmos.matrix = matrix;
		Gizmos.color = new Color(1f, 1f, 1f, 0.8f);
		HeadBob.DrawForceAxes(this.working.position.acceleration.f, this.cfg.elipsoidRadii, this.cfg.springConstant, 0.2f);
		Gizmos.color = Color.white;
		HeadBob.DrawForceAxes(this.working.position.acceleration.f, this.cfg.elipsoidRadii, this.cfg.maxVelocity, 0.1f);
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x000A8520 File Offset: 0x000A6720
	private void PushPosition()
	{
		this.worldToLocal.f = this.otherParent.worldToLocalMatrix;
		this.localToWorld.f = this.otherParent.localToWorldMatrix;
		HeadBob.VectorStamp vectorStamp;
		vectorStamp.timeStamp = Time.time;
		vectorStamp.valid = true;
		Character character;
		Vector3 eulerAngles;
		Vector3 vector;
		if (this._motor && (character = (this._motor.idMain as Character)))
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
		HeadBob.VectorStamp vectorStamp2;
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

	// Token: 0x06002A84 RID: 10884 RVA: 0x000A8978 File Offset: 0x000A6B78
	private void GatherInfo(CCMotor motor)
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

	// Token: 0x06002A85 RID: 10885 RVA: 0x000A8B18 File Offset: 0x000A6D18
	private bool CheckChanges(bool hasMotor, Transform parent)
	{
		if (this.hadMotor != hasMotor || this.otherParent != parent)
		{
			this.hadMotor = hasMotor;
			this.groundLocalVelocity = default(Vector3G);
			this.groundWorldVelocity = default(Vector3G);
			this.localVelocity = default(Vector3G);
			this.worldVelocity = default(Vector3G);
			this.impulseForce = default(HeadBob.VectorAccelSampler);
			this.impulseTorque = default(HeadBob.VectorAccelSampler);
			this.lastPosition = default(HeadBob.VectorStamp);
			this.otherParent = parent;
			this.raw_pos = default(Vector3G);
			this.raw_rot = default(Vector3G);
			BobEffectStack stack = this.predicted.stack;
			this.predicted = default(HeadBob.Weight);
			this.predicted.stack = stack;
			stack = this.working.stack;
			this.working = default(HeadBob.Weight);
			this.working.stack = stack;
			return true;
		}
		return false;
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x000A8C30 File Offset: 0x000A6E30
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

	// Token: 0x06002A87 RID: 10887 RVA: 0x000A8EBC File Offset: 0x000A70BC
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

	// Token: 0x06002A88 RID: 10888 RVA: 0x000A8F44 File Offset: 0x000A7144
	public bool AddEffect(BobEffect effect)
	{
		return this.working.stack.CreateInstance(effect);
	}

	// Token: 0x0400167F RID: 5759
	public BobConfiguration cfg;

	// Token: 0x04001680 RID: 5760
	[SerializeField]
	private CCMotor _motor;

	// Token: 0x04001681 RID: 5761
	[SerializeField]
	private CameraMount _mount;

	// Token: 0x04001682 RID: 5762
	[SerializeField]
	private float _globalScalar = 1f;

	// Token: 0x04001683 RID: 5763
	[SerializeField]
	private float _globalPositionScalar = 1f;

	// Token: 0x04001684 RID: 5764
	[SerializeField]
	private float _globalRotationScalar = 1f;

	// Token: 0x04001685 RID: 5765
	private static double bob_scale = 1.0;

	// Token: 0x04001686 RID: 5766
	private static double bob_scale_linear = 1.0;

	// Token: 0x04001687 RID: 5767
	private static double bob_scale_angular = 1.0;

	// Token: 0x04001688 RID: 5768
	private float _viewModelPositionScalar = 1f;

	// Token: 0x04001689 RID: 5769
	private float _viewModelRotationScalar = 1f;

	// Token: 0x0400168A RID: 5770
	private float _aimPositionScalar = 1f;

	// Token: 0x0400168B RID: 5771
	private float _aimRotationScalar = 1f;

	// Token: 0x0400168C RID: 5772
	public bool simStep = true;

	// Token: 0x0400168D RID: 5773
	public bool allowOnEnable = true;

	// Token: 0x0400168E RID: 5774
	public bool forceForbidOnDisable;

	// Token: 0x0400168F RID: 5775
	public bool allowAntiOutputs;

	// Token: 0x04001690 RID: 5776
	private Transform otherParent;

	// Token: 0x04001691 RID: 5777
	private ViewModel viewModel;

	// Token: 0x04001692 RID: 5778
	private Matrix4x4G worldToLocal;

	// Token: 0x04001693 RID: 5779
	private Matrix4x4G localToWorld;

	// Token: 0x04001694 RID: 5780
	private Vector3G localVelocity;

	// Token: 0x04001695 RID: 5781
	private Vector3G worldVelocity;

	// Token: 0x04001696 RID: 5782
	private Vector3G groundLocalVelocity;

	// Token: 0x04001697 RID: 5783
	private Vector3G groundWorldVelocity;

	// Token: 0x04001698 RID: 5784
	private Vector3G localAngularVelocity;

	// Token: 0x04001699 RID: 5785
	private Vector3G groundLocalAngularVelocity;

	// Token: 0x0400169A RID: 5786
	private double localVelocityMag;

	// Token: 0x0400169B RID: 5787
	private double worldVelocityMag;

	// Token: 0x0400169C RID: 5788
	private double groundLocalVelocityMag;

	// Token: 0x0400169D RID: 5789
	private double groundWorldVelocityMag;

	// Token: 0x0400169E RID: 5790
	private double localAngularVelocityMag;

	// Token: 0x0400169F RID: 5791
	private double groundLocalAngularVelocityMag;

	// Token: 0x040016A0 RID: 5792
	private Vector3G inputForce;

	// Token: 0x040016A1 RID: 5793
	private Vector3G raw_pos;

	// Token: 0x040016A2 RID: 5794
	private Vector3G raw_rot;

	// Token: 0x040016A3 RID: 5795
	private double timeSolve;

	// Token: 0x040016A4 RID: 5796
	private double timeIntermit;

	// Token: 0x040016A5 RID: 5797
	private int additionalCurveCount;

	// Token: 0x040016A6 RID: 5798
	private HeadBob.Weight working;

	// Token: 0x040016A7 RID: 5799
	private HeadBob.Weight predicted;

	// Token: 0x040016A8 RID: 5800
	private HeadBob.Weight intermitStart;

	// Token: 0x040016A9 RID: 5801
	private HeadBob.Weight intermitNext;

	// Token: 0x040016AA RID: 5802
	private double intermitFraction;

	// Token: 0x040016AB RID: 5803
	private HeadBob.VectorAccelSampler impulseForce;

	// Token: 0x040016AC RID: 5804
	private HeadBob.VectorAccelSampler impulseTorque;

	// Token: 0x040016AD RID: 5805
	private HeadBob.VectorStamp lastPosition;

	// Token: 0x040016AE RID: 5806
	private HeadBob.VectorStamp lastRotation;

	// Token: 0x040016AF RID: 5807
	private float allowFractionNormalized;

	// Token: 0x040016B0 RID: 5808
	private float allowValue;

	// Token: 0x040016B1 RID: 5809
	private Vector3 preCullLP;

	// Token: 0x040016B2 RID: 5810
	private Vector3 preCullLR;

	// Token: 0x040016B3 RID: 5811
	private bool anyAdditionalCurves;

	// Token: 0x040016B4 RID: 5812
	private bool _allow;

	// Token: 0x040016B5 RID: 5813
	private bool awake;

	// Token: 0x040016B6 RID: 5814
	private bool added;

	// Token: 0x040016B7 RID: 5815
	private bool hadMotor;

	// Token: 0x040016B8 RID: 5816
	private bool _wasForbidden;

	// Token: 0x020004C3 RID: 1219
	private struct Weight
	{
		// Token: 0x06002A89 RID: 10889 RVA: 0x000A8F58 File Offset: 0x000A7158
		public void CopyTo(ref HeadBob.Weight other)
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

		// Token: 0x040016B9 RID: 5817
		public HeadBob.Weight.Element position;

		// Token: 0x040016BA RID: 5818
		public HeadBob.Weight.Element rotation;

		// Token: 0x040016BB RID: 5819
		public Vector3G[] additionalPositions;

		// Token: 0x040016BC RID: 5820
		public BobEffectStack stack;

		// Token: 0x020004C4 RID: 1220
		public struct Element
		{
			// Token: 0x040016BD RID: 5821
			public Vector3G value;

			// Token: 0x040016BE RID: 5822
			public Vector3G velocity;

			// Token: 0x040016BF RID: 5823
			public Vector3G acceleration;

			// Token: 0x040016C0 RID: 5824
			public Vector3G fI;

			// Token: 0x040016C1 RID: 5825
			public Vector3G fE;

			// Token: 0x040016C2 RID: 5826
			public Vector3G fIV;
		}
	}

	// Token: 0x020004C5 RID: 1221
	private struct VectorStamp
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x000A8FE0 File Offset: 0x000A71E0
		public double AddDifference(ref HeadBob.VectorStamp previous, ref Vector3G difference)
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

		// Token: 0x040016C3 RID: 5827
		public Vector3G vector;

		// Token: 0x040016C4 RID: 5828
		public float timeStamp;

		// Token: 0x040016C5 RID: 5829
		public bool valid;
	}

	// Token: 0x020004C6 RID: 1222
	private struct VectorAccelSampler
	{
		// Token: 0x06002A8B RID: 10891 RVA: 0x000A90A8 File Offset: 0x000A72A8
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

		// Token: 0x040016C6 RID: 5830
		public HeadBob.VectorStamp sample0;

		// Token: 0x040016C7 RID: 5831
		public HeadBob.VectorStamp sample1;

		// Token: 0x040016C8 RID: 5832
		public HeadBob.VectorStamp sample2;

		// Token: 0x040016C9 RID: 5833
		public Vector3G accel;
	}
}
