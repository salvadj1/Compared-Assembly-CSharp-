using System;
using uLink;

// Token: 0x0200063D RID: 1597
public class MagazineDataBlock : global::ItemDataBlock
{
	// Token: 0x06003511 RID: 13585 RVA: 0x000C38D0 File Offset: 0x000C1AD0
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::MagazineDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0200063E RID: 1598
	private sealed class ITEM_TYPE : global::MagazineItem<global::MagazineDataBlock>, global::IInventoryItem, global::IMagazineItem
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x000C38D8 File Offset: 0x000C1AD8
		public ITEM_TYPE(global::MagazineDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000C38E4 File Offset: 0x000C1AE4
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000C38EC File Offset: 0x000C1AEC
		int get_numEmptyBulletSlots()
		{
			return base.numEmptyBulletSlots;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000C38F4 File Offset: 0x000C1AF4
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000C38FC File Offset: 0x000C1AFC
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000C3904 File Offset: 0x000C1B04
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000C390C File Offset: 0x000C1B0C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000C3918 File Offset: 0x000C1B18
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000C3924 File Offset: 0x000C1B24
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000C3930 File Offset: 0x000C1B30
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000C393C File Offset: 0x000C1B3C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000C3948 File Offset: 0x000C1B48
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000C3954 File Offset: 0x000C1B54
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000C3960 File Offset: 0x000C1B60
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000C396C File Offset: 0x000C1B6C
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000C3974 File Offset: 0x000C1B74
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000C397C File Offset: 0x000C1B7C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000C3984 File Offset: 0x000C1B84
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000C398C File Offset: 0x000C1B8C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000C3994 File Offset: 0x000C1B94
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000C399C File Offset: 0x000C1B9C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000C39A4 File Offset: 0x000C1BA4
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000C39AC File Offset: 0x000C1BAC
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000C39B8 File Offset: 0x000C1BB8
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000C39C0 File Offset: 0x000C1BC0
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000C39C8 File Offset: 0x000C1BC8
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000C39D0 File Offset: 0x000C1BD0
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000C39D8 File Offset: 0x000C1BD8
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000C39E0 File Offset: 0x000C1BE0
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000C39E8 File Offset: 0x000C1BE8
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
