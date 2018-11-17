using System;
using UnityEngine;

// Token: 0x020006AD RID: 1709
public abstract class WeaponItem<T> : global::HeldItem<T> where T : global::WeaponDataBlock
{
	// Token: 0x06003A3A RID: 14906 RVA: 0x000CCAEC File Offset: 0x000CACEC
	protected WeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000B52 RID: 2898
	// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000CCAF8 File Offset: 0x000CACF8
	// (set) Token: 0x06003A3C RID: 14908 RVA: 0x000CCB00 File Offset: 0x000CAD00
	public float nextPrimaryAttackTime { get; set; }

	// Token: 0x17000B53 RID: 2899
	// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000CCB0C File Offset: 0x000CAD0C
	// (set) Token: 0x06003A3E RID: 14910 RVA: 0x000CCB14 File Offset: 0x000CAD14
	public float nextSecondaryAttackTime { get; set; }

	// Token: 0x17000B54 RID: 2900
	// (get) Token: 0x06003A3F RID: 14911 RVA: 0x000CCB20 File Offset: 0x000CAD20
	// (set) Token: 0x06003A40 RID: 14912 RVA: 0x000CCB28 File Offset: 0x000CAD28
	public float deployFinishedTime { get; set; }

	// Token: 0x17000B55 RID: 2901
	// (get) Token: 0x06003A41 RID: 14913 RVA: 0x000CCB34 File Offset: 0x000CAD34
	// (set) Token: 0x06003A42 RID: 14914 RVA: 0x000CCB3C File Offset: 0x000CAD3C
	public double lastPrimaryMessageTime { get; set; }

	// Token: 0x06003A43 RID: 14915 RVA: 0x000CCB48 File Offset: 0x000CAD48
	public bool ValidatePrimaryMessageTime(double timestamp)
	{
		double num = timestamp - this.lastPrimaryMessageTime;
		if (num < (double)(this.datablock.fireRate * 0.95f))
		{
			return false;
		}
		if (timestamp > global::NetCull.time + 3.0)
		{
			return false;
		}
		this.lastPrimaryMessageTime = timestamp;
		return true;
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x000CCB9C File Offset: 0x000CAD9C
	public override void ItemPostFrame(ref global::HumanController.InputSample sample)
	{
	}

	// Token: 0x06003A45 RID: 14917 RVA: 0x000CCBA0 File Offset: 0x000CADA0
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (sample.is_sprinting)
		{
			float num = Time.time + 0.1f;
			this.nextPrimaryAttackTime = ((this.nextPrimaryAttackTime <= num) ? num : this.nextPrimaryAttackTime);
			this.wasSprinting = true;
		}
		else if (this.wasSprinting)
		{
			float num2 = Time.time + 0.3f;
			this.nextPrimaryAttackTime = ((this.nextPrimaryAttackTime <= num2) ? num2 : this.nextPrimaryAttackTime);
			this.wasSprinting = false;
		}
		if (this.datablock.isSemiAuto)
		{
			if (sample.attack && this.lastFrameAttack)
			{
				sample.attack = false;
			}
			else if (!sample.attack && this.lastFrameAttack)
			{
				this.lastFrameAttack = false;
			}
			else if (sample.attack && !this.lastFrameAttack)
			{
				this.lastFrameAttack = true;
			}
		}
		if (sample.attack && this.canPrimaryAttack)
		{
			this.PrimaryAttack(ref sample);
		}
		if (sample.attack2 && this.canSecondaryAttack)
		{
			this.SecondaryAttack(ref sample);
		}
		if (sample.reload && this.canReload)
		{
			this.Reload(ref sample);
		}
	}

	// Token: 0x17000B56 RID: 2902
	// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000CCD00 File Offset: 0x000CAF00
	public virtual int possibleReloadCount
	{
		get
		{
			return 999;
		}
	}

	// Token: 0x17000B57 RID: 2903
	// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000CCD08 File Offset: 0x000CAF08
	public virtual bool canPrimaryAttack
	{
		get
		{
			return Time.time >= this.nextPrimaryAttackTime;
		}
	}

	// Token: 0x17000B58 RID: 2904
	// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000CCD1C File Offset: 0x000CAF1C
	public virtual bool canSecondaryAttack
	{
		get
		{
			return Time.time >= this.nextSecondaryAttackTime;
		}
	}

	// Token: 0x17000B59 RID: 2905
	// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000CCD30 File Offset: 0x000CAF30
	public virtual bool canReload
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x000CCD34 File Offset: 0x000CAF34
	public virtual void Reload(ref global::HumanController.InputSample sample)
	{
	}

	// Token: 0x06003A4B RID: 14923 RVA: 0x000CCD38 File Offset: 0x000CAF38
	public virtual void PrimaryAttack(ref global::HumanController.InputSample sample)
	{
		this.nextPrimaryAttackTime = Time.time + 1f;
		Debug.Log("Primary Attack!");
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x000CCD58 File Offset: 0x000CAF58
	public virtual void SecondaryAttack(ref global::HumanController.InputSample sample)
	{
		this.nextSecondaryAttackTime = Time.time + 1f;
		Debug.Log("Secondary Attack!");
	}

	// Token: 0x17000B5A RID: 2906
	// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000CCD78 File Offset: 0x000CAF78
	public virtual bool deployed
	{
		get
		{
			return Time.time > this.deployFinishedTime;
		}
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x000CCD88 File Offset: 0x000CAF88
	protected override bool CanAim()
	{
		return this.deployed && base.CanAim();
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x000CCDA0 File Offset: 0x000CAFA0
	protected override void OnSetActive(bool isActive)
	{
		float deployLength = this.datablock.deployLength;
		this.deployFinishedTime = Time.time + deployLength;
		if (this.deployFinishedTime > this.nextPrimaryAttackTime)
		{
			float deployFinishedTime = this.deployFinishedTime;
			this.nextPrimaryAttackTime = deployFinishedTime;
			this.nextSecondaryAttackTime = deployFinishedTime;
		}
		base.OnSetActive(isActive);
	}

	// Token: 0x04001C87 RID: 7303
	protected bool lastFrameAttack;

	// Token: 0x04001C88 RID: 7304
	private bool wasSprinting;
}
