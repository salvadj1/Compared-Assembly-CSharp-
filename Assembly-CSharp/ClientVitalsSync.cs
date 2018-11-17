using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020000AD RID: 173
public sealed class ClientVitalsSync : global::IDLocalCharacterAddon, global::IInterpTimedEventReceiver
{
	// Token: 0x060003AB RID: 939 RVA: 0x000116DC File Offset: 0x0000F8DC
	public ClientVitalsSync() : this(global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake | global::IDLocalCharacterAddon.AddonFlags.PrerequisitCheck)
	{
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000116E8 File Offset: 0x0000F8E8
	protected ClientVitalsSync(global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000116F4 File Offset: 0x0000F8F4
	void global::IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		string tag = global::InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (global::ClientVitalsSync.<>f__switch$map2 == null)
			{
				global::ClientVitalsSync.<>f__switch$map2 = new Dictionary<string, int>(1)
				{
					{
						"DMG",
						0
					}
				};
			}
			int num;
			if (global::ClientVitalsSync.<>f__switch$map2.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.ClientHealthChange(global::InterpTimedEvent.Argument<float>(0), global::InterpTimedEvent.Argument<GameObject>(1));
					return;
				}
			}
		}
		global::InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00011770 File Offset: 0x0000F970
	protected override bool CheckPrerequesits()
	{
		this.humanBodyTakeDamage = (base.takeDamage as global::HumanBodyTakeDamage);
		return this.humanBodyTakeDamage && base.networkViewOwner.isClient;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x000117B0 File Offset: 0x0000F9B0
	protected override void OnAddonPostAwake()
	{
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060003B0 RID: 944 RVA: 0x000117B4 File Offset: 0x0000F9B4
	public bool bleeding
	{
		get
		{
			return this.humanBodyTakeDamage && this.humanBodyTakeDamage.IsBleeding();
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000117D4 File Offset: 0x0000F9D4
	[RPC]
	public void Local_HealthChange(float amount, uLink.NetworkViewID attackerID, uLink.NetworkMessageInfo info)
	{
		uLink.NetworkView networkView;
		GameObject gameObject;
		if (attackerID != uLink.NetworkViewID.unassigned && (networkView = uLink.NetworkView.Find(attackerID)))
		{
			gameObject = networkView.gameObject;
		}
		else
		{
			gameObject = null;
		}
		global::InterpTimedEvent.Queue(this, "DMG", ref info, new object[]
		{
			amount,
			gameObject
		});
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00011834 File Offset: 0x0000FA34
	[RPC]
	public void Local_BleedChange(float amount)
	{
		if (this.humanBodyTakeDamage)
		{
			this.humanBodyTakeDamage._bleedingLevel = amount;
		}
		if (base.localControlled)
		{
			global::RPOS.SetPlaqueActive("PlaqueBleeding", this.humanBodyTakeDamage._bleedingLevel > 0f);
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00011884 File Offset: 0x0000FA84
	public void ClientHealthChange(float amount, GameObject attacker)
	{
		float health = base.health;
		base.AdjustClientSideHealth(amount);
		float num = Mathf.Abs(amount - health);
		bool flag = amount < health;
		float healthFraction = base.healthFraction;
		if (base.localControlled && num >= 1f)
		{
			base.GetComponent<global::LocalDamageDisplay>().SetNewHealthPercent(healthFraction, attacker);
		}
		if (attacker && flag && num >= 1f && (global::ClientVitalsSync.hudDamagePrefab || Facepunch.Bundling.Load<global::HUDDirectionalDamage>("content/hud/DirectionalDamage", out global::ClientVitalsSync.hudDamagePrefab)))
		{
			global::Character character;
			Vector3 worldDamageDirection;
			if (IDBase.GetMain<global::Character>(attacker, ref character))
			{
				worldDamageDirection = base.eyesOrigin - character.eyesOrigin;
			}
			else
			{
				worldDamageDirection = base.origin - attacker.transform.position;
			}
			global::HUDDirectionalDamage.CreateIndicator(worldDamageDirection, (double)amount, global::NetCull.time, 1.6000000238418579, global::ClientVitalsSync.hudDamagePrefab);
		}
		global::RPOS.HealthUpdate(amount);
	}

	// Token: 0x0400031E RID: 798
	private const global::IDLocalCharacterAddon.AddonFlags ClientVitalsSyncAddonFlags = global::IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake | global::IDLocalCharacterAddon.AddonFlags.PrerequisitCheck;

	// Token: 0x0400031F RID: 799
	[NonSerialized]
	private global::HumanBodyTakeDamage humanBodyTakeDamage;

	// Token: 0x04000320 RID: 800
	private static global::HUDDirectionalDamage hudDamagePrefab;
}
