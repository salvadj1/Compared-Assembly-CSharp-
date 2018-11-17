using System;
using Rust;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public abstract class BowWeaponItem<T> : WeaponItem<T> where T : BowWeaponDataBlock
{
	// Token: 0x06003512 RID: 13586 RVA: 0x000C16D8 File Offset: 0x000BF8D8
	protected BowWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000A6E RID: 2670
	// (get) Token: 0x06003513 RID: 13587 RVA: 0x000C16EC File Offset: 0x000BF8EC
	public override bool canPrimaryAttack
	{
		get
		{
			return Time.time >= base.nextPrimaryAttackTime;
		}
	}

	// Token: 0x17000A6F RID: 2671
	// (get) Token: 0x06003514 RID: 13588 RVA: 0x000C1700 File Offset: 0x000BF900
	public override bool canReload
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x000C1704 File Offset: 0x000BF904
	protected override bool CanAim()
	{
		return !this.IsReloading() && base.CanAim();
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x000C171C File Offset: 0x000BF91C
	public virtual bool IsReloading()
	{
		return false;
	}

	// Token: 0x17000A70 RID: 2672
	// (get) Token: 0x06003517 RID: 13591 RVA: 0x000C1720 File Offset: 0x000BF920
	// (set) Token: 0x06003518 RID: 13592 RVA: 0x000C1728 File Offset: 0x000BF928
	public int currentArrowID
	{
		get
		{
			return this._currentArrowID;
		}
		set
		{
			this._currentArrowID = value;
		}
	}

	// Token: 0x17000A71 RID: 2673
	// (get) Token: 0x06003519 RID: 13593 RVA: 0x000C1734 File Offset: 0x000BF934
	// (set) Token: 0x0600351A RID: 13594 RVA: 0x000C173C File Offset: 0x000BF93C
	public bool arrowDrawn
	{
		get
		{
			return this._arrowDrawn;
		}
		set
		{
			this._arrowDrawn = value;
		}
	}

	// Token: 0x17000A72 RID: 2674
	// (get) Token: 0x0600351B RID: 13595 RVA: 0x000C1748 File Offset: 0x000BF948
	// (set) Token: 0x0600351C RID: 13596 RVA: 0x000C1750 File Offset: 0x000BF950
	public bool tired
	{
		get
		{
			return this._tired;
		}
		set
		{
			this._tired = value;
		}
	}

	// Token: 0x17000A73 RID: 2675
	// (get) Token: 0x0600351D RID: 13597 RVA: 0x000C175C File Offset: 0x000BF95C
	// (set) Token: 0x0600351E RID: 13598 RVA: 0x000C1764 File Offset: 0x000BF964
	public float completeDrawTime
	{
		get
		{
			return this._completeDrawTime;
		}
		set
		{
			this._completeDrawTime = value;
		}
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x000C1770 File Offset: 0x000BF970
	public int GenerateArrowID()
	{
		return Random.Range(1, 65535);
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x000C1780 File Offset: 0x000BF980
	public void ClearArrowID()
	{
		this.currentArrowID = 0;
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x000C178C File Offset: 0x000BF98C
	public bool IsArrowDrawn()
	{
		return this.arrowDrawn;
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x000C1794 File Offset: 0x000BF994
	public bool IsArrowDrawing()
	{
		return this.completeDrawTime != -1f;
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x000C17A8 File Offset: 0x000BF9A8
	public void MakeReadyIn(float delay)
	{
		base.nextPrimaryAttackTime = Time.time + delay;
		this.tired = false;
		this.arrowDrawn = false;
		this.completeDrawTime = -1f;
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x000C17DC File Offset: 0x000BF9DC
	public bool IsArrowDrawingOrDrawn()
	{
		return this.IsArrowDrawn() || this.IsArrowDrawing();
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x000C17F4 File Offset: 0x000BF9F4
	protected override void OnSetActive(bool isActive)
	{
		if (!isActive)
		{
			this.MakeReadyIn(2f);
		}
		base.OnSetActive(isActive);
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x000C1814 File Offset: 0x000BFA14
	protected override bool CanSetActivate(bool value)
	{
		return base.CanSetActivate(value) && (value || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x000C184C File Offset: 0x000BFA4C
	public ItemDataBlock GetDesiredArrow()
	{
		return this.datablock.defaultAmmo;
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x000C1860 File Offset: 0x000BFA60
	public IInventoryItem FindAmmo()
	{
		return base.inventory.FindItem(this.GetDesiredArrow());
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x000C1880 File Offset: 0x000BFA80
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		ViewModel viewModelInstance = base.viewModelInstance;
		if (sample.attack && base.nextPrimaryAttackTime <= Time.time)
		{
			if (this.IsArrowDrawn())
			{
				float num = Time.time - this.completeDrawTime;
				if (num > 1f)
				{
					T datablock = this.datablock;
					datablock.Local_GetTired(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
					this.tired = true;
				}
				if (num > this.datablock.tooTiredLength)
				{
					T datablock2 = this.datablock;
					datablock2.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
				}
			}
			else if (!this.IsArrowDrawn() && !this.IsArrowDrawing())
			{
				if (this.FindAmmo() == null)
				{
					Notice.Popup("", "No Arrows!", 4f);
					this.MakeReadyIn(2f);
				}
				else
				{
					T datablock3 = this.datablock;
					datablock3.Local_ReadyArrow(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
				}
			}
			else if (this.completeDrawTime < Time.time)
			{
				this.arrowDrawn = true;
			}
			if (this.IsArrowDrawingOrDrawn() && Time.time - (this.completeDrawTime - 1f) > 0.5f)
			{
				sample.aim = true;
			}
		}
		else
		{
			if (this.IsArrowDrawn())
			{
				IInventoryItem inventoryItem = this.FindAmmo();
				if (inventoryItem == null)
				{
					Notice.Popup("", "No Arrows!", 4f);
					T datablock4 = this.datablock;
					datablock4.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
				}
				else
				{
					int num2 = 1;
					if (inventoryItem.Consume(ref num2))
					{
						base.inventory.RemoveItem(inventoryItem.slot);
					}
					T datablock5 = this.datablock;
					datablock5.Local_FireArrow(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
				}
			}
			else if (this.IsArrowDrawingOrDrawn())
			{
				T datablock6 = this.datablock;
				datablock6.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as IBowWeaponItem, ref sample);
			}
			sample.aim = false;
		}
		if (sample.aim)
		{
			sample.yaw *= this.datablock.aimSensitivtyPercent;
			sample.pitch *= this.datablock.aimSensitivtyPercent;
		}
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x000C1B30 File Offset: 0x000BFD30
	public void ArrowReportHit(IDMain hitMain, ArrowMovement arrow)
	{
		T datablock = this.datablock;
		datablock.ArrowReportHit(hitMain, arrow, base.itemRepresentation, this.iface as IBowWeaponItem);
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x000C1B64 File Offset: 0x000BFD64
	public void ArrowReportMiss(ArrowMovement arrow)
	{
		T datablock = this.datablock;
		datablock.ArrowReportMiss(arrow, base.itemRepresentation);
	}

	// Token: 0x04001A63 RID: 6755
	private bool _arrowDrawn;

	// Token: 0x04001A64 RID: 6756
	private bool _tired;

	// Token: 0x04001A65 RID: 6757
	private float _completeDrawTime = -1f;

	// Token: 0x04001A66 RID: 6758
	private int _currentArrowID;
}
