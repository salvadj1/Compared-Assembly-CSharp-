using System;
using uLink;

// Token: 0x0200054E RID: 1358
public class AmmoItemDataBlock : ItemDataBlock
{
	// Token: 0x06002DBC RID: 11708 RVA: 0x000B66C4 File Offset: 0x000B48C4
	protected override IInventoryItem ConstructItem()
	{
		return new AmmoItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x000B66CC File Offset: 0x000B48CC
	public override string GetItemDescription()
	{
		return "Ammunition for a weapon";
	}

	// Token: 0x04001909 RID: 6409
	public ItemDataBlock spentCasingType;

	// Token: 0x0200054F RID: 1359
	private sealed class ITEM_TYPE : AmmoItem<AmmoItemDataBlock>, IAmmoItem, IInventoryItem
	{
		// Token: 0x06002DBE RID: 11710 RVA: 0x000B66D4 File Offset: 0x000B48D4
		public ITEM_TYPE(AmmoItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000B66E0 File Offset: 0x000B48E0
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B66E8 File Offset: 0x000B48E8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B66F0 File Offset: 0x000B48F0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B66F8 File Offset: 0x000B48F8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B6700 File Offset: 0x000B4900
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B670C File Offset: 0x000B490C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B6718 File Offset: 0x000B4918
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B6724 File Offset: 0x000B4924
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B6730 File Offset: 0x000B4930
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B673C File Offset: 0x000B493C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B6748 File Offset: 0x000B4948
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000B6754 File Offset: 0x000B4954
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000B6760 File Offset: 0x000B4960
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000B6768 File Offset: 0x000B4968
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000B6770 File Offset: 0x000B4970
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000B6778 File Offset: 0x000B4978
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000B6780 File Offset: 0x000B4980
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000B6788 File Offset: 0x000B4988
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B6790 File Offset: 0x000B4990
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B6798 File Offset: 0x000B4998
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B67A0 File Offset: 0x000B49A0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B67AC File Offset: 0x000B49AC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000B67B4 File Offset: 0x000B49B4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000B67BC File Offset: 0x000B49BC
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B67C4 File Offset: 0x000B49C4
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B67CC File Offset: 0x000B49CC
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B67D4 File Offset: 0x000B49D4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000B67DC File Offset: 0x000B49DC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
