using System;
using uLink;
using UnityEngine;

// Token: 0x0200060E RID: 1550
public class ArmorDataBlock : global::EquipmentDataBlock
{
	// Token: 0x060031A4 RID: 12708 RVA: 0x000BEA48 File Offset: 0x000BCC48
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ArmorDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x000BEA50 File Offset: 0x000BCC50
	public void AddToDamageTypeList(global::DamageTypeList damageList)
	{
		for (int i = 0; i < 6; i++)
		{
			int index2;
			int index = index2 = i;
			float num = damageList[index2];
			damageList[index] = num + this.armorValues[i];
		}
	}

	// Token: 0x060031A6 RID: 12710 RVA: 0x000BEA90 File Offset: 0x000BCC90
	public TArmorModel GetArmorModel<TArmorModel>() where TArmorModel : global::ArmorModel, new()
	{
		return (TArmorModel)((object)this.GetArmorModel(global::ArmorModelSlotUtility.GetArmorModelSlotForClass<TArmorModel>()));
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
	public global::ArmorModel GetArmorModel(global::ArmorModelSlot slot)
	{
		if (!this.armorModel)
		{
			Debug.LogWarning("No armorModel set to datablock " + this, this);
			return null;
		}
		if (this.armorModel.slot != slot)
		{
			Debug.LogError(string.Format("The armor model for {0} is {1}. Its not for slot {2}", this, this.armorModel.slot, slot), this);
			return null;
		}
		return this.armorModel;
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x000BEB14 File Offset: 0x000BCD14
	public bool GetArmorModelSlot(out global::ArmorModelSlot slot)
	{
		if (!this.armorModel)
		{
			slot = (global::ArmorModelSlot)4;
		}
		else
		{
			slot = this.armorModel.slot;
		}
		return slot < (global::ArmorModelSlot)4;
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x000BEB4C File Offset: 0x000BCD4C
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddConditionInfo(tipItem);
		infoWindow.AddSectionTitle("Protection", 0f);
		for (int i = 0; i < 6; i++)
		{
			if (this.armorValues[i] != 0f)
			{
				float contentHeight = infoWindow.GetContentHeight();
				GameObject gameObject = infoWindow.AddBasicLabel(global::TakeDamage.DamageIndexToString((global::DamageTypeIndex)i), 0f);
				GameObject gameObject2 = infoWindow.AddBasicLabel("+" + ((int)this.armorValues[i]).ToString("N0"), 0f);
				gameObject2.transform.SetLocalPositionX(145f);
				gameObject2.GetComponentInChildren<global::UILabel>().color = Color.green;
				gameObject.transform.SetLocalPositionY(-(contentHeight + 10f));
				gameObject2.transform.SetLocalPositionY(-(contentHeight + 10f));
			}
		}
		infoWindow.AddSectionTitle("Equipment Slot", 20f);
		string text = "Head, Chest, Legs, Feet";
		if ((this._itemFlags & global::Inventory.SlotFlags.Head) == global::Inventory.SlotFlags.Head)
		{
			text = "Head";
		}
		else if ((this._itemFlags & global::Inventory.SlotFlags.Chest) == global::Inventory.SlotFlags.Chest)
		{
			text = "Chest";
		}
		infoWindow.AddBasicLabel(text, 10f);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x000BECA4 File Offset: 0x000BCEA4
	public override void OnEquipped(global::IEquipmentItem item)
	{
	}

	// Token: 0x060031AB RID: 12715 RVA: 0x000BECA8 File Offset: 0x000BCEA8
	public override void OnUnEquipped(global::IEquipmentItem item)
	{
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x000BECAC File Offset: 0x000BCEAC
	public override string GetItemDescription()
	{
		return "This is an piece of armor. Drag it to it's corresponding slot in the armor window and it will provide additional protection";
	}

	// Token: 0x04001ADB RID: 6875
	public global::DamageTypeList armorValues;

	// Token: 0x04001ADC RID: 6876
	[SerializeField]
	protected global::ArmorModel armorModel;

	// Token: 0x0200060F RID: 1551
	private sealed class ITEM_TYPE : global::ArmorItem<global::ArmorDataBlock>, global::IArmorItem, global::IEquipmentItem, global::IInventoryItem
	{
		// Token: 0x060031AD RID: 12717 RVA: 0x000BECB4 File Offset: 0x000BCEB4
		public ITEM_TYPE(global::ArmorDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000BECC0 File Offset: 0x000BCEC0
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000BECC8 File Offset: 0x000BCEC8
		void OnUnEquipped()
		{
			base.OnUnEquipped();
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000BECD0 File Offset: 0x000BCED0
		void OnEquipped()
		{
			base.OnEquipped();
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000BECD8 File Offset: 0x000BCED8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000BECE0 File Offset: 0x000BCEE0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000BECE8 File Offset: 0x000BCEE8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000BECF0 File Offset: 0x000BCEF0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000BECFC File Offset: 0x000BCEFC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000BED08 File Offset: 0x000BCF08
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000BED14 File Offset: 0x000BCF14
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000BED20 File Offset: 0x000BCF20
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000BED2C File Offset: 0x000BCF2C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000BED38 File Offset: 0x000BCF38
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000BED44 File Offset: 0x000BCF44
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000BED50 File Offset: 0x000BCF50
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000BED58 File Offset: 0x000BCF58
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000BED60 File Offset: 0x000BCF60
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000BED68 File Offset: 0x000BCF68
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000BED70 File Offset: 0x000BCF70
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000BED78 File Offset: 0x000BCF78
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000BED80 File Offset: 0x000BCF80
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000BED88 File Offset: 0x000BCF88
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000BED90 File Offset: 0x000BCF90
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000BED9C File Offset: 0x000BCF9C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000BEDA4 File Offset: 0x000BCFA4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000BEDAC File Offset: 0x000BCFAC
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000BEDB4 File Offset: 0x000BCFB4
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BEDBC File Offset: 0x000BCFBC
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BEDC4 File Offset: 0x000BCFC4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BEDCC File Offset: 0x000BCFCC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
