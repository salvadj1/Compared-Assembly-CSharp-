using System;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public abstract class BandageItem<T> : HeldItem<T> where T : BandageDataBlock
{
	// Token: 0x060034E7 RID: 13543 RVA: 0x000C12CC File Offset: 0x000BF4CC
	protected BandageItem(T db) : base(db)
	{
	}

	// Token: 0x17000A62 RID: 2658
	// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000C12E0 File Offset: 0x000BF4E0
	// (set) Token: 0x060034E9 RID: 13545 RVA: 0x000C12E8 File Offset: 0x000BF4E8
	public float bandageStartTime
	{
		get
		{
			return this._bandageStartTime;
		}
		set
		{
			this._bandageStartTime = value;
		}
	}

	// Token: 0x17000A63 RID: 2659
	// (get) Token: 0x060034EA RID: 13546 RVA: 0x000C12F4 File Offset: 0x000BF4F4
	// (set) Token: 0x060034EB RID: 13547 RVA: 0x000C12FC File Offset: 0x000BF4FC
	public bool lastFramePrimary
	{
		get
		{
			return this._lastFramePrimary;
		}
		set
		{
			this._lastFramePrimary = value;
		}
	}

	// Token: 0x17000A64 RID: 2660
	// (get) Token: 0x060034EC RID: 13548 RVA: 0x000C1308 File Offset: 0x000BF508
	// (set) Token: 0x060034ED RID: 13549 RVA: 0x000C1310 File Offset: 0x000BF510
	public float lastBandageTime
	{
		get
		{
			return this._lastBandageTime;
		}
		set
		{
			this._lastBandageTime = value;
		}
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x000C131C File Offset: 0x000BF51C
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (sample.attack && this.CanBandage())
		{
			this.Primary(ref sample);
		}
		else
		{
			if (this.lastFramePrimary)
			{
				this.CancelBandage();
			}
			this.lastFramePrimary = false;
		}
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x000C136C File Offset: 0x000BF56C
	public virtual bool CanBandage()
	{
		HumanBodyTakeDamage component = base.inventory.gameObject.GetComponent<HumanBodyTakeDamage>();
		if (!component.IsBleeding())
		{
			if (component.healthLossFraction > 0f)
			{
				T datablock = this.datablock;
				if (datablock.DoesGiveBlood())
				{
					goto IL_45;
				}
			}
			return false;
		}
		IL_45:
		return Time.time - this.lastBandageTime > 1.5f;
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x000C13D4 File Offset: 0x000BF5D4
	public virtual void Primary(ref HumanController.InputSample sample)
	{
		this.lastFramePrimary = true;
		sample.crouch = true;
		sample.walk = 0f;
		sample.strafe = 0f;
		sample.jump = false;
		sample.sprint = false;
		if (this.bandageStartTime == -1f)
		{
			this.StartBandage();
		}
		float num = Time.time - this.bandageStartTime;
		float num2 = Mathf.Clamp(num / this.datablock.bandageDuration, 0f, 1f);
		string label = string.Empty;
		T datablock = this.datablock;
		bool flag = datablock.DoesGiveBlood();
		T datablock2 = this.datablock;
		bool flag2 = datablock2.DoesBandage();
		if (flag2 && !flag)
		{
			label = "Bandaging...";
		}
		else if (flag2 && flag)
		{
			label = "Bandage + Transfusion...";
		}
		else if (!flag2 && flag)
		{
			label = "Transfusing...";
		}
		RPOS.SetActionProgress(true, label, num2);
		if (num2 >= 1f)
		{
			this.FinishBandage();
		}
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x000C14E8 File Offset: 0x000BF6E8
	public void StartBandage()
	{
		this.bandageStartTime = Time.time;
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x000C14F8 File Offset: 0x000BF6F8
	public void FinishBandage()
	{
		this.bandageStartTime = -1f;
		RPOS.SetActionProgress(false, null, 0f);
		int num = 1;
		if (base.Consume(ref num))
		{
			base.inventory.RemoveItem(base.slot);
		}
		base.itemRepresentation.Action(3, 0);
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x000C154C File Offset: 0x000BF74C
	public void CancelBandage()
	{
		RPOS.SetActionProgress(false, null, 0f);
		this.bandageStartTime = -1f;
	}

	// Token: 0x04001A5C RID: 6748
	private float _bandageStartTime = -1f;

	// Token: 0x04001A5D RID: 6749
	private bool _lastFramePrimary;

	// Token: 0x04001A5E RID: 6750
	private float _lastBandageTime;
}
