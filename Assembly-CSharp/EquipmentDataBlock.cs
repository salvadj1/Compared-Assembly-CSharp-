using System;
using uLink;

// Token: 0x0200062E RID: 1582
public class EquipmentDataBlock : global::ItemDataBlock
{
	// Token: 0x060033FF RID: 13311 RVA: 0x000C2984 File Offset: 0x000C0B84
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::EquipmentDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x000C298C File Offset: 0x000C0B8C
	public virtual void OnEquipped(global::IEquipmentItem item)
	{
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x000C2990 File Offset: 0x000C0B90
	public virtual void OnUnEquipped(global::IEquipmentItem item)
	{
	}

	// Token: 0x0200062F RID: 1583
	private sealed class ITEM_TYPE : global::EquipmentItem<global::EquipmentDataBlock>, global::IEquipmentItem, global::IInventoryItem
	{
		// Token: 0x06003402 RID: 13314 RVA: 0x000C2994 File Offset: 0x000C0B94
		public ITEM_TYPE(global::EquipmentDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000C29A0 File Offset: 0x000C0BA0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000C29A8 File Offset: 0x000C0BA8
		void OnUnEquipped()
		{
			base.OnUnEquipped();
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000C29B0 File Offset: 0x000C0BB0
		void OnEquipped()
		{
			base.OnEquipped();
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000C29B8 File Offset: 0x000C0BB8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000C29C0 File Offset: 0x000C0BC0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000C29C8 File Offset: 0x000C0BC8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000C29D0 File Offset: 0x000C0BD0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000C29DC File Offset: 0x000C0BDC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000C29E8 File Offset: 0x000C0BE8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000C29F4 File Offset: 0x000C0BF4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000C2A00 File Offset: 0x000C0C00
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000C2A0C File Offset: 0x000C0C0C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000C2A18 File Offset: 0x000C0C18
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000C2A24 File Offset: 0x000C0C24
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000C2A30 File Offset: 0x000C0C30
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000C2A38 File Offset: 0x000C0C38
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000C2A40 File Offset: 0x000C0C40
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000C2A48 File Offset: 0x000C0C48
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000C2A50 File Offset: 0x000C0C50
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000C2A58 File Offset: 0x000C0C58
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000C2A60 File Offset: 0x000C0C60
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000C2A68 File Offset: 0x000C0C68
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000C2A70 File Offset: 0x000C0C70
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000C2A7C File Offset: 0x000C0C7C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000C2A84 File Offset: 0x000C0C84
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000C2A8C File Offset: 0x000C0C8C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000C2A94 File Offset: 0x000C0C94
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000C2A9C File Offset: 0x000C0C9C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000C2AA4 File Offset: 0x000C0CA4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000C2AAC File Offset: 0x000C0CAC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
