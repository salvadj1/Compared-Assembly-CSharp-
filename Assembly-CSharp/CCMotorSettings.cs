using System;
using UnityEngine;

// Token: 0x02000289 RID: 649
public sealed class CCMotorSettings : ScriptableObject
{
	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005AC38 File Offset: 0x00058E38
	// (set) Token: 0x06001774 RID: 6004 RVA: 0x0005AD10 File Offset: 0x00058F10
	public CCMotor.Movement movement
	{
		get
		{
			CCMotor.Movement result;
			result.maxForwardSpeed = this.maxForwardSpeed;
			result.maxSidewaysSpeed = this.maxSidewaysSpeed;
			result.maxBackwardsSpeed = this.maxBackwardsSpeed;
			result.maxGroundAcceleration = this.maxGroundAcceleration;
			result.maxAirAcceleration = this.maxAirAcceleration;
			result.inputAirVelocityRatio = this.inputAirVelocityRatio;
			result.gravity = this.gravity;
			result.maxFallSpeed = this.maxFallSpeed;
			result.maxAirHorizontalSpeed = this.maxAirHorizontalSpeed;
			result.maxUnblockingHeightDifference = this.maxUnblockingHeightDifference;
			result.slopeSpeedMultiplier = new AnimationCurve(this.slopeSpeedMultiplier.keys);
			result.slopeSpeedMultiplier.postWrapMode = this.slopeSpeedMultiplier.postWrapMode;
			result.slopeSpeedMultiplier.preWrapMode = this.slopeSpeedMultiplier.preWrapMode;
			return result;
		}
		set
		{
			this.maxForwardSpeed = value.maxForwardSpeed;
			this.maxSidewaysSpeed = value.maxSidewaysSpeed;
			this.maxBackwardsSpeed = value.maxBackwardsSpeed;
			this.maxGroundAcceleration = value.maxGroundAcceleration;
			this.maxAirAcceleration = value.maxAirAcceleration;
			this.inputAirVelocityRatio = value.inputAirVelocityRatio;
			this.gravity = value.gravity;
			this.maxFallSpeed = value.maxFallSpeed;
			this.maxUnblockingHeightDifference = value.maxUnblockingHeightDifference;
			this.slopeSpeedMultiplier.keys = value.slopeSpeedMultiplier.keys;
			this.slopeSpeedMultiplier.postWrapMode = value.slopeSpeedMultiplier.postWrapMode;
			this.slopeSpeedMultiplier.preWrapMode = value.slopeSpeedMultiplier.preWrapMode;
		}
	}

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x06001775 RID: 6005 RVA: 0x0005ADD8 File Offset: 0x00058FD8
	// (set) Token: 0x06001776 RID: 6006 RVA: 0x0005AE28 File Offset: 0x00059028
	public CCMotor.Jumping jumping
	{
		get
		{
			CCMotor.Jumping result;
			result.enable = this.jumpEnable;
			result.baseHeight = this.jumpBaseHeight;
			result.extraHeight = this.jumpExtraHeight;
			result.perpAmount = this.jumpPerpAmount;
			result.steepPerpAmount = this.jumpSteepPerpAmount;
			return result;
		}
		set
		{
			this.jumpEnable = value.enable;
			this.jumpBaseHeight = value.baseHeight;
			this.jumpExtraHeight = value.extraHeight;
			this.jumpPerpAmount = value.perpAmount;
			this.jumpSteepPerpAmount = value.steepPerpAmount;
		}
	}

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x06001777 RID: 6007 RVA: 0x0005AE78 File Offset: 0x00059078
	// (set) Token: 0x06001778 RID: 6008 RVA: 0x0005AEBC File Offset: 0x000590BC
	public CCMotor.Sliding sliding
	{
		get
		{
			CCMotor.Sliding result;
			result.enable = this.slidingEnable;
			result.slidingSpeed = this.slidingSpeed;
			result.sidewaysControl = this.slidingSidewaysControl;
			result.speedControl = this.slidingSpeedControl;
			return result;
		}
		set
		{
			this.slidingEnable = value.enable;
			this.slidingSpeed = value.slidingSpeed;
			this.slidingSidewaysControl = value.sidewaysControl;
			this.slidingSpeedControl = value.speedControl;
		}
	}

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06001779 RID: 6009 RVA: 0x0005AF00 File Offset: 0x00059100
	// (set) Token: 0x0600177A RID: 6010 RVA: 0x0005AF28 File Offset: 0x00059128
	public CCMotor.MovingPlatform movingPlatform
	{
		get
		{
			CCMotor.MovingPlatform result;
			result.enable = this.platformMovementEnable;
			result.movementTransfer = this.platformMovementTransfer;
			return result;
		}
		set
		{
			this.platformMovementEnable = value.enable;
			this.platformMovementTransfer = value.movementTransfer;
		}
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x0005AF44 File Offset: 0x00059144
	public void BindSettingsTo(CCMotor motor)
	{
		motor.jumping.setup = this.jumping;
		motor.movement.setup = this.movement;
		motor.movingPlatform.setup = this.movingPlatform;
		motor.sliding = this.sliding;
		motor.OnBindCCMotorSettings();
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x0005AF98 File Offset: 0x00059198
	public void CopySettingsFrom(CCMotor motor)
	{
		this.jumping = motor.jumping.setup;
		this.movement = motor.movement.setup;
		this.movingPlatform = motor.movingPlatform.setup;
		this.sliding = motor.sliding;
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x0005AFE4 File Offset: 0x000591E4
	public override string ToString()
	{
		return string.Format("[CCMotorSettings: movement={0}, jumping={1}, sliding={2}, movingPlatform={3}]", new object[]
		{
			this.movement,
			this.jumping,
			this.sliding,
			this.movingPlatform
		});
	}

	// Token: 0x04000C30 RID: 3120
	private static readonly CCMotor.Movement Movement_init = CCMotor.Movement.init;

	// Token: 0x04000C31 RID: 3121
	public float maxForwardSpeed = CCMotorSettings.Movement_init.maxForwardSpeed;

	// Token: 0x04000C32 RID: 3122
	public float maxSidewaysSpeed = CCMotorSettings.Movement_init.maxSidewaysSpeed;

	// Token: 0x04000C33 RID: 3123
	public float maxBackwardsSpeed = CCMotorSettings.Movement_init.maxBackwardsSpeed;

	// Token: 0x04000C34 RID: 3124
	public float maxGroundAcceleration = CCMotorSettings.Movement_init.maxGroundAcceleration;

	// Token: 0x04000C35 RID: 3125
	public float maxAirAcceleration = CCMotorSettings.Movement_init.maxAirAcceleration;

	// Token: 0x04000C36 RID: 3126
	public float inputAirVelocityRatio = CCMotorSettings.Movement_init.inputAirVelocityRatio;

	// Token: 0x04000C37 RID: 3127
	public float gravity = CCMotorSettings.Movement_init.gravity;

	// Token: 0x04000C38 RID: 3128
	public float maxFallSpeed = CCMotorSettings.Movement_init.maxFallSpeed;

	// Token: 0x04000C39 RID: 3129
	public float maxAirHorizontalSpeed = CCMotorSettings.Movement_init.maxAirHorizontalSpeed;

	// Token: 0x04000C3A RID: 3130
	public float maxUnblockingHeightDifference = CCMotorSettings.Movement_init.maxUnblockingHeightDifference;

	// Token: 0x04000C3B RID: 3131
	public AnimationCurve slopeSpeedMultiplier = CCMotor.Movement.init.slopeSpeedMultiplier;

	// Token: 0x04000C3C RID: 3132
	public bool jumpEnable = CCMotor.Jumping.init.enable;

	// Token: 0x04000C3D RID: 3133
	public float jumpBaseHeight = CCMotor.Jumping.init.baseHeight;

	// Token: 0x04000C3E RID: 3134
	public float jumpExtraHeight = CCMotor.Jumping.init.extraHeight;

	// Token: 0x04000C3F RID: 3135
	public float jumpPerpAmount = CCMotor.Jumping.init.perpAmount;

	// Token: 0x04000C40 RID: 3136
	public float jumpSteepPerpAmount = CCMotor.Jumping.init.steepPerpAmount;

	// Token: 0x04000C41 RID: 3137
	public bool slidingEnable = CCMotor.Sliding.init.enable;

	// Token: 0x04000C42 RID: 3138
	public float slidingSpeed = CCMotor.Sliding.init.slidingSpeed;

	// Token: 0x04000C43 RID: 3139
	public float slidingSidewaysControl = CCMotor.Sliding.init.sidewaysControl;

	// Token: 0x04000C44 RID: 3140
	public float slidingSpeedControl = CCMotor.Sliding.init.speedControl;

	// Token: 0x04000C45 RID: 3141
	public bool platformMovementEnable = CCMotor.MovingPlatform.init.enable;

	// Token: 0x04000C46 RID: 3142
	public CCMotor.JumpMovementTransfer platformMovementTransfer = CCMotor.MovingPlatform.init.movementTransfer;
}
