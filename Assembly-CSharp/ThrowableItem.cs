using System;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
public abstract class ThrowableItem<T> : WeaponItem<T> where T : ThrowableItemDataBlock
{
	// Token: 0x0600362D RID: 13869 RVA: 0x000C4388 File Offset: 0x000C2588
	protected ThrowableItem(T db) : base(db)
	{
	}

	// Token: 0x17000AC2 RID: 2754
	// (get) Token: 0x0600362E RID: 13870 RVA: 0x000C439C File Offset: 0x000C259C
	// (set) Token: 0x0600362F RID: 13871 RVA: 0x000C43A4 File Offset: 0x000C25A4
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

	// Token: 0x17000AC3 RID: 2755
	// (get) Token: 0x06003630 RID: 13872 RVA: 0x000C43B0 File Offset: 0x000C25B0
	// (set) Token: 0x06003631 RID: 13873 RVA: 0x000C43B8 File Offset: 0x000C25B8
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

	// Token: 0x17000AC4 RID: 2756
	// (get) Token: 0x06003632 RID: 13874 RVA: 0x000C43C4 File Offset: 0x000C25C4
	// (set) Token: 0x06003633 RID: 13875 RVA: 0x000C43CC File Offset: 0x000C25CC
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

	// Token: 0x06003634 RID: 13876 RVA: 0x000C43D8 File Offset: 0x000C25D8
	public override void PrimaryAttack(ref HumanController.InputSample sample)
	{
		base.nextPrimaryAttackTime = Time.time + this.datablock.fireRate;
		T datablock = this.datablock;
		datablock.PrimaryAttack(base.viewModelInstance, base.itemRepresentation, this.iface as IThrowableItem, ref sample);
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x000C4430 File Offset: 0x000C2630
	public override void SecondaryAttack(ref HumanController.InputSample sample)
	{
		base.nextSecondaryAttackTime = Time.time + this.datablock.fireRateSecondary;
		T datablock = this.datablock;
		datablock.SecondaryAttack(base.viewModelInstance, base.itemRepresentation, this.iface as IThrowableItem, ref sample);
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x000C4488 File Offset: 0x000C2688
	public virtual void BeginHoldingBack()
	{
		this.holdingStartTime = Time.time;
		this.holdingBack = true;
	}

	// Token: 0x06003637 RID: 13879 RVA: 0x000C449C File Offset: 0x000C269C
	public virtual void EndHoldingBack()
	{
		this.holdingBack = false;
		this.holdingStartTime = 0f;
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x000C44B0 File Offset: 0x000C26B0
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.holdingBack && !sample.attack && Time.time - this.holdingStartTime > this.minReleaseTime)
		{
			T datablock = this.datablock;
			datablock.AttackReleased(base.viewModelInstance, base.itemRepresentation, this.iface as IThrowableItem, ref sample);
			this.holdingBack = false;
		}
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x000C4524 File Offset: 0x000C2724
	protected override void OnSetActive(bool isActive)
	{
		this.EndHoldingBack();
		base.OnSetActive(isActive);
	}

	// Token: 0x17000AC5 RID: 2757
	// (get) Token: 0x0600363A RID: 13882 RVA: 0x000C4534 File Offset: 0x000C2734
	public virtual float heldThrowStrength
	{
		get
		{
			float num = Time.time - this.holdingStartTime;
			return Mathf.Clamp(num * this.datablock.throwStrengthPerSec, this.datablock.throwStrengthMin, this.datablock.throwStrengthMin);
		}
	}

	// Token: 0x04001AAE RID: 6830
	private float _holdingStartTime;

	// Token: 0x04001AAF RID: 6831
	private bool _holdingBack;

	// Token: 0x04001AB0 RID: 6832
	private float _minReleaseTime = 1.25f;
}
