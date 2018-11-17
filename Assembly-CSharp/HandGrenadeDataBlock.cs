using System;
using uLink;
using UnityEngine;

// Token: 0x02000632 RID: 1586
public class HandGrenadeDataBlock : global::ThrowableItemDataBlock
{
	// Token: 0x06003442 RID: 13378 RVA: 0x000C2BE4 File Offset: 0x000C0DE4
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::HandGrenadeDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003443 RID: 13379 RVA: 0x000C2BEC File Offset: 0x000C0DEC
	public global::IHandGrenadeItem GetHandGrenadeItemInstance(global::IInventoryItem itemInstance)
	{
		return itemInstance as global::IHandGrenadeItem;
	}

	// Token: 0x06003444 RID: 13380 RVA: 0x000C2BF4 File Offset: 0x000C0DF4
	public override void PrimaryAttack(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		base.PrimaryAttack(vm, itemRep, itemInstance, ref sample);
		vm.Play("pull_pin");
		this.pullPinSound.Play();
		this.GetHandGrenadeItemInstance(itemInstance).nextPrimaryAttackTime = Time.time + 1000f;
		this.GetHandGrenadeItemInstance(itemInstance).nextSecondaryAttackTime = Time.time + 1000f;
	}

	// Token: 0x06003445 RID: 13381 RVA: 0x000C2C54 File Offset: 0x000C0E54
	public override void AttackReleased(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		Debug.Log("Attack released!!!");
		vm.Play("throw");
		vm.PlayQueued("deploy");
		this.GetHandGrenadeItemInstance(itemInstance).nextPrimaryAttackTime = Time.time + 1f;
		this.GetHandGrenadeItemInstance(itemInstance).nextSecondaryAttackTime = Time.time + 1f;
		global::Character component = global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::Character>();
		Vector3 eyesOrigin = component.eyesOrigin;
		Vector3 forward = component.eyesAngles.forward;
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteVector3(forward * this.GetHandGrenadeItemInstance(itemInstance).heldThrowStrength);
		Debug.Log("Throw strength is : " + this.GetHandGrenadeItemInstance(itemInstance).heldThrowStrength);
		this.GetHandGrenadeItemInstance(itemInstance).EndHoldingBack();
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x000C2D34 File Offset: 0x000C0F34
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003447 RID: 13383 RVA: 0x000C2D38 File Offset: 0x000C0F38
	protected override GameObject ThrowItem(global::ItemRepresentation rep, global::IThrowableItem item, Vector3 origin, Vector3 forward, uLink.NetworkViewID owner)
	{
		forward.Normalize();
		Vector3 velocity = forward * 20f;
		Vector3 position = origin + forward * 0.5f;
		return this.SpawnThrowItem(owner, this.throwObjectPrefab, position, Quaternion.LookRotation(Vector3.up), velocity);
	}

	// Token: 0x04001B5C RID: 7004
	public AudioClip pullPinSound;

	// Token: 0x02000633 RID: 1587
	private sealed class ITEM_TYPE : global::HandGrenadeItem<global::HandGrenadeDataBlock>, global::IHandGrenadeItem, global::IHeldItem, global::IInventoryItem, global::IThrowableItem, global::IWeaponItem
	{
		// Token: 0x06003448 RID: 13384 RVA: 0x000C2D88 File Offset: 0x000C0F88
		public ITEM_TYPE(global::HandGrenadeDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000C2D94 File Offset: 0x000C0F94
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000C2D9C File Offset: 0x000C0F9C
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000C2DA4 File Offset: 0x000C0FA4
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000C2DB0 File Offset: 0x000C0FB0
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000C2DB8 File Offset: 0x000C0FB8
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000C2DC4 File Offset: 0x000C0FC4
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000C2DCC File Offset: 0x000C0FCC
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000C2DD8 File Offset: 0x000C0FD8
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000C2DE4 File Offset: 0x000C0FE4
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C2DEC File Offset: 0x000C0FEC
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C2DF4 File Offset: 0x000C0FF4
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000C2E00 File Offset: 0x000C1000
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000C2E08 File Offset: 0x000C1008
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000C2E14 File Offset: 0x000C1014
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000C2E1C File Offset: 0x000C101C
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000C2E28 File Offset: 0x000C1028
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000C2E34 File Offset: 0x000C1034
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000C2E40 File Offset: 0x000C1040
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000C2E4C File Offset: 0x000C104C
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000C2E58 File Offset: 0x000C1058
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000C2E60 File Offset: 0x000C1060
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000C2E68 File Offset: 0x000C1068
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000C2E70 File Offset: 0x000C1070
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000C2E78 File Offset: 0x000C1078
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000C2E84 File Offset: 0x000C1084
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000C2E8C File Offset: 0x000C108C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000C2E94 File Offset: 0x000C1094
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000C2E9C File Offset: 0x000C109C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000C2EA4 File Offset: 0x000C10A4
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000C2EAC File Offset: 0x000C10AC
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000C2EB4 File Offset: 0x000C10B4
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000C2EBC File Offset: 0x000C10BC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000C2EC4 File Offset: 0x000C10C4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000C2ECC File Offset: 0x000C10CC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000C2ED4 File Offset: 0x000C10D4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000C2EE0 File Offset: 0x000C10E0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000C2EEC File Offset: 0x000C10EC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000C2F04 File Offset: 0x000C1104
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000C2F10 File Offset: 0x000C1110
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000C2F1C File Offset: 0x000C111C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000C2F28 File Offset: 0x000C1128
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000C2F34 File Offset: 0x000C1134
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000C2F3C File Offset: 0x000C113C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000C2F44 File Offset: 0x000C1144
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000C2F4C File Offset: 0x000C114C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000C2F54 File Offset: 0x000C1154
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000C2F5C File Offset: 0x000C115C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000C2F64 File Offset: 0x000C1164
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000C2F6C File Offset: 0x000C116C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000C2F74 File Offset: 0x000C1174
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000C2F80 File Offset: 0x000C1180
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000C2F88 File Offset: 0x000C1188
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000C2F90 File Offset: 0x000C1190
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000C2F98 File Offset: 0x000C1198
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000C2FA0 File Offset: 0x000C11A0
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000C2FA8 File Offset: 0x000C11A8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000C2FB0 File Offset: 0x000C11B0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
