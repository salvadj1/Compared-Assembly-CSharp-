using System;
using uLink;
using UnityEngine;

// Token: 0x0200065A RID: 1626
public class TorchItemDataBlock : global::ThrowableItemDataBlock
{
	// Token: 0x0600375E RID: 14174 RVA: 0x000C643C File Offset: 0x000C463C
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::TorchItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600375F RID: 14175 RVA: 0x000C6444 File Offset: 0x000C4644
	public global::ITorchItem GetTorchInstance(global::IThrowableItem itemInstance)
	{
		return itemInstance as global::ITorchItem;
	}

	// Token: 0x06003760 RID: 14176 RVA: 0x000C644C File Offset: 0x000C464C
	public global::TorchItemRep GetTorchRep(global::ItemRepresentation rep)
	{
		return rep as global::TorchItemRep;
	}

	// Token: 0x06003761 RID: 14177 RVA: 0x000C6454 File Offset: 0x000C4654
	public override void PrimaryAttack(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		global::ITorchItem torchInstance = this.GetTorchInstance(itemInstance);
		if (torchInstance.isLit)
		{
			return;
		}
		if (vm)
		{
			vm.Play("ignite");
		}
		torchInstance.realIgniteTime = Time.time + 0.8f;
		torchInstance.nextPrimaryAttackTime = Time.time + 1.5f;
		torchInstance.nextSecondaryAttackTime = Time.time + 1.5f;
	}

	// Token: 0x06003762 RID: 14178 RVA: 0x000C64C0 File Offset: 0x000C46C0
	public override void DoActualThrow(global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, global::ViewModel vm)
	{
		global::Character component = global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::Character>();
		Vector3 eyesOrigin = component.eyesOrigin;
		Vector3 forward = component.eyesAngles.forward;
		if (vm)
		{
			vm.PlayQueued("deploy");
		}
		this.GetTorchInstance(itemInstance).Extinguish();
		int num = 1;
		if (itemInstance.Consume(ref num))
		{
			itemInstance.inventory.RemoveItem(itemInstance.slot);
		}
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteVector3(forward);
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x06003763 RID: 14179 RVA: 0x000C655C File Offset: 0x000C475C
	public void DoActualIgnite(global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, global::ViewModel vm)
	{
		this.Ignite(vm, itemRep, this.GetTorchInstance(itemInstance));
		itemRep.Action(2, 0);
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x000C6578 File Offset: 0x000C4778
	public override void SecondaryAttack(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		global::ITorchItem torchInstance = this.GetTorchInstance(itemInstance);
		if (!torchInstance.isLit)
		{
			this.PrimaryAttack(vm, itemRep, itemInstance, ref sample);
			torchInstance.forceSecondaryTime = Time.time + 1.51f;
			return;
		}
		if (vm)
		{
			vm.Play("throw");
		}
		torchInstance.realThrowTime = Time.time + 0.5f;
		torchInstance.nextPrimaryAttackTime = Time.time + 1.5f;
		torchInstance.nextSecondaryAttackTime = Time.time + 1.5f;
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x000C6600 File Offset: 0x000C4800
	public override void DoAction2(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
		this.Ignite(null, itemRep, null);
	}

	// Token: 0x06003766 RID: 14182 RVA: 0x000C660C File Offset: 0x000C480C
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		(rep as global::TorchItemRep).RepExtinguish();
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x000C661C File Offset: 0x000C481C
	public override void DoAction3(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x000C6620 File Offset: 0x000C4820
	public void OnExtinguish(global::ViewModel vm, global::ItemRepresentation itemRep, global::ITorchItem torchItem)
	{
	}

	// Token: 0x06003769 RID: 14185 RVA: 0x000C6624 File Offset: 0x000C4824
	public void Ignite(global::ViewModel vm, global::ItemRepresentation itemRep, global::ITorchItem torchItem)
	{
		if (torchItem != null)
		{
			torchItem.Ignite();
		}
		bool flag = vm != null;
		if (flag)
		{
			this.StrikeSound.Play();
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
				((global::TorchItemRep)itemRep)._myLightPrefab = this.ThirdPersonLightPrefab;
			}
			((global::TorchItemRep)itemRep).RepIgnite();
			if (((global::TorchItemRep)itemRep)._myLight && torchItem != null)
			{
				torchItem.light = ((global::TorchItemRep)itemRep)._myLight;
			}
		}
	}

	// Token: 0x04001BD3 RID: 7123
	public GameObject FirstPersonLightPrefab;

	// Token: 0x04001BD4 RID: 7124
	public GameObject ThirdPersonLightPrefab;

	// Token: 0x04001BD5 RID: 7125
	public AudioClip StrikeSound;

	// Token: 0x0200065B RID: 1627
	private sealed class ITEM_TYPE : global::TorchItem<global::TorchItemDataBlock>, global::IHeldItem, global::IInventoryItem, global::IThrowableItem, global::ITorchItem, global::IWeaponItem
	{
		// Token: 0x0600376A RID: 14186 RVA: 0x000C6718 File Offset: 0x000C4918
		public ITEM_TYPE(global::TorchItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000C6724 File Offset: 0x000C4924
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000C672C File Offset: 0x000C492C
		void Ignite()
		{
			base.Ignite();
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000C6734 File Offset: 0x000C4934
		void Extinguish()
		{
			base.Extinguish();
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000C673C File Offset: 0x000C493C
		bool get_isLit()
		{
			return base.isLit;
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000C6744 File Offset: 0x000C4944
		float get_realThrowTime()
		{
			return base.realThrowTime;
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000C674C File Offset: 0x000C494C
		void set_realThrowTime(float value)
		{
			base.realThrowTime = value;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000C6758 File Offset: 0x000C4958
		float get_realIgniteTime()
		{
			return base.realIgniteTime;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000C6760 File Offset: 0x000C4960
		void set_realIgniteTime(float value)
		{
			base.realIgniteTime = value;
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000C676C File Offset: 0x000C496C
		float get_forceSecondaryTime()
		{
			return base.forceSecondaryTime;
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000C6774 File Offset: 0x000C4974
		void set_forceSecondaryTime(float value)
		{
			base.forceSecondaryTime = value;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000C6780 File Offset: 0x000C4980
		GameObject get_light()
		{
			return base.light;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000C6788 File Offset: 0x000C4988
		void set_light(GameObject value)
		{
			base.light = value;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000C6794 File Offset: 0x000C4994
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000C679C File Offset: 0x000C499C
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000C67A8 File Offset: 0x000C49A8
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000C67B0 File Offset: 0x000C49B0
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000C67BC File Offset: 0x000C49BC
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000C67C4 File Offset: 0x000C49C4
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000C67D0 File Offset: 0x000C49D0
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000C67DC File Offset: 0x000C49DC
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000C67E4 File Offset: 0x000C49E4
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000C67EC File Offset: 0x000C49EC
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000C67F8 File Offset: 0x000C49F8
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000C6800 File Offset: 0x000C4A00
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000C680C File Offset: 0x000C4A0C
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000C6814 File Offset: 0x000C4A14
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000C6820 File Offset: 0x000C4A20
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000C682C File Offset: 0x000C4A2C
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000C6838 File Offset: 0x000C4A38
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000C6844 File Offset: 0x000C4A44
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000C6850 File Offset: 0x000C4A50
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000C6858 File Offset: 0x000C4A58
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000C6860 File Offset: 0x000C4A60
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000C6868 File Offset: 0x000C4A68
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000C6870 File Offset: 0x000C4A70
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000C687C File Offset: 0x000C4A7C
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000C6884 File Offset: 0x000C4A84
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000C688C File Offset: 0x000C4A8C
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000C6894 File Offset: 0x000C4A94
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000C689C File Offset: 0x000C4A9C
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000C68A4 File Offset: 0x000C4AA4
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000C68AC File Offset: 0x000C4AAC
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000C68B4 File Offset: 0x000C4AB4
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000C68BC File Offset: 0x000C4ABC
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000C68C4 File Offset: 0x000C4AC4
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000C68CC File Offset: 0x000C4ACC
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000C68D8 File Offset: 0x000C4AD8
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000C68E4 File Offset: 0x000C4AE4
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000C68F0 File Offset: 0x000C4AF0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000C68FC File Offset: 0x000C4AFC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000C6908 File Offset: 0x000C4B08
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000C6914 File Offset: 0x000C4B14
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000C6920 File Offset: 0x000C4B20
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000C692C File Offset: 0x000C4B2C
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000C6934 File Offset: 0x000C4B34
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000C693C File Offset: 0x000C4B3C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000C6944 File Offset: 0x000C4B44
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000C694C File Offset: 0x000C4B4C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000C6954 File Offset: 0x000C4B54
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000C695C File Offset: 0x000C4B5C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000C6964 File Offset: 0x000C4B64
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000C696C File Offset: 0x000C4B6C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000C6978 File Offset: 0x000C4B78
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000C6980 File Offset: 0x000C4B80
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000C6988 File Offset: 0x000C4B88
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000C6990 File Offset: 0x000C4B90
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000C6998 File Offset: 0x000C4B98
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000C69A0 File Offset: 0x000C4BA0
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000C69A8 File Offset: 0x000C4BA8
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
