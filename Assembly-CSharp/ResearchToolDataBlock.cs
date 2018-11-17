using System;
using uLink;
using UnityEngine;

// Token: 0x02000598 RID: 1432
public class ResearchToolDataBlock : ToolDataBlock
{
	// Token: 0x06003350 RID: 13136 RVA: 0x000BDE50 File Offset: 0x000BC050
	protected override IInventoryItem ConstructItem()
	{
		return new ResearchToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x000BDE58 File Offset: 0x000BC058
	public override float GetWorkDuration(IToolItem tool)
	{
		return 30f;
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x000BDE60 File Offset: 0x000BC060
	public override bool CanWork(IToolItem tool, Inventory workbenchInv)
	{
		if (workbenchInv.occupiedSlotCount > 2)
		{
			Debug.Log("Too many items for research");
			return false;
		}
		IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		if (firstItemNotTool != null && firstItemNotTool.datablock.isResearchable && BlueprintDataBlock.FindBlueprintForItem(firstItemNotTool.datablock))
		{
			return true;
		}
		Debug.Log("Can't work!?!!?");
		return false;
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x000BDEC4 File Offset: 0x000BC0C4
	public override bool CompleteWork(IToolItem tool, Inventory workbenchInv)
	{
		if (!this.CanWork(tool, workbenchInv))
		{
			return false;
		}
		IInventoryItem firstItemNotTool = base.GetFirstItemNotTool(tool, workbenchInv);
		BlueprintDataBlock blueprintDataBlock;
		if (BlueprintDataBlock.FindBlueprintForItem<BlueprintDataBlock>(firstItemNotTool.datablock, out blueprintDataBlock))
		{
			workbenchInv.AddItem(blueprintDataBlock, Inventory.Slot.Preference.Define(Inventory.Slot.Kind.Default, blueprintDataBlock.IsSplittable(), Inventory.Slot.Kind.Belt), 1);
			return true;
		}
		return false;
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x000BDF1C File Offset: 0x000BC11C
	public override string GetItemDescription()
	{
		return "Drag this onto another item to learn how to craft it. Requires 1 Paper.";
	}

	// Token: 0x02000599 RID: 1433
	private sealed class ITEM_TYPE : ResearchToolItem<ResearchToolDataBlock>, IInventoryItem, IResearchToolItem, IToolItem
	{
		// Token: 0x06003355 RID: 13141 RVA: 0x000BDF24 File Offset: 0x000BC124
		public ITEM_TYPE(ResearchToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000BDF30 File Offset: 0x000BC130
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000BDF38 File Offset: 0x000BC138
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000BDF40 File Offset: 0x000BC140
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000BDF48 File Offset: 0x000BC148
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000BDF50 File Offset: 0x000BC150
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000BDF5C File Offset: 0x000BC15C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000BDF68 File Offset: 0x000BC168
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000BDF74 File Offset: 0x000BC174
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000BDF80 File Offset: 0x000BC180
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000BDF8C File Offset: 0x000BC18C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000BDF98 File Offset: 0x000BC198
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000BDFA4 File Offset: 0x000BC1A4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000BDFB0 File Offset: 0x000BC1B0
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000BDFB8 File Offset: 0x000BC1B8
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000BDFC0 File Offset: 0x000BC1C0
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000BDFC8 File Offset: 0x000BC1C8
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000BDFD0 File Offset: 0x000BC1D0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000BDFD8 File Offset: 0x000BC1D8
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000BDFE0 File Offset: 0x000BC1E0
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000BDFE8 File Offset: 0x000BC1E8
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000BDFF0 File Offset: 0x000BC1F0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000BDFFC File Offset: 0x000BC1FC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000BE004 File Offset: 0x000BC204
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000BE00C File Offset: 0x000BC20C
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000BE014 File Offset: 0x000BC214
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000BE01C File Offset: 0x000BC21C
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000BE024 File Offset: 0x000BC224
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000BE02C File Offset: 0x000BC22C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
