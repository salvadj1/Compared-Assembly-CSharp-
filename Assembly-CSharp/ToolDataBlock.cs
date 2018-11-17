using System;
using uLink;
using UnityEngine;

// Token: 0x02000658 RID: 1624
public class ToolDataBlock : global::ItemDataBlock
{
	// Token: 0x0600373B RID: 14139 RVA: 0x000C6298 File Offset: 0x000C4498
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600373C RID: 14140 RVA: 0x000C62A0 File Offset: 0x000C44A0
	public virtual bool CanWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		return false;
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x000C62A4 File Offset: 0x000C44A4
	public virtual bool CompleteWork(global::IToolItem tool, global::Inventory workbenchInv)
	{
		return false;
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x000C62A8 File Offset: 0x000C44A8
	public virtual float GetWorkDuration(global::IToolItem tool)
	{
		return 1f;
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x000C62B0 File Offset: 0x000C44B0
	public global::IInventoryItem GetFirstItemNotTool(global::IToolItem tool, global::Inventory workbenchInv)
	{
		using (global::Inventory.OccupiedIterator occupiedIterator = workbenchInv.occupiedIterator)
		{
			global::IInventoryItem inventoryItem;
			while (occupiedIterator.Next(out inventoryItem))
			{
				if (!object.ReferenceEquals(inventoryItem, tool))
				{
					return inventoryItem;
				}
			}
		}
		Debug.LogWarning("Could not find target item");
		return null;
	}

	// Token: 0x02000659 RID: 1625
	private sealed class ITEM_TYPE : global::ToolItem<global::ToolDataBlock>, global::IInventoryItem, global::IToolItem
	{
		// Token: 0x06003740 RID: 14144 RVA: 0x000C6324 File Offset: 0x000C4524
		public ITEM_TYPE(global::ToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000C6330 File Offset: 0x000C4530
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000C6338 File Offset: 0x000C4538
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000C6340 File Offset: 0x000C4540
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000C6348 File Offset: 0x000C4548
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000C6350 File Offset: 0x000C4550
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000C635C File Offset: 0x000C455C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000C6368 File Offset: 0x000C4568
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000C6374 File Offset: 0x000C4574
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000C6380 File Offset: 0x000C4580
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000C638C File Offset: 0x000C458C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000C6398 File Offset: 0x000C4598
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000C63A4 File Offset: 0x000C45A4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000C63B0 File Offset: 0x000C45B0
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000C63B8 File Offset: 0x000C45B8
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000C63C0 File Offset: 0x000C45C0
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000C63C8 File Offset: 0x000C45C8
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000C63D0 File Offset: 0x000C45D0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000C63D8 File Offset: 0x000C45D8
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000C63E0 File Offset: 0x000C45E0
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000C63E8 File Offset: 0x000C45E8
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000C63F0 File Offset: 0x000C45F0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000C63FC File Offset: 0x000C45FC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000C6404 File Offset: 0x000C4604
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000C640C File Offset: 0x000C460C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000C6414 File Offset: 0x000C4614
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000C641C File Offset: 0x000C461C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000C6424 File Offset: 0x000C4624
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000C642C File Offset: 0x000C462C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
