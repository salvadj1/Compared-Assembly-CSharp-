using System;
using uLink;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
public abstract class InventoryItem<DB> : InventoryItem where DB : ItemDataBlock
{
	// Token: 0x060035E6 RID: 13798 RVA: 0x000C30A8 File Offset: 0x000C12A8
	protected InventoryItem(DB datablock) : base(datablock)
	{
		this.datablock = datablock;
	}

	// Token: 0x17000AB3 RID: 2739
	// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000C30C0 File Offset: 0x000C12C0
	public bool doNotSave
	{
		get
		{
			bool result;
			if (this.datablock)
			{
				DB db = this.datablock;
				result = db.doesNotSave;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}

	// Token: 0x17000AB4 RID: 2740
	// (get) Token: 0x060035E8 RID: 13800 RVA: 0x000C30FC File Offset: 0x000C12FC
	public override string toolTip
	{
		get
		{
			string conditionString = this.GetConditionString();
			if (string.IsNullOrEmpty(conditionString))
			{
				DB db = this.datablock;
				return db.name;
			}
			string str = conditionString;
			string str2 = " ";
			DB db2 = this.datablock;
			return str + str2 + db2.name;
		}
	}

	// Token: 0x17000AB5 RID: 2741
	// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000C3150 File Offset: 0x000C1350
	protected sealed override ItemDataBlock __infrastructure_db
	{
		get
		{
			return this.datablock;
		}
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x000C3160 File Offset: 0x000C1360
	protected override void OnBitStreamWrite(BitStream stream)
	{
		InventoryItem.SerializeSharedProperties(stream, this, this.datablock);
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x000C3174 File Offset: 0x000C1374
	protected override void OnBitStreamRead(BitStream stream)
	{
		InventoryItem.DeserializeSharedProperties(stream, this, this.datablock);
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x000C3188 File Offset: 0x000C1388
	public override void OnMovedTo(Inventory inv, int slot)
	{
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x000C318C File Offset: 0x000C138C
	public override InventoryItem.MergeResult TryStack(IInventoryItem other)
	{
		int uses = base.uses;
		if (uses == 0)
		{
			return InventoryItem.MergeResult.Failed;
		}
		DB db = other.datablock as DB;
		if (db && db == this.datablock)
		{
			int uses2 = other.uses;
			if (uses2 == this.maxUses)
			{
				return InventoryItem.MergeResult.Failed;
			}
			DB db2 = this.datablock;
			if (db2.IsSplittable())
			{
				int num = other.AddUses(uses);
				if (num == 0)
				{
					return InventoryItem.MergeResult.Failed;
				}
				bool flag = this.Consume(ref num);
				if (flag)
				{
					this.inventory.RemoveItem(this.slot);
				}
				return InventoryItem.MergeResult.Merged;
			}
		}
		return InventoryItem.MergeResult.Failed;
	}

	// Token: 0x060035EE RID: 13806 RVA: 0x000C3254 File Offset: 0x000C1454
	public override InventoryItem.MenuItemResult OnMenuOption(InventoryItem.MenuItem option)
	{
		DB db = this.datablock;
		InventoryItem.MenuItemResult menuItemResult = db.ExecuteMenuOption(option, this.iface);
		InventoryItem.MenuItemResult menuItemResult2 = menuItemResult;
		if (menuItemResult2 == InventoryItem.MenuItemResult.Unhandled || menuItemResult2 == InventoryItem.MenuItemResult.DoneOnServer)
		{
			base.inventory.NetworkItemAction(base.slot, option);
		}
		return menuItemResult;
	}

	// Token: 0x060035EF RID: 13807 RVA: 0x000C32AC File Offset: 0x000C14AC
	public override InventoryItem.MergeResult TryCombine(IInventoryItem other)
	{
		ItemDataBlock db = other.datablock;
		DB db2 = this.datablock;
		ItemDataBlock.CombineRecipe matchingRecipe = db2.GetMatchingRecipe(db);
		if (matchingRecipe == null)
		{
			return InventoryItem.MergeResult.Failed;
		}
		int uses = other.uses;
		if (uses < matchingRecipe.amountToLoseOther)
		{
			return InventoryItem.MergeResult.Failed;
		}
		if (base.uses < matchingRecipe.amountToLose)
		{
			return InventoryItem.MergeResult.Failed;
		}
		Inventory inventory = other.inventory;
		int num = base.uses / matchingRecipe.amountToLose;
		int num2 = uses / matchingRecipe.amountToLoseOther;
		int num3 = Mathf.Min(num, num2);
		int num4 = 0;
		if (matchingRecipe.resultItem.IsSplittable())
		{
			num4 = Mathf.CeilToInt((float)num3 / (float)num4);
		}
		else
		{
			num4 = num3;
		}
		int vacantSlotCount = inventory.vacantSlotCount;
		if (num4 > vacantSlotCount)
		{
			return InventoryItem.MergeResult.Failed;
		}
		int num5 = num3 * matchingRecipe.amountToLoseOther;
		if (other.Consume(ref num5))
		{
			inventory.RemoveItem(other.slot);
		}
		inventory.AddItemAmount(matchingRecipe.resultItem, num3, Inventory.AmountMode.Default);
		int num6 = num3 * matchingRecipe.amountToLose;
		if (base.Consume(ref num6))
		{
			base.inventory.RemoveItem(base.slot);
		}
		return InventoryItem.MergeResult.Failed;
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x000C33D4 File Offset: 0x000C15D4
	public override string ToString()
	{
		Inventory inventory = base.inventory;
		string text;
		if (this.datablock)
		{
			DB db = this.datablock;
			text = db.name;
		}
		else
		{
			text = InventoryItem<DB>.tostringhelper.nullDatablockString;
		}
		string text2 = text;
		if (inventory)
		{
			return string.Format("[{0} (on {1}[{2}]) with ({3} uses)]", new object[]
			{
				text2,
				inventory.name,
				base.slot,
				base.uses
			});
		}
		return string.Format("[{0} (unbound slot {1}) with ({2} uses)]", text2, base.slot, base.uses);
	}

	// Token: 0x04001A9A RID: 6810
	public new readonly DB datablock;

	// Token: 0x020005D5 RID: 1493
	private static class tostringhelper
	{
		// Token: 0x04001A9B RID: 6811
		public static readonly string nullDatablockString = string.Format("NULL<{0}>", typeof(DB).FullName);
	}
}
