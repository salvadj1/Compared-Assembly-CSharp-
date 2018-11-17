using System;
using RustProto;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class HumanBodyTakeDamage : ProtectionTakeDamage
{
	// Token: 0x0600296C RID: 10604 RVA: 0x000A266C File Offset: 0x000A086C
	protected new void Awake()
	{
		base.Awake();
		this.checkLevelInterval = 1f;
		base.InvokeRepeating("CheckLevels", this.checkLevelInterval, this.checkLevelInterval);
		this._playerInv = base.GetComponent<PlayerInventory>();
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x000A26B0 File Offset: 0x000A08B0
	public void CheckLevels()
	{
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x000A26B4 File Offset: 0x000A08B4
	public virtual bool IsBleeding()
	{
		return this._bleedingLevel > 0f;
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x000A26C4 File Offset: 0x000A08C4
	public void AddBleedingLevel(float lvl)
	{
		this.SetBleedingLevel(this._bleedingLevel + lvl);
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x000A26D4 File Offset: 0x000A08D4
	public void SetBleedingLevel(float lvl)
	{
		this._bleedingLevel = lvl;
		if (this._bleedingLevel > 0f)
		{
			base.CancelInvoke("DoBleed");
			base.InvokeRepeating("DoBleed", this._bleedInterval, this._bleedInterval);
		}
		else
		{
			base.CancelInvoke("DoBleed");
		}
		base.SendMessage("BleedingLevelChanged", lvl, 1);
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x000A273C File Offset: 0x000A093C
	public void Bandage(float amountToRestore)
	{
		this.SetBleedingLevel(Mathf.Clamp(this._bleedingLevel - amountToRestore, 0f, this._bleedingLevelMax));
		if (this._bleedingLevel <= 0f)
		{
			base.CancelInvoke("DoBleed");
		}
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000A2778 File Offset: 0x000A0978
	public void DoBleed()
	{
		if (base.alive && this._bleedingLevel > 0f)
		{
			float healthPoints = this._bleedingLevel;
			Metabolism component = base.GetComponent<Metabolism>();
			if (component)
			{
				healthPoints = this._bleedingLevel * ((!component.IsWarm()) ? 1f : 0.4f);
			}
			LifeStatus lifeStatus;
			if (this.bleedAttacker && this.bleedingID)
			{
				lifeStatus = TakeDamage.Hurt(this.bleedAttacker, this.bleedingID, healthPoints, null);
			}
			else
			{
				lifeStatus = TakeDamage.HurtSelf(this.idMain, healthPoints, null);
			}
			if (lifeStatus == LifeStatus.IsAlive)
			{
				float num = 0.2f;
				this.SetBleedingLevel(Mathf.Clamp(this._bleedingLevel - num, 0f, this._bleedingLevel));
			}
			else
			{
				base.CancelInvoke("DoBleed");
			}
		}
		else
		{
			base.CancelInvoke("DoBleed");
		}
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x000A2878 File Offset: 0x000A0A78
	protected override LifeStatus Hurt(ref DamageEvent damage)
	{
		LifeStatus lifeStatus = base.Hurt(ref damage);
		bool flag = (damage.damageTypes & (DamageTypeFlags.damage_bullet | DamageTypeFlags.damage_melee | DamageTypeFlags.damage_explosion)) != (DamageTypeFlags)0;
		if (flag)
		{
			this._healOverTime = 0f;
		}
		if (lifeStatus == LifeStatus.WasKilled)
		{
			base.CancelInvoke("DoBleed");
		}
		else if (lifeStatus == LifeStatus.IsAlive && base.healthLossFraction > 0.2f)
		{
			float num = damage.amount / base.maxHealth;
			if ((damage.damageTypes & (DamageTypeFlags.damage_bullet | DamageTypeFlags.damage_melee)) != (DamageTypeFlags)0 && damage.amount > base.maxHealth * 0.05f)
			{
				int num2 = 0;
				if (num >= 0.25f)
				{
					num2 = 1;
				}
				else if (num >= 0.15f)
				{
					num2 = 2;
				}
				else if (num >= 0.05f)
				{
					num2 = 3;
				}
				bool flag2 = Random.Range(0, num2) == 1 || num2 == 1;
				if (flag2)
				{
					this.AddBleedingLevel(Mathf.Clamp(damage.amount * 0.15f, 1f, base.maxHealth));
					this.bleedAttacker = damage.attacker.id;
					this.bleedingID = damage.victim.id;
				}
			}
		}
		return lifeStatus;
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x000A29A4 File Offset: 0x000A0BA4
	public virtual void HealOverTime(float amountToHeal)
	{
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x000A29A8 File Offset: 0x000A0BA8
	public override void ServerFrame()
	{
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000A29AC File Offset: 0x000A0BAC
	public override void SaveVitals(ref Vitals.Builder vitals)
	{
		base.SaveVitals(ref vitals);
		vitals.SetBleedSpeed(this._bleedingLevel);
		vitals.SetHealSpeed(this._healOverTime);
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x000A29D4 File Offset: 0x000A0BD4
	public override void LoadVitals(Vitals vitals)
	{
		base.LoadVitals(vitals);
		this._bleedingLevel = vitals.BleedSpeed;
		this._healOverTime = vitals.HealSpeed;
	}

	// Token: 0x04001550 RID: 5456
	private const string CheckLevelMethodName = "CheckLevels";

	// Token: 0x04001551 RID: 5457
	private const string DoBleedMethodName = "DoBleed";

	// Token: 0x04001552 RID: 5458
	public float _bleedInterval = 10f;

	// Token: 0x04001553 RID: 5459
	public float _bleedingLevel;

	// Token: 0x04001554 RID: 5460
	public float _bleedingLevelMax = 100f;

	// Token: 0x04001555 RID: 5461
	private float lastBleedTime;

	// Token: 0x04001556 RID: 5462
	public float checkLevelInterval = 2f;

	// Token: 0x04001557 RID: 5463
	private IDBase bleedAttacker;

	// Token: 0x04001558 RID: 5464
	private IDBase bleedingID;

	// Token: 0x04001559 RID: 5465
	private float _healOverTime;

	// Token: 0x0400155A RID: 5466
	private float _lastLevelCheckTime;

	// Token: 0x0400155B RID: 5467
	private PlayerInventory _playerInv;
}
