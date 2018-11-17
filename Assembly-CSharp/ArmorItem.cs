using System;

// Token: 0x0200066F RID: 1647
public abstract class ArmorItem<T> : global::EquipmentItem<T> where T : global::ArmorDataBlock
{
	// Token: 0x060038A3 RID: 14499 RVA: 0x000C94B0 File Offset: 0x000C76B0
	protected ArmorItem(T db) : base(db)
	{
	}

	// Token: 0x060038A4 RID: 14500 RVA: 0x000C94BC File Offset: 0x000C76BC
	public override void OnMovedTo(global::Inventory toInv, int toSlot)
	{
		base.OnMovedTo(toInv, toSlot);
		this.ArmorUpdate(toInv, toSlot);
	}

	// Token: 0x060038A5 RID: 14501 RVA: 0x000C94D0 File Offset: 0x000C76D0
	public override void OnAddedTo(global::Inventory newInventory, int targetSlot)
	{
		base.OnAddedTo(newInventory, targetSlot);
		this.ArmorUpdate(newInventory, targetSlot);
	}

	// Token: 0x060038A6 RID: 14502 RVA: 0x000C94E4 File Offset: 0x000C76E4
	public virtual void ArmorUpdate(global::Inventory belongInv, int belongSlot)
	{
	}

	// Token: 0x060038A7 RID: 14503 RVA: 0x000C94E8 File Offset: 0x000C76E8
	public override bool CanMoveToSlot(global::Inventory toinv, int toslot)
	{
		if (base.IsBroken())
		{
			global::PlayerInventory playerInventory = toinv as global::PlayerInventory;
			if (playerInventory != null && global::PlayerInventory.IsEquipmentSlot(toslot))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060038A8 RID: 14504 RVA: 0x000C9524 File Offset: 0x000C7724
	public override void ConditionChanged(float oldCondition)
	{
	}
}
