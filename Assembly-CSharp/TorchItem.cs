using System;
using UnityEngine;

// Token: 0x020006AB RID: 1707
public abstract class TorchItem<T> : global::ThrowableItem<T> where T : global::TorchItemDataBlock
{
	// Token: 0x06003A19 RID: 14873 RVA: 0x000CC88C File Offset: 0x000CAA8C
	protected TorchItem(T db) : base(db)
	{
	}

	// Token: 0x17000B45 RID: 2885
	// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000CC898 File Offset: 0x000CAA98
	// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000CC8A0 File Offset: 0x000CAAA0
	public bool isLit { get; protected set; }

	// Token: 0x17000B46 RID: 2886
	// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000CC8AC File Offset: 0x000CAAAC
	// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000CC8B4 File Offset: 0x000CAAB4
	public float realThrowTime { get; set; }

	// Token: 0x17000B47 RID: 2887
	// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
	// (set) Token: 0x06003A1F RID: 14879 RVA: 0x000CC8C8 File Offset: 0x000CAAC8
	public float realIgniteTime { get; set; }

	// Token: 0x17000B48 RID: 2888
	// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000CC8D4 File Offset: 0x000CAAD4
	// (set) Token: 0x06003A21 RID: 14881 RVA: 0x000CC8DC File Offset: 0x000CAADC
	public float forceSecondaryTime { get; set; }

	// Token: 0x17000B49 RID: 2889
	// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000CC8E8 File Offset: 0x000CAAE8
	// (set) Token: 0x06003A23 RID: 14883 RVA: 0x000CC8F0 File Offset: 0x000CAAF0
	public GameObject light { get; set; }

	// Token: 0x06003A24 RID: 14884 RVA: 0x000CC8FC File Offset: 0x000CAAFC
	public bool IsIgnited()
	{
		return this.isLit;
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x000CC904 File Offset: 0x000CAB04
	public void Ignite()
	{
		this.isLit = true;
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x000CC910 File Offset: 0x000CAB10
	protected override void OnSetActive(bool isActive)
	{
		base.OnSetActive(isActive);
		if (!isActive)
		{
			this.OnHolstered();
		}
	}

	// Token: 0x06003A27 RID: 14887 RVA: 0x000CC928 File Offset: 0x000CAB28
	public virtual void OnHolstered()
	{
		if (this.isLit)
		{
			this.Extinguish();
			this.realThrowTime = 0f;
			this.realIgniteTime = 0f;
			this.forceSecondaryTime = 0f;
			int num = 1;
			if (base.Consume(ref num))
			{
				base.inventory.RemoveItem(base.slot);
			}
		}
	}

	// Token: 0x06003A28 RID: 14888 RVA: 0x000CC988 File Offset: 0x000CAB88
	public void Extinguish()
	{
		this.isLit = false;
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
	}

	// Token: 0x06003A29 RID: 14889 RVA: 0x000CC9C0 File Offset: 0x000CABC0
	protected override void DestroyViewModel()
	{
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
		base.DestroyViewModel();
	}

	// Token: 0x06003A2A RID: 14890 RVA: 0x000CC9F8 File Offset: 0x000CABF8
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.realThrowTime != 0f && Time.time >= this.realThrowTime)
		{
			T datablock = this.datablock;
			datablock.DoActualThrow(base.itemRepresentation, this.iface as global::ITorchItem, base.viewModelInstance);
			this.realThrowTime = 0f;
		}
		if (this.realIgniteTime != 0f && Time.time >= this.realIgniteTime)
		{
			T datablock2 = this.datablock;
			datablock2.DoActualIgnite(base.itemRepresentation, this.iface as global::ITorchItem, base.viewModelInstance);
			this.realIgniteTime = 0f;
		}
		if (this.forceSecondaryTime != 0f && Time.time >= this.forceSecondaryTime)
		{
			this.SecondaryAttack(ref sample);
			this.forceSecondaryTime = 0f;
		}
	}
}
