using System;
using RustProto;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class HumanBodyTakeDamage : global::ProtectionTakeDamage
{
	// Token: 0x06002D1E RID: 11550 RVA: 0x000A8A88 File Offset: 0x000A6C88
	protected new void Awake()
	{
		base.Awake();
		this.checkLevelInterval = 1f;
		base.InvokeRepeating("CheckLevels", this.checkLevelInterval, this.checkLevelInterval);
		this._playerInv = base.GetComponent<global::PlayerInventory>();
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x000A8ACC File Offset: 0x000A6CCC
	public void CheckLevels()
	{
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x000A8AD0 File Offset: 0x000A6CD0
	public virtual bool IsBleeding()
	{
		return this._bleedingLevel > 0f;
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x000A8AE0 File Offset: 0x000A6CE0
	public void AddBleedingLevel(float lvl)
	{
		this.SetBleedingLevel(this._bleedingLevel + lvl);
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x000A8AF0 File Offset: 0x000A6CF0
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

	// Token: 0x06002D23 RID: 11555 RVA: 0x000A8B58 File Offset: 0x000A6D58
	public void Bandage(float amountToRestore)
	{
		this.SetBleedingLevel(Mathf.Clamp(this._bleedingLevel - amountToRestore, 0f, this._bleedingLevelMax));
		if (this._bleedingLevel <= 0f)
		{
			base.CancelInvoke("DoBleed");
		}
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x000A8B94 File Offset: 0x000A6D94
	public void DoBleed()
	{
		if (base.alive && this._bleedingLevel > 0f)
		{
			float healthPoints = this._bleedingLevel;
			global::Metabolism component = base.GetComponent<global::Metabolism>();
			if (component)
			{
				healthPoints = this._bleedingLevel * ((!component.IsWarm()) ? 1f : 0.4f);
			}
			global::LifeStatus lifeStatus;
			if (this.bleedAttacker && this.bleedingID)
			{
				lifeStatus = global::TakeDamage.Hurt(this.bleedAttacker, this.bleedingID, healthPoints, null);
			}
			else
			{
				lifeStatus = global::TakeDamage.HurtSelf(this.idMain, healthPoints, null);
			}
			if (lifeStatus == global::LifeStatus.IsAlive)
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

	// Token: 0x06002D25 RID: 11557 RVA: 0x000A8C94 File Offset: 0x000A6E94
	protected override global::LifeStatus Hurt(ref global::DamageEvent damage)
	{
		global::LifeStatus lifeStatus = base.Hurt(ref damage);
		bool flag = (damage.damageTypes & (global::DamageTypeFlags.damage_bullet | global::DamageTypeFlags.damage_melee | global::DamageTypeFlags.damage_explosion)) != (global::DamageTypeFlags)0;
		if (flag)
		{
			this._healOverTime = 0f;
		}
		if (lifeStatus == global::LifeStatus.WasKilled)
		{
			base.CancelInvoke("DoBleed");
		}
		else if (lifeStatus == global::LifeStatus.IsAlive && base.healthLossFraction > 0.2f)
		{
			float num = damage.amount / base.maxHealth;
			if ((damage.damageTypes & (global::DamageTypeFlags.damage_bullet | global::DamageTypeFlags.damage_melee)) != (global::DamageTypeFlags)0 && damage.amount > base.maxHealth * 0.05f)
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

	// Token: 0x06002D26 RID: 11558 RVA: 0x000A8DC0 File Offset: 0x000A6FC0
	public virtual void HealOverTime(float amountToHeal)
	{
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x000A8DC4 File Offset: 0x000A6FC4
	public override void ServerFrame()
	{
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x000A8DC8 File Offset: 0x000A6FC8
	public override void SaveVitals(ref RustProto.Vitals.Builder vitals)
	{
		base.SaveVitals(ref vitals);
		vitals.SetBleedSpeed(this._bleedingLevel);
		vitals.SetHealSpeed(this._healOverTime);
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x000A8DF0 File Offset: 0x000A6FF0
	public override void LoadVitals(RustProto.Vitals vitals)
	{
		base.LoadVitals(vitals);
		this._bleedingLevel = vitals.BleedSpeed;
		this._healOverTime = vitals.HealSpeed;
	}

	// Token: 0x040016E7 RID: 5863
	private const string CheckLevelMethodName = "CheckLevels";

	// Token: 0x040016E8 RID: 5864
	private const string DoBleedMethodName = "DoBleed";

	// Token: 0x040016E9 RID: 5865
	public float _bleedInterval = 10f;

	// Token: 0x040016EA RID: 5866
	public float _bleedingLevel;

	// Token: 0x040016EB RID: 5867
	public float _bleedingLevelMax = 100f;

	// Token: 0x040016EC RID: 5868
	private float lastBleedTime;

	// Token: 0x040016ED RID: 5869
	public float checkLevelInterval = 2f;

	// Token: 0x040016EE RID: 5870
	private IDBase bleedAttacker;

	// Token: 0x040016EF RID: 5871
	private IDBase bleedingID;

	// Token: 0x040016F0 RID: 5872
	private float _healOverTime;

	// Token: 0x040016F1 RID: 5873
	private float _lastLevelCheckTime;

	// Token: 0x040016F2 RID: 5874
	private global::PlayerInventory _playerInv;
}
