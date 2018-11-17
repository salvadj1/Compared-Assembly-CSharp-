using System;
using uLink;

// Token: 0x02000612 RID: 1554
public class BasicHealthKitDataBlock : global::ItemDataBlock
{
	// Token: 0x06003206 RID: 12806 RVA: 0x000BF124 File Offset: 0x000BD324
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BasicHealthKitDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003207 RID: 12807 RVA: 0x000BF12C File Offset: 0x000BD32C
	public virtual global::IBasicHealthKit ItemAsHealthKit(global::IInventoryItem item)
	{
		return item as global::IBasicHealthKit;
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x000BF134 File Offset: 0x000BD334
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x000BF164 File Offset: 0x000BD364
	public override global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option != global::InventoryItem.MenuItem.Use)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return global::InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x0600320A RID: 12810 RVA: 0x000BF18C File Offset: 0x000BD38C
	public virtual void UseItem(global::IBasicHealthKit hk)
	{
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x000BF190 File Offset: 0x000BD390
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddSectionTitle("Medical", 15f);
		string text = string.Empty;
		if (this.healthAddMin == this.healthAddMax)
		{
			text = "Heals " + this.healthAddMin + " health.";
		}
		else
		{
			text = string.Concat(new object[]
			{
				"Heals ",
				this.healthAddMin,
				" to ",
				this.healthAddMax,
				" health."
			});
		}
		infoWindow.AddBasicLabel(text, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x000BF248 File Offset: 0x000BD448
	public override string GetItemDescription()
	{
		return "This is a Medical item. Right click, or put in your belt and press the corresponding number key to use it.";
	}

	// Token: 0x04001AE2 RID: 6882
	public float healthAddMin = 1f;

	// Token: 0x04001AE3 RID: 6883
	public float healthAddMax = 1f;

	// Token: 0x04001AE4 RID: 6884
	public bool stopsBleeding;

	// Token: 0x02000613 RID: 1555
	private sealed class ITEM_TYPE : global::BasicHealthKit<global::BasicHealthKitDataBlock>, global::IBasicHealthKit, global::IInventoryItem
	{
		// Token: 0x0600320D RID: 12813 RVA: 0x000BF250 File Offset: 0x000BD450
		public ITEM_TYPE(global::BasicHealthKitDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000BF25C File Offset: 0x000BD45C
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000BF264 File Offset: 0x000BD464
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000BF26C File Offset: 0x000BD46C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000BF274 File Offset: 0x000BD474
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000BF27C File Offset: 0x000BD47C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000BF288 File Offset: 0x000BD488
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000BF294 File Offset: 0x000BD494
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000BF2A0 File Offset: 0x000BD4A0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000BF2AC File Offset: 0x000BD4AC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000BF2B8 File Offset: 0x000BD4B8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000BF2C4 File Offset: 0x000BD4C4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000BF2D0 File Offset: 0x000BD4D0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000BF2DC File Offset: 0x000BD4DC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000BF2E4 File Offset: 0x000BD4E4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000BF2EC File Offset: 0x000BD4EC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000BF2F4 File Offset: 0x000BD4F4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000BF2FC File Offset: 0x000BD4FC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000BF304 File Offset: 0x000BD504
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000BF30C File Offset: 0x000BD50C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000BF314 File Offset: 0x000BD514
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000BF31C File Offset: 0x000BD51C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000BF328 File Offset: 0x000BD528
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000BF330 File Offset: 0x000BD530
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000BF338 File Offset: 0x000BD538
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000BF340 File Offset: 0x000BD540
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000BF348 File Offset: 0x000BD548
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000BF350 File Offset: 0x000BD550
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000BF358 File Offset: 0x000BD558
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
