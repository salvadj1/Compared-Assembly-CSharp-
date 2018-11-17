using System;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
public abstract class BasicTorchItem<T> : HeldItem<T> where T : BasicTorchItemDataBlock
{
	// Token: 0x060034FB RID: 13563 RVA: 0x000C1574 File Offset: 0x000BF774
	protected BasicTorchItem(T db) : base(db)
	{
	}

	// Token: 0x17000A67 RID: 2663
	// (get) Token: 0x060034FC RID: 13564 RVA: 0x000C1588 File Offset: 0x000BF788
	// (set) Token: 0x060034FD RID: 13565 RVA: 0x000C1590 File Offset: 0x000BF790
	public bool isLit { get; set; }

	// Token: 0x060034FE RID: 13566 RVA: 0x000C159C File Offset: 0x000BF79C
	public void Ignite()
	{
		this.isLit = true;
	}

	// Token: 0x17000A68 RID: 2664
	// (get) Token: 0x060034FF RID: 13567 RVA: 0x000C15A8 File Offset: 0x000BF7A8
	// (set) Token: 0x06003500 RID: 13568 RVA: 0x000C15B0 File Offset: 0x000BF7B0
	public GameObject light { get; set; }

	// Token: 0x06003501 RID: 13569 RVA: 0x000C15BC File Offset: 0x000BF7BC
	public virtual void Extinguish()
	{
		this.isLit = false;
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
	}

	// Token: 0x06003502 RID: 13570 RVA: 0x000C15F4 File Offset: 0x000BF7F4
	protected override void OnSetActive(bool isActive)
	{
		if (isActive)
		{
			this.lastTickTime = Time.time;
			this.consumeAmount = 0f;
			base.OnSetActive(isActive);
			T datablock = this.datablock;
			datablock.DoActualIgnite(base.itemRepresentation, this.iface as IBasicTorchItem, base.viewModelInstance);
		}
		else
		{
			this.lastTickTime = -1f;
			T datablock2 = this.datablock;
			datablock2.DoActualExtinguish(base.itemRepresentation, this.iface as IBasicTorchItem, base.viewModelInstance);
			base.OnSetActive(isActive);
		}
	}

	// Token: 0x04001A5F RID: 6751
	private float lastTickTime = -1f;

	// Token: 0x04001A60 RID: 6752
	private float consumeAmount;
}
