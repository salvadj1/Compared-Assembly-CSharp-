using System;
using uLink;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class EquipmentWearer : IDLocalCharacter
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000358 RID: 856 RVA: 0x00010A18 File Offset: 0x0000EC18
	public ArmorModelRenderer armorModelRenderer
	{
		get
		{
			if (!this._armorModelRenderer.cached)
			{
				this._armorModelRenderer = base.GetLocal<ArmorModelRenderer>();
			}
			return this._armorModelRenderer.value;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000359 RID: 857 RVA: 0x00010A54 File Offset: 0x0000EC54
	public new ProtectionTakeDamage takeDamage
	{
		get
		{
			if (!this._protectionTakeDamage.cached)
			{
				this._protectionTakeDamage = (base.takeDamage as ProtectionTakeDamage);
			}
			return this._protectionTakeDamage.value;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600035A RID: 858 RVA: 0x00010A88 File Offset: 0x0000EC88
	public InventoryHolder inventoryHolder
	{
		get
		{
			if (!this._inventoryHolder.cached)
			{
				this._inventoryHolder = base.GetLocal<InventoryHolder>();
			}
			return this._inventoryHolder.value;
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00010AC4 File Offset: 0x0000ECC4
	public void EquipmentUpdate()
	{
		this.CalculateArmor();
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00010ACC File Offset: 0x0000ECCC
	public void CalculateArmor()
	{
		InventoryHolder inventoryHolder = this.inventoryHolder;
		ProtectionTakeDamage takeDamage = this.takeDamage;
		if (inventoryHolder && takeDamage)
		{
			DamageTypeList damageTypeList = new DamageTypeList();
			for (int i = 36; i < 40; i++)
			{
				IInventoryItem inventoryItem;
				ArmorDataBlock armorDataBlock;
				if (inventoryHolder.inventory.GetItem(i, out inventoryItem) && (armorDataBlock = (inventoryItem.datablock as ArmorDataBlock)))
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

	// Token: 0x0600035D RID: 861 RVA: 0x00010B60 File Offset: 0x0000ED60
	[RPC]
	protected void ArmorData(byte[] data)
	{
		DamageTypeList damageTypeList = new DamageTypeList();
		BitStream bitStream = new BitStream(data, false);
		for (int i = 0; i < 6; i++)
		{
			damageTypeList[i] = bitStream.ReadSingle();
		}
		ProtectionTakeDamage takeDamage = this.takeDamage;
		if (takeDamage)
		{
			takeDamage.SetArmorValues(damageTypeList);
		}
		if (base.localPlayerControlled)
		{
			RPOS.SetEquipmentDirty();
		}
	}

	// Token: 0x040002C4 RID: 708
	[NonSerialized]
	private CacheRef<ArmorModelRenderer> _armorModelRenderer;

	// Token: 0x040002C5 RID: 709
	[NonSerialized]
	private CacheRef<ProtectionTakeDamage> _protectionTakeDamage;

	// Token: 0x040002C6 RID: 710
	[NonSerialized]
	private CacheRef<InventoryHolder> _inventoryHolder;
}
