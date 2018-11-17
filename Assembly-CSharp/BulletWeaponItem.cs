using System;
using UnityEngine;

// Token: 0x020005BF RID: 1471
public abstract class BulletWeaponItem<T> : WeaponItem<T> where T : BulletWeaponDataBlock
{
	// Token: 0x06003534 RID: 13620 RVA: 0x000C1B8C File Offset: 0x000BFD8C
	protected BulletWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000A78 RID: 2680
	// (get) Token: 0x06003535 RID: 13621 RVA: 0x000C1BA0 File Offset: 0x000BFDA0
	// (set) Token: 0x06003536 RID: 13622 RVA: 0x000C1BA8 File Offset: 0x000BFDA8
	public MagazineDataBlock clipType { get; protected set; }

	// Token: 0x17000A79 RID: 2681
	// (get) Token: 0x06003537 RID: 13623 RVA: 0x000C1BB4 File Offset: 0x000BFDB4
	// (set) Token: 0x06003538 RID: 13624 RVA: 0x000C1BBC File Offset: 0x000BFDBC
	public int cachedCasings { get; set; }

	// Token: 0x17000A7A RID: 2682
	// (get) Token: 0x06003539 RID: 13625 RVA: 0x000C1BC8 File Offset: 0x000BFDC8
	// (set) Token: 0x0600353A RID: 13626 RVA: 0x000C1BD0 File Offset: 0x000BFDD0
	public float nextCasingsTime { get; set; }

	// Token: 0x17000A7B RID: 2683
	// (get) Token: 0x0600353B RID: 13627 RVA: 0x000C1BDC File Offset: 0x000BFDDC
	// (set) Token: 0x0600353C RID: 13628 RVA: 0x000C1BE4 File Offset: 0x000BFDE4
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

	// Token: 0x17000A7C RID: 2684
	// (get) Token: 0x0600353D RID: 13629 RVA: 0x000C1BF0 File Offset: 0x000BFDF0
	public override bool canPrimaryAttack
	{
		get
		{
			return base.canPrimaryAttack && this.clipAmmo > 0;
		}
	}

	// Token: 0x17000A7D RID: 2685
	// (get) Token: 0x0600353E RID: 13630 RVA: 0x000C1C0C File Offset: 0x000BFE0C
	public override bool canReload
	{
		get
		{
			if (base.nextPrimaryAttackTime <= Time.time && this.clipAmmo < this.datablock.maxClipAmmo)
			{
				IInventoryItem inventoryItem = base.inventory.FindItem(this.datablock.ammoType);
				if (inventoryItem != null && inventoryItem.uses > 0)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x000C1C78 File Offset: 0x000BFE78
	protected override bool CanAim()
	{
		return !this.IsReloading() && base.CanAim() && Time.time > this.nextAimTime;
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x000C1CAC File Offset: 0x000BFEAC
	public virtual bool IsReloading()
	{
		return this.reloadStartTime != -1f && Time.time < this.reloadStartTime + this.datablock.reloadDuration;
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x000C1CE0 File Offset: 0x000BFEE0
	public override void Reload(ref HumanController.InputSample sample)
	{
		T datablock = this.datablock;
		datablock.Local_Reload(base.viewModelInstance, base.itemRepresentation, this.iface as IBulletWeaponItem, ref sample);
		this.ActualReload();
		base.inventory.Refresh();
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x000C1D2C File Offset: 0x000BFF2C
	public virtual void CacheReloads()
	{
		this.cachedNumReloads = 0;
	}

	// Token: 0x17000A7E RID: 2686
	// (get) Token: 0x06003543 RID: 13635 RVA: 0x000C1D38 File Offset: 0x000BFF38
	public override int possibleReloadCount
	{
		get
		{
			return this.cachedNumReloads;
		}
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x000C1D40 File Offset: 0x000BFF40
	public virtual void ActualReload_COD()
	{
		this.reloadStartTime = Time.time;
		base.nextPrimaryAttackTime = Time.time + this.datablock.reloadDuration;
		Inventory inventory = base.inventory;
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
			IInventoryItem inventoryItem = inventory.FindItem(this.datablock.ammoType);
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

	// Token: 0x06003545 RID: 13637 RVA: 0x000C1E1C File Offset: 0x000C001C
	public virtual void ActualReload()
	{
		this.ActualReload_COD();
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x000C1E24 File Offset: 0x000C0024
	public override void ItemPreFrame(ref HumanController.InputSample sample)
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
			float num2 = this.datablock.aimSway * ((!sample.crouch) ? 1f : 0.6f);
			sample.yaw += (Mathf.PerlinNoise(num, num) - 0.5f) * num2 * Time.deltaTime;
			sample.pitch += (Mathf.PerlinNoise(num + 0.1f, num + 0.2f) - 0.5f) * num2 * Time.deltaTime;
		}
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x000C1F50 File Offset: 0x000C0150
	protected override bool CanSetActivate(bool value)
	{
		return base.CanSetActivate(value) && (value || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x000C1F88 File Offset: 0x000C0188
	public override void PrimaryAttack(ref HumanController.InputSample sample)
	{
		base.nextPrimaryAttackTime = Time.time + this.datablock.fireRate;
		if (this.datablock.NoAimingAfterShot)
		{
			this.nextAimTime = Time.time + this.datablock.fireRate;
		}
		ViewModel vm = base.viewModelInstance;
		if (actor.forceThirdPerson)
		{
			vm = null;
		}
		T datablock = this.datablock;
		datablock.Local_FireWeapon(vm, base.itemRepresentation, this.iface as IBulletWeaponItem, ref sample);
	}

	// Token: 0x04001A67 RID: 6759
	private float reloadStartTime = -1f;

	// Token: 0x04001A68 RID: 6760
	private int cachedNumReloads;

	// Token: 0x04001A69 RID: 6761
	public float nextAimTime;
}
