using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public class ProtectionTakeDamage : TakeDamage
{
	// Token: 0x060029A8 RID: 10664 RVA: 0x000A34BC File Offset: 0x000A16BC
	private void InitializeArmorValues()
	{
		this._armorValues = new DamageTypeList(this._startArmorValues);
		this.initializedArmor = true;
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x000A34D8 File Offset: 0x000A16D8
	protected new void Awake()
	{
		if (!this.initializedArmor)
		{
			this.InitializeArmorValues();
		}
		base.Awake();
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x000A34F4 File Offset: 0x000A16F4
	protected sealed override void ApplyDamageTypeList(ref DamageEvent damage, DamageTypeList damageTypes)
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

	// Token: 0x060029AB RID: 10667 RVA: 0x000A3574 File Offset: 0x000A1774
	public virtual void SetArmorValues(DamageTypeList armor)
	{
		if (!this.initializedArmor)
		{
			this._armorValues = new DamageTypeList(armor);
			this.initializedArmor = true;
		}
		else
		{
			this._armorValues.SetArmorValues(armor);
		}
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x000A35A8 File Offset: 0x000A17A8
	public DamageTypeList GetArmorValues()
	{
		return this._armorValues;
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x000A35B0 File Offset: 0x000A17B0
	public virtual float GetArmorValue(int index)
	{
		return this._armorValues[index];
	}

	// Token: 0x0400158D RID: 5517
	protected const float _maxArmorValue = 200f;

	// Token: 0x0400158E RID: 5518
	[SerializeField]
	private DamageTypeList _startArmorValues;

	// Token: 0x0400158F RID: 5519
	protected DamageTypeList _armorValues;

	// Token: 0x04001590 RID: 5520
	private bool initializedArmor;
}
