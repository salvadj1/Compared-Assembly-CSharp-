using System;
using System.Collections;
using System.Text;
using Facepunch.Geometry;
using UnityEngine;

// Token: 0x020002B0 RID: 688
[AddComponentMenu("ID/Local/CCMotor")]
public sealed class CCMotor : IDRemote
{
	// Token: 0x170006FD RID: 1789
	// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005C024 File Offset: 0x0005A224
	[Obsolete("Do not query this", true)]
	public Transform transform
	{
		get
		{
			return this.tr;
		}
	}

	// Token: 0x170006FE RID: 1790
	// (get) Token: 0x06001874 RID: 6260 RVA: 0x0005C02C File Offset: 0x0005A22C
	private global::CCMotor.YawAngle characterYawAngle
	{
		get
		{
			global::Character character = (global::Character)base.idMain;
			return character.eyesYaw + Mathf.DeltaAngle(this.previousYaw.Degrees, this.currentYaw.Degrees);
		}
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x0005C06C File Offset: 0x0005A26C
	private Vector3 InverseTransformPoint(Vector3 point)
	{
		return this.InverseTransformDirection(this.tr.InverseTransformPoint(point));
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x0005C080 File Offset: 0x0005A280
	private Vector3 TransformPoint(Vector3 point)
	{
		return this.tr.TransformPoint(this.TransformDirection(point));
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x0005C094 File Offset: 0x0005A294
	private Vector3 InverseTransformDirection(Vector3 direction)
	{
		return this.characterYawAngle.Unrotate(direction);
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x0005C0B0 File Offset: 0x0005A2B0
	private Vector3 TransformDirection(Vector3 direction)
	{
		return this.characterYawAngle.Rotate(direction);
	}

	// Token: 0x170006FF RID: 1791
	// (get) Token: 0x06001879 RID: 6265 RVA: 0x0005C0CC File Offset: 0x0005A2CC
	// (set) Token: 0x0600187A RID: 6266 RVA: 0x0005C0D4 File Offset: 0x0005A2D4
	public global::CCMotorSettings settings
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

	// Token: 0x17000700 RID: 1792
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x0005C100 File Offset: 0x0005A300
	private float baseHeightVerticalSpeed
	{
		get
		{
			return this.jumpVerticalSpeedCalculator.CalculateVerticalSpeed(ref this.jumping.setup, ref this.movement.setup);
		}
	}

	// Token: 0x17000701 RID: 1793
	// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005C124 File Offset: 0x0005A324
	public global::CCTotemPole ccTotemPole
	{
		get
		{
			return this.cc;
		}
	}

	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x0600187D RID: 6269 RVA: 0x0005C12C File Offset: 0x0005A32C
	public bool isJumping
	{
		get
		{
			return this.jumping.jumping;
		}
	}

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005C13C File Offset: 0x0005A33C
	public bool isSliding
	{
		get
		{
			return this._grounded && this.sliding.enable && this.tooSteep;
		}
	}

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x0600187F RID: 6271 RVA: 0x0005C170 File Offset: 0x0005A370
	public bool isTouchingCeiling
	{
		get
		{
			return (this.movement.collisionFlags & 2) == 2;
		}
	}

	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005C184 File Offset: 0x0005A384
	public bool isGrounded
	{
		get
		{
			return this._grounded;
		}
	}

	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005C18C File Offset: 0x0005A38C
	public bool isCrouchBlocked
	{
		get
		{
			return this.movement.crouchBlocked;
		}
	}

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005C19C File Offset: 0x0005A39C
	public bool tooSteep
	{
		get
		{
			return this._groundNormal.y <= Mathf.Cos(this.cc.slopeLimit * 0.0174532924f);
		}
	}

	// Token: 0x17000708 RID: 1800
	// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005C1D0 File Offset: 0x0005A3D0
	public Vector3 direction
	{
		get
		{
			return this.input.moveDirection;
		}
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005C1E0 File Offset: 0x0005A3E0
	// (set) Token: 0x06001885 RID: 6277 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
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

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x06001886 RID: 6278 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
	public Vector3 currentGroundNormal
	{
		get
		{
			return this._groundNormal;
		}
	}

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x06001887 RID: 6279 RVA: 0x0005C1FC File Offset: 0x0005A3FC
	public Vector3 previousGroundNormal
	{
		get
		{
			return this._lastGroundNormal;
		}
	}

	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005C204 File Offset: 0x0005A404
	public Vector3 currentHitPoint
	{
		get
		{
			return this.movement.hitPoint;
		}
	}

	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005C214 File Offset: 0x0005A414
	public Vector3 previousHitPoint
	{
		get
		{
			return this.movement.lastHitPoint;
		}
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x0600188A RID: 6282 RVA: 0x0005C224 File Offset: 0x0005A424
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

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005C254 File Offset: 0x0005A454
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

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x0600188C RID: 6284 RVA: 0x0005C2B8 File Offset: 0x0005A4B8
	// (set) Token: 0x0600188D RID: 6285 RVA: 0x0005C2C8 File Offset: 0x0005A4C8
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

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x0600188E RID: 6286 RVA: 0x0005C314 File Offset: 0x0005A514
	// (set) Token: 0x0600188F RID: 6287 RVA: 0x0005C324 File Offset: 0x0005A524
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

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x06001890 RID: 6288 RVA: 0x0005C38C File Offset: 0x0005A58C
	public bool movingWithPlatform
	{
		get
		{
			return this.movingPlatform.setup.enable && (this._grounded || this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.PermaLocked) && this.movingPlatform.activePlatform != null;
		}
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x0005C3E4 File Offset: 0x0005A5E4
	private void Awake()
	{
		if (this._settings)
		{
			this._settings.BindSettingsTo(this);
		}
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x0005C404 File Offset: 0x0005A604
	private global::CCTotem.MoveInfo ApplyMovementDelta(ref Vector3 moveDistance, float crouchDelta)
	{
		float height = this.cc.Height + crouchDelta;
		return this.cc.Move(moveDistance, height);
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x0005C434 File Offset: 0x0005A634
	private void ApplyYawDelta(float yRotation)
	{
		if (yRotation != 0f)
		{
			this.currentYaw = Mathf.DeltaAngle(0f, this.currentYaw.Degrees + yRotation);
		}
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x0005C464 File Offset: 0x0005A664
	public void Step()
	{
		this.Step(Time.deltaTime);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0005C474 File Offset: 0x0005A674
	public void Step(float deltaTime)
	{
		if (deltaTime <= 0f || !base.enabled)
		{
			return;
		}
		this.StepPhysics(deltaTime);
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0005C494 File Offset: 0x0005A694
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
		global::CCTotem.MoveInfo moveInfo = this.ApplyMovementDelta(ref vector5, crouchDelta);
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
				if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.PermaTransfer))
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

	// Token: 0x06001897 RID: 6295 RVA: 0x0005CE9C File Offset: 0x0005B09C
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
		if (this.stepMode == global::CCMotor.StepMode.ViaFixedUpdate)
		{
			this.StepPhysics(deltaTime);
		}
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
	private void Update()
	{
		float deltaTime;
		if (this.stepMode != global::CCMotor.StepMode.ViaUpdate || (deltaTime = Time.deltaTime) == 0f)
		{
			return;
		}
		this.StepPhysics(deltaTime);
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x0005D024 File Offset: 0x0005B224
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

	// Token: 0x0600189A RID: 6298 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
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

	// Token: 0x0600189B RID: 6299 RVA: 0x0005D21C File Offset: 0x0005B41C
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
		if (this.movingPlatform.setup.enable && this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.PermaTransfer)
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

	// Token: 0x0600189C RID: 6300 RVA: 0x0005D738 File Offset: 0x0005B938
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
					if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.PermaTransfer))
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

	// Token: 0x0600189D RID: 6301 RVA: 0x0005DE00 File Offset: 0x0005C000
	internal void OnBindCCMotorSettings()
	{
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x0005DE04 File Offset: 0x0005C004
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
				global::CCMotor.Callbacks.UninstallCallbacks(this, this.cc);
			}
			this.cc = null;
		}
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x0005DE58 File Offset: 0x0005C058
	private void OnHit(ref global::CCDesc.Hit hit)
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

	// Token: 0x060018A0 RID: 6304 RVA: 0x0005DFD8 File Offset: 0x0005C1D8
	private void SubtractNewPlatformVelocity()
	{
		if (this.movingPlatform.setup.enable && (this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.InitTransfer || this.movingPlatform.setup.movementTransfer == global::CCMotor.JumpMovementTransfer.PermaTransfer))
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

	// Token: 0x060018A1 RID: 6305 RVA: 0x0005E074 File Offset: 0x0005C274
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

	// Token: 0x060018A2 RID: 6306 RVA: 0x0005E0A0 File Offset: 0x0005C2A0
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.movement.setup.gravity);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x0005E0C0 File Offset: 0x0005C2C0
	private void DoPush(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
	public void OnPushEnter(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
	public void OnPushStay(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x0005E0DC File Offset: 0x0005C2DC
	public void OnPushExit(Rigidbody pusher, Collider pusherCollider, Collision collisionFromPusher)
	{
		this.DoPush(pusher, pusherCollider, collisionFromPusher);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x0005E0E8 File Offset: 0x0005C2E8
	private void MoveFromCollision(Collision collision)
	{
		global::PlayerPusher component = collision.gameObject.GetComponent<global::PlayerPusher>();
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

	// Token: 0x060018A8 RID: 6312 RVA: 0x0005E210 File Offset: 0x0005C410
	public void OnCollisionEnter(Collision collision)
	{
		this.MoveFromCollision(collision);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0005E21C File Offset: 0x0005C41C
	public void OnCollisionStay(Collision collision)
	{
		this.MoveFromCollision(collision);
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005E228 File Offset: 0x0005C428
	public global::CCMotor ccmotor
	{
		get
		{
			return this;
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x0005E22C File Offset: 0x0005C42C
	public void InitializeSetup(global::Character character, global::CCTotemPole cc, global::CharacterCCMotorTrait trait)
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
			global::CCMotor.Callbacks.InstallCallbacks(this, cc);
		}
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x0005E318 File Offset: 0x0005C518
	private void BindPosition(ref global::CCTotem.PositionPlacement placement)
	{
		this.tr.position = placement.bottom;
		this.LastPositionPlacement = new global::CCTotem.PositionPlacement?(placement);
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005E348 File Offset: 0x0005C548
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

	// Token: 0x060018AE RID: 6318 RVA: 0x0005E3AC File Offset: 0x0005C5AC
	private void BindCharacter()
	{
		global::Character character = (global::Character)base.idMain;
		character.origin = this.tr.position;
		float num = Mathf.DeltaAngle(this.previousYaw.Degrees, this.currentYaw.Degrees);
		if (num != 0f)
		{
			this.previousYaw = this.currentYaw;
			character.eyesYaw += num;
		}
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x0005E418 File Offset: 0x0005C618
	private void RouteMessage(string messageName)
	{
		base.idMain.SendMessage(messageName, 1);
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x0005E428 File Offset: 0x0005C628
	private void RouteMessage(string messageName, SendMessageOptions sendOptions)
	{
		base.idMain.SendMessage(messageName, sendOptions);
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x0005E438 File Offset: 0x0005C638
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

	// Token: 0x060018B2 RID: 6322 RVA: 0x0005E468 File Offset: 0x0005C668
	private void ApplyHorizontalPushVelocity(ref Vector3 velocity)
	{
		global::CCTotemPole cctotemPole = this.cc;
		if (cctotemPole && cctotemPole.Exists)
		{
			global::CCDesc ccdesc = this.cc.totemicObject.CCDesc;
			Capsule capsule;
			if (ColliderUtility.GetGeometricShapeWorld(ccdesc.collider, ref capsule))
			{
				Sphere sphere = (Sphere)capsule;
				Vector3 vector = velocity;
				Vector vector2 = default(Vector);
				bool flag = false;
				foreach (Collider collider in Physics.OverlapSphere(this.cc.totemicObject.CCDesc.worldCenter, this.cc.totemicObject.CCDesc.effectiveSkinnedHeight, 1310720))
				{
					global::CCPusher component = collider.GetComponent<global::CCPusher>();
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

	// Token: 0x04000CF0 RID: 3312
	private const float kYEpsilon = 0.001f;

	// Token: 0x04000CF1 RID: 3313
	private const float kYMaxNotGrounded = 0.01f;

	// Token: 0x04000CF2 RID: 3314
	private const float kResetButtonDownTime = -100f;

	// Token: 0x04000CF3 RID: 3315
	private const float kJumpButtonDelaySeconds = 0.2f;

	// Token: 0x04000CF4 RID: 3316
	private const float kHitEpsilon = 0.001f;

	// Token: 0x04000CF5 RID: 3317
	private global::CCTotemPole cc;

	// Token: 0x04000CF6 RID: 3318
	internal Transform tr;

	// Token: 0x04000CF7 RID: 3319
	public global::CCMotor.StepMode stepMode;

	// Token: 0x04000CF8 RID: 3320
	internal bool canControl;

	// Token: 0x04000CF9 RID: 3321
	internal bool sendFallMessage;

	// Token: 0x04000CFA RID: 3322
	internal bool sendLandMessage;

	// Token: 0x04000CFB RID: 3323
	internal bool sendJumpMessage;

	// Token: 0x04000CFC RID: 3324
	internal bool sendExternalVelocityMessage;

	// Token: 0x04000CFD RID: 3325
	internal bool sendJumpFailureMessage;

	// Token: 0x04000CFE RID: 3326
	private bool _grounded;

	// Token: 0x04000CFF RID: 3327
	private bool _installed;

	// Token: 0x04000D00 RID: 3328
	[NonSerialized]
	public global::CCTotem.PositionPlacement? LastPositionPlacement;

	// Token: 0x04000D01 RID: 3329
	private global::CCMotor.YawAngle currentYaw;

	// Token: 0x04000D02 RID: 3330
	private global::CCMotor.YawAngle previousYaw;

	// Token: 0x04000D03 RID: 3331
	public float minTimeBetweenJumps;

	// Token: 0x04000D04 RID: 3332
	private Vector3 _groundNormal;

	// Token: 0x04000D05 RID: 3333
	private Vector3 _lastGroundNormal;

	// Token: 0x04000D06 RID: 3334
	[SerializeField]
	private global::CCMotorSettings _settings;

	// Token: 0x04000D07 RID: 3335
	public global::CCMotor.InputFrame input;

	// Token: 0x04000D08 RID: 3336
	public global::CCMotor.MovementContext movement = new global::CCMotor.MovementContext(global::CCMotor.Movement.init);

	// Token: 0x04000D09 RID: 3337
	private global::CCMotor.JumpBaseVerticalSpeedArgs jumpVerticalSpeedCalculator;

	// Token: 0x04000D0A RID: 3338
	public global::CCMotor.JumpingContext jumping = new global::CCMotor.JumpingContext(global::CCMotor.Jumping.init);

	// Token: 0x04000D0B RID: 3339
	public global::CCMotor.MovingPlatformContext movingPlatform = new global::CCMotor.MovingPlatformContext(global::CCMotor.MovingPlatform.init);

	// Token: 0x04000D0C RID: 3340
	public global::CCMotor.Sliding sliding;

	// Token: 0x04000D0D RID: 3341
	private StringBuilder stringBuilder;

	// Token: 0x04000D0E RID: 3342
	private static bool ccmotor_debug;

	// Token: 0x020002B1 RID: 689
	public enum StepMode
	{
		// Token: 0x04000D10 RID: 3344
		ViaUpdate,
		// Token: 0x04000D11 RID: 3345
		ViaFixedUpdate,
		// Token: 0x04000D12 RID: 3346
		Elsewhere
	}

	// Token: 0x020002B2 RID: 690
	private struct YawAngle
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0005E5C0 File Offset: 0x0005C7C0
		private YawAngle(float Degrees)
		{
			this.Degrees = Degrees;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0005E5CC File Offset: 0x0005C7CC
		public Vector3 Rotate(Vector3 direction)
		{
			return Quaternion.AngleAxis(this.Degrees, Vector3.up) * direction;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005E5E4 File Offset: 0x0005C7E4
		public Vector3 Unrotate(Vector3 direction)
		{
			return Quaternion.AngleAxis(this.Degrees, Vector3.down) * direction;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		public static implicit operator global::CCMotor.YawAngle(float Degrees)
		{
			return new global::CCMotor.YawAngle(Degrees);
		}

		// Token: 0x04000D13 RID: 3347
		public readonly float Degrees;
	}

	// Token: 0x020002B3 RID: 691
	public struct InputFrame
	{
		// Token: 0x04000D14 RID: 3348
		public Vector3 moveDirection;

		// Token: 0x04000D15 RID: 3349
		public bool jump;

		// Token: 0x04000D16 RID: 3350
		public float crouchSpeed;
	}

	// Token: 0x020002B4 RID: 692
	public struct Movement
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005E604 File Offset: 0x0005C804
		public static global::CCMotor.Movement init
		{
			get
			{
				global::CCMotor.Movement result;
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

		// Token: 0x060018B8 RID: 6328 RVA: 0x0005E6F0 File Offset: 0x0005C8F0
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

		// Token: 0x04000D17 RID: 3351
		public float maxForwardSpeed;

		// Token: 0x04000D18 RID: 3352
		public float maxSidewaysSpeed;

		// Token: 0x04000D19 RID: 3353
		public float maxBackwardsSpeed;

		// Token: 0x04000D1A RID: 3354
		public float maxGroundAcceleration;

		// Token: 0x04000D1B RID: 3355
		public float maxAirAcceleration;

		// Token: 0x04000D1C RID: 3356
		public float inputAirVelocityRatio;

		// Token: 0x04000D1D RID: 3357
		public float gravity;

		// Token: 0x04000D1E RID: 3358
		public float maxFallSpeed;

		// Token: 0x04000D1F RID: 3359
		public float maxAirHorizontalSpeed;

		// Token: 0x04000D20 RID: 3360
		public float maxUnblockingHeightDifference;

		// Token: 0x04000D21 RID: 3361
		public AnimationCurve slopeSpeedMultiplier;
	}

	// Token: 0x020002B5 RID: 693
	public struct MovementContext
	{
		// Token: 0x060018B9 RID: 6329 RVA: 0x0005E798 File Offset: 0x0005C998
		public MovementContext(ref global::CCMotor.Movement setup)
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

		// Token: 0x060018BA RID: 6330 RVA: 0x0005E82C File Offset: 0x0005CA2C
		public MovementContext(global::CCMotor.Movement setup)
		{
			this = new global::CCMotor.MovementContext(ref setup);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005E838 File Offset: 0x0005CA38
		public static implicit operator global::CCMotor.Movement(global::CCMotor.MovementContext c)
		{
			return c.setup;
		}

		// Token: 0x04000D22 RID: 3362
		public global::CCMotor.Movement setup;

		// Token: 0x04000D23 RID: 3363
		public CollisionFlags collisionFlags;

		// Token: 0x04000D24 RID: 3364
		public bool crouchBlocked;

		// Token: 0x04000D25 RID: 3365
		public Vector3 acceleration;

		// Token: 0x04000D26 RID: 3366
		public Vector3 velocity;

		// Token: 0x04000D27 RID: 3367
		public Vector3 frameVelocity;

		// Token: 0x04000D28 RID: 3368
		public Vector3 hitPoint;

		// Token: 0x04000D29 RID: 3369
		public Vector3 lastHitPoint;
	}

	// Token: 0x020002B6 RID: 694
	public struct Jumping
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x0005E88C File Offset: 0x0005CA8C
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

		// Token: 0x04000D2A RID: 3370
		public bool enable;

		// Token: 0x04000D2B RID: 3371
		public float baseHeight;

		// Token: 0x04000D2C RID: 3372
		public float extraHeight;

		// Token: 0x04000D2D RID: 3373
		public float perpAmount;

		// Token: 0x04000D2E RID: 3374
		public float steepPerpAmount;

		// Token: 0x04000D2F RID: 3375
		public static readonly global::CCMotor.Jumping init = new global::CCMotor.Jumping
		{
			enable = true,
			baseHeight = 1f,
			extraHeight = 4.1f,
			steepPerpAmount = 0.5f
		};
	}

	// Token: 0x020002B7 RID: 695
	private struct JumpBaseVerticalSpeedArgs
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0005E8F0 File Offset: 0x0005CAF0
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0005E8F8 File Offset: 0x0005CAF8
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

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0005E914 File Offset: 0x0005CB14
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0005E91C File Offset: 0x0005CB1C
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

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005E938 File Offset: 0x0005CB38
		public float CalculateVerticalSpeed(ref global::CCMotor.Jumping jumping, ref global::CCMotor.Movement movement)
		{
			if (this.dirty || this._baseHeight != jumping.baseHeight || this._gravity != movement.gravity)
			{
				this._baseHeight = jumping.baseHeight;
				this._gravity = movement.gravity;
				this._verticalSpeed = Mathf.Sqrt(2f * this._baseHeight * Mathf.Abs(this._gravity));
				this.dirty = false;
			}
			return this._verticalSpeed;
		}

		// Token: 0x04000D30 RID: 3376
		private float _baseHeight;

		// Token: 0x04000D31 RID: 3377
		private float _gravity;

		// Token: 0x04000D32 RID: 3378
		private float _verticalSpeed;

		// Token: 0x04000D33 RID: 3379
		private bool dirty;
	}

	// Token: 0x020002B8 RID: 696
	public struct JumpingContext
	{
		// Token: 0x060018C3 RID: 6339 RVA: 0x0005E9B4 File Offset: 0x0005CBB4
		public JumpingContext(ref global::CCMotor.Jumping setup)
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

		// Token: 0x060018C4 RID: 6340 RVA: 0x0005EA34 File Offset: 0x0005CC34
		public JumpingContext(global::CCMotor.Jumping setup)
		{
			this = new global::CCMotor.JumpingContext(ref setup);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005EA40 File Offset: 0x0005CC40
		public static implicit operator global::CCMotor.Jumping(global::CCMotor.JumpingContext c)
		{
			return c.setup;
		}

		// Token: 0x04000D34 RID: 3380
		public global::CCMotor.Jumping setup;

		// Token: 0x04000D35 RID: 3381
		public bool jumping;

		// Token: 0x04000D36 RID: 3382
		public bool holdingJumpButton;

		// Token: 0x04000D37 RID: 3383
		public bool startedJumping;

		// Token: 0x04000D38 RID: 3384
		public float lastStartTime;

		// Token: 0x04000D39 RID: 3385
		public float lastButtonDownTime;

		// Token: 0x04000D3A RID: 3386
		public float lastLandTime;

		// Token: 0x04000D3B RID: 3387
		public Vector3 jumpDir;
	}

	// Token: 0x020002B9 RID: 697
	public enum JumpMovementTransfer
	{
		// Token: 0x04000D3D RID: 3389
		None,
		// Token: 0x04000D3E RID: 3390
		InitTransfer,
		// Token: 0x04000D3F RID: 3391
		PermaTransfer,
		// Token: 0x04000D40 RID: 3392
		PermaLocked
	}

	// Token: 0x020002BA RID: 698
	public struct MovingPlatform
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x0005EA78 File Offset: 0x0005CC78
		public override string ToString()
		{
			return string.Format("[MovingPlatform: enable={0}, movementTransfer={1}]", this.enable, this.movementTransfer);
		}

		// Token: 0x04000D41 RID: 3393
		public bool enable;

		// Token: 0x04000D42 RID: 3394
		public global::CCMotor.JumpMovementTransfer movementTransfer;

		// Token: 0x04000D43 RID: 3395
		public static readonly global::CCMotor.MovingPlatform init = new global::CCMotor.MovingPlatform
		{
			enable = true,
			movementTransfer = global::CCMotor.JumpMovementTransfer.PermaTransfer
		};
	}

	// Token: 0x020002BB RID: 699
	public struct MovingPlatformContext
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x0005EAA8 File Offset: 0x0005CCA8
		public MovingPlatformContext(ref global::CCMotor.MovingPlatform setup)
		{
			this.setup = setup;
			this.hitPlatform = null;
			this.activePlatform = null;
			this.activeLocal = default(global::CCMotor.MovingPlatformContext.PointAndRotation);
			this.activeGlobal = default(global::CCMotor.MovingPlatformContext.PointAndRotation);
			this.lastMatrix = default(Matrix4x4);
			this.platformVelocity = default(Vector3);
			this.newPlatform = false;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0005EB14 File Offset: 0x0005CD14
		public MovingPlatformContext(global::CCMotor.MovingPlatform setup)
		{
			this = new global::CCMotor.MovingPlatformContext(ref setup);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0005EB20 File Offset: 0x0005CD20
		public static implicit operator global::CCMotor.MovingPlatform(global::CCMotor.MovingPlatformContext c)
		{
			return c.setup;
		}

		// Token: 0x04000D44 RID: 3396
		public global::CCMotor.MovingPlatform setup;

		// Token: 0x04000D45 RID: 3397
		public Transform hitPlatform;

		// Token: 0x04000D46 RID: 3398
		public Transform activePlatform;

		// Token: 0x04000D47 RID: 3399
		public global::CCMotor.MovingPlatformContext.PointAndRotation activeLocal;

		// Token: 0x04000D48 RID: 3400
		public global::CCMotor.MovingPlatformContext.PointAndRotation activeGlobal;

		// Token: 0x04000D49 RID: 3401
		public Matrix4x4 lastMatrix;

		// Token: 0x04000D4A RID: 3402
		public Vector3 platformVelocity;

		// Token: 0x04000D4B RID: 3403
		public bool newPlatform;

		// Token: 0x020002BC RID: 700
		public struct PointAndRotation
		{
			// Token: 0x04000D4C RID: 3404
			public Vector3 point;

			// Token: 0x04000D4D RID: 3405
			public Quaternion rotation;
		}
	}

	// Token: 0x020002BD RID: 701
	public struct Sliding
	{
		// Token: 0x060018CC RID: 6348 RVA: 0x0005EB74 File Offset: 0x0005CD74
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

		// Token: 0x04000D4E RID: 3406
		public bool enable;

		// Token: 0x04000D4F RID: 3407
		public float slidingSpeed;

		// Token: 0x04000D50 RID: 3408
		public float sidewaysControl;

		// Token: 0x04000D51 RID: 3409
		public float speedControl;

		// Token: 0x04000D52 RID: 3410
		public static readonly global::CCMotor.Sliding init = new global::CCMotor.Sliding
		{
			enable = true,
			slidingSpeed = 15f,
			sidewaysControl = 1f,
			speedControl = 0.4f
		};
	}

	// Token: 0x020002BE RID: 702
	private static class Callbacks
	{
		// Token: 0x060018CE RID: 6350 RVA: 0x0005EC04 File Offset: 0x0005CE04
		public static void InstallCallbacks(global::CCMotor CCMotor, global::CCTotemPole CCTotemPole)
		{
			CCTotemPole.Tag = CCMotor;
			CCTotemPole.OnBindPosition += global::CCMotor.Callbacks.PositionBinder;
			CCTotemPole.OnConfigurationBinding += global::CCMotor.Callbacks.ConfigurationBinder;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0005EC24 File Offset: 0x0005CE24
		public static void UninstallCallbacks(global::CCMotor CCMotor, global::CCTotemPole CCTotemPole)
		{
			if (CCTotemPole && object.ReferenceEquals(CCTotemPole.Tag, CCMotor))
			{
				CCTotemPole.OnConfigurationBinding -= global::CCMotor.Callbacks.ConfigurationBinder;
				CCTotemPole.OnBindPosition -= global::CCMotor.Callbacks.PositionBinder;
				CCTotemPole.Tag = null;
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0005EC60 File Offset: 0x0005CE60
		private static bool OnHit(global::CCDesc.HitManager HitManager, ref global::CCDesc.Hit hit)
		{
			global::CCMotor ccmotor = (global::CCMotor)HitManager.Tag;
			if (global::CCMotor.ccmotor_debug && !(hit.Collider is TerrainCollider))
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

		// Token: 0x060018D1 RID: 6353 RVA: 0x0005ED9C File Offset: 0x0005CF9C
		private static void OnConfigurationBinding(bool Bind, global::CCDesc CCDesc, object Tag)
		{
			global::CCHitDispatch hitDispatch = global::CCHitDispatch.GetHitDispatch(CCDesc);
			if (hitDispatch)
			{
				global::CCDesc.HitManager hits = hitDispatch.Hits;
				if (!object.ReferenceEquals(hits, null))
				{
					if (Bind)
					{
						hits.Tag = Tag;
						hits.OnHit += global::CCMotor.Callbacks.HitFilter;
					}
					else if (object.ReferenceEquals(hits.Tag, Tag))
					{
						hits.Tag = null;
						hits.OnHit -= global::CCMotor.Callbacks.HitFilter;
					}
				}
			}
			if (Bind)
			{
				CCDesc.Tag = Tag;
				if (!CCDesc.GetComponent<global::CCTotemicFigure>())
				{
					IDRemote idremote = CCDesc.GetComponent<IDRemote>();
					if (!idremote)
					{
						idremote = CCDesc.gameObject.AddComponent<IDRemoteDefault>();
					}
					idremote.idMain = ((global::CCMotor)Tag).idMain;
					CCDesc.detectCollisions = true;
				}
			}
			else if (object.ReferenceEquals(CCDesc.Tag, Tag))
			{
				CCDesc.Tag = null;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0005EE80 File Offset: 0x0005D080
		private static void OnBindPosition(ref global::CCTotem.PositionPlacement PositionPlacement, object Tag)
		{
			global::CCMotor ccmotor = (global::CCMotor)Tag;
			if (ccmotor)
			{
				ccmotor.BindPosition(ref PositionPlacement);
			}
		}

		// Token: 0x04000D53 RID: 3411
		public static readonly global::CCDesc.HitFilter HitFilter = new global::CCDesc.HitFilter(global::CCMotor.Callbacks.OnHit);

		// Token: 0x04000D54 RID: 3412
		public static readonly global::CCTotem.PositionBinder PositionBinder = new global::CCTotem.PositionBinder(global::CCMotor.Callbacks.OnBindPosition);

		// Token: 0x04000D55 RID: 3413
		public static readonly global::CCTotem.ConfigurationBinder ConfigurationBinder = new global::CCTotem.ConfigurationBinder(global::CCMotor.Callbacks.OnConfigurationBinding);
	}
}
