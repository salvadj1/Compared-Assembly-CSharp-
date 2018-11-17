using System;
using uLink;

// Token: 0x0200057F RID: 1407
public class MagazineDataBlock : ItemDataBlock
{
	// Token: 0x06003149 RID: 12617 RVA: 0x000BB674 File Offset: 0x000B9874
	protected override IInventoryItem ConstructItem()
	{
		return new MagazineDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x02000580 RID: 1408
	private sealed class ITEM_TYPE : MagazineItem<MagazineDataBlock>, IInventoryItem, IMagazineItem
	{
		// Token: 0x0600314A RID: 12618 RVA: 0x000BB67C File Offset: 0x000B987C
		public ITEM_TYPE(MagazineDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000BB688 File Offset: 0x000B9888
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000BB690 File Offset: 0x000B9890
		int get_numEmptyBulletSlots()
		{
			return base.numEmptyBulletSlots;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000BB698 File Offset: 0x000B9898
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000BB6A0 File Offset: 0x000B98A0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000BB6A8 File Offset: 0x000B98A8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000BB6B0 File Offset: 0x000B98B0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000BB6BC File Offset: 0x000B98BC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000BB6C8 File Offset: 0x000B98C8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000BB6D4 File Offset: 0x000B98D4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000BB6E0 File Offset: 0x000B98E0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000BB6EC File Offset: 0x000B98EC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000BB6F8 File Offset: 0x000B98F8
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000BB704 File Offset: 0x000B9904
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000BB710 File Offset: 0x000B9910
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000BB718 File Offset: 0x000B9918
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000BB720 File Offset: 0x000B9920
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000BB728 File Offset: 0x000B9928
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000BB730 File Offset: 0x000B9930
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000BB738 File Offset: 0x000B9938
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000BB740 File Offset: 0x000B9940
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000BB748 File Offset: 0x000B9948
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000BB750 File Offset: 0x000B9950
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000BB75C File Offset: 0x000B995C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000BB764 File Offset: 0x000B9964
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000BB76C File Offset: 0x000B996C
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000BB774 File Offset: 0x000B9974
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000BB77C File Offset: 0x000B997C
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000BB784 File Offset: 0x000B9984
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000BB78C File Offset: 0x000B998C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
