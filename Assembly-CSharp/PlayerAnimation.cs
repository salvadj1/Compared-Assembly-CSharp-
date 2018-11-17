using System;
using Facepunch.Movement;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class PlayerAnimation : global::IDLocalCharacter
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000419 RID: 1049 RVA: 0x000141B8 File Offset: 0x000123B8
	public global::Socket.LocalSpace itemAttachment
	{
		get
		{
			if (!this._madeItemAttachment && base.idMain)
			{
				global::Socket.ConfigBodyPart socket = base.GetTrait<global::CharacterItemAttachmentTrait>().socket;
				if (socket == null)
				{
					return null;
				}
				this._madeItemAttachment = socket.Extract(ref this._itemAttachmentSocket, base.idMain.hitBoxSystem);
			}
			return this._itemAttachmentSocket;
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00014218 File Offset: 0x00012418
	private void OnDrawGizmosSelected()
	{
		if (this._itemAttachmentSocket != null)
		{
			this.itemAttachment.DrawGizmos("itemAttachment");
		}
		else
		{
			global::Socket.ConfigBodyPart socket = base.GetTrait<global::CharacterItemAttachmentTrait>().socket;
			if (socket != null)
			{
				try
				{
					if (socket.Extract(ref global::PlayerAnimation.EditorHelper.tempSocketForGizmos, base.GetComponentInChildren<global::HitBoxSystem>()))
					{
						global::PlayerAnimation.EditorHelper.tempSocketForGizmos.DrawGizmos("itemAttachment");
					}
				}
				finally
				{
					if (global::PlayerAnimation.EditorHelper.tempSocketForGizmos != null)
					{
						global::PlayerAnimation.EditorHelper.tempSocketForGizmos.parent = null;
					}
				}
			}
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x000142B4 File Offset: 0x000124B4
	[ContextMenu("Rebind Item Attachment")]
	private void RebindItemAttachment()
	{
		if (this._itemAttachmentSocket != null)
		{
			this._itemAttachmentSocket.eulerRotate = base.GetTrait<global::CharacterItemAttachmentTrait>().socket.eulerRotate;
			this._itemAttachmentSocket.offset = base.GetTrait<global::CharacterItemAttachmentTrait>().socket.offset;
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00014304 File Offset: 0x00012504
	private void Awake()
	{
		if (!this.animation && !(this.animation = base.animation))
		{
			Debug.LogError("There must be a animation component defined!", this);
		}
		this.animationTrait = base.GetTrait<global::CharacterAnimationTrait>();
		if (!this.animationTrait.movementAnimationSetup.CreateSampler(this.animation, ref this.movement))
		{
			Debug.LogError("Failed to make movement sampler", this);
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00014380 File Offset: 0x00012580
	private void OnDestroy()
	{
		this.movement = null;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0001438C File Offset: 0x0001258C
	private void Start()
	{
		global::Character idMain = base.idMain;
		this.lastPos = ((!idMain) ? base.transform.position : idMain.origin);
		this.lastPosPrecise.f = this.lastPos;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x000143D8 File Offset: 0x000125D8
	private void Update()
	{
		this.CalculateVelocity();
		global::Character idMain = base.idMain;
		bool flag = idMain;
		global::CharacterStateFlags characterStateFlags;
		if (flag)
		{
			characterStateFlags = idMain.stateFlags;
		}
		else
		{
			characterStateFlags = default(global::CharacterStateFlags);
		}
		bool flag2 = !characterStateFlags.grounded;
		bool focus = characterStateFlags.focus;
		bool crouch = characterStateFlags.crouch;
		Weights weights;
		weights.idle = 0f;
		if (this.movementNormal.x > 0f)
		{
			weights.east = this.movementNormal.x;
			weights.west = 0f;
		}
		else if (this.movementNormal.x < 0f)
		{
			weights.east = 0f;
			weights.west = -this.movementNormal.x;
		}
		else
		{
			weights.east = (weights.west = 0f);
		}
		if (this.movementNormal.y > 0f)
		{
			weights.north = this.movementNormal.y;
			weights.south = 0f;
		}
		else if (this.movementNormal.y < 0f)
		{
			weights.north = 0f;
			weights.south = -this.movementNormal.y;
		}
		else
		{
			weights.north = (weights.south = 0f);
		}
		if (this.movementNormal.y == 0f && this.movementNormal.x == 0f)
		{
			weights = this.lastHeadingWeights;
		}
		weights.idle = 0f;
		this.lastHeadingWeights = weights;
		State state;
		if (flag2)
		{
			state = 1;
		}
		else if (crouch)
		{
			state = 0;
		}
		else if (characterStateFlags.sprint && this.speedPrecise >= (double)this.movement.configuration.runSpeed)
		{
			state = 2;
		}
		else
		{
			state = 1;
		}
		string text = this.itemHolder.animationGroupName;
		if (this.idealGroupName != text)
		{
			this.idealGroupName = text;
			text = (text ?? this.animationTrait.defaultGroupName);
			int? num = this.movement.configuration.GroupIndex(text);
			if (num == null)
			{
				Debug.LogWarning("Could not find group name " + this.idealGroupName);
				this.usingGroupName = this.animationTrait.defaultGroupName;
				int? num2 = this.movement.configuration.GroupIndex(this.usingGroupName);
				this.usingGroupIndex = ((num2 == null) ? 0 : num2.Value);
			}
			else
			{
				this.usingGroupName = this.idealGroupName;
				this.usingGroupIndex = num.Value;
			}
		}
		int group = this.usingGroupIndex;
		double num3;
		float num4;
		if (!characterStateFlags.slipping)
		{
			num3 = (double)Time.deltaTime;
			this.movement.state = state;
			this.movement.group = group;
			num4 = this.movement.UpdateWeights(Time.deltaTime, flag2, !flag || characterStateFlags.movement);
		}
		else
		{
			num3 = (double)(-(double)Time.deltaTime);
			num4 = this.lastUnitScale;
		}
		this.wasAirborne = flag2;
		this.lastUnitScale = num4;
		if (!double.IsNaN(this.speedPrecise) && !double.IsInfinity(this.speedPrecise))
		{
			float num5 = this.positionTime;
			this.positionTime = (float)(((double)this.positionTime + Math.Abs((double)num4 * this.speedPrecise * num3)) % 1.0);
			if (this.positionTime < 0f)
			{
				this.positionTime += 1f;
			}
			else if (float.IsNaN(this.positionTime) || float.IsInfinity(this.positionTime))
			{
				this.positionTime = num5;
			}
			this.movement.configuration.OffsetTime(this.positionTime, ref this.times);
		}
		float num6 = (!flag) ? (-base.transform.eulerAngles.x) : idMain.eyesPitch;
		this.movement.SetWeights(this.animation, ref weights, ref this.times, num6);
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00014844 File Offset: 0x00012A44
	private void CalculateVelocity()
	{
		double num = (double)Time.time - (double)this.lastVelocityCalc;
		global::Character idMain = base.idMain;
		Vector3 vector = (!idMain) ? base.transform.position : idMain.origin;
		Vector3G vector3G;
		vector3G..ctor(ref vector);
		double num2 = 1.0 / num;
		Vector3G vector3G2;
		vector3G2.x = num2 * (vector3G.x - this.lastPosPrecise.x);
		vector3G2.y = num2 * (vector3G.y - this.lastPosPrecise.y);
		vector3G2.z = num2 * (vector3G.z - this.lastPosPrecise.z);
		Matrix4x4G matrix4x4G;
		matrix4x4G..ctor(base.transform.worldToLocalMatrix);
		Matrix4x4G.Mult3x3(ref vector3G2, ref matrix4x4G, ref this.localVelocityPrecise);
		this.lastVelocityCalc = Time.time;
		this.speedPrecise = Math.Sqrt(this.localVelocityPrecise.x * this.localVelocityPrecise.x + this.localVelocityPrecise.z * this.localVelocityPrecise.z);
		if (this.speedPrecise < (double)this.movement.configuration.minMoveSpeed)
		{
			this.speedPrecise = 0.0;
			this.movementNormalPrecise.x = 0.0;
			this.movementNormalPrecise.y = 0.0;
			float maxTimeBetweenTurns;
			if (this.lastAngleSpeedPrecise > 0.0 && (maxTimeBetweenTurns = this.movement.configuration.maxTimeBetweenTurns) > 0f)
			{
				this.lastAngleSpeedPrecise -= (double)(Time.deltaTime / maxTimeBetweenTurns);
			}
		}
		else
		{
			double num3 = 1.0 / this.speedPrecise;
			this.movementNormalPrecise.x = (double)this.localVelocity.x * num3;
			this.movementNormalPrecise.y = (double)this.localVelocity.z * num3;
			double num4 = this.anglePrecise;
			this.anglePrecise = Math.Atan2(this.movementNormalPrecise.x, this.movementNormalPrecise.y) / 3.1415926535897931 * 180.0;
			float maxTurnSpeed = this.movement.configuration.maxTurnSpeed;
			if (maxTurnSpeed > 0f && this.anglePrecise != num4 && this.lastAngleSpeedPrecise >= 0.05)
			{
				double num5 = (double)Time.deltaTime * (double)maxTurnSpeed;
				if (Precise.MoveTowardsAngle(ref num4, ref this.anglePrecise, ref num5, ref this.anglePrecise))
				{
					double num6 = this.anglePrecise / 180.0 * 3.1415926535897931;
					this.movementNormalPrecise.x = Math.Sin(num6);
					this.movementNormalPrecise.y = Math.Cos(num6);
				}
			}
			this.lastAngleSpeedPrecise = this.speedPrecise;
		}
		this.lastPosPrecise = vector3G;
		this.lastPos = vector;
		this.movementNormal = this.movementNormalPrecise.f;
		this.speed = (float)this.speedPrecise;
		this.angle = (float)this.anglePrecise;
		this.localVelocity = this.localVelocityPrecise.f;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00014B7C File Offset: 0x00012D7C
	public bool PlayAnimation(GroupEvent GroupEvent, float animationSpeed, float animationTime)
	{
		if (this.movement == null)
		{
			Debug.Log("no Movement");
			return false;
		}
		AnimationState animationState;
		try
		{
			if (!this.movement.GetGroupEvent(GroupEvent, ref animationState))
			{
				return false;
			}
		}
		catch (NotImplementedException ex)
		{
			Debug.LogException(ex, this);
			return false;
		}
		if (animationTime < 0f)
		{
			animationState.time = -animationTime;
		}
		else
		{
			animationState.normalizedTime = animationTime;
		}
		if (this.animation.Play(animationState.name, 0))
		{
			if (animationState.speed != animationSpeed)
			{
				animationState.speed = animationSpeed;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00014C40 File Offset: 0x00012E40
	public bool PlayAnimation(GroupEvent GroupEvent, float animationSpeed)
	{
		return this.PlayAnimation(GroupEvent, animationSpeed, 0f);
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00014C50 File Offset: 0x00012E50
	public bool PlayAnimation(GroupEvent GroupEvent)
	{
		return this.PlayAnimation(GroupEvent, 1f, 0f);
	}

	// Token: 0x04000380 RID: 896
	public const double MIN_ANIM_SPEED = 0.05;

	// Token: 0x04000381 RID: 897
	[PrefetchComponent]
	public Animation animation;

	// Token: 0x04000382 RID: 898
	[PrefetchComponent]
	public global::InventoryHolder itemHolder;

	// Token: 0x04000383 RID: 899
	private Transform characterTransform;

	// Token: 0x04000384 RID: 900
	private Vector3 localVelocity;

	// Token: 0x04000385 RID: 901
	private Vector3 lastPos;

	// Token: 0x04000386 RID: 902
	private Vector2 movementNormal;

	// Token: 0x04000387 RID: 903
	private Vector4 times;

	// Token: 0x04000388 RID: 904
	private Weights lastHeadingWeights;

	// Token: 0x04000389 RID: 905
	private Weights baseDecay;

	// Token: 0x0400038A RID: 906
	private Vector3G localVelocityPrecise;

	// Token: 0x0400038B RID: 907
	private Vector3G lastPosPrecise;

	// Token: 0x0400038C RID: 908
	private Vector2G movementNormalPrecise;

	// Token: 0x0400038D RID: 909
	private double speedPrecise;

	// Token: 0x0400038E RID: 910
	private double anglePrecise;

	// Token: 0x0400038F RID: 911
	private double lastAngleSpeedPrecise;

	// Token: 0x04000390 RID: 912
	private float speed;

	// Token: 0x04000391 RID: 913
	private float angle;

	// Token: 0x04000392 RID: 914
	private float positionTime;

	// Token: 0x04000393 RID: 915
	private float lastUnitScale;

	// Token: 0x04000394 RID: 916
	private float lastVelocityCalc;

	// Token: 0x04000395 RID: 917
	private Sampler movement;

	// Token: 0x04000396 RID: 918
	private bool wasAirborne;

	// Token: 0x04000397 RID: 919
	private bool decaying;

	// Token: 0x04000398 RID: 920
	private Configuration configuration;

	// Token: 0x04000399 RID: 921
	[NonSerialized]
	private string idealGroupName;

	// Token: 0x0400039A RID: 922
	[NonSerialized]
	private string usingGroupName;

	// Token: 0x0400039B RID: 923
	[NonSerialized]
	private int usingGroupIndex;

	// Token: 0x0400039C RID: 924
	[NonSerialized]
	private global::CharacterAnimationTrait animationTrait;

	// Token: 0x0400039D RID: 925
	private bool _madeItemAttachment;

	// Token: 0x0400039E RID: 926
	private int group_unarmed;

	// Token: 0x0400039F RID: 927
	private int group_armed = 1;

	// Token: 0x040003A0 RID: 928
	[NonSerialized]
	private global::Socket.LocalSpace _itemAttachmentSocket;

	// Token: 0x020000BC RID: 188
	private static class EditorHelper
	{
		// Token: 0x040003A1 RID: 929
		public static global::Socket.LocalSpace tempSocketForGizmos;
	}
}
