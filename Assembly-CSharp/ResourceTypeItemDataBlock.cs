using System;
using uLink;

// Token: 0x0200058C RID: 1420
public class ResourceTypeItemDataBlock : ItemDataBlock
{
	// Token: 0x0600320B RID: 12811 RVA: 0x000BC9F8 File Offset: 0x000BABF8
	protected override IInventoryItem ConstructItem()
	{
		return new ResourceTypeItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x000BCA00 File Offset: 0x000BAC00
	public override string GetItemDescription()
	{
		return "A type of resource";
	}

	// Token: 0x0600320D RID: 12813 RVA: 0x000BCA08 File Offset: 0x000BAC08
	public virtual void UseItem(IResourceTypeItem rs)
	{
	}

	// Token: 0x040019EF RID: 6639
	public bool cookable;

	// Token: 0x040019F0 RID: 6640
	public bool flammable;

	// Token: 0x040019F1 RID: 6641
	public ItemDataBlock cookedVersion;

	// Token: 0x040019F2 RID: 6642
	public int cookHeatRequirement = 1;

	// Token: 0x040019F3 RID: 6643
	public int numToGiveCookedMin = 1;

	// Token: 0x040019F4 RID: 6644
	public int numToGiveCookedMax = 1;

	// Token: 0x0200058D RID: 1421
	private sealed class ITEM_TYPE : ResourceTypeItem<ResourceTypeItemDataBlock>, ICookableItem, IFlammableItem, IInventoryItem, IResourceTypeItem
	{
		// Token: 0x0600320E RID: 12814 RVA: 0x000BCA0C File Offset: 0x000BAC0C
		public ITEM_TYPE(ResourceTypeItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000BCA18 File Offset: 0x000BAC18
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000BCA20 File Offset: 0x000BAC20
		bool GetCookableInfo(out int consumeCount, out ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
		{
			return base.GetCookableInfo(out consumeCount, out cookedVersion, out cookedCount, out cookTempMin, out burnTemp);
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000BCA30 File Offset: 0x000BAC30
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000BCA38 File Offset: 0x000BAC38
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000BCA40 File Offset: 0x000BAC40
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000BCA48 File Offset: 0x000BAC48
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000BCA54 File Offset: 0x000BAC54
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000BCA60 File Offset: 0x000BAC60
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000BCA6C File Offset: 0x000BAC6C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000BCA78 File Offset: 0x000BAC78
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000BCA84 File Offset: 0x000BAC84
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000BCA90 File Offset: 0x000BAC90
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000BCA9C File Offset: 0x000BAC9C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000BCAA8 File Offset: 0x000BACA8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000BCAB0 File Offset: 0x000BACB0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000BCAB8 File Offset: 0x000BACB8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000BCAC0 File Offset: 0x000BACC0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000BCAC8 File Offset: 0x000BACC8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000BCAD0 File Offset: 0x000BACD0
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000BCAD8 File Offset: 0x000BACD8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000BCAE0 File Offset: 0x000BACE0
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000BCAE8 File Offset: 0x000BACE8
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000BCAF4 File Offset: 0x000BACF4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000BCAFC File Offset: 0x000BACFC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000BCB04 File Offset: 0x000BAD04
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000BCB0C File Offset: 0x000BAD0C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000BCB14 File Offset: 0x000BAD14
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000BCB1C File Offset: 0x000BAD1C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000BCB24 File Offset: 0x000BAD24
		bool get_doNotSave()
		{
			return base.doNotSave;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000BCB2C File Offset: 0x000BAD2C
		bool get_flammable()
		{
			return base.flammable;
		}
	}
}
