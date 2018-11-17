using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020004E4 RID: 1252
public class BasicWildLifeAI : NetBehaviour, global::IInterpTimedEventReceiver
{
	// Token: 0x06002B39 RID: 11065 RVA: 0x000A0BC0 File Offset: 0x0009EDC0
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (!this.OnInterpTimedEvent())
		{
			global::InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x1700097F RID: 2431
	// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000A0BE0 File Offset: 0x0009EDE0
	public Transform transform
	{
		get
		{
			return this._myTransform;
		}
	}

	// Token: 0x06002B3B RID: 11067 RVA: 0x000A0BE8 File Offset: 0x0009EDE8
	protected float GetWalkAnimScalar()
	{
		return this.walkAnimScalar;
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x000A0BF0 File Offset: 0x0009EDF0
	protected float GetRunAnimScalar()
	{
		return this.runAnimScalar;
	}

	// Token: 0x06002B3D RID: 11069 RVA: 0x000A0BF8 File Offset: 0x0009EDF8
	protected float GetMoveSpeedForAnim()
	{
		Vector3 vector;
		this._interp.SampleWorldVelocity(out vector);
		return vector.magnitude;
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x000A0C1C File Offset: 0x0009EE1C
	public virtual string GetDeathAnim()
	{
		return "death";
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x000A0C24 File Offset: 0x0009EE24
	protected void DoClientDeath()
	{
		base.animation[this.GetDeathAnim()].wrapMode = 8;
		base.animation.CrossFade(this.GetDeathAnim(), 0.2f);
		this._takeDamage.health = 0f;
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x000A0C70 File Offset: 0x0009EE70
	protected void OnClientDeath(ref Vector3 deathPosition, uLink.NetworkViewID attackerNetViewID, ref uLink.NetworkMessageInfo info)
	{
		Vector3 vector;
		Vector3 up;
		global::TransformHelpers.GetGroundInfo(deathPosition + new Vector3(0f, 0.25f, 0f), 10f, out vector, out up);
		deathPosition = vector;
		Quaternion rot = global::TransformHelpers.LookRotationForcedUp(this._myTransform.rotation * Vector3.forward, up);
		this._interp.SetGoals(deathPosition, rot, info.timestamp);
		if (attackerNetViewID.isMine)
		{
			this.DoClientDeath();
		}
		else
		{
			global::InterpTimedEvent.Queue(this, "DEATH", ref info);
		}
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x000A0D0C File Offset: 0x0009EF0C
	protected void OnNetworkUpdate(ref Vector3 origin, ref Quaternion rotation, ref uLink.NetworkMessageInfo info)
	{
		this._wildMove.ProcessNetworkUpdate(ref origin, ref rotation);
		this._interp.SetGoals(origin, rotation, info.timestamp);
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x000A0D44 File Offset: 0x0009EF44
	protected virtual bool PlaySnd(int type)
	{
		AudioClip audioClip = null;
		float volume;
		float minDistance;
		float maxDistance;
		if (type == 0)
		{
			if (this.idleSounds != null)
			{
				audioClip = this.idleSounds[Random.Range(0, this.idleSounds.Length)];
			}
			volume = 0.4f;
			minDistance = 0.25f;
			maxDistance = 8f;
		}
		else if (type == 3)
		{
			if (this.fleeStartSounds != null)
			{
				audioClip = this.fleeStartSounds[Random.Range(0, this.fleeStartSounds.Length)];
			}
			volume = 0.9f;
			minDistance = 1.25f;
			maxDistance = 10f;
		}
		else
		{
			if (type != 4)
			{
				return false;
			}
			if (this.deathSounds != null)
			{
				audioClip = this.deathSounds[Random.Range(0, this.deathSounds.Length)];
			}
			volume = 1f;
			minDistance = 2.25f;
			maxDistance = 20f;
		}
		if (audioClip)
		{
			audioClip.PlayLocal(this.transform, Vector3.zero, volume, minDistance, maxDistance);
		}
		return true;
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x000A0E6C File Offset: 0x0009F06C
	protected void Awake()
	{
		this._myTransform = base.transform;
		this._takeDamage = base.GetComponent<global::TakeDamage>();
		this._wildMove = base.GetComponent<global::BaseAIMovement>();
		this._interp = base.GetComponent<global::TransformInterpolator>();
		Object.Destroy(base.GetComponent<global::BasicWildLifeMovement>());
		Object.Destroy(base.GetComponent<global::VisNode>());
		this._takeDamage.enabled = false;
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x000A0ECC File Offset: 0x0009F0CC
	protected void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		this._interp.running = true;
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x000A0EDC File Offset: 0x0009F0DC
	protected void OnDestroy()
	{
		global::InterpTimedEvent.Remove(this);
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x000A0EE4 File Offset: 0x0009F0E4
	[RPC]
	protected void GetNetworkUpdate(Vector3 pos, global::Angle2 rot, uLink.NetworkMessageInfo info)
	{
		Quaternion quaternion = (Quaternion)rot;
		this.OnNetworkUpdate(ref pos, ref quaternion, ref info);
	}

	// Token: 0x06002B47 RID: 11079 RVA: 0x000A0F04 File Offset: 0x0009F104
	[RPC]
	protected void Snd(byte type, uLink.NetworkMessageInfo info)
	{
		try
		{
			global::InterpTimedEvent.Queue(this, "SOUND", ref info, new object[]
			{
				(int)type
			});
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			Debug.LogWarning("Running emergency dump because of previous exception in Snd", this);
			global::InterpTimedEvent.EMERGENCY_DUMP(true);
		}
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x000A0F6C File Offset: 0x0009F16C
	[RPC]
	protected void ClientHealthChange(float newHealth)
	{
		if (this._takeDamage.health > newHealth)
		{
		}
		this._takeDamage.health = newHealth;
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x000A0F8C File Offset: 0x0009F18C
	[RPC]
	protected void ClientDeath(Vector3 deadPos, uLink.NetworkViewID attackerID, uLink.NetworkMessageInfo info)
	{
		this.OnClientDeath(ref deadPos, attackerID, ref info);
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x000A0F9C File Offset: 0x0009F19C
	protected virtual bool OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::BasicWildLifeAI.<>f__switch$map7 == null)
			{
				global::BasicWildLifeAI.<>f__switch$map7 = new Dictionary<string, int>(2)
				{
					{
						"DEATH",
						0
					},
					{
						"SOUND",
						1
					}
				};
			}
			int num;
			if (global::BasicWildLifeAI.<>f__switch$map7.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.DoClientDeath();
					return true;
				}
				if (num == 1)
				{
					this.PlaySnd(global::InterpTimedEvent.Argument<int>(0));
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04001507 RID: 5383
	private const string RPCName_GetNetworkUpdate = "GetNetworkUpdate";

	// Token: 0x04001508 RID: 5384
	private const string RPCName_Snd = "Snd";

	// Token: 0x04001509 RID: 5385
	private const string RPCName_ClientHealthChange = "ClientHealthChange";

	// Token: 0x0400150A RID: 5386
	private const string RPCName_ClientDeath = "ClientDeath";

	// Token: 0x0400150B RID: 5387
	public bool afraidOfFootsteps = true;

	// Token: 0x0400150C RID: 5388
	public bool afraidOfDanger = true;

	// Token: 0x0400150D RID: 5389
	[SerializeField]
	protected global::AudioClipArray idleSounds;

	// Token: 0x0400150E RID: 5390
	[SerializeField]
	protected global::AudioClipArray fleeStartSounds;

	// Token: 0x0400150F RID: 5391
	[SerializeField]
	protected global::AudioClipArray deathSounds;

	// Token: 0x04001510 RID: 5392
	[SerializeField]
	protected float walkSpeed = 1f;

	// Token: 0x04001511 RID: 5393
	[SerializeField]
	protected float runSpeed = 3f;

	// Token: 0x04001512 RID: 5394
	[SerializeField]
	protected float walkAnimScalar = 1f;

	// Token: 0x04001513 RID: 5395
	[SerializeField]
	protected float runAnimScalar = 1f;

	// Token: 0x04001514 RID: 5396
	protected Transform _myTransform;

	// Token: 0x04001515 RID: 5397
	protected global::TakeDamage _takeDamage;

	// Token: 0x04001516 RID: 5398
	protected global::BaseAIMovement _wildMove;

	// Token: 0x04001517 RID: 5399
	protected global::TransformInterpolator _interp;

	// Token: 0x020004E5 RID: 1253
	public enum AISound : byte
	{
		// Token: 0x0400151A RID: 5402
		Idle,
		// Token: 0x0400151B RID: 5403
		Warn,
		// Token: 0x0400151C RID: 5404
		Attack,
		// Token: 0x0400151D RID: 5405
		Afraid,
		// Token: 0x0400151E RID: 5406
		Death,
		// Token: 0x0400151F RID: 5407
		Chase,
		// Token: 0x04001520 RID: 5408
		ChaseClose
	}
}
