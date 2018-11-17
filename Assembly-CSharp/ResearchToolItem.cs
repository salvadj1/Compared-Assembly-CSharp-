using System;
using Rust;

// Token: 0x0200069F RID: 1695
public abstract class ResearchToolItem<T> : global::ToolItem<T> where T : global::ToolDataBlock
{
	// Token: 0x060039D8 RID: 14808 RVA: 0x000CBAC4 File Offset: 0x000C9CC4
	protected ResearchToolItem(T db) : base(db)
	{
	}

	// Token: 0x060039D9 RID: 14809 RVA: 0x000CBAD0 File Offset: 0x000C9CD0
	public override global::InventoryItem.MergeResult TryCombine(global::IInventoryItem otherItem)
	{
		global::PlayerInventory playerInventory = base.inventory as global::PlayerInventory;
		if (!playerInventory || otherItem.inventory != playerInventory)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		global::ItemDataBlock datablock = otherItem.datablock;
		if (!datablock || !datablock.isResearchable)
		{
			Rust.Notice.Popup("", "You can't research this", 4f);
			return global::InventoryItem.MergeResult.Failed;
		}
		if (!playerInventory.AtWorkBench())
		{
			Rust.Notice.Popup("", "You must be at a workbench to do this.", 4f);
			return global::InventoryItem.MergeResult.Failed;
		}
		global::BlueprintDataBlock bp;
		if (!global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(otherItem.datablock, out bp))
		{
			Rust.Notice.Popup("", "You can't research this.. No Blueprint Available!...", 4f);
			return global::InventoryItem.MergeResult.Failed;
		}
		if (playerInventory.KnowsBP(bp))
		{
			Rust.Notice.Popup("", "You already know how to make this!", 4f);
			return global::InventoryItem.MergeResult.Failed;
		}
		return global::InventoryItem.MergeResult.Combined;
	}
}
