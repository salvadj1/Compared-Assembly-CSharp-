using System;
using uLink;

// Token: 0x02000554 RID: 1364
public class BasicHealthKitDataBlock : ItemDataBlock
{
	// Token: 0x06002E3E RID: 11838 RVA: 0x000B6EC8 File Offset: 0x000B50C8
	protected override IInventoryItem ConstructItem()
	{
		return new BasicHealthKitDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x000B6ED0 File Offset: 0x000B50D0
	public virtual IBasicHealthKit ItemAsHealthKit(IInventoryItem item)
	{
		return item as IBasicHealthKit;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x000B6ED8 File Offset: 0x000B50D8
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x000B6F08 File Offset: 0x000B5108
	public override InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option != InventoryItem.MenuItem.Use)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x000B6F30 File Offset: 0x000B5130
	public virtual void UseItem(IBasicHealthKit hk)
	{
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x000B6F34 File Offset: 0x000B5134
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
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

	// Token: 0x06002E44 RID: 11844 RVA: 0x000B6FEC File Offset: 0x000B51EC
	public override string GetItemDescription()
	{
		return "This is a Medical item. Right click, or put in your belt and press the corresponding number key to use it.";
	}

	// Token: 0x04001911 RID: 6417
	public float healthAddMin = 1f;

	// Token: 0x04001912 RID: 6418
	public float healthAddMax = 1f;

	// Token: 0x04001913 RID: 6419
	public bool stopsBleeding;

	// Token: 0x02000555 RID: 1365
	private sealed class ITEM_TYPE : BasicHealthKit<BasicHealthKitDataBlock>, IBasicHealthKit, IInventoryItem
	{
		// Token: 0x06002E45 RID: 11845 RVA: 0x000B6FF4 File Offset: 0x000B51F4
		public ITEM_TYPE(BasicHealthKitDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000B7000 File Offset: 0x000B5200
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000B7008 File Offset: 0x000B5208
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000B7010 File Offset: 0x000B5210
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000B7018 File Offset: 0x000B5218
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000B7020 File Offset: 0x000B5220
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000B702C File Offset: 0x000B522C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000B7038 File Offset: 0x000B5238
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000B7044 File Offset: 0x000B5244
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000B7050 File Offset: 0x000B5250
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000B705C File Offset: 0x000B525C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000B7068 File Offset: 0x000B5268
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000B7074 File Offset: 0x000B5274
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000B7080 File Offset: 0x000B5280
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000B7088 File Offset: 0x000B5288
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000B7090 File Offset: 0x000B5290
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000B7098 File Offset: 0x000B5298
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000B70A0 File Offset: 0x000B52A0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000B70A8 File Offset: 0x000B52A8
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000B70B0 File Offset: 0x000B52B0
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000B70B8 File Offset: 0x000B52B8
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000B70C0 File Offset: 0x000B52C0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000B70CC File Offset: 0x000B52CC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000B70D4 File Offset: 0x000B52D4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000B70DC File Offset: 0x000B52DC
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000B70E4 File Offset: 0x000B52E4
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000B70EC File Offset: 0x000B52EC
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000B70F4 File Offset: 0x000B52F4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000B70FC File Offset: 0x000B52FC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
