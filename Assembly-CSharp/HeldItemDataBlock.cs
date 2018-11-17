using System;
using uLink;
using UnityEngine;

// Token: 0x02000576 RID: 1398
public class HeldItemDataBlock : ItemDataBlock
{
	// Token: 0x060030BC RID: 12476 RVA: 0x000BAD7C File Offset: 0x000B8F7C
	protected override IInventoryItem ConstructItem()
	{
		return new HeldItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x000BAD84 File Offset: 0x000B8F84
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x000BAD90 File Offset: 0x000B8F90
	public bool PollForAmmoDatablock(out ItemDataBlock ammoType)
	{
		if (this.IsSplittable())
		{
			ammoType = this;
			return true;
		}
		if (this is BulletWeaponDataBlock)
		{
			ammoType = ((BulletWeaponDataBlock)this).ammoType;
			return ammoType;
		}
		if (this is BowWeaponDataBlock)
		{
			ammoType = ((BowWeaponDataBlock)this).defaultAmmo;
			return ammoType;
		}
		ammoType = null;
		return false;
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x000BADF4 File Offset: 0x000B8FF4
	public virtual void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x000BADF8 File Offset: 0x000B8FF8
	public virtual void DoAction2(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x060030C1 RID: 12481 RVA: 0x000BADFC File Offset: 0x000B8FFC
	public virtual void DoAction3(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x060030C2 RID: 12482 RVA: 0x000BAE00 File Offset: 0x000B9000
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<bool>(this.secondaryFireAims, new object[0]);
		stream.Write<float>(this.aimSensitivtyPercent, new object[0]);
		stream.Write<string>(this.attachmentPoint, new object[0]);
	}

	// Token: 0x0400198C RID: 6540
	public ItemRepresentation _itemRepPrefab;

	// Token: 0x0400198D RID: 6541
	public ViewModel _viewModelPrefab;

	// Token: 0x0400198E RID: 6542
	public AudioClip deploySound;

	// Token: 0x0400198F RID: 6543
	public bool secondaryFireAims;

	// Token: 0x04001990 RID: 6544
	public float aimSensitivtyPercent = 0.4f;

	// Token: 0x04001991 RID: 6545
	public string attachmentPoint = "RArmHand";

	// Token: 0x04001992 RID: 6546
	public string animationGroupName;

	// Token: 0x02000577 RID: 1399
	private sealed class ITEM_TYPE : HeldItem<HeldItemDataBlock>, IHeldItem, IInventoryItem
	{
		// Token: 0x060030C3 RID: 12483 RVA: 0x000BAE4C File Offset: 0x000B904C
		public ITEM_TYPE(HeldItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000BAE58 File Offset: 0x000B9058
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000BAE60 File Offset: 0x000B9060
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000BAE6C File Offset: 0x000B906C
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000BAE78 File Offset: 0x000B9078
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000BAE84 File Offset: 0x000B9084
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000BAE90 File Offset: 0x000B9090
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000BAE98 File Offset: 0x000B9098
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000BAEA0 File Offset: 0x000B90A0
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000BAEA8 File Offset: 0x000B90A8
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000BAEB0 File Offset: 0x000B90B0
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000BAEBC File Offset: 0x000B90BC
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000BAEC4 File Offset: 0x000B90C4
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000BAECC File Offset: 0x000B90CC
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000BAED4 File Offset: 0x000B90D4
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000BAEDC File Offset: 0x000B90DC
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000BAEE4 File Offset: 0x000B90E4
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000BAEEC File Offset: 0x000B90EC
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000BAEF4 File Offset: 0x000B90F4
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000BAEFC File Offset: 0x000B90FC
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000BAF04 File Offset: 0x000B9104
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BAF0C File Offset: 0x000B910C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BAF18 File Offset: 0x000B9118
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BAF24 File Offset: 0x000B9124
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BAF30 File Offset: 0x000B9130
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BAF3C File Offset: 0x000B913C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BAF48 File Offset: 0x000B9148
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000BAF54 File Offset: 0x000B9154
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BAF60 File Offset: 0x000B9160
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BAF6C File Offset: 0x000B916C
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BAF74 File Offset: 0x000B9174
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BAF7C File Offset: 0x000B917C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BAF84 File Offset: 0x000B9184
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BAF8C File Offset: 0x000B918C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BAF94 File Offset: 0x000B9194
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BAF9C File Offset: 0x000B919C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000BAFA4 File Offset: 0x000B91A4
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000BAFAC File Offset: 0x000B91AC
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BAFB8 File Offset: 0x000B91B8
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BAFC0 File Offset: 0x000B91C0
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BAFC8 File Offset: 0x000B91C8
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BAFD0 File Offset: 0x000B91D0
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BAFD8 File Offset: 0x000B91D8
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000BAFE0 File Offset: 0x000B91E0
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000BAFE8 File Offset: 0x000B91E8
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
