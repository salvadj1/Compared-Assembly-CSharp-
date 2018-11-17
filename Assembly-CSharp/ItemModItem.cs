using System;

// Token: 0x02000697 RID: 1687
public abstract class ItemModItem<T> : global::InventoryItem<T> where T : global::ItemModDataBlock
{
	// Token: 0x060039BE RID: 14782 RVA: 0x000CB728 File Offset: 0x000C9928
	protected ItemModItem(T db) : base(db)
	{
	}

	// Token: 0x060039BF RID: 14783 RVA: 0x000CB734 File Offset: 0x000C9934
	public override global::InventoryItem.MergeResult TryStack(global::IInventoryItem otherItem)
	{
		global::InventoryItem.MergeResult mergeResult = this.TryCombine(otherItem);
		if (mergeResult == global::InventoryItem.MergeResult.Failed)
		{
			return base.TryStack(otherItem);
		}
		return mergeResult;
	}

	// Token: 0x060039C0 RID: 14784 RVA: 0x000CB758 File Offset: 0x000C9958
	public override global::InventoryItem.MergeResult TryCombine(global::IInventoryItem otherItem)
	{
		global::IHeldItem heldItem = otherItem as global::IHeldItem;
		if (heldItem == null)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		if (heldItem.freeModSlots <= 0)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		if (!(otherItem.datablock is global::BulletWeaponDataBlock))
		{
			return base.TryCombine(otherItem);
		}
		global::IHeldItem heldItem2 = otherItem as global::IHeldItem;
		if (heldItem2.FindMod(this.datablock) != -1)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		return global::InventoryItem.MergeResult.Combined;
	}
}
