using System;
using uLink;

// Token: 0x02000572 RID: 1394
public class GunpowderDataBlock : ItemDataBlock
{
	// Token: 0x0600305A RID: 12378 RVA: 0x000BA860 File Offset: 0x000B8A60
	protected override IInventoryItem ConstructItem()
	{
		return new GunpowderDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x000BA868 File Offset: 0x000B8A68
	public override string GetItemDescription()
	{
		return "Explosive used in ammunition, Combine this with empty casings to prime them.";
	}

	// Token: 0x02000573 RID: 1395
	private sealed class ITEM_TYPE : GunpowderItem<GunpowderDataBlock>, IGunpowderItem, IInventoryItem
	{
		// Token: 0x0600305C RID: 12380 RVA: 0x000BA870 File Offset: 0x000B8A70
		public ITEM_TYPE(GunpowderDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000BA87C File Offset: 0x000B8A7C
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000BA884 File Offset: 0x000B8A84
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000BA88C File Offset: 0x000B8A8C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000BA894 File Offset: 0x000B8A94
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000BA89C File Offset: 0x000B8A9C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000BA8A8 File Offset: 0x000B8AA8
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000BA8B4 File Offset: 0x000B8AB4
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000BA8C0 File Offset: 0x000B8AC0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000BA8CC File Offset: 0x000B8ACC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000BA8D8 File Offset: 0x000B8AD8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000BA8E4 File Offset: 0x000B8AE4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000BA8F0 File Offset: 0x000B8AF0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000BA8FC File Offset: 0x000B8AFC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000BA904 File Offset: 0x000B8B04
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000BA90C File Offset: 0x000B8B0C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000BA914 File Offset: 0x000B8B14
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000BA91C File Offset: 0x000B8B1C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000BA924 File Offset: 0x000B8B24
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000BA92C File Offset: 0x000B8B2C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000BA934 File Offset: 0x000B8B34
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000BA93C File Offset: 0x000B8B3C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000BA948 File Offset: 0x000B8B48
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000BA950 File Offset: 0x000B8B50
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000BA958 File Offset: 0x000B8B58
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000BA960 File Offset: 0x000B8B60
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000BA968 File Offset: 0x000B8B68
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000BA970 File Offset: 0x000B8B70
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000BA978 File Offset: 0x000B8B78
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
