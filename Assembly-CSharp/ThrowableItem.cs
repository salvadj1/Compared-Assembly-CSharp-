using System;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
public abstract class ThrowableItem<T> : global::WeaponItem<T> where T : global::ThrowableItemDataBlock
{
	// Token: 0x060039F5 RID: 14837 RVA: 0x000CC5E4 File Offset: 0x000CA7E4
	protected ThrowableItem(T db) : base(db)
	{
	}

	// Token: 0x17000B38 RID: 2872
	// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000CC5F8 File Offset: 0x000CA7F8
	// (set) Token: 0x060039F7 RID: 14839 RVA: 0x000CC600 File Offset: 0x000CA800
	public float holdingStartTime
	{
		get
		{
			return this._holdingStartTime;
		}
		set
		{
			this._holdingStartTime = value;
		}
	}

	// Token: 0x17000B39 RID: 2873
	// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000CC60C File Offset: 0x000CA80C
	// (set) Token: 0x060039F9 RID: 14841 RVA: 0x000CC614 File Offset: 0x000CA814
	public bool holdingBack
	{
		get
		{
			return this._holdingBack;
		}
		set
		{
			this._holdingBack = value;
		}
	}

	// Token: 0x17000B3A RID: 2874
	// (get) Token: 0x060039FA RID: 14842 RVA: 0x000CC620 File Offset: 0x000CA820
	// (set) Token: 0x060039FB RID: 14843 RVA: 0x000CC628 File Offset: 0x000CA828
	public float minReleaseTime
	{
		get
		{
			return this._minReleaseTime;
		}
		set
		{
			this._minReleaseTime = value;
		}
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x000CC634 File Offset: 0x000CA834
	public override void PrimaryAttack(ref global::HumanController.InputSample sample)
	{
		base.nextPrimaryAttackTime = Time.time + this.datablock.fireRate;
		T datablock = this.datablock;
		datablock.PrimaryAttack(base.viewModelInstance, base.itemRepresentation, this.iface as global::IThrowableItem, ref sample);
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x000CC68C File Offset: 0x000CA88C
	public override void SecondaryAttack(ref global::HumanController.InputSample sample)
	{
		base.nextSecondaryAttackTime = Time.time + this.datablock.fireRateSecondary;
		T datablock = this.datablock;
		datablock.SecondaryAttack(base.viewModelInstance, base.itemRepresentation, this.iface as global::IThrowableItem, ref sample);
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x000CC6E4 File Offset: 0x000CA8E4
	public virtual void BeginHoldingBack()
	{
		this.holdingStartTime = Time.time;
		this.holdingBack = true;
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x000CC6F8 File Offset: 0x000CA8F8
	public virtual void EndHoldingBack()
	{
		this.holdingBack = false;
		this.holdingStartTime = 0f;
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x000CC70C File Offset: 0x000CA90C
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.holdingBack && !sample.attack && Time.time - this.holdingStartTime > this.minReleaseTime)
		{
			T datablock = this.datablock;
			datablock.AttackReleased(base.viewModelInstance, base.itemRepresentation, this.iface as global::IThrowableItem, ref sample);
			this.holdingBack = false;
		}
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x000CC780 File Offset: 0x000CA980
	protected override void OnSetActive(bool isActive)
	{
		this.EndHoldingBack();
		base.OnSetActive(isActive);
	}

	// Token: 0x17000B3B RID: 2875
	// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000CC790 File Offset: 0x000CA990
	public virtual float heldThrowStrength
	{
		get
		{
			float num = Time.time - this.holdingStartTime;
			return Mathf.Clamp(num * this.datablock.throwStrengthPerSec, this.datablock.throwStrengthMin, this.datablock.throwStrengthMin);
		}
	}

	// Token: 0x04001C7F RID: 7295
	private float _holdingStartTime;

	// Token: 0x04001C80 RID: 7296
	private bool _holdingBack;

	// Token: 0x04001C81 RID: 7297
	private float _minReleaseTime = 1.25f;
}
