using System;
using System.Collections;
using System.Text;
using Facepunch.Geometry;
using UnityEngine;

// Token: 0x0200027A RID: 634
[AddComponentMenu("ID/Local/CCMotor")]
public sealed class CCMotor : IDRemote
{
	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x06001711 RID: 5905 RVA: 0x00057BDC File Offset: 0x00055DDC
	[Obsolete("Do not query this", true)]
	public Transform transform
	{
		get
		{
			return this.tr;
		}
	}

	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x06001712 RID: 5906 RVA: 0x00057BE4 File Offset: 0x00055DE4
	private CCMotor.YawAngle characterYawAngle
	{
		get
		{
			Character character = (Character)base.idMain;
			return character.eyesYaw + Mathf.DeltaAngle(this.previousYaw.Degrees, this.currentYaw.Degrees);
		}
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x00057C24 File Offset: 0x00055E24
	private Vector3 InverseTransformPoint(Vector3 point)
	{
		return this.InverseTransformDirection(this.tr.InverseTransformPoint(point));
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x00057C38 File Offset: 0x00055E38
	private Vector3 TransformPoint(Vector3 point)
	{
		return this.tr.TransformPoint(this.TransformDirection(point));
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x00057C4C File Offset: 0x00055E4C
	private Vector3 InverseTransformDirection(Vector3 direction)
	{
		return this.characterYawAngle.Unrotate(direction);
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x00057C68 File Offset: 0x00055E68
	private Vector3 TransformDirection(Vector3 direction)
	{
		return this.characterYawAngle.Rotate(direction);
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x06001717 RID: 5911 RVA: 0x00057C84 File Offset: 0x00055E84
	// (set) Token: 0x06001718 RID: 5912 RVA: 0x00057C8C File Offset: 0x00055E8C
	public CCMotorSettings settings
	{
		get
		{
			return this._settings;
		}
		set
		{
			if (value != this._settings)
			{
				this._settings = value;
				if (Application.isPlaying)
				{
					value.BindSettingsTo(this);
				}
			}
		}
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x06001719 RID: 5913 RVA: 0x00057CB8 File Offset: 0x00055EB8
	private float baseHeightVerticalSpeed
	{
		get
		{
			return this.jumpVerticalSpeedCalculator.CalculateVerticalSpeed(ref this.jumping.setup, ref this.movement.setup);
		}
	}

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x0600171A RID: 5914 RVA: 0x00057CDC File Offset: 0x00055EDC
	public CCTotemPole ccTotemPole
	{
		get
		{
			return this.cc;
		}
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x0600171B RID: 5915 RVA: 0x00057CE4 File Offset: 0x00055EE4
	public bool isJumping
	{
		get
		{
			return this.jumping.jumping;
		}
	}

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x0600171C RID: 5916 RVA: 0x00057CF4 File Offset: 0x00055EF4
	public bool isSliding
	{
		get
		{
			return this._grounded && this.sliding.enable && this.tooSteep;
		}
	}

	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x0600171D RID: 5917 RVA: 0x00057D28 File Offset: 0x00055F28
	public bool isTouchingCeiling
	{
		get
		{
			return (this.movement.collisionFlags & 2) == 2;
		}
	}

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x0600171E RID: 5918 RVA: 0x00057D3C File Offset: 0x00055F3C
	public bool isGrounded
	{
		get
		{
			return this._grounded;
		}
	}

	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x0600171F RID: 5919 RVA: 0x00057D44 File Offset: 0x00055F44
	public bool isCrouchBlocked
	{
		get
		{
			return this.movement.crouchBlocked;
		}
	}

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x06001720 RID: 5920 RVA: 0x00057D54 File Offset: 0x00055F54
	public bool tooSteep
	{
		get
		{
			return this._groundNormal.y <= Mathf.Cos(this.cc.slopeLimit * 0.0174532924f);
		}
	}

	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06001721 RID: 5921 RVA: 0x00057D88 File Offset: 0x00055F88
	public Vector3 direction
	{
		get
		{
			return this.input.moveDirection;
		}
	}

	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06001722 RID: 5922 RVA: 0x00057D98 File Offset: 0x00055F98
	// (set) Token: 0x06001723 RID: 5923 RVA: 0x00057DA0 File Offset: 0x00055FA0
	public bool driveable
	{
		get
		{
			return this.canControl;
		}
		set
		{
			this.canControl = value;
		}
	}

	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06001724 RID: 5924 RVA: 0x00057DAC File Offset: 0x00055FAC
	public Vector3 currentGroundNormal
	{
		get
		{
			return this._groundNormal;
		}
	}

	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x06001725 RID: 5925 RVA: 0x00057DB4 File Offset: 0x00055FB4
	public Vector3 previousGroundNormal
	{
		get
		{
			return this._lastGroundNormal;
		}
	}

	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06001726 RID: 5926 RVA: 0x00057DBC File Offset: 0x00055FBC
	public Vector3 currentHitPoint
	{
		get
		{
			return this.movement.hitPoint;
		}
	}

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06001727 RID: 5927 RVA: 0x00057DCC File Offset: 0x00055FCC
	public Vector3 previousHitPoint
	{
		get
		{
			return this.movement.lastHitPoint;
		}
	}

	// Token: 0x170006C4 RID: 1732
	// (get) Token: 0x06001728 RID: 5928 RVA: 0x00057DDC File Offset: 0x00055FDC
	public Vector3? fallbackCurrentGroundNormal
	{
		get
		{
			if (this._grounded)
			{
				return new Vector3?(this._groundNormal);
			}
			return null;
		}
	}

	// Token: 0x170006C5 RID: 1733
	// (get) Token: 0x06001729 RID: 5929 RVA: 0x00057E0C File Offset: 0x0005600C
	public Vector3? fallbackPreviousGroundNormal
	{
		get
		{
			if (this._lastGroundNormal.x == 0f && this._lastGroundNormal.y == 0f && this._lastGroundNormal.z == 0f)
			{
				return null;
			}
			return new Vector3?(this._lastGroundNormal);
		}
	}

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x0600172A RID: 5930 RVA: 0x00057E70 File Offset: 0x00056070
	// (set) Token: 0x0600172B RID: 5931 RVA: 0x00057E80 File Offset: 0x00056080
	public Vector3 velocity
	{
		get
		{
			return this.movement.velocity;
		}
		set
		{
			this._grounded = false;
			this.movement.velocity = value;
			this.movement.frameVelocity = default(Vector3);
			if (this.sendExternalVelocityMessage)
			{
				this.RouteMessage("OnExternalVelocity");
			}
		}
	}

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x0600172C RID: 5932 RVA: 0x00057ECC File Offset: 0x000560CC
	// (set) Token: 0x0600172D RID: 5933 RVA: 0x00057EDC File Offset: 0x000560DC
	public Vector3 differentVelocity
	{
		get
		{
			return this.movement.velocity;
		}
		set
		{
			if (this.movement.velocity.x != value.x || this.movement.velocity.y != value.y || this.movement.velocity.z != value.z)
			{
				this.velocity = value;
			}
		}
	}

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x0600172E RID: 5934 RVA: 0x00057F44 File Offset: 0x00056144
	public bool movingWithPlatform
	{
		get
		{
			return this.movingPlatform.setup.enable && (this._grounded || this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.PermaLocked) && this.movingPlatform.activePlatform != null;
		}
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x00057F9C File Offset: 0x0005619C
	private void Awake()
	{
		if (this._settings)
		{
			this._settings.BindSettingsTo(this);
		}
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x00057FBC File Offset: 0x000561BC
	private CCTotem.MoveInfo ApplyMovementDelta(ref Vector3 moveDistance, float crouchDelta)
	{
		float height = this.cc.Height + crouchDelta;
		return this.cc.Move(moveDistance, height);
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x00057FEC File Offset: 0x000561EC
	private void ApplyYawDelta(float yRotation)
	{
		if (yRotation != 0f)
		{
			this.currentYaw = Mathf.DeltaAngle(0f, this.currentYaw.Degrees + yRotation);
		}
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x0005801C File Offset: 0x0005621C
	public void Step()
	{
		this.Step(Time.deltaTime);
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x0005802C File Offset: 0x0005622C
	public void Step(float deltaTime)
	{
		if (deltaTime <= 0f || !base.enabled)
		{
			return;
		}
		this.StepPhysics(deltaTime);
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x0005804C File Offset: 0x0005624C
	private void StepPhysics(float deltaTime)
	{
		Vector3 velocity = this.movement.velocity;
		Vector3 acceleration = this.movement.acceleration;
		this.ApplyInputVelocityChange(deltaTime, ref velocity, ref acceleration);
		bool flag;
		this.ApplyGravityAndJumping(deltaTime, ref velocity, ref acceleration, out flag);
		if (this.movingWithPlatform)
		{
			Vector3 vector = this.movingPlatform.activePlatform.TransformPoint(this.movingPlatform.activeLocal.point);
			Vector3 vector2;
			vector2.x = vector.x - this.movingPlatform.activeGlobal.point.x;
			vector2.y = vector.y - this.movingPlatform.activeGlobal.point.y;
			vector2.z = vector.z - this.movingPlatform.activeGlobal.point.z;
			if (vector2.x != 0f || vector2.y != 0f || vector2.z != 0f)
			{
				this.ApplyMovementDelta(ref vector2, 0f);
			}
			Quaternion quaternion = this.movingPlatform.activePlatform.rotation * this.movingPlatform.activeLocal.rotation;
			float y = (quaternion * Quaternion.Inverse(this.movingPlatform.activeGlobal.rotation)).eulerAngles.y;
			if (y != 0f)
			{
				this.ApplyYawDelta(y);
			}
		}
		Vector3 vector3;
		vector3.x = acceleration.x * deltaTime;
		vector3.y = acceleration.y * deltaTime;
		vector3.z = acceleration.z * deltaTime;
		Vector3 position = this.tr.position;
		Vector3 vector4;
		Vector3 vector5;
		if (flag)
		{
			vector4.x = position.x + deltaTime * (this.movement.velocity.x + vector3.x / 2f);
			vector4.y = position.y + deltaTime * (this.movement.velocity.y + vector3.y / 2f);
			vector4.z = position.z + deltaTime * (this.movement.velocity.z + vector3.z / 2f);
			vector5.x = vector4.x - position.x;
			vector5.y = vector4.y - position.y;
			vector5.z = vector4.z - position.z;
		}
		else
		{
			vector5.x = velocity.x * deltaTime;
			vector5.y = velocity.y * deltaTime;
			vector5.z = velocity.z * deltaTime;
			vector4.x = position.x + vector5.x;
			vector4.y = position.y + vector5.y;
			vector4.z = position.z + vector5.z;
		}
		float stepOffset = this.cc.stepOffset;
		float num = stepOffset * stepOffset;
		float num2 = vector5.x * vector5.x + vector5.z * vector5.z;
		float num3;
		if (num2 > num)
		{
			num3 = Mathf.Sqrt(num2);
		}
		else
		{
			num3 = stepOffset;
		}
		if (this._grounded)
		{
			vector5.y -= num3;
		}
		this.movingPlatform.hitPlatform = null;
		this._groundNormal = default(Vector3);
		float crouchDelta = this.input.crouchSpeed * deltaTime;
		CCTotem.MoveInfo moveInfo = this.ApplyMovementDelta(ref vector5, crouchDelta);
		this.movement.collisionFlags = moveInfo.CollisionFlags;
		float num4 = moveInfo.WantedHeight - moveInfo.PositionPlacement.height;
		CollisionFlags collisionFlags = moveInfo.CollisionFlags | moveInfo.WorkingCollisionFlags;
		this.movement.crouchBlocked = (this.input.crouchSpeed > 0f && (collisionFlags & 2) == 2 && num4 > this.movement.setup.maxUnblockingHeightDifference);
		this.movement.lastHitPoint = this.movement.hitPoint;
		this._lastGroundNormal = this._groundNormal;
		if (this.movingPlatform.setup.enable && this.movingPlatform.activePlatform != this.movingPlatform.hitPlatform && this.movingPlatform.hitPlatform != null)
		{
			this.movingPlatform.activePlatform = this.movingPlatform.hitPlatform;
			this.movingPlatform.lastMatrix = this.movingPlatform.hitPlatform.localToWorldMatrix;
			this.movingPlatform.newPlatform = true;
		}
		Vector3 vector7;
		if (this.movement.collisionFlags != null)
		{
			this.movement.acceleration.x = 0f;
			this.movement.acceleration.y = 0f;
			this.movement.acceleration.z = 0f;
			Vector3 vector6;
			vector6.x = velocity.x;
			vector6.y = 0f;
			vector6.z = velocity.z;
			vector7 = this.tr.position;
			this.movement.velocity.x = (vector7.x - position.x) / deltaTime;
			this.movement.velocity.y = (vector7.y - position.y) / deltaTime;
			this.movement.velocity.z = (vector7.z - position.z) / deltaTime;
			Vector3 vector8;
			vector8.x = this.movement.velocity.x;
			vector8.y = 0f;
			vector8.z = this.movement.velocity.z;
			if (vector6.x == 0f && vector6.z == 0f)
			{
				this.movement.velocity.x = 0f;
				this.movement.velocity.z = 0f;
			}
			else
			{
				float num5 = (vector8.x * vector6.x + vector8.z * vector6.z) / (vector6.x * vector6.x + vector6.z * vector6.z);
				if (num5 <= 0f)
				{
					this.movement.velocity.x = 0f;
					this.movement.velocity.z = 0f;
				}
				else if (num5 >= 1f)
				{
					this.movement.velocity.x = vector6.x;
					this.movement.velocity.z = vector6.z;
				}
				else
				{
					this.movement.velocity.x = vector6.x * num5;
					this.movement.velocity.z = vector6.z * num5;
				}
			}
			if (this.movement.velocity.y < velocity.y - 0.001f)
			{
				if (this.movement.velocity.y < 0f)
				{
					this.movement.velocity.y = velocity.y;
				}
				else
				{
					this.jumping.holdingJumpButton = false;
				}
			}
		}
		else
		{
			vector7 = vector4;
			this.movement.velocity = velocity;
			this.movement.acceleration = acceleration;
		}
		if (this._grounded != this._groundNormal.y > 0.01f)
		{
			if (this._grounded)
			{
				this._grounded = false;
				if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.PermaTransfer))
				{
					this.movement.frameVelocity = this.movingPlatform.platformVelocity;
					this.movement.velocity = this.movement.velocity + this.movingPlatform.platformVelocity;
				}
				if (this.sendFallMessage)
				{
					this.RouteMessage("OnFall", 1);
				}
				vector7.y += num3;
			}
			else
			{
				this._grounded = true;
				this.jumping.jumping = false;
				if (this.jumping.startedJumping)
				{
					this.jumping.startedJumping = false;
					this.jumping.lastLandTime = Time.time;
				}
				this.SubtractNewPlatformVelocity();
				if (this.sendLandMessage)
				{
					this.RouteMessage("OnLand", 1);
				}
			}
		}
		if (this.movingWithPlatform)
		{
			this.movingPlatform.activeGlobal.point.x = vector7.x;
			this.movingPlatform.activeGlobal.point.y = vector7.y + (this.cc.center.y - this.cc.height * 0.5f + this.cc.radius);
			this.movingPlatform.activeGlobal.point.z = vector7.z;
			this.movingPlatform.activeLocal.point = this.movingPlatform.activePlatform.InverseTransformPoint(this.movingPlatform.activeGlobal.point);
			this.movingPlatform.activeGlobal.rotation = this.tr.rotation;
			this.movingPlatform.activeLocal.rotation = Quaternion.Inverse(this.movingPlatform.activePlatform.rotation) * this.movingPlatform.activeGlobal.rotation;
		}
		this.BindCharacter();
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x00058A54 File Offset: 0x00056C54
	private void FixedUpdate()
	{
		float deltaTime = Time.deltaTime;
		if (deltaTime == 0f)
		{
			return;
		}
		if (this.movingPlatform.setup.enable)
		{
			if (this.movingPlatform.activePlatform != null)
			{
				Matrix4x4 localToWorldMatrix = this.movingPlatform.activePlatform.localToWorldMatrix;
				if (!this.movingPlatform.newPlatform)
				{
					Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocal.point);
					Vector3 vector2 = this.movingPlatform.lastMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocal.point);
					this.movingPlatform.platformVelocity.x = (vector.x - vector2.x) / deltaTime;
					this.movingPlatform.platformVelocity.y = (vector.y - vector2.y) / deltaTime;
					this.movingPlatform.platformVelocity.z = (vector.z - vector2.z) / deltaTime;
				}
				else
				{
					this.movingPlatform.newPlatform = false;
				}
				this.movingPlatform.lastMatrix = localToWorldMatrix;
			}
			else
			{
				this.movingPlatform.platformVelocity = default(Vector3);
			}
		}
		if (this.stepMode == CCMotor.StepMode.ViaFixedUpdate)
		{
			this.StepPhysics(deltaTime);
		}
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x00058BA8 File Offset: 0x00056DA8
	private void Update()
	{
		float deltaTime;
		if (this.stepMode != CCMotor.StepMode.ViaUpdate || (deltaTime = Time.deltaTime) == 0f)
		{
			return;
		}
		this.StepPhysics(deltaTime);
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x00058BDC File Offset: 0x00056DDC
	private void DesiredHorizontalVelocity(ref Vector3 inputMoveDirection, out Vector3 desiredVelocity)
	{
		Vector3 vector = this.InverseTransformDirection(inputMoveDirection);
		float num = this.MaxSpeedInDirection(ref vector);
		if (this._grounded)
		{
			num *= this.movement.setup.slopeSpeedMultiplier.Evaluate(Mathf.Asin(this.movement.velocity.normalized.y) * 57.29578f);
		}
		desiredVelocity = this.TransformDirection(vector * num);
		if (this._grounded)
		{
			this.ApplyHorizontalPushVelocity(ref desiredVelocity);
		}
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00058C6C File Offset: 0x00056E6C
	public float MaxSpeedInDirection(ref Vector3 desiredMovementDirection)
	{
		if (desiredMovementDirection.x == 0f && desiredMovementDirection.y == 0f && desiredMovementDirection.z == 0f)
		{
			return 0f;
		}
		if (this.movement.setup.maxSidewaysSpeed == 0f)
		{
			return 0f;
		}
		float num = ((desiredMovementDirection.z <= 0f) ? this.movement.setup.maxBackwardsSpeed : this.movement.setup.maxForwardSpeed) / this.movement.setup.maxSidewaysSpeed;
		Vector3 vector;
		vector.x = desiredMovementDirection.x;
		vector.y = 0f;
		vector.z = desiredMovementDirection.z / num;
		float num2 = vector.x * vector.x + vector.z * vector.z;
		if (num2 != 1f)
		{
			float num3 = Mathf.Sqrt(num2);
			vector.x /= num3;
			vector.z /= num3;
		}
		vector.z *= num;
		return Mathf.Sqrt(vector.x * vector.x + vector.z * vector.z) * this.movement.setup.maxSidewaysSpeed;
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00058DD4 File Offset: 0x00056FD4
	private void ApplyInputVelocityChange(float deltaTime, ref Vector3 velocity, ref Vector3 acceleration)
	{
		Vector3 vector = (!this.canControl) ? default(Vector3) : this.input.moveDirection;
		Vector3 vector2;
		if (this._grounded && this.tooSteep)
		{
			vector2.y = 0f;
			float num = this._groundNormal.x * this._groundNormal.x + this._groundNormal.z * this._groundNormal.z;
			if (num == 1f)
			{
				vector2.x = this._groundNormal.x;
				vector2.z = this._groundNormal.z;
			}
			else
			{
				float num2 = Mathf.Sqrt(num);
				vector2.x = this._groundNormal.x / num2;
				vector2.z = this._groundNormal.z / num2;
			}
			Vector3 vector3 = Vector3.Project(vector, vector2);
			vector2.x += vector3.x * this.sliding.speedControl + (vector.x - vector3.x) * this.sliding.sidewaysControl;
			vector2.z += vector3.z * this.sliding.speedControl + (vector.z - vector3.z) * this.sliding.sidewaysControl;
			vector2.y = vector3.y * this.sliding.speedControl + (vector.y - vector3.y) * this.sliding.sidewaysControl;
			vector2.x *= this.sliding.slidingSpeed;
			vector2.y *= this.sliding.slidingSpeed;
			vector2.z *= this.sliding.slidingSpeed;
		}
		else
		{
			this.DesiredHorizontalVelocity(ref vector, out vector2);
		}
		if (this.movingPlatform.setup.enable && this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.PermaTransfer)
		{
			vector2.x += this.movement.frameVelocity.x;
			vector2.z += this.movement.frameVelocity.z;
			vector2.y = 0f;
		}
		if (this._grounded)
		{
			acceleration.x = 0f;
			acceleration.y = 0f;
			acceleration.z = 0f;
			float num3 = vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z;
			if (num3 != 0f)
			{
				vector2 = Vector3.Cross(Vector3.Cross(Vector3.up, vector2), this._groundNormal);
				float num4 = vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z;
				if (num4 != num3)
				{
					float num5 = Mathf.Sqrt(num3);
					if (num4 == 1f)
					{
						vector2.x *= num5;
						vector2.y *= num5;
						vector2.z *= num5;
					}
					else
					{
						float num6 = num5 / Mathf.Sqrt(num4);
						vector2.x *= num6;
						vector2.y *= num6;
						vector2.z *= num6;
					}
				}
			}
		}
		else
		{
			acceleration.x = 0f;
			acceleration.y = 0f;
			acceleration.z = 0f;
			velocity.y = 0f;
		}
		if (this._grounded || this.canControl)
		{
			float num7 = ((!this._grounded) ? this.movement.setup.maxAirAcceleration : this.movement.setup.maxGroundAcceleration) * deltaTime;
			Vector3 vector4;
			vector4.x = vector2.x - velocity.x;
			vector4.y = vector2.y - velocity.y;
			vector4.z = vector2.z - velocity.z;
			float num8 = vector4.x * vector4.x + vector4.y * vector4.y + vector4.z * vector4.z;
			if (num8 > num7 * num7)
			{
				float num9 = num7 / Mathf.Sqrt(num8);
				vector4.x *= num9;
				vector4.y *= num9;
				vector4.z *= num9;
			}
			velocity += vector4;
		}
		if (this._grounded && velocity.y > 0f)
		{
			velocity.y = 0f;
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000592F0 File Offset: 0x000574F0
	private void ApplyGravityAndJumping(float deltaTime, ref Vector3 velocity, ref Vector3 acceleration, out bool simulate)
	{
		float time = Time.time;
		if (!this.input.jump || !this.canControl)
		{
			this.jumping.holdingJumpButton = false;
			this.jumping.lastButtonDownTime = -100f;
		}
		if (this.input.jump && this.jumping.lastButtonDownTime < 0f && this.canControl)
		{
			this.jumping.lastButtonDownTime = time;
		}
		if (this._grounded)
		{
			if (velocity.y >= 0f)
			{
				velocity.y = -this.movement.setup.gravity * deltaTime;
			}
			else
			{
				velocity.y -= this.movement.setup.gravity * deltaTime;
			}
			if (this.jumping.setup.enable && this.canControl && time - this.jumping.lastButtonDownTime < 0.2f && (this.minTimeBetweenJumps <= 0f || time - this.jumping.lastLandTime >= this.minTimeBetweenJumps))
			{
				if (this.minTimeBetweenJumps > 0f && time - this.jumping.lastLandTime < this.minTimeBetweenJumps)
				{
					if (this.sendJumpFailureMessage)
					{
						this.RouteMessage("OnJumpFailed", 1);
					}
				}
				else
				{
					this._grounded = false;
					this.jumping.jumping = true;
					this.jumping.lastStartTime = time;
					this.jumping.lastButtonDownTime = -100f;
					this.jumping.holdingJumpButton = true;
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this._groundNormal, (!this.tooSteep) ? this.jumping.setup.perpAmount : this.jumping.setup.steepPerpAmount);
					float num = this.jumpVerticalSpeedCalculator.CalculateVerticalSpeed(ref this.jumping.setup, ref this.movement.setup);
					velocity.x += this.jumping.jumpDir.x * num;
					velocity.y = this.jumping.jumpDir.y * num;
					velocity.z += this.jumping.jumpDir.z * num;
					if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.PermaTransfer))
					{
						this.movement.frameVelocity = this.movingPlatform.platformVelocity;
						velocity.x += this.movingPlatform.platformVelocity.x;
						velocity.y += this.movingPlatform.platformVelocity.y;
						velocity.z += this.movingPlatform.platformVelocity.z;
					}
					this.jumping.startedJumping = true;
					if (this.sendJumpMessage)
					{
						this.RouteMessage("OnJump", 1);
					}
				}
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
			simulate = false;
		}
		else
		{
			acceleration.y = -this.movement.setup.gravity;
			acceleration.z = 0f;
			acceleration.x = 0f;
			if (this.jumping.jumping && this.jumping.holdingJumpButton && time < this.jumping.lastStartTime + this.jumping.setup.extraHeight / this.jumpVerticalSpeedCalculator.CalculateVerticalSpeed(ref this.jumping.setup, ref this.movement.setup))
			{
				acceleration.x += this.jumping.jumpDir.x * this.movement.setup.gravity;
				acceleration.y += this.jumping.jumpDir.y * this.movement.setup.gravity;
				acceleration.z += this.jumping.jumpDir.z * this.movement.setup.gravity;
			}
			Vector3 vector;
			vector.x = acceleration.x * deltaTime;
			vector.y = acceleration.y * deltaTime;
			vector.z = acceleration.z * deltaTime;
			velocity.y = this.movement.velocity.y + vector.y;
			if (this.movement.setup.inputAirVelocityRatio == 1f)
			{
				velocity.x += vector.x;
				velocity.z += vector.z;
			}
			else if (this.movement.setup.inputAirVelocityRatio == 0f)
			{
				velocity.x = this.movement.velocity.x + vector.x;
				velocity.z = this.movement.velocity.z + vector.z;
			}
			else
			{
				float num2 = 1f - this.movement.setup.inputAirVelocityRatio;
				velocity.x = velocity.x * this.movement.setup.inputAirVelocityRatio + this.movement.velocity.x * num2 + vector.x;
				velocity.z = velocity.z * this.movement.setup.inputAirVelocityRatio + this.movement.velocity.z * num2 + vector.z;
			}
			if (-velocity.y > this.movement.setup.maxFallSpeed)
			{
				velocity.y = -this.movement.setup.maxFallSpeed;
			}
			if (this.movement.setup.maxAirHorizontalSpeed > 0f)
			{
				float num3 = velocity.x * velocity.x + velocity.z * velocity.z;
				if (num3 > this.movement.setup.maxAirHorizontalSpeed * this.movement.setup.maxAirHorizontalSpeed)
				{
					float num4 = this.movement.setup.maxAirHorizontalSpeed / Mathf.Sqrt(num3);
					velocity.x *= num4;
					velocity.z *= num4;
				}
			}
			simulate = true;
		}
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000599B8 File Offset: 0x00057BB8
	internal void OnBindCCMotorSettings()
	{
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x000599BC File Offset: 0x00057BBC
	private void OnDestroy()
	{
		try
		{
			base.OnDestroy();
		}
		finally
		{
			if (this._installed)
			{
				CCMotor.Callbacks.UninstallCallbacks(this, this.cc);
			}
			this.cc = null;
		}
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x00059A10 File Offset: 0x00057C10
	private void OnHit(ref CCDesc.Hit hit)
	{
		Vector3 normal = hit.Normal;
		Vector3 moveDirection = hit.MoveDirection;
		if (normal.y > 0f && normal.y > this._groundNormal.y && moveDirection.y < 0f)
		{
			Vector3 point = hit.Point;
			Vector3 vector;
			vector.x = point.x - this.movement.lastHitPoint.x;
			vector.y = point.y - this.movement.lastHitPoint.y;
			vector.z = point.z - this.movement.lastHitPoint.z;
			if ((this._lastGroundNormal.x == 0f && this._lastGroundNormal.y == 0f && this._lastGroundNormal.z == 0f) || vector.x * vector.x + vector.y * vector.y + vector.z * vector.z > 0.001f)
			{
				this._groundNormal = normal;
			}
			else
			{
				this._groundNormal = this._lastGroundNormal;
			}
			this.movingPlatform.hitPlatform = hit.Collider.transform;
			this.movement.hitPoint = point;
			this.movement.frameVelocity = default(Vector3);
		}
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x00059B90 File Offset: 0x00057D90
	private void SubtractNewPlatformVelocity()
	{
		if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == CCMotor.JumpMovementTransfer.PermaTransfer))
		{
			if (this.movingPlatform.newPlatform)
			{
				base.StartCoroutine(this.SubtractNewPlatformVelocityLateRoutine(this.movingPlatform.activePlatform));
			}
			else
			{
				this.movement.velocity = this.movement.velocity - this.movingPlatform.platformVelocity;
			}
		}
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x00059C2C File Offset: 0x00057E2C
	private IEnumerator SubtractNewPlatformVelocityLateRoutine(Transform platform)
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		if (this._grounded && platform == this.movingPlatform.activePlatform)
		{
			yield return 1;
		}
		this.movement.velocity = this.movement.velocity - this.movingPlatform.platformVelocity;
		yield break;
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x00059C58 File Offset: 0x00057E58
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.movement.setup.gravity);
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00059C78 File Offset: 0x00057E78
	private void DoPush(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x00059C7C File Offset: 0x00057E7C
	public void OnPushEnter(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x00059C88 File Offset: 0x00057E88
	public void OnPushStay(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x00059C94 File Offset: 0x00057E94
	public void OnPushExit(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x00059CA0 File Offset: 0x00057EA0
	private void MoveFromCollision(Collision collision)
	{
		PlayerPusher component = collision.gameObject.GetComponent<PlayerPusher>();
		if (component)
		{
			ContactPoint[] contacts = collision.contacts;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			for (int i = 0; i < contacts.Length; i++)
			{
				vector3 += contacts[i].point;
				vector2 += contacts[i].normal;
			}
			vector2.Normalize();
			vector3 /= (float)contacts.Length;
			Vector3 position = this.tr.position;
			position.y = vector3.y;
			vector = vector2 * (component.rigidbody.GetPointVelocity(position).magnitude * Time.deltaTime);
			Vector3 position2 = this.tr.position;
			Debug.DrawLine(position2, position2 + vector, Color.yellow, 60f);
			this.ApplyMovementDelta(ref vector, 0f);
			Debug.DrawLine(position2, this.tr.position, Color.green, 60f);
			this.BindCharacter();
		}
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x00059DC8 File Offset: 0x00057FC8
	public void OnCollisionEnter(Collision collision)
	{
		this.MoveFromCollision(collision);
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x00059DD4 File Offset: 0x00057FD4
	public void OnCollisionStay(Collision collision)
	{
		this.MoveFromCollision(collision);
	}

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x06001748 RID: 5960 RVA: 0x00059DE0 File Offset: 0x00057FE0
	public CCMotor ccmotor
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x00059DE4 File Offset: 0x00057FE4
	public void InitializeSetup(Character character, CCTotemPole cc, CharacterCCMotorTrait trait)
	{
		this.tr = base.transform;
		this.cc = cc;
		base.idMain = character;
		this.currentYaw = (this.previousYaw = 0f);
		if (trait)
		{
			if (trait.settings)
			{
				this.settings = trait.settings;
			}
			this.canControl = trait.canControl;
			this.sendLandMessage = trait.sendLandMessage;
			this.sendJumpMessage = trait.sendJumpMessage;
			this.sendJumpFailureMessage = trait.sendJumpFailureMessage;
			this.sendFallMessage = trait.sendFallMessage;
			this.sendExternalVelocityMessage = trait.sendExternalVelocityMessage;
			this.stepMode = trait.stepMode;
			this.minTimeBetweenJumps = trait.minTimeBetweenJumps;
		}
		if (!this._installed && cc)
		{
			this._installed = true;
			CCMotor.Callbacks.InstallCallbacks(this, cc);
		}
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x00059ED0 File Offset: 0x000580D0
	private void BindPosition(ref CCTotem.PositionPlacement placement)
	{
		this.tr.position = placement.bottom;
		this.LastPositionPlacement = new CCTotem.PositionPlacement?(placement);
	}

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x0600174B RID: 5963 RVA: 0x00059F00 File Offset: 0x00058100
	public string setupString
	{
		get
		{
			return string.Format("movement={0}, jumping={1}, sliding={2}, movingPlatform={3}", new object[]
			{
				this.movement.setup,
				this.jumping.setup,
				this.sliding,
				this.movingPlatform.setup
			});
		}
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x00059F64 File Offset: 0x00058164
	private void BindCharacter()
	{
		Character character = (Character)base.idMain;
		character.origin = this.tr.position;
		float num = Mathf.DeltaAngle(this.previousYaw.Degrees, this.currentYaw.Degrees);
		if (num != 0f)
		{
			this.previousYaw = this.currentYaw;
			character.eyesYaw += num;
		}
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x00059FD0 File Offset: 0x000581D0
	private void RouteMessage(string messageName)
	{
		base.idMain.SendMessage(messageName, 1);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x00059FE0 File Offset: 0x000581E0
	private void RouteMessage(string messageName, SendMessageOptions sendOptions)
	{
		base.idMain.SendMessage(messageName, sendOptions);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x00059FF0 File Offset: 0x000581F0
	public void Teleport(Vector3 origin)
	{
		if (this.cc)
		{
			this.cc.Teleport(origin);
		}
		else
		{
			this.tr.position = origin;
		}
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x0005A020 File Offset: 0x00058220
	private void ApplyHorizontalPushVelocity(ref Vector3 velocity)
	{
		CCTotemPole cctotemPole = this.cc;
		if (cctotemPole && cctotemPole.Exists)
		{
			CCDesc ccdesc = this.cc.totemicObject.CCDesc;
			Capsule capsule;
			if (ColliderUtility.GetGeometricShapeWorld(ccdesc.collider, ref capsule))
			{
				Sphere sphere = (Sphere)capsule;
				Vector3 vector = velocity;
				Vector vector2 = default(Vector);
				bool flag = false;
				foreach (Collider collider in Physics.OverlapSphere(this.cc.totemicObject.CCDesc.worldCenter, this.cc.totemicObject.CCDesc.effectiveSkinnedHeight, 1310720))
				{
					CCPusher component = collider.GetComponent<CCPusher>();
					if (component)
					{
						Vector3 vector3 = default(Vector3);
						if (component.Push(sphere.Transform(ColliderUtility.WorldToCollider(collider)), ref vector3))
						{
							flag = true;
							vector2 += ColliderUtility.ColliderToWorld(collider) * vector3;
						}
					}
				}
				if (flag)
				{
					vector2.y = 0f;
					velocity.x += vector2.x;
					velocity.z += vector2.z;
				}
			}
		}
	}

	// Token: 0x04000BCA RID: 3018
	private const float kYEpsilon = 0.001f;

	// Token: 0x04000BCB RID: 3019
	private const float kYMaxNotGrounded = 0.01f;

	// Token: 0x04000BCC RID: 3020
	private const float kResetButtonDownTime = -100f;

	// Token: 0x04000BCD RID: 3021
	private const float kJumpButtonDelaySeconds = 0.2f;

	// Token: 0x04000BCE RID: 3022
	private const float kHitEpsilon = 0.001f;

	// Token: 0x04000BCF RID: 3023
	private CCTotemPole cc;

	// Token: 0x04000BD0 RID: 3024
	internal Transform tr;

	// Token: 0x04000BD1 RID: 3025
	public CCMotor.StepMode stepMode;

	// Token: 0x04000BD2 RID: 3026
	internal bool canControl;

	// Token: 0x04000BD3 RID: 3027
	internal bool sendFallMessage;

	// Token: 0x04000BD4 RID: 3028
	internal bool sendLandMessage;

	// Token: 0x04000BD5 RID: 3029
	internal bool sendJumpMessage;

	// Token: 0x04000BD6 RID: 3030
	internal bool sendExternalVelocityMessage;

	// Token: 0x04000BD7 RID: 3031
	internal bool sendJumpFailureMessage;

	// Token: 0x04000BD8 RID: 3032
	private bool _grounded;

	// Token: 0x04000BD9 RID: 3033
	private bool _installed;

	// Token: 0x04000BDA RID: 3034
	[NonSerialized]
	public CCTotem.PositionPlacement? LastPositionPlacement;

	// Token: 0x04000BDB RID: 3035
	private CCMotor.YawAngle currentYaw;

	// Token: 0x04000BDC RID: 3036
	private CCMotor.YawAngle previousYaw;

	// Token: 0x04000BDD RID: 3037
	public float minTimeBetweenJumps;

	// Token: 0x04000BDE RID: 3038
	private Vector3 _groundNormal;

	// Token: 0x04000BDF RID: 3039
	private Vector3 _lastGroundNormal;

	// Token: 0x04000BE0 RID: 3040
	[SerializeField]
	private CCMotorSettings _settings;

	// Token: 0x04000BE1 RID: 3041
	public CCMotor.InputFrame input;

	// Token: 0x04000BE2 RID: 3042
	public CCMotor.MovementContext movement = new CCMotor.MovementContext(CCMotor.Movement.init);

	// Token: 0x04000BE3 RID: 3043
	private CCMotor.JumpBaseVerticalSpeedArgs jumpVerticalSpeedCalculator;

	// Token: 0x04000BE4 RID: 3044
	public CCMotor.JumpingContext jumping = new CCMotor.JumpingContext(CCMotor.Jumping.init);

	// Token: 0x04000BE5 RID: 3045
	public CCMotor.MovingPlatformContext movingPlatform = new CCMotor.MovingPlatformContext(CCMotor.MovingPlatform.init);

	// Token: 0x04000BE6 RID: 3046
	public CCMotor.Sliding sliding;

	// Token: 0x04000BE7 RID: 3047
	private StringBuilder stringBuilder;

	// Token: 0x04000BE8 RID: 3048
	private static bool ccmotor_debug;

	// Token: 0x0200027B RID: 635
	public enum StepMode
	{
		// Token: 0x04000BEA RID: 3050
		ViaUpdate,
		// Token: 0x04000BEB RID: 3051
		ViaFixedUpdate,
		// Token: 0x04000BEC RID: 3052
		Elsewhere
	}

	// Token: 0x0200027C RID: 636
	private struct YawAngle
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x0005A178 File Offset: 0x00058378
		private YawAngle(float Degrees)
		{
			this.Degrees = Degrees;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005A184 File Offset: 0x00058384
		public Vector3 Rotate(Vector3 direction)
		{
			return Quaternion.AngleAxis(this.Degrees, Vector3.up) * direction;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005A19C File Offset: 0x0005839C
		public Vector3 Unrotate(Vector3 direction)
		{
			return Quaternion.AngleAxis(this.Degrees, Vector3.down) * direction;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005A1B4 File Offset: 0x000583B4
		public static implicit operator CCMotor.YawAngle(float Degrees)
		{
			return new CCMotor.YawAngle(Degrees);
		}

		// Token: 0x04000BED RID: 3053
		public readonly float Degrees;
	}

	// Token: 0x0200027D RID: 637
	public struct InputFrame
	{
		// Token: 0x04000BEE RID: 3054
		public Vector3 moveDirection;

		// Token: 0x04000BEF RID: 3055
		public bool jump;

		// Token: 0x04000BF0 RID: 3056
		public float crouchSpeed;
	}

	// Token: 0x0200027E RID: 638
	public struct Movement
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0005A1BC File Offset: 0x000583BC
		public static CCMotor.Movement init
		{
			get
			{
				CCMotor.Movement result;
				result.maxForwardSpeed = 3f;
				result.maxSidewaysSpeed = 3f;
				result.maxBackwardsSpeed = 3f;
				result.maxGroundAcceleration = 30f;
				result.maxAirAcceleration = 20f;
				result.gravity = 10f;
				result.maxFallSpeed = 20f;
				result.inputAirVelocityRatio = 0.8f;
				result.maxAirHorizontalSpeed = 750f;
				result.maxUnblockingHeightDifference = 0f;
				result.slopeSpeedMultiplier = new AnimationCurve(new Keyframe[]
				{
					new Keyframe(-90f, 1f),
					new Keyframe(0f, 1f),
					new Keyframe(90f, 0f)
				});
				return result;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005A2A8 File Offset: 0x000584A8
		public override string ToString()
		{
			return string.Format("[Movement: maxForwardSpeed={0}, maxSidewaysSpeed={1}, maxBackwardsSpeed={2}, maxGroundAcceleration={3}, maxAirAcceleration={4}, inputAirVelocityRatio={5}, gravity={6}, maxFallSpeed={7}, slopeSpeedMultiplier={8}, maxAirHorizontalSpeed={9}]", new object[]
			{
				this.maxForwardSpeed,
				this.maxSidewaysSpeed,
				this.maxBackwardsSpeed,
				this.maxGroundAcceleration,
				this.maxAirAcceleration,
				this.inputAirVelocityRatio,
				this.gravity,
				this.maxFallSpeed,
				this.slopeSpeedMultiplier,
				this.maxAirHorizontalSpeed
			});
		}

		// Token: 0x04000BF1 RID: 3057
		public float maxForwardSpeed;

		// Token: 0x04000BF2 RID: 3058
		public float maxSidewaysSpeed;

		// Token: 0x04000BF3 RID: 3059
		public float maxBackwardsSpeed;

		// Token: 0x04000BF4 RID: 3060
		public float maxGroundAcceleration;

		// Token: 0x04000BF5 RID: 3061
		public float maxAirAcceleration;

		// Token: 0x04000BF6 RID: 3062
		public float inputAirVelocityRatio;

		// Token: 0x04000BF7 RID: 3063
		public float gravity;

		// Token: 0x04000BF8 RID: 3064
		public float maxFallSpeed;

		// Token: 0x04000BF9 RID: 3065
		public float maxAirHorizontalSpeed;

		// Token: 0x04000BFA RID: 3066
		public float maxUnblockingHeightDifference;

		// Token: 0x04000BFB RID: 3067
		public AnimationCurve slopeSpeedMultiplier;
	}

	// Token: 0x0200027F RID: 639
	public struct MovementContext
	{
		// Token: 0x06001757 RID: 5975 RVA: 0x0005A350 File Offset: 0x00058550
		public MovementContext(ref CCMotor.Movement setup)
		{
			this.setup = setup;
			this.collisionFlags = 0;
			this.crouchBlocked = false;
			this.acceleration = default(Vector3);
			this.velocity = default(Vector3);
			this.frameVelocity = default(Vector3);
			this.hitPoint = default(Vector3);
			this.lastHitPoint.x = float.PositiveInfinity;
			this.lastHitPoint.y = 0f;
			this.lastHitPoint.z = 0f;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005A3E4 File Offset: 0x000585E4
		public MovementContext(CCMotor.Movement setup)
		{
			this = new CCMotor.MovementContext(ref setup);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005A3F0 File Offset: 0x000585F0
		public static implicit operator CCMotor.Movement(CCMotor.MovementContext c)
		{
			return c.setup;
		}

		// Token: 0x04000BFC RID: 3068
		public CCMotor.Movement setup;

		// Token: 0x04000BFD RID: 3069
		public CollisionFlags collisionFlags;

		// Token: 0x04000BFE RID: 3070
		public bool crouchBlocked;

		// Token: 0x04000BFF RID: 3071
		public Vector3 acceleration;

		// Token: 0x04000C00 RID: 3072
		public Vector3 velocity;

		// Token: 0x04000C01 RID: 3073
		public Vector3 frameVelocity;

		// Token: 0x04000C02 RID: 3074
		public Vector3 hitPoint;

		// Token: 0x04000C03 RID: 3075
		public Vector3 lastHitPoint;
	}

	// Token: 0x02000280 RID: 640
	public struct Jumping
	{
		// Token: 0x0600175B RID: 5979 RVA: 0x0005A444 File Offset: 0x00058644
		public override string ToString()
		{
			return string.Format("[Jumping: enable={0}, baseHeight={1}, extraHeight={2}, perpAmount={3}, steepPerpAmount={4}]", new object[]
			{
				this.enable,
				this.baseHeight,
				this.extraHeight,
				this.perpAmount,
				this.steepPerpAmount
			});
		}

		// Token: 0x04000C04 RID: 3076
		public bool enable;

		// Token: 0x04000C05 RID: 3077
		public float baseHeight;

		// Token: 0x04000C06 RID: 3078
		public float extraHeight;

		// Token: 0x04000C07 RID: 3079
		public float perpAmount;

		// Token: 0x04000C08 RID: 3080
		public float steepPerpAmount;

		// Token: 0x04000C09 RID: 3081
		public static readonly CCMotor.Jumping init = new CCMotor.Jumping
		{
			enable = true,
			baseHeight = 1f,
			extraHeight = 4.1f,
			steepPerpAmount = 0.5f
		};
	}

	// Token: 0x02000281 RID: 641
	private struct JumpBaseVerticalSpeedArgs
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0005A4A8 File Offset: 0x000586A8
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x0005A4B0 File Offset: 0x000586B0
		public float baseHeight
		{
			get
			{
				return this._baseHeight;
			}
			set
			{
				if (this._baseHeight != value)
				{
					this.dirty = true;
					this._baseHeight = value;
				}
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0005A4CC File Offset: 0x000586CC
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x0005A4D4 File Offset: 0x000586D4
		public float gravity
		{
			get
			{
				return this._gravity;
			}
			set
			{
				if (this._gravity != value)
				{
					this.dirty = true;
					this._gravity = value;
				}
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005A4F0 File Offset: 0x000586F0
		public float CalculateVerticalSpeed(ref CCMotor.Jumping jumping, ref CCMotor.Movement movement)
		{
			if (this.dirty || this._baseHeight != jumping.baseHeight || this._gravity != movement.gravity)
			{
				this._baseHeight = jumping.baseHeight;
				this._gravity = movement.gravity;
				this._verticalSpeed = Mathf.Sqrt(2f * this._baseHeight * this._gravity);
				this.dirty = false;
			}
			return this._verticalSpeed;
		}

		// Token: 0x04000C0A RID: 3082
		private float _baseHeight;

		// Token: 0x04000C0B RID: 3083
		private float _gravity;

		// Token: 0x04000C0C RID: 3084
		private float _verticalSpeed;

		// Token: 0x04000C0D RID: 3085
		private bool dirty;
	}

	// Token: 0x02000282 RID: 642
	public struct JumpingContext
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x0005A570 File Offset: 0x00058770
		public JumpingContext(ref CCMotor.Jumping setup)
		{
			this.setup = setup;
			this.jumping = false;
			this.holdingJumpButton = false;
			this.startedJumping = false;
			this.lastStartTime = 0f;
			this.lastButtonDownTime = -100f;
			this.jumpDir.x = 0f;
			this.jumpDir.y = 1f;
			this.jumpDir.z = 0f;
			this.lastLandTime = float.MinValue;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005A5F0 File Offset: 0x000587F0
		public JumpingContext(CCMotor.Jumping setup)
		{
			this = new CCMotor.JumpingContext(ref setup);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005A5FC File Offset: 0x000587FC
		public static implicit operator CCMotor.Jumping(CCMotor.JumpingContext c)
		{
			return c.setup;
		}

		// Token: 0x04000C0E RID: 3086
		public CCMotor.Jumping setup;

		// Token: 0x04000C0F RID: 3087
		public bool jumping;

		// Token: 0x04000C10 RID: 3088
		public bool holdingJumpButton;

		// Token: 0x04000C11 RID: 3089
		public bool startedJumping;

		// Token: 0x04000C12 RID: 3090
		public float lastStartTime;

		// Token: 0x04000C13 RID: 3091
		public float lastButtonDownTime;

		// Token: 0x04000C14 RID: 3092
		public float lastLandTime;

		// Token: 0x04000C15 RID: 3093
		public Vector3 jumpDir;
	}

	// Token: 0x02000283 RID: 643
	public enum JumpMovementTransfer
	{
		// Token: 0x04000C17 RID: 3095
		None,
		// Token: 0x04000C18 RID: 3096
		InitTransfer,
		// Token: 0x04000C19 RID: 3097
		PermaTransfer,
		// Token: 0x04000C1A RID: 3098
		PermaLocked
	}

	// Token: 0x02000284 RID: 644
	public struct MovingPlatform
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x0005A634 File Offset: 0x00058834
		public override string ToString()
		{
			return string.Format("[MovingPlatform: enable={0}, movementTransfer={1}]", this.enable, this.movementTransfer);
		}

		// Token: 0x04000C1B RID: 3099
		public bool enable;

		// Token: 0x04000C1C RID: 3100
		public CCMotor.JumpMovementTransfer movementTransfer;

		// Token: 0x04000C1D RID: 3101
		public static readonly CCMotor.MovingPlatform init = new CCMotor.MovingPlatform
		{
			enable = true,
			movementTransfer = CCMotor.JumpMovementTransfer.PermaTransfer
		};
	}

	// Token: 0x02000285 RID: 645
	public struct MovingPlatformContext
	{
		// Token: 0x06001766 RID: 5990 RVA: 0x0005A664 File Offset: 0x00058864
		public MovingPlatformContext(ref CCMotor.MovingPlatform setup)
		{
			this.setup = setup;
			this.hitPlatform = null;
			this.activePlatform = null;
			this.activeLocal = default(CCMotor.MovingPlatformContext.PointAndRotation);
			this.activeGlobal = default(CCMotor.MovingPlatformContext.PointAndRotation);
			this.lastMatrix = default(Matrix4x4);
			this.platformVelocity = default(Vector3);
			this.newPlatform = false;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005A6D0 File Offset: 0x000588D0
		public MovingPlatformContext(CCMotor.MovingPlatform setup)
		{
			this = new CCMotor.MovingPlatformContext(ref setup);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005A6DC File Offset: 0x000588DC
		public static implicit operator CCMotor.MovingPlatform(CCMotor.MovingPlatformContext c)
		{
			return c.setup;
		}

		// Token: 0x04000C1E RID: 3102
		public CCMotor.MovingPlatform setup;

		// Token: 0x04000C1F RID: 3103
		public Transform hitPlatform;

		// Token: 0x04000C20 RID: 3104
		public Transform activePlatform;

		// Token: 0x04000C21 RID: 3105
		public CCMotor.MovingPlatformContext.PointAndRotation activeLocal;

		// Token: 0x04000C22 RID: 3106
		public CCMotor.MovingPlatformContext.PointAndRotation activeGlobal;

		// Token: 0x04000C23 RID: 3107
		public Matrix4x4 lastMatrix;

		// Token: 0x04000C24 RID: 3108
		public Vector3 platformVelocity;

		// Token: 0x04000C25 RID: 3109
		public bool newPlatform;

		// Token: 0x02000286 RID: 646
		public struct PointAndRotation
		{
			// Token: 0x04000C26 RID: 3110
			public Vector3 point;

			// Token: 0x04000C27 RID: 3111
			public Quaternion rotation;
		}
	}

	// Token: 0x02000287 RID: 647
	public struct Sliding
	{
		// Token: 0x0600176A RID: 5994 RVA: 0x0005A730 File Offset: 0x00058930
		public override string ToString()
		{
			return string.Format("[Sliding enable={0}, slidingSpeed={1}, sidewaysControl={2}, speedControl={3}]", new object[]
			{
				this.enable,
				this.slidingSpeed,
				this.sidewaysControl,
				this.speedControl
			});
		}

		// Token: 0x04000C28 RID: 3112
		public bool enable;

		// Token: 0x04000C29 RID: 3113
		public float slidingSpeed;

		// Token: 0x04000C2A RID: 3114
		public float sidewaysControl;

		// Token: 0x04000C2B RID: 3115
		public float speedControl;

		// Token: 0x04000C2C RID: 3116
		public static readonly CCMotor.Sliding init = new CCMotor.Sliding
		{
			enable = true,
			slidingSpeed = 15f,
			sidewaysControl = 1f,
			speedControl = 0.4f
		};
	}

	// Token: 0x02000288 RID: 648
	private static class Callbacks
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x0005A7C0 File Offset: 0x000589C0
		public static void InstallCallbacks(CCMotor CCMotor, CCTotemPole CCTotemPole)
		{
			CCTotemPole.Tag = CCMotor;
			CCTotemPole.OnBindPosition += CCMotor.Callbacks.PositionBinder;
			CCTotemPole.OnConfigurationBinding += CCMotor.Callbacks.ConfigurationBinder;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005A7E0 File Offset: 0x000589E0
		public static void UninstallCallbacks(CCMotor CCMotor, CCTotemPole CCTotemPole)
		{
			if (CCTotemPole && object.ReferenceEquals(CCTotemPole.Tag, CCMotor))
			{
				CCTotemPole.OnConfigurationBinding -= CCMotor.Callbacks.ConfigurationBinder;
				CCTotemPole.OnBindPosition -= CCMotor.Callbacks.PositionBinder;
				CCTotemPole.Tag = null;
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005A81C File Offset: 0x00058A1C
		private static bool OnHit(CCDesc.HitManager HitManager, ref CCDesc.Hit hit)
		{
			CCMotor ccmotor = (CCMotor)HitManager.Tag;
			if (CCMotor.ccmotor_debug && !(hit.Collider is TerrainCollider))
			{
				Debug.Log(string.Format("{{\"ccmotor\":{{\"hit\":{{\"point\":[{0},{1},{2}],\"normal\":[{3},{4},{5}]}},\"dir\":[{6},{7},{8}],\"move\":{9},\"obj\":{10}}}}}", new object[]
				{
					hit.Point.x,
					hit.Point.y,
					hit.Point.z,
					hit.Normal.x,
					hit.Normal.y,
					hit.Normal.z,
					hit.MoveDirection.x,
					hit.MoveDirection.y,
					hit.MoveDirection.z,
					hit.MoveLength,
					hit.Collider
				}), hit.GameObject);
			}
			ccmotor.OnHit(ref hit);
			return true;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005A958 File Offset: 0x00058B58
		private static void OnConfigurationBinding(bool Bind, CCDesc CCDesc, object Tag)
		{
			CCHitDispatch hitDispatch = CCHitDispatch.GetHitDispatch(CCDesc);
			if (hitDispatch)
			{
				CCDesc.HitManager hits = hitDispatch.Hits;
				if (!object.ReferenceEquals(hits, null))
				{
					if (Bind)
					{
						hits.Tag = Tag;
						hits.OnHit += CCMotor.Callbacks.HitFilter;
					}
					else if (object.ReferenceEquals(hits.Tag, Tag))
					{
						hits.Tag = null;
						hits.OnHit -= CCMotor.Callbacks.HitFilter;
					}
				}
			}
			if (Bind)
			{
				CCDesc.Tag = Tag;
				if (!CCDesc.GetComponent<CCTotemicFigure>())
				{
					IDRemote idremote = CCDesc.GetComponent<IDRemote>();
					if (!idremote)
					{
						idremote = CCDesc.gameObject.AddComponent<IDRemoteDefault>();
					}
					idremote.idMain = ((CCMotor)Tag).idMain;
					CCDesc.detectCollisions = true;
				}
			}
			else if (object.ReferenceEquals(CCDesc.Tag, Tag))
			{
				CCDesc.Tag = null;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005AA3C File Offset: 0x00058C3C
		private static void OnBindPosition(ref CCTotem.PositionPlacement PositionPlacement, object Tag)
		{
			CCMotor ccmotor = (CCMotor)Tag;
			if (ccmotor)
			{
				ccmotor.BindPosition(ref PositionPlacement);
			}
		}

		// Token: 0x04000C2D RID: 3117
		public static readonly CCDesc.HitFilter HitFilter = new CCDesc.HitFilter(CCMotor.Callbacks.OnHit);

		// Token: 0x04000C2E RID: 3118
		public static readonly CCTotem.PositionBinder PositionBinder = new CCTotem.PositionBinder(CCMotor.Callbacks.OnBindPosition);

		// Token: 0x04000C2F RID: 3119
		public static readonly CCTotem.ConfigurationBinder ConfigurationBinder = new CCTotem.ConfigurationBinder(CCMotor.Callbacks.OnConfigurationBinding);
	}
}
