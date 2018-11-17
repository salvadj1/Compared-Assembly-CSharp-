using System;
using uLink;

// Token: 0x02000610 RID: 1552
public class BandageDataBlock : global::HeldItemDataBlock
{
	// Token: 0x060031CD RID: 12749 RVA: 0x000BEE14 File Offset: 0x000BD014
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BandageDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060031CE RID: 12750 RVA: 0x000BEE1C File Offset: 0x000BD01C
	public bool DoesGiveBlood()
	{
		return this.bloodAddMax > 0f;
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x000BEE2C File Offset: 0x000BD02C
	public bool DoesBandage()
	{
		return this.bandageAmount > 0f;
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x000BEE3C File Offset: 0x000BD03C
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x000BEE6C File Offset: 0x000BD06C
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
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

	// Token: 0x04001ADD RID: 6877
	public float bandageDuration = 3f;

	// Token: 0x04001ADE RID: 6878
	public float bandageStartTime;

	// Token: 0x04001ADF RID: 6879
	public float bandageAmount = 100f;

	// Token: 0x04001AE0 RID: 6880
	public float bloodAddMin = 20f;

	// Token: 0x04001AE1 RID: 6881
	public float bloodAddMax = 30f;

	// Token: 0x02000611 RID: 1553
	private sealed class ITEM_TYPE : global::BandageItem<global::BandageDataBlock>, global::IBandageItem, global::IHeldItem, global::IInventoryItem
	{
		// Token: 0x060031D2 RID: 12754 RVA: 0x000BEF24 File Offset: 0x000BD124
		public ITEM_TYPE(global::BandageDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000BEF30 File Offset: 0x000BD130
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000BEF38 File Offset: 0x000BD138
		float get_bandageStartTime()
		{
			return base.bandageStartTime;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000BEF40 File Offset: 0x000BD140
		void set_bandageStartTime(float value)
		{
			base.bandageStartTime = value;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000BEF4C File Offset: 0x000BD14C
		bool get_lastFramePrimary()
		{
			return base.lastFramePrimary;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000BEF54 File Offset: 0x000BD154
		void set_lastFramePrimary(bool value)
		{
			base.lastFramePrimary = value;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000BEF60 File Offset: 0x000BD160
		float get_lastBandageTime()
		{
			return base.lastBandageTime;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000BEF68 File Offset: 0x000BD168
		void set_lastBandageTime(float value)
		{
			base.lastBandageTime = value;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000BEF74 File Offset: 0x000BD174
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000BEF80 File Offset: 0x000BD180
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000BEF8C File Offset: 0x000BD18C
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000BEF98 File Offset: 0x000BD198
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000BEFA4 File Offset: 0x000BD1A4
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000BEFAC File Offset: 0x000BD1AC
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000BEFB4 File Offset: 0x000BD1B4
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000BEFBC File Offset: 0x000BD1BC
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000BEFC4 File Offset: 0x000BD1C4
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000BEFD0 File Offset: 0x000BD1D0
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000BEFD8 File Offset: 0x000BD1D8
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000BEFE0 File Offset: 0x000BD1E0
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000BEFE8 File Offset: 0x000BD1E8
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000BEFF0 File Offset: 0x000BD1F0
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000BEFF8 File Offset: 0x000BD1F8
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000BF000 File Offset: 0x000BD200
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000BF008 File Offset: 0x000BD208
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000BF010 File Offset: 0x000BD210
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000BF018 File Offset: 0x000BD218
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000BF020 File Offset: 0x000BD220
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000BF02C File Offset: 0x000BD22C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000BF038 File Offset: 0x000BD238
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000BF044 File Offset: 0x000BD244
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000BF050 File Offset: 0x000BD250
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000BF05C File Offset: 0x000BD25C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000BF068 File Offset: 0x000BD268
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000BF074 File Offset: 0x000BD274
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000BF080 File Offset: 0x000BD280
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000BF088 File Offset: 0x000BD288
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000BF090 File Offset: 0x000BD290
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000BF098 File Offset: 0x000BD298
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000BF0A0 File Offset: 0x000BD2A0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000BF0A8 File Offset: 0x000BD2A8
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000BF0B0 File Offset: 0x000BD2B0
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000BF0B8 File Offset: 0x000BD2B8
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000BF0C0 File Offset: 0x000BD2C0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000BF0CC File Offset: 0x000BD2CC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000BF0D4 File Offset: 0x000BD2D4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000BF0DC File Offset: 0x000BD2DC
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000BF0E4 File Offset: 0x000BD2E4
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000BF0EC File Offset: 0x000BD2EC
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000BF0F4 File Offset: 0x000BD2F4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000BF0FC File Offset: 0x000BD2FC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
