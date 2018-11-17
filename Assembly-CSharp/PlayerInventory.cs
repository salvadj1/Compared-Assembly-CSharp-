using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
public class PlayerInventory : CraftingInventory, FixedSizeInventory
{
	// Token: 0x060036BA RID: 14010 RVA: 0x000C5468 File Offset: 0x000C3668
	protected void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			base.InitializeThisFixedSizeInventory();
		}
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x000C5484 File Offset: 0x000C3684
	public void MakeBPsDirty()
	{
		this.bpDirty = true;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x000C5490 File Offset: 0x000C3690
	protected override void ConfigureSlots(int totalCount, ref Inventory.Slot.KindDictionary<Inventory.Slot.Range> ranges, ref Inventory.SlotFlags[] flags)
	{
		if (totalCount != 40)
		{
			Debug.LogError("Invalid size for player inventory " + totalCount, this);
		}
		ranges = PlayerInventory.LateLoaded.SlotRanges;
		flags = PlayerInventory.LateLoaded.EveryPlayerInventory;
		if (base.networkView.isMine)
		{
			this._boundBPs = new List<BlueprintDataBlock>();
		}
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x000C54E8 File Offset: 0x000C36E8
	public bool KnowsBP(BlueprintDataBlock bp)
	{
		return bp && this._boundBPs.Contains(bp);
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x000C5504 File Offset: 0x000C3704
	[NGCRPCSkip]
	[RPC]
	public void ReceiveBoundBPs(byte[] data, NetworkMessageInfo info)
	{
		this._boundBPs = (this._boundBPs ?? new List<BlueprintDataBlock>());
		this._boundBPs.Clear();
		BitStream bitStream = new BitStream(data, false);
		int num = bitStream.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			int uniqueID = bitStream.ReadInt32();
			ItemDataBlock byUniqueID = DatablockDictionary.GetByUniqueID(uniqueID);
			if (byUniqueID)
			{
				BlueprintDataBlock item = byUniqueID as BlueprintDataBlock;
				this._boundBPs.Add(item);
			}
		}
		this.Refresh();
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x000C558C File Offset: 0x000C378C
	public List<BlueprintDataBlock> GetBoundBPs()
	{
		return this._boundBPs;
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x000C5594 File Offset: 0x000C3794
	protected override bool CheckSlotFlags(Inventory.SlotFlags itemSlotFlags, Inventory.SlotFlags slotFlags)
	{
		return base.CheckSlotFlags(itemSlotFlags, slotFlags) && ((slotFlags & Inventory.SlotFlags.Equip) != Inventory.SlotFlags.Equip || (itemSlotFlags & slotFlags) == slotFlags);
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x000C55C4 File Offset: 0x000C37C4
	public static bool IsEquipmentSlot(int slot)
	{
		return slot >= 36 && slot < 40;
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x000C55D8 File Offset: 0x000C37D8
	public static bool IsBeltSlot(int slot)
	{
		return slot >= 30 && slot < 36;
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x000C55EC File Offset: 0x000C37EC
	protected override void DoSetActiveItem(InventoryItem item)
	{
		InventoryItem activeItem = this._activeItem;
		this._activeItem = item;
		if (activeItem != null)
		{
			IHeldItem heldItem = activeItem.iface as IHeldItem;
			if (heldItem != null)
			{
				heldItem.OnDeactivate();
			}
		}
		if (this._activeItem != null)
		{
			IHeldItem heldItem2 = this._activeItem as IHeldItem;
			if (heldItem2 != null)
			{
				heldItem2.OnActivate();
			}
		}
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x000C5648 File Offset: 0x000C3848
	protected override void DoDeactivateItem()
	{
		if (this._activeItem != null)
		{
			IHeldItem heldItem = this._activeItem as IHeldItem;
			if (heldItem != null)
			{
				heldItem.OnDeactivate();
			}
		}
		this._activeItem = null;
		base.DoDeactivateItem();
	}

	// Token: 0x060036C5 RID: 14021 RVA: 0x000C5688 File Offset: 0x000C3888
	public override void Refresh()
	{
		InventoryHolder inventoryHolder = base.inventoryHolder;
		if (inventoryHolder)
		{
			inventoryHolder.InventoryModified();
		}
	}

	// Token: 0x17000AED RID: 2797
	// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000C56B0 File Offset: 0x000C38B0
	private new EquipmentWearer equipmentWearer
	{
		get
		{
			return (!this._equipmentWearer) ? (this._equipmentWearer = base.GetLocal<EquipmentWearer>()) : this._equipmentWearer;
		}
	}

	// Token: 0x060036C7 RID: 14023 RVA: 0x000C56E8 File Offset: 0x000C38E8
	private void UpdateEquipment()
	{
		EquipmentWearer equipmentWearer = this.equipmentWearer;
		if (equipmentWearer)
		{
			equipmentWearer.EquipmentUpdate();
		}
	}

	// Token: 0x060036C8 RID: 14024 RVA: 0x000C5710 File Offset: 0x000C3910
	protected override void ItemRemoved(int slot, IInventoryItem item)
	{
		if (PlayerInventory.IsEquipmentSlot(slot))
		{
			IEquipmentItem equipmentItem = item as IEquipmentItem;
			if (equipmentItem != null)
			{
				equipmentItem.OnUnEquipped();
				this.UpdateEquipment();
			}
		}
	}

	// Token: 0x060036C9 RID: 14025 RVA: 0x000C5744 File Offset: 0x000C3944
	protected override void ItemAdded(int slot, IInventoryItem item)
	{
		if (PlayerInventory.IsEquipmentSlot(slot))
		{
			IEquipmentItem equipmentItem = item as IEquipmentItem;
			if (equipmentItem != null)
			{
				equipmentItem.OnEquipped();
				this.UpdateEquipment();
			}
		}
	}

	// Token: 0x17000AEE RID: 2798
	// (get) Token: 0x060036CA RID: 14026 RVA: 0x000C5778 File Offset: 0x000C3978
	public int fixedSlotCount
	{
		get
		{
			return 40;
		}
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x000C577C File Offset: 0x000C397C
	public bool GetArmorItem<IArmorItem>(ArmorModelSlot slot, out IArmorItem item) where IArmorItem : class, IInventoryItem
	{
		int slot2;
		switch (slot)
		{
		case ArmorModelSlot.Feet:
			slot2 = 39;
			break;
		case ArmorModelSlot.Legs:
			slot2 = 38;
			break;
		case ArmorModelSlot.Torso:
			slot2 = 37;
			break;
		case ArmorModelSlot.Head:
			slot2 = 36;
			break;
		default:
			item = (IArmorItem)((object)null);
			return false;
		}
		IInventoryItem inventoryItem;
		if (base.GetItem(slot2, out inventoryItem))
		{
			return !object.ReferenceEquals(item = (inventoryItem as IArmorItem), null);
		}
		item = (IArmorItem)((object)null);
		return false;
	}

	// Token: 0x04001AE3 RID: 6883
	private const int _storageSpace = 30;

	// Token: 0x04001AE4 RID: 6884
	private const int _beltSpace = 6;

	// Token: 0x04001AE5 RID: 6885
	private const int _equipSpace = 4;

	// Token: 0x04001AE6 RID: 6886
	public const int EquipmentStart = 36;

	// Token: 0x04001AE7 RID: 6887
	public const int EquipmentEnd = 40;

	// Token: 0x04001AE8 RID: 6888
	public const int NumEquipItems = 4;

	// Token: 0x04001AE9 RID: 6889
	public const int BeltStart = 30;

	// Token: 0x04001AEA RID: 6890
	public const int BeltEnd = 36;

	// Token: 0x04001AEB RID: 6891
	public const int NumBeltItems = 6;

	// Token: 0x04001AEC RID: 6892
	public const int StorageStart = 0;

	// Token: 0x04001AED RID: 6893
	public const int StorageEnd = 30;

	// Token: 0x04001AEE RID: 6894
	public const int NumStorageItems = 30;

	// Token: 0x04001AEF RID: 6895
	private const int TotalSlotCount = 40;

	// Token: 0x04001AF0 RID: 6896
	private List<BlueprintDataBlock> _boundBPs;

	// Token: 0x04001AF1 RID: 6897
	public bool bpDirty = true;

	// Token: 0x04001AF2 RID: 6898
	[NonSerialized]
	private EquipmentWearer _equipmentWearer;

	// Token: 0x020005FA RID: 1530
	private static class LateLoaded
	{
		// Token: 0x060036CC RID: 14028 RVA: 0x000C5814 File Offset: 0x000C3A14
		static LateLoaded()
		{
			for (int i = 0; i < 40; i++)
			{
				Inventory.SlotFlags slotFlags = (Inventory.SlotFlags)0;
				if (PlayerInventory.IsBeltSlot(i))
				{
					slotFlags |= Inventory.SlotFlags.Belt;
				}
				if (i == 30)
				{
					slotFlags |= Inventory.SlotFlags.Safe;
				}
				if (PlayerInventory.IsEquipmentSlot(i))
				{
					slotFlags |= Inventory.SlotFlags.Equip;
					switch (i)
					{
					case 36:
						slotFlags |= Inventory.SlotFlags.Head;
						break;
					case 37:
						slotFlags |= Inventory.SlotFlags.Chest;
						break;
					case 38:
						slotFlags |= Inventory.SlotFlags.Legs;
						break;
					case 39:
						slotFlags |= Inventory.SlotFlags.Feet;
						break;
					}
				}
				PlayerInventory.LateLoaded.EveryPlayerInventory[i] = slotFlags;
			}
			PlayerInventory.LateLoaded.SlotRanges[Inventory.Slot.Kind.Default] = new Inventory.Slot.Range(0, 30);
			PlayerInventory.LateLoaded.SlotRanges[Inventory.Slot.Kind.Belt] = new Inventory.Slot.Range(30, 6);
			PlayerInventory.LateLoaded.SlotRanges[Inventory.Slot.Kind.Armor] = new Inventory.Slot.Range(36, 4);
		}

		// Token: 0x04001AF3 RID: 6899
		public static readonly Inventory.SlotFlags[] EveryPlayerInventory = new Inventory.SlotFlags[40];

		// Token: 0x04001AF4 RID: 6900
		public static Inventory.Slot.KindDictionary<Inventory.Slot.Range> SlotRanges;
	}
}
