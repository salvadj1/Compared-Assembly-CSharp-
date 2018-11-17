using System;
using uLink;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public class BasicTorchItemDataBlock : HeldItemDataBlock
{
	// Token: 0x06002E63 RID: 11875 RVA: 0x000B710C File Offset: 0x000B530C
	protected override IInventoryItem ConstructItem()
	{
		return new BasicTorchItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002E64 RID: 11876 RVA: 0x000B7114 File Offset: 0x000B5314
	public void DoActualIgnite(ItemRepresentation itemRep, IBasicTorchItem itemInstance, ViewModel vm)
	{
		this.Ignite(vm, itemRep, itemInstance);
		itemRep.Action(2, 0);
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x000B7128 File Offset: 0x000B5328
	public void DoActualExtinguish(ItemRepresentation itemRep, IBasicTorchItem itemInstance, ViewModel vm)
	{
		if (itemInstance == null)
		{
			Debug.Log("inst null");
		}
		if (itemRep == null)
		{
			Debug.Log("rep null");
		}
		if (vm == null)
		{
			Debug.Log("vm null ");
		}
		itemInstance.Extinguish();
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x000B7178 File Offset: 0x000B5378
	public override void DoAction2(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
		this.Ignite(null, itemRep, null);
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x000B7184 File Offset: 0x000B5384
	public override void DoAction3(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
		this.Extinguish(itemRep);
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x000B7190 File Offset: 0x000B5390
	public void Extinguish(ItemRepresentation itemRep)
	{
		(itemRep as TorchItemRep).RepExtinguish();
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x000B71A0 File Offset: 0x000B53A0
	public void Ignite(ViewModel vm, ItemRepresentation itemRep, IBasicTorchItem torchItem)
	{
		if (torchItem != null)
		{
			torchItem.Ignite();
		}
		bool flag = vm != null;
		if (flag)
		{
			GameObject light = vm.socketMap["muzzle"].socket.InstantiateAsChild(this.FirstPersonLightPrefab, false);
			if (torchItem != null)
			{
				torchItem.light = light;
			}
		}
		else if ((torchItem == null || !torchItem.light) && (!itemRep.networkView.isMine || actor.forceThirdPerson))
		{
			if (this.ThirdPersonLightPrefab)
			{
				((BasicTorchItemRep)itemRep)._myLightPrefab = this.ThirdPersonLightPrefab;
			}
			((BasicTorchItemRep)itemRep).RepIgnite();
			if (((BasicTorchItemRep)itemRep)._myLight && torchItem != null)
			{
				torchItem.light = ((BasicTorchItemRep)itemRep)._myLight;
			}
		}
	}

	// Token: 0x04001914 RID: 6420
	public GameObject FirstPersonLightPrefab;

	// Token: 0x04001915 RID: 6421
	public GameObject ThirdPersonLightPrefab;

	// Token: 0x02000557 RID: 1367
	private sealed class ITEM_TYPE : BasicTorchItem<BasicTorchItemDataBlock>, IBasicTorchItem, IHeldItem, IInventoryItem
	{
		// Token: 0x06002E6A RID: 11882 RVA: 0x000B7288 File Offset: 0x000B5488
		public ITEM_TYPE(BasicTorchItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000B7294 File Offset: 0x000B5494
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000B729C File Offset: 0x000B549C
		void Ignite()
		{
			base.Ignite();
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000B72A4 File Offset: 0x000B54A4
		bool get_isLit()
		{
			return base.isLit;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000B72AC File Offset: 0x000B54AC
		void set_isLit(bool value)
		{
			base.isLit = value;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000B72B8 File Offset: 0x000B54B8
		GameObject get_light()
		{
			return base.light;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000B72C0 File Offset: 0x000B54C0
		void set_light(GameObject value)
		{
			base.light = value;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000B72CC File Offset: 0x000B54CC
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000B72D8 File Offset: 0x000B54D8
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000B72E4 File Offset: 0x000B54E4
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000B72F0 File Offset: 0x000B54F0
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000B72FC File Offset: 0x000B54FC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000B7304 File Offset: 0x000B5504
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000B730C File Offset: 0x000B550C
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000B7314 File Offset: 0x000B5514
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000B731C File Offset: 0x000B551C
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000B7328 File Offset: 0x000B5528
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000B7330 File Offset: 0x000B5530
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000B7338 File Offset: 0x000B5538
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000B7340 File Offset: 0x000B5540
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000B7348 File Offset: 0x000B5548
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000B7350 File Offset: 0x000B5550
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000B7358 File Offset: 0x000B5558
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000B7360 File Offset: 0x000B5560
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000B7368 File Offset: 0x000B5568
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000B7370 File Offset: 0x000B5570
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000B7378 File Offset: 0x000B5578
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000B7384 File Offset: 0x000B5584
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000B7390 File Offset: 0x000B5590
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000B739C File Offset: 0x000B559C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000B73A8 File Offset: 0x000B55A8
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000B73B4 File Offset: 0x000B55B4
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000B73C0 File Offset: 0x000B55C0
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000B73CC File Offset: 0x000B55CC
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000B73D8 File Offset: 0x000B55D8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000B73E0 File Offset: 0x000B55E0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000B73E8 File Offset: 0x000B55E8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000B73F0 File Offset: 0x000B55F0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000B73F8 File Offset: 0x000B55F8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000B7400 File Offset: 0x000B5600
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000B7408 File Offset: 0x000B5608
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000B7410 File Offset: 0x000B5610
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000B7418 File Offset: 0x000B5618
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000B7424 File Offset: 0x000B5624
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000B742C File Offset: 0x000B562C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000B7434 File Offset: 0x000B5634
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000B743C File Offset: 0x000B563C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000B7444 File Offset: 0x000B5644
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000B744C File Offset: 0x000B564C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000B7454 File Offset: 0x000B5654
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
