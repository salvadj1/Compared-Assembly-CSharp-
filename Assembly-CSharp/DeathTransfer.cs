using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x0200009C RID: 156
public sealed class DeathTransfer : IDLocalCharacterAddon, IInterpTimedEventReceiver
{
	// Token: 0x06000344 RID: 836 RVA: 0x00010398 File Offset: 0x0000E598
	public DeathTransfer() : this((IDLocalCharacterAddon.AddonFlags)0)
	{
	}

	// Token: 0x06000345 RID: 837 RVA: 0x000103A4 File Offset: 0x0000E5A4
	protected DeathTransfer(IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x06000346 RID: 838 RVA: 0x000103B0 File Offset: 0x0000E5B0
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (DeathTransfer.<>f__switch$map3 == null)
			{
				DeathTransfer.<>f__switch$map3 = new Dictionary<string, int>(2)
				{
					{
						"ClientLocalDeath",
						0
					},
					{
						"RAG",
						1
					}
				};
			}
			int num;
			if (DeathTransfer.<>f__switch$map3.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					try
					{
						this.ClientLocalDeath();
					}
					finally
					{
						if (!base.localControlled)
						{
							Object.Destroy(base.gameObject);
						}
					}
					return;
				}
				if (num == 1)
				{
					try
					{
						this.DeathRagdoll();
					}
					finally
					{
						Object.Destroy(base.gameObject);
					}
					return;
				}
			}
		}
		InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x06000347 RID: 839 RVA: 0x000104A0 File Offset: 0x0000E6A0
	private Ragdoll CreateRagdoll()
	{
		CharacterRagdollTrait trait = base.GetTrait<CharacterRagdollTrait>();
		if (trait)
		{
			GameObject gameObject = Object.Instantiate(trait.ragdollPrefab, base.transform.position, base.transform.rotation) as GameObject;
			Ragdoll component = gameObject.GetComponent<Ragdoll>();
			component.sourceMain = base.idMain;
			this._ragdollInstance = gameObject;
			Object.Destroy(gameObject, 80f);
			this.deathShot.LinkRagdoll(base.transform, gameObject);
			ArmorModelRenderer local = base.GetLocal<ArmorModelRenderer>();
			ArmorModelRenderer local2 = component.GetLocal<ArmorModelRenderer>();
			if (local && local2)
			{
				local2.BindArmorModels(local.GetArmorModelMemberMapCopy());
			}
			return component;
		}
		return null;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00010554 File Offset: 0x0000E754
	private Ragdoll DeathRagdoll()
	{
		Ragdoll result = this.CreateRagdoll();
		PlayerProxyTest component = base.GetComponent<PlayerProxyTest>();
		if (component.body)
		{
			component.body.SetActive(false);
		}
		return result;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0001058C File Offset: 0x0000E78C
	private void Client_OnKilledShared(bool killedBy, Character attacker, ref NetworkMessageInfo info)
	{
		AudioClipArray death = base.GetTrait<CharacterSoundsTrait>().death;
		AudioClip clip = death[Random.Range(0, death.Length)];
		clip.Play(base.transform.position, 1f, 1f, 10f);
		bool localControlled = base.localControlled;
		if (localControlled)
		{
			base.rposLimitFlags = (RPOSLimitFlags.KeepOff | RPOSLimitFlags.HideInventory | RPOSLimitFlags.HideContext | RPOSLimitFlags.HideSprites);
			DeathScreen.Show();
		}
		BaseHitBox remote = base.idMain.GetRemote<BaseHitBox>();
		if (remote)
		{
			remote.collider.enabled = false;
		}
		bool flag;
		if (killedBy && attacker)
		{
			Controllable controllable = attacker.controllable;
			flag = (controllable && controllable.localPlayerControlled);
		}
		else
		{
			flag = false;
		}
		base.AdjustClientSideHealth(0f);
		if (!localControlled && !flag)
		{
			foreach (Collider collider in base.GetComponentsInChildren<Collider>())
			{
				if (collider)
				{
					collider.enabled = false;
				}
			}
			InterpTimedEvent.Queue(this, "RAG", ref info);
			NetCull.DontDestroyWithNetwork(this);
		}
		else
		{
			InterpTimedEvent.Queue(this, "ClientLocalDeath", ref info);
			if (localControlled)
			{
				InterpTimedEvent.Clear(true);
			}
			else
			{
				InterpTimedEvent.Remove(this, true);
			}
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000106E4 File Offset: 0x0000E8E4
	private void ClientLocalDeath()
	{
		Ragdoll ragdoll = this.DeathRagdoll();
		if (base.localControlled)
		{
			if (!actor.forceThirdPerson)
			{
				CameraMount componentInChildren = base.GetComponentInChildren<CameraMount>();
				if (componentInChildren && componentInChildren.open)
				{
					RagdollTransferInfoProvider ragdollTransferInfoProvider = base.controller as RagdollTransferInfoProvider;
					Transform transform;
					bool flag;
					if (ragdollTransferInfoProvider != null)
					{
						try
						{
							flag = ragdollTransferInfoProvider.RagdollTransferInfo.FindHead(ragdoll.transform, out transform);
						}
						catch (Exception ex)
						{
							Debug.LogException(ex, this);
							transform = null;
							flag = false;
						}
					}
					else
					{
						transform = null;
						flag = false;
					}
					if (flag)
					{
						Vector3 vector = transform.InverseTransformPoint(componentInChildren.transform.position);
						vector.y += 0.08f;
						Vector3 vector2 = transform.TransformPoint(vector);
						ragdoll.origin += vector2 - transform.position;
						CameraMount cameraMount = CameraMount.CreateTemporaryCameraMount(componentInChildren, transform);
						cameraMount.camera.nearClipPlane = 0.02f;
					}
					ArmorModelRenderer local = ragdoll.GetLocal<ArmorModelRenderer>();
					if (local)
					{
						local.enabled = false;
					}
				}
				else
				{
					Debug.Log("No camera?");
				}
			}
			Object.Destroy(base.GetComponent<LocalDamageDisplay>());
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00010838 File Offset: 0x0000EA38
	private void Client_ShotShared(ref Vector3 point, ref Angle2 normal, byte bodyPart, ref NetworkMessageInfo info)
	{
		this.deathShot.Set(base.idMain.hitBoxSystem, ref point, ref normal, bodyPart, ref info);
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00010860 File Offset: 0x0000EA60
	[RPC]
	protected void Client_OnKilled(NetworkMessageInfo info)
	{
		this.Client_OnKilledShared(false, null, ref info);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001086C File Offset: 0x0000EA6C
	[RPC]
	protected void Client_OnKilledBy(NetworkViewID attackerViewID, NetworkMessageInfo info)
	{
		NetworkView networkView = NetworkView.Find(attackerViewID);
		if (!networkView)
		{
			this.Client_OnKilledShared(false, null, ref info);
		}
		else
		{
			Character component = networkView.GetComponent<Character>();
			this.Client_OnKilledShared(component, component, ref info);
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x000108B0 File Offset: 0x0000EAB0
	[RPC]
	protected void Client_OnKilledShot(Vector3 point, Angle2 normal, byte bodyPart, NetworkMessageInfo info)
	{
		this.Client_ShotShared(ref point, ref normal, bodyPart, ref info);
		this.Client_OnKilled(info);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x000108C8 File Offset: 0x0000EAC8
	[RPC]
	protected void Client_OnKilledShotBy(NetworkViewID attackerViewID, Vector3 point, Angle2 normal, byte bodyPart, NetworkMessageInfo info)
	{
		this.Client_ShotShared(ref point, ref normal, bodyPart, ref info);
		this.Client_OnKilledBy(attackerViewID, info);
	}

	// Token: 0x040002BC RID: 700
	private const IDLocalCharacterAddon.AddonFlags DeathTransferAddonFlags = (IDLocalCharacterAddon.AddonFlags)0;

	// Token: 0x040002BD RID: 701
	[NonSerialized]
	private QueuedShotDeathInfo deathShot;

	// Token: 0x040002BE RID: 702
	[NonSerialized]
	private GameObject _ragdollInstance;
}
