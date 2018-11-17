using System;
using uLink;

// Token: 0x02000552 RID: 1362
public class BandageDataBlock : HeldItemDataBlock
{
	// Token: 0x06002E05 RID: 11781 RVA: 0x000B6BB8 File Offset: 0x000B4DB8
	protected override IInventoryItem ConstructItem()
	{
		return new BandageDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x000B6BC0 File Offset: 0x000B4DC0
	public bool DoesGiveBlood()
	{
		return this.bloodAddMax > 0f;
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x000B6BD0 File Offset: 0x000B4DD0
	public bool DoesBandage()
	{
		return this.bandageAmount > 0f;
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x000B6BE0 File Offset: 0x000B4DE0
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x000B6C10 File Offset: 0x000B4E10
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddSectionTitle("Medical", 15f);
		string text = string.Empty;
		if (this.bloodAddMin == this.bloodAddMax)
		{
			text = "Heals " + this.bloodAddMin + " health.";
		}
		else
		{
			text = string.Concat(new object[]
			{
				"Heals ",
				this.bloodAddMin,
				" to ",
				this.bloodAddMax,
				" health."
			});
		}
		infoWindow.AddBasicLabel(text, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x0400190C RID: 6412
	public float bandageDuration = 3f;

	// Token: 0x0400190D RID: 6413
	public float bandageStartTime;

	// Token: 0x0400190E RID: 6414
	public float bandageAmount = 100f;

	// Token: 0x0400190F RID: 6415
	public float bloodAddMin = 20f;

	// Token: 0x04001910 RID: 6416
	public float bloodAddMax = 30f;

	// Token: 0x02000553 RID: 1363
	private sealed class ITEM_TYPE : BandageItem<BandageDataBlock>, IBandageItem, IHeldItem, IInventoryItem
	{
		// Token: 0x06002E0A RID: 11786 RVA: 0x000B6CC8 File Offset: 0x000B4EC8
		public ITEM_TYPE(BandageDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		float get_bandageStartTime()
		{
			return base.bandageStartTime;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000B6CE4 File Offset: 0x000B4EE4
		void set_bandageStartTime(float value)
		{
			base.bandageStartTime = value;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000B6CF0 File Offset: 0x000B4EF0
		bool get_lastFramePrimary()
		{
			return base.lastFramePrimary;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
		void set_lastFramePrimary(bool value)
		{
			base.lastFramePrimary = value;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000B6D04 File Offset: 0x000B4F04
		float get_lastBandageTime()
		{
			return base.lastBandageTime;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000B6D0C File Offset: 0x000B4F0C
		void set_lastBandageTime(float value)
		{
			base.lastBandageTime = value;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000B6D18 File Offset: 0x000B4F18
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000B6D24 File Offset: 0x000B4F24
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000B6D30 File Offset: 0x000B4F30
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B6D3C File Offset: 0x000B4F3C
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000B6D48 File Offset: 0x000B4F48
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B6D50 File Offset: 0x000B4F50
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000B6D58 File Offset: 0x000B4F58
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B6D60 File Offset: 0x000B4F60
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000B6D68 File Offset: 0x000B4F68
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000B6D74 File Offset: 0x000B4F74
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000B6D7C File Offset: 0x000B4F7C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000B6D84 File Offset: 0x000B4F84
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000B6D8C File Offset: 0x000B4F8C
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000B6D94 File Offset: 0x000B4F94
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000B6D9C File Offset: 0x000B4F9C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000B6DA4 File Offset: 0x000B4FA4
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000B6DB4 File Offset: 0x000B4FB4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000B6DBC File Offset: 0x000B4FBC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000B6DC4 File Offset: 0x000B4FC4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000B6DD0 File Offset: 0x000B4FD0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000B6DDC File Offset: 0x000B4FDC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000B6DE8 File Offset: 0x000B4FE8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000B6DF4 File Offset: 0x000B4FF4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000B6E00 File Offset: 0x000B5000
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000B6E0C File Offset: 0x000B500C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000B6E18 File Offset: 0x000B5018
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000B6E24 File Offset: 0x000B5024
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000B6E2C File Offset: 0x000B502C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B6E34 File Offset: 0x000B5034
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B6E3C File Offset: 0x000B503C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B6E44 File Offset: 0x000B5044
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B6E4C File Offset: 0x000B504C
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000B6E54 File Offset: 0x000B5054
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000B6E5C File Offset: 0x000B505C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000B6E64 File Offset: 0x000B5064
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000B6E70 File Offset: 0x000B5070
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000B6E78 File Offset: 0x000B5078
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000B6E80 File Offset: 0x000B5080
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000B6E88 File Offset: 0x000B5088
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000B6E90 File Offset: 0x000B5090
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000B6E98 File Offset: 0x000B5098
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B6EA0 File Offset: 0x000B50A0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
