using System;
using uLink;
using UnityEngine;

// Token: 0x02000596 RID: 1430
public class RecycleToolDataBlock : ToolDataBlock
{
	// Token: 0x0600332D RID: 13101 RVA: 0x000BDBC4 File Offset: 0x000BBDC4
	protected override IInventoryItem ConstructItem()
	{
		return new RecycleToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600332E RID: 13102 RVA: 0x000BDBCC File Offset: 0x000BBDCC
	public override bool CanWork(IToolItem tool, Inventory workbenchInv)
	{
		if (workbenchInv.occupiedSlotCount > 2)
		{
			Debug.Log("Too many items for recycle");
			return false;
		}
		IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		return firstItemNotTool.datablock.isRecycleable && BlueprintDataBlock.FindBlueprintForItem(firstItemNotTool.datablock);
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x000BDC20 File Offset: 0x000BBE20
	public override float GetWorkDuration(IToolItem tool)
	{
		return 15f;
	}

	// Token: 0x06003330 RID: 13104 RVA: 0x000BDC28 File Offset: 0x000BBE28
	public override bool CompleteWork(IToolItem tool, Inventory workbenchInv)
	{
		if (!this.CanWork(tool, workbenchInv))
		{
			return false;
		}
		IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		BlueprintDataBlock blueprintDataBlock;
		BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(firstItemNotTool.datablock, out blueprintDataBlock);
		int num = 1;
		if (firstItemNotTool.datablock.IsSplittable())
		{
			num = firstItemNotTool.uses;
		}
		for (int i = 0; i < num; i++)
		{
			foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in blueprintDataBlock.ingredients)
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

	// Token: 0x06003331 RID: 13105 RVA: 0x000BDD30 File Offset: 0x000BBF30
	public override string GetItemDescription()
	{
		return "This doesn't do anything.. yet";
	}

	// Token: 0x02000597 RID: 1431
	private sealed class ITEM_TYPE : ResearchToolItem<RecycleToolDataBlock>, IInventoryItem, IResearchToolItem, IToolItem
	{
		// Token: 0x06003332 RID: 13106 RVA: 0x000BDD38 File Offset: 0x000BBF38
		public ITEM_TYPE(RecycleToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000BDD44 File Offset: 0x000BBF44
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000BDD4C File Offset: 0x000BBF4C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000BDD54 File Offset: 0x000BBF54
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000BDD5C File Offset: 0x000BBF5C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000BDD64 File Offset: 0x000BBF64
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000BDD70 File Offset: 0x000BBF70
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000BDD7C File Offset: 0x000BBF7C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000BDD88 File Offset: 0x000BBF88
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000BDD94 File Offset: 0x000BBF94
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000BDDAC File Offset: 0x000BBFAC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000BDDB8 File Offset: 0x000BBFB8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000BDDC4 File Offset: 0x000BBFC4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000BDDCC File Offset: 0x000BBFCC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000BDDD4 File Offset: 0x000BBFD4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000BDDDC File Offset: 0x000BBFDC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000BDDE4 File Offset: 0x000BBFE4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000BDDEC File Offset: 0x000BBFEC
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000BDDF4 File Offset: 0x000BBFF4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000BDDFC File Offset: 0x000BBFFC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000BDE04 File Offset: 0x000BC004
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000BDE10 File Offset: 0x000BC010
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000BDE18 File Offset: 0x000BC018
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000BDE20 File Offset: 0x000BC020
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000BDE28 File Offset: 0x000BC028
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000BDE30 File Offset: 0x000BC030
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000BDE38 File Offset: 0x000BC038
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000BDE40 File Offset: 0x000BC040
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
