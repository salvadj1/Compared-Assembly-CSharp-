using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class BasicWildLifeAI : NetBehaviour, IInterpTimedEventReceiver
{
	// Token: 0x060027A9 RID: 10153 RVA: 0x0009AC40 File Offset: 0x00098E40
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		if (!this.OnInterpTimedEvent())
		{
			InterpTimedEvent.MarkUnhandled();
		}
	}

	// Token: 0x17000917 RID: 2327
	// (get) Token: 0x060027AA RID: 10154 RVA: 0x0009AC60 File Offset: 0x00098E60
	public Transform transform
	{
		get
		{
			return this._myTransform;
		}
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x0009AC68 File Offset: 0x00098E68
	protected float GetWalkAnimScalar()
	{
		return this.walkAnimScalar;
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x0009AC70 File Offset: 0x00098E70
	protected float GetRunAnimScalar()
	{
		return this.runAnimScalar;
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x0009AC78 File Offset: 0x00098E78
	protected float GetMoveSpeedForAnim()
	{
		Vector3 vector;
		this._interp.SampleWorldVelocity(out vector);
		return vector.magnitude;
	}

	// Token: 0x060027AE RID: 10158 RVA: 0x0009AC9C File Offset: 0x00098E9C
	public virtual string GetDeathAnim()
	{
		return "death";
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x0009ACA4 File Offset: 0x00098EA4
	protected void DoClientDeath()
	{
		base.animation[this.GetDeathAnim()].wrapMode = 8;
		base.animation.CrossFade(this.GetDeathAnim(), 0.2f);
		this._takeDamage.health = 0f;
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x0009ACF0 File Offset: 0x00098EF0
	protected void OnClientDeath(ref Vector3 deathPosition, NetworkViewID attackerNetViewID, ref NetworkMessageInfo info)
	{
		Vector3 vector;
		Vector3 up;
		TransformHelpers.GetGroundInfo(deathPosition + new Vector3(0f, 0.25f, 0f), 10f, out vector, out up);
		deathPosition = vector;
		Quaternion rot = TransformHelpers.LookRotationForcedUp(this._myTransform.rotation * Vector3.forward, up);
		this._interp.SetGoals(deathPosition, rot, info.timestamp);
		if (attackerNetViewID.isMine)
		{
			this.DoClientDeath();
		}
		else
		{
			InterpTimedEvent.Queue(this, "DEATH", ref info);
		}
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x0009AD8C File Offset: 0x00098F8C
	protected void OnNetworkUpdate(ref Vector3 origin, ref Quaternion rotation, ref NetworkMessageInfo info)
	{
		this._wildMove.ProcessNetworkUpdate(ref origin, ref rotation);
		this._interp.SetGoals(origin, rotation, info.timestamp);
	}

	// Token: 0x060027B2 RID: 10162 RVA: 0x0009ADC4 File Offset: 0x00098FC4
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

	// Token: 0x060027B3 RID: 10163 RVA: 0x0009AEEC File Offset: 0x000990EC
	protected void Awake()
	{
		this._myTransform = base.transform;
		this._takeDamage = base.GetComponent<TakeDamage>();
		this._wildMove = base.GetComponent<BaseAIMovement>();
		this._interp = base.GetComponent<TransformInterpolator>();
		Object.Destroy(base.GetComponent<BasicWildLifeMovement>());
		Object.Destroy(base.GetComponent<VisNode>());
		this._takeDamage.enabled = false;
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x0009AF4C File Offset: 0x0009914C
	protected void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		this._interp.running = true;
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x0009AF5C File Offset: 0x0009915C
	protected void OnDestroy()
	{
		InterpTimedEvent.Remove(this);
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x0009AF64 File Offset: 0x00099164
	[RPC]
	protected void GetNetworkUpdate(Vector3 pos, Angle2 rot, NetworkMessageInfo info)
	{
		Quaternion quaternion = (Quaternion)rot;
		this.OnNetworkUpdate(ref pos, ref quaternion, ref info);
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x0009AF84 File Offset: 0x00099184
	[RPC]
	protected void Snd(byte type, NetworkMessageInfo info)
	{
		try
		{
			InterpTimedEvent.Queue(this, "SOUND", ref info, new object[]
			{
				(int)type
			});
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			Debug.LogWarning("Running emergency dump because of previous exception in Snd", this);
			InterpTimedEvent.EMERGENCY_DUMP(true);
		}
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x0009AFEC File Offset: 0x000991EC
	[RPC]
	protected void ClientHealthChange(float newHealth)
	{
		if (this._takeDamage.health > newHealth)
		{
		}
		this._takeDamage.health = newHealth;
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x0009B00C File Offset: 0x0009920C
	[RPC]
	protected void ClientDeath(Vector3 deadPos, NetworkViewID attackerID, NetworkMessageInfo info)
	{
		this.OnClientDeath(ref deadPos, attackerID, ref info);
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x0009B01C File Offset: 0x0009921C
	protected virtual bool OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (BasicWildLifeAI.<>f__switch$map7 == null)
			{
				BasicWildLifeAI.<>f__switch$map7 = new Dictionary<string, int>(2)
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
			if (BasicWildLifeAI.<>f__switch$map7.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.DoClientDeath();
					return true;
				}
				if (num == 1)
				{
					this.PlaySnd(InterpTimedEvent.Argument<int>(0));
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04001384 RID: 4996
	private const string RPCName_GetNetworkUpdate = "GetNetworkUpdate";

	// Token: 0x04001385 RID: 4997
	private const string RPCName_Snd = "Snd";

	// Token: 0x04001386 RID: 4998
	private const string RPCName_ClientHealthChange = "ClientHealthChange";

	// Token: 0x04001387 RID: 4999
	private const string RPCName_ClientDeath = "ClientDeath";

	// Token: 0x04001388 RID: 5000
	public bool afraidOfFootsteps = true;

	// Token: 0x04001389 RID: 5001
	public bool afraidOfDanger = true;

	// Token: 0x0400138A RID: 5002
	[SerializeField]
	protected AudioClipArray idleSounds;

	// Token: 0x0400138B RID: 5003
	[SerializeField]
	protected AudioClipArray fleeStartSounds;

	// Token: 0x0400138C RID: 5004
	[SerializeField]
	protected AudioClipArray deathSounds;

	// Token: 0x0400138D RID: 5005
	[SerializeField]
	protected float walkSpeed = 1f;

	// Token: 0x0400138E RID: 5006
	[SerializeField]
	protected float runSpeed = 3f;

	// Token: 0x0400138F RID: 5007
	[SerializeField]
	protected float walkAnimScalar = 1f;

	// Token: 0x04001390 RID: 5008
	[SerializeField]
	protected float runAnimScalar = 1f;

	// Token: 0x04001391 RID: 5009
	protected Transform _myTransform;

	// Token: 0x04001392 RID: 5010
	protected TakeDamage _takeDamage;

	// Token: 0x04001393 RID: 5011
	protected BaseAIMovement _wildMove;

	// Token: 0x04001394 RID: 5012
	protected TransformInterpolator _interp;

	// Token: 0x0200042F RID: 1071
	public enum AISound : byte
	{
		// Token: 0x04001397 RID: 5015
		Idle,
		// Token: 0x04001398 RID: 5016
		Warn,
		// Token: 0x04001399 RID: 5017
		Attack,
		// Token: 0x0400139A RID: 5018
		Afraid,
		// Token: 0x0400139B RID: 5019
		Death,
		// Token: 0x0400139C RID: 5020
		Chase,
		// Token: 0x0400139D RID: 5021
		ChaseClose
	}
}
