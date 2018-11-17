using System;
using uLink;

// Token: 0x02000630 RID: 1584
public class GunpowderDataBlock : global::ItemDataBlock
{
	// Token: 0x06003422 RID: 13346 RVA: 0x000C2ABC File Offset: 0x000C0CBC
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::GunpowderDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x000C2AC4 File Offset: 0x000C0CC4
	public override string GetItemDescription()
	{
		return "Explosive used in ammunition, Combine this with empty casings to prime them.";
	}

	// Token: 0x02000631 RID: 1585
	private sealed class ITEM_TYPE : global::GunpowderItem<global::GunpowderDataBlock>, global::IGunpowderItem, global::IInventoryItem
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x000C2ACC File Offset: 0x000C0CCC
		public ITEM_TYPE(global::GunpowderDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000C2AD8 File Offset: 0x000C0CD8
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000C2AE0 File Offset: 0x000C0CE0
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000C2AE8 File Offset: 0x000C0CE8
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000C2AF0 File Offset: 0x000C0CF0
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000C2B04 File Offset: 0x000C0D04
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000C2B10 File Offset: 0x000C0D10
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000C2B1C File Offset: 0x000C0D1C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000C2B28 File Offset: 0x000C0D28
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000C2B34 File Offset: 0x000C0D34
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000C2B40 File Offset: 0x000C0D40
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000C2B4C File Offset: 0x000C0D4C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000C2B58 File Offset: 0x000C0D58
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000C2B60 File Offset: 0x000C0D60
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000C2B68 File Offset: 0x000C0D68
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000C2B70 File Offset: 0x000C0D70
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000C2B78 File Offset: 0x000C0D78
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000C2B80 File Offset: 0x000C0D80
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000C2B88 File Offset: 0x000C0D88
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000C2B90 File Offset: 0x000C0D90
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000C2B98 File Offset: 0x000C0D98
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000C2BAC File Offset: 0x000C0DAC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000C2BB4 File Offset: 0x000C0DB4
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000C2BBC File Offset: 0x000C0DBC
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000C2BC4 File Offset: 0x000C0DC4
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000C2BCC File Offset: 0x000C0DCC
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000C2BD4 File Offset: 0x000C0DD4
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
