using System;
using uLink;
using UnityEngine;

// Token: 0x02000634 RID: 1588
public class HeldItemDataBlock : global::ItemDataBlock
{
	// Token: 0x06003484 RID: 13444 RVA: 0x000C2FD8 File Offset: 0x000C11D8
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::HeldItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003485 RID: 13445 RVA: 0x000C2FE0 File Offset: 0x000C11E0
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x06003486 RID: 13446 RVA: 0x000C2FEC File Offset: 0x000C11EC
	public bool PollForAmmoDatablock(out global::ItemDataBlock ammoType)
	{
		if (this.IsSplittable())
		{
			ammoType = this;
			return true;
		}
		if (this is global::BulletWeaponDataBlock)
		{
			ammoType = ((global::BulletWeaponDataBlock)this).ammoType;
			return ammoType;
		}
		if (this is global::BowWeaponDataBlock)
		{
			ammoType = ((global::BowWeaponDataBlock)this).defaultAmmo;
			return ammoType;
		}
		ammoType = null;
		return false;
	}

	// Token: 0x06003487 RID: 13447 RVA: 0x000C3050 File Offset: 0x000C1250
	public virtual void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003488 RID: 13448 RVA: 0x000C3054 File Offset: 0x000C1254
	public virtual void DoAction2(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x000C3058 File Offset: 0x000C1258
	public virtual void DoAction3(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x000C305C File Offset: 0x000C125C
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<bool>(this.secondaryFireAims, new object[0]);
		stream.Write<float>(this.aimSensitivtyPercent, new object[0]);
		stream.Write<string>(this.attachmentPoint, new object[0]);
	}

	// Token: 0x04001B5D RID: 7005
	public global::ItemRepresentation _itemRepPrefab;

	// Token: 0x04001B5E RID: 7006
	public global::ViewModel _viewModelPrefab;

	// Token: 0x04001B5F RID: 7007
	public AudioClip deploySound;

	// Token: 0x04001B60 RID: 7008
	public bool secondaryFireAims;

	// Token: 0x04001B61 RID: 7009
	public float aimSensitivtyPercent = 0.4f;

	// Token: 0x04001B62 RID: 7010
	public string attachmentPoint = "RArmHand";

	// Token: 0x04001B63 RID: 7011
	public string animationGroupName;

	// Token: 0x02000635 RID: 1589
	private sealed class ITEM_TYPE : global::HeldItem<global::HeldItemDataBlock>, global::IHeldItem, global::IInventoryItem
	{
		// Token: 0x0600348B RID: 13451 RVA: 0x000C30A8 File Offset: 0x000C12A8
		public ITEM_TYPE(global::HeldItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x000C30B4 File Offset: 0x000C12B4
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000C30BC File Offset: 0x000C12BC
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000C30C8 File Offset: 0x000C12C8
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000C30D4 File Offset: 0x000C12D4
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000C30E0 File Offset: 0x000C12E0
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000C30EC File Offset: 0x000C12EC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000C30F4 File Offset: 0x000C12F4
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000C30FC File Offset: 0x000C12FC
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000C3104 File Offset: 0x000C1304
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000C310C File Offset: 0x000C130C
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000C3118 File Offset: 0x000C1318
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000C3120 File Offset: 0x000C1320
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000C3128 File Offset: 0x000C1328
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000C3130 File Offset: 0x000C1330
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000C3138 File Offset: 0x000C1338
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000C3140 File Offset: 0x000C1340
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000C3148 File Offset: 0x000C1348
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000C3150 File Offset: 0x000C1350
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000C3158 File Offset: 0x000C1358
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000C3160 File Offset: 0x000C1360
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000C3168 File Offset: 0x000C1368
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000C3174 File Offset: 0x000C1374
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000C3180 File Offset: 0x000C1380
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000C318C File Offset: 0x000C138C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000C3198 File Offset: 0x000C1398
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000C31A4 File Offset: 0x000C13A4
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000C31B0 File Offset: 0x000C13B0
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000C31BC File Offset: 0x000C13BC
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000C31C8 File Offset: 0x000C13C8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000C31D0 File Offset: 0x000C13D0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000C31D8 File Offset: 0x000C13D8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000C31E0 File Offset: 0x000C13E0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000C31E8 File Offset: 0x000C13E8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000C31F0 File Offset: 0x000C13F0
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000C31F8 File Offset: 0x000C13F8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000C3200 File Offset: 0x000C1400
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000C3208 File Offset: 0x000C1408
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000C3214 File Offset: 0x000C1414
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000C321C File Offset: 0x000C141C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000C3224 File Offset: 0x000C1424
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000C322C File Offset: 0x000C142C
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000C3234 File Offset: 0x000C1434
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000C323C File Offset: 0x000C143C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000C3244 File Offset: 0x000C1444
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
