using System;

// Token: 0x020005B1 RID: 1457
public abstract class ArmorItem<T> : EquipmentItem<T> where T : ArmorDataBlock
{
	// Token: 0x060034DB RID: 13531 RVA: 0x000C1254 File Offset: 0x000BF454
	protected ArmorItem(T db) : base(db)
	{
	}

	// Token: 0x060034DC RID: 13532 RVA: 0x000C1260 File Offset: 0x000BF460
	public override void OnMovedTo(Inventory toInv, int toSlot)
	{
		base.OnMovedTo(toInv, toSlot);
		this.ArmorUpdate(toInv, toSlot);
	}

	// Token: 0x060034DD RID: 13533 RVA: 0x000C1274 File Offset: 0x000BF474
	public override void OnAddedTo(Inventory newInventory, int targetSlot)
	{
		base.OnAddedTo(newInventory, targetSlot);
		this.ArmorUpdate(newInventory, targetSlot);
	}

	// Token: 0x060034DE RID: 13534 RVA: 0x000C1288 File Offset: 0x000BF488
	public virtual void ArmorUpdate(Inventory belongInv, int belongSlot)
	{
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x000C128C File Offset: 0x000BF48C
	public override bool CanMoveToSlot(Inventory toinv, int toslot)
	{
		if (base.IsBroken())
		{
			PlayerInventory playerInventory = toinv as PlayerInventory;
			if (playerInventory != null && PlayerInventory.IsEquipmentSlot(toslot))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060034E0 RID: 13536 RVA: 0x000C12C8 File Offset: 0x000BF4C8
	public override void ConditionChanged(float oldCondition)
	{
	}
}
