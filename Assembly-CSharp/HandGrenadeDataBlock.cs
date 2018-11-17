using System;
using uLink;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class HandGrenadeDataBlock : ThrowableItemDataBlock
{
	// Token: 0x0600307A RID: 12410 RVA: 0x000BA988 File Offset: 0x000B8B88
	protected override IInventoryItem ConstructItem()
	{
		return new HandGrenadeDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x000BA990 File Offset: 0x000B8B90
	public IHandGrenadeItem GetHandGrenadeItemInstance(IInventoryItem itemInstance)
	{
		return itemInstance as IHandGrenadeItem;
	}

	// Token: 0x0600307C RID: 12412 RVA: 0x000BA998 File Offset: 0x000B8B98
	public override void PrimaryAttack(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		base.PrimaryAttack(vm, itemRep, itemInstance, ref sample);
		vm.Play("pull_pin");
		this.pullPinSound.Play();
		this.GetHandGrenadeItemInstance(itemInstance).nextPrimaryAttackTime = Time.time + 1000f;
		this.GetHandGrenadeItemInstance(itemInstance).nextSecondaryAttackTime = Time.time + 1000f;
	}

	// Token: 0x0600307D RID: 12413 RVA: 0x000BA9F8 File Offset: 0x000B8BF8
	public override void AttackReleased(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		Debug.Log("Attack released!!!");
		vm.Play("throw");
		vm.PlayQueued("deploy");
		this.GetHandGrenadeItemInstance(itemInstance).nextPrimaryAttackTime = Time.time + 1f;
		this.GetHandGrenadeItemInstance(itemInstance).nextSecondaryAttackTime = Time.time + 1f;
		Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
		Vector3 eyesOrigin = component.eyesOrigin;
		Vector3 forward = component.eyesAngles.forward;
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteVector3(forward * this.GetHandGrenadeItemInstance(itemInstance).heldThrowStrength);
		Debug.Log("Throw strength is : " + this.GetHandGrenadeItemInstance(itemInstance).heldThrowStrength);
		this.GetHandGrenadeItemInstance(itemInstance).EndHoldingBack();
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x0600307E RID: 12414 RVA: 0x000BAAD8 File Offset: 0x000B8CD8
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x0600307F RID: 12415 RVA: 0x000BAADC File Offset: 0x000B8CDC
	protected override GameObject ThrowItem(ItemRepresentation rep, IThrowableItem item, Vector3 origin, Vector3 forward, NetworkViewID owner)
	{
		forward.Normalize();
		Vector3 velocity = forward * 20f;
		Vector3 position = origin + forward * 0.5f;
		return this.SpawnThrowItem(owner, this.throwObjectPrefab, position, Quaternion.LookRotation(Vector3.up), velocity);
	}

	// Token: 0x0400198B RID: 6539
	public AudioClip pullPinSound;

	// Token: 0x02000575 RID: 1397
	private sealed class ITEM_TYPE : HandGrenadeItem<HandGrenadeDataBlock>, IHandGrenadeItem, IHeldItem, IInventoryItem, IThrowableItem, IWeaponItem
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x000BAB2C File Offset: 0x000B8D2C
		public ITEM_TYPE(HandGrenadeDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x000BAB38 File Offset: 0x000B8D38
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000BAB40 File Offset: 0x000B8D40
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000BAB48 File Offset: 0x000B8D48
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000BAB54 File Offset: 0x000B8D54
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000BAB5C File Offset: 0x000B8D5C
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000BAB68 File Offset: 0x000B8D68
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000BAB70 File Offset: 0x000B8D70
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000BAB7C File Offset: 0x000B8D7C
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000BAB88 File Offset: 0x000B8D88
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000BAB90 File Offset: 0x000B8D90
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000BAB98 File Offset: 0x000B8D98
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000BABA4 File Offset: 0x000B8DA4
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000BABAC File Offset: 0x000B8DAC
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000BABB8 File Offset: 0x000B8DB8
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000BABC0 File Offset: 0x000B8DC0
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000BABCC File Offset: 0x000B8DCC
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000BABD8 File Offset: 0x000B8DD8
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000BABE4 File Offset: 0x000B8DE4
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000BABF0 File Offset: 0x000B8DF0
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000BABFC File Offset: 0x000B8DFC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000BAC04 File Offset: 0x000B8E04
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000BAC0C File Offset: 0x000B8E0C
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000BAC14 File Offset: 0x000B8E14
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000BAC1C File Offset: 0x000B8E1C
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000BAC28 File Offset: 0x000B8E28
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000BAC30 File Offset: 0x000B8E30
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000BAC38 File Offset: 0x000B8E38
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000BAC40 File Offset: 0x000B8E40
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000BAC48 File Offset: 0x000B8E48
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000BAC50 File Offset: 0x000B8E50
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000BAC58 File Offset: 0x000B8E58
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000BAC60 File Offset: 0x000B8E60
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000BAC68 File Offset: 0x000B8E68
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000BAC70 File Offset: 0x000B8E70
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000BAC78 File Offset: 0x000B8E78
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000BAC84 File Offset: 0x000B8E84
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000BAC90 File Offset: 0x000B8E90
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000BAC9C File Offset: 0x000B8E9C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000BACA8 File Offset: 0x000B8EA8
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000BACB4 File Offset: 0x000B8EB4
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000BACC0 File Offset: 0x000B8EC0
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000BACCC File Offset: 0x000B8ECC
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000BACD8 File Offset: 0x000B8ED8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000BACE0 File Offset: 0x000B8EE0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000BACE8 File Offset: 0x000B8EE8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000BACF0 File Offset: 0x000B8EF0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000BACF8 File Offset: 0x000B8EF8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000BAD00 File Offset: 0x000B8F00
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000BAD08 File Offset: 0x000B8F08
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000BAD10 File Offset: 0x000B8F10
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000BAD18 File Offset: 0x000B8F18
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000BAD24 File Offset: 0x000B8F24
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000BAD2C File Offset: 0x000B8F2C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000BAD34 File Offset: 0x000B8F34
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000BAD3C File Offset: 0x000B8F3C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000BAD44 File Offset: 0x000B8F44
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000BAD4C File Offset: 0x000B8F4C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000BAD54 File Offset: 0x000B8F54
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
