using System;
using UnityEngine;

// Token: 0x02000671 RID: 1649
public abstract class BandageItem<T> : global::HeldItem<T> where T : global::BandageDataBlock
{
	// Token: 0x060038AF RID: 14511 RVA: 0x000C9528 File Offset: 0x000C7728
	protected BandageItem(T db) : base(db)
	{
	}

	// Token: 0x17000AD8 RID: 2776
	// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000C953C File Offset: 0x000C773C
	// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000C9544 File Offset: 0x000C7744
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

	// Token: 0x17000AD9 RID: 2777
	// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000C9550 File Offset: 0x000C7750
	// (set) Token: 0x060038B3 RID: 14515 RVA: 0x000C9558 File Offset: 0x000C7758
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

	// Token: 0x17000ADA RID: 2778
	// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000C9564 File Offset: 0x000C7764
	// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000C956C File Offset: 0x000C776C
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

	// Token: 0x060038B6 RID: 14518 RVA: 0x000C9578 File Offset: 0x000C7778
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
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

	// Token: 0x060038B7 RID: 14519 RVA: 0x000C95C8 File Offset: 0x000C77C8
	public virtual bool CanBandage()
	{
		global::HumanBodyTakeDamage component = base.inventory.gameObject.GetComponent<global::HumanBodyTakeDamage>();
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

	// Token: 0x060038B8 RID: 14520 RVA: 0x000C9630 File Offset: 0x000C7830
	public virtual void Primary(ref global::HumanController.InputSample sample)
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
		global::RPOS.SetActionProgress(true, label, num2);
		if (num2 >= 1f)
		{
			this.FinishBandage();
		}
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x000C9744 File Offset: 0x000C7944
	public void StartBandage()
	{
		this.bandageStartTime = Time.time;
	}

	// Token: 0x060038BA RID: 14522 RVA: 0x000C9754 File Offset: 0x000C7954
	public void FinishBandage()
	{
		this.bandageStartTime = -1f;
		global::RPOS.SetActionProgress(false, null, 0f);
		int num = 1;
		if (base.Consume(ref num))
		{
			base.inventory.RemoveItem(base.slot);
		}
		base.itemRepresentation.Action(3, 0);
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x000C97A8 File Offset: 0x000C79A8
	public void CancelBandage()
	{
		global::RPOS.SetActionProgress(false, null, 0f);
		this.bandageStartTime = -1f;
	}

	// Token: 0x04001C2D RID: 7213
	private float _bandageStartTime = -1f;

	// Token: 0x04001C2E RID: 7214
	private bool _lastFramePrimary;

	// Token: 0x04001C2F RID: 7215
	private float _lastBandageTime;
}
