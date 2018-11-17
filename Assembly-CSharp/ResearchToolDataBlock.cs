using System;
using uLink;
using UnityEngine;

// Token: 0x02000656 RID: 1622
public class ResearchToolDataBlock : global::ToolDataBlock
{
	// Token: 0x06003718 RID: 14104 RVA: 0x000C60AC File Offset: 0x000C42AC
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ResearchToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x000C60B4 File Offset: 0x000C42B4
	public override float GetWorkDuration(global::IToolItem tool)
	{
		return 30f;
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x000C60BC File Offset: 0x000C42BC
	public override bool CanWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		if (workbenchInv.occupiedSlotCount > 2)
		{
			Debug.Log("Too many items for research");
			return false;
		}
		global::IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		if (firstItemNotTool != null && firstItemNotTool.datablock.isResearchable && global::BlueprintDataBlock.FindBlueprintForItem(firstItemNotTool.datablock))
		{
			return true;
		}
		Debug.Log("Can't work!?!!?");
		return false;
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x000C6120 File Offset: 0x000C4320
	public override bool CompleteWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		if (!this.CanWork(tool, workbenchInv))
		{
			return false;
		}
		global::IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		global::BlueprintDataBlock blueprintDataBlock;
		if (global::BlueprintDataBlock.FindBlueprintForItem<global::BlueprintDataBlock>(firstItemNotTool.datablock, out blueprintDataBlock))
		{
			workbenchInv.AddItem(blueprintDataBlock, global::Inventory.Slot.Preference.Define(global::Inventory.Slot.Kind.Default, blueprintDataBlock.IsSplittable(), global::Inventory.Slot.Kind.Belt), 1);
			return true;
		}
		return false;
	}

	// Token: 0x0600371C RID: 14108 RVA: 0x000C6178 File Offset: 0x000C4378
	public override string GetItemDescription()
	{
		return "Drag this onto another item to learn how to craft it. Requires 1 Paper.";
	}

	// Token: 0x02000657 RID: 1623
	private sealed class ITEM_TYPE : global::ResearchToolItem<global::ResearchToolDataBlock>, global::IInventoryItem, global::IResearchToolItem, global::IToolItem
	{
		// Token: 0x0600371D RID: 14109 RVA: 0x000C6180 File Offset: 0x000C4380
		public ITEM_TYPE(global::ResearchToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000C618C File Offset: 0x000C438C
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000C6194 File Offset: 0x000C4394
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000C619C File Offset: 0x000C439C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000C61A4 File Offset: 0x000C43A4
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000C61AC File Offset: 0x000C43AC
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000C61B8 File Offset: 0x000C43B8
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000C61C4 File Offset: 0x000C43C4
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000C61D0 File Offset: 0x000C43D0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000C61DC File Offset: 0x000C43DC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000C61E8 File Offset: 0x000C43E8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000C61F4 File Offset: 0x000C43F4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000C6200 File Offset: 0x000C4400
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000C620C File Offset: 0x000C440C
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000C6214 File Offset: 0x000C4414
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000C621C File Offset: 0x000C441C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000C6224 File Offset: 0x000C4424
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000C622C File Offset: 0x000C442C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000C6234 File Offset: 0x000C4434
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000C623C File Offset: 0x000C443C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000C6244 File Offset: 0x000C4444
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000C624C File Offset: 0x000C444C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000C6258 File Offset: 0x000C4458
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000C6260 File Offset: 0x000C4460
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000C6268 File Offset: 0x000C4468
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000C6270 File Offset: 0x000C4470
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000C6278 File Offset: 0x000C4478
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000C6280 File Offset: 0x000C4480
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000C6288 File Offset: 0x000C4488
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
