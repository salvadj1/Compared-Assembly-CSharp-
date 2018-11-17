using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020005D8 RID: 1496
[global::NGCAutoAddScript]
public class Inventory : IDLocal
{
	// Token: 0x17000A29 RID: 2601
	// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000B7C64 File Offset: 0x000B5E64
	public bool isCraftingInventory
	{
		get
		{
			return this is global::CraftingInventory;
		}
	}

	// Token: 0x17000A2A RID: 2602
	// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000B7C70 File Offset: 0x000B5E70
	public float craftingSpeed
	{
		get
		{
			global::CraftingInventory craftingInventory = this as global::CraftingInventory;
			if (craftingInventory == null)
			{
				return 0f;
			}
			return craftingInventory.craftingSpeedPerSec;
		}
	}

	// Token: 0x17000A2B RID: 2603
	// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000B7C9C File Offset: 0x000B5E9C
	public bool isCrafting
	{
		get
		{
			global::CraftingInventory craftingInventory = this as global::CraftingInventory;
			return craftingInventory && craftingInventory.isCrafting;
		}
	}

	// Token: 0x17000A2C RID: 2604
	// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000B7CC4 File Offset: 0x000B5EC4
	public float? craftingCompletePercent
	{
		get
		{
			global::CraftingInventory craftingInventory = this as global::CraftingInventory;
			if (craftingInventory)
			{
				return craftingInventory.craftingCompletePercent;
			}
			return null;
		}
	}

	// Token: 0x17000A2D RID: 2605
	// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000B7CF4 File Offset: 0x000B5EF4
	public float? craftingSecondsRemaining
	{
		get
		{
			global::CraftingInventory craftingInventory = this as global::CraftingInventory;
			if (craftingInventory)
			{
				return craftingInventory.craftingSecondsRemaining;
			}
			return null;
		}
	}

	// Token: 0x17000A2E RID: 2606
	// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000B7D24 File Offset: 0x000B5F24
	public int slotCount
	{
		get
		{
			return this.collection.Capacity;
		}
	}

	// Token: 0x17000A2F RID: 2607
	// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000B7D34 File Offset: 0x000B5F34
	public int occupiedSlotCount
	{
		get
		{
			return this.collection.OccupiedCount;
		}
	}

	// Token: 0x17000A30 RID: 2608
	// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000B7D44 File Offset: 0x000B5F44
	public int vacantSlotCount
	{
		get
		{
			return this.collection.VacantCount;
		}
	}

	// Token: 0x17000A31 RID: 2609
	// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000B7D54 File Offset: 0x000B5F54
	public int dirtySlotCount
	{
		get
		{
			return this.collection.DirtyCount;
		}
	}

	// Token: 0x17000A32 RID: 2610
	// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000B7D64 File Offset: 0x000B5F64
	public global::IInventoryItem firstItem
	{
		get
		{
			global::InventoryItem inventoryItem;
			if (this.collection.GetByOrder(0, out inventoryItem))
			{
				return inventoryItem.iface;
			}
			return null;
		}
	}

	// Token: 0x17000A33 RID: 2611
	// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000B7D8C File Offset: 0x000B5F8C
	public global::Inventory.OccupiedIterator occupiedIterator
	{
		get
		{
			return new global::Inventory.OccupiedIterator(this);
		}
	}

	// Token: 0x17000A34 RID: 2612
	// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000B7D94 File Offset: 0x000B5F94
	public global::Inventory.OccupiedReverseIterator occupiedReverseIterator
	{
		get
		{
			return new global::Inventory.OccupiedReverseIterator(this);
		}
	}

	// Token: 0x17000A35 RID: 2613
	// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000B7D9C File Offset: 0x000B5F9C
	public global::Inventory.VacantIterator vacantIterator
	{
		get
		{
			return new global::Inventory.VacantIterator(this);
		}
	}

	// Token: 0x17000A36 RID: 2614
	// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000B7DA4 File Offset: 0x000B5FA4
	public bool noVacantSlots
	{
		get
		{
			return this.collection.HasNoVacancy;
		}
	}

	// Token: 0x17000A37 RID: 2615
	// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000B7DB4 File Offset: 0x000B5FB4
	public bool noOccupiedSlots
	{
		get
		{
			return this.collection.HasNoOccupant;
		}
	}

	// Token: 0x17000A38 RID: 2616
	// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000B7DC4 File Offset: 0x000B5FC4
	public bool anyVacantSlots
	{
		get
		{
			return this.collection.HasVacancy;
		}
	}

	// Token: 0x17000A39 RID: 2617
	// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000B7DD4 File Offset: 0x000B5FD4
	public bool anyOccupiedSlots
	{
		get
		{
			return this.collection.HasAnyOccupant;
		}
	}

	// Token: 0x17000A3A RID: 2618
	// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000B7DE4 File Offset: 0x000B5FE4
	public bool initialized
	{
		get
		{
			return this._collection_made_;
		}
	}

	// Token: 0x17000A3B RID: 2619
	// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000B7DEC File Offset: 0x000B5FEC
	// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x000B7DF4 File Offset: 0x000B5FF4
	public bool locked
	{
		get
		{
			return this._locked;
		}
		set
		{
			this._locked = value;
		}
	}

	// Token: 0x17000A3C RID: 2620
	// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000B7E00 File Offset: 0x000B6000
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

	// Token: 0x17000A3D RID: 2621
	// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000B7E3C File Offset: 0x000B603C
	public global::EquipmentWearer equipmentWearer
	{
		get
		{
			if (!this._equipmentWearer.cached)
			{
				this._equipmentWearer = base.GetLocal<global::EquipmentWearer>();
			}
			return this._equipmentWearer.value;
		}
	}

	// Token: 0x17000A3E RID: 2622
	// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000B7E78 File Offset: 0x000B6078
	protected global::InventoryItem firstInventoryItem
	{
		get
		{
			global::InventoryItem result;
			if (this.collection.GetByOrder(0, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x17000A3F RID: 2623
	// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000B7E9C File Offset: 0x000B609C
	protected global::HumanController hackyNeedToFixHumanControllGetValue
	{
		get
		{
			global::Character character = this.idMain as global::Character;
			return (!character) ? null : (character.controller as global::HumanController);
		}
	}

	// Token: 0x06002FD6 RID: 12246 RVA: 0x000B7ED4 File Offset: 0x000B60D4
	private void Initialize(int slotCount)
	{
		if (this._collection_made_)
		{
			this.Clear();
			this._collection_ = null;
			this._collection_made_ = false;
		}
		this._slotFlags = global::Inventory.Empty.SlotFlags;
		this._collection_ = new global::Inventory.Collection<global::InventoryItem>(slotCount);
		this._collection_made_ = true;
		this.slotRanges = default(global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range>);
		this.slotRanges[global::Inventory.Slot.Kind.Default] = new global::Inventory.Slot.Range(0, slotCount);
		this.ConfigureSlots(slotCount, ref this.slotRanges, ref this._slotFlags);
		this._collection_.MarkCompletelyDirty();
	}

	// Token: 0x06002FD7 RID: 12247 RVA: 0x000B7F60 File Offset: 0x000B6160
	protected bool InitializeThisFixedSizeInventory()
	{
		global::FixedSizeInventory fixedSizeInventory = this as global::FixedSizeInventory;
		if (object.ReferenceEquals(fixedSizeInventory, null))
		{
			return false;
		}
		int fixedSlotCount = fixedSizeInventory.fixedSlotCount;
		if (this._collection_made_)
		{
			if (this._collection_.Capacity == fixedSlotCount)
			{
				return false;
			}
			Debug.LogError("Some how this inventory was already inititalized to a different size. It will be reinitialized. the original off size was " + this._collection_.Capacity, this);
		}
		this.Initialize(fixedSlotCount);
		return true;
	}

	// Token: 0x06002FD8 RID: 12248 RVA: 0x000B7FD4 File Offset: 0x000B61D4
	public static byte RPCInteger(int i)
	{
		return (byte)i;
	}

	// Token: 0x06002FD9 RID: 12249 RVA: 0x000B7FD8 File Offset: 0x000B61D8
	public static byte RPCInteger(byte i)
	{
		return i;
	}

	// Token: 0x06002FDA RID: 12250 RVA: 0x000B7FDC File Offset: 0x000B61DC
	public static byte RPCInteger(BitStream stream)
	{
		return stream.Read<byte>(new object[0]);
	}

	// Token: 0x06002FDB RID: 12251 RVA: 0x000B7FEC File Offset: 0x000B61EC
	public global::Inventory.AddExistingItemResult AddExistingItem(global::IInventoryItem iitem, bool forbidStacking)
	{
		return this.AddExistingItem(iitem, forbidStacking, false);
	}

	// Token: 0x06002FDC RID: 12252 RVA: 0x000B7FF8 File Offset: 0x000B61F8
	public global::IInventoryItem AddItem(global::ItemDataBlock datablock, global::Inventory.Slot.Preference slot, global::Inventory.Uses.Quantity uses)
	{
		global::Datablock.Ident ident = (global::Datablock.Ident)datablock;
		return this.AddItem(ref ident, slot, uses);
	}

	// Token: 0x06002FDD RID: 12253 RVA: 0x000B8018 File Offset: 0x000B6218
	public global::IInventoryItem AddItem(ref global::Datablock.Ident ident, global::Inventory.Slot.Preference slot, global::Inventory.Uses.Quantity uses)
	{
		global::Inventory.Addition addition = default(global::Inventory.Addition);
		global::Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = (global::ItemDataBlock)ident.datablock;
		addition2.SlotPreference = slot;
		addition2.UsesQuantity = uses;
		addition = addition2;
		return this.AddItem(ref addition);
	}

	// Token: 0x06002FDE RID: 12254 RVA: 0x000B805C File Offset: 0x000B625C
	public global::IInventoryItem AddItem(ref global::Inventory.Addition itemAdd)
	{
		return this.AddItem(ref itemAdd, (global::Inventory.Payload.Opt)0, null);
	}

	// Token: 0x06002FDF RID: 12255 RVA: 0x000B8068 File Offset: 0x000B6268
	public void AddItems(global::Inventory.Addition[] itemAdds)
	{
		for (int i = 0; i < itemAdds.Length; i++)
		{
			this.AddItem(ref itemAdds[i]);
		}
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x000B8098 File Offset: 0x000B6298
	public global::IInventoryItem AddItemSomehow(global::ItemDataBlock item, global::Inventory.Slot.Kind? slotKindPref, int slotOffset, int usesCount)
	{
		global::IInventoryItem result;
		if (item && (usesCount > 0 || !item.IsSplittable()))
		{
			global::IInventoryItem inventoryItem = this.AddItemSomehowWork(item, slotKindPref, slotOffset, usesCount);
			result = inventoryItem;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06002FE1 RID: 12257 RVA: 0x000B80D8 File Offset: 0x000B62D8
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.AmountMode mode, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount(datablock, amount, mode, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002FE2 RID: 12258 RVA: 0x000B8100 File Offset: 0x000B6300
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.AmountMode mode, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, mode, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x000B8130 File Offset: 0x000B6330
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.AmountMode mode)
	{
		return this.AddItemAmount(datablock, amount, mode, null, null);
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x000B8158 File Offset: 0x000B6358
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.AmountMode mode)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, mode, null, null);
	}

	// Token: 0x06002FE5 RID: 12261 RVA: 0x000B818C File Offset: 0x000B638C
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount(datablock, amount, global::Inventory.AmountMode.Default, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002FE6 RID: 12262 RVA: 0x000B81B4 File Offset: 0x000B63B4
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, global::Inventory.AmountMode.Default, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002FE7 RID: 12263 RVA: 0x000B81E4 File Offset: 0x000B63E4
	public int AddItemAmount(global::ItemDataBlock datablock, int amount)
	{
		return this.AddItemAmount(datablock, amount, global::Inventory.AmountMode.Default, null, null);
	}

	// Token: 0x06002FE8 RID: 12264 RVA: 0x000B820C File Offset: 0x000B640C
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, global::Inventory.AmountMode.Default, null, null);
	}

	// Token: 0x06002FE9 RID: 12265 RVA: 0x000B8240 File Offset: 0x000B6440
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.AmountMode mode, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, mode, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FEA RID: 12266 RVA: 0x000B825C File Offset: 0x000B645C
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.AmountMode mode, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, mode, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FEB RID: 12267 RVA: 0x000B828C File Offset: 0x000B648C
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.AmountMode mode, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, mode, null, new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FEC RID: 12268 RVA: 0x000B82B4 File Offset: 0x000B64B4
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.AmountMode mode, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, mode, null, new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FED RID: 12269 RVA: 0x000B82E4 File Offset: 0x000B64E4
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, global::Inventory.AmountMode.Default, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x000B82FC File Offset: 0x000B64FC
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.Uses.Quantity perNonSplittableItemUseQuantity, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, global::Inventory.AmountMode.Default, new global::Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FEF RID: 12271 RVA: 0x000B832C File Offset: 0x000B652C
	public int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, global::Inventory.AmountMode.Default, null, new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x000B8354 File Offset: 0x000B6554
	public int AddItemAmount(ref global::Datablock.Ident ident, int amount, global::Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((global::ItemDataBlock)ident.datablock, amount, global::Inventory.AmountMode.Default, null, new global::Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002FF1 RID: 12273 RVA: 0x000B8384 File Offset: 0x000B6584
	public bool RemoveItem(int slot)
	{
		return this.RemoveItem(slot, null, false);
	}

	// Token: 0x06002FF2 RID: 12274 RVA: 0x000B8390 File Offset: 0x000B6590
	public bool RemoveItem(global::InventoryItem item)
	{
		return !object.ReferenceEquals(item, null) && !(item.inventory != this) && this.RemoveItem(item.slot, item, true);
	}

	// Token: 0x06002FF3 RID: 12275 RVA: 0x000B83CC File Offset: 0x000B65CC
	public bool RemoveItem(global::IInventoryItem item)
	{
		return this.RemoveItem(item as global::InventoryItem);
	}

	// Token: 0x06002FF4 RID: 12276 RVA: 0x000B83DC File Offset: 0x000B65DC
	[Obsolete("This isnt right")]
	public void NULL_SLOT_FIX_ME(int slot)
	{
		this.DeleteItem(slot);
	}

	// Token: 0x06002FF5 RID: 12277 RVA: 0x000B83E8 File Offset: 0x000B65E8
	public void Clear()
	{
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.ReverseEnumerator occupiedReverseEnumerator = this.collection.OccupiedReverseEnumerator)
		{
			while (occupiedReverseEnumerator.MoveNext())
			{
				this.DeleteItem(occupiedReverseEnumerator.Slot);
			}
		}
	}

	// Token: 0x06002FF6 RID: 12278 RVA: 0x000B844C File Offset: 0x000B664C
	public bool IsSlotDirty(int slot)
	{
		return this.collection.IsDirty(slot);
	}

	// Token: 0x06002FF7 RID: 12279 RVA: 0x000B845C File Offset: 0x000B665C
	public bool MarkSlotDirty(int slot)
	{
		return this.collection.MarkDirty(slot);
	}

	// Token: 0x06002FF8 RID: 12280 RVA: 0x000B846C File Offset: 0x000B666C
	public bool MarkSlotClean(int slot)
	{
		return this.collection.MarkClean(slot);
	}

	// Token: 0x06002FF9 RID: 12281 RVA: 0x000B847C File Offset: 0x000B667C
	public bool IsSlotVacant(int slot)
	{
		return this.collection.IsVacant(slot);
	}

	// Token: 0x06002FFA RID: 12282 RVA: 0x000B848C File Offset: 0x000B668C
	public bool IsSlotOccupied(int slot)
	{
		return this.collection.IsOccupied(slot);
	}

	// Token: 0x06002FFB RID: 12283 RVA: 0x000B849C File Offset: 0x000B669C
	public bool IsSlotWithinRange(int slot)
	{
		return this.collection.IsWithinRange(slot);
	}

	// Token: 0x06002FFC RID: 12284 RVA: 0x000B84AC File Offset: 0x000B66AC
	public int CanConsume(global::ItemDataBlock db, int useCount, List<int> storeToList)
	{
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		if (useCount <= 0 || !db || collection.HasNoOccupant)
		{
			return 0;
		}
		if (storeToList == null)
		{
			return this.CanConsume(db, useCount);
		}
		int count = storeToList.Count;
		int num = 0;
		int uniqueID = db.uniqueID;
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = occupiedEnumerator.Current;
				if (inventoryItem.datablockUniqueID == uniqueID)
				{
					useCount -= inventoryItem.uses;
					storeToList.Add(occupiedEnumerator.Slot);
					num++;
					if (useCount <= 0)
					{
						return num;
					}
				}
			}
		}
		if (num > 0)
		{
			storeToList.RemoveRange(count, num);
		}
		return -useCount;
	}

	// Token: 0x06002FFD RID: 12285 RVA: 0x000B859C File Offset: 0x000B679C
	public int CanConsume(global::ItemDataBlock db, int useCount)
	{
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		if (useCount <= 0 || collection.HasNoOccupant)
		{
			return 0;
		}
		int num = 0;
		int uniqueID = db.uniqueID;
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = occupiedEnumerator.Current;
				if (inventoryItem.datablockUniqueID == uniqueID)
				{
					useCount -= inventoryItem.uses;
					num++;
					if (useCount <= 0)
					{
						return num;
					}
				}
			}
		}
		return -useCount;
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x000B8650 File Offset: 0x000B6850
	public bool GetItem(int slot, out global::IInventoryItem item)
	{
		global::InventoryItem inventoryItem;
		if (!this._collection_made_ || !this._collection_.Get(slot, out inventoryItem))
		{
			item = null;
			return false;
		}
		item = inventoryItem.iface;
		return true;
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x000B868C File Offset: 0x000B688C
	protected bool GetItem(int slot, out global::InventoryItem item)
	{
		if (!this._collection_made_)
		{
			item = null;
			return false;
		}
		return this._collection_.Get(slot, out item);
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x000B86AC File Offset: 0x000B68AC
	public bool GetSlotsOfKind(global::Inventory.Slot.Kind kind, out global::Inventory.Slot.Range range)
	{
		return this.slotRanges.TryGetValue(kind, out range);
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x000B86BC File Offset: 0x000B68BC
	public bool HasSlotsOfKind(global::Inventory.Slot.Kind kind)
	{
		return this.slotRanges.ContainsKey(kind);
	}

	// Token: 0x06003002 RID: 12290 RVA: 0x000B86CC File Offset: 0x000B68CC
	public bool IsSlotFree(int slot)
	{
		return this.collection.IsVacant(slot);
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x000B86DC File Offset: 0x000B68DC
	public global::Inventory.SlotFlags GetSlotFlags(int slot)
	{
		return (this._slotFlags != null && this._slotFlags.Length > slot) ? this._slotFlags[slot] : ((global::Inventory.SlotFlags)0);
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x000B8708 File Offset: 0x000B6908
	public bool GetSlotKind(int slot, out global::Inventory.Slot.Kind kind, out int offset)
	{
		if (slot >= 0 && slot < this.slotCount)
		{
			for (global::Inventory.Slot.Kind kind2 = global::Inventory.Slot.Kind.Default; kind2 < (global::Inventory.Slot.Kind)3; kind2 += 1)
			{
				global::Inventory.Slot.Range range;
				if (this.slotRanges.TryGetValue(kind2, out range))
				{
					offset = range.GetOffset(slot);
					if (offset != -1)
					{
						kind = kind2;
						return true;
					}
				}
			}
		}
		kind = global::Inventory.Slot.Kind.Default;
		offset = -1;
		return false;
	}

	// Token: 0x06003005 RID: 12293 RVA: 0x000B876C File Offset: 0x000B696C
	public bool GetSlotForKind(global::Inventory.Slot.Kind kind, int offset, out int slot)
	{
		global::Inventory.Slot.Range range;
		if (offset >= 0 && this.slotRanges.TryGetValue(kind, out range) && offset < range.Count)
		{
			slot = range.Start + offset;
			return true;
		}
		slot = -1;
		return false;
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x000B87B4 File Offset: 0x000B69B4
	public bool IsSlotOffsetValid(global::Inventory.Slot.Kind kind, int offset)
	{
		int num;
		return this.GetSlotForKind(kind, offset, out num);
	}

	// Token: 0x06003007 RID: 12295 RVA: 0x000B87CC File Offset: 0x000B69CC
	public bool CanItemFit(global::IInventoryItem iitem)
	{
		global::InventoryItem inventoryItem = iitem as global::InventoryItem;
		global::ItemDataBlock datablock = inventoryItem.datablock;
		if (datablock.IsSplittable())
		{
			int num = inventoryItem.uses;
			using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
			{
				while (occupiedEnumerator.MoveNext())
				{
					global::InventoryItem inventoryItem2 = occupiedEnumerator.Current;
					if (inventoryItem2.datablockUniqueID == inventoryItem.datablockUniqueID)
					{
						if (inventoryItem2 != iitem)
						{
							int num2 = datablock._maxUses - inventoryItem2.uses;
							if (num2 >= num)
							{
								return true;
							}
							num -= num2;
						}
					}
				}
			}
			return false;
		}
		return this.anyVacantSlots;
	}

	// Token: 0x06003008 RID: 12296 RVA: 0x000B889C File Offset: 0x000B6A9C
	private bool CheckSlotFlagsAgainstSlot(global::Inventory.SlotFlags itemSlotFlags, int slot)
	{
		return this.CheckSlotFlags(itemSlotFlags, this.GetSlotFlags(slot));
	}

	// Token: 0x06003009 RID: 12297 RVA: 0x000B88AC File Offset: 0x000B6AAC
	public global::IngredientList<global::ItemDataBlock> ToIngredientList()
	{
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		int occupiedCount = collection.OccupiedCount;
		global::ItemDataBlock[] array = new global::ItemDataBlock[occupiedCount];
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
		{
			int newSize = 0;
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = occupiedEnumerator.Current;
				array[newSize++] = inventoryItem.datablock;
			}
			Array.Resize<global::ItemDataBlock>(ref array, newSize);
		}
		return new global::IngredientList<global::ItemDataBlock>(array);
	}

	// Token: 0x0600300A RID: 12298 RVA: 0x000B8940 File Offset: 0x000B6B40
	public bool MoveItemAtSlotToEmptySlot(global::Inventory toInv, int fromSlot, int toSlot)
	{
		if (!toInv)
		{
			return false;
		}
		if (toInv == this && fromSlot == toSlot)
		{
			return false;
		}
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		if (collection.HasNoOccupant)
		{
			return false;
		}
		global::InventoryItem inventoryItem;
		if (!collection.Get(fromSlot, out inventoryItem))
		{
			return false;
		}
		global::ItemDataBlock datablock = inventoryItem.datablock;
		global::Inventory.Addition addition = default(global::Inventory.Addition);
		global::Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = datablock;
		addition2.UsesQuantity = inventoryItem.uses;
		addition2.SlotPreference = global::Inventory.Slot.Preference.Define(toSlot, datablock.IsSplittable());
		addition = addition2;
		return !object.ReferenceEquals(toInv.AddItem(ref addition, global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.RestrictToOffset | global::Inventory.Payload.Opt.ReuseItem, inventoryItem), null);
	}

	// Token: 0x0600300B RID: 12299 RVA: 0x000B89EC File Offset: 0x000B6BEC
	public T FindItemType<T>() where T : class, global::IInventoryItem
	{
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = occupiedEnumerator.Current;
				T t = inventoryItem.iface as T;
				if (!object.ReferenceEquals(t, null))
				{
					return t;
				}
			}
		}
		return (T)((object)null);
	}

	// Token: 0x0600300C RID: 12300 RVA: 0x000B8A7C File Offset: 0x000B6C7C
	public IItemT FindItem<IItemT>() where IItemT : class, global::IInventoryItem
	{
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = occupiedEnumerator.Current;
				IItemT itemT = inventoryItem.iface as IItemT;
				if (!object.ReferenceEquals(itemT, null))
				{
					return itemT;
				}
			}
		}
		return (IItemT)((object)null);
	}

	// Token: 0x0600300D RID: 12301 RVA: 0x000B8B0C File Offset: 0x000B6D0C
	public IEnumerable<IItemT> FindItems<IItemT>() where IItemT : class, global::IInventoryItem
	{
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator enumerator = this.collection.OccupiedEnumerator)
		{
			while (enumerator.MoveNext())
			{
				global::InventoryItem inventoryItem = enumerator.Current;
				IItemT item = inventoryItem.iface as IItemT;
				if (!object.ReferenceEquals(item, null))
				{
					yield return item;
				}
			}
		}
		yield break;
	}

	// Token: 0x0600300E RID: 12302 RVA: 0x000B8B30 File Offset: 0x000B6D30
	public global::IInventoryItem FindItem(string itemDBName)
	{
		return this.FindItem(global::DatablockDictionary.GetByName(itemDBName));
	}

	// Token: 0x0600300F RID: 12303 RVA: 0x000B8B40 File Offset: 0x000B6D40
	public global::IInventoryItem FindItem(global::ItemDataBlock itemDB)
	{
		int num = 0;
		return this.FindItem(itemDB, out num);
	}

	// Token: 0x06003010 RID: 12304 RVA: 0x000B8B58 File Offset: 0x000B6D58
	public global::IInventoryItem FindItem(global::ItemDataBlock itemDB, out int totalNum)
	{
		bool flag = false;
		global::InventoryItem inventoryItem = null;
		int num = 0;
		int num2 = -1;
		int uniqueID = itemDB.uniqueID;
		using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				global::InventoryItem inventoryItem2 = occupiedEnumerator.Current;
				if (inventoryItem2.datablockUniqueID == uniqueID)
				{
					int uses = inventoryItem2.uses;
					if (!flag || uses > num2)
					{
						inventoryItem = inventoryItem2;
						num2 = uses;
						flag = true;
					}
					num += uses;
				}
			}
		}
		totalNum = num;
		global::IInventoryItem result;
		if (flag)
		{
			global::IInventoryItem iface = inventoryItem.iface;
			result = iface;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x17000A40 RID: 2624
	// (get) Token: 0x06003011 RID: 12305 RVA: 0x000B8C18 File Offset: 0x000B6E18
	public global::IInventoryItem activeItem
	{
		get
		{
			global::IInventoryItem result;
			if (this._activeItem == null)
			{
				global::IInventoryItem inventoryItem = null;
				result = inventoryItem;
			}
			else
			{
				result = this._activeItem.iface;
			}
			return result;
		}
	}

	// Token: 0x06003012 RID: 12306 RVA: 0x000B8C44 File Offset: 0x000B6E44
	public void SetActiveItemManually(int itemIndex, global::ItemRepresentation itemRep)
	{
		global::IInventoryItem inventoryItem;
		this.GetItem(itemIndex, out inventoryItem);
		((global::IHeldItem)inventoryItem).itemRepresentation = itemRep;
		this.DoSetActiveItem((global::InventoryItem)inventoryItem);
	}

	// Token: 0x06003013 RID: 12307 RVA: 0x000B8C74 File Offset: 0x000B6E74
	public void DeactivateItem()
	{
		this.DoDeactivateItem();
	}

	// Token: 0x06003014 RID: 12308 RVA: 0x000B8C7C File Offset: 0x000B6E7C
	public global::Inventory.Transfer[] GenerateOptimizedInventoryListing(global::Inventory.Slot.KindFlags fallbackPlacement)
	{
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		if (collection.HasNoOccupant)
		{
			return new global::Inventory.Transfer[0];
		}
		global::Inventory.Transfer[] result;
		try
		{
			global::Inventory.Report.Begin();
			using (global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
			{
				while (occupiedEnumerator.MoveNext())
				{
					global::InventoryItem item = occupiedEnumerator.Current;
					global::Inventory.Report.Take(item);
				}
			}
			result = global::Inventory.Report.Build(fallbackPlacement);
		}
		finally
		{
			global::Inventory.Report.Recover();
		}
		return result;
	}

	// Token: 0x06003015 RID: 12309 RVA: 0x000B8D28 File Offset: 0x000B6F28
	public global::Inventory.Transfer[] GenerateOptimizedInventoryListing(global::Inventory.Slot.KindFlags fallbackPlacement, bool randomize)
	{
		global::Inventory.Transfer[] array = this.GenerateOptimizedInventoryListing(fallbackPlacement);
		if (randomize && array.Length > 0)
		{
			global::Inventory.Shuffle.Array<global::Inventory.Transfer>(array);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].addition.SlotPreference = array[i].addition.SlotPreference.CloneOffsetChange(i);
			}
		}
		return array;
	}

	// Token: 0x06003016 RID: 12310 RVA: 0x000B8D90 File Offset: 0x000B6F90
	public void ResetToReport(global::Inventory.Transfer[] items)
	{
		if (this._collection_made_)
		{
			this.Clear();
		}
		this.Initialize(items.Length);
		for (int i = 0; i < items.Length; i++)
		{
			this.AssignItem(ref items[i].addition, global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.RestrictToOffset | global::Inventory.Payload.Opt.ReuseItem, items[i].item);
		}
	}

	// Token: 0x06003017 RID: 12311 RVA: 0x000B8DEC File Offset: 0x000B6FEC
	protected void BindArmorModelsFromArmorDatablockMap(global::ArmorModelMemberMap<global::ArmorDataBlock> armorDatablockMap)
	{
		this.lastNetworkedArmorDatablocks = armorDatablockMap;
		global::ArmorModelRenderer local = base.GetLocal<global::ArmorModelRenderer>();
		if (local)
		{
			global::ArmorModelMemberMap map = default(global::ArmorModelMemberMap);
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
			{
				global::ArmorDataBlock armorDataBlock = armorDatablockMap[armorModelSlot];
				map[armorModelSlot] = ((!armorDataBlock) ? null : armorDataBlock.GetArmorModel(armorModelSlot));
			}
			local.BindArmorModels(map);
		}
	}

	// Token: 0x06003018 RID: 12312 RVA: 0x000B8E60 File Offset: 0x000B7060
	protected void RequestCellUpdate(int cell)
	{
		global::NetCull.RPC<byte>(this, "SVUC", 0, global::Inventory.RPCInteger(cell));
	}

	// Token: 0x06003019 RID: 12313 RVA: 0x000B8E78 File Offset: 0x000B7078
	public void RequestFullUpdate()
	{
		global::NetCull.RPC(this, "SVUF", 0);
	}

	// Token: 0x0600301A RID: 12314 RVA: 0x000B8E88 File Offset: 0x000B7088
	private void OnNetSlotUpdate(global::Inventory.Collection<global::InventoryItem> _collection, int slot, bool occupied, BitStream invdata)
	{
		if (occupied)
		{
			int num = invdata.ReadInt32();
			global::InventoryItem inventoryItem;
			bool flag = _collection.Get(slot, out inventoryItem);
			if (flag && inventoryItem.datablockUniqueID != num)
			{
				this.DeleteItem(slot);
				flag = false;
				inventoryItem = null;
			}
			if (!flag)
			{
				global::Inventory.Addition addition = default(global::Inventory.Addition);
				global::Inventory.Addition addition2 = addition;
				addition2.UniqueID = num;
				addition2.UsesQuantity = global::Inventory.Uses.Quantity.Maximum;
				addition2.SlotPreference = global::Inventory.Slot.Preference.Define(slot, false);
				addition = addition2;
				inventoryItem = (this.AddItem(ref addition, global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.RestrictToOffset, null) as global::InventoryItem);
			}
			inventoryItem.Deserialize(invdata);
			if (flag)
			{
				_collection.MarkDirty(slot);
			}
		}
		else
		{
			this.DeleteItem(slot);
		}
	}

	// Token: 0x0600301B RID: 12315 RVA: 0x000B8F34 File Offset: 0x000B7134
	protected void OnNetUpdate(BitStream invdata)
	{
		int num = (int)invdata.ReadByte();
		global::Inventory.Collection<global::InventoryItem> collection_;
		if (this._collection_made_)
		{
			collection_ = this._collection_;
		}
		else
		{
			this.Initialize(num);
			collection_ = this._collection_;
		}
		int capacity = collection_.Capacity;
		if (num != capacity)
		{
			this.Initialize(num);
		}
		bool flag = invdata.ReadBoolean();
		if (flag)
		{
			int num2 = num;
			for (int i = 0; i < num2; i++)
			{
				bool occupied = invdata.ReadBoolean();
				this.OnNetSlotUpdate(collection_, i, occupied, invdata);
			}
		}
		else
		{
			int num2 = (int)invdata.ReadByte();
			int num3 = 0;
			try
			{
				for (int j = 0; j < num2; j++)
				{
					num3++;
					bool occupied2 = invdata.ReadBoolean();
					int slot = (int)invdata.ReadByte();
					this.OnNetSlotUpdate(collection_, slot, occupied2, invdata);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
				Debug.Log(string.Format("numItemsInUpdate = {0}, iterated pos = {1}", num2, num3), this);
			}
		}
	}

	// Token: 0x17000A41 RID: 2625
	// (get) Token: 0x0600301C RID: 12316 RVA: 0x000B9054 File Offset: 0x000B7254
	private global::Inventory.Collection<global::InventoryItem> collection
	{
		get
		{
			if (!this._collection_made_)
			{
				return global::Inventory.Collection<global::InventoryItem>.Default.Empty;
			}
			return this._collection_;
		}
	}

	// Token: 0x0600301D RID: 12317 RVA: 0x000B9070 File Offset: 0x000B7270
	private global::Inventory.Payload.Result AssignItem(ref global::Inventory.Addition addition, global::Inventory.Payload.Opt flags, global::InventoryItem reuse)
	{
		return global::Inventory.Payload.AddItem(this, ref addition, flags, reuse);
	}

	// Token: 0x0600301E RID: 12318 RVA: 0x000B9088 File Offset: 0x000B7288
	private static global::IInventoryItem ResultToItem(ref global::Inventory.Payload.Result result, global::Inventory.Payload.Opt flags)
	{
		if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.AssignedInstance) == 64)
		{
			return result.item.iface;
		}
		if ((byte)(flags & global::Inventory.Payload.Opt.AllowStackedItemsToBeReturned) != 32)
		{
			return null;
		}
		if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
		{
			return result.item.iface;
		}
		return null;
	}

	// Token: 0x0600301F RID: 12319 RVA: 0x000B90E0 File Offset: 0x000B72E0
	private global::IInventoryItem AddItem(ref global::Inventory.Addition addition, global::Inventory.Payload.Opt flags, global::InventoryItem reuse)
	{
		global::Inventory.Payload.Result result = this.AssignItem(ref addition, flags, reuse);
		return global::Inventory.ResultToItem(ref result, flags);
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x000B9100 File Offset: 0x000B7300
	private global::Inventory.AddExistingItemResult AddExistingItem(global::IInventoryItem iitem, bool forbidStacking, bool mustBeUnassigned)
	{
		global::InventoryItem inventoryItem = iitem as global::InventoryItem;
		if (object.ReferenceEquals(inventoryItem, null) || (mustBeUnassigned && inventoryItem.inventory))
		{
			return global::Inventory.AddExistingItemResult.BadItemArgument;
		}
		global::ItemDataBlock datablock = inventoryItem.datablock;
		global::Inventory.Addition addition = default(global::Inventory.Addition);
		global::Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = datablock;
		addition2.UsesQuantity = inventoryItem.uses;
		addition2.SlotPreference = global::Inventory.Slot.Preference.Define(global::Inventory.Slot.Kind.Default, !forbidStacking && datablock.IsSplittable(), global::Inventory.Slot.Kind.Belt);
		addition = addition2;
		global::Inventory.Payload.Opt opt = global::Inventory.Payload.Opt.IgnoreSlotOffset | global::Inventory.Payload.Opt.ReuseItem;
		if (forbidStacking)
		{
			opt |= global::Inventory.Payload.Opt.DoNotStack;
		}
		global::Inventory.Payload.Result result = this.AssignItem(ref addition, opt, inventoryItem);
		if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Complete) == 128)
		{
			if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.AssignedInstance) == 64)
			{
				return global::Inventory.AddExistingItemResult.Moved;
			}
			if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				inventoryItem.SetUses(0);
				return global::Inventory.AddExistingItemResult.CompletlyStacked;
			}
			Debug.LogWarning("unhandled", this);
			return global::Inventory.AddExistingItemResult.Failed;
		}
		else
		{
			if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				inventoryItem.SetUses(result.usesRemaining);
				return global::Inventory.AddExistingItemResult.PartiallyStacked;
			}
			return global::Inventory.AddExistingItemResult.Failed;
		}
	}

	// Token: 0x06003021 RID: 12321 RVA: 0x000B9214 File Offset: 0x000B7414
	private static global::Inventory.Slot.Preference DefaultAddMultipleItemsSlotPreference(bool stack)
	{
		return global::Inventory.Slot.Preference.Define(global::Inventory.Slot.Kind.Default, stack, global::Inventory.Slot.KindFlags.Belt);
	}

	// Token: 0x06003022 RID: 12322 RVA: 0x000B9220 File Offset: 0x000B7420
	private int AddMultipleItems(global::ItemDataBlock itemDB, int usesOrItemCountWhenNotSplittable, global::Inventory.Uses.Quantity nonSplittableUses, global::Inventory.AddMultipleItemFlags amif, global::Inventory.Slot.Preference? slotPreference)
	{
		global::Inventory.Addition addition = default(global::Inventory.Addition);
		global::Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = itemDB;
		addition = addition2;
		bool flag = itemDB.IsSplittable();
		if (((amif & (global::Inventory.AddMultipleItemFlags.MustBeSplittable | global::Inventory.AddMultipleItemFlags.MustBeNonSplittable)) | ((!flag) ? global::Inventory.AddMultipleItemFlags.MustBeNonSplittable : global::Inventory.AddMultipleItemFlags.MustBeSplittable)) == (global::Inventory.AddMultipleItemFlags.MustBeSplittable | global::Inventory.AddMultipleItemFlags.MustBeNonSplittable))
		{
			return usesOrItemCountWhenNotSplittable;
		}
		if (!flag)
		{
			addition.UsesQuantity = nonSplittableUses;
			addition.SlotPreference = ((slotPreference == null) ? global::Inventory.Slot.Preference.Define(global::Inventory.Slot.Kind.Default, false, global::Inventory.Slot.Kind.Belt) : slotPreference.Value.CloneStackChange(false));
			while (usesOrItemCountWhenNotSplittable > 0 && (byte)(this.AssignItem(ref addition, global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.IgnoreSlotOffset, null).flags & global::Inventory.Payload.Result.Flags.Complete) == 128)
			{
				usesOrItemCountWhenNotSplittable--;
			}
			return usesOrItemCountWhenNotSplittable;
		}
		if (usesOrItemCountWhenNotSplittable == 0)
		{
			return 0;
		}
		if ((amif & (global::Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks | global::Inventory.AddMultipleItemFlags.DoNotStackSplittables)) == (global::Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks | global::Inventory.AddMultipleItemFlags.DoNotStackSplittables))
		{
			return usesOrItemCountWhenNotSplittable;
		}
		int num = usesOrItemCountWhenNotSplittable / itemDB._maxUses;
		global::Inventory.Payload.Opt opt = global::Inventory.Payload.Opt.IgnoreSlotOffset;
		bool flag2;
		if ((amif & global::Inventory.AddMultipleItemFlags.DoNotStackSplittables) == global::Inventory.AddMultipleItemFlags.DoNotStackSplittables)
		{
			flag2 = true;
			opt |= global::Inventory.Payload.Opt.DoNotStack;
			if (slotPreference != null)
			{
				addition.SlotPreference = slotPreference.Value.CloneStackChange(false);
			}
			else
			{
				addition.SlotPreference = global::Inventory.DefaultAddMultipleItemsSlotPreference(false);
			}
		}
		else
		{
			flag2 = false;
			if (slotPreference != null)
			{
				addition.SlotPreference = slotPreference.Value;
			}
			else
			{
				addition.SlotPreference = global::Inventory.DefaultAddMultipleItemsSlotPreference(true);
			}
		}
		if ((amif & global::Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks) == global::Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks)
		{
			opt |= global::Inventory.Payload.Opt.DoNotAssign;
		}
		int num2 = 0;
		if (num > 0)
		{
			addition.UsesQuantity = itemDB._maxUses;
			global::Inventory.Payload.Result result;
			for (;;)
			{
				result = this.AssignItem(ref addition, opt, null);
				if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Complete) != 128)
				{
					break;
				}
				num2 += itemDB._maxUses;
				if (!flag2 && (byte)(result.flags & global::Inventory.Payload.Result.Flags.AssignedInstance) == 64)
				{
					opt |= global::Inventory.Payload.Opt.DoNotStack;
					flag2 = true;
				}
				if (--num <= 0)
				{
					goto IL_18F;
				}
			}
			if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				num2 += itemDB._maxUses - result.usesRemaining;
			}
			return usesOrItemCountWhenNotSplittable - num2;
		}
		IL_18F:
		if (num2 == usesOrItemCountWhenNotSplittable)
		{
			return 0;
		}
		int num3 = usesOrItemCountWhenNotSplittable - num2;
		addition.UsesQuantity = num3;
		global::Inventory.Payload.Result result2 = this.AssignItem(ref addition, opt, null);
		if ((byte)(result2.flags & (global::Inventory.Payload.Result.Flags.Complete | global::Inventory.Payload.Result.Flags.Stacked)) != 0)
		{
			num2 += num3 - result2.usesRemaining;
		}
		return usesOrItemCountWhenNotSplittable - num2;
	}

	// Token: 0x06003023 RID: 12323 RVA: 0x000B947C File Offset: 0x000B767C
	private int AddItemAmount(global::ItemDataBlock datablock, int amount, global::Inventory.AmountMode mode, global::Inventory.Uses.Quantity? perNonSplittableItemQuantity, global::Inventory.Slot.Preference? slotPref)
	{
		if (!datablock)
		{
			return amount;
		}
		global::Inventory.AddMultipleItemFlags addMultipleItemFlags;
		global::Inventory.Uses.Quantity nonSplittableUses;
		if (datablock.IsSplittable())
		{
			addMultipleItemFlags = global::Inventory.AddMultipleItemFlags.MustBeSplittable;
			switch (mode)
			{
			case global::Inventory.AmountMode.OnlyStack:
				addMultipleItemFlags |= global::Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks;
				break;
			case global::Inventory.AmountMode.OnlyCreateNew:
				addMultipleItemFlags |= global::Inventory.AddMultipleItemFlags.DoNotStackSplittables;
				break;
			case global::Inventory.AmountMode.IgnoreSplittables:
				return amount;
			}
			nonSplittableUses = default(global::Inventory.Uses.Quantity);
		}
		else
		{
			if (mode == global::Inventory.AmountMode.OnlyStack)
			{
				return amount;
			}
			addMultipleItemFlags = global::Inventory.AddMultipleItemFlags.MustBeNonSplittable;
			nonSplittableUses = ((perNonSplittableItemQuantity == null) ? global::Inventory.Uses.Quantity.Random : perNonSplittableItemQuantity.Value);
		}
		return this.AddMultipleItems(datablock, amount, nonSplittableUses, addMultipleItemFlags, slotPref);
	}

	// Token: 0x06003024 RID: 12324 RVA: 0x000B9520 File Offset: 0x000B7720
	private global::IInventoryItem AddItemSomehowWork(global::ItemDataBlock item, global::Inventory.Slot.Kind? slotKindPref, int slotOffset, int usesCount)
	{
		global::Inventory.Slot.Kind value;
		int num;
		bool flag;
		bool flag2;
		if (slotKindPref != null)
		{
			value = slotKindPref.Value;
			flag = this.GetSlotForKind(value, slotOffset, out num);
			flag2 = (flag || this.HasSlotsOfKind(value));
		}
		else
		{
			num = slotOffset;
			flag = (flag2 = this.GetSlotKind(num, out value, out slotOffset));
		}
		global::Inventory.Addition addition;
		addition.Ident = (global::Datablock.Ident)item;
		addition.UsesQuantity = usesCount;
		if (flag2)
		{
			if (flag)
			{
				addition.SlotPreference = global::Inventory.Slot.Preference.Define(value, slotOffset);
				global::Inventory.Payload.Result result = this.AssignItem(ref addition, global::Inventory.Payload.Opt.RestrictToOffset, null);
				if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Complete) == 128)
				{
					return global::Inventory.ResultToItem(ref result, global::Inventory.Payload.Opt.RestrictToOffset);
				}
				if ((byte)(result.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
				{
					addition.UsesQuantity = (usesCount = result.usesRemaining);
				}
			}
			addition.SlotPreference = value;
			global::Inventory.Payload.Result result2 = this.AssignItem(ref addition, (global::Inventory.Payload.Opt)0, null);
			if ((byte)(result2.flags & global::Inventory.Payload.Result.Flags.Complete) == 128)
			{
				return global::Inventory.ResultToItem(ref result2, (global::Inventory.Payload.Opt)0);
			}
			if ((byte)(result2.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				addition.UsesQuantity = (usesCount = result2.usesRemaining);
			}
		}
		else if (num >= 0 && num < this.slotCount)
		{
			addition.SlotPreference = global::Inventory.Slot.Preference.Define(num);
			global::Inventory.Payload.Result result3 = this.AssignItem(ref addition, global::Inventory.Payload.Opt.RestrictToOffset, null);
			if ((byte)(result3.flags & global::Inventory.Payload.Result.Flags.Complete) == 128)
			{
				return global::Inventory.ResultToItem(ref result3, global::Inventory.Payload.Opt.RestrictToOffset);
			}
			if ((byte)(result3.flags & global::Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				addition.UsesQuantity = (usesCount = result3.usesRemaining);
			}
		}
		global::Inventory.Slot.KindFlags kindFlags = global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor;
		if (flag2)
		{
			kindFlags &= ~(global::Inventory.Slot.KindFlags)(1 << (int)value);
		}
		addition.SlotPreference = global::Inventory.Slot.Preference.Define(kindFlags);
		return this.AddItem(ref addition);
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x000B9708 File Offset: 0x000B7908
	private bool RemoveItem(int slot, global::InventoryItem match, bool mustMatch)
	{
		global::Inventory.Collection<global::InventoryItem> collection = this.collection;
		global::InventoryItem inventoryItem;
		if ((!mustMatch || (collection.Get(slot, out inventoryItem) && object.ReferenceEquals(inventoryItem, match))) && collection.Evict(slot, out inventoryItem))
		{
			if (inventoryItem == this._activeItem)
			{
				this.DeactivateItem();
			}
			this.ItemRemoved(slot, inventoryItem.iface);
			this.MarkSlotDirty(slot);
			return true;
		}
		return false;
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x000B9774 File Offset: 0x000B7974
	private void DeleteItem(int slot)
	{
		this.RemoveItem(slot);
	}

	// Token: 0x06003027 RID: 12327 RVA: 0x000B9780 File Offset: 0x000B7980
	public bool NetworkItemAction(int slot, global::InventoryItem.MenuItem option)
	{
		Facepunch.NetworkView networkView = base.networkView;
		if (networkView)
		{
			networkView.RPC("IACT", 0, new object[]
			{
				(byte)slot,
				(byte)option
			});
			return true;
		}
		return false;
	}

	// Token: 0x06003028 RID: 12328 RVA: 0x000B97C8 File Offset: 0x000B79C8
	private void ItemMergeRPC(global::NetEntityID toInvID, int fromSlot, int toSlot, bool tryCombine)
	{
		global::NetCull.RPC<global::NetEntityID, byte, byte, bool>(this, "ITMG", 0, toInvID, (byte)fromSlot, (byte)toSlot, tryCombine);
	}

	// Token: 0x06003029 RID: 12329 RVA: 0x000B97E0 File Offset: 0x000B79E0
	private void ItemMergeRPC(int fromSlot, int toSlot, bool tryCombine)
	{
		global::NetCull.RPC<byte, byte, bool>(this, "ITSM", 0, (byte)fromSlot, (byte)toSlot, tryCombine);
	}

	// Token: 0x0600302A RID: 12330 RVA: 0x000B97F4 File Offset: 0x000B79F4
	private void ItemMoveRPC(global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		global::NetCull.RPC<global::NetEntityID, byte, byte>(this, "ITMV", 0, toInvID, (byte)fromSlot, (byte)toSlot);
	}

	// Token: 0x0600302B RID: 12331 RVA: 0x000B9808 File Offset: 0x000B7A08
	private void ItemMoveRPC(int fromSlot, int toSlot)
	{
		global::NetCull.RPC<byte, byte>(this, "ISMV", 0, (byte)fromSlot, (byte)toSlot);
	}

	// Token: 0x0600302C RID: 12332 RVA: 0x000B981C File Offset: 0x000B7A1C
	private global::Inventory.SlotOperationResult ItemMergeRPCPred(global::NetEntityID toInvID, int fromSlot, int toSlot, bool tryCombine)
	{
		global::Inventory component = toInvID.GetComponent<global::Inventory>();
		global::Inventory.SlotOperationResult result;
		if (component == this)
		{
			if ((int)(result = this.SlotOperation(fromSlot, toSlot, global::Inventory.SlotOperationsMerge(tryCombine))) > 0)
			{
				this.ItemMergeRPC(fromSlot, toSlot, tryCombine);
			}
		}
		else if ((int)(result = this.SlotOperation(fromSlot, component, toSlot, global::Inventory.SlotOperationsMerge(tryCombine))) > 0)
		{
			this.ItemMergeRPC(toInvID, fromSlot, toSlot, tryCombine);
		}
		return result;
	}

	// Token: 0x0600302D RID: 12333 RVA: 0x000B9894 File Offset: 0x000B7A94
	private global::Inventory.SlotOperationResult ItemMergeRPCPred(int fromSlot, int toSlot, bool tryCombine)
	{
		global::Inventory.SlotOperationResult result;
		if ((int)(result = this.SlotOperation(fromSlot, toSlot, global::Inventory.SlotOperationsMerge(tryCombine))) > 0)
		{
			this.ItemMergeRPC(fromSlot, toSlot, tryCombine);
		}
		return result;
	}

	// Token: 0x0600302E RID: 12334 RVA: 0x000B98C8 File Offset: 0x000B7AC8
	private global::Inventory.SlotOperationResult ItemMoveRPCPred(global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		global::Inventory component = toInvID.GetComponent<global::Inventory>();
		global::Inventory.SlotOperationResult result;
		if (component == this)
		{
			if ((int)(result = this.SlotOperation(fromSlot, toSlot, global::Inventory.SlotOperations.Move)) > 0)
			{
				this.ItemMoveRPC(fromSlot, toSlot);
			}
		}
		else if ((int)(result = this.SlotOperation(fromSlot, component, toSlot, global::Inventory.SlotOperations.Move)) > 0)
		{
			this.ItemMoveRPC(toInvID, fromSlot, toSlot);
		}
		return result;
	}

	// Token: 0x0600302F RID: 12335 RVA: 0x000B9930 File Offset: 0x000B7B30
	private global::Inventory.SlotOperationResult ItemMoveRPCPred(int fromSlot, int toSlot)
	{
		global::Inventory.SlotOperationResult result;
		if ((int)(result = this.SlotOperation(fromSlot, toSlot, global::Inventory.SlotOperations.Move)) > 0)
		{
			this.ItemMoveRPC(fromSlot, toSlot);
		}
		return result;
	}

	// Token: 0x06003030 RID: 12336 RVA: 0x000B9960 File Offset: 0x000B7B60
	[RPC]
	protected void GNUP(byte[] data, uLink.NetworkMessageInfo info)
	{
		this.OnNetUpdate(new BitStream(data, false));
		this.Refresh();
	}

	// Token: 0x06003031 RID: 12337 RVA: 0x000B9978 File Offset: 0x000B7B78
	[RPC]
	protected void ITMV(global::NetEntityID toInvID, byte fromSlot, byte toSlot, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003032 RID: 12338 RVA: 0x000B997C File Offset: 0x000B7B7C
	[RPC]
	protected void ISMV(byte fromSlot, byte toSlot, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003033 RID: 12339 RVA: 0x000B9980 File Offset: 0x000B7B80
	[RPC]
	[global::NGCRPCSkip]
	protected void IACT(byte itemIndex, byte action, uLink.NetworkMessageInfo info)
	{
		global::InventoryItem inventoryItem;
		if (this.collection.Get((int)itemIndex, out inventoryItem))
		{
			inventoryItem.OnMenuOption((global::InventoryItem.MenuItem)action);
		}
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x000B99A8 File Offset: 0x000B7BA8
	[RPC]
	[global::NGCRPCSkip]
	protected void IAST(byte itemIndex, uLink.NetworkViewID itemRepID, uLink.NetworkMessageInfo info)
	{
		this.SetActiveItemManually((int)itemIndex, (!(itemRepID != uLink.NetworkViewID.unassigned)) ? null : uLink.NetworkView.Find(itemRepID).GetComponent<global::ItemRepresentation>());
	}

	// Token: 0x06003035 RID: 12341 RVA: 0x000B99E0 File Offset: 0x000B7BE0
	[RPC]
	[global::NGCRPCSkip]
	protected void ITDE(uLink.NetworkMessageInfo info)
	{
		this.DeactivateItem();
	}

	// Token: 0x06003036 RID: 12342 RVA: 0x000B99E8 File Offset: 0x000B7BE8
	[RPC]
	protected void CFAR(BitStream stream)
	{
		global::ArmorModelMemberMap<global::ArmorDataBlock> armorDatablockMap = default(global::ArmorModelMemberMap<global::ArmorDataBlock>);
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			armorDatablockMap[armorModelSlot] = (global::DatablockDictionary.GetByUniqueID(stream.ReadInt32()) as global::ArmorDataBlock);
		}
		this.BindArmorModelsFromArmorDatablockMap(armorDatablockMap);
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x000B9A30 File Offset: 0x000B7C30
	[RPC]
	protected void SVUF(uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x000B9A34 File Offset: 0x000B7C34
	[RPC]
	protected void ITMG(global::NetEntityID toInvID, byte fromSlot, byte toSlot, bool tryCombine, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003039 RID: 12345 RVA: 0x000B9A38 File Offset: 0x000B7C38
	[RPC]
	protected void ITSM(byte fromSlot, byte toSlot, bool tryCombine, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x0600303A RID: 12346 RVA: 0x000B9A3C File Offset: 0x000B7C3C
	public bool SplitStack(int slotNumber)
	{
		global::InventoryItem inventoryItem;
		if (this.GetItem(slotNumber, out inventoryItem))
		{
			int num = inventoryItem.uses;
			if (num > 1 && this.anyVacantSlots && inventoryItem.datablock.IsSplittable())
			{
				int num2 = num / 2;
				int num3 = num2 - this.AddItemAmount(inventoryItem.datablock, num2, global::Inventory.AmountMode.OnlyCreateNew);
				if (num3 > 0)
				{
					num -= num3;
					inventoryItem.SetUses(num);
					global::NetCull.RPC<byte>(this, "ITSP", 0, (byte)slotNumber);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600303B RID: 12347 RVA: 0x000B9ABC File Offset: 0x000B7CBC
	[RPC]
	protected void ITSP(byte slotNumber, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x000B9AC0 File Offset: 0x000B7CC0
	[RPC]
	protected void CLEV(byte itemEvent, int uniqueID)
	{
		global::ItemDataBlock byUniqueID = global::DatablockDictionary.GetByUniqueID(uniqueID);
		if (byUniqueID)
		{
			byUniqueID.OnItemEvent((global::InventoryItem.ItemEvent)itemEvent);
		}
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
	[RPC]
	protected void SVUC(byte cell, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x0600303E RID: 12350 RVA: 0x000B9AEC File Offset: 0x000B7CEC
	private global::Inventory.SlotOperationResult SlotOperation(int fromSlot, int toSlot, global::Inventory.SlotOperationsInfo info)
	{
		return this.SlotOperation(fromSlot, this, toSlot, info);
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x000B9AF8 File Offset: 0x000B7CF8
	private global::Inventory.SlotOperationResult SlotOperation(int fromSlot, global::Inventory toInventory, int toSlot, global::Inventory.SlotOperationsInfo info)
	{
		if ((byte)((global::Inventory.SlotOperations)7 & info.SlotOperations) == 0)
		{
			return global::Inventory.SlotOperationResult.Error_NoOpArgs;
		}
		if (!this || !toInventory)
		{
			return global::Inventory.SlotOperationResult.Error_MissingInventory;
		}
		bool flag = this == toInventory;
		if (flag && toSlot == fromSlot)
		{
			return global::Inventory.SlotOperationResult.Error_SameSlot;
		}
		global::InventoryItem inventoryItem;
		if (!this.GetItem(fromSlot, out inventoryItem))
		{
			return global::Inventory.SlotOperationResult.Error_EmptySourceSlot;
		}
		global::InventoryItem inventoryItem2;
		if (toInventory.GetItem(toSlot, out inventoryItem2))
		{
			this.MarkSlotDirty(fromSlot);
			toInventory.MarkSlotDirty(toSlot);
			global::InventoryItem.MergeResult mergeResult;
			if ((byte)((global::Inventory.SlotOperations)3 & info.SlotOperations) == 1 && inventoryItem.datablockUniqueID == inventoryItem2.datablockUniqueID)
			{
				mergeResult = inventoryItem.iface.TryStack(inventoryItem2.iface);
			}
			else if ((byte)((global::Inventory.SlotOperations)3 & info.SlotOperations) != 0)
			{
				mergeResult = inventoryItem.iface.TryCombine(inventoryItem2.iface);
			}
			else
			{
				mergeResult = global::InventoryItem.MergeResult.Failed;
			}
			global::InventoryItem.MergeResult mergeResult2 = mergeResult;
			if (mergeResult2 == global::InventoryItem.MergeResult.Merged)
			{
				return global::Inventory.SlotOperationResult.Success_Stacked;
			}
			if (mergeResult2 == global::InventoryItem.MergeResult.Combined)
			{
				return global::Inventory.SlotOperationResult.Success_Combined;
			}
			if ((byte)(global::Inventory.SlotOperations.Move & info.SlotOperations) == 4)
			{
				return global::Inventory.SlotOperationResult.Error_OccupiedDestination;
			}
			return global::Inventory.SlotOperationResult.NoOp;
		}
		else
		{
			if ((byte)(global::Inventory.SlotOperations.Move & info.SlotOperations) == 0)
			{
				return global::Inventory.SlotOperationResult.Error_EmptyDestinationSlot;
			}
			if (this.MoveItemAtSlotToEmptySlot(toInventory, fromSlot, toSlot))
			{
				if (this)
				{
					this.MarkSlotDirty(fromSlot);
				}
				if (toInventory)
				{
					toInventory.MarkSlotDirty(toSlot);
				}
				return global::Inventory.SlotOperationResult.Success_Moved;
			}
			return global::Inventory.SlotOperationResult.Error_Failed;
		}
	}

	// Token: 0x06003040 RID: 12352 RVA: 0x000B9C54 File Offset: 0x000B7E54
	private static global::Inventory.SlotOperations SlotOperationsMerge(bool tryCombine)
	{
		return tryCombine ? ((global::Inventory.SlotOperations)3) : global::Inventory.SlotOperations.Stack;
	}

	// Token: 0x06003041 RID: 12353 RVA: 0x000B9C64 File Offset: 0x000B7E64
	public global::Inventory.SlotOperationResult ItemMergePredicted(global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(toInvID, fromSlot, toSlot, false);
	}

	// Token: 0x06003042 RID: 12354 RVA: 0x000B9C70 File Offset: 0x000B7E70
	public global::Inventory.SlotOperationResult ItemMergePredicted(global::Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemMergePredicted(global::NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x000B9C80 File Offset: 0x000B7E80
	public global::Inventory.SlotOperationResult ItemMergePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(fromSlot, toSlot, false);
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x000B9C8C File Offset: 0x000B7E8C
	public static global::Inventory.SlotOperationResult ItemMergePredicted(global::NetEntityID fromInvID, global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		global::Inventory component = fromInvID.GetComponent<global::Inventory>();
		if (!component)
		{
			return global::Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemMergePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x000B9CB8 File Offset: 0x000B7EB8
	public global::Inventory.SlotOperationResult ItemCombinePredicted(global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(toInvID, fromSlot, toSlot, true);
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x000B9CC4 File Offset: 0x000B7EC4
	public global::Inventory.SlotOperationResult ItemCombinePredicted(global::Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemCombinePredicted(global::NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x000B9CD4 File Offset: 0x000B7ED4
	public global::Inventory.SlotOperationResult ItemCombinePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(fromSlot, toSlot, true);
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x000B9CE0 File Offset: 0x000B7EE0
	public static global::Inventory.SlotOperationResult ItemCombinePredicted(global::NetEntityID fromInvID, global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		global::Inventory component = fromInvID.GetComponent<global::Inventory>();
		if (!component)
		{
			return global::Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemCombinePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06003049 RID: 12361 RVA: 0x000B9D0C File Offset: 0x000B7F0C
	public global::Inventory.SlotOperationResult ItemMovePredicted(global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMoveRPCPred(toInvID, fromSlot, toSlot);
	}

	// Token: 0x0600304A RID: 12362 RVA: 0x000B9D18 File Offset: 0x000B7F18
	public global::Inventory.SlotOperationResult ItemMovePredicted(global::Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemMovePredicted(global::NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x0600304B RID: 12363 RVA: 0x000B9D28 File Offset: 0x000B7F28
	public global::Inventory.SlotOperationResult ItemMovePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMovePredicted(fromSlot, toSlot);
	}

	// Token: 0x0600304C RID: 12364 RVA: 0x000B9D34 File Offset: 0x000B7F34
	public static global::Inventory.SlotOperationResult ItemMovePredicted(global::NetEntityID fromInvID, global::NetEntityID toInvID, int fromSlot, int toSlot)
	{
		global::Inventory component = fromInvID.GetComponent<global::Inventory>();
		if (!component)
		{
			return global::Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemMovePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x0600304D RID: 12365 RVA: 0x000B9D60 File Offset: 0x000B7F60
	protected virtual void ConfigureSlots(int totalCount, ref global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> ranges, ref global::Inventory.SlotFlags[] flags)
	{
	}

	// Token: 0x0600304E RID: 12366 RVA: 0x000B9D64 File Offset: 0x000B7F64
	protected virtual void ItemRemoved(int slot, global::IInventoryItem item)
	{
		global::FireBarrel local = base.GetLocal<global::FireBarrel>();
		if (local)
		{
			local.InvItemRemoved();
		}
	}

	// Token: 0x0600304F RID: 12367 RVA: 0x000B9D8C File Offset: 0x000B7F8C
	protected virtual void ItemAdded(int slot, global::IInventoryItem item)
	{
		global::FireBarrel local = base.GetLocal<global::FireBarrel>();
		if (local)
		{
			local.InvItemAdded();
		}
	}

	// Token: 0x06003050 RID: 12368 RVA: 0x000B9DB4 File Offset: 0x000B7FB4
	protected virtual bool CheckSlotFlags(global::Inventory.SlotFlags itemSlotFlags, global::Inventory.SlotFlags slotFlags)
	{
		return true;
	}

	// Token: 0x06003051 RID: 12369 RVA: 0x000B9DB8 File Offset: 0x000B7FB8
	protected virtual void DoSetActiveItem(global::InventoryItem item)
	{
		this._activeItem = item;
	}

	// Token: 0x06003052 RID: 12370 RVA: 0x000B9DC4 File Offset: 0x000B7FC4
	protected virtual void DoDeactivateItem()
	{
		this._activeItem = null;
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x000B9DD0 File Offset: 0x000B7FD0
	public virtual void Refresh()
	{
	}

	// Token: 0x040019E0 RID: 6624
	private const uLink.RPCMode ItemAction_RPCMode = 0;

	// Token: 0x040019E1 RID: 6625
	private const string GetNetUpdate_RPC = "GNUP";

	// Token: 0x040019E2 RID: 6626
	private const string ItemMove_RPC = "ITMV";

	// Token: 0x040019E3 RID: 6627
	private const string ItemMoveSelf_RPC = "ISMV";

	// Token: 0x040019E4 RID: 6628
	private const string DoItemAction_RPC = "IACT";

	// Token: 0x040019E5 RID: 6629
	private const string SetActiveItem_RPC = "IAST";

	// Token: 0x040019E6 RID: 6630
	private const string DeactivateItem_RPC = "ITDE";

	// Token: 0x040019E7 RID: 6631
	private const string ConfigureArmor_RPC = "CFAR";

	// Token: 0x040019E8 RID: 6632
	private const string Server_Request_Inventory_Update_Full = "SVUF";

	// Token: 0x040019E9 RID: 6633
	private const string MergeItems_RPC = "ITMG";

	// Token: 0x040019EA RID: 6634
	private const string MergeItemsSelf_RPC = "ITSM";

	// Token: 0x040019EB RID: 6635
	private const string SplitStack_RPCName = "ITSP";

	// Token: 0x040019EC RID: 6636
	private const string Client_ItemEvent = "CLEV";

	// Token: 0x040019ED RID: 6637
	private const string Server_Request_Inventory_Update_Cell = "SVUC";

	// Token: 0x040019EE RID: 6638
	private const global::Inventory.SlotOperations SlotOperations_Mask = (global::Inventory.SlotOperations)7;

	// Token: 0x040019EF RID: 6639
	private const global::Inventory.SlotOperations SlotOperations_Operations = (global::Inventory.SlotOperations)7;

	// Token: 0x040019F0 RID: 6640
	private const global::Inventory.SlotOperations SlotOperations_Options = (global::Inventory.SlotOperations)0;

	// Token: 0x040019F1 RID: 6641
	[NonSerialized]
	public global::InventoryItem _activeItem;

	// Token: 0x040019F2 RID: 6642
	[NonSerialized]
	private global::CacheRef<global::InventoryHolder> _inventoryHolder;

	// Token: 0x040019F3 RID: 6643
	[NonSerialized]
	private global::CacheRef<global::EquipmentWearer> _equipmentWearer;

	// Token: 0x040019F4 RID: 6644
	[NonSerialized]
	private global::Inventory.Slot.KindDictionary<global::Inventory.Slot.Range> slotRanges;

	// Token: 0x040019F5 RID: 6645
	[NonSerialized]
	private global::Inventory.Collection<global::InventoryItem> _collection_;

	// Token: 0x040019F6 RID: 6646
	[NonSerialized]
	private global::Inventory.SlotFlags[] _slotFlags;

	// Token: 0x040019F7 RID: 6647
	[NonSerialized]
	private global::ArmorModelMemberMap<global::ArmorDataBlock> lastNetworkedArmorDatablocks;

	// Token: 0x040019F8 RID: 6648
	[NonSerialized]
	private bool _collection_made_;

	// Token: 0x040019F9 RID: 6649
	[NonSerialized]
	private bool _locked;

	// Token: 0x020005D9 RID: 1497
	public enum AddExistingItemResult
	{
		// Token: 0x040019FB RID: 6651
		CompletlyStacked,
		// Token: 0x040019FC RID: 6652
		Moved,
		// Token: 0x040019FD RID: 6653
		PartiallyStacked,
		// Token: 0x040019FE RID: 6654
		Failed,
		// Token: 0x040019FF RID: 6655
		BadItemArgument
	}

	// Token: 0x020005DA RID: 1498
	[Flags]
	private enum AddMultipleItemFlags
	{
		// Token: 0x04001A01 RID: 6657
		MustBeSplittable = 2,
		// Token: 0x04001A02 RID: 6658
		MustBeNonSplittable = 1,
		// Token: 0x04001A03 RID: 6659
		DoNotCreateNewSplittableStacks = 4,
		// Token: 0x04001A04 RID: 6660
		DoNotStackSplittables = 8
	}

	// Token: 0x020005DB RID: 1499
	public struct Addition
	{
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000B9DD4 File Offset: 0x000B7FD4
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x000B9DE8 File Offset: 0x000B7FE8
		public global::ItemDataBlock ItemDataBlock
		{
			get
			{
				return (global::ItemDataBlock)this.Ident.datablock;
			}
			set
			{
				this.Ident = (global::Datablock.Ident)value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000B9DF8 File Offset: 0x000B7FF8
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000B9E24 File Offset: 0x000B8024
		public string Name
		{
			get
			{
				global::ItemDataBlock itemDataBlock = this.ItemDataBlock;
				return (!itemDataBlock) ? null : itemDataBlock.name;
			}
			set
			{
				this.Ident = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000B9E34 File Offset: 0x000B8034
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x000B9E60 File Offset: 0x000B8060
		public int UniqueID
		{
			get
			{
				global::ItemDataBlock itemDataBlock = this.ItemDataBlock;
				return (!itemDataBlock) ? 0 : itemDataBlock.uniqueID;
			}
			set
			{
				this.Ident = value;
			}
		}

		// Token: 0x04001A05 RID: 6661
		public global::Datablock.Ident Ident;

		// Token: 0x04001A06 RID: 6662
		public global::Inventory.Uses.Quantity UsesQuantity;

		// Token: 0x04001A07 RID: 6663
		public global::Inventory.Slot.Preference SlotPreference;
	}

	// Token: 0x020005DC RID: 1500
	public enum AmountMode
	{
		// Token: 0x04001A09 RID: 6665
		Default,
		// Token: 0x04001A0A RID: 6666
		OnlyStack,
		// Token: 0x04001A0B RID: 6667
		OnlyCreateNew,
		// Token: 0x04001A0C RID: 6668
		IgnoreSplittables
	}

	// Token: 0x020005DD RID: 1501
	private sealed class Collection<T>
	{
		// Token: 0x0600305A RID: 12378 RVA: 0x000B9E70 File Offset: 0x000B8070
		public Collection(int Capacity)
		{
			if (Capacity < 0 || Capacity > 256)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.capacity = Capacity;
			this.count = 0;
			this.array = new T[Capacity];
			this.indices = new byte[Capacity];
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000B9EC4 File Offset: 0x000B80C4
		public bool AnyVacantOrOccupied
		{
			get
			{
				return this.capacity > 0;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000B9ED0 File Offset: 0x000B80D0
		public bool IsCompletelyVacant
		{
			get
			{
				return this.count == 0 && this.capacity > 0;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000B9EEC File Offset: 0x000B80EC
		public bool HasVacancy
		{
			get
			{
				return this.count < this.capacity;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000B9EFC File Offset: 0x000B80FC
		public bool HasNoVacancy
		{
			get
			{
				return this.count == this.capacity;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000B9F0C File Offset: 0x000B810C
		public bool HasNoOccupant
		{
			get
			{
				return this.count == 0;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000B9F18 File Offset: 0x000B8118
		public bool HasAnyOccupant
		{
			get
			{
				return this.count > 0;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000B9F24 File Offset: 0x000B8124
		public int FirstVacancy
		{
			get
			{
				if (this.count == this.capacity)
				{
					return -1;
				}
				for (int i = 0; i < 256; i++)
				{
					if (!this.occupied[i])
					{
						return i;
					}
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000B9F74 File Offset: 0x000B8174
		public int FirstOccupied
		{
			get
			{
				if (this.count > 0)
				{
					return (int)this.indices[0];
				}
				return -1;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000B9F8C File Offset: 0x000B818C
		public int LastOccupied
		{
			get
			{
				if (this.count > 0)
				{
					return (int)this.indices[this.count - 1];
				}
				return -1;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000B9FAC File Offset: 0x000B81AC
		public bool MarkedDirty
		{
			get
			{
				return this.forcedDirty || this.countDirty > 0;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000B9FC8 File Offset: 0x000B81C8
		public bool CompletelyDirty
		{
			get
			{
				return this.countDirty == this.capacity && this.capacity > 0;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000B9FE8 File Offset: 0x000B81E8
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000B9FF0 File Offset: 0x000B81F0
		public bool ForcedDirty
		{
			get
			{
				return this.forcedDirty;
			}
			set
			{
				if (value != this.forcedDirty && this.capacity > 0)
				{
					this.forcedDirty = value;
				}
			}
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000BA014 File Offset: 0x000B8214
		public bool Clean(out global::Inventory.Mask dirtyMask, out int numDirty)
		{
			return this.Clean(out dirtyMask, out numDirty, false);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000BA020 File Offset: 0x000B8220
		public bool Clean(out global::Inventory.Mask dirtyMask, out int numDirty, bool dontActuallyClean)
		{
			if (this.countDirty > 0)
			{
				dirtyMask = this.dirty;
				numDirty = this.countDirty;
				if (!dontActuallyClean)
				{
					this.dirty = default(global::Inventory.Mask);
					this.countDirty = 0;
					this.forcedDirty = false;
				}
				return true;
			}
			dirtyMask = default(global::Inventory.Mask);
			numDirty = 0;
			if (this.forcedDirty)
			{
				if (!dontActuallyClean)
				{
					this.forcedDirty = false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000BA0A0 File Offset: 0x000B82A0
		public bool GetByOrder(int index, out T value)
		{
			if (index < this.count)
			{
				value = this.array[(int)this.indices[index]];
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000BA0E4 File Offset: 0x000B82E4
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000BA0EC File Offset: 0x000B82EC
		public int OccupiedCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000BA0F4 File Offset: 0x000B82F4
		public int VacantCount
		{
			get
			{
				return this.capacity - this.count;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000BA104 File Offset: 0x000B8304
		public int DirtyCount
		{
			get
			{
				return this.countDirty;
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000BA10C File Offset: 0x000B830C
		public void MarkCompletelyDirty()
		{
			this.dirty = new global::Inventory.Mask(0, this.capacity);
			this.countDirty = this.capacity;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000BA12C File Offset: 0x000B832C
		public bool MarkDirty(int slot)
		{
			if (slot >= 0 && slot < this.capacity && this.dirty.On(slot))
			{
				this.countDirty++;
				return true;
			}
			return false;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000BA164 File Offset: 0x000B8364
		public bool IsVacant(int slot)
		{
			return slot >= 0 && slot < this.capacity && !this.occupied[slot];
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000BA18C File Offset: 0x000B838C
		public bool IsOccupied(int slot)
		{
			return slot >= 0 && slot < this.capacity && this.occupied[slot];
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000BA1BC File Offset: 0x000B83BC
		public bool IsWithinRange(int slot)
		{
			return slot >= 0 && slot < this.capacity;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000BA1D4 File Offset: 0x000B83D4
		public global::Inventory.Collection<T>.OccupiedCollection.Enumerator OccupiedEnumerator
		{
			get
			{
				return new global::Inventory.Collection<T>.OccupiedCollection.Enumerator(this);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x000BA1DC File Offset: 0x000B83DC
		public global::Inventory.Collection<T>.OccupiedCollection.ReverseEnumerator OccupiedReverseEnumerator
		{
			get
			{
				return new global::Inventory.Collection<T>.OccupiedCollection.ReverseEnumerator(this);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000BA1E4 File Offset: 0x000B83E4
		public global::Inventory.Collection<T>.VacantCollection.Enumerator VacantEnumerator
		{
			get
			{
				return new global::Inventory.Collection<T>.VacantCollection.Enumerator(this);
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000BA1EC File Offset: 0x000B83EC
		public void Contract()
		{
			this.Contract(new global::Inventory.Slot.Range(0, this.capacity));
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000BA200 File Offset: 0x000B8400
		public void Contract(global::Inventory.Slot.Range range)
		{
			int start = range.Start;
			int num = start + range.Count;
			if (start < 0 || num > this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.count == this.capacity || start == num)
			{
				return;
			}
			for (int i = 0; i < this.count; i++)
			{
				if ((int)this.indices[i] >= start)
				{
					if ((int)this.indices[i] >= num)
					{
						break;
					}
					do
					{
						int num2 = start++;
						if (num2 != (int)this.indices[i])
						{
							this.array[num2] = this.array[(int)this.indices[i]];
							this.array[(int)this.indices[i]] = default(T);
							if (this.dirty.On((int)this.indices[i]))
							{
								this.countDirty++;
							}
							this.indices[i] = (byte)num2;
							if (this.dirty.On(i))
							{
								this.countDirty++;
							}
							if (start == num)
							{
								break;
							}
						}
					}
					while (++i < this.count && (int)this.indices[i] < num);
				}
			}
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000BA35C File Offset: 0x000B855C
		public bool Get(int slot, out T value)
		{
			if (slot < 0 || slot >= this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.occupied[slot])
			{
				value = this.array[slot];
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000BA3B8 File Offset: 0x000B85B8
		private bool DoReplace(bool equalityCheck, int slot, T value, out T replacedValue)
		{
			replacedValue = this.array[slot];
			if (equalityCheck && object.Equals(replacedValue, value))
			{
				return false;
			}
			this.array[slot] = value;
			if (this.dirty.On(slot))
			{
				this.countDirty++;
			}
			return true;
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000BA42C File Offset: 0x000B862C
		public bool Supplant(int slot, T value, out T replacedValue, bool equalityCheck)
		{
			if (slot < 0 || slot >= this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.occupied.On(slot))
			{
				return this.DoReplace(equalityCheck, slot, value, out replacedValue);
			}
			replacedValue = default(T);
			return false;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000BA480 File Offset: 0x000B8680
		private void DoSet(int slot, T value)
		{
			if (this.count == 0 || (int)this.indices[0] > slot)
			{
				int num = this.count;
				for (int i = this.count - 1; i >= 0; i--)
				{
					this.indices[num] = this.indices[i];
					num--;
				}
				this.indices[0] = (byte)slot;
			}
			else
			{
				for (int j = this.count - 1; j >= 0; j--)
				{
					if ((int)this.indices[j] <= slot)
					{
						this.indices[j + 1] = (byte)slot;
						break;
					}
					this.indices[j + 1] = this.indices[j];
				}
			}
			this.array[slot] = value;
			this.count++;
			if (this.dirty.On(slot))
			{
				this.countDirty++;
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000BA574 File Offset: 0x000B8774
		public bool Occupy(int slot, T occupant)
		{
			if (slot < 0 || slot >= this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.occupied.On(slot))
			{
				this.DoSet(slot, occupant);
				return true;
			}
			return false;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000BA5AC File Offset: 0x000B87AC
		public bool SupplantOrOccupy(int slot, T occupant, out T replacedValue, bool equalityCheck)
		{
			if (slot < 0 || slot >= this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.occupied.On(slot))
			{
				replacedValue = default(T);
				this.DoSet(slot, occupant);
				return false;
			}
			return this.DoReplace(equalityCheck, slot, occupant, out replacedValue);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000BA608 File Offset: 0x000B8808
		public bool Evict(int slot, out T value)
		{
			if (slot < 0 || slot >= this.capacity)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.occupied.Off(slot))
			{
				for (int i = 0; i < this.count; i++)
				{
					if ((int)this.indices[i] == slot)
					{
						for (int j = i + 1; j < this.count; j++)
						{
							this.indices[i] = this.indices[j];
							i++;
						}
						this.indices[--this.count] = 0;
						value = this.array[slot];
						this.array[slot] = default(T);
						if (this.dirty.On(slot))
						{
							this.countDirty++;
						}
						return true;
					}
				}
				throw new InvalidOperationException();
			}
			value = default(T);
			return false;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000BA70C File Offset: 0x000B890C
		public global::Inventory.Collection<T>.OccupiedCollection Occupied
		{
			get
			{
				global::Inventory.Collection<T>.OccupiedCollection result;
				if ((result = this.occupiedCollection) == null)
				{
					result = (this.occupiedCollection = new global::Inventory.Collection<T>.OccupiedCollection(this));
				}
				return result;
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000BA738 File Offset: 0x000B8938
		public T[] OccupiedToArray()
		{
			T[] array = new T[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.array[(int)this.indices[i]];
			}
			return array;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000BA784 File Offset: 0x000B8984
		public global::Inventory.Collection<T>.VacantCollection Vacant
		{
			get
			{
				global::Inventory.Collection<T>.VacantCollection result;
				if ((result = this.vacantCollection) == null)
				{
					result = (this.vacantCollection = new global::Inventory.Collection<T>.VacantCollection(this));
				}
				return result;
			}
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000BA7B0 File Offset: 0x000B89B0
		public bool IsDirty(int slot)
		{
			return slot >= 0 && slot < this.capacity && this.dirty[slot];
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		public void MarkCompletelyClean()
		{
			this.dirty = default(global::Inventory.Mask);
			this.countDirty = 0;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000BA804 File Offset: 0x000B8A04
		public bool MarkClean(int slot)
		{
			if (slot >= 0 && slot < this.capacity && this.dirty.Off(slot))
			{
				this.countDirty--;
				return true;
			}
			return false;
		}

		// Token: 0x04001A0D RID: 6669
		[NonSerialized]
		private global::Inventory.Collection<T>.OccupiedCollection occupiedCollection;

		// Token: 0x04001A0E RID: 6670
		[NonSerialized]
		private global::Inventory.Collection<T>.VacantCollection vacantCollection;

		// Token: 0x04001A0F RID: 6671
		[NonSerialized]
		private T[] array;

		// Token: 0x04001A10 RID: 6672
		[NonSerialized]
		private byte[] indices;

		// Token: 0x04001A11 RID: 6673
		[NonSerialized]
		private global::Inventory.Mask occupied;

		// Token: 0x04001A12 RID: 6674
		[NonSerialized]
		private global::Inventory.Mask dirty;

		// Token: 0x04001A13 RID: 6675
		[NonSerialized]
		private int count;

		// Token: 0x04001A14 RID: 6676
		[NonSerialized]
		private int capacity;

		// Token: 0x04001A15 RID: 6677
		[NonSerialized]
		private int countDirty;

		// Token: 0x04001A16 RID: 6678
		[NonSerialized]
		private bool forcedDirty;

		// Token: 0x020005DE RID: 1502
		public static class Default
		{
			// Token: 0x04001A17 RID: 6679
			public static readonly global::Inventory.Collection<T> Empty = new global::Inventory.Collection<T>(0);
		}

		// Token: 0x020005DF RID: 1503
		public sealed class OccupiedCollection : IEnumerable, IEnumerable<T>
		{
			// Token: 0x06003087 RID: 12423 RVA: 0x000BA84C File Offset: 0x000B8A4C
			internal OccupiedCollection(global::Inventory.Collection<T> collection)
			{
				this.Collection = collection;
			}

			// Token: 0x06003088 RID: 12424 RVA: 0x000BA85C File Offset: 0x000B8A5C
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003089 RID: 12425 RVA: 0x000BA86C File Offset: 0x000B8A6C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000A5A RID: 2650
			// (get) Token: 0x0600308A RID: 12426 RVA: 0x000BA87C File Offset: 0x000B8A7C
			public int Count
			{
				get
				{
					return this.Collection.count;
				}
			}

			// Token: 0x17000A5B RID: 2651
			// (get) Token: 0x0600308B RID: 12427 RVA: 0x000BA88C File Offset: 0x000B8A8C
			public bool Empty
			{
				get
				{
					return this.Collection.count == 0;
				}
			}

			// Token: 0x0600308C RID: 12428 RVA: 0x000BA89C File Offset: 0x000B8A9C
			public T[] ToArray()
			{
				return this.Collection.OccupiedToArray();
			}

			// Token: 0x0600308D RID: 12429 RVA: 0x000BA8AC File Offset: 0x000B8AAC
			public global::Inventory.Collection<T>.OccupiedCollection.Enumerator GetEnumerator()
			{
				return new global::Inventory.Collection<T>.OccupiedCollection.Enumerator(this.Collection);
			}

			// Token: 0x04001A18 RID: 6680
			public readonly global::Inventory.Collection<T> Collection;

			// Token: 0x020005E0 RID: 1504
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
			{
				// Token: 0x0600308E RID: 12430 RVA: 0x000BA8BC File Offset: 0x000B8ABC
				internal Enumerator(global::Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.indexPosition = -1;
				}

				// Token: 0x17000A5C RID: 2652
				// (get) Token: 0x0600308F RID: 12431 RVA: 0x000BA8CC File Offset: 0x000B8ACC
				object IEnumerator.Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x06003090 RID: 12432 RVA: 0x000BA8F8 File Offset: 0x000B8AF8
				public bool MoveNext()
				{
					return ++this.indexPosition < this.collection.count;
				}

				// Token: 0x17000A5D RID: 2653
				// (get) Token: 0x06003091 RID: 12433 RVA: 0x000BA924 File Offset: 0x000B8B24
				public T Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x17000A5E RID: 2654
				// (get) Token: 0x06003092 RID: 12434 RVA: 0x000BA954 File Offset: 0x000B8B54
				public int Slot
				{
					get
					{
						return (int)this.collection.indices[this.indexPosition];
					}
				}

				// Token: 0x06003093 RID: 12435 RVA: 0x000BA968 File Offset: 0x000B8B68
				public void Reset()
				{
					this.indexPosition = -1;
				}

				// Token: 0x06003094 RID: 12436 RVA: 0x000BA974 File Offset: 0x000B8B74
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x04001A19 RID: 6681
				private global::Inventory.Collection<T> collection;

				// Token: 0x04001A1A RID: 6682
				private int indexPosition;
			}

			// Token: 0x020005E1 RID: 1505
			public struct ReverseEnumerator : IDisposable, IEnumerator, IEnumerator<T>
			{
				// Token: 0x06003095 RID: 12437 RVA: 0x000BA980 File Offset: 0x000B8B80
				internal ReverseEnumerator(global::Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.indexPosition = collection.count;
				}

				// Token: 0x17000A5F RID: 2655
				// (get) Token: 0x06003096 RID: 12438 RVA: 0x000BA998 File Offset: 0x000B8B98
				object IEnumerator.Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x06003097 RID: 12439 RVA: 0x000BA9C4 File Offset: 0x000B8BC4
				public bool MoveNext()
				{
					return --this.indexPosition >= 0;
				}

				// Token: 0x17000A60 RID: 2656
				// (get) Token: 0x06003098 RID: 12440 RVA: 0x000BA9E8 File Offset: 0x000B8BE8
				public T Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x17000A61 RID: 2657
				// (get) Token: 0x06003099 RID: 12441 RVA: 0x000BAA18 File Offset: 0x000B8C18
				public int Slot
				{
					get
					{
						return (int)this.collection.indices[this.indexPosition];
					}
				}

				// Token: 0x0600309A RID: 12442 RVA: 0x000BAA2C File Offset: 0x000B8C2C
				public void Reset()
				{
					this.indexPosition = this.collection.count;
				}

				// Token: 0x0600309B RID: 12443 RVA: 0x000BAA40 File Offset: 0x000B8C40
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x04001A1B RID: 6683
				private global::Inventory.Collection<T> collection;

				// Token: 0x04001A1C RID: 6684
				private int indexPosition;
			}
		}

		// Token: 0x020005E2 RID: 1506
		public sealed class VacantCollection : IEnumerable, IEnumerable<int>
		{
			// Token: 0x0600309C RID: 12444 RVA: 0x000BAA4C File Offset: 0x000B8C4C
			internal VacantCollection(global::Inventory.Collection<T> collection)
			{
				this.Collection = collection;
			}

			// Token: 0x0600309D RID: 12445 RVA: 0x000BAA5C File Offset: 0x000B8C5C
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600309E RID: 12446 RVA: 0x000BAA6C File Offset: 0x000B8C6C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000A62 RID: 2658
			// (get) Token: 0x0600309F RID: 12447 RVA: 0x000BAA7C File Offset: 0x000B8C7C
			public int Count
			{
				get
				{
					return this.Collection.capacity - this.Collection.count;
				}
			}

			// Token: 0x17000A63 RID: 2659
			// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000BAA98 File Offset: 0x000B8C98
			public bool Empty
			{
				get
				{
					return this.Collection.count == this.Collection.capacity;
				}
			}

			// Token: 0x060030A1 RID: 12449 RVA: 0x000BAAB4 File Offset: 0x000B8CB4
			public global::Inventory.Collection<T>.VacantCollection.Enumerator GetEnumerator()
			{
				return new global::Inventory.Collection<T>.VacantCollection.Enumerator(this.Collection);
			}

			// Token: 0x04001A1D RID: 6685
			public readonly global::Inventory.Collection<T> Collection;

			// Token: 0x020005E3 RID: 1507
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<int>
			{
				// Token: 0x060030A2 RID: 12450 RVA: 0x000BAAC4 File Offset: 0x000B8CC4
				internal Enumerator(global::Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.slotPosition = -1;
				}

				// Token: 0x17000A64 RID: 2660
				// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000BAAD4 File Offset: 0x000B8CD4
				object IEnumerator.Current
				{
					get
					{
						return this.slotPosition;
					}
				}

				// Token: 0x060030A4 RID: 12452 RVA: 0x000BAAE4 File Offset: 0x000B8CE4
				public bool MoveNext()
				{
					while (++this.slotPosition < this.collection.capacity)
					{
						if (!this.collection.occupied[this.slotPosition])
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x17000A65 RID: 2661
				// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000BAB38 File Offset: 0x000B8D38
				public int Current
				{
					get
					{
						return this.slotPosition;
					}
				}

				// Token: 0x060030A6 RID: 12454 RVA: 0x000BAB40 File Offset: 0x000B8D40
				public void Reset()
				{
					this.slotPosition = -1;
				}

				// Token: 0x060030A7 RID: 12455 RVA: 0x000BAB4C File Offset: 0x000B8D4C
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x04001A1E RID: 6686
				private global::Inventory.Collection<T> collection;

				// Token: 0x04001A1F RID: 6687
				private int slotPosition;
			}
		}
	}

	// Token: 0x020005E4 RID: 1508
	public static class Constants
	{
		// Token: 0x04001A20 RID: 6688
		public const int MaximumSlotCount = 256;
	}

	// Token: 0x020005E5 RID: 1509
	public struct Mask
	{
		// Token: 0x060030A8 RID: 12456 RVA: 0x000BAB58 File Offset: 0x000B8D58
		public Mask(bool defaultOn)
		{
			int num = (!defaultOn) ? 0 : -1;
			this.a = (this.b = (this.c = (this.d = (this.e = (this.f = (this.g = (this.h = num)))))));
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000BABBC File Offset: 0x000B8DBC
		public Mask(int onStart, int onCount)
		{
			this = new global::Inventory.Mask(false);
			int num = onStart;
			int num2 = onStart + onCount;
			while (num < 256 && num < num2)
			{
				this[num] = true;
				num++;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000BABFC File Offset: 0x000B8DFC
		public bool any
		{
			get
			{
				return this.a != 0 || this.b != 0 || this.c != 0 || this.d != 0 || this.e != 0 || this.f != 0 || this.g != 0 || this.h != 0;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000BAC68 File Offset: 0x000B8E68
		public int firstOnBit
		{
			get
			{
				int num = 0;
				int num2;
				if (this.a == 0)
				{
					num++;
					if (this.b == 0)
					{
						num++;
						if (this.c == 0)
						{
							num++;
							if (this.d == 0)
							{
								num++;
								if (this.e == 0)
								{
									num++;
									if (this.f == 0)
									{
										num++;
										if (this.g == 0)
										{
											num++;
											if (this.h == 0)
											{
												num++;
												num2 = 0;
											}
											else
											{
												num2 = this.h;
											}
										}
										else
										{
											num2 = this.g;
										}
									}
									else
									{
										num2 = this.f;
									}
								}
								else
								{
									num2 = this.e;
								}
							}
							else
							{
								num2 = this.d;
							}
						}
						else
						{
							num2 = this.c;
						}
					}
					else
					{
						num2 = this.b;
					}
				}
				else
				{
					num2 = this.a;
				}
				int num3 = 0;
				for (int i = 0; i < 32; i++)
				{
					if ((num2 & 1 << i) == 1 << i)
					{
						break;
					}
					num3++;
				}
				return num * 32 + num3;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000BAD8C File Offset: 0x000B8F8C
		public int lastOnBit
		{
			get
			{
				int num = 7;
				int num2;
				if (this.h == 0)
				{
					num--;
					if (this.g == 0)
					{
						num--;
						if (this.f == 0)
						{
							num--;
							if (this.e == 0)
							{
								num--;
								if (this.d == 0)
								{
									num--;
									if (this.c == 0)
									{
										num--;
										if (this.b == 0)
										{
											num--;
											if (this.a == 0)
											{
												return -1;
											}
											num2 = this.a;
										}
										else
										{
											num2 = this.b;
										}
									}
									else
									{
										num2 = this.c;
									}
								}
								else
								{
									num2 = this.d;
								}
							}
							else
							{
								num2 = this.e;
							}
						}
						else
						{
							num2 = this.f;
						}
					}
					else
					{
						num2 = this.g;
					}
				}
				else
				{
					num2 = this.h;
				}
				int num3 = 0;
				for (int i = 31; i >= 0; i--)
				{
					if ((num2 & 1 << i) == 1 << i)
					{
						break;
					}
					num3++;
				}
				return num * 32 + num3;
			}
		}

		// Token: 0x17000A69 RID: 2665
		public bool this[int bit]
		{
			get
			{
				if (bit < 128)
				{
					if (bit < 64)
					{
						if (bit < 32)
						{
							return (this.a & 1 << bit) != 0;
						}
						return (this.b & 1 << bit - 32) != 0;
					}
					else
					{
						if (bit < 96)
						{
							return (this.c & 1 << bit - 64) != 0;
						}
						return (this.d & 1 << bit - 96) != 0;
					}
				}
				else if (bit < 192)
				{
					if (bit < 160)
					{
						return (this.e & 1 << bit - 128) != 0;
					}
					return (this.f & 1 << bit - 160) != 0;
				}
				else
				{
					if (bit < 224)
					{
						return (this.g & 1 << bit - 192) != 0;
					}
					return (this.h & 1 << bit - 224) != 0;
				}
			}
			set
			{
				if (value)
				{
					if (bit < 128)
					{
						if (bit < 64)
						{
							if (bit < 32)
							{
								this.a |= 1 << bit;
							}
							else
							{
								this.b |= 1 << bit - 32;
							}
						}
						else if (bit < 96)
						{
							this.c |= 1 << bit - 64;
						}
						else
						{
							this.d |= 1 << bit - 96;
						}
					}
					else if (bit < 192)
					{
						if (bit < 160)
						{
							this.e |= 1 << bit - 128;
						}
						else
						{
							this.f |= 1 << bit - 160;
						}
					}
					else if (bit < 224)
					{
						this.g |= 1 << bit - 192;
					}
					else
					{
						this.h |= 1 << bit - 224;
					}
				}
				else if (bit < 128)
				{
					if (bit < 64)
					{
						if (bit < 32)
						{
							this.a &= ~(1 << bit);
						}
						else
						{
							this.b &= ~(1 << bit - 32);
						}
					}
					else if (bit < 96)
					{
						this.c &= ~(1 << bit - 64);
					}
					else
					{
						this.d &= ~(1 << bit - 96);
					}
				}
				else if (bit < 192)
				{
					if (bit < 160)
					{
						this.e &= ~(1 << bit - 128);
					}
					else
					{
						this.f &= ~(1 << bit - 160);
					}
				}
				else if (bit < 224)
				{
					this.g &= ~(1 << bit - 192);
				}
				else
				{
					this.h &= ~(1 << bit - 224);
				}
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000BB24C File Offset: 0x000B944C
		public bool On(int bit)
		{
			if (bit < 128)
			{
				if (bit < 64)
				{
					if (bit < 32)
					{
						int num = 1 << bit;
						if (num != 0 && (this.a & num) == 0)
						{
							this.a |= num;
							return true;
						}
						return false;
					}
					else
					{
						int num = 1 << bit - 32;
						if (num != 0 && (this.b & num) == 0)
						{
							this.b |= num;
							return true;
						}
						return false;
					}
				}
				else if (bit < 96)
				{
					int num = 1 << bit - 64;
					if (num != 0 && (this.c & num) == 0)
					{
						this.c |= num;
						return true;
					}
					return false;
				}
				else
				{
					int num = 1 << bit - 96;
					if (num != 0 && (this.d & num) == 0)
					{
						this.d |= num;
						return true;
					}
					return false;
				}
			}
			else if (bit < 192)
			{
				if (bit < 160)
				{
					int num = 1 << bit - 128;
					if (num != 0 && (this.e & num) == 0)
					{
						this.e |= num;
						return true;
					}
					return false;
				}
				else
				{
					int num = 1 << bit - 160;
					if (num != 0 && (this.f & num) == 0)
					{
						this.f |= num;
						return true;
					}
					return false;
				}
			}
			else if (bit < 224)
			{
				int num = 1 << bit - 192;
				if (num != 0 && (this.g & num) == 0)
				{
					this.g |= num;
					return true;
				}
				return false;
			}
			else
			{
				int num = 1 << bit - 224;
				if (num != 0 && (this.h & num) == 0)
				{
					this.h |= num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000BB420 File Offset: 0x000B9620
		public bool Off(int bit)
		{
			if (bit < 128)
			{
				if (bit < 64)
				{
					if (bit < 32)
					{
						int num = 1 << bit;
						if (num != 0 && (this.a & num) == num)
						{
							this.a &= ~num;
							return true;
						}
						return false;
					}
					else
					{
						int num = 1 << bit - 32;
						if (num != 0 && (this.b & num) == num)
						{
							this.b &= ~num;
							return true;
						}
						return false;
					}
				}
				else if (bit < 96)
				{
					int num = 1 << bit - 64;
					if (num != 0 && (this.c & num) == num)
					{
						this.c &= ~num;
						return true;
					}
					return false;
				}
				else
				{
					int num = 1 << bit - 96;
					if (num != 0 && (this.d & num) == num)
					{
						this.d &= ~num;
						return true;
					}
					return false;
				}
			}
			else if (bit < 192)
			{
				if (bit < 160)
				{
					int num = 1 << bit - 128;
					if (num != 0 && (this.e & num) == num)
					{
						this.e &= ~num;
						return true;
					}
					return false;
				}
				else
				{
					int num = 1 << bit - 160;
					if (num != 0 && (this.f & num) == num)
					{
						this.f &= ~num;
						return true;
					}
					return false;
				}
			}
			else if (bit < 224)
			{
				int num = 1 << bit - 192;
				if (num != 0 && (this.g & num) == num)
				{
					this.g &= ~num;
					return true;
				}
				return false;
			}
			else
			{
				int num = 1 << bit - 224;
				if (num != 0 && (this.h & num) == num)
				{
					this.h &= ~num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000BB604 File Offset: 0x000B9804
		public int CountOnBits()
		{
			int num = 0;
			if (this.a != 0)
			{
				uint num2 = (uint)this.a;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.b != 0)
			{
				uint num2 = (uint)this.b;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.c != 0)
			{
				uint num2 = (uint)this.c;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.d != 0)
			{
				uint num2 = (uint)this.d;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.e != 0)
			{
				uint num2 = (uint)this.e;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.f != 0)
			{
				uint num2 = (uint)this.f;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.g != 0)
			{
				uint num2 = (uint)this.g;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			if (this.h != 0)
			{
				uint num2 = (uint)this.h;
				while (num2 != 0u)
				{
					num2 &= num2 - 1u;
					num++;
				}
			}
			return num;
		}

		// Token: 0x04001A21 RID: 6689
		public int a;

		// Token: 0x04001A22 RID: 6690
		public int b;

		// Token: 0x04001A23 RID: 6691
		public int c;

		// Token: 0x04001A24 RID: 6692
		public int d;

		// Token: 0x04001A25 RID: 6693
		public int e;

		// Token: 0x04001A26 RID: 6694
		public int f;

		// Token: 0x04001A27 RID: 6695
		public int g;

		// Token: 0x04001A28 RID: 6696
		public int h;
	}

	// Token: 0x020005E6 RID: 1510
	public struct OccupiedIterator : IDisposable
	{
		// Token: 0x060030B2 RID: 12466 RVA: 0x000BB74C File Offset: 0x000B994C
		public OccupiedIterator(global::Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.OccupiedEnumerator;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000BB760 File Offset: 0x000B9960
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000BB770 File Offset: 0x000B9970
		public global::IInventoryItem item
		{
			get
			{
				return this.baseEnumerator.Current.iface;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000BB784 File Offset: 0x000B9984
		internal global::InventoryItem inventoryItem
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000BB794 File Offset: 0x000B9994
		public int slot
		{
			get
			{
				return this.baseEnumerator.Slot;
			}
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000BB7A4 File Offset: 0x000B99A4
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000BB7B4 File Offset: 0x000B99B4
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000BB7C4 File Offset: 0x000B99C4
		internal bool Next(out global::InventoryItem item, out int slot)
		{
			if (this.Next())
			{
				slot = this.baseEnumerator.Slot;
				item = this.baseEnumerator.Current;
				return true;
			}
			slot = -1;
			item = null;
			return false;
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000BB800 File Offset: 0x000B9A00
		internal bool Next(int datablockUniqueID, out global::InventoryItem item, out int slot)
		{
			while (this.Next(out item, out slot))
			{
				if (item.datablockUniqueID == datablockUniqueID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000BB830 File Offset: 0x000B9A30
		internal bool Next(global::ItemDataBlock datablock, out global::InventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000BB840 File Offset: 0x000B9A40
		public bool Next(out global::IInventoryItem item, out int slot)
		{
			global::InventoryItem inventoryItem;
			if (this.Next(out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000BB86C File Offset: 0x000B9A6C
		public bool Next(int datablockUniqueID, out global::IInventoryItem item, out int slot)
		{
			global::InventoryItem inventoryItem;
			if (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000BB898 File Offset: 0x000B9A98
		internal bool Next(global::ItemDataBlock datablock, out global::IInventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000BB8A8 File Offset: 0x000B9AA8
		public bool Next<TItemInterface>(out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			global::IInventoryItem inventoryItem;
			while (this.Next(out inventoryItem, out slot))
			{
				if (inventoryItem is TItemInterface)
				{
					item = (TItemInterface)((object)this.inventoryItem.iface);
					return true;
				}
			}
			item = (TItemInterface)((object)null);
			return false;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000BB900 File Offset: 0x000B9B00
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			global::IInventoryItem inventoryItem;
			while (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				if (inventoryItem is TItemInterface)
				{
					item = (TItemInterface)((object)this.inventoryItem.iface);
					return true;
				}
			}
			item = (TItemInterface)((object)null);
			return false;
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000BB958 File Offset: 0x000B9B58
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000BB968 File Offset: 0x000B9B68
		public bool Next(out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000BB980 File Offset: 0x000B9B80
		public bool Next(int datablockUniqueID, out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000BB998 File Offset: 0x000B9B98
		public bool Next(global::ItemDataBlock datablock, out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(datablock.uniqueID, out inventoryItem, out slot);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000BB9B4 File Offset: 0x000B9BB4
		public bool Next<TItemInterface>(out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000BB9CC File Offset: 0x000B9BCC
		public bool Next<TItemInterface>(int datablockUniqueID, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000BB9E4 File Offset: 0x000B9BE4
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(datablock.uniqueID, out titemInterface, out slot);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000BBA00 File Offset: 0x000B9C00
		internal bool Next(out global::InventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000BBA18 File Offset: 0x000B9C18
		internal bool Next(int datablockUniqueID, out global::InventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000BBA30 File Offset: 0x000B9C30
		internal bool Next(global::ItemDataBlock datablock, out global::InventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000BBA4C File Offset: 0x000B9C4C
		public bool Next(out global::IInventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000BBA64 File Offset: 0x000B9C64
		public bool Next(int datablockUniqueID, out global::IInventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000BBA7C File Offset: 0x000B9C7C
		internal bool Next(global::ItemDataBlock datablock, out global::IInventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000BBA98 File Offset: 0x000B9C98
		public bool Next<TItemInterface>(out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(out item, out num);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000BBAB0 File Offset: 0x000B9CB0
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000BBAC8 File Offset: 0x000B9CC8
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out num);
		}

		// Token: 0x04001A29 RID: 6697
		private global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator baseEnumerator;
	}

	// Token: 0x020005E7 RID: 1511
	public struct OccupiedReverseIterator : IDisposable
	{
		// Token: 0x060030D1 RID: 12497 RVA: 0x000BBAE4 File Offset: 0x000B9CE4
		public OccupiedReverseIterator(global::Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.OccupiedReverseEnumerator;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000BBAF8 File Offset: 0x000B9CF8
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000BBB08 File Offset: 0x000B9D08
		public global::IInventoryItem item
		{
			get
			{
				return this.baseEnumerator.Current.iface;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000BBB1C File Offset: 0x000B9D1C
		internal global::InventoryItem inventoryItem
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000BBB2C File Offset: 0x000B9D2C
		public int slot
		{
			get
			{
				return this.baseEnumerator.Slot;
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000BBB3C File Offset: 0x000B9D3C
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000BBB4C File Offset: 0x000B9D4C
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BBB5C File Offset: 0x000B9D5C
		internal bool Next(out global::InventoryItem item, out int slot)
		{
			if (this.Next())
			{
				slot = this.baseEnumerator.Slot;
				item = this.baseEnumerator.Current;
				return true;
			}
			slot = -1;
			item = null;
			return false;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BBB98 File Offset: 0x000B9D98
		internal bool Next(int datablockUniqueID, out global::InventoryItem item, out int slot)
		{
			while (this.Next(out item, out slot))
			{
				if (item.datablockUniqueID == datablockUniqueID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BBBC8 File Offset: 0x000B9DC8
		internal bool Next(global::ItemDataBlock datablock, out global::InventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BBBD8 File Offset: 0x000B9DD8
		public bool Next(out global::IInventoryItem item, out int slot)
		{
			global::InventoryItem inventoryItem;
			if (this.Next(out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BBC04 File Offset: 0x000B9E04
		public bool Next(int datablockUniqueID, out global::IInventoryItem item, out int slot)
		{
			global::InventoryItem inventoryItem;
			if (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BBC30 File Offset: 0x000B9E30
		internal bool Next(global::ItemDataBlock datablock, out global::IInventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000BBC40 File Offset: 0x000B9E40
		public bool Next<TItemInterface>(out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			global::IInventoryItem inventoryItem;
			while (this.Next(out inventoryItem, out slot))
			{
				if (inventoryItem is TItemInterface)
				{
					item = (TItemInterface)((object)this.inventoryItem.iface);
					return true;
				}
			}
			item = (TItemInterface)((object)null);
			return false;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BBC98 File Offset: 0x000B9E98
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			global::IInventoryItem inventoryItem;
			while (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				if (inventoryItem is TItemInterface)
				{
					item = (TItemInterface)((object)this.inventoryItem.iface);
					return true;
				}
			}
			item = (TItemInterface)((object)null);
			return false;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BBCF0 File Offset: 0x000B9EF0
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out TItemInterface item, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BBD00 File Offset: 0x000B9F00
		public bool Next(out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BBD18 File Offset: 0x000B9F18
		public bool Next(int datablockUniqueID, out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BBD30 File Offset: 0x000B9F30
		public bool Next(global::ItemDataBlock datablock, out int slot)
		{
			global::InventoryItem inventoryItem;
			return this.Next(datablock.uniqueID, out inventoryItem, out slot);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BBD4C File Offset: 0x000B9F4C
		public bool Next<TItemInterface>(out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BBD64 File Offset: 0x000B9F64
		public bool Next<TItemInterface>(int datablockUniqueID, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BBD7C File Offset: 0x000B9F7C
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out int slot) where TItemInterface : class, global::IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(datablock.uniqueID, out titemInterface, out slot);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000BBD98 File Offset: 0x000B9F98
		internal bool Next(out global::InventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000BBDB0 File Offset: 0x000B9FB0
		internal bool Next(int datablockUniqueID, out global::InventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BBDC8 File Offset: 0x000B9FC8
		internal bool Next(global::ItemDataBlock datablock, out global::InventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BBDE4 File Offset: 0x000B9FE4
		public bool Next(out global::IInventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BBDFC File Offset: 0x000B9FFC
		public bool Next(int datablockUniqueID, out global::IInventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BBE14 File Offset: 0x000BA014
		internal bool Next(global::ItemDataBlock datablock, out global::IInventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BBE30 File Offset: 0x000BA030
		public bool Next<TItemInterface>(out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(out item, out num);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000BBE48 File Offset: 0x000BA048
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablockUniqueID, out item, out num);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000BBE60 File Offset: 0x000BA060
		public bool Next<TItemInterface>(global::ItemDataBlock datablock, out TItemInterface item) where TItemInterface : class, global::IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out num);
		}

		// Token: 0x04001A2A RID: 6698
		private global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.ReverseEnumerator baseEnumerator;
	}

	// Token: 0x020005E8 RID: 1512
	private static class Payload
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x000BBE7C File Offset: 0x000BA07C
		private static bool StackUsesSlot(ref global::Inventory.Payload.StackArguments args, ref global::Inventory.Payload.StackWork work)
		{
			if (work.instance.datablockUniqueID != args.datablockUID)
			{
				return false;
			}
			int useCount = args.useCount;
			args.useCount -= work.instance.AddUses(args.useCount);
			if (useCount != args.useCount)
			{
				args.collection.MarkDirty(work.slot);
				if (args.useCount == 0)
				{
					return true;
				}
				if (!work.gotFirstUsage)
				{
					work.firstUsage = work.instance;
					work.gotFirstUsage = true;
				}
			}
			return false;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000BBF10 File Offset: 0x000BA110
		private static global::Inventory.Payload.StackResult StackUses(ref global::Inventory.Payload.StackArguments args, ref global::Inventory.Payload.RangeArray.Holder ranges, out global::InventoryItem item)
		{
			if (ranges.Count == 0)
			{
				item = null;
				return global::Inventory.Payload.StackResult.NoRange;
			}
			if ((byte)(args.prefFlags & global::Inventory.Slot.PreferenceFlags.Stack) != 8)
			{
				item = null;
				return global::Inventory.Payload.StackResult.NoneNotMarked;
			}
			if (args.splittable)
			{
				global::Inventory.Payload.StackWork stackWork;
				stackWork.gotFirstUsage = false;
				stackWork.firstUsage = null;
				int useCount = args.useCount;
				bool flag = false;
				int num = -1;
				global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator enumerator = default(global::Inventory.Collection<global::InventoryItem>.OccupiedCollection.Enumerator);
				try
				{
					for (int i = 0; i < ranges.Count; i++)
					{
						if (ranges.Range[i].Count == 1)
						{
							if (args.collection.Get(stackWork.slot = ranges.Range[i].Start, out stackWork.instance) && global::Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
							{
								item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
								return global::Inventory.Payload.StackResult.Complete;
							}
						}
						else
						{
							if (flag)
							{
								if (ranges.Range[i].Start < num)
								{
									enumerator.Reset();
								}
								else if (ranges.Range[i].Start == num)
								{
									stackWork.slot = num;
									stackWork.instance = enumerator.Current;
									if (global::Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
									{
										item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
										return global::Inventory.Payload.StackResult.Complete;
									}
								}
							}
							else
							{
								enumerator = args.collection.OccupiedEnumerator;
								flag = true;
							}
							bool flag2;
							while (flag2 = enumerator.MoveNext())
							{
								num = enumerator.Slot;
								if (ranges.Range[i].Start <= num)
								{
									if (num - ranges.Range[i].Start >= ranges.Range[i].Count)
									{
										break;
									}
									stackWork.slot = num;
									stackWork.instance = enumerator.Current;
									if (global::Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
									{
										item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
										return global::Inventory.Payload.StackResult.Complete;
									}
								}
							}
							if (!flag2)
							{
								num = 257;
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						enumerator.Dispose();
					}
				}
				if (stackWork.gotFirstUsage)
				{
					item = stackWork.firstUsage;
					return (args.useCount >= useCount) ? global::Inventory.Payload.StackResult.None_FoundFull : global::Inventory.Payload.StackResult.Partial;
				}
				item = null;
				return global::Inventory.Payload.StackResult.None;
			}
			item = null;
			return global::Inventory.Payload.StackResult.NoneUnsplittable;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000BC1E4 File Offset: 0x000BA3E4
		private static bool AssignItem(ref global::Inventory.Payload.Assignment args)
		{
			if (args.inventory.CheckSlotFlagsAgainstSlot(args.datablock._itemFlags, args.slot) && args.item.CanMoveToSlot(args.inventory, args.slot))
			{
				args.attemptsMade++;
				if (args.collection.Occupy(args.slot, args.item))
				{
					if (!args.fresh && args.item.inventory)
					{
						args.item.inventory.RemoveItem(args.item.slot);
					}
					args.item.SetUses(args.uses);
					args.item.OnAddedTo(args.inventory, args.slot);
					args.inventory.ItemAdded(args.slot, args.item.iface);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000BC2DC File Offset: 0x000BA4DC
		private static bool AssignItemInsideRanges(ref global::Inventory.Collection<global::InventoryItem>.VacantCollection.Enumerator enumerator, ref global::Inventory.Payload.RangeArray.Holder ranges, ref global::Inventory.Payload.Assignment args)
		{
			int i = 0;
			while (i < ranges.Count)
			{
				if (ranges.Range[i].Count != 1)
				{
					goto IL_5D;
				}
				args.slot = ranges.Range[i].Start;
				if (!args.collection.IsOccupied(args.slot))
				{
					if (global::Inventory.Payload.AssignItem(ref args))
					{
						return true;
					}
					goto IL_5D;
				}
				IL_DB:
				i++;
				continue;
				IL_5D:
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					int slot = enumerator.Current;
					args.slot = slot;
					sbyte b = ranges.Range[i].ContainEx(args.slot);
					bool flag;
					switch (b + 1)
					{
					case 0:
						continue;
					case 1:
						goto IL_AA;
					case 2:
						flag = true;
						break;
					default:
						goto IL_AA;
					}
					IL_B8:
					if (flag)
					{
						break;
					}
					if (global::Inventory.Payload.AssignItem(ref args))
					{
						return true;
					}
					continue;
					IL_AA:
					flag = false;
					goto IL_B8;
				}
				goto IL_DB;
			}
			return false;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000BC3D8 File Offset: 0x000BA5D8
		public static global::Inventory.Payload.Result AddItem(global::Inventory inventory, ref global::Inventory.Addition addition, global::Inventory.Payload.Opt options, global::InventoryItem reuseItem)
		{
			global::Inventory.Payload.Result result;
			if ((byte)(options & (global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.DoNotAssign)) == 3 || (byte)(options & (global::Inventory.Payload.Opt.IgnoreSlotOffset | global::Inventory.Payload.Opt.RestrictToOffset)) == 12)
			{
				result.item = null;
				result.flags = global::Inventory.Payload.Result.Flags.OptionsResultedInNoOp;
				result.usesRemaining = 0;
			}
			else
			{
				global::ItemDataBlock itemDataBlock = addition.ItemDataBlock;
				if (!itemDataBlock)
				{
					result.item = null;
					result.flags = global::Inventory.Payload.Result.Flags.NoItemDatablock;
					result.usesRemaining = 0;
					return result;
				}
				global::Inventory.Slot.KindFlags kindFlags = addition.SlotPreference.PrimaryKindFlags;
				global::Inventory.Slot.KindFlags kindFlags2 = addition.SlotPreference.SecondaryKindFlags;
				global::Inventory.Slot.Range explicitSlot;
				if ((byte)(options & global::Inventory.Payload.Opt.IgnoreSlotOffset) == 4)
				{
					explicitSlot = default(global::Inventory.Slot.Range);
				}
				else
				{
					explicitSlot = global::Inventory.Payload.RangeArray.CalculateExplicitSlotPosition(inventory, ref addition.SlotPreference);
				}
				bool flag = (byte)(options & global::Inventory.Payload.Opt.RestrictToOffset) == 8;
				bool any = explicitSlot.Any;
				if (flag && !any)
				{
					result.item = null;
					result.flags = global::Inventory.Payload.Result.Flags.MissingRequiredOffset;
					result.usesRemaining = 0;
					return result;
				}
				if (flag)
				{
					global::Inventory.Payload.RangeArray.FillTemporaryRanges(ref global::Inventory.Payload.RangeArray.Primary, inventory, (global::Inventory.Slot.KindFlags)0, explicitSlot, true);
					global::Inventory.Payload.RangeArray.FillTemporaryRanges(ref global::Inventory.Payload.RangeArray.Secondary, inventory, (global::Inventory.Slot.KindFlags)0, explicitSlot, false);
				}
				else
				{
					global::Inventory.Payload.RangeArray.FillTemporaryRanges(ref global::Inventory.Payload.RangeArray.Primary, inventory, kindFlags, explicitSlot, true);
					global::Inventory.Payload.RangeArray.FillTemporaryRanges(ref global::Inventory.Payload.RangeArray.Secondary, inventory, kindFlags2, explicitSlot, false);
				}
				int num;
				if (global::Inventory.Payload.RangeArray.Primary.Count == 0)
				{
					kindFlags = (global::Inventory.Slot.KindFlags)0;
					if (global::Inventory.Payload.RangeArray.Secondary.Count == 0)
					{
						kindFlags2 = (global::Inventory.Slot.KindFlags)0;
						num = 0;
					}
					else
					{
						num = global::Inventory.Payload.RangeArray.Secondary.Count;
					}
				}
				else if (global::Inventory.Payload.RangeArray.Secondary.Count == 0)
				{
					kindFlags2 = (global::Inventory.Slot.KindFlags)0;
					num = global::Inventory.Payload.RangeArray.Primary.Count;
				}
				else
				{
					num = global::Inventory.Payload.RangeArray.Primary.Count + global::Inventory.Payload.RangeArray.Secondary.Count;
				}
				if (num == 0 || (!any && ((byte)(kindFlags | kindFlags2) & 7) == 0))
				{
					result.item = null;
					result.flags = global::Inventory.Payload.Result.Flags.NoSlotRanges;
					result.usesRemaining = 0;
				}
				else
				{
					int maxUses = itemDataBlock._maxUses;
					bool flag2 = (byte)(options & global::Inventory.Payload.Opt.ReuseItem) == 16;
					if (flag2 && (object.ReferenceEquals(reuseItem, null) || (itemDataBlock.untransferable && reuseItem.inventory != inventory)))
					{
						result.flags = global::Inventory.Payload.Result.Flags.FailedToReuse;
						result.item = null;
						result.usesRemaining = 0;
					}
					else
					{
						global::Inventory.Collection<global::InventoryItem> collection = inventory.collection;
						result.usesRemaining = ((!flag2) ? addition.UsesQuantity.CalculateCount(itemDataBlock) : reuseItem.uses);
						global::InventoryItem item;
						global::Inventory.Payload.StackResult stackResult2;
						if ((byte)(options & global::Inventory.Payload.Opt.DoNotStack) != 1 && (byte)(addition.SlotPreference.Flags & global::Inventory.Slot.PreferenceFlags.Stack) == 8)
						{
							global::Inventory.Payload.StackArguments stackArguments;
							stackArguments.collection = collection;
							stackArguments.datablockUID = itemDataBlock.uniqueID;
							stackArguments.splittable = itemDataBlock.IsSplittable();
							stackArguments.useCount = result.usesRemaining;
							stackArguments.prefFlags = addition.SlotPreference.Flags;
							global::InventoryItem inventoryItem;
							global::Inventory.Payload.StackResult stackResult = global::Inventory.Payload.StackUses(ref stackArguments, ref global::Inventory.Payload.RangeArray.Primary, out inventoryItem);
							if (stackResult == global::Inventory.Payload.StackResult.NoneUnsplittable || stackResult == global::Inventory.Payload.StackResult.Complete)
							{
								global::InventoryItem inventoryItem2 = item = inventoryItem;
								stackResult2 = stackResult;
							}
							else
							{
								global::InventoryItem inventoryItem2;
								global::Inventory.Payload.StackResult stackResult3 = global::Inventory.Payload.StackUses(ref stackArguments, ref global::Inventory.Payload.RangeArray.Secondary, out inventoryItem2);
								if (stackResult > stackResult3)
								{
									item = (inventoryItem ?? inventoryItem2);
									stackResult2 = stackResult;
								}
								else
								{
									item = (inventoryItem ?? inventoryItem2);
									stackResult2 = stackResult3;
								}
							}
							result.usesRemaining = stackArguments.useCount;
						}
						else
						{
							item = null;
							stackResult2 = global::Inventory.Payload.StackResult.NoneNotMarked;
						}
						if (stackResult2 == global::Inventory.Payload.StackResult.Complete)
						{
							result.item = item;
							result.flags = (global::Inventory.Payload.Result.Flags.Complete | global::Inventory.Payload.Result.Flags.Stacked);
						}
						else
						{
							if (stackResult2 == global::Inventory.Payload.StackResult.Partial)
							{
								result.item = item;
								result.flags = global::Inventory.Payload.Result.Flags.Stacked;
							}
							else
							{
								result.flags = global::Inventory.Payload.Result.Flags.OptionsResultedInNoOp;
							}
							if ((byte)(options & global::Inventory.Payload.Opt.DoNotAssign) != 2)
							{
								if (collection.HasNoVacancy)
								{
									result.item = item;
									result.flags |= global::Inventory.Payload.Result.Flags.NoVacancy;
								}
								else
								{
									global::Inventory.Payload.Assignment assignment;
									assignment.inventory = inventory;
									assignment.collection = collection;
									assignment.fresh = !flag2;
									assignment.item = ((!assignment.fresh) ? reuseItem : (itemDataBlock.CreateItem() as global::InventoryItem));
									assignment.uses = result.usesRemaining;
									assignment.datablock = itemDataBlock;
									if (!flag2 && object.ReferenceEquals(assignment.item, null))
									{
										result.item = item;
										result.flags |= ((!assignment.fresh) ? global::Inventory.Payload.Result.Flags.FailedToReuse : global::Inventory.Payload.Result.Flags.FailedToCreate);
									}
									else
									{
										assignment.slot = -1;
										assignment.attemptsMade = 0;
										global::Inventory.Collection<global::InventoryItem>.VacantCollection.Enumerator vacantEnumerator = collection.VacantEnumerator;
										bool flag3;
										try
										{
											flag3 = (global::Inventory.Payload.AssignItemInsideRanges(ref vacantEnumerator, ref global::Inventory.Payload.RangeArray.Primary, ref assignment) || global::Inventory.Payload.AssignItemInsideRanges(ref vacantEnumerator, ref global::Inventory.Payload.RangeArray.Secondary, ref assignment));
										}
										finally
										{
											vacantEnumerator.Dispose();
										}
										if (flag3)
										{
											result.flags |= (global::Inventory.Payload.Result.Flags.Complete | global::Inventory.Payload.Result.Flags.AssignedInstance);
											result.item = assignment.item;
											result.usesRemaining -= result.item.uses;
										}
										else if (assignment.attemptsMade > 0)
										{
											result.flags |= global::Inventory.Payload.Result.Flags.NoVacancy;
											result.item = item;
										}
										else
										{
											result.flags |= global::Inventory.Payload.Result.Flags.NoSlotRanges;
											result.item = item;
										}
									}
								}
							}
							else
							{
								result.item = item;
								if (result.flags == global::Inventory.Payload.Result.Flags.OptionsResultedInNoOp)
								{
									result.flags = global::Inventory.Payload.Result.Flags.MissingRequiredOffset;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04001A2B RID: 6699
		private const global::Inventory.Payload.Opt NoOp1_Mask = global::Inventory.Payload.Opt.DoNotStack | global::Inventory.Payload.Opt.DoNotAssign;

		// Token: 0x04001A2C RID: 6700
		private const global::Inventory.Payload.Opt NoOp2_Mask = global::Inventory.Payload.Opt.IgnoreSlotOffset | global::Inventory.Payload.Opt.RestrictToOffset;

		// Token: 0x020005E9 RID: 1513
		public struct Result
		{
			// Token: 0x04001A2D RID: 6701
			public global::InventoryItem item;

			// Token: 0x04001A2E RID: 6702
			public global::Inventory.Payload.Result.Flags flags;

			// Token: 0x04001A2F RID: 6703
			public int usesRemaining;

			// Token: 0x020005EA RID: 1514
			[Flags]
			public enum Flags : byte
			{
				// Token: 0x04001A31 RID: 6705
				Complete = 128,
				// Token: 0x04001A32 RID: 6706
				AssignedInstance = 64,
				// Token: 0x04001A33 RID: 6707
				Stacked = 32,
				// Token: 0x04001A34 RID: 6708
				NoVacancy = 16,
				// Token: 0x04001A35 RID: 6709
				DidNotCreate = 6,
				// Token: 0x04001A36 RID: 6710
				FailedToReuse = 5,
				// Token: 0x04001A37 RID: 6711
				FailedToCreate = 4,
				// Token: 0x04001A38 RID: 6712
				NoSlotRanges = 3,
				// Token: 0x04001A39 RID: 6713
				MissingRequiredOffset = 2,
				// Token: 0x04001A3A RID: 6714
				NoItemDatablock = 1,
				// Token: 0x04001A3B RID: 6715
				OptionsResultedInNoOp = 0
			}
		}

		// Token: 0x020005EB RID: 1515
		[Flags]
		public enum Opt : byte
		{
			// Token: 0x04001A3D RID: 6717
			DoNotStack = 1,
			// Token: 0x04001A3E RID: 6718
			DoNotAssign = 2,
			// Token: 0x04001A3F RID: 6719
			IgnoreSlotOffset = 4,
			// Token: 0x04001A40 RID: 6720
			RestrictToOffset = 8,
			// Token: 0x04001A41 RID: 6721
			ReuseItem = 16,
			// Token: 0x04001A42 RID: 6722
			AllowStackedItemsToBeReturned = 32
		}

		// Token: 0x020005EC RID: 1516
		private enum StackResult : byte
		{
			// Token: 0x04001A44 RID: 6724
			None,
			// Token: 0x04001A45 RID: 6725
			NoneNotMarked,
			// Token: 0x04001A46 RID: 6726
			NoneUnsplittable,
			// Token: 0x04001A47 RID: 6727
			NoRange,
			// Token: 0x04001A48 RID: 6728
			None_FoundFull,
			// Token: 0x04001A49 RID: 6729
			Partial,
			// Token: 0x04001A4A RID: 6730
			Complete
		}

		// Token: 0x020005ED RID: 1517
		private struct Assignment
		{
			// Token: 0x04001A4B RID: 6731
			public global::Inventory.Collection<global::InventoryItem> collection;

			// Token: 0x04001A4C RID: 6732
			public global::Inventory inventory;

			// Token: 0x04001A4D RID: 6733
			public global::InventoryItem item;

			// Token: 0x04001A4E RID: 6734
			public global::ItemDataBlock datablock;

			// Token: 0x04001A4F RID: 6735
			public int slot;

			// Token: 0x04001A50 RID: 6736
			public int uses;

			// Token: 0x04001A51 RID: 6737
			public bool fresh;

			// Token: 0x04001A52 RID: 6738
			public int attemptsMade;
		}

		// Token: 0x020005EE RID: 1518
		private static class RangeArray
		{
			// Token: 0x060030F6 RID: 12534 RVA: 0x000BC994 File Offset: 0x000BAB94
			public static void FillTemporaryRanges(ref global::Inventory.Payload.RangeArray.Holder temp, global::Inventory inventory, global::Inventory.Slot.KindFlags kindFlags, global::Inventory.Slot.Range explicitSlot, bool insertExplicitSlot)
			{
				kindFlags &= (global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor);
				temp.Count = 0;
				int num = 0;
				int num2 = 0;
				int gougeIndex;
				if (explicitSlot.Any)
				{
					if (insertExplicitSlot)
					{
						temp.Range[temp.Count++] = explicitSlot;
					}
					gougeIndex = explicitSlot.Start;
				}
				else
				{
					gougeIndex = -1;
				}
				for (global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default; kind < (global::Inventory.Slot.Kind)3; kind += 1)
				{
					global::Inventory.Slot.KindFlags flag = (global::Inventory.Slot.KindFlags)(1 << (int)kind);
					if (global::Inventory.Payload.RangeArray.CheckSlotKindFlag(inventory, kindFlags, flag, kind, ref num, ref num2))
					{
						temp.Insert(ref num, ref num2, gougeIndex);
					}
				}
			}

			// Token: 0x060030F7 RID: 12535 RVA: 0x000BCA34 File Offset: 0x000BAC34
			public static global::Inventory.Slot.Range CalculateExplicitSlotPosition(global::Inventory inventory, ref global::Inventory.Slot.Preference pref)
			{
				global::Inventory.Slot.Offset offset = pref.Offset;
				if (!offset.Specified)
				{
					return default(global::Inventory.Slot.Range);
				}
				global::Inventory.Slot.Range range;
				if (offset.HasOffsetOfKind)
				{
					if (!inventory.slotRanges.TryGetValue(offset.OffsetOfKind, out range))
					{
						return default(global::Inventory.Slot.Range);
					}
				}
				else
				{
					range = new global::Inventory.Slot.Range(0, inventory.slotCount);
				}
				int slotOffset = offset.SlotOffset;
				if (range.Count > slotOffset)
				{
					return new global::Inventory.Slot.Range(range.Start + slotOffset, 1);
				}
				return default(global::Inventory.Slot.Range);
			}

			// Token: 0x060030F8 RID: 12536 RVA: 0x000BCAD0 File Offset: 0x000BACD0
			private static bool CheckSlotKindFlag(global::Inventory inventory, global::Inventory.Slot.KindFlags flags, global::Inventory.Slot.KindFlags flag, global::Inventory.Slot.Kind kind, ref int start, ref int count)
			{
				global::Inventory.Slot.Range range;
				if ((flags & flag) == flag && inventory.slotRanges.TryGetValue(kind, out range) && range.Any)
				{
					if (range.End <= inventory.slotCount)
					{
						start = range.Start;
						count = range.Count;
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001A53 RID: 6739
			private const int ArrayElementCount = 6;

			// Token: 0x04001A54 RID: 6740
			public static global::Inventory.Payload.RangeArray.Holder Primary = new global::Inventory.Payload.RangeArray.Holder(new global::Inventory.Slot.Range[6]);

			// Token: 0x04001A55 RID: 6741
			public static global::Inventory.Payload.RangeArray.Holder Secondary = new global::Inventory.Payload.RangeArray.Holder(new global::Inventory.Slot.Range[6]);

			// Token: 0x020005EF RID: 1519
			public struct Holder
			{
				// Token: 0x060030F9 RID: 12537 RVA: 0x000BCB34 File Offset: 0x000BAD34
				public Holder(global::Inventory.Slot.Range[] array)
				{
					this.Count = 0;
					this.Range = array;
				}

				// Token: 0x060030FA RID: 12538 RVA: 0x000BCB44 File Offset: 0x000BAD44
				public void Insert(ref int start, ref int count, int gougeIndex)
				{
					global::Inventory.Slot.Range range = new global::Inventory.Slot.Range(start, count);
					if (gougeIndex != -1)
					{
						global::Inventory.Slot.RangePair rangePair;
						switch (range.Gouge(gougeIndex, out rangePair))
						{
						case 1:
							this.Range[this.Count++] = rangePair.A;
							break;
						case 2:
							this.Range[this.Count++] = rangePair.A;
							this.Range[this.Count++] = rangePair.B;
							break;
						}
					}
					else
					{
						this.Range[this.Count++] = range;
					}
					start = (count = 0);
				}

				// Token: 0x04001A56 RID: 6742
				public int Count;

				// Token: 0x04001A57 RID: 6743
				public readonly global::Inventory.Slot.Range[] Range;
			}
		}

		// Token: 0x020005F0 RID: 1520
		private struct StackArguments
		{
			// Token: 0x04001A58 RID: 6744
			public global::Inventory.Collection<global::InventoryItem> collection;

			// Token: 0x04001A59 RID: 6745
			public global::Inventory.Slot.PreferenceFlags prefFlags;

			// Token: 0x04001A5A RID: 6746
			public int useCount;

			// Token: 0x04001A5B RID: 6747
			public int datablockUID;

			// Token: 0x04001A5C RID: 6748
			public bool splittable;
		}

		// Token: 0x020005F1 RID: 1521
		private struct StackWork
		{
			// Token: 0x04001A5D RID: 6749
			public bool gotFirstUsage;

			// Token: 0x04001A5E RID: 6750
			public global::InventoryItem firstUsage;

			// Token: 0x04001A5F RID: 6751
			public int slot;

			// Token: 0x04001A60 RID: 6752
			public global::InventoryItem instance;
		}
	}

	// Token: 0x020005F2 RID: 1522
	private class Report
	{
		// Token: 0x060030FD RID: 12541 RVA: 0x000BCC50 File Offset: 0x000BAE50
		private static global::Inventory.Report Create()
		{
			global::Inventory.Report report;
			if (global::Inventory.Report.dumpSize > 0)
			{
				report = global::Inventory.Report.dump;
				if (--global::Inventory.Report.dumpSize == 0)
				{
					global::Inventory.Report.dump = null;
				}
				else
				{
					global::Inventory.Report.dump = report.dumpNext;
				}
				report.dumpNext = null;
				report.Disposed = false;
				report.amount = 0;
			}
			else
			{
				report = new global::Inventory.Report();
			}
			return report;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000BCCB8 File Offset: 0x000BAEB8
		public static void Begin()
		{
			if (global::Inventory.Report.begun)
			{
				throw new InvalidOperationException();
			}
			global::Inventory.Report.begun = true;
			global::Inventory.Report.totalItemCount = 0;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000BCCD8 File Offset: 0x000BAED8
		public static void Take(global::InventoryItem item)
		{
			int uses = item.uses;
			int datablockUniqueID = item.datablockUniqueID;
			global::Inventory.Report report;
			if (global::Inventory.Report.dict.TryGetValue(datablockUniqueID, out report))
			{
				global::Inventory.Report report2 = report.first;
				if (report.splittable)
				{
					int num = report2.amount + uses;
					if (num > item.maxUses)
					{
						global::Inventory.Report report3 = global::Inventory.Report.Create();
						report3.typeNext = report2;
						report3.amount = num - report.maxUses;
						report3.item = item;
						report2.amount = report.maxUses;
						report.first = report3;
						report.length++;
						global::Inventory.Report.totalItemCount++;
					}
					else
					{
						report.first.amount = num;
					}
				}
				else
				{
					global::Inventory.Report report4 = global::Inventory.Report.Create();
					report4.typeNext = report2;
					report4.amount = uses;
					report4.item = item;
					report.first = report4;
					report.length++;
					global::Inventory.Report.totalItemCount++;
				}
			}
			else
			{
				global::ItemDataBlock itemDataBlock = item.datablock;
				if (itemDataBlock.transferable)
				{
					global::Inventory.Report report5 = global::Inventory.Report.Create();
					report5.amount = uses;
					report5.splittable = itemDataBlock.IsSplittable();
					report5.first = report5;
					report5.length = 1;
					report5.datablock = itemDataBlock;
					report5.item = item;
					if (report5.splittable)
					{
						report5.maxUses = item.maxUses;
					}
					global::Inventory.Report.dict.Add(item.datablockUniqueID, report5);
					global::Inventory.Report.totalItemCount++;
				}
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000BCE6C File Offset: 0x000BB06C
		public static global::Inventory.Transfer[] Build(global::Inventory.Slot.KindFlags fallbackKindFlags)
		{
			if (!global::Inventory.Report.begun)
			{
				throw new InvalidOperationException();
			}
			global::Inventory.Transfer[] array = new global::Inventory.Transfer[global::Inventory.Report.totalItemCount];
			int slotNumber = 0;
			foreach (KeyValuePair<int, global::Inventory.Report> keyValuePair in global::Inventory.Report.dict)
			{
				global::Inventory.Report value = keyValuePair.Value;
				global::Inventory.Transfer transfer;
				transfer.addition.Ident = (global::Datablock.Ident)value.datablock;
				int num = value.length;
				value = value.first;
				bool flag = value.splittable;
				for (int i = 0; i < num; i++)
				{
					transfer.addition.SlotPreference = global::Inventory.Slot.Preference.Define(slotNumber, false, fallbackKindFlags);
					transfer.addition.UsesQuantity = global::Inventory.Uses.Quantity.Manual(value.amount);
					transfer.item = value.item;
					array[slotNumber++] = transfer;
					global::Inventory.Report report = value;
					value = value.typeNext;
					if (!report.Disposed)
					{
						report.Disposed = true;
						report.dumpNext = global::Inventory.Report.dump;
						report.first = (report.typeNext = null);
						report.datablock = null;
						report.item = null;
						global::Inventory.Report.dump = report;
						global::Inventory.Report.dumpSize++;
					}
				}
			}
			global::Inventory.Report.dict.Clear();
			global::Inventory.Report.begun = false;
			return array;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000BCFFC File Offset: 0x000BB1FC
		public static void Recover()
		{
			if (global::Inventory.Report.begun)
			{
				foreach (global::Inventory.Report report in global::Inventory.Report.dict.Values)
				{
					if (!report.Disposed)
					{
						report.Disposed = true;
						report.dumpNext = global::Inventory.Report.dump;
						report.first = (report.typeNext = null);
						report.datablock = null;
						report.item = null;
						global::Inventory.Report.dump = report;
						global::Inventory.Report.dumpSize++;
					}
				}
				global::Inventory.Report.dict.Clear();
			}
		}

		// Token: 0x04001A61 RID: 6753
		private int amount;

		// Token: 0x04001A62 RID: 6754
		private bool Disposed;

		// Token: 0x04001A63 RID: 6755
		private global::Inventory.Report dumpNext;

		// Token: 0x04001A64 RID: 6756
		private global::Inventory.Report typeNext;

		// Token: 0x04001A65 RID: 6757
		private global::Inventory.Report first;

		// Token: 0x04001A66 RID: 6758
		private global::ItemDataBlock datablock;

		// Token: 0x04001A67 RID: 6759
		private global::InventoryItem item;

		// Token: 0x04001A68 RID: 6760
		private bool splittable;

		// Token: 0x04001A69 RID: 6761
		private int length;

		// Token: 0x04001A6A RID: 6762
		private int maxUses;

		// Token: 0x04001A6B RID: 6763
		private static global::Inventory.Report dump;

		// Token: 0x04001A6C RID: 6764
		private static int dumpSize;

		// Token: 0x04001A6D RID: 6765
		private static readonly Dictionary<int, global::Inventory.Report> dict = new Dictionary<int, global::Inventory.Report>();

		// Token: 0x04001A6E RID: 6766
		private static bool begun;

		// Token: 0x04001A6F RID: 6767
		private static int totalItemCount;
	}

	// Token: 0x020005F3 RID: 1523
	public static class Slot
	{
		// Token: 0x04001A70 RID: 6768
		public const global::Inventory.Slot.Kind KindBegin = global::Inventory.Slot.Kind.Default;

		// Token: 0x04001A71 RID: 6769
		public const global::Inventory.Slot.Kind KindLast = global::Inventory.Slot.Kind.Armor;

		// Token: 0x04001A72 RID: 6770
		public const global::Inventory.Slot.Kind KindFirst = global::Inventory.Slot.Kind.Default;

		// Token: 0x04001A73 RID: 6771
		public const global::Inventory.Slot.Kind KindEnd = (global::Inventory.Slot.Kind)3;

		// Token: 0x04001A74 RID: 6772
		public const int KindCount = 3;

		// Token: 0x04001A75 RID: 6773
		private const global::Inventory.Slot.Kind HiddenKind_Explicit = (global::Inventory.Slot.Kind)4;

		// Token: 0x04001A76 RID: 6774
		private const global::Inventory.Slot.Kind HiddenKind_Null = (global::Inventory.Slot.Kind)5;

		// Token: 0x04001A77 RID: 6775
		public const int NumberOfKinds = 3;

		// Token: 0x04001A78 RID: 6776
		public const global::Inventory.Slot.KindFlags KindFlagsMask_Kind = global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor;

		// Token: 0x04001A79 RID: 6777
		private const int PrimaryShift = 4;

		// Token: 0x020005F4 RID: 1524
		public enum Kind : byte
		{
			// Token: 0x04001A7B RID: 6779
			Default,
			// Token: 0x04001A7C RID: 6780
			Belt,
			// Token: 0x04001A7D RID: 6781
			Armor
		}

		// Token: 0x020005F5 RID: 1525
		public struct KindDictionary<TValue> : IEnumerable, IDictionary<global::Inventory.Slot.Kind, TValue>, ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>, IEnumerable<KeyValuePair<global::Inventory.Slot.Kind, TValue>>
		{
			// Token: 0x06003102 RID: 12546 RVA: 0x000BD0C0 File Offset: 0x000BB2C0
			void IDictionary<global::Inventory.Slot.Kind, TValue>.Add(global::Inventory.Slot.Kind key, TValue value)
			{
				if (this.GetMember(key).Defined)
				{
					throw new ArgumentException("Key was already set to a value");
				}
				this.SetMember(key, new global::Inventory.Slot.KindDictionary<TValue>.Member(value));
				this.count += 1;
			}

			// Token: 0x17000A70 RID: 2672
			// (get) Token: 0x06003103 RID: 12547 RVA: 0x000BD108 File Offset: 0x000BB308
			ICollection<global::Inventory.Slot.Kind> IDictionary<global::Inventory.Slot.Kind, TValue>.Keys
			{
				get
				{
					global::Inventory.Slot.Kind[] array = new global::Inventory.Slot.Kind[(int)this.count];
					int num = 0;
					for (global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default; kind < (global::Inventory.Slot.Kind)3; kind += 1)
					{
						if (this.GetMember(kind).Defined)
						{
							array[num++] = kind;
						}
					}
					return array;
				}
			}

			// Token: 0x06003104 RID: 12548 RVA: 0x000BD154 File Offset: 0x000BB354
			void ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.Add(KeyValuePair<global::Inventory.Slot.Kind, TValue> item)
			{
				this[item.Key] = item.Value;
			}

			// Token: 0x17000A71 RID: 2673
			// (get) Token: 0x06003105 RID: 12549 RVA: 0x000BD16C File Offset: 0x000BB36C
			ICollection<TValue> IDictionary<global::Inventory.Slot.Kind, TValue>.Values
			{
				get
				{
					TValue[] array = new TValue[(int)this.count];
					int num = 0;
					for (global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default; kind < (global::Inventory.Slot.Kind)3; kind += 1)
					{
						global::Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
						if (member.Defined)
						{
							array[num++] = member.Value;
						}
					}
					return array;
				}
			}

			// Token: 0x06003106 RID: 12550 RVA: 0x000BD1C4 File Offset: 0x000BB3C4
			bool ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.Contains(KeyValuePair<global::Inventory.Slot.Kind, TValue> item)
			{
				bool result;
				try
				{
					global::Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(item.Key);
					if (!member.Defined)
					{
						result = false;
					}
					else
					{
						KeyValuePair<global::Inventory.Slot.Kind, TValue> keyValuePair = new KeyValuePair<global::Inventory.Slot.Kind, TValue>(item.Key, member.Value);
						result = object.Equals(keyValuePair, item);
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06003107 RID: 12551 RVA: 0x000BD250 File Offset: 0x000BB450
			void ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.CopyTo(KeyValuePair<global::Inventory.Slot.Kind, TValue>[] array, int arrayIndex)
			{
				for (global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default; kind < (global::Inventory.Slot.Kind)3; kind += 1)
				{
					global::Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
					if (member.Defined)
					{
						array[arrayIndex++] = new KeyValuePair<global::Inventory.Slot.Kind, TValue>(kind, member.Value);
					}
				}
			}

			// Token: 0x17000A72 RID: 2674
			// (get) Token: 0x06003108 RID: 12552 RVA: 0x000BD2A4 File Offset: 0x000BB4A4
			bool ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003109 RID: 12553 RVA: 0x000BD2A8 File Offset: 0x000BB4A8
			bool ICollection<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.Remove(KeyValuePair<global::Inventory.Slot.Kind, TValue> item)
			{
				bool result;
				try
				{
					global::Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(item.Key);
					if (!member.Defined)
					{
						result = false;
					}
					else
					{
						KeyValuePair<global::Inventory.Slot.Kind, TValue> keyValuePair = new KeyValuePair<global::Inventory.Slot.Kind, TValue>(item.Key, member.Value);
						if (object.Equals(keyValuePair, item))
						{
							this.SetMember(item.Key, default(global::Inventory.Slot.KindDictionary<TValue>.Member));
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x0600310A RID: 12554 RVA: 0x000BD354 File Offset: 0x000BB554
			IEnumerator<KeyValuePair<global::Inventory.Slot.Kind, TValue>> IEnumerable<KeyValuePair<global::Inventory.Slot.Kind, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600310B RID: 12555 RVA: 0x000BD364 File Offset: 0x000BB564
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600310C RID: 12556 RVA: 0x000BD374 File Offset: 0x000BB574
			private global::Inventory.Slot.KindDictionary<TValue>.Member GetMember(global::Inventory.Slot.Kind kind)
			{
				switch (kind)
				{
				case global::Inventory.Slot.Kind.Default:
					return this.mDefault;
				case global::Inventory.Slot.Kind.Belt:
					return this.mBelt;
				case global::Inventory.Slot.Kind.Armor:
					return this.mArmor;
				default:
					throw new ArgumentNullException("Unimplemented kind");
				}
			}

			// Token: 0x0600310D RID: 12557 RVA: 0x000BD3BC File Offset: 0x000BB5BC
			private void SetMember(global::Inventory.Slot.Kind kind, global::Inventory.Slot.KindDictionary<TValue>.Member member)
			{
				switch (kind)
				{
				case global::Inventory.Slot.Kind.Default:
					this.mDefault = member;
					break;
				case global::Inventory.Slot.Kind.Belt:
					this.mBelt = member;
					break;
				case global::Inventory.Slot.Kind.Armor:
					this.mArmor = member;
					break;
				default:
					throw new ArgumentNullException("Unimplemented kind");
				}
			}

			// Token: 0x17000A73 RID: 2675
			// (get) Token: 0x0600310E RID: 12558 RVA: 0x000BD414 File Offset: 0x000BB614
			public int Count
			{
				get
				{
					return (int)this.count;
				}
			}

			// Token: 0x17000A74 RID: 2676
			public TValue this[global::Inventory.Slot.Kind kind]
			{
				get
				{
					global::Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
					if (!member.Defined)
					{
						throw new KeyNotFoundException();
					}
					return member.Value;
				}
				set
				{
					if (!this.GetMember(kind).Defined)
					{
						this.SetMember(kind, new global::Inventory.Slot.KindDictionary<TValue>.Member(value));
						this.count += 1;
					}
					else
					{
						this.SetMember(kind, new global::Inventory.Slot.KindDictionary<TValue>.Member(value));
					}
				}
			}

			// Token: 0x06003111 RID: 12561 RVA: 0x000BD4A0 File Offset: 0x000BB6A0
			public bool ContainsKey(global::Inventory.Slot.Kind key)
			{
				return key >= global::Inventory.Slot.Kind.Default && key < (global::Inventory.Slot.Kind)3 && this.GetMember(key).Defined;
			}

			// Token: 0x06003112 RID: 12562 RVA: 0x000BD4D0 File Offset: 0x000BB6D0
			public bool Remove(global::Inventory.Slot.Kind key)
			{
				if (this.GetMember(key).Defined)
				{
					this.SetMember(key, default(global::Inventory.Slot.KindDictionary<TValue>.Member));
					this.count -= 1;
					return true;
				}
				return false;
			}

			// Token: 0x06003113 RID: 12563 RVA: 0x000BD514 File Offset: 0x000BB714
			public void Clear()
			{
				global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default;
				while ((int)this.count > 0 && kind < (global::Inventory.Slot.Kind)3)
				{
					this.Remove(kind);
					kind += 1;
				}
			}

			// Token: 0x06003114 RID: 12564 RVA: 0x000BD548 File Offset: 0x000BB748
			public bool TryGetValue(global::Inventory.Slot.Kind key, out TValue value)
			{
				global::Inventory.Slot.KindDictionary<TValue>.Member member;
				try
				{
					member = this.GetMember(key);
				}
				catch (ArgumentNullException)
				{
					value = default(TValue);
					return false;
				}
				if (member.Defined)
				{
					value = member.Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x06003115 RID: 12565 RVA: 0x000BD5C8 File Offset: 0x000BB7C8
			public global::Inventory.Slot.KindDictionary<TValue>.Enumerator GetEnumerator()
			{
				return new global::Inventory.Slot.KindDictionary<TValue>.Enumerator(this);
			}

			// Token: 0x04001A7E RID: 6782
			private global::Inventory.Slot.KindDictionary<TValue>.Member mDefault;

			// Token: 0x04001A7F RID: 6783
			private global::Inventory.Slot.KindDictionary<TValue>.Member mBelt;

			// Token: 0x04001A80 RID: 6784
			private global::Inventory.Slot.KindDictionary<TValue>.Member mArmor;

			// Token: 0x04001A81 RID: 6785
			private sbyte count;

			// Token: 0x020005F6 RID: 1526
			private struct Member
			{
				// Token: 0x06003116 RID: 12566 RVA: 0x000BD5D8 File Offset: 0x000BB7D8
				public Member(TValue value)
				{
					this.Value = value;
					this.Defined = true;
				}

				// Token: 0x04001A82 RID: 6786
				public TValue Value;

				// Token: 0x04001A83 RID: 6787
				public bool Defined;
			}

			// Token: 0x020005F7 RID: 1527
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<KeyValuePair<global::Inventory.Slot.Kind, TValue>>
			{
				// Token: 0x06003117 RID: 12567 RVA: 0x000BD5E8 File Offset: 0x000BB7E8
				public Enumerator(global::Inventory.Slot.KindDictionary<TValue> dict)
				{
					this.dict = dict;
					this.kind = -1;
				}

				// Token: 0x17000A75 RID: 2677
				// (get) Token: 0x06003118 RID: 12568 RVA: 0x000BD5F8 File Offset: 0x000BB7F8
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06003119 RID: 12569 RVA: 0x000BD608 File Offset: 0x000BB808
				public void Reset()
				{
					this.kind = -1;
				}

				// Token: 0x0600311A RID: 12570 RVA: 0x000BD614 File Offset: 0x000BB814
				public void Dispose()
				{
					this.dict = default(global::Inventory.Slot.KindDictionary<TValue>);
				}

				// Token: 0x17000A76 RID: 2678
				// (get) Token: 0x0600311B RID: 12571 RVA: 0x000BD630 File Offset: 0x000BB830
				public KeyValuePair<global::Inventory.Slot.Kind, TValue> Current
				{
					get
					{
						global::Inventory.Slot.KindDictionary<TValue>.Member member = this.dict.GetMember((global::Inventory.Slot.Kind)this.kind);
						return new KeyValuePair<global::Inventory.Slot.Kind, TValue>((global::Inventory.Slot.Kind)this.kind, member.Value);
					}
				}

				// Token: 0x0600311C RID: 12572 RVA: 0x000BD664 File Offset: 0x000BB864
				public bool MoveNext()
				{
					global::Inventory.Slot.Kind kind;
					while ((kind = (global::Inventory.Slot.Kind)(++this.kind)) < (global::Inventory.Slot.Kind)3)
					{
						if (this.dict.GetMember(kind).Defined)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x04001A84 RID: 6788
				private global::Inventory.Slot.KindDictionary<TValue> dict;

				// Token: 0x04001A85 RID: 6789
				private int kind;
			}
		}

		// Token: 0x020005F8 RID: 1528
		[Flags]
		public enum KindFlags : byte
		{
			// Token: 0x04001A87 RID: 6791
			Default = 1,
			// Token: 0x04001A88 RID: 6792
			Belt = 2,
			// Token: 0x04001A89 RID: 6793
			Armor = 4
		}

		// Token: 0x020005F9 RID: 1529
		[Flags]
		public enum PreferenceFlags : byte
		{
			// Token: 0x04001A8B RID: 6795
			Secondary_Default = 1,
			// Token: 0x04001A8C RID: 6796
			Secondary_Belt = 2,
			// Token: 0x04001A8D RID: 6797
			Secondary_Armor = 4,
			// Token: 0x04001A8E RID: 6798
			Stack = 8,
			// Token: 0x04001A8F RID: 6799
			Primary_Default = 16,
			// Token: 0x04001A90 RID: 6800
			Primary_Belt = 32,
			// Token: 0x04001A91 RID: 6801
			Primary_Armor = 64,
			// Token: 0x04001A92 RID: 6802
			Offset = 128,
			// Token: 0x04001A93 RID: 6803
			Primary_ExplicitSlot = 0
		}

		// Token: 0x020005FA RID: 1530
		public struct Offset
		{
			// Token: 0x0600311D RID: 12573 RVA: 0x000BD6AC File Offset: 0x000BB8AC
			public Offset(int offset)
			{
				this.offset = (byte)offset;
				this.kind = (global::Inventory.Slot.Kind)4;
			}

			// Token: 0x0600311E RID: 12574 RVA: 0x000BD6C0 File Offset: 0x000BB8C0
			public Offset(global::Inventory.Slot.Kind kind, int offset)
			{
				this.kind = kind;
				this.offset = (byte)offset;
			}

			// Token: 0x17000A77 RID: 2679
			// (get) Token: 0x0600311F RID: 12575 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
			public static global::Inventory.Slot.Offset None
			{
				get
				{
					return new global::Inventory.Slot.Offset((global::Inventory.Slot.Kind)5, 0);
				}
			}

			// Token: 0x17000A78 RID: 2680
			// (get) Token: 0x06003120 RID: 12576 RVA: 0x000BD6E0 File Offset: 0x000BB8E0
			public bool Specified
			{
				get
				{
					return this.kind < (global::Inventory.Slot.Kind)3 || (this.kind >= (global::Inventory.Slot.Kind)4 && this.kind < (global::Inventory.Slot.Kind)5);
				}
			}

			// Token: 0x17000A79 RID: 2681
			// (get) Token: 0x06003121 RID: 12577 RVA: 0x000BD70C File Offset: 0x000BB90C
			public bool HasOffsetOfKind
			{
				get
				{
					return this.kind < (global::Inventory.Slot.Kind)3;
				}
			}

			// Token: 0x17000A7A RID: 2682
			// (get) Token: 0x06003122 RID: 12578 RVA: 0x000BD718 File Offset: 0x000BB918
			public bool ExplicitSlot
			{
				get
				{
					return this.kind == (global::Inventory.Slot.Kind)4;
				}
			}

			// Token: 0x17000A7B RID: 2683
			// (get) Token: 0x06003123 RID: 12579 RVA: 0x000BD724 File Offset: 0x000BB924
			public global::Inventory.Slot.Kind OffsetOfKind
			{
				get
				{
					if (!this.HasOffsetOfKind)
					{
						throw new InvalidOperationException("You must check HasOffsetOfKind == true before requesting this value");
					}
					return this.kind;
				}
			}

			// Token: 0x17000A7C RID: 2684
			// (get) Token: 0x06003124 RID: 12580 RVA: 0x000BD744 File Offset: 0x000BB944
			public int SlotOffset
			{
				get
				{
					return (int)this.offset;
				}
			}

			// Token: 0x06003125 RID: 12581 RVA: 0x000BD74C File Offset: 0x000BB94C
			public override string ToString()
			{
				if (!this.Specified)
				{
					return "[Unspecified]";
				}
				if (this.HasOffsetOfKind)
				{
					return string.Format("[{0}+{1}]", this.OffsetOfKind, this.SlotOffset);
				}
				return string.Format("[{0}]", this.SlotOffset);
			}

			// Token: 0x04001A94 RID: 6804
			private global::Inventory.Slot.Kind kind;

			// Token: 0x04001A95 RID: 6805
			private byte offset;
		}

		// Token: 0x020005FB RID: 1531
		public struct Preference
		{
			// Token: 0x06003126 RID: 12582 RVA: 0x000BD7AC File Offset: 0x000BB9AC
			private Preference(global::Inventory.Slot.PreferenceFlags preferenceFlags, int primaryOffset)
			{
				this.Flags = preferenceFlags;
				this.offset = (byte)primaryOffset;
			}

			// Token: 0x17000A7D RID: 2685
			// (get) Token: 0x06003127 RID: 12583 RVA: 0x000BD7C0 File Offset: 0x000BB9C0
			public bool IsUndefined
			{
				get
				{
					return (byte)(this.Flags & ~global::Inventory.Slot.PreferenceFlags.Stack) == 0;
				}
			}

			// Token: 0x17000A7E RID: 2686
			// (get) Token: 0x06003128 RID: 12584 RVA: 0x000BD7D4 File Offset: 0x000BB9D4
			public bool IsDefined
			{
				get
				{
					return (byte)(this.Flags & ~global::Inventory.Slot.PreferenceFlags.Stack) != 0;
				}
			}

			// Token: 0x17000A7F RID: 2687
			// (get) Token: 0x06003129 RID: 12585 RVA: 0x000BD7EC File Offset: 0x000BB9EC
			public global::Inventory.Slot.KindFlags PrimaryKindFlags
			{
				get
				{
					return (global::Inventory.Slot.KindFlags)((byte)(this.Flags >> 4) & 7);
				}
			}

			// Token: 0x17000A80 RID: 2688
			// (get) Token: 0x0600312A RID: 12586 RVA: 0x000BD7FC File Offset: 0x000BB9FC
			public global::Inventory.Slot.KindFlags SecondaryKindFlags
			{
				get
				{
					return (global::Inventory.Slot.KindFlags)(this.Flags & (global::Inventory.Slot.PreferenceFlags.Secondary_Default | global::Inventory.Slot.PreferenceFlags.Secondary_Belt | global::Inventory.Slot.PreferenceFlags.Secondary_Armor));
				}
			}

			// Token: 0x17000A81 RID: 2689
			// (get) Token: 0x0600312B RID: 12587 RVA: 0x000BD808 File Offset: 0x000BBA08
			public bool HasOffset
			{
				get
				{
					return (byte)(this.Flags & global::Inventory.Slot.PreferenceFlags.Offset) == 128;
				}
			}

			// Token: 0x17000A82 RID: 2690
			// (get) Token: 0x0600312C RID: 12588 RVA: 0x000BD820 File Offset: 0x000BBA20
			public bool Stack
			{
				get
				{
					return (byte)(this.Flags & global::Inventory.Slot.PreferenceFlags.Stack) == 8;
				}
			}

			// Token: 0x17000A83 RID: 2691
			// (get) Token: 0x0600312D RID: 12589 RVA: 0x000BD830 File Offset: 0x000BBA30
			public global::Inventory.Slot.Offset Offset
			{
				get
				{
					if ((byte)(this.Flags & global::Inventory.Slot.PreferenceFlags.Offset) == 128)
					{
						uint num = (uint)((byte)(this.Flags & ~global::Inventory.Slot.PreferenceFlags.Offset)) >> 4;
						if (num == 0u)
						{
							return new global::Inventory.Slot.Offset((int)this.offset);
						}
						if ((num & num - 1u) == 0u)
						{
							global::Inventory.Slot.Kind kind = global::Inventory.Slot.Kind.Default;
							while ((num >>= 1) != 0u)
							{
								kind += 1;
							}
							return new global::Inventory.Slot.Offset(kind, (int)this.offset);
						}
					}
					return global::Inventory.Slot.Offset.None;
				}
			}

			// Token: 0x0600312E RID: 12590 RVA: 0x000BD8A4 File Offset: 0x000BBAA4
			public global::Inventory.Slot.Preference CloneOffsetChange(int newOffset)
			{
				return new global::Inventory.Slot.Preference(this.Flags, newOffset);
			}

			// Token: 0x0600312F RID: 12591 RVA: 0x000BD8B4 File Offset: 0x000BBAB4
			public global::Inventory.Slot.Preference CloneStackChange(bool stack)
			{
				if (stack)
				{
					return new global::Inventory.Slot.Preference(this.Flags | global::Inventory.Slot.PreferenceFlags.Stack, (int)this.offset);
				}
				return new global::Inventory.Slot.Preference(this.Flags & ~global::Inventory.Slot.PreferenceFlags.Stack, (int)this.offset);
			}

			// Token: 0x06003130 RID: 12592 RVA: 0x000BD8EC File Offset: 0x000BBAEC
			public static global::Inventory.Slot.Preference Define(int slotNumber, bool stack, global::Inventory.Slot.KindFlags fallbackSlots)
			{
				global::Inventory.Slot.PreferenceFlags preferenceFlags = (global::Inventory.Slot.PreferenceFlags)(fallbackSlots & (global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor));
				if (stack)
				{
					preferenceFlags |= global::Inventory.Slot.PreferenceFlags.Stack;
				}
				if (slotNumber >= 0)
				{
					preferenceFlags |= global::Inventory.Slot.PreferenceFlags.Offset;
				}
				else
				{
					slotNumber = 0;
				}
				return new global::Inventory.Slot.Preference(preferenceFlags, slotNumber);
			}

			// Token: 0x06003131 RID: 12593 RVA: 0x000BD928 File Offset: 0x000BBB28
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, bool stack, global::Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				global::Inventory.Slot.PreferenceFlags preferenceFlags = (global::Inventory.Slot.PreferenceFlags)(fallbackSlotKinds & (global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor));
				if (stack)
				{
					preferenceFlags |= global::Inventory.Slot.PreferenceFlags.Stack;
				}
				if (offsetOfSlotKind >= 0)
				{
					preferenceFlags |= global::Inventory.Slot.PreferenceFlags.Offset;
				}
				else
				{
					offsetOfSlotKind = 0;
				}
				global::Inventory.Slot.PreferenceFlags preferenceFlags2 = (global::Inventory.Slot.PreferenceFlags)(1 << (int)startSlotKind);
				preferenceFlags &= ~preferenceFlags2;
				preferenceFlags |= preferenceFlags2 << 4;
				return new global::Inventory.Slot.Preference(preferenceFlags, offsetOfSlotKind);
			}

			// Token: 0x06003132 RID: 12594 RVA: 0x000BD97C File Offset: 0x000BBB7C
			public static global::Inventory.Slot.Preference Define(int offsetOfSlotKind, bool stack)
			{
				return global::Inventory.Slot.Preference.Define(offsetOfSlotKind, stack, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003133 RID: 12595 RVA: 0x000BD988 File Offset: 0x000BBB88
			public static global::Inventory.Slot.Preference Define(int offsetOfSlotKind, global::Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				return global::Inventory.Slot.Preference.Define(offsetOfSlotKind, true, fallbackSlotKinds);
			}

			// Token: 0x06003134 RID: 12596 RVA: 0x000BD994 File Offset: 0x000BBB94
			public static global::Inventory.Slot.Preference Define(int offsetOfSlotKind, global::Inventory.Slot.Kind fallbackSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(offsetOfSlotKind, true, (global::Inventory.Slot.KindFlags)(1 << (int)fallbackSlotKind));
			}

			// Token: 0x06003135 RID: 12597 RVA: 0x000BD9A4 File Offset: 0x000BBBA4
			public static global::Inventory.Slot.Preference Define(int offsetOfSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(offsetOfSlotKind, true, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003136 RID: 12598 RVA: 0x000BD9B0 File Offset: 0x000BBBB0
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, bool stack)
			{
				return global::Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, stack, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003137 RID: 12599 RVA: 0x000BD9BC File Offset: 0x000BBBBC
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, global::Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				return global::Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, fallbackSlotKinds);
			}

			// Token: 0x06003138 RID: 12600 RVA: 0x000BD9C8 File Offset: 0x000BBBC8
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, global::Inventory.Slot.Kind fallbackSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, (global::Inventory.Slot.KindFlags)(1 << (int)fallbackSlotKind));
			}

			// Token: 0x06003139 RID: 12601 RVA: 0x000BD9DC File Offset: 0x000BBBDC
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x0600313A RID: 12602 RVA: 0x000BD9E8 File Offset: 0x000BBBE8
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags firstPreferenceSlotKinds, bool stack, global::Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				global::Inventory.Slot.PreferenceFlags preferenceFlags = (global::Inventory.Slot.PreferenceFlags)((byte)(secondPreferenceSlotKinds & (global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor)) & (byte)(~(byte)firstPreferenceSlotKinds));
				if (stack)
				{
					preferenceFlags |= global::Inventory.Slot.PreferenceFlags.Stack;
				}
				preferenceFlags |= (global::Inventory.Slot.PreferenceFlags)(firstPreferenceSlotKinds << 4);
				return new global::Inventory.Slot.Preference(preferenceFlags, 0);
			}

			// Token: 0x0600313B RID: 12603 RVA: 0x000BDA1C File Offset: 0x000BBC1C
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind firstPreferenceSlotKind, bool stack, global::Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return global::Inventory.Slot.Preference.Define((global::Inventory.Slot.KindFlags)(1 << (int)firstPreferenceSlotKind), stack, secondPreferenceSlotKinds);
			}

			// Token: 0x0600313C RID: 12604 RVA: 0x000BDA2C File Offset: 0x000BBC2C
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind firstPreferenceSlotKind, bool stack, global::Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return global::Inventory.Slot.Preference.Define((global::Inventory.Slot.KindFlags)(1 << (int)firstPreferenceSlotKind), stack, (global::Inventory.Slot.KindFlags)(1 << (int)secondPreferenceSlotKind));
			}

			// Token: 0x0600313D RID: 12605 RVA: 0x000BDA44 File Offset: 0x000BBC44
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags firstPreferenceSlotKind, bool stack, global::Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(firstPreferenceSlotKind, stack, (global::Inventory.Slot.KindFlags)(1 << (int)secondPreferenceSlotKind));
			}

			// Token: 0x0600313E RID: 12606 RVA: 0x000BDA54 File Offset: 0x000BBC54
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind slotsOfKind, bool stack)
			{
				return global::Inventory.Slot.Preference.Define(slotsOfKind, stack, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x0600313F RID: 12607 RVA: 0x000BDA60 File Offset: 0x000BBC60
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags slotsOfKinds, bool stack)
			{
				return global::Inventory.Slot.Preference.Define(slotsOfKinds, stack, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003140 RID: 12608 RVA: 0x000BDA6C File Offset: 0x000BBC6C
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind firstPreferenceSlotKind, global::Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(firstPreferenceSlotKind, true, secondPreferenceSlotKind);
			}

			// Token: 0x06003141 RID: 12609 RVA: 0x000BDA78 File Offset: 0x000BBC78
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags firstPreferenceSlotKinds, global::Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return global::Inventory.Slot.Preference.Define(firstPreferenceSlotKinds, true, secondPreferenceSlotKinds);
			}

			// Token: 0x06003142 RID: 12610 RVA: 0x000BDA84 File Offset: 0x000BBC84
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind firstPreferenceSlotKind, global::Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return global::Inventory.Slot.Preference.Define(firstPreferenceSlotKind, true, secondPreferenceSlotKinds);
			}

			// Token: 0x06003143 RID: 12611 RVA: 0x000BDA90 File Offset: 0x000BBC90
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags firstPreferenceSlotKinds, global::Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return global::Inventory.Slot.Preference.Define(firstPreferenceSlotKinds, true, secondPreferenceSlotKind);
			}

			// Token: 0x06003144 RID: 12612 RVA: 0x000BDA9C File Offset: 0x000BBC9C
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.Kind slotsOfKind)
			{
				return global::Inventory.Slot.Preference.Define(slotsOfKind, true, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003145 RID: 12613 RVA: 0x000BDAA8 File Offset: 0x000BBCA8
			public static global::Inventory.Slot.Preference Define(global::Inventory.Slot.KindFlags slotsOfKinds)
			{
				return global::Inventory.Slot.Preference.Define(slotsOfKinds, true, (global::Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06003146 RID: 12614 RVA: 0x000BDAB4 File Offset: 0x000BBCB4
			public override string ToString()
			{
				global::Inventory.Slot.KindFlags primaryKindFlags = this.PrimaryKindFlags;
				global::Inventory.Slot.KindFlags secondaryKindFlags = this.SecondaryKindFlags;
				global::Inventory.Slot.Offset offset = this.Offset;
				if (secondaryKindFlags != (global::Inventory.Slot.KindFlags)0)
				{
					if (offset.Specified)
					{
						if (offset.HasOffsetOfKind)
						{
							if (this.Stack)
							{
								return string.Format("[{0}+{1}|{2} (stack)]", offset.OffsetOfKind, offset.SlotOffset, secondaryKindFlags);
							}
							return string.Format("[{0}+{1}|{2}]", offset.OffsetOfKind, offset.SlotOffset, secondaryKindFlags);
						}
						else
						{
							if (this.Stack)
							{
								return string.Format("[{0}|{1} (stack)]", offset.SlotOffset, secondaryKindFlags);
							}
							return string.Format("[{0}|{1}]", offset.SlotOffset, secondaryKindFlags);
						}
					}
					else if (primaryKindFlags != (global::Inventory.Slot.KindFlags)0)
					{
						if (this.Stack)
						{
							return string.Format("[{0}|{1} (stack)]", primaryKindFlags, secondaryKindFlags);
						}
						return string.Format("[{0}|{1}]", primaryKindFlags, secondaryKindFlags);
					}
					else
					{
						if (this.Stack)
						{
							return string.Format("[|{1} (stack)]", secondaryKindFlags);
						}
						return string.Format("[|{1}]", secondaryKindFlags);
					}
				}
				else if (offset.Specified)
				{
					if (offset.HasOffsetOfKind)
					{
						if (this.Stack)
						{
							return string.Format("[{0}+{1} (stack)]", offset.OffsetOfKind, offset.SlotOffset);
						}
						return string.Format("[{0}+{1}]", offset.OffsetOfKind, offset.SlotOffset);
					}
					else
					{
						if (this.Stack)
						{
							return string.Format("[{0} (stack)]", offset.SlotOffset);
						}
						return string.Format("[{0}]", offset.SlotOffset);
					}
				}
				else
				{
					if (primaryKindFlags == (global::Inventory.Slot.KindFlags)0)
					{
						return "[Undefined]";
					}
					if (this.Stack)
					{
						return string.Format("[{0} (stack)]", primaryKindFlags);
					}
					return string.Format("[{0}]", primaryKindFlags);
				}
			}

			// Token: 0x06003147 RID: 12615 RVA: 0x000BDCDC File Offset: 0x000BBEDC
			public static implicit operator global::Inventory.Slot.Preference(int slot)
			{
				return new global::Inventory.Slot.Preference(global::Inventory.Slot.PreferenceFlags.Stack | global::Inventory.Slot.PreferenceFlags.Offset, (int)((byte)slot));
			}

			// Token: 0x06003148 RID: 12616 RVA: 0x000BDCEC File Offset: 0x000BBEEC
			public static implicit operator global::Inventory.Slot.Preference(global::Inventory.Slot.Kind kind)
			{
				return new global::Inventory.Slot.Preference((global::Inventory.Slot.PreferenceFlags)((byte)(((byte)(1 << (int)kind) & 7) << 4) | 8), 0);
			}

			// Token: 0x06003149 RID: 12617 RVA: 0x000BDD04 File Offset: 0x000BBF04
			public static implicit operator global::Inventory.Slot.Preference(global::Inventory.Slot.KindFlags kindFlags)
			{
				return new global::Inventory.Slot.Preference((global::Inventory.Slot.PreferenceFlags)((byte)((byte)(kindFlags & (global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt | global::Inventory.Slot.KindFlags.Armor)) << 4) | 8), 0);
			}

			// Token: 0x04001A96 RID: 6806
			private const bool kDefaultStack = true;

			// Token: 0x04001A97 RID: 6807
			public readonly global::Inventory.Slot.PreferenceFlags Flags;

			// Token: 0x04001A98 RID: 6808
			private readonly byte offset;
		}

		// Token: 0x020005FC RID: 1532
		public struct Range
		{
			// Token: 0x0600314A RID: 12618 RVA: 0x000BDD18 File Offset: 0x000BBF18
			public Range(int start, int length)
			{
				this.Start = start;
				this.Count = length;
			}

			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x0600314B RID: 12619 RVA: 0x000BDD28 File Offset: 0x000BBF28
			public int End
			{
				get
				{
					return this.Start + this.Count;
				}
			}

			// Token: 0x17000A85 RID: 2693
			// (get) Token: 0x0600314C RID: 12620 RVA: 0x000BDD38 File Offset: 0x000BBF38
			public int Last
			{
				get
				{
					return (this.Count > 1) ? (this.Start + (this.Count - 1)) : this.Start;
				}
			}

			// Token: 0x17000A86 RID: 2694
			// (get) Token: 0x0600314D RID: 12621 RVA: 0x000BDD6C File Offset: 0x000BBF6C
			public bool Any
			{
				get
				{
					return this.Count > 0;
				}
			}

			// Token: 0x0600314E RID: 12622 RVA: 0x000BDD78 File Offset: 0x000BBF78
			public bool Contains(int i)
			{
				return this.Count > 0 && (this.Start == i || (this.Start < i && this.Start + this.Count > i));
			}

			// Token: 0x0600314F RID: 12623 RVA: 0x000BDDB8 File Offset: 0x000BBFB8
			public sbyte ContainEx(int i)
			{
				if (this.Start > i)
				{
					return -1;
				}
				if (i - this.Start < this.Count)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06003150 RID: 12624 RVA: 0x000BDDEC File Offset: 0x000BBFEC
			public int Gouge(int i, out global::Inventory.Slot.RangePair pair)
			{
				if (this.Count <= 0 || (this.Count == 1 && i == this.Start))
				{
					pair = default(global::Inventory.Slot.RangePair);
					return 0;
				}
				if (i < this.Start || i >= this.Start + this.Count)
				{
					pair = new global::Inventory.Slot.RangePair(this);
					return 1;
				}
				if (i == this.Start)
				{
					pair = new global::Inventory.Slot.RangePair(new global::Inventory.Slot.Range(this.Start + 1, this.Count - 1));
					return 1;
				}
				if (i == this.Start + this.Count - 1)
				{
					pair = new global::Inventory.Slot.RangePair(new global::Inventory.Slot.Range(this.Start, this.Count - 1));
					return 1;
				}
				pair = new global::Inventory.Slot.RangePair(new global::Inventory.Slot.Range(this.Start, i - this.Start), new global::Inventory.Slot.Range(i + 1, this.Count - (i - this.Start + 1)));
				return 2;
			}

			// Token: 0x06003151 RID: 12625 RVA: 0x000BDEE8 File Offset: 0x000BC0E8
			public int Index(int offset)
			{
				int num = this.Start + offset;
				return (!this.Contains(num)) ? -1 : num;
			}

			// Token: 0x06003152 RID: 12626 RVA: 0x000BDF14 File Offset: 0x000BC114
			public int GetOffset(int i)
			{
				if (this.Contains(i))
				{
					return i - this.Start;
				}
				return -1;
			}

			// Token: 0x06003153 RID: 12627 RVA: 0x000BDF2C File Offset: 0x000BC12C
			public override string ToString()
			{
				return string.Format("[{0}:{1}]", this.Start, this.Count);
			}

			// Token: 0x04001A99 RID: 6809
			public readonly int Start;

			// Token: 0x04001A9A RID: 6810
			public readonly int Count;
		}

		// Token: 0x020005FD RID: 1533
		public struct RangePair
		{
			// Token: 0x06003154 RID: 12628 RVA: 0x000BDF5C File Offset: 0x000BC15C
			public RangePair(global::Inventory.Slot.Range A, global::Inventory.Slot.Range B)
			{
				this.A = A;
				this.B = B;
			}

			// Token: 0x06003155 RID: 12629 RVA: 0x000BDF6C File Offset: 0x000BC16C
			public RangePair(global::Inventory.Slot.Range AB)
			{
				this.A = AB;
				this.B = AB;
			}

			// Token: 0x04001A9B RID: 6811
			public readonly global::Inventory.Slot.Range A;

			// Token: 0x04001A9C RID: 6812
			public readonly global::Inventory.Slot.Range B;
		}
	}

	// Token: 0x020005FE RID: 1534
	[Flags]
	public enum SlotFlags
	{
		// Token: 0x04001A9E RID: 6814
		Belt = 1,
		// Token: 0x04001A9F RID: 6815
		Storage = 2,
		// Token: 0x04001AA0 RID: 6816
		Equip = 4,
		// Token: 0x04001AA1 RID: 6817
		Head = 8,
		// Token: 0x04001AA2 RID: 6818
		Chest = 16,
		// Token: 0x04001AA3 RID: 6819
		Legs = 32,
		// Token: 0x04001AA4 RID: 6820
		Feet = 64,
		// Token: 0x04001AA5 RID: 6821
		FuelBasic = 128,
		// Token: 0x04001AA6 RID: 6822
		Debris = 256,
		// Token: 0x04001AA7 RID: 6823
		Raw = 512,
		// Token: 0x04001AA8 RID: 6824
		Cooked = 1024,
		// Token: 0x04001AA9 RID: 6825
		Safe = -2147483648
	}

	// Token: 0x020005FF RID: 1535
	public struct Transfer
	{
		// Token: 0x04001AAA RID: 6826
		public global::InventoryItem item;

		// Token: 0x04001AAB RID: 6827
		public global::Inventory.Addition addition;
	}

	// Token: 0x02000600 RID: 1536
	public static class Uses
	{
		// Token: 0x02000601 RID: 1537
		public enum Quantifier : byte
		{
			// Token: 0x04001AAD RID: 6829
			Default,
			// Token: 0x04001AAE RID: 6830
			Manual,
			// Token: 0x04001AAF RID: 6831
			Minimum,
			// Token: 0x04001AB0 RID: 6832
			Maximum,
			// Token: 0x04001AB1 RID: 6833
			StackSize,
			// Token: 0x04001AB2 RID: 6834
			Random
		}

		// Token: 0x02000602 RID: 1538
		public struct Quantity
		{
			// Token: 0x06003156 RID: 12630 RVA: 0x000BDF7C File Offset: 0x000BC17C
			private Quantity(global::Inventory.Uses.Quantifier quantifier, byte manualAmount)
			{
				this.Quantifier = quantifier;
				this.manualAmount = manualAmount;
			}

			// Token: 0x17000A87 RID: 2695
			// (get) Token: 0x06003158 RID: 12632 RVA: 0x000BDFCC File Offset: 0x000BC1CC
			public int ManualAmount
			{
				get
				{
					if (this.Quantifier == global::Inventory.Uses.Quantifier.Manual)
					{
						return (int)this.manualAmount;
					}
					return -1;
				}
			}

			// Token: 0x06003159 RID: 12633 RVA: 0x000BDFE4 File Offset: 0x000BC1E4
			public static global::Inventory.Uses.Quantity Manual(int amount)
			{
				return new global::Inventory.Uses.Quantity(global::Inventory.Uses.Quantifier.Manual, (byte)amount);
			}

			// Token: 0x0600315A RID: 12634 RVA: 0x000BDFF0 File Offset: 0x000BC1F0
			public int CalculateCount(global::ItemDataBlock datablock)
			{
				switch (this.Quantifier)
				{
				case global::Inventory.Uses.Quantifier.Default:
					return datablock._spawnUsesMin + (datablock._spawnUsesMax - datablock._spawnUsesMin) / 2;
				case global::Inventory.Uses.Quantifier.Manual:
					return (this.manualAmount != 0) ? (((int)this.manualAmount <= datablock._maxUses) ? ((int)this.manualAmount) : datablock._maxUses) : 1;
				case global::Inventory.Uses.Quantifier.Minimum:
					return datablock._spawnUsesMin;
				case global::Inventory.Uses.Quantifier.Maximum:
					return datablock._spawnUsesMax;
				case global::Inventory.Uses.Quantifier.StackSize:
					return datablock._maxUses;
				case global::Inventory.Uses.Quantifier.Random:
					return UnityEngine.Random.Range(datablock._spawnUsesMin, datablock._spawnUsesMax + 1);
				default:
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600315B RID: 12635 RVA: 0x000BE0A0 File Offset: 0x000BC2A0
			public override string ToString()
			{
				if (this.Quantifier == global::Inventory.Uses.Quantifier.Manual)
				{
					return this.manualAmount.ToString();
				}
				return this.Quantifier.ToString();
			}

			// Token: 0x0600315C RID: 12636 RVA: 0x000BE0D8 File Offset: 0x000BC2D8
			public static bool TryParse(string text, out global::Inventory.Uses.Quantity uses)
			{
				int num;
				if (int.TryParse(text, out num))
				{
					if (num == 0)
					{
						uses = global::Inventory.Uses.Quantity.Random;
					}
					else if (num < 0)
					{
						uses = global::Inventory.Uses.Quantity.Minimum;
					}
					else if (num > 255)
					{
						uses = global::Inventory.Uses.Quantity.Maximum;
					}
					else
					{
						uses = num;
					}
					return true;
				}
				if (string.Equals(text, "min", StringComparison.InvariantCultureIgnoreCase))
				{
					uses = global::Inventory.Uses.Quantity.Minimum;
					return true;
				}
				if (string.Equals(text, "max", StringComparison.InvariantCultureIgnoreCase))
				{
					uses = global::Inventory.Uses.Quantity.Maximum;
					return true;
				}
				bool result;
				try
				{
					switch ((byte)Enum.Parse(typeof(global::Inventory.Uses.Quantifier), text, true))
					{
					case 0:
						uses = global::Inventory.Uses.Quantity.Default;
						return true;
					case 2:
						uses = global::Inventory.Uses.Quantity.Minimum;
						return true;
					case 3:
						uses = global::Inventory.Uses.Quantity.Maximum;
						return true;
					case 5:
						uses = global::Inventory.Uses.Quantity.Random;
						return true;
					}
					throw new NotImplementedException();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					uses = global::Inventory.Uses.Quantity.Default;
					result = false;
				}
				return result;
			}

			// Token: 0x0600315D RID: 12637 RVA: 0x000BE24C File Offset: 0x000BC44C
			public static implicit operator global::Inventory.Uses.Quantity(int amount)
			{
				return global::Inventory.Uses.Quantity.Manual(amount);
			}

			// Token: 0x04001AB3 RID: 6835
			public readonly global::Inventory.Uses.Quantifier Quantifier;

			// Token: 0x04001AB4 RID: 6836
			private readonly byte manualAmount;

			// Token: 0x04001AB5 RID: 6837
			public static readonly global::Inventory.Uses.Quantity Default = new global::Inventory.Uses.Quantity(global::Inventory.Uses.Quantifier.Default, 0);

			// Token: 0x04001AB6 RID: 6838
			public static readonly global::Inventory.Uses.Quantity Minimum = new global::Inventory.Uses.Quantity(global::Inventory.Uses.Quantifier.Minimum, 0);

			// Token: 0x04001AB7 RID: 6839
			public static readonly global::Inventory.Uses.Quantity Maximum = new global::Inventory.Uses.Quantity(global::Inventory.Uses.Quantifier.Maximum, 0);

			// Token: 0x04001AB8 RID: 6840
			public static readonly global::Inventory.Uses.Quantity Random = new global::Inventory.Uses.Quantity(global::Inventory.Uses.Quantifier.Random, 0);
		}
	}

	// Token: 0x02000603 RID: 1539
	public struct VacantIterator : IDisposable
	{
		// Token: 0x0600315E RID: 12638 RVA: 0x000BE254 File Offset: 0x000BC454
		public VacantIterator(global::Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.VacantEnumerator;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000BE268 File Offset: 0x000BC468
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000BE278 File Offset: 0x000BC478
		public int slot
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000BE288 File Offset: 0x000BC488
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000BE298 File Offset: 0x000BC498
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000BE2A8 File Offset: 0x000BC4A8
		public bool Next(out int slot)
		{
			if (this.Next())
			{
				slot = this.baseEnumerator.Current;
				return true;
			}
			slot = -1;
			return false;
		}

		// Token: 0x04001AB9 RID: 6841
		private global::Inventory.Collection<global::InventoryItem>.VacantCollection.Enumerator baseEnumerator;
	}

	// Token: 0x02000604 RID: 1540
	private static class Empty
	{
		// Token: 0x04001ABA RID: 6842
		public static readonly global::Inventory.SlotFlags[] SlotFlags = new global::Inventory.SlotFlags[0];
	}

	// Token: 0x02000605 RID: 1541
	private static class Shuffle
	{
		// Token: 0x06003166 RID: 12646 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
		public static void Array<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = global::Inventory.Shuffle.r.Next(i);
				if (num != i)
				{
					T t = array[i];
					array[i] = array[num];
					array[num] = t;
				}
			}
		}

		// Token: 0x04001ABB RID: 6843
		private static readonly Random r = new Random();
	}

	// Token: 0x02000606 RID: 1542
	public enum SlotOperationResult : sbyte
	{
		// Token: 0x04001ABD RID: 6845
		NoOp,
		// Token: 0x04001ABE RID: 6846
		Success_Stacked,
		// Token: 0x04001ABF RID: 6847
		Success_Combined,
		// Token: 0x04001AC0 RID: 6848
		Success_Moved = 4,
		// Token: 0x04001AC1 RID: 6849
		Error_OccupiedDestination = -8,
		// Token: 0x04001AC2 RID: 6850
		Error_SameSlot,
		// Token: 0x04001AC3 RID: 6851
		Error_MissingInventory,
		// Token: 0x04001AC4 RID: 6852
		Error_EmptySourceSlot,
		// Token: 0x04001AC5 RID: 6853
		Error_EmptyDestinationSlot,
		// Token: 0x04001AC6 RID: 6854
		Error_SlotRange,
		// Token: 0x04001AC7 RID: 6855
		Error_NoOpArgs,
		// Token: 0x04001AC8 RID: 6856
		Error_Failed
	}

	// Token: 0x02000607 RID: 1543
	private enum SlotOperations : byte
	{
		// Token: 0x04001ACA RID: 6858
		Stack = 1,
		// Token: 0x04001ACB RID: 6859
		Combine,
		// Token: 0x04001ACC RID: 6860
		Move = 4
	}

	// Token: 0x02000608 RID: 1544
	private struct SlotOperationsInfo
	{
		// Token: 0x06003167 RID: 12647 RVA: 0x000BE340 File Offset: 0x000BC540
		public SlotOperationsInfo(global::Inventory.SlotOperations SlotOperations)
		{
			this.SlotOperations = SlotOperations;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000BE34C File Offset: 0x000BC54C
		public override string ToString()
		{
			return this.SlotOperations.ToString();
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000BE360 File Offset: 0x000BC560
		public override bool Equals(object obj)
		{
			return obj is global::Inventory.SlotOperationsInfo && this.Equals((global::Inventory.SlotOperationsInfo)obj);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000BE37C File Offset: 0x000BC57C
		public override int GetHashCode()
		{
			return (int)((byte)(this.SlotOperations & (global::Inventory.SlotOperations)7)) << 16;
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000BE38C File Offset: 0x000BC58C
		public bool Equals(global::Inventory.SlotOperationsInfo other)
		{
			return (byte)(this.SlotOperations & (global::Inventory.SlotOperations)7) == (byte)(other.SlotOperations & (global::Inventory.SlotOperations)7);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
		public static implicit operator global::Inventory.SlotOperationsInfo(global::Inventory.SlotOperations ops)
		{
			return new global::Inventory.SlotOperationsInfo(ops);
		}

		// Token: 0x04001ACD RID: 6861
		[NonSerialized]
		public readonly global::Inventory.SlotOperations SlotOperations;
	}
}
