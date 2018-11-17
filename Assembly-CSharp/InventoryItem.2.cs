using System;
using uLink;
using UnityEngine;

// Token: 0x02000692 RID: 1682
public abstract class InventoryItem<DB> : global::InventoryItem where DB : global::ItemDataBlock
{
	// Token: 0x060039AE RID: 14766 RVA: 0x000CB304 File Offset: 0x000C9504
	protected InventoryItem(DB datablock) : base(datablock)
	{
		this.datablock = datablock;
	}

	// Token: 0x17000B29 RID: 2857
	// (get) Token: 0x060039AF RID: 14767 RVA: 0x000CB31C File Offset: 0x000C951C
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

	// Token: 0x17000B2A RID: 2858
	// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000CB358 File Offset: 0x000C9558
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

	// Token: 0x17000B2B RID: 2859
	// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000CB3AC File Offset: 0x000C95AC
	protected sealed override global::ItemDataBlock __infrastructure_db
	{
		get
		{
			return this.datablock;
		}
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x000CB3BC File Offset: 0x000C95BC
	protected override void OnBitStreamWrite(BitStream stream)
	{
		global::InventoryItem.SerializeSharedProperties(stream, this, this.datablock);
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x000CB3D0 File Offset: 0x000C95D0
	protected override void OnBitStreamRead(BitStream stream)
	{
		global::InventoryItem.DeserializeSharedProperties(stream, this, this.datablock);
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x000CB3E4 File Offset: 0x000C95E4
	public override void OnMovedTo(global::Inventory inv, int slot)
	{
	}

	// Token: 0x060039B5 RID: 14773 RVA: 0x000CB3E8 File Offset: 0x000C95E8
	public override global::InventoryItem.MergeResult TryStack(global::IInventoryItem other)
	{
		int uses = base.uses;
		if (uses == 0)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		DB db = other.datablock as DB;
		if (db && db == this.datablock)
		{
			int uses2 = other.uses;
			if (uses2 == this.maxUses)
			{
				return global::InventoryItem.MergeResult.Failed;
			}
			DB db2 = this.datablock;
			if (db2.IsSplittable())
			{
				int num = other.AddUses(uses);
				if (num == 0)
				{
					return global::InventoryItem.MergeResult.Failed;
				}
				bool flag = this.Consume(ref num);
				if (flag)
				{
					this.inventory.RemoveItem(this.slot);
				}
				return global::InventoryItem.MergeResult.Merged;
			}
		}
		return global::InventoryItem.MergeResult.Failed;
	}

	// Token: 0x060039B6 RID: 14774 RVA: 0x000CB4B0 File Offset: 0x000C96B0
	public override global::InventoryItem.MenuItemResult OnMenuOption(global::InventoryItem.MenuItem option)
	{
		DB db = this.datablock;
		global::InventoryItem.MenuItemResult menuItemResult = db.ExecuteMenuOption(option, this.iface);
		global::InventoryItem.MenuItemResult menuItemResult2 = menuItemResult;
		if (menuItemResult2 == global::InventoryItem.MenuItemResult.Unhandled || menuItemResult2 == global::InventoryItem.MenuItemResult.DoneOnServer)
		{
			base.inventory.NetworkItemAction(base.slot, option);
		}
		return menuItemResult;
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x000CB508 File Offset: 0x000C9708
	public override global::InventoryItem.MergeResult TryCombine(global::IInventoryItem other)
	{
		global::ItemDataBlock db = other.datablock;
		DB db2 = this.datablock;
		global::ItemDataBlock.CombineRecipe matchingRecipe = db2.GetMatchingRecipe(db);
		if (matchingRecipe == null)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		int uses = other.uses;
		if (uses < matchingRecipe.amountToLoseOther)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		if (base.uses < matchingRecipe.amountToLose)
		{
			return global::InventoryItem.MergeResult.Failed;
		}
		global::Inventory inventory = other.inventory;
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
			return global::InventoryItem.MergeResult.Failed;
		}
		int num5 = num3 * matchingRecipe.amountToLoseOther;
		if (other.Consume(ref num5))
		{
			inventory.RemoveItem(other.slot);
		}
		inventory.AddItemAmount(matchingRecipe.resultItem, num3, global::Inventory.AmountMode.Default);
		int num6 = num3 * matchingRecipe.amountToLose;
		if (base.Consume(ref num6))
		{
			base.inventory.RemoveItem(base.slot);
		}
		return global::InventoryItem.MergeResult.Failed;
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x000CB630 File Offset: 0x000C9830
	public override string ToString()
	{
		global::Inventory inventory = base.inventory;
		string text;
		if (this.datablock)
		{
			DB db = this.datablock;
			text = db.name;
		}
		else
		{
			text = global::InventoryItem<DB>.tostringhelper.nullDatablockString;
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

	// Token: 0x04001C6B RID: 7275
	public new readonly DB datablock;

	// Token: 0x02000693 RID: 1683
	private static class tostringhelper
	{
		// Token: 0x04001C6C RID: 7276
		public static readonly string nullDatablockString = string.Format("NULL<{0}>", typeof(DB).FullName);
	}
}
