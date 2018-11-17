using System;
using uLink;

// Token: 0x02000568 RID: 1384
public class DeployableInventoryDataBlock : DeployableItemDataBlock
{
	// Token: 0x06002FD1 RID: 12241 RVA: 0x000B9A68 File Offset: 0x000B7C68
	protected override IInventoryItem ConstructItem()
	{
		return new DeployableInventoryDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0400195F RID: 6495
	public DeployableInventoryDataBlock.InitialItem[] initialItems;

	// Token: 0x02000569 RID: 1385
	[Serializable]
	public class InitialItem
	{
		// Token: 0x04001960 RID: 6496
		public ItemDataBlock item;

		// Token: 0x04001961 RID: 6497
		public int count;

		// Token: 0x04001962 RID: 6498
		public int slot;
	}

	// Token: 0x0200056A RID: 1386
	private sealed class ITEM_TYPE : DeployableItem<DeployableInventoryDataBlock>, IDeployableItem, IHeldItem, IInventoryItem
	{
		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B9A78 File Offset: 0x000B7C78
		public ITEM_TYPE(DeployableInventoryDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000B9A84 File Offset: 0x000B7C84
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B9A8C File Offset: 0x000B7C8C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B9A98 File Offset: 0x000B7C98
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B9AA4 File Offset: 0x000B7CA4
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000B9AB0 File Offset: 0x000B7CB0
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000B9ABC File Offset: 0x000B7CBC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000B9ACC File Offset: 0x000B7CCC
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000B9AD4 File Offset: 0x000B7CD4
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000B9ADC File Offset: 0x000B7CDC
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000B9AF0 File Offset: 0x000B7CF0
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000B9AF8 File Offset: 0x000B7CF8
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000B9B00 File Offset: 0x000B7D00
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000B9B08 File Offset: 0x000B7D08
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000B9B10 File Offset: 0x000B7D10
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000B9B18 File Offset: 0x000B7D18
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000B9B20 File Offset: 0x000B7D20
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000B9B28 File Offset: 0x000B7D28
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000B9B30 File Offset: 0x000B7D30
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000B9B38 File Offset: 0x000B7D38
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000B9B44 File Offset: 0x000B7D44
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B9B50 File Offset: 0x000B7D50
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B9B5C File Offset: 0x000B7D5C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000B9B68 File Offset: 0x000B7D68
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000B9B74 File Offset: 0x000B7D74
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000B9B80 File Offset: 0x000B7D80
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000B9B8C File Offset: 0x000B7D8C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000B9B98 File Offset: 0x000B7D98
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000B9BA0 File Offset: 0x000B7DA0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000B9BA8 File Offset: 0x000B7DA8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000B9BB0 File Offset: 0x000B7DB0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B9BB8 File Offset: 0x000B7DB8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000B9BC0 File Offset: 0x000B7DC0
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B9BC8 File Offset: 0x000B7DC8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B9BD0 File Offset: 0x000B7DD0
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B9BD8 File Offset: 0x000B7DD8
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B9BE4 File Offset: 0x000B7DE4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000B9BEC File Offset: 0x000B7DEC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000B9BF4 File Offset: 0x000B7DF4
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000B9BFC File Offset: 0x000B7DFC
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000B9C04 File Offset: 0x000B7E04
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000B9C0C File Offset: 0x000B7E0C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000B9C14 File Offset: 0x000B7E14
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
