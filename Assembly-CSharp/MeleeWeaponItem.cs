using System;
using UnityEngine;

// Token: 0x0200069D RID: 1693
public abstract class MeleeWeaponItem<T> : global::WeaponItem<T> where T : global::MeleeWeaponDataBlock
{
	// Token: 0x060039CC RID: 14796 RVA: 0x000CB88C File Offset: 0x000C9A8C
	protected MeleeWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000B31 RID: 2865
	// (get) Token: 0x060039CD RID: 14797 RVA: 0x000CB8AC File Offset: 0x000C9AAC
	// (set) Token: 0x060039CE RID: 14798 RVA: 0x000CB8B4 File Offset: 0x000C9AB4
	public float queuedSwingAttackTime
	{
		get
		{
			return this._queuedSwingAttackTime;
		}
		set
		{
			this._queuedSwingAttackTime = value;
		}
	}

	// Token: 0x17000B32 RID: 2866
	// (get) Token: 0x060039CF RID: 14799 RVA: 0x000CB8C0 File Offset: 0x000C9AC0
	// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000CB8C8 File Offset: 0x000C9AC8
	public float queuedSwingSoundTime
	{
		get
		{
			return this._queuedSwingSoundTime;
		}
		set
		{
			this._queuedSwingSoundTime = value;
		}
	}

	// Token: 0x060039D1 RID: 14801 RVA: 0x000CB8D4 File Offset: 0x000C9AD4
	public override void PrimaryAttack(ref global::HumanController.InputSample sample)
	{
		float num = this.datablock.fireRate;
		global::Metabolism local = base.inventory.GetLocal<global::Metabolism>();
		if (local && local.GetCalorieLevel() <= 0f)
		{
			num = this.datablock.fireRate * 2f;
		}
		float num2 = Time.time + num;
		base.nextSecondaryAttackTime = num2;
		base.nextPrimaryAttackTime = num2;
		T datablock = this.datablock;
		datablock.Local_FireWeapon(base.viewModelInstance, base.itemRepresentation, this.iface as global::IMeleeWeaponItem, ref sample);
	}

	// Token: 0x060039D2 RID: 14802 RVA: 0x000CB974 File Offset: 0x000C9B74
	public override void SecondaryAttack(ref global::HumanController.InputSample sample)
	{
		float num = Time.time + this.datablock.fireRate;
		base.nextPrimaryAttackTime = num;
		base.nextSecondaryAttackTime = num;
	}

	// Token: 0x060039D3 RID: 14803 RVA: 0x000CB9A8 File Offset: 0x000C9BA8
	public virtual void QueueMidSwing(float time)
	{
		this.queuedSwingAttackTime = time;
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x000CB9B4 File Offset: 0x000C9BB4
	public virtual void QueueSwingSound(float time)
	{
		this.queuedSwingSoundTime = time;
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x000CB9C0 File Offset: 0x000C9BC0
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.queuedSwingAttackTime > 0f && this.queuedSwingAttackTime < Time.time)
		{
			T datablock = this.datablock;
			datablock.Local_MidSwing(base.viewModelInstance, base.itemRepresentation, this.iface as global::IMeleeWeaponItem, ref sample);
			this.queuedSwingAttackTime = -1f;
		}
		if (this.queuedSwingSoundTime > 0f && this.queuedSwingSoundTime < Time.time)
		{
			T datablock2 = this.datablock;
			datablock2.SwingSound();
			this.queuedSwingSoundTime = -1f;
		}
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x000CBA6C File Offset: 0x000C9C6C
	protected override void OnSetActive(bool isActive)
	{
		this.queuedSwingSoundTime = -1f;
		this.queuedSwingAttackTime = -1f;
		base.OnSetActive(isActive);
	}

	// Token: 0x060039D7 RID: 14807 RVA: 0x000CBA8C File Offset: 0x000C9C8C
	protected override bool CanSetActivate(bool wantsTrue)
	{
		return base.CanSetActivate(wantsTrue) && (wantsTrue || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x04001C6F RID: 7279
	private float _queuedSwingAttackTime = -1f;

	// Token: 0x04001C70 RID: 7280
	private float _queuedSwingSoundTime = -1f;
}
