using System;
using uLink;
using UnityEngine;

// Token: 0x02000550 RID: 1360
public class ArmorDataBlock : EquipmentDataBlock
{
	// Token: 0x06002DDC RID: 11740 RVA: 0x000B67EC File Offset: 0x000B49EC
	protected override IInventoryItem ConstructItem()
	{
		return new ArmorDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x000B67F4 File Offset: 0x000B49F4
	public void AddToDamageTypeList(DamageTypeList damageList)
	{
		for (int i = 0; i < 6; i++)
		{
			int index2;
			int index = index2 = i;
			float num = damageList[index2];
			damageList[index] = num + this.armorValues[i];
		}
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x000B6834 File Offset: 0x000B4A34
	public TArmorModel GetArmorModel<TArmorModel>() where TArmorModel : ArmorModel, new()
	{
		return (TArmorModel)((object)this.GetArmorModel(ArmorModelSlotUtility.GetArmorModelSlotForClass<TArmorModel>()));
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x000B6848 File Offset: 0x000B4A48
	public ArmorModel GetArmorModel(ArmorModelSlot slot)
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

	// Token: 0x06002DE0 RID: 11744 RVA: 0x000B68B8 File Offset: 0x000B4AB8
	public bool GetArmorModelSlot(out ArmorModelSlot slot)
	{
		if (!this.armorModel)
		{
			slot = (ArmorModelSlot)4;
		}
		else
		{
			slot = this.armorModel.slot;
		}
		return slot < (ArmorModelSlot)4;
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x000B68F0 File Offset: 0x000B4AF0
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddConditionInfo(tipItem);
		infoWindow.AddSectionTitle("Protection", 0f);
		for (int i = 0; i < 6; i++)
		{
			if (this.armorValues[i] != 0f)
			{
				float contentHeight = infoWindow.GetContentHeight();
				GameObject gameObject = infoWindow.AddBasicLabel(TakeDamage.DamageIndexToString((DamageTypeIndex)i), 0f);
				GameObject gameObject2 = infoWindow.AddBasicLabel("+" + ((int)this.armorValues[i]).ToString("N0"), 0f);
				gameObject2.transform.SetLocalPositionX(145f);
				gameObject2.GetComponentInChildren<UILabel>().color = Color.green;
				gameObject.transform.SetLocalPositionY(-(contentHeight + 10f));
				gameObject2.transform.SetLocalPositionY(-(contentHeight + 10f));
			}
		}
		infoWindow.AddSectionTitle("Equipment Slot", 20f);
		string text = "Head, Chest, Legs, Feet";
		if ((this._itemFlags & Inventory.SlotFlags.Head) == Inventory.SlotFlags.Head)
		{
			text = "Head";
		}
		else if ((this._itemFlags & Inventory.SlotFlags.Chest) == Inventory.SlotFlags.Chest)
		{
			text = "Chest";
		}
		infoWindow.AddBasicLabel(text, 10f);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x000B6A48 File Offset: 0x000B4C48
	public override void OnEquipped(IEquipmentItem item)
	{
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x000B6A4C File Offset: 0x000B4C4C
	public override void OnUnEquipped(IEquipmentItem item)
	{
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x000B6A50 File Offset: 0x000B4C50
	public override string GetItemDescription()
	{
		return "This is an piece of armor. Drag it to it's corresponding slot in the armor window and it will provide additional protection";
	}

	// Token: 0x0400190A RID: 6410
	public DamageTypeList armorValues;

	// Token: 0x0400190B RID: 6411
	[SerializeField]
	protected ArmorModel armorModel;

	// Token: 0x02000551 RID: 1361
	private sealed class ITEM_TYPE : ArmorItem<ArmorDataBlock>, IArmorItem, IEquipmentItem, IInventoryItem
	{
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000B6A58 File Offset: 0x000B4C58
		public ITEM_TYPE(ArmorDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000B6A64 File Offset: 0x000B4C64
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B6A6C File Offset: 0x000B4C6C
		void OnUnEquipped()
		{
			base.OnUnEquipped();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B6A74 File Offset: 0x000B4C74
		void OnEquipped()
		{
			base.OnEquipped();
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000B6A7C File Offset: 0x000B4C7C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000B6A84 File Offset: 0x000B4C84
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000B6A8C File Offset: 0x000B4C8C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000B6A94 File Offset: 0x000B4C94
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000B6AA0 File Offset: 0x000B4CA0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B6AAC File Offset: 0x000B4CAC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B6AB8 File Offset: 0x000B4CB8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B6AC4 File Offset: 0x000B4CC4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B6AD0 File Offset: 0x000B4CD0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B6ADC File Offset: 0x000B4CDC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000B6AE8 File Offset: 0x000B4CE8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B6AF4 File Offset: 0x000B4CF4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000B6AFC File Offset: 0x000B4CFC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000B6B04 File Offset: 0x000B4D04
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000B6B0C File Offset: 0x000B4D0C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000B6B14 File Offset: 0x000B4D14
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000B6B1C File Offset: 0x000B4D1C
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000B6B24 File Offset: 0x000B4D24
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000B6B2C File Offset: 0x000B4D2C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000B6B34 File Offset: 0x000B4D34
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000B6B40 File Offset: 0x000B4D40
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000B6B48 File Offset: 0x000B4D48
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000B6B50 File Offset: 0x000B4D50
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000B6B58 File Offset: 0x000B4D58
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000B6B60 File Offset: 0x000B4D60
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000B6B68 File Offset: 0x000B4D68
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000B6B70 File Offset: 0x000B4D70
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
