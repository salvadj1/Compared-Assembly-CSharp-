using System;
using uLink;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class EquipmentWearer : global::IDLocalCharacter
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x00012208 File Offset: 0x00010408
	public global::ArmorModelRenderer armorModelRenderer
	{
		get
		{
			if (!this._armorModelRenderer.cached)
			{
				this._armorModelRenderer = base.GetLocal<global::ArmorModelRenderer>();
			}
			return this._armorModelRenderer.value;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x00012244 File Offset: 0x00010444
	public new global::ProtectionTakeDamage takeDamage
	{
		get
		{
			if (!this._protectionTakeDamage.cached)
			{
				this._protectionTakeDamage = (base.takeDamage as global::ProtectionTakeDamage);
			}
			return this._protectionTakeDamage.value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060003D2 RID: 978 RVA: 0x00012278 File Offset: 0x00010478
	public global::InventoryHolder inventoryHolder
	{
		get
		{
			if (!this._inventoryHolder.cached)
			{
				this._inventoryHolder = base.GetLocal<global::InventoryHolder>();
			}
			return this._inventoryHolder.value;
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x000122B4 File Offset: 0x000104B4
	public void EquipmentUpdate()
	{
		this.CalculateArmor();
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x000122BC File Offset: 0x000104BC
	public void CalculateArmor()
	{
		global::InventoryHolder inventoryHolder = this.inventoryHolder;
		global::ProtectionTakeDamage takeDamage = this.takeDamage;
		if (inventoryHolder && takeDamage)
		{
			global::DamageTypeList damageTypeList = new global::DamageTypeList();
			for (int i = 36; i < 40; i++)
			{
				global::IInventoryItem inventoryItem;
				global::ArmorDataBlock armorDataBlock;
				if (inventoryHolder.inventory.GetItem(i, out inventoryItem) && (armorDataBlock = (inventoryItem.datablock as global::ArmorDataBlock)))
				{
					armorDataBlock.AddToDamageTypeList(damageTypeList);
				}
			}
			if (takeDamage)
			{
				takeDamage.SetArmorValues(damageTypeList);
			}
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00012350 File Offset: 0x00010550
	[RPC]
	protected void ArmorData(byte[] data)
	{
		global::DamageTypeList damageTypeList = new global::DamageTypeList();
		BitStream bitStream = new BitStream(data, false);
		for (int i = 0; i < 6; i++)
		{
			damageTypeList[i] = bitStream.ReadSingle();
		}
		global::ProtectionTakeDamage takeDamage = this.takeDamage;
		if (takeDamage)
		{
			takeDamage.SetArmorValues(damageTypeList);
		}
		if (base.localPlayerControlled)
		{
			global::RPOS.SetEquipmentDirty();
		}
	}

	// Token: 0x0400032F RID: 815
	[NonSerialized]
	private global::CacheRef<global::ArmorModelRenderer> _armorModelRenderer;

	// Token: 0x04000330 RID: 816
	[NonSerialized]
	private global::CacheRef<global::ProtectionTakeDamage> _protectionTakeDamage;

	// Token: 0x04000331 RID: 817
	[NonSerialized]
	private global::CacheRef<global::InventoryHolder> _inventoryHolder;
}
