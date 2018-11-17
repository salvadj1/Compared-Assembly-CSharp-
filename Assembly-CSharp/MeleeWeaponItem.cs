using System;
using UnityEngine;

// Token: 0x020005DF RID: 1503
public abstract class MeleeWeaponItem<T> : WeaponItem<T> where T : MeleeWeaponDataBlock
{
	// Token: 0x06003604 RID: 13828 RVA: 0x000C3630 File Offset: 0x000C1830
	protected MeleeWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000ABB RID: 2747
	// (get) Token: 0x06003605 RID: 13829 RVA: 0x000C3650 File Offset: 0x000C1850
	// (set) Token: 0x06003606 RID: 13830 RVA: 0x000C3658 File Offset: 0x000C1858
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

	// Token: 0x17000ABC RID: 2748
	// (get) Token: 0x06003607 RID: 13831 RVA: 0x000C3664 File Offset: 0x000C1864
	// (set) Token: 0x06003608 RID: 13832 RVA: 0x000C366C File Offset: 0x000C186C
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

	// Token: 0x06003609 RID: 13833 RVA: 0x000C3678 File Offset: 0x000C1878
	public override void PrimaryAttack(ref HumanController.InputSample sample)
	{
		float num = this.datablock.fireRate;
		Metabolism local = base.inventory.GetLocal<Metabolism>();
		if (local && local.GetCalorieLevel() <= 0f)
		{
			num = this.datablock.fireRate * 2f;
		}
		float num2 = Time.time + num;
		base.nextSecondaryAttackTime = num2;
		base.nextPrimaryAttackTime = num2;
		T datablock = this.datablock;
		datablock.Local_FireWeapon(base.viewModelInstance, base.itemRepresentation, this.iface as IMeleeWeaponItem, ref sample);
	}

	// Token: 0x0600360A RID: 13834 RVA: 0x000C3718 File Offset: 0x000C1918
	public override void SecondaryAttack(ref HumanController.InputSample sample)
	{
		float num = Time.time + this.datablock.fireRate;
		base.nextPrimaryAttackTime = num;
		base.nextSecondaryAttackTime = num;
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x000C374C File Offset: 0x000C194C
	public virtual void QueueMidSwing(float time)
	{
		this.queuedSwingAttackTime = time;
	}

	// Token: 0x0600360C RID: 13836 RVA: 0x000C3758 File Offset: 0x000C1958
	public virtual void QueueSwingSound(float time)
	{
		this.queuedSwingSoundTime = time;
	}

	// Token: 0x0600360D RID: 13837 RVA: 0x000C3764 File Offset: 0x000C1964
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.queuedSwingAttackTime > 0f && this.queuedSwingAttackTime < Time.time)
		{
			T datablock = this.datablock;
			datablock.Local_MidSwing(base.viewModelInstance, base.itemRepresentation, this.iface as IMeleeWeaponItem, ref sample);
			this.queuedSwingAttackTime = -1f;
		}
		if (this.queuedSwingSoundTime > 0f && this.queuedSwingSoundTime < Time.time)
		{
			T datablock2 = this.datablock;
			datablock2.SwingSound();
			this.queuedSwingSoundTime = -1f;
		}
	}

	// Token: 0x0600360E RID: 13838 RVA: 0x000C3810 File Offset: 0x000C1A10
	protected override void OnSetActive(bool isActive)
	{
		this.queuedSwingSoundTime = -1f;
		this.queuedSwingAttackTime = -1f;
		base.OnSetActive(isActive);
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x000C3830 File Offset: 0x000C1A30
	protected override bool CanSetActivate(bool wantsTrue)
	{
		return base.CanSetActivate(wantsTrue) && (wantsTrue || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x04001A9E RID: 6814
	private float _queuedSwingAttackTime = -1f;

	// Token: 0x04001A9F RID: 6815
	private float _queuedSwingSoundTime = -1f;
}
