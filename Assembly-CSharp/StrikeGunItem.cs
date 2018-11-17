using System;
using UnityEngine;

// Token: 0x020005E5 RID: 1509
public abstract class StrikeGunItem<T> : BulletWeaponItem<T> where T : StrikeGunDataBlock
{
	// Token: 0x06003615 RID: 13845 RVA: 0x000C3A18 File Offset: 0x000C1C18
	protected StrikeGunItem(T db) : base(db)
	{
	}

	// Token: 0x06003616 RID: 13846 RVA: 0x000C3A24 File Offset: 0x000C1C24
	public void ResetFiring()
	{
		this.actualFireTime = 0f;
		this.beganFiring = false;
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x000C3A38 File Offset: 0x000C1C38
	protected override void OnSetActive(bool isActive)
	{
		this.ResetFiring();
		base.OnSetActive(isActive);
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x000C3A48 File Offset: 0x000C1C48
	public override void ItemPreFrame(ref HumanController.InputSample sample)
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

	// Token: 0x06003619 RID: 13849 RVA: 0x000C3ADC File Offset: 0x000C1CDC
	public virtual void CancelAttack(ref HumanController.InputSample sample)
	{
		if (this.beganFiring)
		{
			ViewModel viewModelInstance = base.viewModelInstance;
			T datablock = this.datablock;
			datablock.Local_CancelStrikes(base.viewModelInstance, base.itemRepresentation, this.iface as IStrikeGunItem, ref sample);
			base.nextPrimaryAttackTime = Time.time + 1f;
			this.ResetFiring();
		}
	}

	// Token: 0x0600361A RID: 13850 RVA: 0x000C3B40 File Offset: 0x000C1D40
	public override void PrimaryAttack(ref HumanController.InputSample sample)
	{
		if (!this.beganFiring)
		{
			int num = Random.Range(1, this.datablock.strikeDurations.Length + 1);
			num = Mathf.Clamp(num, 1, this.datablock.strikeDurations.Length);
			this.actualFireTime = Time.time + this.datablock.strikeDurations[num - 1];
			T datablock = this.datablock;
			datablock.Local_BeginStrikes(num, base.viewModelInstance, base.itemRepresentation, this.iface as IStrikeGunItem, ref sample);
			this.beganFiring = true;
		}
	}

	// Token: 0x04001AA1 RID: 6817
	public bool beganFiring;

	// Token: 0x04001AA2 RID: 6818
	public float actualFireTime;
}
