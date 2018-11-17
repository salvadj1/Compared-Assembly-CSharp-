using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public sealed class CCMotorSettings : ScriptableObject
{
	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005F18C File Offset: 0x0005D38C
	// (set) Token: 0x060018DC RID: 6364 RVA: 0x0005F264 File Offset: 0x0005D464
	public global::CCMotor.Movement movement
	{
		get
		{
			global::CCMotor.Movement result;
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

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x060018DD RID: 6365 RVA: 0x0005F32C File Offset: 0x0005D52C
	// (set) Token: 0x060018DE RID: 6366 RVA: 0x0005F37C File Offset: 0x0005D57C
	public global::CCMotor.Jumping jumping
	{
		get
		{
			global::CCMotor.Jumping result;
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

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x060018DF RID: 6367 RVA: 0x0005F3CC File Offset: 0x0005D5CC
	// (set) Token: 0x060018E0 RID: 6368 RVA: 0x0005F410 File Offset: 0x0005D610
	public global::CCMotor.Sliding sliding
	{
		get
		{
			global::CCMotor.Sliding result;
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

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0005F454 File Offset: 0x0005D654
	// (set) Token: 0x060018E2 RID: 6370 RVA: 0x0005F47C File Offset: 0x0005D67C
	public global::CCMotor.MovingPlatform movingPlatform
	{
		get
		{
			global::CCMotor.MovingPlatform result;
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

	// Token: 0x060018E3 RID: 6371 RVA: 0x0005F498 File Offset: 0x0005D698
	public void BindSettingsTo(global::CCMotor motor)
	{
		motor.jumping.setup = this.jumping;
		motor.movement.setup = this.movement;
		motor.movingPlatform.setup = this.movingPlatform;
		motor.sliding = this.sliding;
		motor.OnBindCCMotorSettings();
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x0005F4EC File Offset: 0x0005D6EC
	public void CopySettingsFrom(global::CCMotor motor)
	{
		this.jumping = motor.jumping.setup;
		this.movement = motor.movement.setup;
		this.movingPlatform = motor.movingPlatform.setup;
		this.sliding = motor.sliding;
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x0005F538 File Offset: 0x0005D738
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

	// Token: 0x04000D5B RID: 3419
	private static readonly global::CCMotor.Movement Movement_init = global::CCMotor.Movement.init;

	// Token: 0x04000D5C RID: 3420
	public float maxForwardSpeed = global::CCMotorSettings.Movement_init.maxForwardSpeed;

	// Token: 0x04000D5D RID: 3421
	public float maxSidewaysSpeed = global::CCMotorSettings.Movement_init.maxSidewaysSpeed;

	// Token: 0x04000D5E RID: 3422
	public float maxBackwardsSpeed = global::CCMotorSettings.Movement_init.maxBackwardsSpeed;

	// Token: 0x04000D5F RID: 3423
	public float maxGroundAcceleration = global::CCMotorSettings.Movement_init.maxGroundAcceleration;

	// Token: 0x04000D60 RID: 3424
	public float maxAirAcceleration = global::CCMotorSettings.Movement_init.maxAirAcceleration;

	// Token: 0x04000D61 RID: 3425
	public float inputAirVelocityRatio = global::CCMotorSettings.Movement_init.inputAirVelocityRatio;

	// Token: 0x04000D62 RID: 3426
	public float gravity = global::CCMotorSettings.Movement_init.gravity;

	// Token: 0x04000D63 RID: 3427
	public float maxFallSpeed = global::CCMotorSettings.Movement_init.maxFallSpeed;

	// Token: 0x04000D64 RID: 3428
	public float maxAirHorizontalSpeed = global::CCMotorSettings.Movement_init.maxAirHorizontalSpeed;

	// Token: 0x04000D65 RID: 3429
	public float maxUnblockingHeightDifference = global::CCMotorSettings.Movement_init.maxUnblockingHeightDifference;

	// Token: 0x04000D66 RID: 3430
	public AnimationCurve slopeSpeedMultiplier = global::CCMotor.Movement.init.slopeSpeedMultiplier;

	// Token: 0x04000D67 RID: 3431
	public bool jumpEnable = global::CCMotor.Jumping.init.enable;

	// Token: 0x04000D68 RID: 3432
	public float jumpBaseHeight = global::CCMotor.Jumping.init.baseHeight;

	// Token: 0x04000D69 RID: 3433
	public float jumpExtraHeight = global::CCMotor.Jumping.init.extraHeight;

	// Token: 0x04000D6A RID: 3434
	public float jumpPerpAmount = global::CCMotor.Jumping.init.perpAmount;

	// Token: 0x04000D6B RID: 3435
	public float jumpSteepPerpAmount = global::CCMotor.Jumping.init.steepPerpAmount;

	// Token: 0x04000D6C RID: 3436
	public bool slidingEnable = global::CCMotor.Sliding.init.enable;

	// Token: 0x04000D6D RID: 3437
	public float slidingSpeed = global::CCMotor.Sliding.init.slidingSpeed;

	// Token: 0x04000D6E RID: 3438
	public float slidingSidewaysControl = global::CCMotor.Sliding.init.sidewaysControl;

	// Token: 0x04000D6F RID: 3439
	public float slidingSpeedControl = global::CCMotor.Sliding.init.speedControl;

	// Token: 0x04000D70 RID: 3440
	public bool platformMovementEnable = global::CCMotor.MovingPlatform.init.enable;

	// Token: 0x04000D71 RID: 3441
	public global::CCMotor.JumpMovementTransfer platformMovementTransfer = global::CCMotor.MovingPlatform.init.movementTransfer;
}
