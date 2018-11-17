using System;
using UnityEngine;

// Token: 0x0200067D RID: 1661
public abstract class BulletWeaponItem<T> : global::WeaponItem<T> where T : global::BulletWeaponDataBlock
{
	// Token: 0x060038FC RID: 14588 RVA: 0x000C9DE8 File Offset: 0x000C7FE8
	protected BulletWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000AEE RID: 2798
	// (get) Token: 0x060038FD RID: 14589 RVA: 0x000C9DFC File Offset: 0x000C7FFC
	// (set) Token: 0x060038FE RID: 14590 RVA: 0x000C9E04 File Offset: 0x000C8004
	public global::MagazineDataBlock clipType { get; protected set; }

	// Token: 0x17000AEF RID: 2799
	// (get) Token: 0x060038FF RID: 14591 RVA: 0x000C9E10 File Offset: 0x000C8010
	// (set) Token: 0x06003900 RID: 14592 RVA: 0x000C9E18 File Offset: 0x000C8018
	public int cachedCasings { get; set; }

	// Token: 0x17000AF0 RID: 2800
	// (get) Token: 0x06003901 RID: 14593 RVA: 0x000C9E24 File Offset: 0x000C8024
	// (set) Token: 0x06003902 RID: 14594 RVA: 0x000C9E2C File Offset: 0x000C802C
	public float nextCasingsTime { get; set; }

	// Token: 0x17000AF1 RID: 2801
	// (get) Token: 0x06003903 RID: 14595 RVA: 0x000C9E38 File Offset: 0x000C8038
	// (set) Token: 0x06003904 RID: 14596 RVA: 0x000C9E40 File Offset: 0x000C8040
	public int clipAmmo
	{
		get
		{
			return base.uses;
		}
		set
		{
			base.SetUses(value);
		}
	}

	// Token: 0x17000AF2 RID: 2802
	// (get) Token: 0x06003905 RID: 14597 RVA: 0x000C9E4C File Offset: 0x000C804C
	public override bool canPrimaryAttack
	{
		get
		{
			return base.canPrimaryAttack && this.clipAmmo > 0;
		}
	}

	// Token: 0x17000AF3 RID: 2803
	// (get) Token: 0x06003906 RID: 14598 RVA: 0x000C9E68 File Offset: 0x000C8068
	public override bool canReload
	{
		get
		{
			if (base.nextPrimaryAttackTime <= Time.time && this.clipAmmo < this.datablock.maxClipAmmo)
			{
				global::IInventoryItem inventoryItem = base.inventory.FindItem(this.datablock.ammoType);
				if (inventoryItem != null && inventoryItem.uses > 0)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06003907 RID: 14599 RVA: 0x000C9ED4 File Offset: 0x000C80D4
	protected override bool CanAim()
	{
		return !this.IsReloading() && base.CanAim() && Time.time > this.nextAimTime;
	}

	// Token: 0x06003908 RID: 14600 RVA: 0x000C9F08 File Offset: 0x000C8108
	public virtual bool IsReloading()
	{
		return this.reloadStartTime != -1f && Time.time < this.reloadStartTime + this.datablock.reloadDuration;
	}

	// Token: 0x06003909 RID: 14601 RVA: 0x000C9F3C File Offset: 0x000C813C
	public override void Reload(ref global::HumanController.InputSample sample)
	{
		T datablock = this.datablock;
		datablock.Local_Reload(base.viewModelInstance, base.itemRepresentation, this.iface as global::IBulletWeaponItem, ref sample);
		this.ActualReload();
		base.inventory.Refresh();
	}

	// Token: 0x0600390A RID: 14602 RVA: 0x000C9F88 File Offset: 0x000C8188
	public virtual void CacheReloads()
	{
		this.cachedNumReloads = 0;
	}

	// Token: 0x17000AF4 RID: 2804
	// (get) Token: 0x0600390B RID: 14603 RVA: 0x000C9F94 File Offset: 0x000C8194
	public override int possibleReloadCount
	{
		get
		{
			return this.cachedNumReloads;
		}
	}

	// Token: 0x0600390C RID: 14604 RVA: 0x000C9F9C File Offset: 0x000C819C
	public virtual void ActualReload_COD()
	{
		this.reloadStartTime = Time.time;
		base.nextPrimaryAttackTime = Time.time + this.datablock.reloadDuration;
		global::Inventory inventory = base.inventory;
		int i = base.uses;
		int maxClipAmmo = this.datablock.maxClipAmmo;
		if (i == maxClipAmmo)
		{
			return;
		}
		int num = maxClipAmmo - i;
		int num2 = 0;
		while (i < maxClipAmmo)
		{
			global::IInventoryItem inventoryItem = inventory.FindItem(this.datablock.ammoType);
			if (inventoryItem == null)
			{
				break;
			}
			int num3 = num;
			if (inventoryItem.Consume(ref num))
			{
				inventory.RemoveItem(inventoryItem.slot);
			}
			num2 += num3 - num;
			if (num == 0)
			{
				break;
			}
		}
		if (num2 > 0)
		{
			base.AddUses(num2);
		}
		inventory.Refresh();
	}

	// Token: 0x0600390D RID: 14605 RVA: 0x000CA078 File Offset: 0x000C8278
	public virtual void ActualReload()
	{
		this.ActualReload_COD();
	}

	// Token: 0x0600390E RID: 14606 RVA: 0x000CA080 File Offset: 0x000C8280
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		if (sample.attack && this.clipAmmo == 0 && base.nextPrimaryAttackTime <= Time.time)
		{
			T datablock = this.datablock;
			datablock.Local_DryFire(base.viewModelInstance, base.itemRepresentation);
			base.nextPrimaryAttackTime = Time.time + 1f;
			sample.attack = false;
		}
		base.ItemPreFrame(ref sample);
		if (sample.aim && this.datablock.aimSway > 0f)
		{
			float num = Time.time * this.datablock.aimSwaySpeed;
			float num2 = this.datablock.aimSway * ((!sample.crouch) ? 0f : 0f);
			sample.yaw += (Mathf.PerlinNoise(num, num) - 0.5f) * num2 * Time.deltaTime;
			sample.pitch += (Mathf.PerlinNoise(num + 0.1f, num + 0.2f) - 0.5f) * num2 * Time.deltaTime;
		}
	}

	// Token: 0x0600390F RID: 14607 RVA: 0x000CA1AC File Offset: 0x000C83AC
	protected override bool CanSetActivate(bool value)
	{
		return base.CanSetActivate(value) && (value || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x06003910 RID: 14608 RVA: 0x000CA1E4 File Offset: 0x000C83E4
	public override void PrimaryAttack(ref global::HumanController.InputSample sample)
	{
		base.nextPrimaryAttackTime = Time.time + this.datablock.fireRate;
		if (this.datablock.NoAimingAfterShot)
		{
			this.nextAimTime = Time.time + this.datablock.fireRate;
		}
		global::ViewModel vm = base.viewModelInstance;
		if (global::actor.forceThirdPerson)
		{
			vm = null;
		}
		T datablock = this.datablock;
		datablock.Local_FireWeapon(vm, base.itemRepresentation, this.iface as global::IBulletWeaponItem, ref sample);
	}

	// Token: 0x04001C38 RID: 7224
	private float reloadStartTime = -1f;

	// Token: 0x04001C39 RID: 7225
	private int cachedNumReloads;

	// Token: 0x04001C3A RID: 7226
	public float nextAimTime;
}
