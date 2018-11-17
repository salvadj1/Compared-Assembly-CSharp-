using System;
using uLink;

// Token: 0x02000626 RID: 1574
public class DeployableInventoryDataBlock : global::DeployableItemDataBlock
{
	// Token: 0x06003399 RID: 13209 RVA: 0x000C1CC4 File Offset: 0x000BFEC4
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::DeployableInventoryDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x04001B30 RID: 6960
	public global::DeployableInventoryDataBlock.InitialItem[] initialItems;

	// Token: 0x02000627 RID: 1575
	[Serializable]
	public class InitialItem
	{
		// Token: 0x04001B31 RID: 6961
		public global::ItemDataBlock item;

		// Token: 0x04001B32 RID: 6962
		public int count;

		// Token: 0x04001B33 RID: 6963
		public int slot;
	}

	// Token: 0x02000628 RID: 1576
	private sealed class ITEM_TYPE : global::DeployableItem<global::DeployableInventoryDataBlock>, global::IDeployableItem, global::IHeldItem, global::IInventoryItem
	{
		// Token: 0x0600339B RID: 13211 RVA: 0x000C1CD4 File Offset: 0x000BFED4
		public ITEM_TYPE(global::DeployableInventoryDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000C1CE0 File Offset: 0x000BFEE0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000C1CE8 File Offset: 0x000BFEE8
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000C1CF4 File Offset: 0x000BFEF4
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000C1D00 File Offset: 0x000BFF00
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000C1D0C File Offset: 0x000BFF0C
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C1D18 File Offset: 0x000BFF18
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000C1D20 File Offset: 0x000BFF20
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C1D28 File Offset: 0x000BFF28
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C1D30 File Offset: 0x000BFF30
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000C1D38 File Offset: 0x000BFF38
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000C1D44 File Offset: 0x000BFF44
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000C1D4C File Offset: 0x000BFF4C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000C1D54 File Offset: 0x000BFF54
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000C1D5C File Offset: 0x000BFF5C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000C1D64 File Offset: 0x000BFF64
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000C1D6C File Offset: 0x000BFF6C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000C1D74 File Offset: 0x000BFF74
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000C1D7C File Offset: 0x000BFF7C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000C1D84 File Offset: 0x000BFF84
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000C1D8C File Offset: 0x000BFF8C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000C1D94 File Offset: 0x000BFF94
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000C1DA0 File Offset: 0x000BFFA0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000C1DAC File Offset: 0x000BFFAC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000C1DB8 File Offset: 0x000BFFB8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000C1DC4 File Offset: 0x000BFFC4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000C1DD0 File Offset: 0x000BFFD0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000C1DDC File Offset: 0x000BFFDC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000C1DE8 File Offset: 0x000BFFE8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000C1DF4 File Offset: 0x000BFFF4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000C1DFC File Offset: 0x000BFFFC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000C1E04 File Offset: 0x000C0004
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000C1E0C File Offset: 0x000C000C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000C1E14 File Offset: 0x000C0014
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000C1E1C File Offset: 0x000C001C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000C1E24 File Offset: 0x000C0024
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000C1E2C File Offset: 0x000C002C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000C1E34 File Offset: 0x000C0034
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000C1E40 File Offset: 0x000C0040
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000C1E48 File Offset: 0x000C0048
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000C1E50 File Offset: 0x000C0050
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000C1E58 File Offset: 0x000C0058
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000C1E60 File Offset: 0x000C0060
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000C1E68 File Offset: 0x000C0068
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000C1E70 File Offset: 0x000C0070
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
