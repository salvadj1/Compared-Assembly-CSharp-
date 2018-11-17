using System;
using Rust;
using UnityEngine;

// Token: 0x0200067B RID: 1659
public abstract class BowWeaponItem<T> : global::WeaponItem<T> where T : global::BowWeaponDataBlock
{
	// Token: 0x060038DA RID: 14554 RVA: 0x000C9934 File Offset: 0x000C7B34
	protected BowWeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000AE4 RID: 2788
	// (get) Token: 0x060038DB RID: 14555 RVA: 0x000C9948 File Offset: 0x000C7B48
	public override bool canPrimaryAttack
	{
		get
		{
			return Time.time >= base.nextPrimaryAttackTime;
		}
	}

	// Token: 0x17000AE5 RID: 2789
	// (get) Token: 0x060038DC RID: 14556 RVA: 0x000C995C File Offset: 0x000C7B5C
	public override bool canReload
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060038DD RID: 14557 RVA: 0x000C9960 File Offset: 0x000C7B60
	protected override bool CanAim()
	{
		return !this.IsReloading() && base.CanAim();
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x000C9978 File Offset: 0x000C7B78
	public virtual bool IsReloading()
	{
		return false;
	}

	// Token: 0x17000AE6 RID: 2790
	// (get) Token: 0x060038DF RID: 14559 RVA: 0x000C997C File Offset: 0x000C7B7C
	// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000C9984 File Offset: 0x000C7B84
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

	// Token: 0x17000AE7 RID: 2791
	// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000C9990 File Offset: 0x000C7B90
	// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000C9998 File Offset: 0x000C7B98
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

	// Token: 0x17000AE8 RID: 2792
	// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000C99A4 File Offset: 0x000C7BA4
	// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000C99AC File Offset: 0x000C7BAC
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

	// Token: 0x17000AE9 RID: 2793
	// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000C99B8 File Offset: 0x000C7BB8
	// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000C99C0 File Offset: 0x000C7BC0
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

	// Token: 0x060038E7 RID: 14567 RVA: 0x000C99CC File Offset: 0x000C7BCC
	public int GenerateArrowID()
	{
		return Random.Range(1, 65535);
	}

	// Token: 0x060038E8 RID: 14568 RVA: 0x000C99DC File Offset: 0x000C7BDC
	public void ClearArrowID()
	{
		this.currentArrowID = 0;
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x000C99E8 File Offset: 0x000C7BE8
	public bool IsArrowDrawn()
	{
		return this.arrowDrawn;
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x000C99F0 File Offset: 0x000C7BF0
	public bool IsArrowDrawing()
	{
		return this.completeDrawTime != -1f;
	}

	// Token: 0x060038EB RID: 14571 RVA: 0x000C9A04 File Offset: 0x000C7C04
	public void MakeReadyIn(float delay)
	{
		base.nextPrimaryAttackTime = Time.time + delay;
		this.tired = false;
		this.arrowDrawn = false;
		this.completeDrawTime = -1f;
	}

	// Token: 0x060038EC RID: 14572 RVA: 0x000C9A38 File Offset: 0x000C7C38
	public bool IsArrowDrawingOrDrawn()
	{
		return this.IsArrowDrawn() || this.IsArrowDrawing();
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x000C9A50 File Offset: 0x000C7C50
	protected override void OnSetActive(bool isActive)
	{
		if (!isActive)
		{
			this.MakeReadyIn(2f);
		}
		base.OnSetActive(isActive);
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x000C9A70 File Offset: 0x000C7C70
	protected override bool CanSetActivate(bool value)
	{
		return base.CanSetActivate(value) && (value || base.nextPrimaryAttackTime <= Time.time);
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x000C9AA8 File Offset: 0x000C7CA8
	public global::ItemDataBlock GetDesiredArrow()
	{
		return this.datablock.defaultAmmo;
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x000C9ABC File Offset: 0x000C7CBC
	public global::IInventoryItem FindAmmo()
	{
		return base.inventory.FindItem(this.GetDesiredArrow());
	}

	// Token: 0x060038F1 RID: 14577 RVA: 0x000C9ADC File Offset: 0x000C7CDC
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		global::ViewModel viewModelInstance = base.viewModelInstance;
		if (sample.attack && base.nextPrimaryAttackTime <= Time.time)
		{
			if (this.IsArrowDrawn())
			{
				float num = Time.time - this.completeDrawTime;
				if (num > 1f)
				{
					T datablock = this.datablock;
					datablock.Local_GetTired(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
					this.tired = true;
				}
				if (num > this.datablock.tooTiredLength)
				{
					T datablock2 = this.datablock;
					datablock2.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
				}
			}
			else if (!this.IsArrowDrawn() && !this.IsArrowDrawing())
			{
				if (this.FindAmmo() == null)
				{
					Rust.Notice.Popup("", "No Arrows!", 4f);
					this.MakeReadyIn(2f);
				}
				else
				{
					T datablock3 = this.datablock;
					datablock3.Local_ReadyArrow(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
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
				global::IInventoryItem inventoryItem = this.FindAmmo();
				if (inventoryItem == null)
				{
					Rust.Notice.Popup("", "No Arrows!", 4f);
					T datablock4 = this.datablock;
					datablock4.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
				}
				else
				{
					int num2 = 1;
					if (inventoryItem.Consume(ref num2))
					{
						base.inventory.RemoveItem(inventoryItem.slot);
					}
					T datablock5 = this.datablock;
					datablock5.Local_FireArrow(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
				}
			}
			else if (this.IsArrowDrawingOrDrawn())
			{
				T datablock6 = this.datablock;
				datablock6.Local_CancelArrow(viewModelInstance, base.itemRepresentation, this.iface as global::IBowWeaponItem, ref sample);
			}
			sample.aim = false;
		}
		if (sample.aim)
		{
			sample.yaw *= this.datablock.aimSensitivtyPercent;
			sample.pitch *= this.datablock.aimSensitivtyPercent;
		}
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x000C9D8C File Offset: 0x000C7F8C
	public void ArrowReportHit(IDMain hitMain, global::ArrowMovement arrow)
	{
		T datablock = this.datablock;
		datablock.ArrowReportHit(hitMain, arrow, base.itemRepresentation, this.iface as global::IBowWeaponItem);
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x000C9DC0 File Offset: 0x000C7FC0
	public void ArrowReportMiss(global::ArrowMovement arrow)
	{
		T datablock = this.datablock;
		datablock.ArrowReportMiss(arrow, base.itemRepresentation);
	}

	// Token: 0x04001C34 RID: 7220
	private bool _arrowDrawn;

	// Token: 0x04001C35 RID: 7221
	private bool _tired;

	// Token: 0x04001C36 RID: 7222
	private float _completeDrawTime = -1f;

	// Token: 0x04001C37 RID: 7223
	private int _currentArrowID;
}
