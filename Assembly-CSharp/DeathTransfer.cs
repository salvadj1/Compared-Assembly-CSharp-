using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020000AF RID: 175
public sealed class DeathTransfer : global::IDLocalCharacterAddon, global::IInterpTimedEventReceiver
{
	// Token: 0x060003BC RID: 956 RVA: 0x00011B88 File Offset: 0x0000FD88
	public DeathTransfer() : this((global::IDLocalCharacterAddon.AddonFlags)0)
	{
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00011B94 File Offset: 0x0000FD94
	protected DeathTransfer(global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00011BA0 File Offset: 0x0000FDA0
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::DeathTransfer.<>f__switch$map3 == null)
			{
				global::DeathTransfer.<>f__switch$map3 = new Dictionary<string, int>(2)
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
			if (global::DeathTransfer.<>f__switch$map3.TryGetValue(tag, out num))
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
		global::InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00011C90 File Offset: 0x0000FE90
	private global::Ragdoll CreateRagdoll()
	{
		global::CharacterRagdollTrait trait = base.GetTrait<global::CharacterRagdollTrait>();
		if (trait)
		{
			GameObject gameObject = Object.Instantiate(trait.ragdollPrefab, base.transform.position, base.transform.rotation) as GameObject;
			global::Ragdoll component = gameObject.GetComponent<global::Ragdoll>();
			component.sourceMain = base.idMain;
			this._ragdollInstance = gameObject;
			Object.Destroy(gameObject, 80f);
			this.deathShot.LinkRagdoll(base.transform, gameObject);
			global::ArmorModelRenderer local = base.GetLocal<global::ArmorModelRenderer>();
			global::ArmorModelRenderer local2 = component.GetLocal<global::ArmorModelRenderer>();
			if (local && local2)
			{
				local2.BindArmorModels(local.GetArmorModelMemberMapCopy());
			}
			return component;
		}
		return null;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00011D44 File Offset: 0x0000FF44
	private global::Ragdoll DeathRagdoll()
	{
		global::Ragdoll result = this.CreateRagdoll();
		global::PlayerProxyTest component = base.GetComponent<global::PlayerProxyTest>();
		if (component.body)
		{
			component.body.SetActive(false);
		}
		return result;
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00011D7C File Offset: 0x0000FF7C
	private void Client_OnKilledShared(bool killedBy, global::Character attacker, ref uLink.NetworkMessageInfo info)
	{
		global::AudioClipArray death = base.GetTrait<global::CharacterSoundsTrait>().death;
		AudioClip clip = death[Random.Range(0, death.Length)];
		clip.Play(base.transform.position, 1f, 1f, 10f);
		bool localControlled = base.localControlled;
		if (localControlled)
		{
			base.rposLimitFlags = (global::RPOSLimitFlags.KeepOff | global::RPOSLimitFlags.HideInventory | global::RPOSLimitFlags.HideContext | global::RPOSLimitFlags.HideSprites);
			global::DeathScreen.Show();
		}
		global::BaseHitBox remote = base.idMain.GetRemote<global::BaseHitBox>();
		if (remote)
		{
			remote.collider.enabled = false;
		}
		bool flag;
		if (killedBy && attacker)
		{
			global::Controllable controllable = attacker.controllable;
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
			global::InterpTimedEvent.Queue(this, "RAG", ref info);
			global::NetCull.DontDestroyWithNetwork(this);
		}
		else
		{
			global::InterpTimedEvent.Queue(this, "ClientLocalDeath", ref info);
			if (localControlled)
			{
				global::InterpTimedEvent.Clear(true);
			}
			else
			{
				global::InterpTimedEvent.Remove(this, true);
			}
		}
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00011ED4 File Offset: 0x000100D4
	private void ClientLocalDeath()
	{
		global::Ragdoll ragdoll = this.DeathRagdoll();
		if (base.localControlled)
		{
			if (!global::actor.forceThirdPerson)
			{
				global::CameraMount componentInChildren = base.GetComponentInChildren<global::CameraMount>();
				if (componentInChildren && componentInChildren.open)
				{
					global::RagdollTransferInfoProvider ragdollTransferInfoProvider = base.controller as global::RagdollTransferInfoProvider;
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
						global::CameraMount cameraMount = global::CameraMount.CreateTemporaryCameraMount(componentInChildren, transform);
						cameraMount.camera.nearClipPlane = 0.02f;
					}
					global::ArmorModelRenderer local = ragdoll.GetLocal<global::ArmorModelRenderer>();
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
			Object.Destroy(base.GetComponent<global::LocalDamageDisplay>());
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00012028 File Offset: 0x00010228
	private void Client_ShotShared(ref Vector3 point, ref global::Angle2 normal, byte bodyPart, ref uLink.NetworkMessageInfo info)
	{
		this.deathShot.Set(base.idMain.hitBoxSystem, ref point, ref normal, bodyPart, ref info);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00012050 File Offset: 0x00010250
	[RPC]
	protected void Client_OnKilled(uLink.NetworkMessageInfo info)
	{
		this.Client_OnKilledShared(false, null, ref info);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001205C File Offset: 0x0001025C
	[RPC]
	protected void Client_OnKilledBy(uLink.NetworkViewID attackerViewID, uLink.NetworkMessageInfo info)
	{
		uLink.NetworkView networkView = uLink.NetworkView.Find(attackerViewID);
		if (!networkView)
		{
			this.Client_OnKilledShared(false, null, ref info);
		}
		else
		{
			global::Character component = networkView.GetComponent<global::Character>();
			this.Client_OnKilledShared(component, component, ref info);
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000120A0 File Offset: 0x000102A0
	[RPC]
	protected void Client_OnKilledShot(Vector3 point, global::Angle2 normal, byte bodyPart, uLink.NetworkMessageInfo info)
	{
		this.Client_ShotShared(ref point, ref normal, bodyPart, ref info);
		this.Client_OnKilled(info);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x000120B8 File Offset: 0x000102B8
	[RPC]
	protected void Client_OnKilledShotBy(uLink.NetworkViewID attackerViewID, Vector3 point, global::Angle2 normal, byte bodyPart, uLink.NetworkMessageInfo info)
	{
		this.Client_ShotShared(ref point, ref normal, bodyPart, ref info);
		this.Client_OnKilledBy(attackerViewID, info);
	}

	// Token: 0x04000327 RID: 807
	private const global::IDLocalCharacterAddon.AddonFlags DeathTransferAddonFlags = (global::IDLocalCharacterAddon.AddonFlags)0;

	// Token: 0x04000328 RID: 808
	[NonSerialized]
	private global::QueuedShotDeathInfo deathShot;

	// Token: 0x04000329 RID: 809
	[NonSerialized]
	private GameObject _ragdollInstance;
}
