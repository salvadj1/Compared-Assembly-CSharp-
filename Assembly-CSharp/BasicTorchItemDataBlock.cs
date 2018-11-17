using System;
using uLink;
using UnityEngine;

// Token: 0x02000614 RID: 1556
public class BasicTorchItemDataBlock : global::HeldItemDataBlock
{
	// Token: 0x0600322B RID: 12843 RVA: 0x000BF368 File Offset: 0x000BD568
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BasicTorchItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x000BF370 File Offset: 0x000BD570
	public void DoActualIgnite(global::ItemRepresentation itemRep, global::IBasicTorchItem itemInstance, global::ViewModel vm)
	{
		this.Ignite(vm, itemRep, itemInstance);
		itemRep.Action(2, 0);
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x000BF384 File Offset: 0x000BD584
	public void DoActualExtinguish(global::ItemRepresentation itemRep, global::IBasicTorchItem itemInstance, global::ViewModel vm)
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

	// Token: 0x0600322E RID: 12846 RVA: 0x000BF3D4 File Offset: 0x000BD5D4
	public override void DoAction2(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
		this.Ignite(null, itemRep, null);
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x000BF3E0 File Offset: 0x000BD5E0
	public override void DoAction3(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
		this.Extinguish(itemRep);
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x000BF3EC File Offset: 0x000BD5EC
	public void Extinguish(global::ItemRepresentation itemRep)
	{
		(itemRep as global::TorchItemRep).RepExtinguish();
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x000BF3FC File Offset: 0x000BD5FC
	public void Ignite(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBasicTorchItem torchItem)
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
		else if ((torchItem == null || !torchItem.light) && (!itemRep.networkView.isMine || global::actor.forceThirdPerson))
		{
			if (this.ThirdPersonLightPrefab)
			{
				((global::BasicTorchItemRep)itemRep)._myLightPrefab = this.ThirdPersonLightPrefab;
			}
			((global::BasicTorchItemRep)itemRep).RepIgnite();
			if (((global::BasicTorchItemRep)itemRep)._myLight && torchItem != null)
			{
				torchItem.light = ((global::BasicTorchItemRep)itemRep)._myLight;
			}
		}
	}

	// Token: 0x04001AE5 RID: 6885
	public GameObject FirstPersonLightPrefab;

	// Token: 0x04001AE6 RID: 6886
	public GameObject ThirdPersonLightPrefab;

	// Token: 0x02000615 RID: 1557
	private sealed class ITEM_TYPE : global::BasicTorchItem<global::BasicTorchItemDataBlock>, global::IBasicTorchItem, global::IHeldItem, global::IInventoryItem
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x000BF4E4 File Offset: 0x000BD6E4
		public ITEM_TYPE(global::BasicTorchItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000BF4F0 File Offset: 0x000BD6F0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000BF4F8 File Offset: 0x000BD6F8
		void Ignite()
		{
			base.Ignite();
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000BF500 File Offset: 0x000BD700
		bool get_isLit()
		{
			return base.isLit;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000BF508 File Offset: 0x000BD708
		void set_isLit(bool value)
		{
			base.isLit = value;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000BF514 File Offset: 0x000BD714
		GameObject get_light()
		{
			return base.light;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000BF51C File Offset: 0x000BD71C
		void set_light(GameObject value)
		{
			base.light = value;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000BF528 File Offset: 0x000BD728
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000BF534 File Offset: 0x000BD734
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000BF540 File Offset: 0x000BD740
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000BF54C File Offset: 0x000BD74C
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000BF558 File Offset: 0x000BD758
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000BF560 File Offset: 0x000BD760
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000BF568 File Offset: 0x000BD768
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000BF570 File Offset: 0x000BD770
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000BF578 File Offset: 0x000BD778
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000BF584 File Offset: 0x000BD784
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000BF58C File Offset: 0x000BD78C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000BF594 File Offset: 0x000BD794
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000BF59C File Offset: 0x000BD79C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000BF5A4 File Offset: 0x000BD7A4
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000BF5AC File Offset: 0x000BD7AC
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000BF5BC File Offset: 0x000BD7BC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000BF5C4 File Offset: 0x000BD7C4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000BF5CC File Offset: 0x000BD7CC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000BF5D4 File Offset: 0x000BD7D4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000BF5E0 File Offset: 0x000BD7E0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000BF5EC File Offset: 0x000BD7EC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000BF5F8 File Offset: 0x000BD7F8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000BF604 File Offset: 0x000BD804
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000BF610 File Offset: 0x000BD810
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000BF61C File Offset: 0x000BD81C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000BF628 File Offset: 0x000BD828
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000BF634 File Offset: 0x000BD834
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000BF63C File Offset: 0x000BD83C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000BF644 File Offset: 0x000BD844
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000BF64C File Offset: 0x000BD84C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000BF654 File Offset: 0x000BD854
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000BF65C File Offset: 0x000BD85C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000BF664 File Offset: 0x000BD864
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000BF66C File Offset: 0x000BD86C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000BF674 File Offset: 0x000BD874
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000BF680 File Offset: 0x000BD880
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000BF688 File Offset: 0x000BD888
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000BF690 File Offset: 0x000BD890
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000BF698 File Offset: 0x000BD898
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000BF6A0 File Offset: 0x000BD8A0
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000BF6A8 File Offset: 0x000BD8A8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000BF6B0 File Offset: 0x000BD8B0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
