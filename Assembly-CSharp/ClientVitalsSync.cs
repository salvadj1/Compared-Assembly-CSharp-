using System;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200009A RID: 154
public sealed class ClientVitalsSync : IDLocalCharacterAddon, IInterpTimedEventReceiver
{
	// Token: 0x06000333 RID: 819 RVA: 0x0000FEEC File Offset: 0x0000E0EC
	public ClientVitalsSync() : this(IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake | IDLocalCharacterAddon.AddonFlags.PrerequisitCheck)
	{
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
	protected ClientVitalsSync(IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0000FF04 File Offset: 0x0000E104
	void IInterpTimedEventReceiver.OnInterpTimedEvent()
	{
		string tag = InterpTimedEvent.Tag;
		if (tag != null)
		{
			if (ClientVitalsSync.<>f__switch$map2 == null)
			{
				ClientVitalsSync.<>f__switch$map2 = new Dictionary<string, int>(1)
				{
					{
						"DMG",
						0
					}
				};
			}
			int num;
			if (ClientVitalsSync.<>f__switch$map2.TryGetValue(tag, out num))
			{
				if (num == 0)
				{
					this.ClientHealthChange(InterpTimedEvent.Argument<float>(0), InterpTimedEvent.Argument<GameObject>(1));
					return;
				}
			}
		}
		InterpTimedEvent.MarkUnhandled();
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0000FF80 File Offset: 0x0000E180
	protected override bool CheckPrerequesits()
	{
		this.humanBodyTakeDamage = (base.takeDamage as HumanBodyTakeDamage);
		return this.humanBodyTakeDamage && base.networkViewOwner.isClient;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
	protected override void OnAddonPostAwake()
	{
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000338 RID: 824 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	public bool bleeding
	{
		get
		{
			return this.humanBodyTakeDamage && this.humanBodyTakeDamage.IsBleeding();
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
	[RPC]
	public void Local_HealthChange(float amount, NetworkViewID attackerID, NetworkMessageInfo info)
	{
		NetworkView networkView;
		GameObject gameObject;
		if (attackerID != NetworkViewID.unassigned && (networkView = NetworkView.Find(attackerID)))
		{
			gameObject = networkView.gameObject;
		}
		else
		{
			gameObject = null;
		}
		InterpTimedEvent.Queue(this, "DMG", ref info, new object[]
		{
			amount,
			gameObject
		});
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00010044 File Offset: 0x0000E244
	[RPC]
	public void Local_BleedChange(float amount)
	{
		if (this.humanBodyTakeDamage)
		{
			this.humanBodyTakeDamage._bleedingLevel = amount;
		}
		if (base.localControlled)
		{
			RPOS.SetPlaqueActive("PlaqueBleeding", this.humanBodyTakeDamage._bleedingLevel > 0f);
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00010094 File Offset: 0x0000E294
	public void ClientHealthChange(float amount, GameObject attacker)
	{
		float health = base.health;
		base.AdjustClientSideHealth(amount);
		float num = Mathf.Abs(amount - health);
		bool flag = amount < health;
		float healthFraction = base.healthFraction;
		if (base.localControlled && num >= 1f)
		{
			base.GetComponent<LocalDamageDisplay>().SetNewHealthPercent(healthFraction, attacker);
		}
		if (attacker && flag && num >= 1f && (ClientVitalsSync.hudDamagePrefab || Bundling.Load<HUDDirectionalDamage>("content/hud/DirectionalDamage", out ClientVitalsSync.hudDamagePrefab)))
		{
			Character character;
			Vector3 worldDamageDirection;
			if (IDBase.GetMain<Character>(attacker, ref character))
			{
				worldDamageDirection = base.eyesOrigin - character.eyesOrigin;
			}
			else
			{
				worldDamageDirection = base.origin - attacker.transform.position;
			}
			HUDDirectionalDamage.CreateIndicator(worldDamageDirection, (double)amount, NetCull.time, 1.6000000238418579, ClientVitalsSync.hudDamagePrefab);
		}
		RPOS.HealthUpdate(amount);
	}

	// Token: 0x040002B3 RID: 691
	private const IDLocalCharacterAddon.AddonFlags ClientVitalsSyncAddonFlags = IDLocalCharacterAddon.AddonFlags.FireOnAddonPostAwake | IDLocalCharacterAddon.AddonFlags.PrerequisitCheck;

	// Token: 0x040002B4 RID: 692
	[NonSerialized]
	private HumanBodyTakeDamage humanBodyTakeDamage;

	// Token: 0x040002B5 RID: 693
	private static HUDDirectionalDamage hudDamagePrefab;
}
