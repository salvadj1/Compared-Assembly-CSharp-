using System;

// Token: 0x020005D9 RID: 1497
public abstract class ItemModItem<T> : InventoryItem<T> where T : ItemModDataBlock
{
	// Token: 0x060035F6 RID: 13814 RVA: 0x000C34CC File Offset: 0x000C16CC
	protected ItemModItem(T db) : base(db)
	{
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x000C34D8 File Offset: 0x000C16D8
	public override InventoryItem.MergeResult TryStack(IInventoryItem otherItem)
	{
		InventoryItem.MergeResult mergeResult = this.TryCombine(otherItem);
		if (mergeResult == InventoryItem.MergeResult.Failed)
		{
			return base.TryStack(otherItem);
		}
		return mergeResult;
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x000C34FC File Offset: 0x000C16FC
	public override InventoryItem.MergeResult TryCombine(IInventoryItem otherItem)
	{
		IHeldItem heldItem = otherItem as IHeldItem;
		if (heldItem == null)
		{
			return InventoryItem.MergeResult.Failed;
		}
		if (heldItem.freeModSlots <= 0)
		{
			return InventoryItem.MergeResult.Failed;
		}
		if (!(otherItem.datablock is BulletWeaponDataBlock))
		{
			return base.TryCombine(otherItem);
		}
		IHeldItem heldItem2 = otherItem as IHeldItem;
		if (heldItem2.FindMod(this.datablock) != -1)
		{
			return InventoryItem.MergeResult.Failed;
		}
		return InventoryItem.MergeResult.Combined;
	}
}
