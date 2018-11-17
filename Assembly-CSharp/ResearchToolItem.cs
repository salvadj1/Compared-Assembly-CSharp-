using System;
using Rust;

// Token: 0x020005E1 RID: 1505
public abstract class ResearchToolItem<T> : ToolItem<T> where T : ToolDataBlock
{
	// Token: 0x06003610 RID: 13840 RVA: 0x000C3868 File Offset: 0x000C1A68
	protected ResearchToolItem(T db) : base(db)
	{
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x000C3874 File Offset: 0x000C1A74
	public override InventoryItem.MergeResult TryCombine(IInventoryItem otherItem)
	{
		PlayerInventory playerInventory = base.inventory as PlayerInventory;
		if (!playerInventory || otherItem.inventory != playerInventory)
		{
			return InventoryItem.MergeResult.Failed;
		}
		ItemDataBlock datablock = otherItem.datablock;
		if (!datablock || !datablock.isResearchable)
		{
			Notice.Popup("", "You can't research this", 4f);
			return InventoryItem.MergeResult.Failed;
		}
		if (!playerInventory.AtWorkBench())
		{
			Notice.Popup("", "You must be at a workbench to do this.", 4f);
			return InventoryItem.MergeResult.Failed;
		}
		BlueprintDataBlock bp;
		if (!BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(otherItem.datablock, out bp))
		{
			Notice.Popup("", "You can't research this.. No Blueprint Available!...", 4f);
			return InventoryItem.MergeResult.Failed;
		}
		if (playerInventory.KnowsBP(bp))
		{
			Notice.Popup("", "You already know how to make this!", 4f);
			return InventoryItem.MergeResult.Failed;
		}
		return InventoryItem.MergeResult.Combined;
	}
}
