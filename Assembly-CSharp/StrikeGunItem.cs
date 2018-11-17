using System;
using UnityEngine;

// Token: 0x020006A3 RID: 1699
public abstract class StrikeGunItem<T> : global::BulletWeaponItem<T> where T : global::StrikeGunDataBlock
{
	// Token: 0x060039DD RID: 14813 RVA: 0x000CBC74 File Offset: 0x000C9E74
	protected StrikeGunItem(T db) : base(db)
	{
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x000CBC80 File Offset: 0x000C9E80
	public void ResetFiring()
	{
		this.actualFireTime = 0f;
		this.beganFiring = false;
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x000CBC94 File Offset: 0x000C9E94
	protected override void OnSetActive(bool isActive)
	{
		this.ResetFiring();
		base.OnSetActive(isActive);
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (!sample.attack && this.beganFiring)
		{
			this.CancelAttack(ref sample);
			sample.attack = false;
		}
		if (sample.attack && base.clipAmmo == 0 && this.canReload)
		{
			this.Reload(ref sample);
		}
		if (this.beganFiring && sample.attack && Time.time > this.actualFireTime)
		{
			base.PrimaryAttack(ref sample);
			this.ResetFiring();
		}
	}

	// Token: 0x060039E1 RID: 14817 RVA: 0x000CBD38 File Offset: 0x000C9F38
	public virtual void CancelAttack(ref global::HumanController.InputSample sample)
	{
		if (this.beganFiring)
		{
			global::ViewModel viewModelInstance = base.viewModelInstance;
			T datablock = this.datablock;
			datablock.Local_CancelStrikes(base.viewModelInstance, base.itemRepresentation, this.iface as global::IStrikeGunItem, ref sample);
			base.nextPrimaryAttackTime = Time.time + 1f;
			this.ResetFiring();
		}
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x000CBD9C File Offset: 0x000C9F9C
	public override void PrimaryAttack(ref global::HumanController.InputSample sample)
	{
		if (!this.beganFiring)
		{
			int num = Random.Range(1, this.datablock.strikeDurations.Length + 1);
			num = Mathf.Clamp(num, 1, this.datablock.strikeDurations.Length);
			this.actualFireTime = Time.time + this.datablock.strikeDurations[num - 1];
			T datablock = this.datablock;
			datablock.Local_BeginStrikes(num, base.viewModelInstance, base.itemRepresentation, this.iface as global::IStrikeGunItem, ref sample);
			this.beganFiring = true;
		}
	}

	// Token: 0x04001C72 RID: 7282
	public bool beganFiring;

	// Token: 0x04001C73 RID: 7283
	public float actualFireTime;
}
