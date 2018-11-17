using System;
using Facepunch.Movement;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class PlayerAnimation : IDLocalCharacter
{
	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060003A1 RID: 929 RVA: 0x000129C8 File Offset: 0x00010BC8
	public Socket.LocalSpace itemAttachment
	{
		get
		{
			if (!this._madeItemAttachment && base.idMain)
			{
				Socket.ConfigBodyPart socket = base.GetTrait<CharacterItemAttachmentTrait>().socket;
				if (socket == null)
				{
					return null;
				}
				this._madeItemAttachment = socket.Extract(ref this._itemAttachmentSocket, base.idMain.hitBoxSystem);
			}
			return this._itemAttachmentSocket;
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00012A28 File Offset: 0x00010C28
	private void OnDrawGizmosSelected()
	{
		if (this._itemAttachmentSocket != null)
		{
			this.itemAttachment.DrawGizmos("itemAttachment");
		}
		else
		{
			Socket.ConfigBodyPart socket = base.GetTrait<CharacterItemAttachmentTrait>().socket;
			if (socket != null)
			{
				try
				{
					if (socket.Extract(ref PlayerAnimation.EditorHelper.tempSocketForGizmos, base.GetComponentInChildren<HitBoxSystem>()))
					{
						PlayerAnimation.EditorHelper.tempSocketForGizmos.DrawGizmos("itemAttachment");
					}
				}
				finally
				{
					if (PlayerAnimation.EditorHelper.tempSocketForGizmos != null)
					{
						PlayerAnimation.EditorHelper.tempSocketForGizmos.parent = null;
					}
				}
			}
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00012AC4 File Offset: 0x00010CC4
	[ContextMenu("Rebind Item Attachment")]
	private void RebindItemAttachment()
	{
		if (this._itemAttachmentSocket != null)
		{
			this._itemAttachmentSocket.eulerRotate = base.GetTrait<CharacterItemAttachmentTrait>().socket.eulerRotate;
			this._itemAttachmentSocket.offset = base.GetTrait<CharacterItemAttachmentTrait>().socket.offset;
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00012B14 File Offset: 0x00010D14
	private void Awake()
	{
		if (!this.animation && !(this.animation = base.animation))
		{
			Debug.LogError("There must be a animation component defined!", this);
		}
		this.animationTrait = base.GetTrait<CharacterAnimationTrait>();
		if (!this.animationTrait.movementAnimationSetup.CreateSampler(this.animation, ref this.movement))
		{
			Debug.LogError("Failed to make movement sampler", this);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00012B90 File Offset: 0x00010D90
	private void OnDestroy()
	{
		this.movement = null;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00012B9C File Offset: 0x00010D9C
	private void Start()
	{
		Character idMain = base.idMain;
		this.lastPos = ((!idMain) ? base.transform.position : idMain.origin);
		this.lastPosPrecise.f = this.lastPos;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00012BE8 File Offset: 0x00010DE8
	private void Update()
	{
		this.CalculateVelocity();
		Character idMain = base.idMain;
		bool flag = idMain;
		CharacterStateFlags characterStateFlags;
		if (flag)
		{
			characterStateFlags = idMain.stateFlags;
		}
		else
		{
			characterStateFlags = default(CharacterStateFlags);
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

	// Token: 0x060003A8 RID: 936 RVA: 0x00013054 File Offset: 0x00011254
	private void CalculateVelocity()
	{
		double num = (double)Time.time - (double)this.lastVelocityCalc;
		Character idMain = base.idMain;
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

	// Token: 0x060003A9 RID: 937 RVA: 0x0001338C File Offset: 0x0001158C
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

	// Token: 0x060003AA RID: 938 RVA: 0x00013450 File Offset: 0x00011650
	public bool PlayAnimation(GroupEvent GroupEvent, float animationSpeed)
	{
		return this.PlayAnimation(GroupEvent, animationSpeed, 0f);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00013460 File Offset: 0x00011660
	public bool PlayAnimation(GroupEvent GroupEvent)
	{
		return this.PlayAnimation(GroupEvent, 1f, 0f);
	}

	// Token: 0x04000315 RID: 789
	public const double MIN_ANIM_SPEED = 0.05;

	// Token: 0x04000316 RID: 790
	[PrefetchComponent]
	public Animation animation;

	// Token: 0x04000317 RID: 791
	[PrefetchComponent]
	public InventoryHolder itemHolder;

	// Token: 0x04000318 RID: 792
	private Transform characterTransform;

	// Token: 0x04000319 RID: 793
	private Vector3 localVelocity;

	// Token: 0x0400031A RID: 794
	private Vector3 lastPos;

	// Token: 0x0400031B RID: 795
	private Vector2 movementNormal;

	// Token: 0x0400031C RID: 796
	private Vector4 times;

	// Token: 0x0400031D RID: 797
	private Weights lastHeadingWeights;

	// Token: 0x0400031E RID: 798
	private Weights baseDecay;

	// Token: 0x0400031F RID: 799
	private Vector3G localVelocityPrecise;

	// Token: 0x04000320 RID: 800
	private Vector3G lastPosPrecise;

	// Token: 0x04000321 RID: 801
	private Vector2G movementNormalPrecise;

	// Token: 0x04000322 RID: 802
	private double speedPrecise;

	// Token: 0x04000323 RID: 803
	private double anglePrecise;

	// Token: 0x04000324 RID: 804
	private double lastAngleSpeedPrecise;

	// Token: 0x04000325 RID: 805
	private float speed;

	// Token: 0x04000326 RID: 806
	private float angle;

	// Token: 0x04000327 RID: 807
	private float positionTime;

	// Token: 0x04000328 RID: 808
	private float lastUnitScale;

	// Token: 0x04000329 RID: 809
	private float lastVelocityCalc;

	// Token: 0x0400032A RID: 810
	private Sampler movement;

	// Token: 0x0400032B RID: 811
	private bool wasAirborne;

	// Token: 0x0400032C RID: 812
	private bool decaying;

	// Token: 0x0400032D RID: 813
	private Configuration configuration;

	// Token: 0x0400032E RID: 814
	[NonSerialized]
	private string idealGroupName;

	// Token: 0x0400032F RID: 815
	[NonSerialized]
	private string usingGroupName;

	// Token: 0x04000330 RID: 816
	[NonSerialized]
	private int usingGroupIndex;

	// Token: 0x04000331 RID: 817
	[NonSerialized]
	private CharacterAnimationTrait animationTrait;

	// Token: 0x04000332 RID: 818
	private bool _madeItemAttachment;

	// Token: 0x04000333 RID: 819
	private int group_unarmed;

	// Token: 0x04000334 RID: 820
	private int group_armed = 1;

	// Token: 0x04000335 RID: 821
	[NonSerialized]
	private Socket.LocalSpace _itemAttachmentSocket;

	// Token: 0x020000A9 RID: 169
	private static class EditorHelper
	{
		// Token: 0x04000336 RID: 822
		public static Socket.LocalSpace tempSocketForGizmos;
	}
}
