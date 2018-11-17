using System;
using uLink;

// Token: 0x0200057D RID: 1405
public class LockpickItemDataBlock : ItemDataBlock
{
	// Token: 0x0600312A RID: 12586 RVA: 0x000BB554 File Offset: 0x000B9754
	protected override IInventoryItem ConstructItem()
	{
		return new LockpickItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x040019C0 RID: 6592
	public float pickingAbility;

	// Token: 0x0200057E RID: 1406
	private sealed class ITEM_TYPE : LockpickItem<LockpickItemDataBlock>, IInventoryItem, ILockpickItem
	{
		// Token: 0x0600312B RID: 12587 RVA: 0x000BB55C File Offset: 0x000B975C
		public ITEM_TYPE(LockpickItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000BB568 File Offset: 0x000B9768
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000BB570 File Offset: 0x000B9770
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000BB578 File Offset: 0x000B9778
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000BB580 File Offset: 0x000B9780
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000BB588 File Offset: 0x000B9788
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000BB594 File Offset: 0x000B9794
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000BB5A0 File Offset: 0x000B97A0
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000BB5AC File Offset: 0x000B97AC
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000BB5B8 File Offset: 0x000B97B8
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000BB5C4 File Offset: 0x000B97C4
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000BB5D0 File Offset: 0x000B97D0
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000BB5DC File Offset: 0x000B97DC
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000BB5E8 File Offset: 0x000B97E8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000BB5F0 File Offset: 0x000B97F0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000BB5F8 File Offset: 0x000B97F8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000BB600 File Offset: 0x000B9800
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000BB608 File Offset: 0x000B9808
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000BB610 File Offset: 0x000B9810
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000BB618 File Offset: 0x000B9818
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000BB620 File Offset: 0x000B9820
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000BB628 File Offset: 0x000B9828
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000BB634 File Offset: 0x000B9834
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000BB63C File Offset: 0x000B983C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000BB644 File Offset: 0x000B9844
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000BB64C File Offset: 0x000B984C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000BB654 File Offset: 0x000B9854
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000BB65C File Offset: 0x000B985C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000BB664 File Offset: 0x000B9864
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
