using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020006B9 RID: 1721
public class PlayerInventory : global::CraftingInventory, global::FixedSizeInventory
{
	// Token: 0x06003A92 RID: 14994 RVA: 0x000CD998 File Offset: 0x000CBB98
	protected void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			base.InitializeThisFixedSizeInventory();
		}
	}

	// Token: 0x06003A93 RID: 14995 RVA: 0x000CD9B4 File Offset: 0x000CBBB4
	public void MakeBPsDirty()
	{
		this.bpDirty = true;
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x000CD9C0 File Offset: 0x000CBBC0
	protected override void ConfigureSlots(int totalCount, ref global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> ranges, ref global::Inventory.SlotFlags[] flags)
	{
		if (totalCount != 40)
		{
			Debug.LogError("Invalid size for player inventory " + totalCount, this);
		}
		ranges = global::PlayerInventory.LateLoaded.SlotRanges;
		flags = global::PlayerInventory.LateLoaded.EveryPlayerInventory;
		if (base.networkView.isMine)
		{
			this._boundBPs = new List<global::BlueprintDataBlock>();
		}
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x000CDA18 File Offset: 0x000CBC18
	public bool KnowsBP(global::BlueprintDataBlock bp)
	{
		return bp && this._boundBPs.Contains(bp);
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x000CDA34 File Offset: 0x000CBC34
	[RPC]
	[global::NGCRPCSkip]
	public void ReceiveBoundBPs(byte[] data, uLink.NetworkMessageInfo info)
	{
		this._boundBPs = (this._boundBPs ?? new List<global::BlueprintDataBlock>());
		this._boundBPs.Clear();
		BitStream bitStream = new BitStream(data, false);
		int num = bitStream.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			int uniqueID = bitStream.ReadInt32();
			global::ItemDataBlock byUniqueID = global::DatablockDictionary.GetByUniqueID(uniqueID);
			if (byUniqueID)
			{
				global::BlueprintDataBlock item = byUniqueID as global::BlueprintDataBlock;
				this._boundBPs.Add(item);
			}
		}
		this.Refresh();
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x000CDABC File Offset: 0x000CBCBC
	public List<global::BlueprintDataBlock> GetBoundBPs()
	{
		return this._boundBPs;
	}

	// Token: 0x06003A98 RID: 15000 RVA: 0x000CDAC4 File Offset: 0x000CBCC4
	protected override bool CheckSlotFlags(global::Inventory.SlotFlags itemSlotFlags, global::Inventory.SlotFlags slotFlags)
	{
		return base.CheckSlotFlags(itemSlotFlags, slotFlags) && ((slotFlags & global::Inventory.SlotFlags.Equip) != global::Inventory.SlotFlags.Equip || (itemSlotFlags & slotFlags) == slotFlags);
	}

	// Token: 0x06003A99 RID: 15001 RVA: 0x000CDAF4 File Offset: 0x000CBCF4
	public static bool IsEquipmentSlot(int slot)
	{
		return slot >= 36 && slot < 40;
	}

	// Token: 0x06003A9A RID: 15002 RVA: 0x000CDB08 File Offset: 0x000CBD08
	public static bool IsBeltSlot(int slot)
	{
		return slot >= 30 && slot < 36;
	}

	// Token: 0x06003A9B RID: 15003 RVA: 0x000CDB1C File Offset: 0x000CBD1C
	protected override void DoSetActiveItem(global::InventoryItem item)
	{
		global::InventoryItem activeItem = this._activeItem;
		this._activeItem = item;
		if (activeItem != null)
		{
			global::IHeldItem heldItem = activeItem.iface as global::IHeldItem;
			if (heldItem != null)
			{
				heldItem.OnDeactivate();
			}
		}
		if (this._activeItem != null)
		{
			global::IHeldItem heldItem2 = this._activeItem as global::IHeldItem;
			if (heldItem2 != null)
			{
				heldItem2.OnActivate();
			}
		}
	}

	// Token: 0x06003A9C RID: 15004 RVA: 0x000CDB78 File Offset: 0x000CBD78
	protected override void DoDeactivateItem()
	{
		if (this._activeItem != null)
		{
			global::IHeldItem heldItem = this._activeItem as global::IHeldItem;
			if (heldItem != null)
			{
				heldItem.OnDeactivate();
			}
		}
		this._activeItem = null;
		base.DoDeactivateItem();
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x000CDBB8 File Offset: 0x000CBDB8
	public override void Refresh()
	{
		global::InventoryHolder inventoryHolder = base.inventoryHolder;
		if (inventoryHolder)
		{
			inventoryHolder.InventoryModified();
		}
	}

	// Token: 0x17000B67 RID: 2919
	// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000CDBE0 File Offset: 0x000CBDE0
	private new global::EquipmentWearer equipmentWearer
	{
		get
		{
			return (!this._equipmentWearer) ? (this._equipmentWearer = base.GetLocal<global::EquipmentWearer>()) : this._equipmentWearer;
		}
	}

	// Token: 0x06003A9F RID: 15007 RVA: 0x000CDC18 File Offset: 0x000CBE18
	private void UpdateEquipment()
	{
		global::EquipmentWearer equipmentWearer = this.equipmentWearer;
		if (equipmentWearer)
		{
			equipmentWearer.EquipmentUpdate();
		}
	}

	// Token: 0x06003AA0 RID: 15008 RVA: 0x000CDC40 File Offset: 0x000CBE40
	protected override void ItemRemoved(int slot, global::IInventoryItem item)
	{
		if (global::PlayerInventory.IsEquipmentSlot(slot))
		{
			global::IEquipmentItem equipmentItem = item as global::IEquipmentItem;
			if (equipmentItem != null)
			{
				equipmentItem.OnUnEquipped();
				this.UpdateEquipment();
			}
		}
	}

	// Token: 0x06003AA1 RID: 15009 RVA: 0x000CDC74 File Offset: 0x000CBE74
	protected override void ItemAdded(int slot, global::IInventoryItem item)
	{
		if (global::PlayerInventory.IsEquipmentSlot(slot))
		{
			global::IEquipmentItem equipmentItem = item as global::IEquipmentItem;
			if (equipmentItem != null)
			{
				equipmentItem.OnEquipped();
				this.UpdateEquipment();
			}
		}
	}

	// Token: 0x17000B68 RID: 2920
	// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000CDCA8 File Offset: 0x000CBEA8
	public int fixedSlotCount
	{
		get
		{
			return 40;
		}
	}

	// Token: 0x06003AA3 RID: 15011 RVA: 0x000CDCAC File Offset: 0x000CBEAC
	public bool GetArmorItem<IArmorItem>(global::ArmorModelSlot slot, out IArmorItem item) where IArmorItem : class, global::IInventoryItem
	{
		int slot2;
		switch (slot)
		{
		case global::ArmorModelSlot.Feet:
			slot2 = 39;
			break;
		case global::ArmorModelSlot.Legs:
			slot2 = 38;
			break;
		case global::ArmorModelSlot.Torso:
			slot2 = 37;
			break;
		case global::ArmorModelSlot.Head:
			slot2 = 36;
			break;
		default:
			item = (IArmorItem)((object)null);
			return false;
		}
		global::IInventoryItem inventoryItem;
		if (base.GetItem(slot2, out inventoryItem))
		{
			return !object.ReferenceEquals(item = (inventoryItem as IArmorItem), null);
		}
		item = (IArmorItem)((object)null);
		return false;
	}

	// Token: 0x04001CC9 RID: 7369
	private const int _storageSpace = 30;

	// Token: 0x04001CCA RID: 7370
	private const int _beltSpace = 6;

	// Token: 0x04001CCB RID: 7371
	private const int _equipSpace = 4;

	// Token: 0x04001CCC RID: 7372
	public const int EquipmentStart = 36;

	// Token: 0x04001CCD RID: 7373
	public const int EquipmentEnd = 40;

	// Token: 0x04001CCE RID: 7374
	public const int NumEquipItems = 4;

	// Token: 0x04001CCF RID: 7375
	public const int BeltStart = 30;

	// Token: 0x04001CD0 RID: 7376
	public const int BeltEnd = 36;

	// Token: 0x04001CD1 RID: 7377
	public const int NumBeltItems = 6;

	// Token: 0x04001CD2 RID: 7378
	public const int StorageStart = 0;

	// Token: 0x04001CD3 RID: 7379
	public const int StorageEnd = 30;

	// Token: 0x04001CD4 RID: 7380
	public const int NumStorageItems = 30;

	// Token: 0x04001CD5 RID: 7381
	private const int TotalSlotCount = 40;

	// Token: 0x04001CD6 RID: 7382
	private List<global::BlueprintDataBlock> _boundBPs;

	// Token: 0x04001CD7 RID: 7383
	public bool bpDirty = true;

	// Token: 0x04001CD8 RID: 7384
	[NonSerialized]
	private global::EquipmentWearer _equipmentWearer;

	// Token: 0x020006BA RID: 1722
	private static class LateLoaded
	{
		// Token: 0x06003AA4 RID: 15012 RVA: 0x000CDD44 File Offset: 0x000CBF44
		static LateLoaded()
		{
			for (int i = 0; i < 40; i++)
			{
				global::Inventory.SlotFlags slotFlags = (global::Inventory.SlotFlags)0;
				if (global::PlayerInventory.IsBeltSlot(i))
				{
					slotFlags |= global::Inventory.SlotFlags.Belt;
				}
				if (i == 30)
				{
					slotFlags |= global::Inventory.SlotFlags.Safe;
				}
				if (global::PlayerInventory.IsEquipmentSlot(i))
				{
					slotFlags |= global::Inventory.SlotFlags.Equip;
					switch (i)
					{
					case 36:
						slotFlags |= global::Inventory.SlotFlags.Head;
						break;
					case 37:
						slotFlags |= global::Inventory.SlotFlags.Chest;
						break;
					case 38:
						slotFlags |= global::Inventory.SlotFlags.Legs;
						break;
					case 39:
						slotFlags |= global::Inventory.SlotFlags.Feet;
						break;
					}
				}
				global::PlayerInventory.LateLoaded.EveryPlayerInventory[i] = slotFlags;
			}
			global::PlayerInventory.LateLoaded.SlotRanges[global::Inventory.Slot.Kind.Default] = new global::Inventory.Slot.Range(0, 30);
			global::PlayerInventory.LateLoaded.SlotRanges[global::Inventory.Slot.Kind.Belt] = new global::Inventory.Slot.Range(30, 6);
			global::PlayerInventory.LateLoaded.SlotRanges[global::Inventory.Slot.Kind.Armor] = new global::Inventory.Slot.Range(36, 4);
		}

		// Token: 0x04001CD9 RID: 7385
		public static readonly global::Inventory.SlotFlags[] EveryPlayerInventory = new global::Inventory.SlotFlags[40];

		// Token: 0x04001CDA RID: 7386
		public static global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> SlotRanges;
	}
}
