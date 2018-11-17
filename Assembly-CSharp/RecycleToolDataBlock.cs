using System;
using uLink;
using UnityEngine;

// Token: 0x02000654 RID: 1620
public class RecycleToolDataBlock : global::ToolDataBlock
{
	// Token: 0x060036F5 RID: 14069 RVA: 0x000C5E20 File Offset: 0x000C4020
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::RecycleToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x000C5E28 File Offset: 0x000C4028
	public override bool CanWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		if (workbenchInv.occupiedSlotCount > 2)
		{
			Debug.Log("Too many items for recycle");
			return false;
		}
		global::IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		return firstItemNotTool.datablock.isRecycleable && global::BlueprintDataBlock.FindBlueprintForItem(firstItemNotTool.datablock);
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x000C5E7C File Offset: 0x000C407C
	public override float GetWorkDuration(global::IToolItem tool)
	{
		return 15f;
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x000C5E84 File Offset: 0x000C4084
	public override bool CompleteWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		if (!this.CanWork(tool, workbenchInv))
		{
			return false;
		}
		global::IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		global::BlueprintDataBlock blueprintDataBlock;
		global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(firstItemNotTool.datablock, out blueprintDataBlock);
		int num = 1;
		if (firstItemNotTool.datablock.IsSplittable())
		{
			num = firstItemNotTool.uses;
		}
		for (int i = 0; i < num; i++)
		{
			foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in blueprintDataBlock.ingredients)
			{
				int num2 = Random.Range(0, 4);
				if (num2 != 0)
				{
					if (num2 == 1 || num2 == 2 || num2 == 3)
					{
						workbenchInv.AddItemAmount(ingredientEntry.Ingredient, ingredientEntry.amount);
					}
				}
			}
		}
		int num3;
		if (!firstItemNotTool.datablock.IsSplittable())
		{
			num3 = firstItemNotTool.uses;
		}
		else
		{
			num3 = num;
		}
		if (firstItemNotTool.Consume(ref num3))
		{
			firstItemNotTool.inventory.RemoveItem(firstItemNotTool.slot);
		}
		return true;
	}

	// Token: 0x060036F9 RID: 14073 RVA: 0x000C5F8C File Offset: 0x000C418C
	public override string GetItemDescription()
	{
		return "This doesn't do anything.. yet";
	}

	// Token: 0x02000655 RID: 1621
	private sealed class ITEM_TYPE : global::ResearchToolItem<global::RecycleToolDataBlock>, global::IInventoryItem, global::IResearchToolItem, global::IToolItem
	{
		// Token: 0x060036FA RID: 14074 RVA: 0x000C5F94 File Offset: 0x000C4194
		public ITEM_TYPE(global::RecycleToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000C5FA0 File Offset: 0x000C41A0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000C5FA8 File Offset: 0x000C41A8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000C5FB0 File Offset: 0x000C41B0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000C5FB8 File Offset: 0x000C41B8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000C5FC0 File Offset: 0x000C41C0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000C5FCC File Offset: 0x000C41CC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000C5FD8 File Offset: 0x000C41D8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000C5FE4 File Offset: 0x000C41E4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000C5FF0 File Offset: 0x000C41F0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000C5FFC File Offset: 0x000C41FC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000C6008 File Offset: 0x000C4208
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000C6014 File Offset: 0x000C4214
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000C6020 File Offset: 0x000C4220
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000C6028 File Offset: 0x000C4228
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000C6030 File Offset: 0x000C4230
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000C6038 File Offset: 0x000C4238
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000C6040 File Offset: 0x000C4240
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000C6048 File Offset: 0x000C4248
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000C6050 File Offset: 0x000C4250
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000C6058 File Offset: 0x000C4258
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000C6060 File Offset: 0x000C4260
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000C606C File Offset: 0x000C426C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000C6074 File Offset: 0x000C4274
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000C607C File Offset: 0x000C427C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000C6084 File Offset: 0x000C4284
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000C608C File Offset: 0x000C428C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000C6094 File Offset: 0x000C4294
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000C609C File Offset: 0x000C429C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
