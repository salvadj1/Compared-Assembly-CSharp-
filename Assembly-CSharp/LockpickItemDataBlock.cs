using System;
using uLink;

// Token: 0x0200063B RID: 1595
public class LockpickItemDataBlock : global::ItemDataBlock
{
	// Token: 0x060034F2 RID: 13554 RVA: 0x000C37B0 File Offset: 0x000C19B0
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::LockpickItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x04001B91 RID: 7057
	public float pickingAbility;

	// Token: 0x0200063C RID: 1596
	private sealed class ITEM_TYPE : global::LockpickItem<global::LockpickItemDataBlock>, global::IInventoryItem, global::ILockpickItem
	{
		// Token: 0x060034F3 RID: 13555 RVA: 0x000C37B8 File Offset: 0x000C19B8
		public ITEM_TYPE(global::LockpickItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000C37C4 File Offset: 0x000C19C4
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000C37CC File Offset: 0x000C19CC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000C37D4 File Offset: 0x000C19D4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000C37DC File Offset: 0x000C19DC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000C37E4 File Offset: 0x000C19E4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000C37F0 File Offset: 0x000C19F0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000C37FC File Offset: 0x000C19FC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000C3808 File Offset: 0x000C1A08
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000C3814 File Offset: 0x000C1A14
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000C3820 File Offset: 0x000C1A20
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000C382C File Offset: 0x000C1A2C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000C3838 File Offset: 0x000C1A38
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000C3844 File Offset: 0x000C1A44
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000C384C File Offset: 0x000C1A4C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000C3854 File Offset: 0x000C1A54
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000C385C File Offset: 0x000C1A5C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000C3864 File Offset: 0x000C1A64
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000C386C File Offset: 0x000C1A6C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000C3874 File Offset: 0x000C1A74
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000C387C File Offset: 0x000C1A7C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000C3884 File Offset: 0x000C1A84
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000C3890 File Offset: 0x000C1A90
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000C3898 File Offset: 0x000C1A98
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000C38A0 File Offset: 0x000C1AA0
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000C38A8 File Offset: 0x000C1AA8
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000C38B0 File Offset: 0x000C1AB0
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000C38B8 File Offset: 0x000C1AB8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000C38C0 File Offset: 0x000C1AC0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
