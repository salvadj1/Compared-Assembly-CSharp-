using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class BlueprintDataBlock : ToolDataBlock
{
	// Token: 0x06002EC0 RID: 11968 RVA: 0x000B7680 File Offset: 0x000B5880
	public BlueprintDataBlock()
	{
		this.icon = "Items/BlueprintIcon";
	}

	// Token: 0x06002EC2 RID: 11970 RVA: 0x000B76AC File Offset: 0x000B58AC
	protected override IInventoryItem ConstructItem()
	{
		return new BlueprintDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002EC3 RID: 11971 RVA: 0x000B76B4 File Offset: 0x000B58B4
	public virtual void DefaultChancesInit()
	{
		if (!BlueprintDataBlock.chancesInitalized)
		{
			BlueprintDataBlock.chancesInitalized = true;
			BlueprintDataBlock.defaultSlotChances = new BlueprintDataBlock.SlotChanceWeightedEntry[5];
			BlueprintDataBlock.defaultSlotChances[0].numSlots = 1;
			BlueprintDataBlock.defaultSlotChances[1].numSlots = 2;
			BlueprintDataBlock.defaultSlotChances[2].numSlots = 3;
			BlueprintDataBlock.defaultSlotChances[3].numSlots = 4;
			BlueprintDataBlock.defaultSlotChances[4].numSlots = 5;
			BlueprintDataBlock.defaultSlotChances[0].weight = 50f;
			BlueprintDataBlock.defaultSlotChances[1].weight = 40f;
			BlueprintDataBlock.defaultSlotChances[2].weight = 30f;
			BlueprintDataBlock.defaultSlotChances[3].weight = 20f;
			BlueprintDataBlock.defaultSlotChances[4].weight = 10f;
		}
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x000B7774 File Offset: 0x000B5974
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x000B7780 File Offset: 0x000B5980
	public virtual int MaxAmount(Inventory workbenchInv)
	{
		int num = int.MaxValue;
		foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
		{
			int num2 = 0;
			IInventoryItem inventoryItem = workbenchInv.FindItem(ingredientEntry.Ingredient, out num2);
			if (inventoryItem != null)
			{
				int num3 = num2 / ingredientEntry.amount;
				if (num3 < num)
				{
					num = num3;
				}
			}
		}
		return (num != int.MaxValue) ? num : 0;
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x000B77F4 File Offset: 0x000B59F4
	public virtual bool CanWork(int amount, Inventory workbenchInv)
	{
		if (this.lastCanWorkResult == null)
		{
			this.lastCanWorkResult = new List<int>();
		}
		else
		{
			this.lastCanWorkResult.Clear();
		}
		if (this.lastCanWorkIngredientCount == null)
		{
			this.lastCanWorkIngredientCount = new List<int>(this.ingredients.Length);
		}
		else
		{
			this.lastCanWorkIngredientCount.Clear();
		}
		if (this.RequireWorkbench)
		{
			CraftingInventory component = workbenchInv.GetComponent<CraftingInventory>();
			if (!component || !component.AtWorkBench())
			{
				return false;
			}
		}
		foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
		{
			if (ingredientEntry.amount != 0)
			{
				int num = workbenchInv.CanConsume(ingredientEntry.Ingredient, ingredientEntry.amount * amount, this.lastCanWorkResult);
				if (num <= 0)
				{
					this.lastCanWorkResult.Clear();
					this.lastCanWorkIngredientCount.Clear();
					return false;
				}
				this.lastCanWorkIngredientCount.Add(num);
			}
		}
		return true;
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x000B78F8 File Offset: 0x000B5AF8
	public override float GetWorkDuration(IToolItem tool)
	{
		return this.craftingDuration;
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x000B7900 File Offset: 0x000B5B00
	public virtual bool CompleteWork(int amount, Inventory workbenchInv)
	{
		if (!this.CanWork(amount, workbenchInv))
		{
			return false;
		}
		int num = 0;
		for (int i = 0; i < this.ingredients.Length; i++)
		{
			int num2 = this.ingredients[i].amount * amount;
			if (num2 != 0)
			{
				int num3 = this.lastCanWorkIngredientCount[i];
				for (int j = 0; j < num3; j++)
				{
					int slot = this.lastCanWorkResult[num++];
					IInventoryItem inventoryItem;
					if (workbenchInv.GetItem(slot, out inventoryItem) && inventoryItem.Consume(ref num2))
					{
						workbenchInv.RemoveItem(slot);
					}
				}
			}
		}
		workbenchInv.AddItemAmount(this.resultItem, amount * this.numResultItem);
		return true;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x000B79C4 File Offset: 0x000B5BC4
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Study;
		}
		return offset;
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x000B79F4 File Offset: 0x000B5BF4
	public override InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option != InventoryItem.MenuItem.Study)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x000B7A1C File Offset: 0x000B5C1C
	public virtual void UseItem(IBlueprintItem item)
	{
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x000B7A20 File Offset: 0x000B5C20
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddSectionTitle("Ingredients", 15f);
		for (int i = 0; i < this.ingredients.Length; i++)
		{
			string text = this.ingredients[i].Ingredient.name;
			if (this.ingredients[i].amount > 1)
			{
				text = text + " x" + this.ingredients[i].amount;
			}
			infoWindow.AddBasicLabel(text, 15f);
		}
		infoWindow.AddSectionTitle("Result Item", 15f);
		infoWindow.AddBasicLabel(this.resultItem.name, 15f);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x000B7AF4 File Offset: 0x000B5CF4
	public override string GetItemDescription()
	{
		return "This is an item Blueprint. Study it to learn how to craft the item it represents!";
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x000B7AFC File Offset: 0x000B5CFC
	public static bool FindBlueprintForItem<T>(ItemDataBlock item, out T blueprint) where T : BlueprintDataBlock
	{
		foreach (ItemDataBlock itemDataBlock in DatablockDictionary.All)
		{
			T t = itemDataBlock as T;
			if (t && t.resultItem == item)
			{
				blueprint = t;
				return true;
			}
		}
		Debug.LogWarning("Could not find blueprint foritem");
		blueprint = (T)((object)null);
		return false;
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x000B7B7C File Offset: 0x000B5D7C
	public static bool FindBlueprintForItem(ItemDataBlock item)
	{
		BlueprintDataBlock blueprintDataBlock;
		return BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(item, out blueprintDataBlock);
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x000B7B94 File Offset: 0x000B5D94
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.craftingDuration, new object[0]);
		if (this.ingredients != null)
		{
			foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
			{
				if (ingredientEntry != null)
				{
					if (ingredientEntry.Ingredient)
					{
						stream.Write<int>(ingredientEntry.Ingredient.uniqueID ^ ingredientEntry.amount, new object[0]);
					}
					else
					{
						stream.Write<int>(ingredientEntry.amount, new object[0]);
					}
				}
			}
		}
		if (this.resultItem)
		{
			stream.Write<int>(this.resultItem.uniqueID, new object[0]);
		}
		if (BlueprintDataBlock.defaultSlotChances != null)
		{
			foreach (BlueprintDataBlock.SlotChanceWeightedEntry slotChanceWeightedEntry in BlueprintDataBlock.defaultSlotChances)
			{
				stream.Write<float>(slotChanceWeightedEntry.weight, new object[0]);
			}
			foreach (BlueprintDataBlock.SlotChanceWeightedEntry slotChanceWeightedEntry2 in BlueprintDataBlock.defaultSlotChances)
			{
				stream.Write<byte>(slotChanceWeightedEntry2.numSlots, new object[0]);
			}
		}
	}

	// Token: 0x04001918 RID: 6424
	public ItemDataBlock resultItem;

	// Token: 0x04001919 RID: 6425
	public int numResultItem = 1;

	// Token: 0x0400191A RID: 6426
	public BlueprintDataBlock.IngredientEntry[] ingredients;

	// Token: 0x0400191B RID: 6427
	public static BlueprintDataBlock.SlotChanceWeightedEntry[] defaultSlotChances;

	// Token: 0x0400191C RID: 6428
	public static bool chancesInitalized;

	// Token: 0x0400191D RID: 6429
	public float craftingDuration = 20f;

	// Token: 0x0400191E RID: 6430
	public bool RequireWorkbench;

	// Token: 0x0400191F RID: 6431
	private List<int> lastCanWorkResult;

	// Token: 0x04001920 RID: 6432
	private List<int> lastCanWorkIngredientCount;

	// Token: 0x0200055C RID: 1372
	private sealed class ITEM_TYPE : BlueprintItem<BlueprintDataBlock>, IBlueprintItem, IInventoryItem, IToolItem
	{
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B7CD4 File Offset: 0x000B5ED4
		public ITEM_TYPE(BlueprintDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000B7CE0 File Offset: 0x000B5EE0
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000B7CE8 File Offset: 0x000B5EE8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000B7CF0 File Offset: 0x000B5EF0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000B7CF8 File Offset: 0x000B5EF8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000B7D00 File Offset: 0x000B5F00
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000B7D0C File Offset: 0x000B5F0C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000B7D18 File Offset: 0x000B5F18
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000B7D24 File Offset: 0x000B5F24
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000B7D30 File Offset: 0x000B5F30
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000B7D3C File Offset: 0x000B5F3C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000B7D48 File Offset: 0x000B5F48
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000B7D54 File Offset: 0x000B5F54
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000B7D60 File Offset: 0x000B5F60
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000B7D68 File Offset: 0x000B5F68
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000B7D70 File Offset: 0x000B5F70
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000B7D78 File Offset: 0x000B5F78
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000B7D80 File Offset: 0x000B5F80
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000B7D88 File Offset: 0x000B5F88
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000B7D90 File Offset: 0x000B5F90
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000B7D98 File Offset: 0x000B5F98
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000B7DA0 File Offset: 0x000B5FA0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B7DAC File Offset: 0x000B5FAC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B7DB4 File Offset: 0x000B5FB4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000B7DBC File Offset: 0x000B5FBC
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000B7DC4 File Offset: 0x000B5FC4
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000B7DCC File Offset: 0x000B5FCC
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000B7DD4 File Offset: 0x000B5FD4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000B7DDC File Offset: 0x000B5FDC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200055D RID: 1373
	[Serializable]
	public class IngredientEntry
	{
		// Token: 0x04001921 RID: 6433
		public ItemDataBlock Ingredient;

		// Token: 0x04001922 RID: 6434
		public int amount;
	}

	// Token: 0x0200055E RID: 1374
	[Serializable]
	public class SlotChanceWeightedEntry : WeightSelection.WeightedEntry
	{
		// Token: 0x04001923 RID: 6435
		public byte numSlots;
	}
}
