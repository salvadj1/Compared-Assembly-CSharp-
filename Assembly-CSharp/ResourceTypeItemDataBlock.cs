using System;
using uLink;

// Token: 0x0200064A RID: 1610
public class ResourceTypeItemDataBlock : global::ItemDataBlock
{
	// Token: 0x060035D3 RID: 13779 RVA: 0x000C4C54 File Offset: 0x000C2E54
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ResourceTypeItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x000C4C5C File Offset: 0x000C2E5C
	public override string GetItemDescription()
	{
		return "A type of resource";
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x000C4C64 File Offset: 0x000C2E64
	public virtual void UseItem(global::IResourceTypeItem rs)
	{
	}

	// Token: 0x04001BC0 RID: 7104
	public bool cookable;

	// Token: 0x04001BC1 RID: 7105
	public bool flammable;

	// Token: 0x04001BC2 RID: 7106
	public global::ItemDataBlock cookedVersion;

	// Token: 0x04001BC3 RID: 7107
	public int cookHeatRequirement = 1;

	// Token: 0x04001BC4 RID: 7108
	public int numToGiveCookedMin = 1;

	// Token: 0x04001BC5 RID: 7109
	public int numToGiveCookedMax = 1;

	// Token: 0x0200064B RID: 1611
	private sealed class ITEM_TYPE : global::ResourceTypeItem<global::ResourceTypeItemDataBlock>, global::ICookableItem, global::IFlammableItem, global::IInventoryItem, global::IResourceTypeItem
	{
		// Token: 0x060035D6 RID: 13782 RVA: 0x000C4C68 File Offset: 0x000C2E68
		public ITEM_TYPE(global::ResourceTypeItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000C4C74 File Offset: 0x000C2E74
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000C4C7C File Offset: 0x000C2E7C
		bool GetCookableInfo(out int consumeCount, out global::ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
		{
			return base.GetCookableInfo(out consumeCount, out cookedVersion, out cookedCount, out cookTempMin, out burnTemp);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000C4C8C File Offset: 0x000C2E8C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000C4C94 File Offset: 0x000C2E94
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000C4C9C File Offset: 0x000C2E9C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000C4CA4 File Offset: 0x000C2EA4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000C4CB0 File Offset: 0x000C2EB0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000C4CBC File Offset: 0x000C2EBC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000C4CC8 File Offset: 0x000C2EC8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000C4CD4 File Offset: 0x000C2ED4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000C4CE0 File Offset: 0x000C2EE0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000C4CEC File Offset: 0x000C2EEC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000C4CF8 File Offset: 0x000C2EF8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000C4D04 File Offset: 0x000C2F04
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000C4D0C File Offset: 0x000C2F0C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000C4D14 File Offset: 0x000C2F14
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000C4D1C File Offset: 0x000C2F1C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000C4D24 File Offset: 0x000C2F24
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000C4D2C File Offset: 0x000C2F2C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000C4D34 File Offset: 0x000C2F34
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000C4D3C File Offset: 0x000C2F3C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000C4D44 File Offset: 0x000C2F44
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000C4D50 File Offset: 0x000C2F50
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000C4D58 File Offset: 0x000C2F58
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000C4D60 File Offset: 0x000C2F60
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000C4D68 File Offset: 0x000C2F68
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000C4D70 File Offset: 0x000C2F70
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000C4D78 File Offset: 0x000C2F78
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000C4D80 File Offset: 0x000C2F80
		bool get_doNotSave()
		{
			return base.doNotSave;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000C4D88 File Offset: 0x000C2F88
		bool get_flammable()
		{
			return base.flammable;
		}
	}
}
