using System;
using UnityEngine;

// Token: 0x020005ED RID: 1517
public abstract class TorchItem<T> : ThrowableItem<T> where T : TorchItemDataBlock
{
	// Token: 0x06003651 RID: 13905 RVA: 0x000C4630 File Offset: 0x000C2830
	protected TorchItem(T db) : base(db)
	{
	}

	// Token: 0x17000ACF RID: 2767
	// (get) Token: 0x06003652 RID: 13906 RVA: 0x000C463C File Offset: 0x000C283C
	// (set) Token: 0x06003653 RID: 13907 RVA: 0x000C4644 File Offset: 0x000C2844
	public bool isLit { get; protected set; }

	// Token: 0x17000AD0 RID: 2768
	// (get) Token: 0x06003654 RID: 13908 RVA: 0x000C4650 File Offset: 0x000C2850
	// (set) Token: 0x06003655 RID: 13909 RVA: 0x000C4658 File Offset: 0x000C2858
	public float realThrowTime { get; set; }

	// Token: 0x17000AD1 RID: 2769
	// (get) Token: 0x06003656 RID: 13910 RVA: 0x000C4664 File Offset: 0x000C2864
	// (set) Token: 0x06003657 RID: 13911 RVA: 0x000C466C File Offset: 0x000C286C
	public float realIgniteTime { get; set; }

	// Token: 0x17000AD2 RID: 2770
	// (get) Token: 0x06003658 RID: 13912 RVA: 0x000C4678 File Offset: 0x000C2878
	// (set) Token: 0x06003659 RID: 13913 RVA: 0x000C4680 File Offset: 0x000C2880
	public float forceSecondaryTime { get; set; }

	// Token: 0x17000AD3 RID: 2771
	// (get) Token: 0x0600365A RID: 13914 RVA: 0x000C468C File Offset: 0x000C288C
	// (set) Token: 0x0600365B RID: 13915 RVA: 0x000C4694 File Offset: 0x000C2894
	public GameObject light { get; set; }

	// Token: 0x0600365C RID: 13916 RVA: 0x000C46A0 File Offset: 0x000C28A0
	public bool IsIgnited()
	{
		return this.isLit;
	}

	// Token: 0x0600365D RID: 13917 RVA: 0x000C46A8 File Offset: 0x000C28A8
	public void Ignite()
	{
		this.isLit = true;
	}

	// Token: 0x0600365E RID: 13918 RVA: 0x000C46B4 File Offset: 0x000C28B4
	protected override void OnSetActive(bool isActive)
	{
		base.OnSetActive(isActive);
		if (!isActive)
		{
			this.OnHolstered();
		}
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x000C46CC File Offset: 0x000C28CC
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

	// Token: 0x06003660 RID: 13920 RVA: 0x000C472C File Offset: 0x000C292C
	public void Extinguish()
	{
		this.isLit = false;
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x000C4764 File Offset: 0x000C2964
	protected override void DestroyViewModel()
	{
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
		base.DestroyViewModel();
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x000C479C File Offset: 0x000C299C
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (this.realThrowTime != 0f && Time.time >= this.realThrowTime)
		{
			T datablock = this.datablock;
			datablock.DoActualThrow(base.itemRepresentation, this.iface as ITorchItem, base.viewModelInstance);
			this.realThrowTime = 0f;
		}
		if (this.realIgniteTime != 0f && Time.time >= this.realIgniteTime)
		{
			T datablock2 = this.datablock;
			datablock2.DoActualIgnite(base.itemRepresentation, this.iface as ITorchItem, base.viewModelInstance);
			this.realIgniteTime = 0f;
		}
		if (this.forceSecondaryTime != 0f && Time.time >= this.forceSecondaryTime)
		{
			this.SecondaryAttack(ref sample);
			this.forceSecondaryTime = 0f;
		}
	}
}
