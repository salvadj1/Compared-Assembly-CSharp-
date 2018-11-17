using System;
using UnityEngine;

// Token: 0x020005EF RID: 1519
public abstract class WeaponItem<T> : HeldItem<T> where T : WeaponDataBlock
{
	// Token: 0x06003672 RID: 13938 RVA: 0x000C4890 File Offset: 0x000C2A90
	protected WeaponItem(T db) : base(db)
	{
	}

	// Token: 0x17000ADC RID: 2780
	// (get) Token: 0x06003673 RID: 13939 RVA: 0x000C489C File Offset: 0x000C2A9C
	// (set) Token: 0x06003674 RID: 13940 RVA: 0x000C48A4 File Offset: 0x000C2AA4
	public float nextPrimaryAttackTime { get; set; }

	// Token: 0x17000ADD RID: 2781
	// (get) Token: 0x06003675 RID: 13941 RVA: 0x000C48B0 File Offset: 0x000C2AB0
	// (set) Token: 0x06003676 RID: 13942 RVA: 0x000C48B8 File Offset: 0x000C2AB8
	public float nextSecondaryAttackTime { get; set; }

	// Token: 0x17000ADE RID: 2782
	// (get) Token: 0x06003677 RID: 13943 RVA: 0x000C48C4 File Offset: 0x000C2AC4
	// (set) Token: 0x06003678 RID: 13944 RVA: 0x000C48CC File Offset: 0x000C2ACC
	public float deployFinishedTime { get; set; }

	// Token: 0x17000ADF RID: 2783
	// (get) Token: 0x06003679 RID: 13945 RVA: 0x000C48D8 File Offset: 0x000C2AD8
	// (set) Token: 0x0600367A RID: 13946 RVA: 0x000C48E0 File Offset: 0x000C2AE0
	public double lastPrimaryMessageTime { get; set; }

	// Token: 0x0600367B RID: 13947 RVA: 0x000C48EC File Offset: 0x000C2AEC
	public bool ValidatePrimaryMessageTime(double timestamp)
	{
		double num = timestamp - this.lastPrimaryMessageTime;
		if (num < (double)(this.datablock.fireRate * 0.95f))
		{
			return false;
		}
		if (timestamp > NetCull.time + 3.0)
		{
			return false;
		}
		this.lastPrimaryMessageTime = timestamp;
		return true;
	}

	// Token: 0x0600367C RID: 13948 RVA: 0x000C4940 File Offset: 0x000C2B40
	public override void ItemPostFrame(ref HumanController.InputSample sample)
	{
	}

	// Token: 0x0600367D RID: 13949 RVA: 0x000C4944 File Offset: 0x000C2B44
	public override void ItemPreFrame(ref HumanController.InputSample sample)
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

	// Token: 0x17000AE0 RID: 2784
	// (get) Token: 0x0600367E RID: 13950 RVA: 0x000C4AA4 File Offset: 0x000C2CA4
	public virtual int possibleReloadCount
	{
		get
		{
			return 999;
		}
	}

	// Token: 0x17000AE1 RID: 2785
	// (get) Token: 0x0600367F RID: 13951 RVA: 0x000C4AAC File Offset: 0x000C2CAC
	public virtual bool canPrimaryAttack
	{
		get
		{
			return Time.time >= this.nextPrimaryAttackTime;
		}
	}

	// Token: 0x17000AE2 RID: 2786
	// (get) Token: 0x06003680 RID: 13952 RVA: 0x000C4AC0 File Offset: 0x000C2CC0
	public virtual bool canSecondaryAttack
	{
		get
		{
			return Time.time >= this.nextSecondaryAttackTime;
		}
	}

	// Token: 0x17000AE3 RID: 2787
	// (get) Token: 0x06003681 RID: 13953 RVA: 0x000C4AD4 File Offset: 0x000C2CD4
	public virtual bool canReload
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06003682 RID: 13954 RVA: 0x000C4AD8 File Offset: 0x000C2CD8
	public virtual void Reload(ref HumanController.InputSample sample)
	{
	}

	// Token: 0x06003683 RID: 13955 RVA: 0x000C4ADC File Offset: 0x000C2CDC
	public virtual void PrimaryAttack(ref HumanController.InputSample sample)
	{
		this.nextPrimaryAttackTime = Time.time + 1f;
		Debug.Log("Primary Attack!");
	}

	// Token: 0x06003684 RID: 13956 RVA: 0x000C4AFC File Offset: 0x000C2CFC
	public virtual void SecondaryAttack(ref HumanController.InputSample sample)
	{
		this.nextSecondaryAttackTime = Time.time + 1f;
		Debug.Log("Secondary Attack!");
	}

	// Token: 0x17000AE4 RID: 2788
	// (get) Token: 0x06003685 RID: 13957 RVA: 0x000C4B1C File Offset: 0x000C2D1C
	public virtual bool deployed
	{
		get
		{
			return Time.time > this.deployFinishedTime;
		}
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x000C4B2C File Offset: 0x000C2D2C
	protected override bool CanAim()
	{
		return this.deployed && base.CanAim();
	}

	// Token: 0x06003687 RID: 13959 RVA: 0x000C4B44 File Offset: 0x000C2D44
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

	// Token: 0x04001AB6 RID: 6838
	protected bool lastFrameAttack;

	// Token: 0x04001AB7 RID: 6839
	private bool wasSprinting;
}
