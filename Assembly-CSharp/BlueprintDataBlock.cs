using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000619 RID: 1561
public class BlueprintDataBlock : global::ToolDataBlock
{
	// Token: 0x06003288 RID: 12936 RVA: 0x000BF8DC File Offset: 0x000BDADC
	public BlueprintDataBlock()
	{
		this.icon = "Items/BlueprintIcon";
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x000BF908 File Offset: 0x000BDB08
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BlueprintDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x000BF910 File Offset: 0x000BDB10
	public virtual void DefaultChancesInit()
	{
		if (!global::BlueprintDataBlock.chancesInitalized)
		{
			global::BlueprintDataBlock.chancesInitalized = true;
			global::BlueprintDataBlock.defaultSlotChances = new global::BlueprintDataBlock.SlotChanceWeightedEntry[5];
			global::BlueprintDataBlock.defaultSlotChances[0].numSlots = 1;
			global::BlueprintDataBlock.defaultSlotChances[1].numSlots = 2;
			global::BlueprintDataBlock.defaultSlotChances[2].numSlots = 3;
			global::BlueprintDataBlock.defaultSlotChances[3].numSlots = 4;
			global::BlueprintDataBlock.defaultSlotChances[4].numSlots = 5;
			global::BlueprintDataBlock.defaultSlotChances[0].weight = 50f;
			global::BlueprintDataBlock.defaultSlotChances[1].weight = 40f;
			global::BlueprintDataBlock.defaultSlotChances[2].weight = 30f;
			global::BlueprintDataBlock.defaultSlotChances[3].weight = 20f;
			global::BlueprintDataBlock.defaultSlotChances[4].weight = 10f;
		}
	}

	// Token: 0x0600328C RID: 12940 RVA: 0x000BF9D0 File Offset: 0x000BDBD0
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x0600328D RID: 12941 RVA: 0x000BF9DC File Offset: 0x000BDBDC
	public virtual int MaxAmount(global::Inventory workbenchInv)
	{
		int num = int.MaxValue;
		foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
		{
			int num2 = 0;
			global::IInventoryItem inventoryItem = workbenchInv.FindItem(ingredientEntry.Ingredient, out num2);
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

	// Token: 0x0600328E RID: 12942 RVA: 0x000BFA50 File Offset: 0x000BDC50
	public virtual bool CanWork(int amount, global::Inventory workbenchInv)
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
			global::CraftingInventory component = workbenchInv.GetComponent<global::CraftingInventory>();
			if (!component || !component.AtWorkBench())
			{
				return false;
			}
		}
		foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
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

	// Token: 0x0600328F RID: 12943 RVA: 0x000BFB54 File Offset: 0x000BDD54
	public override float GetWorkDuration(global::IToolItem tool)
	{
		return this.craftingDuration;
	}

	// Token: 0x06003290 RID: 12944 RVA: 0x000BFB5C File Offset: 0x000BDD5C
	public virtual bool CompleteWork(int amount, global::Inventory workbenchInv)
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
					global::IInventoryItem inventoryItem;
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

	// Token: 0x06003291 RID: 12945 RVA: 0x000BFC20 File Offset: 0x000BDE20
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Study;
		}
		return offset;
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x000BFC50 File Offset: 0x000BDE50
	public override global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option != global::InventoryItem.MenuItem.Study)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return global::InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x000BFC78 File Offset: 0x000BDE78
	public virtual void UseItem(global::IBlueprintItem item)
	{
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x000BFC7C File Offset: 0x000BDE7C
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
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

	// Token: 0x06003295 RID: 12949 RVA: 0x000BFD50 File Offset: 0x000BDF50
	public override string GetItemDescription()
	{
		return "This is an item Blueprint. Study it to learn how to craft the item it represents!";
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x000BFD58 File Offset: 0x000BDF58
	public static bool FindBlueprintForItem<T>(global::ItemDataBlock item, out T blueprint) where T : global::BlueprintDataBlock
	{
		foreach (global::ItemDataBlock itemDataBlock in global::DatablockDictionary.All)
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

	// Token: 0x06003297 RID: 12951 RVA: 0x000BFDD8 File Offset: 0x000BDFD8
	public static bool FindBlueprintForItem(global::ItemDataBlock item)
	{
		global::BlueprintDataBlock blueprintDataBlock;
		return global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(item, out blueprintDataBlock);
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x000BFDF0 File Offset: 0x000BDFF0
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.craftingDuration, new object[0]);
		if (this.ingredients != null)
		{
			foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in this.ingredients)
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
		if (global::BlueprintDataBlock.defaultSlotChances != null)
		{
			foreach (global::BlueprintDataBlock.SlotChanceWeightedEntry slotChanceWeightedEntry in global::BlueprintDataBlock.defaultSlotChances)
			{
				stream.Write<float>(slotChanceWeightedEntry.weight, new object[0]);
			}
			foreach (global::BlueprintDataBlock.SlotChanceWeightedEntry slotChanceWeightedEntry2 in global::BlueprintDataBlock.defaultSlotChances)
			{
				stream.Write<byte>(slotChanceWeightedEntry2.numSlots, new object[0]);
			}
		}
	}

	// Token: 0x04001AE9 RID: 6889
	public global::ItemDataBlock resultItem;

	// Token: 0x04001AEA RID: 6890
	public int numResultItem = 1;

	// Token: 0x04001AEB RID: 6891
	public global::BlueprintDataBlock.IngredientEntry[] ingredients;

	// Token: 0x04001AEC RID: 6892
	public static global::BlueprintDataBlock.SlotChanceWeightedEntry[] defaultSlotChances;

	// Token: 0x04001AED RID: 6893
	public static bool chancesInitalized;

	// Token: 0x04001AEE RID: 6894
	public float craftingDuration = 20f;

	// Token: 0x04001AEF RID: 6895
	public bool RequireWorkbench;

	// Token: 0x04001AF0 RID: 6896
	private List<int> lastCanWorkResult;

	// Token: 0x04001AF1 RID: 6897
	private List<int> lastCanWorkIngredientCount;

	// Token: 0x0200061A RID: 1562
	private sealed class ITEM_TYPE : global::BlueprintItem<global::BlueprintDataBlock>, global::IBlueprintItem, global::IInventoryItem, global::IToolItem
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x000BFF30 File Offset: 0x000BE130
		public ITEM_TYPE(global::BlueprintDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000BFF3C File Offset: 0x000BE13C
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000BFF44 File Offset: 0x000BE144
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000BFF4C File Offset: 0x000BE14C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000BFF54 File Offset: 0x000BE154
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000BFF5C File Offset: 0x000BE15C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000BFF68 File Offset: 0x000BE168
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000BFF74 File Offset: 0x000BE174
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000BFF80 File Offset: 0x000BE180
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000BFF8C File Offset: 0x000BE18C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000BFF98 File Offset: 0x000BE198
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000BFFA4 File Offset: 0x000BE1A4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000BFFB0 File Offset: 0x000BE1B0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000BFFBC File Offset: 0x000BE1BC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000BFFC4 File Offset: 0x000BE1C4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000BFFCC File Offset: 0x000BE1CC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000BFFD4 File Offset: 0x000BE1D4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000BFFDC File Offset: 0x000BE1DC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000BFFE4 File Offset: 0x000BE1E4
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000BFFEC File Offset: 0x000BE1EC
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000BFFF4 File Offset: 0x000BE1F4
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000BFFFC File Offset: 0x000BE1FC
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000C0008 File Offset: 0x000BE208
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000C0010 File Offset: 0x000BE210
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000C0018 File Offset: 0x000BE218
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000C0020 File Offset: 0x000BE220
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000C0028 File Offset: 0x000BE228
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000C0030 File Offset: 0x000BE230
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000C0038 File Offset: 0x000BE238
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200061B RID: 1563
	[Serializable]
	public class IngredientEntry
	{
		// Token: 0x04001AF2 RID: 6898
		public global::ItemDataBlock Ingredient;

		// Token: 0x04001AF3 RID: 6899
		public int amount;
	}

	// Token: 0x0200061C RID: 1564
	[Serializable]
	public class SlotChanceWeightedEntry : global::WeightSelection.WeightedEntry
	{
		// Token: 0x04001AF4 RID: 6900
		public byte numSlots;
	}
}
