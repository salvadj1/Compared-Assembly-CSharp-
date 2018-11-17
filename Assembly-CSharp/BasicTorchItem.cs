using System;
using UnityEngine;

// Token: 0x02000675 RID: 1653
public abstract class BasicTorchItem<T> : global::HeldItem<T> where T : global::BasicTorchItemDataBlock
{
	// Token: 0x060038C3 RID: 14531 RVA: 0x000C97D0 File Offset: 0x000C79D0
	protected BasicTorchItem(T db) : base(db)
	{
	}

	// Token: 0x17000ADD RID: 2781
	// (get) Token: 0x060038C4 RID: 14532 RVA: 0x000C97E4 File Offset: 0x000C79E4
	// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000C97EC File Offset: 0x000C79EC
	public bool isLit { get; set; }

	// Token: 0x060038C6 RID: 14534 RVA: 0x000C97F8 File Offset: 0x000C79F8
	public void Ignite()
	{
		this.isLit = true;
	}

	// Token: 0x17000ADE RID: 2782
	// (get) Token: 0x060038C7 RID: 14535 RVA: 0x000C9804 File Offset: 0x000C7A04
	// (set) Token: 0x060038C8 RID: 14536 RVA: 0x000C980C File Offset: 0x000C7A0C
	public GameObject light { get; set; }

	// Token: 0x060038C9 RID: 14537 RVA: 0x000C9818 File Offset: 0x000C7A18
	public virtual void Extinguish()
	{
		this.isLit = false;
		if (this.light)
		{
			Object.Destroy(this.light);
			this.light = null;
		}
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x000C9850 File Offset: 0x000C7A50
	protected override void OnSetActive(bool isActive)
	{
		if (isActive)
		{
			this.lastTickTime = Time.time;
			this.consumeAmount = 0f;
			base.OnSetActive(isActive);
			T datablock = this.datablock;
			datablock.DoActualIgnite(base.itemRepresentation, this.iface as global::IBasicTorchItem, base.viewModelInstance);
		}
		else
		{
			this.lastTickTime = -1f;
			T datablock2 = this.datablock;
			datablock2.DoActualExtinguish(base.itemRepresentation, this.iface as global::IBasicTorchItem, base.viewModelInstance);
			base.OnSetActive(isActive);
		}
	}

	// Token: 0x04001C30 RID: 7216
	private float lastTickTime = -1f;

	// Token: 0x04001C31 RID: 7217
	private float consumeAmount;
}
