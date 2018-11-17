using System;
using UnityEngine;

// Token: 0x0200054B RID: 1355
public class ProtectionTakeDamage : global::TakeDamage
{
	// Token: 0x06002D5A RID: 11610 RVA: 0x000AB254 File Offset: 0x000A9454
	private void InitializeArmorValues()
	{
		this._armorValues = new global::DamageTypeList(this._startArmorValues);
		this.initializedArmor = true;
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x000AB270 File Offset: 0x000A9470
	protected new void Awake()
	{
		if (!this.initializedArmor)
		{
			this.InitializeArmorValues();
		}
		base.Awake();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x000AB28C File Offset: 0x000A948C
	protected sealed override void ApplyDamageTypeList(ref global::DamageEvent damage, global::DamageTypeList damageTypes)
	{
		for (int i = 0; i < 6; i++)
		{
			if (this._armorValues[i] > 0f && damageTypes[i] > 0f)
			{
				int index2;
				int index = index2 = i;
				float num = damageTypes[index2];
				damageTypes[index] = num * Mathf.Clamp01(1f - this._armorValues[i] / 200f);
			}
		}
		base.ApplyDamageTypeList(ref damage, damageTypes);
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x000AB30C File Offset: 0x000A950C
	public virtual void SetArmorValues(global::DamageTypeList armor)
	{
		if (!this.initializedArmor)
		{
			this._armorValues = new global::DamageTypeList(armor);
			this.initializedArmor = true;
		}
		else
		{
			this._armorValues.SetArmorValues(armor);
		}
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x000AB340 File Offset: 0x000A9540
	public global::DamageTypeList GetArmorValues()
	{
		return this._armorValues;
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x000AB348 File Offset: 0x000A9548
	public virtual float GetArmorValue(int index)
	{
		return this._armorValues[index];
	}

	// Token: 0x0400174A RID: 5962
	protected const float _maxArmorValue = 200f;

	// Token: 0x0400174B RID: 5963
	[SerializeField]
	private global::DamageTypeList _startArmorValues;

	// Token: 0x0400174C RID: 5964
	protected global::DamageTypeList _armorValues;

	// Token: 0x0400174D RID: 5965
	private bool initializedArmor;
}
