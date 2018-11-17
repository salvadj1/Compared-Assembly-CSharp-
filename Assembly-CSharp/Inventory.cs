using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200051B RID: 1307
[NGCAutoAddScript]
public class Inventory : IDLocal
{
	// Token: 0x170009B5 RID: 2485
	// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000AFBC8 File Offset: 0x000ADDC8
	public bool isCraftingInventory
	{
		get
		{
			return this is CraftingInventory;
		}
	}

	// Token: 0x170009B6 RID: 2486
	// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000AFBD4 File Offset: 0x000ADDD4
	public float craftingSpeed
	{
		get
		{
			CraftingInventory craftingInventory = this as CraftingInventory;
			if (craftingInventory == null)
			{
				return 0f;
			}
			return craftingInventory.craftingSpeedPerSec;
		}
	}

	// Token: 0x170009B7 RID: 2487
	// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000AFC00 File Offset: 0x000ADE00
	public bool isCrafting
	{
		get
		{
			CraftingInventory craftingInventory = this as CraftingInventory;
			return craftingInventory && craftingInventory.isCrafting;
		}
	}

	// Token: 0x170009B8 RID: 2488
	// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000AFC28 File Offset: 0x000ADE28
	public float? craftingCompletePercent
	{
		get
		{
			CraftingInventory craftingInventory = this as CraftingInventory;
			if (craftingInventory)
			{
				return craftingInventory.craftingCompletePercent;
			}
			return null;
		}
	}

	// Token: 0x170009B9 RID: 2489
	// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000AFC58 File Offset: 0x000ADE58
	public float? craftingSecondsRemaining
	{
		get
		{
			CraftingInventory craftingInventory = this as CraftingInventory;
			if (craftingInventory)
			{
				return craftingInventory.craftingSecondsRemaining;
			}
			return null;
		}
	}

	// Token: 0x170009BA RID: 2490
	// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000AFC88 File Offset: 0x000ADE88
	public int slotCount
	{
		get
		{
			return this.collection.Capacity;
		}
	}

	// Token: 0x170009BB RID: 2491
	// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000AFC98 File Offset: 0x000ADE98
	public int occupiedSlotCount
	{
		get
		{
			return this.collection.OccupiedCount;
		}
	}

	// Token: 0x170009BC RID: 2492
	// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000AFCA8 File Offset: 0x000ADEA8
	public int vacantSlotCount
	{
		get
		{
			return this.collection.VacantCount;
		}
	}

	// Token: 0x170009BD RID: 2493
	// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000AFCB8 File Offset: 0x000ADEB8
	public int dirtySlotCount
	{
		get
		{
			return this.collection.DirtyCount;
		}
	}

	// Token: 0x170009BE RID: 2494
	// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000AFCC8 File Offset: 0x000ADEC8
	public IInventoryItem firstItem
	{
		get
		{
			InventoryItem inventoryItem;
			if (this.collection.GetByOrder(0, out inventoryItem))
			{
				return inventoryItem.iface;
			}
			return null;
		}
	}

	// Token: 0x170009BF RID: 2495
	// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000AFCF0 File Offset: 0x000ADEF0
	public Inventory.OccupiedIterator occupiedIterator
	{
		get
		{
			return new Inventory.OccupiedIterator(this);
		}
	}

	// Token: 0x170009C0 RID: 2496
	// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000AFCF8 File Offset: 0x000ADEF8
	public Inventory.OccupiedReverseIterator occupiedReverseIterator
	{
		get
		{
			return new Inventory.OccupiedReverseIterator(this);
		}
	}

	// Token: 0x170009C1 RID: 2497
	// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000AFD00 File Offset: 0x000ADF00
	public Inventory.VacantIterator vacantIterator
	{
		get
		{
			return new Inventory.VacantIterator(this);
		}
	}

	// Token: 0x170009C2 RID: 2498
	// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000AFD08 File Offset: 0x000ADF08
	public bool noVacantSlots
	{
		get
		{
			return this.collection.HasNoVacancy;
		}
	}

	// Token: 0x170009C3 RID: 2499
	// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000AFD18 File Offset: 0x000ADF18
	public bool noOccupiedSlots
	{
		get
		{
			return this.collection.HasNoOccupant;
		}
	}

	// Token: 0x170009C4 RID: 2500
	// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000AFD28 File Offset: 0x000ADF28
	public bool anyVacantSlots
	{
		get
		{
			return this.collection.HasVacancy;
		}
	}

	// Token: 0x170009C5 RID: 2501
	// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000AFD38 File Offset: 0x000ADF38
	public bool anyOccupiedSlots
	{
		get
		{
			return this.collection.HasAnyOccupant;
		}
	}

	// Token: 0x170009C6 RID: 2502
	// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000AFD48 File Offset: 0x000ADF48
	public bool initialized
	{
		get
		{
			return this._collection_made_;
		}
	}

	// Token: 0x170009C7 RID: 2503
	// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000AFD50 File Offset: 0x000ADF50
	// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000AFD58 File Offset: 0x000ADF58
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

	// Token: 0x170009C8 RID: 2504
	// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000AFD64 File Offset: 0x000ADF64
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

	// Token: 0x170009C9 RID: 2505
	// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000AFDA0 File Offset: 0x000ADFA0
	public EquipmentWearer equipmentWearer
	{
		get
		{
			if (!this._equipmentWearer.cached)
			{
				this._equipmentWearer = base.GetLocal<EquipmentWearer>();
			}
			return this._equipmentWearer.value;
		}
	}

	// Token: 0x170009CA RID: 2506
	// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000AFDDC File Offset: 0x000ADFDC
	protected InventoryItem firstInventoryItem
	{
		get
		{
			InventoryItem result;
			if (this.collection.GetByOrder(0, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x170009CB RID: 2507
	// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000AFE00 File Offset: 0x000AE000
	protected HumanController hackyNeedToFixHumanControllGetValue
	{
		get
		{
			Character character = this.idMain as Character;
			return (!character) ? null : (character.controller as HumanController);
		}
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x000AFE38 File Offset: 0x000AE038
	private void Initialize(int slotCount)
	{
		if (this._collection_made_)
		{
			this.Clear();
			this._collection_ = null;
			this._collection_made_ = false;
		}
		this._slotFlags = Inventory.Empty.SlotFlags;
		this._collection_ = new Inventory.Collection<InventoryItem>(slotCount);
		this._collection_made_ = true;
		this.slotRanges = default(Inventory.Slot.KindDictionary<Inventory.Slot.Range>);
		this.slotRanges[Inventory.Slot.Kind.Default] = new Inventory.Slot.Range(0, slotCount);
		this.ConfigureSlots(slotCount, ref this.slotRanges, ref this._slotFlags);
		this._collection_.MarkCompletelyDirty();
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x000AFEC4 File Offset: 0x000AE0C4
	protected bool InitializeThisFixedSizeInventory()
	{
		FixedSizeInventory fixedSizeInventory = this as FixedSizeInventory;
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

	// Token: 0x06002C18 RID: 11288 RVA: 0x000AFF38 File Offset: 0x000AE138
	public static byte RPCInteger(int i)
	{
		return (byte)i;
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x000AFF3C File Offset: 0x000AE13C
	public static byte RPCInteger(byte i)
	{
		return i;
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x000AFF40 File Offset: 0x000AE140
	public static byte RPCInteger(BitStream stream)
	{
		return stream.Read<byte>(new object[0]);
	}

	// Token: 0x06002C1B RID: 11291 RVA: 0x000AFF50 File Offset: 0x000AE150
	public Inventory.AddExistingItemResult AddExistingItem(IInventoryItem iitem, bool forbidStacking)
	{
		return this.AddExistingItem(iitem, forbidStacking, false);
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x000AFF5C File Offset: 0x000AE15C
	public IInventoryItem AddItem(ItemDataBlock datablock, Inventory.Slot.Preference slot, Inventory.Uses.Quantity uses)
	{
		Datablock.Ident ident = (Datablock.Ident)datablock;
		return this.AddItem(ref ident, slot, uses);
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000AFF7C File Offset: 0x000AE17C
	public IInventoryItem AddItem(ref Datablock.Ident ident, Inventory.Slot.Preference slot, Inventory.Uses.Quantity uses)
	{
		Inventory.Addition addition = default(Inventory.Addition);
		Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = (ItemDataBlock)ident.datablock;
		addition2.SlotPreference = slot;
		addition2.UsesQuantity = uses;
		addition = addition2;
		return this.AddItem(ref addition);
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x000AFFC0 File Offset: 0x000AE1C0
	public IInventoryItem AddItem(ref Inventory.Addition itemAdd)
	{
		return this.AddItem(ref itemAdd, (Inventory.Payload.Opt)0, null);
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000AFFCC File Offset: 0x000AE1CC
	public void AddItems(Inventory.Addition[] itemAdds)
	{
		for (int i = 0; i < itemAdds.Length; i++)
		{
			this.AddItem(ref itemAdds[i]);
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000AFFFC File Offset: 0x000AE1FC
	public IInventoryItem AddItemSomehow(ItemDataBlock item, Inventory.Slot.Kind? slotKindPref, int slotOffset, int usesCount)
	{
		IInventoryItem result;
		if (item && (usesCount > 0 || !item.IsSplittable()))
		{
			IInventoryItem inventoryItem = this.AddItemSomehowWork(item, slotKindPref, slotOffset, usesCount);
			result = inventoryItem;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x000B003C File Offset: 0x000AE23C
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.AmountMode mode, Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount(datablock, amount, mode, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x000B0064 File Offset: 0x000AE264
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.AmountMode mode, Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, mode, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x000B0094 File Offset: 0x000AE294
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.AmountMode mode)
	{
		return this.AddItemAmount(datablock, amount, mode, null, null);
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x000B00BC File Offset: 0x000AE2BC
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.AmountMode mode)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, mode, null, null);
	}

	// Token: 0x06002C25 RID: 11301 RVA: 0x000B00F0 File Offset: 0x000AE2F0
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount(datablock, amount, Inventory.AmountMode.Default, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002C26 RID: 11302 RVA: 0x000B0118 File Offset: 0x000AE318
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.Uses.Quantity perNonSplittableItemUseQuantity)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, Inventory.AmountMode.Default, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), null);
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x000B0148 File Offset: 0x000AE348
	public int AddItemAmount(ItemDataBlock datablock, int amount)
	{
		return this.AddItemAmount(datablock, amount, Inventory.AmountMode.Default, null, null);
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x000B0170 File Offset: 0x000AE370
	public int AddItemAmount(ref Datablock.Ident ident, int amount)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, Inventory.AmountMode.Default, null, null);
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x000B01A4 File Offset: 0x000AE3A4
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.AmountMode mode, Inventory.Uses.Quantity perNonSplittableItemUseQuantity, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, mode, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2A RID: 11306 RVA: 0x000B01C0 File Offset: 0x000AE3C0
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.AmountMode mode, Inventory.Uses.Quantity perNonSplittableItemUseQuantity, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, mode, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2B RID: 11307 RVA: 0x000B01F0 File Offset: 0x000AE3F0
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.AmountMode mode, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, mode, null, new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2C RID: 11308 RVA: 0x000B0218 File Offset: 0x000AE418
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.AmountMode mode, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, mode, null, new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x000B0248 File Offset: 0x000AE448
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.Uses.Quantity perNonSplittableItemUseQuantity, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, Inventory.AmountMode.Default, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x000B0260 File Offset: 0x000AE460
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.Uses.Quantity perNonSplittableItemUseQuantity, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, Inventory.AmountMode.Default, new Inventory.Uses.Quantity?(perNonSplittableItemUseQuantity), new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x000B0290 File Offset: 0x000AE490
	public int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount(datablock, amount, Inventory.AmountMode.Default, null, new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x000B02B8 File Offset: 0x000AE4B8
	public int AddItemAmount(ref Datablock.Ident ident, int amount, Inventory.Slot.Preference slotPref)
	{
		return this.AddItemAmount((ItemDataBlock)ident.datablock, amount, Inventory.AmountMode.Default, null, new Inventory.Slot.Preference?(slotPref));
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x000B02E8 File Offset: 0x000AE4E8
	public bool RemoveItem(int slot)
	{
		return this.RemoveItem(slot, null, false);
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x000B02F4 File Offset: 0x000AE4F4
	public bool RemoveItem(InventoryItem item)
	{
		return !object.ReferenceEquals(item, null) && !(item.inventory != this) && this.RemoveItem(item.slot, item, true);
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x000B0330 File Offset: 0x000AE530
	public bool RemoveItem(IInventoryItem item)
	{
		return this.RemoveItem(item as InventoryItem);
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x000B0340 File Offset: 0x000AE540
	[Obsolete("This isnt right")]
	public void NULL_SLOT_FIX_ME(int slot)
	{
		this.DeleteItem(slot);
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x000B034C File Offset: 0x000AE54C
	public void Clear()
	{
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.ReverseEnumerator occupiedReverseEnumerator = this.collection.OccupiedReverseEnumerator)
		{
			while (occupiedReverseEnumerator.MoveNext())
			{
				this.DeleteItem(occupiedReverseEnumerator.Slot);
			}
		}
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x000B03B0 File Offset: 0x000AE5B0
	public bool IsSlotDirty(int slot)
	{
		return this.collection.IsDirty(slot);
	}

	// Token: 0x06002C37 RID: 11319 RVA: 0x000B03C0 File Offset: 0x000AE5C0
	public bool MarkSlotDirty(int slot)
	{
		return this.collection.MarkDirty(slot);
	}

	// Token: 0x06002C38 RID: 11320 RVA: 0x000B03D0 File Offset: 0x000AE5D0
	public bool MarkSlotClean(int slot)
	{
		return this.collection.MarkClean(slot);
	}

	// Token: 0x06002C39 RID: 11321 RVA: 0x000B03E0 File Offset: 0x000AE5E0
	public bool IsSlotVacant(int slot)
	{
		return this.collection.IsVacant(slot);
	}

	// Token: 0x06002C3A RID: 11322 RVA: 0x000B03F0 File Offset: 0x000AE5F0
	public bool IsSlotOccupied(int slot)
	{
		return this.collection.IsOccupied(slot);
	}

	// Token: 0x06002C3B RID: 11323 RVA: 0x000B0400 File Offset: 0x000AE600
	public bool IsSlotWithinRange(int slot)
	{
		return this.collection.IsWithinRange(slot);
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x000B0410 File Offset: 0x000AE610
	public int CanConsume(ItemDataBlock db, int useCount, List<int> storeToList)
	{
		Inventory.Collection<InventoryItem> collection = this.collection;
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
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem = occupiedEnumerator.Current;
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

	// Token: 0x06002C3D RID: 11325 RVA: 0x000B0500 File Offset: 0x000AE700
	public int CanConsume(ItemDataBlock db, int useCount)
	{
		Inventory.Collection<InventoryItem> collection = this.collection;
		if (useCount <= 0 || collection.HasNoOccupant)
		{
			return 0;
		}
		int num = 0;
		int uniqueID = db.uniqueID;
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem = occupiedEnumerator.Current;
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

	// Token: 0x06002C3E RID: 11326 RVA: 0x000B05B4 File Offset: 0x000AE7B4
	public bool GetItem(int slot, out IInventoryItem item)
	{
		InventoryItem inventoryItem;
		if (!this._collection_made_ || !this._collection_.Get(slot, out inventoryItem))
		{
			item = null;
			return false;
		}
		item = inventoryItem.iface;
		return true;
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x000B05F0 File Offset: 0x000AE7F0
	protected bool GetItem(int slot, out InventoryItem item)
	{
		if (!this._collection_made_)
		{
			item = null;
			return false;
		}
		return this._collection_.Get(slot, out item);
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x000B0610 File Offset: 0x000AE810
	public bool GetSlotsOfKind(Inventory.Slot.Kind kind, out Inventory.Slot.Range range)
	{
		return this.slotRanges.TryGetValue(kind, out range);
	}

	// Token: 0x06002C41 RID: 11329 RVA: 0x000B0620 File Offset: 0x000AE820
	public bool HasSlotsOfKind(Inventory.Slot.Kind kind)
	{
		return this.slotRanges.ContainsKey(kind);
	}

	// Token: 0x06002C42 RID: 11330 RVA: 0x000B0630 File Offset: 0x000AE830
	public bool IsSlotFree(int slot)
	{
		return this.collection.IsVacant(slot);
	}

	// Token: 0x06002C43 RID: 11331 RVA: 0x000B0640 File Offset: 0x000AE840
	public Inventory.SlotFlags GetSlotFlags(int slot)
	{
		return (this._slotFlags != null && this._slotFlags.Length > slot) ? this._slotFlags[slot] : ((Inventory.SlotFlags)0);
	}

	// Token: 0x06002C44 RID: 11332 RVA: 0x000B066C File Offset: 0x000AE86C
	public bool GetSlotKind(int slot, out Inventory.Slot.Kind kind, out int offset)
	{
		if (slot >= 0 && slot < this.slotCount)
		{
			for (Inventory.Slot.Kind kind2 = Inventory.Slot.Kind.Default; kind2 < (Inventory.Slot.Kind)3; kind2 += 1)
			{
				Inventory.Slot.Range range;
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
		kind = Inventory.Slot.Kind.Default;
		offset = -1;
		return false;
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x000B06D0 File Offset: 0x000AE8D0
	public bool GetSlotForKind(Inventory.Slot.Kind kind, int offset, out int slot)
	{
		Inventory.Slot.Range range;
		if (offset >= 0 && this.slotRanges.TryGetValue(kind, out range) && offset < range.Count)
		{
			slot = range.Start + offset;
			return true;
		}
		slot = -1;
		return false;
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x000B0718 File Offset: 0x000AE918
	public bool IsSlotOffsetValid(Inventory.Slot.Kind kind, int offset)
	{
		int num;
		return this.GetSlotForKind(kind, offset, out num);
	}

	// Token: 0x06002C47 RID: 11335 RVA: 0x000B0730 File Offset: 0x000AE930
	public bool CanItemFit(IInventoryItem iitem)
	{
		InventoryItem inventoryItem = iitem as InventoryItem;
		ItemDataBlock datablock = inventoryItem.datablock;
		if (datablock.IsSplittable())
		{
			int num = inventoryItem.uses;
			using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
			{
				while (occupiedEnumerator.MoveNext())
				{
					InventoryItem inventoryItem2 = occupiedEnumerator.Current;
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

	// Token: 0x06002C48 RID: 11336 RVA: 0x000B0800 File Offset: 0x000AEA00
	private bool CheckSlotFlagsAgainstSlot(Inventory.SlotFlags itemSlotFlags, int slot)
	{
		return this.CheckSlotFlags(itemSlotFlags, this.GetSlotFlags(slot));
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x000B0810 File Offset: 0x000AEA10
	public IngredientList<ItemDataBlock> ToIngredientList()
	{
		Inventory.Collection<InventoryItem> collection = this.collection;
		int occupiedCount = collection.OccupiedCount;
		ItemDataBlock[] array = new ItemDataBlock[occupiedCount];
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
		{
			int newSize = 0;
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem = occupiedEnumerator.Current;
				array[newSize++] = inventoryItem.datablock;
			}
			Array.Resize<ItemDataBlock>(ref array, newSize);
		}
		return new IngredientList<ItemDataBlock>(array);
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000B08A4 File Offset: 0x000AEAA4
	public bool MoveItemAtSlotToEmptySlot(Inventory toInv, int fromSlot, int toSlot)
	{
		if (!toInv)
		{
			return false;
		}
		if (toInv == this && fromSlot == toSlot)
		{
			return false;
		}
		Inventory.Collection<InventoryItem> collection = this.collection;
		if (collection.HasNoOccupant)
		{
			return false;
		}
		InventoryItem inventoryItem;
		if (!collection.Get(fromSlot, out inventoryItem))
		{
			return false;
		}
		ItemDataBlock datablock = inventoryItem.datablock;
		Inventory.Addition addition = default(Inventory.Addition);
		Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = datablock;
		addition2.UsesQuantity = inventoryItem.uses;
		addition2.SlotPreference = Inventory.Slot.Preference.Define(toSlot, datablock.IsSplittable());
		addition = addition2;
		return !object.ReferenceEquals(toInv.AddItem(ref addition, Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.RestrictToOffset | Inventory.Payload.Opt.ReuseItem, inventoryItem), null);
	}

	// Token: 0x06002C4B RID: 11339 RVA: 0x000B0950 File Offset: 0x000AEB50
	public T FindItemType<T>() where T : class, IInventoryItem
	{
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem = occupiedEnumerator.Current;
				T t = inventoryItem.iface as T;
				if (!object.ReferenceEquals(t, null))
				{
					return t;
				}
			}
		}
		return (T)((object)null);
	}

	// Token: 0x06002C4C RID: 11340 RVA: 0x000B09E0 File Offset: 0x000AEBE0
	public IItemT FindItem<IItemT>() where IItemT : class, IInventoryItem
	{
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem = occupiedEnumerator.Current;
				IItemT itemT = inventoryItem.iface as IItemT;
				if (!object.ReferenceEquals(itemT, null))
				{
					return itemT;
				}
			}
		}
		return (IItemT)((object)null);
	}

	// Token: 0x06002C4D RID: 11341 RVA: 0x000B0A70 File Offset: 0x000AEC70
	public IEnumerable<IItemT> FindItems<IItemT>() where IItemT : class, IInventoryItem
	{
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator enumerator = this.collection.OccupiedEnumerator)
		{
			while (enumerator.MoveNext())
			{
				InventoryItem inventoryItem = enumerator.Current;
				IItemT item = inventoryItem.iface as IItemT;
				if (!object.ReferenceEquals(item, null))
				{
					yield return item;
				}
			}
		}
		yield break;
	}

	// Token: 0x06002C4E RID: 11342 RVA: 0x000B0A94 File Offset: 0x000AEC94
	public IInventoryItem FindItem(string itemDBName)
	{
		return this.FindItem(DatablockDictionary.GetByName(itemDBName));
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x000B0AA4 File Offset: 0x000AECA4
	public IInventoryItem FindItem(ItemDataBlock itemDB)
	{
		int num = 0;
		return this.FindItem(itemDB, out num);
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000B0ABC File Offset: 0x000AECBC
	public IInventoryItem FindItem(ItemDataBlock itemDB, out int totalNum)
	{
		bool flag = false;
		InventoryItem inventoryItem = null;
		int num = 0;
		int num2 = -1;
		int uniqueID = itemDB.uniqueID;
		using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = this.collection.OccupiedEnumerator)
		{
			while (occupiedEnumerator.MoveNext())
			{
				InventoryItem inventoryItem2 = occupiedEnumerator.Current;
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
		IInventoryItem result;
		if (flag)
		{
			IInventoryItem iface = inventoryItem.iface;
			result = iface;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x170009CC RID: 2508
	// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000B0B7C File Offset: 0x000AED7C
	public IInventoryItem activeItem
	{
		get
		{
			IInventoryItem result;
			if (this._activeItem == null)
			{
				IInventoryItem inventoryItem = null;
				result = inventoryItem;
			}
			else
			{
				result = this._activeItem.iface;
			}
			return result;
		}
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x000B0BA8 File Offset: 0x000AEDA8
	public void SetActiveItemManually(int itemIndex, ItemRepresentation itemRep)
	{
		IInventoryItem inventoryItem;
		this.GetItem(itemIndex, out inventoryItem);
		((IHeldItem)inventoryItem).itemRepresentation = itemRep;
		this.DoSetActiveItem((InventoryItem)inventoryItem);
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x000B0BD8 File Offset: 0x000AEDD8
	public void DeactivateItem()
	{
		this.DoDeactivateItem();
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x000B0BE0 File Offset: 0x000AEDE0
	public Inventory.Transfer[] GenerateOptimizedInventoryListing(Inventory.Slot.KindFlags fallbackPlacement)
	{
		Inventory.Collection<InventoryItem> collection = this.collection;
		if (collection.HasNoOccupant)
		{
			return new Inventory.Transfer[0];
		}
		Inventory.Transfer[] result;
		try
		{
			Inventory.Report.Begin();
			using (Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator occupiedEnumerator = collection.OccupiedEnumerator)
			{
				while (occupiedEnumerator.MoveNext())
				{
					InventoryItem item = occupiedEnumerator.Current;
					Inventory.Report.Take(item);
				}
			}
			result = Inventory.Report.Build(fallbackPlacement);
		}
		finally
		{
			Inventory.Report.Recover();
		}
		return result;
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x000B0C8C File Offset: 0x000AEE8C
	public Inventory.Transfer[] GenerateOptimizedInventoryListing(Inventory.Slot.KindFlags fallbackPlacement, bool randomize)
	{
		Inventory.Transfer[] array = this.GenerateOptimizedInventoryListing(fallbackPlacement);
		if (randomize && array.Length > 0)
		{
			Inventory.Shuffle.Array<Inventory.Transfer>(array);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].addition.SlotPreference = array[i].addition.SlotPreference.CloneOffsetChange(i);
			}
		}
		return array;
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x000B0CF4 File Offset: 0x000AEEF4
	public void ResetToReport(Inventory.Transfer[] items)
	{
		if (this._collection_made_)
		{
			this.Clear();
		}
		this.Initialize(items.Length);
		for (int i = 0; i < items.Length; i++)
		{
			this.AssignItem(ref items[i].addition, Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.RestrictToOffset | Inventory.Payload.Opt.ReuseItem, items[i].item);
		}
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x000B0D50 File Offset: 0x000AEF50
	protected void BindArmorModelsFromArmorDatablockMap(ArmorModelMemberMap<ArmorDataBlock> armorDatablockMap)
	{
		this.lastNetworkedArmorDatablocks = armorDatablockMap;
		ArmorModelRenderer local = base.GetLocal<ArmorModelRenderer>();
		if (local)
		{
			ArmorModelMemberMap map = default(ArmorModelMemberMap);
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ArmorDataBlock armorDataBlock = armorDatablockMap[armorModelSlot];
				map[armorModelSlot] = ((!armorDataBlock) ? null : armorDataBlock.GetArmorModel(armorModelSlot));
			}
			local.BindArmorModels(map);
		}
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x000B0DC4 File Offset: 0x000AEFC4
	protected void RequestCellUpdate(int cell)
	{
		NetCull.RPC<byte>(this, "SVUC", 0, Inventory.RPCInteger(cell));
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x000B0DDC File Offset: 0x000AEFDC
	public void RequestFullUpdate()
	{
		NetCull.RPC(this, "SVUF", 0);
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x000B0DEC File Offset: 0x000AEFEC
	private void OnNetSlotUpdate(Inventory.Collection<InventoryItem> _collection, int slot, bool occupied, BitStream invdata)
	{
		if (occupied)
		{
			int num = invdata.ReadInt32();
			InventoryItem inventoryItem;
			bool flag = _collection.Get(slot, out inventoryItem);
			if (flag && inventoryItem.datablockUniqueID != num)
			{
				this.DeleteItem(slot);
				flag = false;
				inventoryItem = null;
			}
			if (!flag)
			{
				Inventory.Addition addition = default(Inventory.Addition);
				Inventory.Addition addition2 = addition;
				addition2.UniqueID = num;
				addition2.UsesQuantity = Inventory.Uses.Quantity.Maximum;
				addition2.SlotPreference = Inventory.Slot.Preference.Define(slot, false);
				addition = addition2;
				inventoryItem = (this.AddItem(ref addition, Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.RestrictToOffset, null) as InventoryItem);
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

	// Token: 0x06002C5B RID: 11355 RVA: 0x000B0E98 File Offset: 0x000AF098
	protected void OnNetUpdate(BitStream invdata)
	{
		int num = (int)invdata.ReadByte();
		Inventory.Collection<InventoryItem> collection_;
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

	// Token: 0x170009CD RID: 2509
	// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000B0FB8 File Offset: 0x000AF1B8
	private Inventory.Collection<InventoryItem> collection
	{
		get
		{
			if (!this._collection_made_)
			{
				return Inventory.Collection<InventoryItem>.Default.Empty;
			}
			return this._collection_;
		}
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x000B0FD4 File Offset: 0x000AF1D4
	private Inventory.Payload.Result AssignItem(ref Inventory.Addition addition, Inventory.Payload.Opt flags, InventoryItem reuse)
	{
		return Inventory.Payload.AddItem(this, ref addition, flags, reuse);
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x000B0FEC File Offset: 0x000AF1EC
	private static IInventoryItem ResultToItem(ref Inventory.Payload.Result result, Inventory.Payload.Opt flags)
	{
		if ((byte)(result.flags & Inventory.Payload.Result.Flags.AssignedInstance) == 64)
		{
			return result.item.iface;
		}
		if ((byte)(flags & Inventory.Payload.Opt.AllowStackedItemsToBeReturned) != 32)
		{
			return null;
		}
		if ((byte)(result.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
		{
			return result.item.iface;
		}
		return null;
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x000B1044 File Offset: 0x000AF244
	private IInventoryItem AddItem(ref Inventory.Addition addition, Inventory.Payload.Opt flags, InventoryItem reuse)
	{
		Inventory.Payload.Result result = this.AssignItem(ref addition, flags, reuse);
		return Inventory.ResultToItem(ref result, flags);
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x000B1064 File Offset: 0x000AF264
	private Inventory.AddExistingItemResult AddExistingItem(IInventoryItem iitem, bool forbidStacking, bool mustBeUnassigned)
	{
		InventoryItem inventoryItem = iitem as InventoryItem;
		if (object.ReferenceEquals(inventoryItem, null) || (mustBeUnassigned && inventoryItem.inventory))
		{
			return Inventory.AddExistingItemResult.BadItemArgument;
		}
		ItemDataBlock datablock = inventoryItem.datablock;
		Inventory.Addition addition = default(Inventory.Addition);
		Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = datablock;
		addition2.UsesQuantity = inventoryItem.uses;
		addition2.SlotPreference = Inventory.Slot.Preference.Define(Inventory.Slot.Kind.Default, !forbidStacking && datablock.IsSplittable(), Inventory.Slot.Kind.Belt);
		addition = addition2;
		Inventory.Payload.Opt opt = Inventory.Payload.Opt.IgnoreSlotOffset | Inventory.Payload.Opt.ReuseItem;
		if (forbidStacking)
		{
			opt |= Inventory.Payload.Opt.DoNotStack;
		}
		Inventory.Payload.Result result = this.AssignItem(ref addition, opt, inventoryItem);
		if ((byte)(result.flags & Inventory.Payload.Result.Flags.Complete) == 128)
		{
			if ((byte)(result.flags & Inventory.Payload.Result.Flags.AssignedInstance) == 64)
			{
				return Inventory.AddExistingItemResult.Moved;
			}
			if ((byte)(result.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				inventoryItem.SetUses(0);
				return Inventory.AddExistingItemResult.CompletlyStacked;
			}
			Debug.LogWarning("unhandled", this);
			return Inventory.AddExistingItemResult.Failed;
		}
		else
		{
			if ((byte)(result.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				inventoryItem.SetUses(result.usesRemaining);
				return Inventory.AddExistingItemResult.PartiallyStacked;
			}
			return Inventory.AddExistingItemResult.Failed;
		}
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x000B1178 File Offset: 0x000AF378
	private static Inventory.Slot.Preference DefaultAddMultipleItemsSlotPreference(bool stack)
	{
		return Inventory.Slot.Preference.Define(Inventory.Slot.Kind.Default, stack, Inventory.Slot.KindFlags.Belt);
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x000B1184 File Offset: 0x000AF384
	private int AddMultipleItems(ItemDataBlock itemDB, int usesOrItemCountWhenNotSplittable, Inventory.Uses.Quantity nonSplittableUses, Inventory.AddMultipleItemFlags amif, Inventory.Slot.Preference? slotPreference)
	{
		Inventory.Addition addition = default(Inventory.Addition);
		Inventory.Addition addition2 = addition;
		addition2.ItemDataBlock = itemDB;
		addition = addition2;
		bool flag = itemDB.IsSplittable();
		if (((amif & (Inventory.AddMultipleItemFlags.MustBeSplittable | Inventory.AddMultipleItemFlags.MustBeNonSplittable)) | ((!flag) ? Inventory.AddMultipleItemFlags.MustBeNonSplittable : Inventory.AddMultipleItemFlags.MustBeSplittable)) == (Inventory.AddMultipleItemFlags.MustBeSplittable | Inventory.AddMultipleItemFlags.MustBeNonSplittable))
		{
			return usesOrItemCountWhenNotSplittable;
		}
		if (!flag)
		{
			addition.UsesQuantity = nonSplittableUses;
			addition.SlotPreference = ((slotPreference == null) ? Inventory.Slot.Preference.Define(Inventory.Slot.Kind.Default, false, Inventory.Slot.Kind.Belt) : slotPreference.Value.CloneStackChange(false));
			while (usesOrItemCountWhenNotSplittable > 0 && (byte)(this.AssignItem(ref addition, Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.IgnoreSlotOffset, null).flags & Inventory.Payload.Result.Flags.Complete) == 128)
			{
				usesOrItemCountWhenNotSplittable--;
			}
			return usesOrItemCountWhenNotSplittable;
		}
		if (usesOrItemCountWhenNotSplittable == 0)
		{
			return 0;
		}
		if ((amif & (Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks | Inventory.AddMultipleItemFlags.DoNotStackSplittables)) == (Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks | Inventory.AddMultipleItemFlags.DoNotStackSplittables))
		{
			return usesOrItemCountWhenNotSplittable;
		}
		int num = usesOrItemCountWhenNotSplittable / itemDB._maxUses;
		Inventory.Payload.Opt opt = Inventory.Payload.Opt.IgnoreSlotOffset;
		bool flag2;
		if ((amif & Inventory.AddMultipleItemFlags.DoNotStackSplittables) == Inventory.AddMultipleItemFlags.DoNotStackSplittables)
		{
			flag2 = true;
			opt |= Inventory.Payload.Opt.DoNotStack;
			if (slotPreference != null)
			{
				addition.SlotPreference = slotPreference.Value.CloneStackChange(false);
			}
			else
			{
				addition.SlotPreference = Inventory.DefaultAddMultipleItemsSlotPreference(false);
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
				addition.SlotPreference = Inventory.DefaultAddMultipleItemsSlotPreference(true);
			}
		}
		if ((amif & Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks) == Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks)
		{
			opt |= Inventory.Payload.Opt.DoNotAssign;
		}
		int num2 = 0;
		if (num > 0)
		{
			addition.UsesQuantity = itemDB._maxUses;
			Inventory.Payload.Result result;
			for (;;)
			{
				result = this.AssignItem(ref addition, opt, null);
				if ((byte)(result.flags & Inventory.Payload.Result.Flags.Complete) != 128)
				{
					break;
				}
				num2 += itemDB._maxUses;
				if (!flag2 && (byte)(result.flags & Inventory.Payload.Result.Flags.AssignedInstance) == 64)
				{
					opt |= Inventory.Payload.Opt.DoNotStack;
					flag2 = true;
				}
				if (--num <= 0)
				{
					goto IL_18F;
				}
			}
			if ((byte)(result.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
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
		Inventory.Payload.Result result2 = this.AssignItem(ref addition, opt, null);
		if ((byte)(result2.flags & (Inventory.Payload.Result.Flags.Complete | Inventory.Payload.Result.Flags.Stacked)) != 0)
		{
			num2 += num3 - result2.usesRemaining;
		}
		return usesOrItemCountWhenNotSplittable - num2;
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x000B13E0 File Offset: 0x000AF5E0
	private int AddItemAmount(ItemDataBlock datablock, int amount, Inventory.AmountMode mode, Inventory.Uses.Quantity? perNonSplittableItemQuantity, Inventory.Slot.Preference? slotPref)
	{
		if (!datablock)
		{
			return amount;
		}
		Inventory.AddMultipleItemFlags addMultipleItemFlags;
		Inventory.Uses.Quantity nonSplittableUses;
		if (datablock.IsSplittable())
		{
			addMultipleItemFlags = Inventory.AddMultipleItemFlags.MustBeSplittable;
			switch (mode)
			{
			case Inventory.AmountMode.OnlyStack:
				addMultipleItemFlags |= Inventory.AddMultipleItemFlags.DoNotCreateNewSplittableStacks;
				break;
			case Inventory.AmountMode.OnlyCreateNew:
				addMultipleItemFlags |= Inventory.AddMultipleItemFlags.DoNotStackSplittables;
				break;
			case Inventory.AmountMode.IgnoreSplittables:
				return amount;
			}
			nonSplittableUses = default(Inventory.Uses.Quantity);
		}
		else
		{
			if (mode == Inventory.AmountMode.OnlyStack)
			{
				return amount;
			}
			addMultipleItemFlags = Inventory.AddMultipleItemFlags.MustBeNonSplittable;
			nonSplittableUses = ((perNonSplittableItemQuantity == null) ? Inventory.Uses.Quantity.Random : perNonSplittableItemQuantity.Value);
		}
		return this.AddMultipleItems(datablock, amount, nonSplittableUses, addMultipleItemFlags, slotPref);
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x000B1484 File Offset: 0x000AF684
	private IInventoryItem AddItemSomehowWork(ItemDataBlock item, Inventory.Slot.Kind? slotKindPref, int slotOffset, int usesCount)
	{
		Inventory.Slot.Kind value;
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
		Inventory.Addition addition;
		addition.Ident = (Datablock.Ident)item;
		addition.UsesQuantity = usesCount;
		if (flag2)
		{
			if (flag)
			{
				addition.SlotPreference = Inventory.Slot.Preference.Define(value, slotOffset);
				Inventory.Payload.Result result = this.AssignItem(ref addition, Inventory.Payload.Opt.RestrictToOffset, null);
				if ((byte)(result.flags & Inventory.Payload.Result.Flags.Complete) == 128)
				{
					return Inventory.ResultToItem(ref result, Inventory.Payload.Opt.RestrictToOffset);
				}
				if ((byte)(result.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
				{
					addition.UsesQuantity = (usesCount = result.usesRemaining);
				}
			}
			addition.SlotPreference = value;
			Inventory.Payload.Result result2 = this.AssignItem(ref addition, (Inventory.Payload.Opt)0, null);
			if ((byte)(result2.flags & Inventory.Payload.Result.Flags.Complete) == 128)
			{
				return Inventory.ResultToItem(ref result2, (Inventory.Payload.Opt)0);
			}
			if ((byte)(result2.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				addition.UsesQuantity = (usesCount = result2.usesRemaining);
			}
		}
		else if (num >= 0 && num < this.slotCount)
		{
			addition.SlotPreference = Inventory.Slot.Preference.Define(num);
			Inventory.Payload.Result result3 = this.AssignItem(ref addition, Inventory.Payload.Opt.RestrictToOffset, null);
			if ((byte)(result3.flags & Inventory.Payload.Result.Flags.Complete) == 128)
			{
				return Inventory.ResultToItem(ref result3, Inventory.Payload.Opt.RestrictToOffset);
			}
			if ((byte)(result3.flags & Inventory.Payload.Result.Flags.Stacked) == 32)
			{
				addition.UsesQuantity = (usesCount = result3.usesRemaining);
			}
		}
		Inventory.Slot.KindFlags kindFlags = Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor;
		if (flag2)
		{
			kindFlags &= ~(Inventory.Slot.KindFlags)(1 << (int)value);
		}
		addition.SlotPreference = Inventory.Slot.Preference.Define(kindFlags);
		return this.AddItem(ref addition);
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000B166C File Offset: 0x000AF86C
	private bool RemoveItem(int slot, InventoryItem match, bool mustMatch)
	{
		Inventory.Collection<InventoryItem> collection = this.collection;
		InventoryItem inventoryItem;
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

	// Token: 0x06002C66 RID: 11366 RVA: 0x000B16D8 File Offset: 0x000AF8D8
	private void DeleteItem(int slot)
	{
		this.RemoveItem(slot);
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x000B16E4 File Offset: 0x000AF8E4
	public bool NetworkItemAction(int slot, InventoryItem.MenuItem option)
	{
		NetworkView networkView = base.networkView;
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

	// Token: 0x06002C68 RID: 11368 RVA: 0x000B172C File Offset: 0x000AF92C
	private void ItemMergeRPC(NetEntityID toInvID, int fromSlot, int toSlot, bool tryCombine)
	{
		NetCull.RPC<NetEntityID, byte, byte, bool>(this, "ITMG", 0, toInvID, (byte)fromSlot, (byte)toSlot, tryCombine);
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x000B1744 File Offset: 0x000AF944
	private void ItemMergeRPC(int fromSlot, int toSlot, bool tryCombine)
	{
		NetCull.RPC<byte, byte, bool>(this, "ITSM", 0, (byte)fromSlot, (byte)toSlot, tryCombine);
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x000B1758 File Offset: 0x000AF958
	private void ItemMoveRPC(NetEntityID toInvID, int fromSlot, int toSlot)
	{
		NetCull.RPC<NetEntityID, byte, byte>(this, "ITMV", 0, toInvID, (byte)fromSlot, (byte)toSlot);
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x000B176C File Offset: 0x000AF96C
	private void ItemMoveRPC(int fromSlot, int toSlot)
	{
		NetCull.RPC<byte, byte>(this, "ISMV", 0, (byte)fromSlot, (byte)toSlot);
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x000B1780 File Offset: 0x000AF980
	private Inventory.SlotOperationResult ItemMergeRPCPred(NetEntityID toInvID, int fromSlot, int toSlot, bool tryCombine)
	{
		Inventory component = toInvID.GetComponent<Inventory>();
		Inventory.SlotOperationResult result;
		if (component == this)
		{
			if ((int)(result = this.SlotOperation(fromSlot, toSlot, Inventory.SlotOperationsMerge(tryCombine))) > 0)
			{
				this.ItemMergeRPC(fromSlot, toSlot, tryCombine);
			}
		}
		else if ((int)(result = this.SlotOperation(fromSlot, component, toSlot, Inventory.SlotOperationsMerge(tryCombine))) > 0)
		{
			this.ItemMergeRPC(toInvID, fromSlot, toSlot, tryCombine);
		}
		return result;
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x000B17F8 File Offset: 0x000AF9F8
	private Inventory.SlotOperationResult ItemMergeRPCPred(int fromSlot, int toSlot, bool tryCombine)
	{
		Inventory.SlotOperationResult result;
		if ((int)(result = this.SlotOperation(fromSlot, toSlot, Inventory.SlotOperationsMerge(tryCombine))) > 0)
		{
			this.ItemMergeRPC(fromSlot, toSlot, tryCombine);
		}
		return result;
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x000B182C File Offset: 0x000AFA2C
	private Inventory.SlotOperationResult ItemMoveRPCPred(NetEntityID toInvID, int fromSlot, int toSlot)
	{
		Inventory component = toInvID.GetComponent<Inventory>();
		Inventory.SlotOperationResult result;
		if (component == this)
		{
			if ((int)(result = this.SlotOperation(fromSlot, toSlot, Inventory.SlotOperations.Move)) > 0)
			{
				this.ItemMoveRPC(fromSlot, toSlot);
			}
		}
		else if ((int)(result = this.SlotOperation(fromSlot, component, toSlot, Inventory.SlotOperations.Move)) > 0)
		{
			this.ItemMoveRPC(toInvID, fromSlot, toSlot);
		}
		return result;
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x000B1894 File Offset: 0x000AFA94
	private Inventory.SlotOperationResult ItemMoveRPCPred(int fromSlot, int toSlot)
	{
		Inventory.SlotOperationResult result;
		if ((int)(result = this.SlotOperation(fromSlot, toSlot, Inventory.SlotOperations.Move)) > 0)
		{
			this.ItemMoveRPC(fromSlot, toSlot);
		}
		return result;
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x000B18C4 File Offset: 0x000AFAC4
	[RPC]
	protected void GNUP(byte[] data, NetworkMessageInfo info)
	{
		this.OnNetUpdate(new BitStream(data, false));
		this.Refresh();
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x000B18DC File Offset: 0x000AFADC
	[RPC]
	protected void ITMV(NetEntityID toInvID, byte fromSlot, byte toSlot, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x000B18E0 File Offset: 0x000AFAE0
	[RPC]
	protected void ISMV(byte fromSlot, byte toSlot, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x000B18E4 File Offset: 0x000AFAE4
	[RPC]
	[NGCRPCSkip]
	protected void IACT(byte itemIndex, byte action, NetworkMessageInfo info)
	{
		InventoryItem inventoryItem;
		if (this.collection.Get((int)itemIndex, out inventoryItem))
		{
			inventoryItem.OnMenuOption((InventoryItem.MenuItem)action);
		}
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x000B190C File Offset: 0x000AFB0C
	[NGCRPCSkip]
	[RPC]
	protected void IAST(byte itemIndex, NetworkViewID itemRepID, NetworkMessageInfo info)
	{
		this.SetActiveItemManually((int)itemIndex, (!(itemRepID != NetworkViewID.unassigned)) ? null : NetworkView.Find(itemRepID).GetComponent<ItemRepresentation>());
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x000B1944 File Offset: 0x000AFB44
	[NGCRPCSkip]
	[RPC]
	protected void ITDE(NetworkMessageInfo info)
	{
		this.DeactivateItem();
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x000B194C File Offset: 0x000AFB4C
	[RPC]
	protected void CFAR(BitStream stream)
	{
		ArmorModelMemberMap<ArmorDataBlock> armorDatablockMap = default(ArmorModelMemberMap<ArmorDataBlock>);
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			armorDatablockMap[armorModelSlot] = (DatablockDictionary.GetByUniqueID(stream.ReadInt32()) as ArmorDataBlock);
		}
		this.BindArmorModelsFromArmorDatablockMap(armorDatablockMap);
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x000B1994 File Offset: 0x000AFB94
	[RPC]
	protected void SVUF(NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x000B1998 File Offset: 0x000AFB98
	[RPC]
	protected void ITMG(NetEntityID toInvID, byte fromSlot, byte toSlot, bool tryCombine, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x000B199C File Offset: 0x000AFB9C
	[RPC]
	protected void ITSM(byte fromSlot, byte toSlot, bool tryCombine, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x000B19A0 File Offset: 0x000AFBA0
	public bool SplitStack(int slotNumber)
	{
		InventoryItem inventoryItem;
		if (this.GetItem(slotNumber, out inventoryItem))
		{
			int num = inventoryItem.uses;
			if (num > 1 && this.anyVacantSlots && inventoryItem.datablock.IsSplittable())
			{
				int num2 = num / 2;
				int num3 = num2 - this.AddItemAmount(inventoryItem.datablock, num2, Inventory.AmountMode.OnlyCreateNew);
				if (num3 > 0)
				{
					num -= num3;
					inventoryItem.SetUses(num);
					NetCull.RPC<byte>(this, "ITSP", 0, (byte)slotNumber);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x000B1A20 File Offset: 0x000AFC20
	[RPC]
	protected void ITSP(byte slotNumber, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C7C RID: 11388 RVA: 0x000B1A24 File Offset: 0x000AFC24
	[RPC]
	protected void CLEV(byte itemEvent, int uniqueID)
	{
		ItemDataBlock byUniqueID = DatablockDictionary.GetByUniqueID(uniqueID);
		if (byUniqueID)
		{
			byUniqueID.OnItemEvent((InventoryItem.ItemEvent)itemEvent);
		}
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x000B1A4C File Offset: 0x000AFC4C
	[RPC]
	protected void SVUC(byte cell, NetworkMessageInfo info)
	{
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x000B1A50 File Offset: 0x000AFC50
	private Inventory.SlotOperationResult SlotOperation(int fromSlot, int toSlot, Inventory.SlotOperationsInfo info)
	{
		return this.SlotOperation(fromSlot, this, toSlot, info);
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x000B1A5C File Offset: 0x000AFC5C
	private Inventory.SlotOperationResult SlotOperation(int fromSlot, Inventory toInventory, int toSlot, Inventory.SlotOperationsInfo info)
	{
		if ((byte)((Inventory.SlotOperations)7 & info.SlotOperations) == 0)
		{
			return Inventory.SlotOperationResult.Error_NoOpArgs;
		}
		if (!this || !toInventory)
		{
			return Inventory.SlotOperationResult.Error_MissingInventory;
		}
		bool flag = this == toInventory;
		if (flag && toSlot == fromSlot)
		{
			return Inventory.SlotOperationResult.Error_SameSlot;
		}
		InventoryItem inventoryItem;
		if (!this.GetItem(fromSlot, out inventoryItem))
		{
			return Inventory.SlotOperationResult.Error_EmptySourceSlot;
		}
		InventoryItem inventoryItem2;
		if (toInventory.GetItem(toSlot, out inventoryItem2))
		{
			this.MarkSlotDirty(fromSlot);
			toInventory.MarkSlotDirty(toSlot);
			InventoryItem.MergeResult mergeResult;
			if ((byte)((Inventory.SlotOperations)3 & info.SlotOperations) == 1 && inventoryItem.datablockUniqueID == inventoryItem2.datablockUniqueID)
			{
				mergeResult = inventoryItem.iface.TryStack(inventoryItem2.iface);
			}
			else if ((byte)((Inventory.SlotOperations)3 & info.SlotOperations) != 0)
			{
				mergeResult = inventoryItem.iface.TryCombine(inventoryItem2.iface);
			}
			else
			{
				mergeResult = InventoryItem.MergeResult.Failed;
			}
			InventoryItem.MergeResult mergeResult2 = mergeResult;
			if (mergeResult2 == InventoryItem.MergeResult.Merged)
			{
				return Inventory.SlotOperationResult.Success_Stacked;
			}
			if (mergeResult2 == InventoryItem.MergeResult.Combined)
			{
				return Inventory.SlotOperationResult.Success_Combined;
			}
			if ((byte)(Inventory.SlotOperations.Move & info.SlotOperations) == 4)
			{
				return Inventory.SlotOperationResult.Error_OccupiedDestination;
			}
			return Inventory.SlotOperationResult.NoOp;
		}
		else
		{
			if ((byte)(Inventory.SlotOperations.Move & info.SlotOperations) == 0)
			{
				return Inventory.SlotOperationResult.Error_EmptyDestinationSlot;
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
				return Inventory.SlotOperationResult.Success_Moved;
			}
			return Inventory.SlotOperationResult.Error_Failed;
		}
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x000B1BB8 File Offset: 0x000AFDB8
	private static Inventory.SlotOperations SlotOperationsMerge(bool tryCombine)
	{
		return tryCombine ? ((Inventory.SlotOperations)3) : Inventory.SlotOperations.Stack;
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
	public Inventory.SlotOperationResult ItemMergePredicted(NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(toInvID, fromSlot, toSlot, false);
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x000B1BD4 File Offset: 0x000AFDD4
	public Inventory.SlotOperationResult ItemMergePredicted(Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemMergePredicted(NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x000B1BE4 File Offset: 0x000AFDE4
	public Inventory.SlotOperationResult ItemMergePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(fromSlot, toSlot, false);
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x000B1BF0 File Offset: 0x000AFDF0
	public static Inventory.SlotOperationResult ItemMergePredicted(NetEntityID fromInvID, NetEntityID toInvID, int fromSlot, int toSlot)
	{
		Inventory component = fromInvID.GetComponent<Inventory>();
		if (!component)
		{
			return Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemMergePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x000B1C1C File Offset: 0x000AFE1C
	public Inventory.SlotOperationResult ItemCombinePredicted(NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(toInvID, fromSlot, toSlot, true);
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x000B1C28 File Offset: 0x000AFE28
	public Inventory.SlotOperationResult ItemCombinePredicted(Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemCombinePredicted(NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x000B1C38 File Offset: 0x000AFE38
	public Inventory.SlotOperationResult ItemCombinePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMergeRPCPred(fromSlot, toSlot, true);
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x000B1C44 File Offset: 0x000AFE44
	public static Inventory.SlotOperationResult ItemCombinePredicted(NetEntityID fromInvID, NetEntityID toInvID, int fromSlot, int toSlot)
	{
		Inventory component = fromInvID.GetComponent<Inventory>();
		if (!component)
		{
			return Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemCombinePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x000B1C70 File Offset: 0x000AFE70
	public Inventory.SlotOperationResult ItemMovePredicted(NetEntityID toInvID, int fromSlot, int toSlot)
	{
		return this.ItemMoveRPCPred(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x000B1C7C File Offset: 0x000AFE7C
	public Inventory.SlotOperationResult ItemMovePredicted(Inventory toInventory, int fromSlot, int toSlot)
	{
		return this.ItemMovePredicted(NetEntityID.Get(toInventory), fromSlot, toSlot);
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x000B1C8C File Offset: 0x000AFE8C
	public Inventory.SlotOperationResult ItemMovePredicted(int fromSlot, int toSlot)
	{
		return this.ItemMovePredicted(fromSlot, toSlot);
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x000B1C98 File Offset: 0x000AFE98
	public static Inventory.SlotOperationResult ItemMovePredicted(NetEntityID fromInvID, NetEntityID toInvID, int fromSlot, int toSlot)
	{
		Inventory component = fromInvID.GetComponent<Inventory>();
		if (!component)
		{
			return Inventory.SlotOperationResult.Error_MissingInventory;
		}
		return component.ItemMovePredicted(toInvID, fromSlot, toSlot);
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x000B1CC4 File Offset: 0x000AFEC4
	protected virtual void ConfigureSlots(int totalCount, ref Inventory.Slot.KindDictionary<Inventory.Slot.Range> ranges, ref Inventory.SlotFlags[] flags)
	{
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x000B1CC8 File Offset: 0x000AFEC8
	protected virtual void ItemRemoved(int slot, IInventoryItem item)
	{
		FireBarrel local = base.GetLocal<FireBarrel>();
		if (local)
		{
			local.InvItemRemoved();
		}
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x000B1CF0 File Offset: 0x000AFEF0
	protected virtual void ItemAdded(int slot, IInventoryItem item)
	{
		FireBarrel local = base.GetLocal<FireBarrel>();
		if (local)
		{
			local.InvItemAdded();
		}
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x000B1D18 File Offset: 0x000AFF18
	protected virtual bool CheckSlotFlags(Inventory.SlotFlags itemSlotFlags, Inventory.SlotFlags slotFlags)
	{
		return true;
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x000B1D1C File Offset: 0x000AFF1C
	protected virtual void DoSetActiveItem(InventoryItem item)
	{
		this._activeItem = item;
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x000B1D28 File Offset: 0x000AFF28
	protected virtual void DoDeactivateItem()
	{
		this._activeItem = null;
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x000B1D34 File Offset: 0x000AFF34
	public virtual void Refresh()
	{
	}

	// Token: 0x04001814 RID: 6164
	private const RPCMode ItemAction_RPCMode = 0;

	// Token: 0x04001815 RID: 6165
	private const string GetNetUpdate_RPC = "GNUP";

	// Token: 0x04001816 RID: 6166
	private const string ItemMove_RPC = "ITMV";

	// Token: 0x04001817 RID: 6167
	private const string ItemMoveSelf_RPC = "ISMV";

	// Token: 0x04001818 RID: 6168
	private const string DoItemAction_RPC = "IACT";

	// Token: 0x04001819 RID: 6169
	private const string SetActiveItem_RPC = "IAST";

	// Token: 0x0400181A RID: 6170
	private const string DeactivateItem_RPC = "ITDE";

	// Token: 0x0400181B RID: 6171
	private const string ConfigureArmor_RPC = "CFAR";

	// Token: 0x0400181C RID: 6172
	private const string Server_Request_Inventory_Update_Full = "SVUF";

	// Token: 0x0400181D RID: 6173
	private const string MergeItems_RPC = "ITMG";

	// Token: 0x0400181E RID: 6174
	private const string MergeItemsSelf_RPC = "ITSM";

	// Token: 0x0400181F RID: 6175
	private const string SplitStack_RPCName = "ITSP";

	// Token: 0x04001820 RID: 6176
	private const string Client_ItemEvent = "CLEV";

	// Token: 0x04001821 RID: 6177
	private const string Server_Request_Inventory_Update_Cell = "SVUC";

	// Token: 0x04001822 RID: 6178
	private const Inventory.SlotOperations SlotOperations_Mask = (Inventory.SlotOperations)7;

	// Token: 0x04001823 RID: 6179
	private const Inventory.SlotOperations SlotOperations_Operations = (Inventory.SlotOperations)7;

	// Token: 0x04001824 RID: 6180
	private const Inventory.SlotOperations SlotOperations_Options = (Inventory.SlotOperations)0;

	// Token: 0x04001825 RID: 6181
	[NonSerialized]
	public InventoryItem _activeItem;

	// Token: 0x04001826 RID: 6182
	[NonSerialized]
	private CacheRef<InventoryHolder> _inventoryHolder;

	// Token: 0x04001827 RID: 6183
	[NonSerialized]
	private CacheRef<EquipmentWearer> _equipmentWearer;

	// Token: 0x04001828 RID: 6184
	[NonSerialized]
	private Inventory.Slot.KindDictionary<Inventory.Slot.Range> slotRanges;

	// Token: 0x04001829 RID: 6185
	[NonSerialized]
	private Inventory.Collection<InventoryItem> _collection_;

	// Token: 0x0400182A RID: 6186
	[NonSerialized]
	private Inventory.SlotFlags[] _slotFlags;

	// Token: 0x0400182B RID: 6187
	[NonSerialized]
	private ArmorModelMemberMap<ArmorDataBlock> lastNetworkedArmorDatablocks;

	// Token: 0x0400182C RID: 6188
	[NonSerialized]
	private bool _collection_made_;

	// Token: 0x0400182D RID: 6189
	[NonSerialized]
	private bool _locked;

	// Token: 0x0200051C RID: 1308
	public enum AddExistingItemResult
	{
		// Token: 0x0400182F RID: 6191
		CompletlyStacked,
		// Token: 0x04001830 RID: 6192
		Moved,
		// Token: 0x04001831 RID: 6193
		PartiallyStacked,
		// Token: 0x04001832 RID: 6194
		Failed,
		// Token: 0x04001833 RID: 6195
		BadItemArgument
	}

	// Token: 0x0200051D RID: 1309
	[Flags]
	private enum AddMultipleItemFlags
	{
		// Token: 0x04001835 RID: 6197
		MustBeSplittable = 2,
		// Token: 0x04001836 RID: 6198
		MustBeNonSplittable = 1,
		// Token: 0x04001837 RID: 6199
		DoNotCreateNewSplittableStacks = 4,
		// Token: 0x04001838 RID: 6200
		DoNotStackSplittables = 8
	}

	// Token: 0x0200051E RID: 1310
	public struct Addition
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000B1D38 File Offset: 0x000AFF38
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000B1D4C File Offset: 0x000AFF4C
		public ItemDataBlock ItemDataBlock
		{
			get
			{
				return (ItemDataBlock)this.Ident.datablock;
			}
			set
			{
				this.Ident = (Datablock.Ident)value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B1D5C File Offset: 0x000AFF5C
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x000B1D88 File Offset: 0x000AFF88
		public string Name
		{
			get
			{
				ItemDataBlock itemDataBlock = this.ItemDataBlock;
				return (!itemDataBlock) ? null : itemDataBlock.name;
			}
			set
			{
				this.Ident = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B1D98 File Offset: 0x000AFF98
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x000B1DC4 File Offset: 0x000AFFC4
		public int UniqueID
		{
			get
			{
				ItemDataBlock itemDataBlock = this.ItemDataBlock;
				return (!itemDataBlock) ? 0 : itemDataBlock.uniqueID;
			}
			set
			{
				this.Ident = value;
			}
		}

		// Token: 0x04001839 RID: 6201
		public Datablock.Ident Ident;

		// Token: 0x0400183A RID: 6202
		public Inventory.Uses.Quantity UsesQuantity;

		// Token: 0x0400183B RID: 6203
		public Inventory.Slot.Preference SlotPreference;
	}

	// Token: 0x0200051F RID: 1311
	public enum AmountMode
	{
		// Token: 0x0400183D RID: 6205
		Default,
		// Token: 0x0400183E RID: 6206
		OnlyStack,
		// Token: 0x0400183F RID: 6207
		OnlyCreateNew,
		// Token: 0x04001840 RID: 6208
		IgnoreSplittables
	}

	// Token: 0x02000520 RID: 1312
	private sealed class Collection<T>
	{
		// Token: 0x06002C9A RID: 11418 RVA: 0x000B1DD4 File Offset: 0x000AFFD4
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

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000B1E28 File Offset: 0x000B0028
		public bool AnyVacantOrOccupied
		{
			get
			{
				return this.capacity > 0;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000B1E34 File Offset: 0x000B0034
		public bool IsCompletelyVacant
		{
			get
			{
				return this.count == 0 && this.capacity > 0;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000B1E50 File Offset: 0x000B0050
		public bool HasVacancy
		{
			get
			{
				return this.count < this.capacity;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000B1E60 File Offset: 0x000B0060
		public bool HasNoVacancy
		{
			get
			{
				return this.count == this.capacity;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000B1E70 File Offset: 0x000B0070
		public bool HasNoOccupant
		{
			get
			{
				return this.count == 0;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000B1E7C File Offset: 0x000B007C
		public bool HasAnyOccupant
		{
			get
			{
				return this.count > 0;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000B1E88 File Offset: 0x000B0088
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x000B1ED8 File Offset: 0x000B00D8
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000B1EF0 File Offset: 0x000B00F0
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

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000B1F10 File Offset: 0x000B0110
		public bool MarkedDirty
		{
			get
			{
				return this.forcedDirty || this.countDirty > 0;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000B1F2C File Offset: 0x000B012C
		public bool CompletelyDirty
		{
			get
			{
				return this.countDirty == this.capacity && this.capacity > 0;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000B1F4C File Offset: 0x000B014C
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x000B1F54 File Offset: 0x000B0154
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

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B1F78 File Offset: 0x000B0178
		public bool Clean(out Inventory.Mask dirtyMask, out int numDirty)
		{
			return this.Clean(out dirtyMask, out numDirty, false);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B1F84 File Offset: 0x000B0184
		public bool Clean(out Inventory.Mask dirtyMask, out int numDirty, bool dontActuallyClean)
		{
			if (this.countDirty > 0)
			{
				dirtyMask = this.dirty;
				numDirty = this.countDirty;
				if (!dontActuallyClean)
				{
					this.dirty = default(Inventory.Mask);
					this.countDirty = 0;
					this.forcedDirty = false;
				}
				return true;
			}
			dirtyMask = default(Inventory.Mask);
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

		// Token: 0x06002CAA RID: 11434 RVA: 0x000B2004 File Offset: 0x000B0204
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

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000B2048 File Offset: 0x000B0248
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000B2050 File Offset: 0x000B0250
		public int OccupiedCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000B2058 File Offset: 0x000B0258
		public int VacantCount
		{
			get
			{
				return this.capacity - this.count;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000B2068 File Offset: 0x000B0268
		public int DirtyCount
		{
			get
			{
				return this.countDirty;
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000B2070 File Offset: 0x000B0270
		public void MarkCompletelyDirty()
		{
			this.dirty = new Inventory.Mask(0, this.capacity);
			this.countDirty = this.capacity;
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000B2090 File Offset: 0x000B0290
		public bool MarkDirty(int slot)
		{
			if (slot >= 0 && slot < this.capacity && this.dirty.On(slot))
			{
				this.countDirty++;
				return true;
			}
			return false;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000B20C8 File Offset: 0x000B02C8
		public bool IsVacant(int slot)
		{
			return slot >= 0 && slot < this.capacity && !this.occupied[slot];
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000B20F0 File Offset: 0x000B02F0
		public bool IsOccupied(int slot)
		{
			return slot >= 0 && slot < this.capacity && this.occupied[slot];
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000B2120 File Offset: 0x000B0320
		public bool IsWithinRange(int slot)
		{
			return slot >= 0 && slot < this.capacity;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B2138 File Offset: 0x000B0338
		public Inventory.Collection<T>.OccupiedCollection.Enumerator OccupiedEnumerator
		{
			get
			{
				return new Inventory.Collection<T>.OccupiedCollection.Enumerator(this);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000B2140 File Offset: 0x000B0340
		public Inventory.Collection<T>.OccupiedCollection.ReverseEnumerator OccupiedReverseEnumerator
		{
			get
			{
				return new Inventory.Collection<T>.OccupiedCollection.ReverseEnumerator(this);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000B2148 File Offset: 0x000B0348
		public Inventory.Collection<T>.VacantCollection.Enumerator VacantEnumerator
		{
			get
			{
				return new Inventory.Collection<T>.VacantCollection.Enumerator(this);
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B2150 File Offset: 0x000B0350
		public void Contract()
		{
			this.Contract(new Inventory.Slot.Range(0, this.capacity));
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B2164 File Offset: 0x000B0364
		public void Contract(Inventory.Slot.Range range)
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

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000B22C0 File Offset: 0x000B04C0
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

		// Token: 0x06002CBA RID: 11450 RVA: 0x000B231C File Offset: 0x000B051C
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

		// Token: 0x06002CBB RID: 11451 RVA: 0x000B2390 File Offset: 0x000B0590
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

		// Token: 0x06002CBC RID: 11452 RVA: 0x000B23E4 File Offset: 0x000B05E4
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

		// Token: 0x06002CBD RID: 11453 RVA: 0x000B24D8 File Offset: 0x000B06D8
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

		// Token: 0x06002CBE RID: 11454 RVA: 0x000B2510 File Offset: 0x000B0710
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

		// Token: 0x06002CBF RID: 11455 RVA: 0x000B256C File Offset: 0x000B076C
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

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000B2670 File Offset: 0x000B0870
		public Inventory.Collection<T>.OccupiedCollection Occupied
		{
			get
			{
				Inventory.Collection<T>.OccupiedCollection result;
				if ((result = this.occupiedCollection) == null)
				{
					result = (this.occupiedCollection = new Inventory.Collection<T>.OccupiedCollection(this));
				}
				return result;
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000B269C File Offset: 0x000B089C
		public T[] OccupiedToArray()
		{
			T[] array = new T[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.array[(int)this.indices[i]];
			}
			return array;
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000B26E8 File Offset: 0x000B08E8
		public Inventory.Collection<T>.VacantCollection Vacant
		{
			get
			{
				Inventory.Collection<T>.VacantCollection result;
				if ((result = this.vacantCollection) == null)
				{
					result = (this.vacantCollection = new Inventory.Collection<T>.VacantCollection(this));
				}
				return result;
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000B2714 File Offset: 0x000B0914
		public bool IsDirty(int slot)
		{
			return slot >= 0 && slot < this.capacity && this.dirty[slot];
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000B2744 File Offset: 0x000B0944
		public void MarkCompletelyClean()
		{
			this.dirty = default(Inventory.Mask);
			this.countDirty = 0;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000B2768 File Offset: 0x000B0968
		public bool MarkClean(int slot)
		{
			if (slot >= 0 && slot < this.capacity && this.dirty.Off(slot))
			{
				this.countDirty--;
				return true;
			}
			return false;
		}

		// Token: 0x04001841 RID: 6209
		[NonSerialized]
		private Inventory.Collection<T>.OccupiedCollection occupiedCollection;

		// Token: 0x04001842 RID: 6210
		[NonSerialized]
		private Inventory.Collection<T>.VacantCollection vacantCollection;

		// Token: 0x04001843 RID: 6211
		[NonSerialized]
		private T[] array;

		// Token: 0x04001844 RID: 6212
		[NonSerialized]
		private byte[] indices;

		// Token: 0x04001845 RID: 6213
		[NonSerialized]
		private Inventory.Mask occupied;

		// Token: 0x04001846 RID: 6214
		[NonSerialized]
		private Inventory.Mask dirty;

		// Token: 0x04001847 RID: 6215
		[NonSerialized]
		private int count;

		// Token: 0x04001848 RID: 6216
		[NonSerialized]
		private int capacity;

		// Token: 0x04001849 RID: 6217
		[NonSerialized]
		private int countDirty;

		// Token: 0x0400184A RID: 6218
		[NonSerialized]
		private bool forcedDirty;

		// Token: 0x02000521 RID: 1313
		public static class Default
		{
			// Token: 0x0400184B RID: 6219
			public static readonly Inventory.Collection<T> Empty = new Inventory.Collection<T>(0);
		}

		// Token: 0x02000522 RID: 1314
		public sealed class OccupiedCollection : IEnumerable, IEnumerable<T>
		{
			// Token: 0x06002CC7 RID: 11463 RVA: 0x000B27B0 File Offset: 0x000B09B0
			internal OccupiedCollection(Inventory.Collection<T> collection)
			{
				this.Collection = collection;
			}

			// Token: 0x06002CC8 RID: 11464 RVA: 0x000B27C0 File Offset: 0x000B09C0
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002CC9 RID: 11465 RVA: 0x000B27D0 File Offset: 0x000B09D0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170009E6 RID: 2534
			// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000B27E0 File Offset: 0x000B09E0
			public int Count
			{
				get
				{
					return this.Collection.count;
				}
			}

			// Token: 0x170009E7 RID: 2535
			// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000B27F0 File Offset: 0x000B09F0
			public bool Empty
			{
				get
				{
					return this.Collection.count == 0;
				}
			}

			// Token: 0x06002CCC RID: 11468 RVA: 0x000B2800 File Offset: 0x000B0A00
			public T[] ToArray()
			{
				return this.Collection.OccupiedToArray();
			}

			// Token: 0x06002CCD RID: 11469 RVA: 0x000B2810 File Offset: 0x000B0A10
			public Inventory.Collection<T>.OccupiedCollection.Enumerator GetEnumerator()
			{
				return new Inventory.Collection<T>.OccupiedCollection.Enumerator(this.Collection);
			}

			// Token: 0x0400184C RID: 6220
			public readonly Inventory.Collection<T> Collection;

			// Token: 0x02000523 RID: 1315
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
			{
				// Token: 0x06002CCE RID: 11470 RVA: 0x000B2820 File Offset: 0x000B0A20
				internal Enumerator(Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.indexPosition = -1;
				}

				// Token: 0x170009E8 RID: 2536
				// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000B2830 File Offset: 0x000B0A30
				object IEnumerator.Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x06002CD0 RID: 11472 RVA: 0x000B285C File Offset: 0x000B0A5C
				public bool MoveNext()
				{
					return ++this.indexPosition < this.collection.count;
				}

				// Token: 0x170009E9 RID: 2537
				// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000B2888 File Offset: 0x000B0A88
				public T Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x170009EA RID: 2538
				// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000B28B8 File Offset: 0x000B0AB8
				public int Slot
				{
					get
					{
						return (int)this.collection.indices[this.indexPosition];
					}
				}

				// Token: 0x06002CD3 RID: 11475 RVA: 0x000B28CC File Offset: 0x000B0ACC
				public void Reset()
				{
					this.indexPosition = -1;
				}

				// Token: 0x06002CD4 RID: 11476 RVA: 0x000B28D8 File Offset: 0x000B0AD8
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x0400184D RID: 6221
				private Inventory.Collection<T> collection;

				// Token: 0x0400184E RID: 6222
				private int indexPosition;
			}

			// Token: 0x02000524 RID: 1316
			public struct ReverseEnumerator : IDisposable, IEnumerator, IEnumerator<T>
			{
				// Token: 0x06002CD5 RID: 11477 RVA: 0x000B28E4 File Offset: 0x000B0AE4
				internal ReverseEnumerator(Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.indexPosition = collection.count;
				}

				// Token: 0x170009EB RID: 2539
				// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000B28FC File Offset: 0x000B0AFC
				object IEnumerator.Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x06002CD7 RID: 11479 RVA: 0x000B2928 File Offset: 0x000B0B28
				public bool MoveNext()
				{
					return --this.indexPosition >= 0;
				}

				// Token: 0x170009EC RID: 2540
				// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000B294C File Offset: 0x000B0B4C
				public T Current
				{
					get
					{
						return this.collection.array[(int)this.collection.indices[this.indexPosition]];
					}
				}

				// Token: 0x170009ED RID: 2541
				// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000B297C File Offset: 0x000B0B7C
				public int Slot
				{
					get
					{
						return (int)this.collection.indices[this.indexPosition];
					}
				}

				// Token: 0x06002CDA RID: 11482 RVA: 0x000B2990 File Offset: 0x000B0B90
				public void Reset()
				{
					this.indexPosition = this.collection.count;
				}

				// Token: 0x06002CDB RID: 11483 RVA: 0x000B29A4 File Offset: 0x000B0BA4
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x0400184F RID: 6223
				private Inventory.Collection<T> collection;

				// Token: 0x04001850 RID: 6224
				private int indexPosition;
			}
		}

		// Token: 0x02000525 RID: 1317
		public sealed class VacantCollection : IEnumerable, IEnumerable<int>
		{
			// Token: 0x06002CDC RID: 11484 RVA: 0x000B29B0 File Offset: 0x000B0BB0
			internal VacantCollection(Inventory.Collection<T> collection)
			{
				this.Collection = collection;
			}

			// Token: 0x06002CDD RID: 11485 RVA: 0x000B29C0 File Offset: 0x000B0BC0
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002CDE RID: 11486 RVA: 0x000B29D0 File Offset: 0x000B0BD0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170009EE RID: 2542
			// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000B29E0 File Offset: 0x000B0BE0
			public int Count
			{
				get
				{
					return this.Collection.capacity - this.Collection.count;
				}
			}

			// Token: 0x170009EF RID: 2543
			// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000B29FC File Offset: 0x000B0BFC
			public bool Empty
			{
				get
				{
					return this.Collection.count == this.Collection.capacity;
				}
			}

			// Token: 0x06002CE1 RID: 11489 RVA: 0x000B2A18 File Offset: 0x000B0C18
			public Inventory.Collection<T>.VacantCollection.Enumerator GetEnumerator()
			{
				return new Inventory.Collection<T>.VacantCollection.Enumerator(this.Collection);
			}

			// Token: 0x04001851 RID: 6225
			public readonly Inventory.Collection<T> Collection;

			// Token: 0x02000526 RID: 1318
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<int>
			{
				// Token: 0x06002CE2 RID: 11490 RVA: 0x000B2A28 File Offset: 0x000B0C28
				internal Enumerator(Inventory.Collection<T> collection)
				{
					this.collection = collection;
					this.slotPosition = -1;
				}

				// Token: 0x170009F0 RID: 2544
				// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000B2A38 File Offset: 0x000B0C38
				object IEnumerator.Current
				{
					get
					{
						return this.slotPosition;
					}
				}

				// Token: 0x06002CE4 RID: 11492 RVA: 0x000B2A48 File Offset: 0x000B0C48
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

				// Token: 0x170009F1 RID: 2545
				// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000B2A9C File Offset: 0x000B0C9C
				public int Current
				{
					get
					{
						return this.slotPosition;
					}
				}

				// Token: 0x06002CE6 RID: 11494 RVA: 0x000B2AA4 File Offset: 0x000B0CA4
				public void Reset()
				{
					this.slotPosition = -1;
				}

				// Token: 0x06002CE7 RID: 11495 RVA: 0x000B2AB0 File Offset: 0x000B0CB0
				public void Dispose()
				{
					this.collection = null;
				}

				// Token: 0x04001852 RID: 6226
				private Inventory.Collection<T> collection;

				// Token: 0x04001853 RID: 6227
				private int slotPosition;
			}
		}
	}

	// Token: 0x02000527 RID: 1319
	public static class Constants
	{
		// Token: 0x04001854 RID: 6228
		public const int MaximumSlotCount = 256;
	}

	// Token: 0x02000528 RID: 1320
	public struct Mask
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B2ABC File Offset: 0x000B0CBC
		public Mask(bool defaultOn)
		{
			int num = (!defaultOn) ? 0 : -1;
			this.a = (this.b = (this.c = (this.d = (this.e = (this.f = (this.g = (this.h = num)))))));
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000B2B20 File Offset: 0x000B0D20
		public Mask(int onStart, int onCount)
		{
			this = new Inventory.Mask(false);
			int num = onStart;
			int num2 = onStart + onCount;
			while (num < 256 && num < num2)
			{
				this[num] = true;
				num++;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000B2B60 File Offset: 0x000B0D60
		public bool any
		{
			get
			{
				return this.a != 0 || this.b != 0 || this.c != 0 || this.d != 0 || this.e != 0 || this.f != 0 || this.g != 0 || this.h != 0;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000B2BCC File Offset: 0x000B0DCC
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

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000B2CF0 File Offset: 0x000B0EF0
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

		// Token: 0x170009F5 RID: 2549
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

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B31B0 File Offset: 0x000B13B0
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

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B3384 File Offset: 0x000B1584
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

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B3568 File Offset: 0x000B1768
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

		// Token: 0x04001855 RID: 6229
		public int a;

		// Token: 0x04001856 RID: 6230
		public int b;

		// Token: 0x04001857 RID: 6231
		public int c;

		// Token: 0x04001858 RID: 6232
		public int d;

		// Token: 0x04001859 RID: 6233
		public int e;

		// Token: 0x0400185A RID: 6234
		public int f;

		// Token: 0x0400185B RID: 6235
		public int g;

		// Token: 0x0400185C RID: 6236
		public int h;
	}

	// Token: 0x02000529 RID: 1321
	public struct OccupiedIterator : IDisposable
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B36B0 File Offset: 0x000B18B0
		public OccupiedIterator(Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.OccupiedEnumerator;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B36C4 File Offset: 0x000B18C4
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000B36D4 File Offset: 0x000B18D4
		public IInventoryItem item
		{
			get
			{
				return this.baseEnumerator.Current.iface;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000B36E8 File Offset: 0x000B18E8
		internal InventoryItem inventoryItem
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000B36F8 File Offset: 0x000B18F8
		public int slot
		{
			get
			{
				return this.baseEnumerator.Slot;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000B3708 File Offset: 0x000B1908
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000B3718 File Offset: 0x000B1918
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B3728 File Offset: 0x000B1928
		internal bool Next(out InventoryItem item, out int slot)
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

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B3764 File Offset: 0x000B1964
		internal bool Next(int datablockUniqueID, out InventoryItem item, out int slot)
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

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B3794 File Offset: 0x000B1994
		internal bool Next(ItemDataBlock datablock, out InventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000B37A4 File Offset: 0x000B19A4
		public bool Next(out IInventoryItem item, out int slot)
		{
			InventoryItem inventoryItem;
			if (this.Next(out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000B37D0 File Offset: 0x000B19D0
		public bool Next(int datablockUniqueID, out IInventoryItem item, out int slot)
		{
			InventoryItem inventoryItem;
			if (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000B37FC File Offset: 0x000B19FC
		internal bool Next(ItemDataBlock datablock, out IInventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000B380C File Offset: 0x000B1A0C
		public bool Next<TItemInterface>(out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			IInventoryItem inventoryItem;
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

		// Token: 0x06002D00 RID: 11520 RVA: 0x000B3864 File Offset: 0x000B1A64
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			IInventoryItem inventoryItem;
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

		// Token: 0x06002D01 RID: 11521 RVA: 0x000B38BC File Offset: 0x000B1ABC
		public bool Next<TItemInterface>(ItemDataBlock datablock, out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000B38CC File Offset: 0x000B1ACC
		public bool Next(out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B38E4 File Offset: 0x000B1AE4
		public bool Next(int datablockUniqueID, out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000B38FC File Offset: 0x000B1AFC
		public bool Next(ItemDataBlock datablock, out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(datablock.uniqueID, out inventoryItem, out slot);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000B3918 File Offset: 0x000B1B18
		public bool Next<TItemInterface>(out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000B3930 File Offset: 0x000B1B30
		public bool Next<TItemInterface>(int datablockUniqueID, out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000B3948 File Offset: 0x000B1B48
		public bool Next<TItemInterface>(ItemDataBlock datablock, out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(datablock.uniqueID, out titemInterface, out slot);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000B3964 File Offset: 0x000B1B64
		internal bool Next(out InventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000B397C File Offset: 0x000B1B7C
		internal bool Next(int datablockUniqueID, out InventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000B3994 File Offset: 0x000B1B94
		internal bool Next(ItemDataBlock datablock, out InventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000B39B0 File Offset: 0x000B1BB0
		public bool Next(out IInventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000B39C8 File Offset: 0x000B1BC8
		public bool Next(int datablockUniqueID, out IInventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B39E0 File Offset: 0x000B1BE0
		internal bool Next(ItemDataBlock datablock, out IInventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B39FC File Offset: 0x000B1BFC
		public bool Next<TItemInterface>(out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(out item, out num);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B3A14 File Offset: 0x000B1C14
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B3A2C File Offset: 0x000B1C2C
		public bool Next<TItemInterface>(ItemDataBlock datablock, out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out num);
		}

		// Token: 0x0400185D RID: 6237
		private Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator baseEnumerator;
	}

	// Token: 0x0200052A RID: 1322
	public struct OccupiedReverseIterator : IDisposable
	{
		// Token: 0x06002D11 RID: 11537 RVA: 0x000B3A48 File Offset: 0x000B1C48
		public OccupiedReverseIterator(Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.OccupiedReverseEnumerator;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000B3A5C File Offset: 0x000B1C5C
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000B3A6C File Offset: 0x000B1C6C
		public IInventoryItem item
		{
			get
			{
				return this.baseEnumerator.Current.iface;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000B3A80 File Offset: 0x000B1C80
		internal InventoryItem inventoryItem
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000B3A90 File Offset: 0x000B1C90
		public int slot
		{
			get
			{
				return this.baseEnumerator.Slot;
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B3AA0 File Offset: 0x000B1CA0
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000B3AB0 File Offset: 0x000B1CB0
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000B3AC0 File Offset: 0x000B1CC0
		internal bool Next(out InventoryItem item, out int slot)
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

		// Token: 0x06002D19 RID: 11545 RVA: 0x000B3AFC File Offset: 0x000B1CFC
		internal bool Next(int datablockUniqueID, out InventoryItem item, out int slot)
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

		// Token: 0x06002D1A RID: 11546 RVA: 0x000B3B2C File Offset: 0x000B1D2C
		internal bool Next(ItemDataBlock datablock, out InventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000B3B3C File Offset: 0x000B1D3C
		public bool Next(out IInventoryItem item, out int slot)
		{
			InventoryItem inventoryItem;
			if (this.Next(out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000B3B68 File Offset: 0x000B1D68
		public bool Next(int datablockUniqueID, out IInventoryItem item, out int slot)
		{
			InventoryItem inventoryItem;
			if (this.Next(datablockUniqueID, out inventoryItem, out slot))
			{
				item = inventoryItem.iface;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000B3B94 File Offset: 0x000B1D94
		internal bool Next(ItemDataBlock datablock, out IInventoryItem item, out int slot)
		{
			return this.Next(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000B3BA4 File Offset: 0x000B1DA4
		public bool Next<TItemInterface>(out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			IInventoryItem inventoryItem;
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

		// Token: 0x06002D1F RID: 11551 RVA: 0x000B3BFC File Offset: 0x000B1DFC
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			IInventoryItem inventoryItem;
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

		// Token: 0x06002D20 RID: 11552 RVA: 0x000B3C54 File Offset: 0x000B1E54
		public bool Next<TItemInterface>(ItemDataBlock datablock, out TItemInterface item, out int slot) where TItemInterface : class, IInventoryItem
		{
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out slot);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000B3C64 File Offset: 0x000B1E64
		public bool Next(out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000B3C7C File Offset: 0x000B1E7C
		public bool Next(int datablockUniqueID, out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(out inventoryItem, out slot);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000B3C94 File Offset: 0x000B1E94
		public bool Next(ItemDataBlock datablock, out int slot)
		{
			InventoryItem inventoryItem;
			return this.Next(datablock.uniqueID, out inventoryItem, out slot);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000B3CB0 File Offset: 0x000B1EB0
		public bool Next<TItemInterface>(out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000B3CC8 File Offset: 0x000B1EC8
		public bool Next<TItemInterface>(int datablockUniqueID, out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(out titemInterface, out slot);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000B3CE0 File Offset: 0x000B1EE0
		public bool Next<TItemInterface>(ItemDataBlock datablock, out int slot) where TItemInterface : class, IInventoryItem
		{
			TItemInterface titemInterface;
			return this.Next<TItemInterface>(datablock.uniqueID, out titemInterface, out slot);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000B3CFC File Offset: 0x000B1EFC
		internal bool Next(out InventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000B3D14 File Offset: 0x000B1F14
		internal bool Next(int datablockUniqueID, out InventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000B3D2C File Offset: 0x000B1F2C
		internal bool Next(ItemDataBlock datablock, out InventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000B3D48 File Offset: 0x000B1F48
		public bool Next(out IInventoryItem item)
		{
			int num;
			return this.Next(out item, out num);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000B3D60 File Offset: 0x000B1F60
		public bool Next(int datablockUniqueID, out IInventoryItem item)
		{
			int num;
			return this.Next(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000B3D78 File Offset: 0x000B1F78
		internal bool Next(ItemDataBlock datablock, out IInventoryItem item)
		{
			int num;
			return this.Next(datablock.uniqueID, out item, out num);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000B3D94 File Offset: 0x000B1F94
		public bool Next<TItemInterface>(out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(out item, out num);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000B3DAC File Offset: 0x000B1FAC
		public bool Next<TItemInterface>(int datablockUniqueID, out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablockUniqueID, out item, out num);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000B3DC4 File Offset: 0x000B1FC4
		public bool Next<TItemInterface>(ItemDataBlock datablock, out TItemInterface item) where TItemInterface : class, IInventoryItem
		{
			int num;
			return this.Next<TItemInterface>(datablock.uniqueID, out item, out num);
		}

		// Token: 0x0400185E RID: 6238
		private Inventory.Collection<InventoryItem>.OccupiedCollection.ReverseEnumerator baseEnumerator;
	}

	// Token: 0x0200052B RID: 1323
	private static class Payload
	{
		// Token: 0x06002D30 RID: 11568 RVA: 0x000B3DE0 File Offset: 0x000B1FE0
		private static bool StackUsesSlot(ref Inventory.Payload.StackArguments args, ref Inventory.Payload.StackWork work)
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

		// Token: 0x06002D31 RID: 11569 RVA: 0x000B3E74 File Offset: 0x000B2074
		private static Inventory.Payload.StackResult StackUses(ref Inventory.Payload.StackArguments args, ref Inventory.Payload.RangeArray.Holder ranges, out InventoryItem item)
		{
			if (ranges.Count == 0)
			{
				item = null;
				return Inventory.Payload.StackResult.NoRange;
			}
			if ((byte)(args.prefFlags & Inventory.Slot.PreferenceFlags.Stack) != 8)
			{
				item = null;
				return Inventory.Payload.StackResult.NoneNotMarked;
			}
			if (args.splittable)
			{
				Inventory.Payload.StackWork stackWork;
				stackWork.gotFirstUsage = false;
				stackWork.firstUsage = null;
				int useCount = args.useCount;
				bool flag = false;
				int num = -1;
				Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator enumerator = default(Inventory.Collection<InventoryItem>.OccupiedCollection.Enumerator);
				try
				{
					for (int i = 0; i < ranges.Count; i++)
					{
						if (ranges.Range[i].Count == 1)
						{
							if (args.collection.Get(stackWork.slot = ranges.Range[i].Start, out stackWork.instance) && Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
							{
								item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
								return Inventory.Payload.StackResult.Complete;
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
									if (Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
									{
										item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
										return Inventory.Payload.StackResult.Complete;
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
									if (Inventory.Payload.StackUsesSlot(ref args, ref stackWork))
									{
										item = ((!stackWork.gotFirstUsage) ? stackWork.instance : stackWork.firstUsage);
										return Inventory.Payload.StackResult.Complete;
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
					return (args.useCount >= useCount) ? Inventory.Payload.StackResult.None_FoundFull : Inventory.Payload.StackResult.Partial;
				}
				item = null;
				return Inventory.Payload.StackResult.None;
			}
			item = null;
			return Inventory.Payload.StackResult.NoneUnsplittable;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000B4148 File Offset: 0x000B2348
		private static bool AssignItem(ref Inventory.Payload.Assignment args)
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

		// Token: 0x06002D33 RID: 11571 RVA: 0x000B4240 File Offset: 0x000B2440
		private static bool AssignItemInsideRanges(ref Inventory.Collection<InventoryItem>.VacantCollection.Enumerator enumerator, ref Inventory.Payload.RangeArray.Holder ranges, ref Inventory.Payload.Assignment args)
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
					if (Inventory.Payload.AssignItem(ref args))
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
					if (Inventory.Payload.AssignItem(ref args))
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

		// Token: 0x06002D34 RID: 11572 RVA: 0x000B433C File Offset: 0x000B253C
		public static Inventory.Payload.Result AddItem(Inventory inventory, ref Inventory.Addition addition, Inventory.Payload.Opt options, InventoryItem reuseItem)
		{
			Inventory.Payload.Result result;
			if ((byte)(options & (Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.DoNotAssign)) == 3 || (byte)(options & (Inventory.Payload.Opt.IgnoreSlotOffset | Inventory.Payload.Opt.RestrictToOffset)) == 12)
			{
				result.item = null;
				result.flags = Inventory.Payload.Result.Flags.OptionsResultedInNoOp;
				result.usesRemaining = 0;
			}
			else
			{
				ItemDataBlock itemDataBlock = addition.ItemDataBlock;
				if (!itemDataBlock)
				{
					result.item = null;
					result.flags = Inventory.Payload.Result.Flags.NoItemDatablock;
					result.usesRemaining = 0;
					return result;
				}
				Inventory.Slot.KindFlags kindFlags = addition.SlotPreference.PrimaryKindFlags;
				Inventory.Slot.KindFlags kindFlags2 = addition.SlotPreference.SecondaryKindFlags;
				Inventory.Slot.Range explicitSlot;
				if ((byte)(options & Inventory.Payload.Opt.IgnoreSlotOffset) == 4)
				{
					explicitSlot = default(Inventory.Slot.Range);
				}
				else
				{
					explicitSlot = Inventory.Payload.RangeArray.CalculateExplicitSlotPosition(inventory, ref addition.SlotPreference);
				}
				bool flag = (byte)(options & Inventory.Payload.Opt.RestrictToOffset) == 8;
				bool any = explicitSlot.Any;
				if (flag && !any)
				{
					result.item = null;
					result.flags = Inventory.Payload.Result.Flags.MissingRequiredOffset;
					result.usesRemaining = 0;
					return result;
				}
				if (flag)
				{
					Inventory.Payload.RangeArray.FillTemporaryRanges(ref Inventory.Payload.RangeArray.Primary, inventory, (Inventory.Slot.KindFlags)0, explicitSlot, true);
					Inventory.Payload.RangeArray.FillTemporaryRanges(ref Inventory.Payload.RangeArray.Secondary, inventory, (Inventory.Slot.KindFlags)0, explicitSlot, false);
				}
				else
				{
					Inventory.Payload.RangeArray.FillTemporaryRanges(ref Inventory.Payload.RangeArray.Primary, inventory, kindFlags, explicitSlot, true);
					Inventory.Payload.RangeArray.FillTemporaryRanges(ref Inventory.Payload.RangeArray.Secondary, inventory, kindFlags2, explicitSlot, false);
				}
				int num;
				if (Inventory.Payload.RangeArray.Primary.Count == 0)
				{
					kindFlags = (Inventory.Slot.KindFlags)0;
					if (Inventory.Payload.RangeArray.Secondary.Count == 0)
					{
						kindFlags2 = (Inventory.Slot.KindFlags)0;
						num = 0;
					}
					else
					{
						num = Inventory.Payload.RangeArray.Secondary.Count;
					}
				}
				else if (Inventory.Payload.RangeArray.Secondary.Count == 0)
				{
					kindFlags2 = (Inventory.Slot.KindFlags)0;
					num = Inventory.Payload.RangeArray.Primary.Count;
				}
				else
				{
					num = Inventory.Payload.RangeArray.Primary.Count + Inventory.Payload.RangeArray.Secondary.Count;
				}
				if (num == 0 || (!any && ((byte)(kindFlags | kindFlags2) & 7) == 0))
				{
					result.item = null;
					result.flags = Inventory.Payload.Result.Flags.NoSlotRanges;
					result.usesRemaining = 0;
				}
				else
				{
					int maxUses = itemDataBlock._maxUses;
					bool flag2 = (byte)(options & Inventory.Payload.Opt.ReuseItem) == 16;
					if (flag2 && (object.ReferenceEquals(reuseItem, null) || (itemDataBlock.untransferable && reuseItem.inventory != inventory)))
					{
						result.flags = Inventory.Payload.Result.Flags.FailedToReuse;
						result.item = null;
						result.usesRemaining = 0;
					}
					else
					{
						Inventory.Collection<InventoryItem> collection = inventory.collection;
						result.usesRemaining = ((!flag2) ? addition.UsesQuantity.CalculateCount(itemDataBlock) : reuseItem.uses);
						InventoryItem item;
						Inventory.Payload.StackResult stackResult2;
						if ((byte)(options & Inventory.Payload.Opt.DoNotStack) != 1 && (byte)(addition.SlotPreference.Flags & Inventory.Slot.PreferenceFlags.Stack) == 8)
						{
							Inventory.Payload.StackArguments stackArguments;
							stackArguments.collection = collection;
							stackArguments.datablockUID = itemDataBlock.uniqueID;
							stackArguments.splittable = itemDataBlock.IsSplittable();
							stackArguments.useCount = result.usesRemaining;
							stackArguments.prefFlags = addition.SlotPreference.Flags;
							InventoryItem inventoryItem;
							Inventory.Payload.StackResult stackResult = Inventory.Payload.StackUses(ref stackArguments, ref Inventory.Payload.RangeArray.Primary, out inventoryItem);
							if (stackResult == Inventory.Payload.StackResult.NoneUnsplittable || stackResult == Inventory.Payload.StackResult.Complete)
							{
								InventoryItem inventoryItem2 = item = inventoryItem;
								stackResult2 = stackResult;
							}
							else
							{
								InventoryItem inventoryItem2;
								Inventory.Payload.StackResult stackResult3 = Inventory.Payload.StackUses(ref stackArguments, ref Inventory.Payload.RangeArray.Secondary, out inventoryItem2);
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
							stackResult2 = Inventory.Payload.StackResult.NoneNotMarked;
						}
						if (stackResult2 == Inventory.Payload.StackResult.Complete)
						{
							result.item = item;
							result.flags = (Inventory.Payload.Result.Flags.Complete | Inventory.Payload.Result.Flags.Stacked);
						}
						else
						{
							if (stackResult2 == Inventory.Payload.StackResult.Partial)
							{
								result.item = item;
								result.flags = Inventory.Payload.Result.Flags.Stacked;
							}
							else
							{
								result.flags = Inventory.Payload.Result.Flags.OptionsResultedInNoOp;
							}
							if ((byte)(options & Inventory.Payload.Opt.DoNotAssign) != 2)
							{
								if (collection.HasNoVacancy)
								{
									result.item = item;
									result.flags |= Inventory.Payload.Result.Flags.NoVacancy;
								}
								else
								{
									Inventory.Payload.Assignment assignment;
									assignment.inventory = inventory;
									assignment.collection = collection;
									assignment.fresh = !flag2;
									assignment.item = ((!assignment.fresh) ? reuseItem : (itemDataBlock.CreateItem() as InventoryItem));
									assignment.uses = result.usesRemaining;
									assignment.datablock = itemDataBlock;
									if (!flag2 && object.ReferenceEquals(assignment.item, null))
									{
										result.item = item;
										result.flags |= ((!assignment.fresh) ? Inventory.Payload.Result.Flags.FailedToReuse : Inventory.Payload.Result.Flags.FailedToCreate);
									}
									else
									{
										assignment.slot = -1;
										assignment.attemptsMade = 0;
										Inventory.Collection<InventoryItem>.VacantCollection.Enumerator vacantEnumerator = collection.VacantEnumerator;
										bool flag3;
										try
										{
											flag3 = (Inventory.Payload.AssignItemInsideRanges(ref vacantEnumerator, ref Inventory.Payload.RangeArray.Primary, ref assignment) || Inventory.Payload.AssignItemInsideRanges(ref vacantEnumerator, ref Inventory.Payload.RangeArray.Secondary, ref assignment));
										}
										finally
										{
											vacantEnumerator.Dispose();
										}
										if (flag3)
										{
											result.flags |= (Inventory.Payload.Result.Flags.Complete | Inventory.Payload.Result.Flags.AssignedInstance);
											result.item = assignment.item;
											result.usesRemaining -= result.item.uses;
										}
										else if (assignment.attemptsMade > 0)
										{
											result.flags |= Inventory.Payload.Result.Flags.NoVacancy;
											result.item = item;
										}
										else
										{
											result.flags |= Inventory.Payload.Result.Flags.NoSlotRanges;
											result.item = item;
										}
									}
								}
							}
							else
							{
								result.item = item;
								if (result.flags == Inventory.Payload.Result.Flags.OptionsResultedInNoOp)
								{
									result.flags = Inventory.Payload.Result.Flags.MissingRequiredOffset;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0400185F RID: 6239
		private const Inventory.Payload.Opt NoOp1_Mask = Inventory.Payload.Opt.DoNotStack | Inventory.Payload.Opt.DoNotAssign;

		// Token: 0x04001860 RID: 6240
		private const Inventory.Payload.Opt NoOp2_Mask = Inventory.Payload.Opt.IgnoreSlotOffset | Inventory.Payload.Opt.RestrictToOffset;

		// Token: 0x0200052C RID: 1324
		public struct Result
		{
			// Token: 0x04001861 RID: 6241
			public InventoryItem item;

			// Token: 0x04001862 RID: 6242
			public Inventory.Payload.Result.Flags flags;

			// Token: 0x04001863 RID: 6243
			public int usesRemaining;

			// Token: 0x0200052D RID: 1325
			[Flags]
			public enum Flags : byte
			{
				// Token: 0x04001865 RID: 6245
				Complete = 128,
				// Token: 0x04001866 RID: 6246
				AssignedInstance = 64,
				// Token: 0x04001867 RID: 6247
				Stacked = 32,
				// Token: 0x04001868 RID: 6248
				NoVacancy = 16,
				// Token: 0x04001869 RID: 6249
				DidNotCreate = 6,
				// Token: 0x0400186A RID: 6250
				FailedToReuse = 5,
				// Token: 0x0400186B RID: 6251
				FailedToCreate = 4,
				// Token: 0x0400186C RID: 6252
				NoSlotRanges = 3,
				// Token: 0x0400186D RID: 6253
				MissingRequiredOffset = 2,
				// Token: 0x0400186E RID: 6254
				NoItemDatablock = 1,
				// Token: 0x0400186F RID: 6255
				OptionsResultedInNoOp = 0
			}
		}

		// Token: 0x0200052E RID: 1326
		[Flags]
		public enum Opt : byte
		{
			// Token: 0x04001871 RID: 6257
			DoNotStack = 1,
			// Token: 0x04001872 RID: 6258
			DoNotAssign = 2,
			// Token: 0x04001873 RID: 6259
			IgnoreSlotOffset = 4,
			// Token: 0x04001874 RID: 6260
			RestrictToOffset = 8,
			// Token: 0x04001875 RID: 6261
			ReuseItem = 16,
			// Token: 0x04001876 RID: 6262
			AllowStackedItemsToBeReturned = 32
		}

		// Token: 0x0200052F RID: 1327
		private enum StackResult : byte
		{
			// Token: 0x04001878 RID: 6264
			None,
			// Token: 0x04001879 RID: 6265
			NoneNotMarked,
			// Token: 0x0400187A RID: 6266
			NoneUnsplittable,
			// Token: 0x0400187B RID: 6267
			NoRange,
			// Token: 0x0400187C RID: 6268
			None_FoundFull,
			// Token: 0x0400187D RID: 6269
			Partial,
			// Token: 0x0400187E RID: 6270
			Complete
		}

		// Token: 0x02000530 RID: 1328
		private struct Assignment
		{
			// Token: 0x0400187F RID: 6271
			public Inventory.Collection<InventoryItem> collection;

			// Token: 0x04001880 RID: 6272
			public Inventory inventory;

			// Token: 0x04001881 RID: 6273
			public InventoryItem item;

			// Token: 0x04001882 RID: 6274
			public ItemDataBlock datablock;

			// Token: 0x04001883 RID: 6275
			public int slot;

			// Token: 0x04001884 RID: 6276
			public int uses;

			// Token: 0x04001885 RID: 6277
			public bool fresh;

			// Token: 0x04001886 RID: 6278
			public int attemptsMade;
		}

		// Token: 0x02000531 RID: 1329
		private static class RangeArray
		{
			// Token: 0x06002D36 RID: 11574 RVA: 0x000B48F8 File Offset: 0x000B2AF8
			public static void FillTemporaryRanges(ref Inventory.Payload.RangeArray.Holder temp, Inventory inventory, Inventory.Slot.KindFlags kindFlags, Inventory.Slot.Range explicitSlot, bool insertExplicitSlot)
			{
				kindFlags &= (Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor);
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
				for (Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default; kind < (Inventory.Slot.Kind)3; kind += 1)
				{
					Inventory.Slot.KindFlags flag = (Inventory.Slot.KindFlags)(1 << (int)kind);
					if (Inventory.Payload.RangeArray.CheckSlotKindFlag(inventory, kindFlags, flag, kind, ref num, ref num2))
					{
						temp.Insert(ref num, ref num2, gougeIndex);
					}
				}
			}

			// Token: 0x06002D37 RID: 11575 RVA: 0x000B4998 File Offset: 0x000B2B98
			public static Inventory.Slot.Range CalculateExplicitSlotPosition(Inventory inventory, ref Inventory.Slot.Preference pref)
			{
				Inventory.Slot.Offset offset = pref.Offset;
				if (!offset.Specified)
				{
					return default(Inventory.Slot.Range);
				}
				Inventory.Slot.Range range;
				if (offset.HasOffsetOfKind)
				{
					if (!inventory.slotRanges.TryGetValue(offset.OffsetOfKind, out range))
					{
						return default(Inventory.Slot.Range);
					}
				}
				else
				{
					range = new Inventory.Slot.Range(0, inventory.slotCount);
				}
				int slotOffset = offset.SlotOffset;
				if (range.Count > slotOffset)
				{
					return new Inventory.Slot.Range(range.Start + slotOffset, 1);
				}
				return default(Inventory.Slot.Range);
			}

			// Token: 0x06002D38 RID: 11576 RVA: 0x000B4A34 File Offset: 0x000B2C34
			private static bool CheckSlotKindFlag(Inventory inventory, Inventory.Slot.KindFlags flags, Inventory.Slot.KindFlags flag, Inventory.Slot.Kind kind, ref int start, ref int count)
			{
				Inventory.Slot.Range range;
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

			// Token: 0x04001887 RID: 6279
			private const int ArrayElementCount = 6;

			// Token: 0x04001888 RID: 6280
			public static Inventory.Payload.RangeArray.Holder Primary = new Inventory.Payload.RangeArray.Holder(new Inventory.Slot.Range[6]);

			// Token: 0x04001889 RID: 6281
			public static Inventory.Payload.RangeArray.Holder Secondary = new Inventory.Payload.RangeArray.Holder(new Inventory.Slot.Range[6]);

			// Token: 0x02000532 RID: 1330
			public struct Holder
			{
				// Token: 0x06002D39 RID: 11577 RVA: 0x000B4A98 File Offset: 0x000B2C98
				public Holder(Inventory.Slot.Range[] array)
				{
					this.Count = 0;
					this.Range = array;
				}

				// Token: 0x06002D3A RID: 11578 RVA: 0x000B4AA8 File Offset: 0x000B2CA8
				public void Insert(ref int start, ref int count, int gougeIndex)
				{
					Inventory.Slot.Range range = new Inventory.Slot.Range(start, count);
					if (gougeIndex != -1)
					{
						Inventory.Slot.RangePair rangePair;
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

				// Token: 0x0400188A RID: 6282
				public int Count;

				// Token: 0x0400188B RID: 6283
				public readonly Inventory.Slot.Range[] Range;
			}
		}

		// Token: 0x02000533 RID: 1331
		private struct StackArguments
		{
			// Token: 0x0400188C RID: 6284
			public Inventory.Collection<InventoryItem> collection;

			// Token: 0x0400188D RID: 6285
			public Inventory.Slot.PreferenceFlags prefFlags;

			// Token: 0x0400188E RID: 6286
			public int useCount;

			// Token: 0x0400188F RID: 6287
			public int datablockUID;

			// Token: 0x04001890 RID: 6288
			public bool splittable;
		}

		// Token: 0x02000534 RID: 1332
		private struct StackWork
		{
			// Token: 0x04001891 RID: 6289
			public bool gotFirstUsage;

			// Token: 0x04001892 RID: 6290
			public InventoryItem firstUsage;

			// Token: 0x04001893 RID: 6291
			public int slot;

			// Token: 0x04001894 RID: 6292
			public InventoryItem instance;
		}
	}

	// Token: 0x02000535 RID: 1333
	private class Report
	{
		// Token: 0x06002D3D RID: 11581 RVA: 0x000B4BB4 File Offset: 0x000B2DB4
		private static Inventory.Report Create()
		{
			Inventory.Report report;
			if (Inventory.Report.dumpSize > 0)
			{
				report = Inventory.Report.dump;
				if (--Inventory.Report.dumpSize == 0)
				{
					Inventory.Report.dump = null;
				}
				else
				{
					Inventory.Report.dump = report.dumpNext;
				}
				report.dumpNext = null;
				report.Disposed = false;
				report.amount = 0;
			}
			else
			{
				report = new Inventory.Report();
			}
			return report;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000B4C1C File Offset: 0x000B2E1C
		public static void Begin()
		{
			if (Inventory.Report.begun)
			{
				throw new InvalidOperationException();
			}
			Inventory.Report.begun = true;
			Inventory.Report.totalItemCount = 0;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000B4C3C File Offset: 0x000B2E3C
		public static void Take(InventoryItem item)
		{
			int uses = item.uses;
			int datablockUniqueID = item.datablockUniqueID;
			Inventory.Report report;
			if (Inventory.Report.dict.TryGetValue(datablockUniqueID, out report))
			{
				Inventory.Report report2 = report.first;
				if (report.splittable)
				{
					int num = report2.amount + uses;
					if (num > item.maxUses)
					{
						Inventory.Report report3 = Inventory.Report.Create();
						report3.typeNext = report2;
						report3.amount = num - report.maxUses;
						report3.item = item;
						report2.amount = report.maxUses;
						report.first = report3;
						report.length++;
						Inventory.Report.totalItemCount++;
					}
					else
					{
						report.first.amount = num;
					}
				}
				else
				{
					Inventory.Report report4 = Inventory.Report.Create();
					report4.typeNext = report2;
					report4.amount = uses;
					report4.item = item;
					report.first = report4;
					report.length++;
					Inventory.Report.totalItemCount++;
				}
			}
			else
			{
				ItemDataBlock itemDataBlock = item.datablock;
				if (itemDataBlock.transferable)
				{
					Inventory.Report report5 = Inventory.Report.Create();
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
					Inventory.Report.dict.Add(item.datablockUniqueID, report5);
					Inventory.Report.totalItemCount++;
				}
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000B4DD0 File Offset: 0x000B2FD0
		public static Inventory.Transfer[] Build(Inventory.Slot.KindFlags fallbackKindFlags)
		{
			if (!Inventory.Report.begun)
			{
				throw new InvalidOperationException();
			}
			Inventory.Transfer[] array = new Inventory.Transfer[Inventory.Report.totalItemCount];
			int slotNumber = 0;
			foreach (KeyValuePair<int, Inventory.Report> keyValuePair in Inventory.Report.dict)
			{
				Inventory.Report value = keyValuePair.Value;
				Inventory.Transfer transfer;
				transfer.addition.Ident = (Datablock.Ident)value.datablock;
				int num = value.length;
				value = value.first;
				bool flag = value.splittable;
				for (int i = 0; i < num; i++)
				{
					transfer.addition.SlotPreference = Inventory.Slot.Preference.Define(slotNumber, false, fallbackKindFlags);
					transfer.addition.UsesQuantity = Inventory.Uses.Quantity.Manual(value.amount);
					transfer.item = value.item;
					array[slotNumber++] = transfer;
					Inventory.Report report = value;
					value = value.typeNext;
					if (!report.Disposed)
					{
						report.Disposed = true;
						report.dumpNext = Inventory.Report.dump;
						report.first = (report.typeNext = null);
						report.datablock = null;
						report.item = null;
						Inventory.Report.dump = report;
						Inventory.Report.dumpSize++;
					}
				}
			}
			Inventory.Report.dict.Clear();
			Inventory.Report.begun = false;
			return array;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000B4F60 File Offset: 0x000B3160
		public static void Recover()
		{
			if (Inventory.Report.begun)
			{
				foreach (Inventory.Report report in Inventory.Report.dict.Values)
				{
					if (!report.Disposed)
					{
						report.Disposed = true;
						report.dumpNext = Inventory.Report.dump;
						report.first = (report.typeNext = null);
						report.datablock = null;
						report.item = null;
						Inventory.Report.dump = report;
						Inventory.Report.dumpSize++;
					}
				}
				Inventory.Report.dict.Clear();
			}
		}

		// Token: 0x04001895 RID: 6293
		private int amount;

		// Token: 0x04001896 RID: 6294
		private bool Disposed;

		// Token: 0x04001897 RID: 6295
		private Inventory.Report dumpNext;

		// Token: 0x04001898 RID: 6296
		private Inventory.Report typeNext;

		// Token: 0x04001899 RID: 6297
		private Inventory.Report first;

		// Token: 0x0400189A RID: 6298
		private ItemDataBlock datablock;

		// Token: 0x0400189B RID: 6299
		private InventoryItem item;

		// Token: 0x0400189C RID: 6300
		private bool splittable;

		// Token: 0x0400189D RID: 6301
		private int length;

		// Token: 0x0400189E RID: 6302
		private int maxUses;

		// Token: 0x0400189F RID: 6303
		private static Inventory.Report dump;

		// Token: 0x040018A0 RID: 6304
		private static int dumpSize;

		// Token: 0x040018A1 RID: 6305
		private static readonly Dictionary<int, Inventory.Report> dict = new Dictionary<int, Inventory.Report>();

		// Token: 0x040018A2 RID: 6306
		private static bool begun;

		// Token: 0x040018A3 RID: 6307
		private static int totalItemCount;
	}

	// Token: 0x02000536 RID: 1334
	public static class Slot
	{
		// Token: 0x040018A4 RID: 6308
		public const Inventory.Slot.Kind KindBegin = Inventory.Slot.Kind.Default;

		// Token: 0x040018A5 RID: 6309
		public const Inventory.Slot.Kind KindLast = Inventory.Slot.Kind.Armor;

		// Token: 0x040018A6 RID: 6310
		public const Inventory.Slot.Kind KindFirst = Inventory.Slot.Kind.Default;

		// Token: 0x040018A7 RID: 6311
		public const Inventory.Slot.Kind KindEnd = (Inventory.Slot.Kind)3;

		// Token: 0x040018A8 RID: 6312
		public const int KindCount = 3;

		// Token: 0x040018A9 RID: 6313
		private const Inventory.Slot.Kind HiddenKind_Explicit = (Inventory.Slot.Kind)4;

		// Token: 0x040018AA RID: 6314
		private const Inventory.Slot.Kind HiddenKind_Null = (Inventory.Slot.Kind)5;

		// Token: 0x040018AB RID: 6315
		public const int NumberOfKinds = 3;

		// Token: 0x040018AC RID: 6316
		public const Inventory.Slot.KindFlags KindFlagsMask_Kind = Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor;

		// Token: 0x040018AD RID: 6317
		private const int PrimaryShift = 4;

		// Token: 0x02000537 RID: 1335
		public enum Kind : byte
		{
			// Token: 0x040018AF RID: 6319
			Default,
			// Token: 0x040018B0 RID: 6320
			Belt,
			// Token: 0x040018B1 RID: 6321
			Armor
		}

		// Token: 0x02000538 RID: 1336
		public struct KindDictionary<TValue> : IEnumerable, IDictionary<Inventory.Slot.Kind, TValue>, ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>, IEnumerable<KeyValuePair<Inventory.Slot.Kind, TValue>>
		{
			// Token: 0x06002D42 RID: 11586 RVA: 0x000B5024 File Offset: 0x000B3224
			void IDictionary<Inventory.Slot.Kind, TValue>.Add(Inventory.Slot.Kind key, TValue value)
			{
				if (this.GetMember(key).Defined)
				{
					throw new ArgumentException("Key was already set to a value");
				}
				this.SetMember(key, new Inventory.Slot.KindDictionary<TValue>.Member(value));
				this.count += 1;
			}

			// Token: 0x170009FC RID: 2556
			// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000B506C File Offset: 0x000B326C
			ICollection<Inventory.Slot.Kind> IDictionary<Inventory.Slot.Kind, TValue>.Keys
			{
				get
				{
					Inventory.Slot.Kind[] array = new Inventory.Slot.Kind[(int)this.count];
					int num = 0;
					for (Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default; kind < (Inventory.Slot.Kind)3; kind += 1)
					{
						if (this.GetMember(kind).Defined)
						{
							array[num++] = kind;
						}
					}
					return array;
				}
			}

			// Token: 0x06002D44 RID: 11588 RVA: 0x000B50B8 File Offset: 0x000B32B8
			void ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>.Add(KeyValuePair<Inventory.Slot.Kind, TValue> item)
			{
				this[item.Key] = item.Value;
			}

			// Token: 0x170009FD RID: 2557
			// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000B50D0 File Offset: 0x000B32D0
			ICollection<TValue> IDictionary<Inventory.Slot.Kind, TValue>.Values
			{
				get
				{
					TValue[] array = new TValue[(int)this.count];
					int num = 0;
					for (Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default; kind < (Inventory.Slot.Kind)3; kind += 1)
					{
						Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
						if (member.Defined)
						{
							array[num++] = member.Value;
						}
					}
					return array;
				}
			}

			// Token: 0x06002D46 RID: 11590 RVA: 0x000B5128 File Offset: 0x000B3328
			bool ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>.Contains(KeyValuePair<Inventory.Slot.Kind, TValue> item)
			{
				bool result;
				try
				{
					Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(item.Key);
					if (!member.Defined)
					{
						result = false;
					}
					else
					{
						KeyValuePair<Inventory.Slot.Kind, TValue> keyValuePair = new KeyValuePair<Inventory.Slot.Kind, TValue>(item.Key, member.Value);
						result = object.Equals(keyValuePair, item);
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06002D47 RID: 11591 RVA: 0x000B51B4 File Offset: 0x000B33B4
			void ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>.CopyTo(KeyValuePair<Inventory.Slot.Kind, TValue>[] array, int arrayIndex)
			{
				for (Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default; kind < (Inventory.Slot.Kind)3; kind += 1)
				{
					Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
					if (member.Defined)
					{
						array[arrayIndex++] = new KeyValuePair<Inventory.Slot.Kind, TValue>(kind, member.Value);
					}
				}
			}

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000B5208 File Offset: 0x000B3408
			bool ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06002D49 RID: 11593 RVA: 0x000B520C File Offset: 0x000B340C
			bool ICollection<KeyValuePair<Inventory.Slot.Kind, TValue>>.Remove(KeyValuePair<Inventory.Slot.Kind, TValue> item)
			{
				bool result;
				try
				{
					Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(item.Key);
					if (!member.Defined)
					{
						result = false;
					}
					else
					{
						KeyValuePair<Inventory.Slot.Kind, TValue> keyValuePair = new KeyValuePair<Inventory.Slot.Kind, TValue>(item.Key, member.Value);
						if (object.Equals(keyValuePair, item))
						{
							this.SetMember(item.Key, default(Inventory.Slot.KindDictionary<TValue>.Member));
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

			// Token: 0x06002D4A RID: 11594 RVA: 0x000B52B8 File Offset: 0x000B34B8
			IEnumerator<KeyValuePair<Inventory.Slot.Kind, TValue>> IEnumerable<KeyValuePair<Inventory.Slot.Kind, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002D4B RID: 11595 RVA: 0x000B52C8 File Offset: 0x000B34C8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06002D4C RID: 11596 RVA: 0x000B52D8 File Offset: 0x000B34D8
			private Inventory.Slot.KindDictionary<TValue>.Member GetMember(Inventory.Slot.Kind kind)
			{
				switch (kind)
				{
				case Inventory.Slot.Kind.Default:
					return this.mDefault;
				case Inventory.Slot.Kind.Belt:
					return this.mBelt;
				case Inventory.Slot.Kind.Armor:
					return this.mArmor;
				default:
					throw new ArgumentNullException("Unimplemented kind");
				}
			}

			// Token: 0x06002D4D RID: 11597 RVA: 0x000B5320 File Offset: 0x000B3520
			private void SetMember(Inventory.Slot.Kind kind, Inventory.Slot.KindDictionary<TValue>.Member member)
			{
				switch (kind)
				{
				case Inventory.Slot.Kind.Default:
					this.mDefault = member;
					break;
				case Inventory.Slot.Kind.Belt:
					this.mBelt = member;
					break;
				case Inventory.Slot.Kind.Armor:
					this.mArmor = member;
					break;
				default:
					throw new ArgumentNullException("Unimplemented kind");
				}
			}

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000B5378 File Offset: 0x000B3578
			public int Count
			{
				get
				{
					return (int)this.count;
				}
			}

			// Token: 0x17000A00 RID: 2560
			public TValue this[Inventory.Slot.Kind kind]
			{
				get
				{
					Inventory.Slot.KindDictionary<TValue>.Member member = this.GetMember(kind);
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
						this.SetMember(kind, new Inventory.Slot.KindDictionary<TValue>.Member(value));
						this.count += 1;
					}
					else
					{
						this.SetMember(kind, new Inventory.Slot.KindDictionary<TValue>.Member(value));
					}
				}
			}

			// Token: 0x06002D51 RID: 11601 RVA: 0x000B5404 File Offset: 0x000B3604
			public bool ContainsKey(Inventory.Slot.Kind key)
			{
				return key >= Inventory.Slot.Kind.Default && key < (Inventory.Slot.Kind)3 && this.GetMember(key).Defined;
			}

			// Token: 0x06002D52 RID: 11602 RVA: 0x000B5434 File Offset: 0x000B3634
			public bool Remove(Inventory.Slot.Kind key)
			{
				if (this.GetMember(key).Defined)
				{
					this.SetMember(key, default(Inventory.Slot.KindDictionary<TValue>.Member));
					this.count -= 1;
					return true;
				}
				return false;
			}

			// Token: 0x06002D53 RID: 11603 RVA: 0x000B5478 File Offset: 0x000B3678
			public void Clear()
			{
				Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default;
				while ((int)this.count > 0 && kind < (Inventory.Slot.Kind)3)
				{
					this.Remove(kind);
					kind += 1;
				}
			}

			// Token: 0x06002D54 RID: 11604 RVA: 0x000B54AC File Offset: 0x000B36AC
			public bool TryGetValue(Inventory.Slot.Kind key, out TValue value)
			{
				Inventory.Slot.KindDictionary<TValue>.Member member;
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

			// Token: 0x06002D55 RID: 11605 RVA: 0x000B552C File Offset: 0x000B372C
			public Inventory.Slot.KindDictionary<TValue>.Enumerator GetEnumerator()
			{
				return new Inventory.Slot.KindDictionary<TValue>.Enumerator(this);
			}

			// Token: 0x040018B2 RID: 6322
			private Inventory.Slot.KindDictionary<TValue>.Member mDefault;

			// Token: 0x040018B3 RID: 6323
			private Inventory.Slot.KindDictionary<TValue>.Member mBelt;

			// Token: 0x040018B4 RID: 6324
			private Inventory.Slot.KindDictionary<TValue>.Member mArmor;

			// Token: 0x040018B5 RID: 6325
			private sbyte count;

			// Token: 0x02000539 RID: 1337
			private struct Member
			{
				// Token: 0x06002D56 RID: 11606 RVA: 0x000B553C File Offset: 0x000B373C
				public Member(TValue value)
				{
					this.Value = value;
					this.Defined = true;
				}

				// Token: 0x040018B6 RID: 6326
				public TValue Value;

				// Token: 0x040018B7 RID: 6327
				public bool Defined;
			}

			// Token: 0x0200053A RID: 1338
			public struct Enumerator : IDisposable, IEnumerator, IEnumerator<KeyValuePair<Inventory.Slot.Kind, TValue>>
			{
				// Token: 0x06002D57 RID: 11607 RVA: 0x000B554C File Offset: 0x000B374C
				public Enumerator(Inventory.Slot.KindDictionary<TValue> dict)
				{
					this.dict = dict;
					this.kind = -1;
				}

				// Token: 0x17000A01 RID: 2561
				// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000B555C File Offset: 0x000B375C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06002D59 RID: 11609 RVA: 0x000B556C File Offset: 0x000B376C
				public void Reset()
				{
					this.kind = -1;
				}

				// Token: 0x06002D5A RID: 11610 RVA: 0x000B5578 File Offset: 0x000B3778
				public void Dispose()
				{
					this.dict = default(Inventory.Slot.KindDictionary<TValue>);
				}

				// Token: 0x17000A02 RID: 2562
				// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000B5594 File Offset: 0x000B3794
				public KeyValuePair<Inventory.Slot.Kind, TValue> Current
				{
					get
					{
						Inventory.Slot.KindDictionary<TValue>.Member member = this.dict.GetMember((Inventory.Slot.Kind)this.kind);
						return new KeyValuePair<Inventory.Slot.Kind, TValue>((Inventory.Slot.Kind)this.kind, member.Value);
					}
				}

				// Token: 0x06002D5C RID: 11612 RVA: 0x000B55C8 File Offset: 0x000B37C8
				public bool MoveNext()
				{
					Inventory.Slot.Kind kind;
					while ((kind = (Inventory.Slot.Kind)(++this.kind)) < (Inventory.Slot.Kind)3)
					{
						if (this.dict.GetMember(kind).Defined)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x040018B8 RID: 6328
				private Inventory.Slot.KindDictionary<TValue> dict;

				// Token: 0x040018B9 RID: 6329
				private int kind;
			}
		}

		// Token: 0x0200053B RID: 1339
		[Flags]
		public enum KindFlags : byte
		{
			// Token: 0x040018BB RID: 6331
			Default = 1,
			// Token: 0x040018BC RID: 6332
			Belt = 2,
			// Token: 0x040018BD RID: 6333
			Armor = 4
		}

		// Token: 0x0200053C RID: 1340
		[Flags]
		public enum PreferenceFlags : byte
		{
			// Token: 0x040018BF RID: 6335
			Secondary_Default = 1,
			// Token: 0x040018C0 RID: 6336
			Secondary_Belt = 2,
			// Token: 0x040018C1 RID: 6337
			Secondary_Armor = 4,
			// Token: 0x040018C2 RID: 6338
			Stack = 8,
			// Token: 0x040018C3 RID: 6339
			Primary_Default = 16,
			// Token: 0x040018C4 RID: 6340
			Primary_Belt = 32,
			// Token: 0x040018C5 RID: 6341
			Primary_Armor = 64,
			// Token: 0x040018C6 RID: 6342
			Offset = 128,
			// Token: 0x040018C7 RID: 6343
			Primary_ExplicitSlot = 0
		}

		// Token: 0x0200053D RID: 1341
		public struct Offset
		{
			// Token: 0x06002D5D RID: 11613 RVA: 0x000B5610 File Offset: 0x000B3810
			public Offset(int offset)
			{
				this.offset = (byte)offset;
				this.kind = (Inventory.Slot.Kind)4;
			}

			// Token: 0x06002D5E RID: 11614 RVA: 0x000B5624 File Offset: 0x000B3824
			public Offset(Inventory.Slot.Kind kind, int offset)
			{
				this.kind = kind;
				this.offset = (byte)offset;
			}

			// Token: 0x17000A03 RID: 2563
			// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000B5638 File Offset: 0x000B3838
			public static Inventory.Slot.Offset None
			{
				get
				{
					return new Inventory.Slot.Offset((Inventory.Slot.Kind)5, 0);
				}
			}

			// Token: 0x17000A04 RID: 2564
			// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000B5644 File Offset: 0x000B3844
			public bool Specified
			{
				get
				{
					return this.kind < (Inventory.Slot.Kind)3 || (this.kind >= (Inventory.Slot.Kind)4 && this.kind < (Inventory.Slot.Kind)5);
				}
			}

			// Token: 0x17000A05 RID: 2565
			// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000B5670 File Offset: 0x000B3870
			public bool HasOffsetOfKind
			{
				get
				{
					return this.kind < (Inventory.Slot.Kind)3;
				}
			}

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000B567C File Offset: 0x000B387C
			public bool ExplicitSlot
			{
				get
				{
					return this.kind == (Inventory.Slot.Kind)4;
				}
			}

			// Token: 0x17000A07 RID: 2567
			// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000B5688 File Offset: 0x000B3888
			public Inventory.Slot.Kind OffsetOfKind
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

			// Token: 0x17000A08 RID: 2568
			// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000B56A8 File Offset: 0x000B38A8
			public int SlotOffset
			{
				get
				{
					return (int)this.offset;
				}
			}

			// Token: 0x06002D65 RID: 11621 RVA: 0x000B56B0 File Offset: 0x000B38B0
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

			// Token: 0x040018C8 RID: 6344
			private Inventory.Slot.Kind kind;

			// Token: 0x040018C9 RID: 6345
			private byte offset;
		}

		// Token: 0x0200053E RID: 1342
		public struct Preference
		{
			// Token: 0x06002D66 RID: 11622 RVA: 0x000B5710 File Offset: 0x000B3910
			private Preference(Inventory.Slot.PreferenceFlags preferenceFlags, int primaryOffset)
			{
				this.Flags = preferenceFlags;
				this.offset = (byte)primaryOffset;
			}

			// Token: 0x17000A09 RID: 2569
			// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000B5724 File Offset: 0x000B3924
			public bool IsUndefined
			{
				get
				{
					return (byte)(this.Flags & ~Inventory.Slot.PreferenceFlags.Stack) == 0;
				}
			}

			// Token: 0x17000A0A RID: 2570
			// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000B5738 File Offset: 0x000B3938
			public bool IsDefined
			{
				get
				{
					return (byte)(this.Flags & ~Inventory.Slot.PreferenceFlags.Stack) != 0;
				}
			}

			// Token: 0x17000A0B RID: 2571
			// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000B5750 File Offset: 0x000B3950
			public Inventory.Slot.KindFlags PrimaryKindFlags
			{
				get
				{
					return (Inventory.Slot.KindFlags)((byte)(this.Flags >> 4) & 7);
				}
			}

			// Token: 0x17000A0C RID: 2572
			// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000B5760 File Offset: 0x000B3960
			public Inventory.Slot.KindFlags SecondaryKindFlags
			{
				get
				{
					return (Inventory.Slot.KindFlags)(this.Flags & (Inventory.Slot.PreferenceFlags.Secondary_Default | Inventory.Slot.PreferenceFlags.Secondary_Belt | Inventory.Slot.PreferenceFlags.Secondary_Armor));
				}
			}

			// Token: 0x17000A0D RID: 2573
			// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000B576C File Offset: 0x000B396C
			public bool HasOffset
			{
				get
				{
					return (byte)(this.Flags & Inventory.Slot.PreferenceFlags.Offset) == 128;
				}
			}

			// Token: 0x17000A0E RID: 2574
			// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000B5784 File Offset: 0x000B3984
			public bool Stack
			{
				get
				{
					return (byte)(this.Flags & Inventory.Slot.PreferenceFlags.Stack) == 8;
				}
			}

			// Token: 0x17000A0F RID: 2575
			// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000B5794 File Offset: 0x000B3994
			public Inventory.Slot.Offset Offset
			{
				get
				{
					if ((byte)(this.Flags & Inventory.Slot.PreferenceFlags.Offset) == 128)
					{
						uint num = (uint)((byte)(this.Flags & ~Inventory.Slot.PreferenceFlags.Offset)) >> 4;
						if (num == 0u)
						{
							return new Inventory.Slot.Offset((int)this.offset);
						}
						if ((num & num - 1u) == 0u)
						{
							Inventory.Slot.Kind kind = Inventory.Slot.Kind.Default;
							while ((num >>= 1) != 0u)
							{
								kind += 1;
							}
							return new Inventory.Slot.Offset(kind, (int)this.offset);
						}
					}
					return Inventory.Slot.Offset.None;
				}
			}

			// Token: 0x06002D6E RID: 11630 RVA: 0x000B5808 File Offset: 0x000B3A08
			public Inventory.Slot.Preference CloneOffsetChange(int newOffset)
			{
				return new Inventory.Slot.Preference(this.Flags, newOffset);
			}

			// Token: 0x06002D6F RID: 11631 RVA: 0x000B5818 File Offset: 0x000B3A18
			public Inventory.Slot.Preference CloneStackChange(bool stack)
			{
				if (stack)
				{
					return new Inventory.Slot.Preference(this.Flags | Inventory.Slot.PreferenceFlags.Stack, (int)this.offset);
				}
				return new Inventory.Slot.Preference(this.Flags & ~Inventory.Slot.PreferenceFlags.Stack, (int)this.offset);
			}

			// Token: 0x06002D70 RID: 11632 RVA: 0x000B5850 File Offset: 0x000B3A50
			public static Inventory.Slot.Preference Define(int slotNumber, bool stack, Inventory.Slot.KindFlags fallbackSlots)
			{
				Inventory.Slot.PreferenceFlags preferenceFlags = (Inventory.Slot.PreferenceFlags)(fallbackSlots & (Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor));
				if (stack)
				{
					preferenceFlags |= Inventory.Slot.PreferenceFlags.Stack;
				}
				if (slotNumber >= 0)
				{
					preferenceFlags |= Inventory.Slot.PreferenceFlags.Offset;
				}
				else
				{
					slotNumber = 0;
				}
				return new Inventory.Slot.Preference(preferenceFlags, slotNumber);
			}

			// Token: 0x06002D71 RID: 11633 RVA: 0x000B588C File Offset: 0x000B3A8C
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, bool stack, Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				Inventory.Slot.PreferenceFlags preferenceFlags = (Inventory.Slot.PreferenceFlags)(fallbackSlotKinds & (Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor));
				if (stack)
				{
					preferenceFlags |= Inventory.Slot.PreferenceFlags.Stack;
				}
				if (offsetOfSlotKind >= 0)
				{
					preferenceFlags |= Inventory.Slot.PreferenceFlags.Offset;
				}
				else
				{
					offsetOfSlotKind = 0;
				}
				Inventory.Slot.PreferenceFlags preferenceFlags2 = (Inventory.Slot.PreferenceFlags)(1 << (int)startSlotKind);
				preferenceFlags &= ~preferenceFlags2;
				preferenceFlags |= preferenceFlags2 << 4;
				return new Inventory.Slot.Preference(preferenceFlags, offsetOfSlotKind);
			}

			// Token: 0x06002D72 RID: 11634 RVA: 0x000B58E0 File Offset: 0x000B3AE0
			public static Inventory.Slot.Preference Define(int offsetOfSlotKind, bool stack)
			{
				return Inventory.Slot.Preference.Define(offsetOfSlotKind, stack, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D73 RID: 11635 RVA: 0x000B58EC File Offset: 0x000B3AEC
			public static Inventory.Slot.Preference Define(int offsetOfSlotKind, Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				return Inventory.Slot.Preference.Define(offsetOfSlotKind, true, fallbackSlotKinds);
			}

			// Token: 0x06002D74 RID: 11636 RVA: 0x000B58F8 File Offset: 0x000B3AF8
			public static Inventory.Slot.Preference Define(int offsetOfSlotKind, Inventory.Slot.Kind fallbackSlotKind)
			{
				return Inventory.Slot.Preference.Define(offsetOfSlotKind, true, (Inventory.Slot.KindFlags)(1 << (int)fallbackSlotKind));
			}

			// Token: 0x06002D75 RID: 11637 RVA: 0x000B5908 File Offset: 0x000B3B08
			public static Inventory.Slot.Preference Define(int offsetOfSlotKind)
			{
				return Inventory.Slot.Preference.Define(offsetOfSlotKind, true, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D76 RID: 11638 RVA: 0x000B5914 File Offset: 0x000B3B14
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, bool stack)
			{
				return Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, stack, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D77 RID: 11639 RVA: 0x000B5920 File Offset: 0x000B3B20
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, Inventory.Slot.KindFlags fallbackSlotKinds)
			{
				return Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, fallbackSlotKinds);
			}

			// Token: 0x06002D78 RID: 11640 RVA: 0x000B592C File Offset: 0x000B3B2C
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind, Inventory.Slot.Kind fallbackSlotKind)
			{
				return Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, (Inventory.Slot.KindFlags)(1 << (int)fallbackSlotKind));
			}

			// Token: 0x06002D79 RID: 11641 RVA: 0x000B5940 File Offset: 0x000B3B40
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind startSlotKind, int offsetOfSlotKind)
			{
				return Inventory.Slot.Preference.Define(startSlotKind, offsetOfSlotKind, true, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D7A RID: 11642 RVA: 0x000B594C File Offset: 0x000B3B4C
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags firstPreferenceSlotKinds, bool stack, Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				Inventory.Slot.PreferenceFlags preferenceFlags = (Inventory.Slot.PreferenceFlags)((byte)(secondPreferenceSlotKinds & (Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor)) & (byte)(~(byte)firstPreferenceSlotKinds));
				if (stack)
				{
					preferenceFlags |= Inventory.Slot.PreferenceFlags.Stack;
				}
				preferenceFlags |= (Inventory.Slot.PreferenceFlags)(firstPreferenceSlotKinds << 4);
				return new Inventory.Slot.Preference(preferenceFlags, 0);
			}

			// Token: 0x06002D7B RID: 11643 RVA: 0x000B5980 File Offset: 0x000B3B80
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind firstPreferenceSlotKind, bool stack, Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return Inventory.Slot.Preference.Define((Inventory.Slot.KindFlags)(1 << (int)firstPreferenceSlotKind), stack, secondPreferenceSlotKinds);
			}

			// Token: 0x06002D7C RID: 11644 RVA: 0x000B5990 File Offset: 0x000B3B90
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind firstPreferenceSlotKind, bool stack, Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return Inventory.Slot.Preference.Define((Inventory.Slot.KindFlags)(1 << (int)firstPreferenceSlotKind), stack, (Inventory.Slot.KindFlags)(1 << (int)secondPreferenceSlotKind));
			}

			// Token: 0x06002D7D RID: 11645 RVA: 0x000B59A8 File Offset: 0x000B3BA8
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags firstPreferenceSlotKind, bool stack, Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return Inventory.Slot.Preference.Define(firstPreferenceSlotKind, stack, (Inventory.Slot.KindFlags)(1 << (int)secondPreferenceSlotKind));
			}

			// Token: 0x06002D7E RID: 11646 RVA: 0x000B59B8 File Offset: 0x000B3BB8
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind slotsOfKind, bool stack)
			{
				return Inventory.Slot.Preference.Define(slotsOfKind, stack, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D7F RID: 11647 RVA: 0x000B59C4 File Offset: 0x000B3BC4
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags slotsOfKinds, bool stack)
			{
				return Inventory.Slot.Preference.Define(slotsOfKinds, stack, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D80 RID: 11648 RVA: 0x000B59D0 File Offset: 0x000B3BD0
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind firstPreferenceSlotKind, Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return Inventory.Slot.Preference.Define(firstPreferenceSlotKind, true, secondPreferenceSlotKind);
			}

			// Token: 0x06002D81 RID: 11649 RVA: 0x000B59DC File Offset: 0x000B3BDC
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags firstPreferenceSlotKinds, Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return Inventory.Slot.Preference.Define(firstPreferenceSlotKinds, true, secondPreferenceSlotKinds);
			}

			// Token: 0x06002D82 RID: 11650 RVA: 0x000B59E8 File Offset: 0x000B3BE8
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind firstPreferenceSlotKind, Inventory.Slot.KindFlags secondPreferenceSlotKinds)
			{
				return Inventory.Slot.Preference.Define(firstPreferenceSlotKind, true, secondPreferenceSlotKinds);
			}

			// Token: 0x06002D83 RID: 11651 RVA: 0x000B59F4 File Offset: 0x000B3BF4
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags firstPreferenceSlotKinds, Inventory.Slot.Kind secondPreferenceSlotKind)
			{
				return Inventory.Slot.Preference.Define(firstPreferenceSlotKinds, true, secondPreferenceSlotKind);
			}

			// Token: 0x06002D84 RID: 11652 RVA: 0x000B5A00 File Offset: 0x000B3C00
			public static Inventory.Slot.Preference Define(Inventory.Slot.Kind slotsOfKind)
			{
				return Inventory.Slot.Preference.Define(slotsOfKind, true, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D85 RID: 11653 RVA: 0x000B5A0C File Offset: 0x000B3C0C
			public static Inventory.Slot.Preference Define(Inventory.Slot.KindFlags slotsOfKinds)
			{
				return Inventory.Slot.Preference.Define(slotsOfKinds, true, (Inventory.Slot.KindFlags)0);
			}

			// Token: 0x06002D86 RID: 11654 RVA: 0x000B5A18 File Offset: 0x000B3C18
			public override string ToString()
			{
				Inventory.Slot.KindFlags primaryKindFlags = this.PrimaryKindFlags;
				Inventory.Slot.KindFlags secondaryKindFlags = this.SecondaryKindFlags;
				Inventory.Slot.Offset offset = this.Offset;
				if (secondaryKindFlags != (Inventory.Slot.KindFlags)0)
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
					else if (primaryKindFlags != (Inventory.Slot.KindFlags)0)
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
					if (primaryKindFlags == (Inventory.Slot.KindFlags)0)
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

			// Token: 0x06002D87 RID: 11655 RVA: 0x000B5C40 File Offset: 0x000B3E40
			public static implicit operator Inventory.Slot.Preference(int slot)
			{
				return new Inventory.Slot.Preference(Inventory.Slot.PreferenceFlags.Stack | Inventory.Slot.PreferenceFlags.Offset, (int)((byte)slot));
			}

			// Token: 0x06002D88 RID: 11656 RVA: 0x000B5C50 File Offset: 0x000B3E50
			public static implicit operator Inventory.Slot.Preference(Inventory.Slot.Kind kind)
			{
				return new Inventory.Slot.Preference((Inventory.Slot.PreferenceFlags)((byte)(((byte)(1 << (int)kind) & 7) << 4) | 8), 0);
			}

			// Token: 0x06002D89 RID: 11657 RVA: 0x000B5C68 File Offset: 0x000B3E68
			public static implicit operator Inventory.Slot.Preference(Inventory.Slot.KindFlags kindFlags)
			{
				return new Inventory.Slot.Preference((Inventory.Slot.PreferenceFlags)((byte)((byte)(kindFlags & (Inventory.Slot.KindFlags.Default | Inventory.Slot.KindFlags.Belt | Inventory.Slot.KindFlags.Armor)) << 4) | 8), 0);
			}

			// Token: 0x040018CA RID: 6346
			private const bool kDefaultStack = true;

			// Token: 0x040018CB RID: 6347
			public readonly Inventory.Slot.PreferenceFlags Flags;

			// Token: 0x040018CC RID: 6348
			private readonly byte offset;
		}

		// Token: 0x0200053F RID: 1343
		public struct Range
		{
			// Token: 0x06002D8A RID: 11658 RVA: 0x000B5C7C File Offset: 0x000B3E7C
			public Range(int start, int length)
			{
				this.Start = start;
				this.Count = length;
			}

			// Token: 0x17000A10 RID: 2576
			// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000B5C8C File Offset: 0x000B3E8C
			public int End
			{
				get
				{
					return this.Start + this.Count;
				}
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000B5C9C File Offset: 0x000B3E9C
			public int Last
			{
				get
				{
					return (this.Count > 1) ? (this.Start + (this.Count - 1)) : this.Start;
				}
			}

			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000B5CD0 File Offset: 0x000B3ED0
			public bool Any
			{
				get
				{
					return this.Count > 0;
				}
			}

			// Token: 0x06002D8E RID: 11662 RVA: 0x000B5CDC File Offset: 0x000B3EDC
			public bool Contains(int i)
			{
				return this.Count > 0 && (this.Start == i || (this.Start < i && this.Start + this.Count > i));
			}

			// Token: 0x06002D8F RID: 11663 RVA: 0x000B5D1C File Offset: 0x000B3F1C
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

			// Token: 0x06002D90 RID: 11664 RVA: 0x000B5D50 File Offset: 0x000B3F50
			public int Gouge(int i, out Inventory.Slot.RangePair pair)
			{
				if (this.Count <= 0 || (this.Count == 1 && i == this.Start))
				{
					pair = default(Inventory.Slot.RangePair);
					return 0;
				}
				if (i < this.Start || i >= this.Start + this.Count)
				{
					pair = new Inventory.Slot.RangePair(this);
					return 1;
				}
				if (i == this.Start)
				{
					pair = new Inventory.Slot.RangePair(new Inventory.Slot.Range(this.Start + 1, this.Count - 1));
					return 1;
				}
				if (i == this.Start + this.Count - 1)
				{
					pair = new Inventory.Slot.RangePair(new Inventory.Slot.Range(this.Start, this.Count - 1));
					return 1;
				}
				pair = new Inventory.Slot.RangePair(new Inventory.Slot.Range(this.Start, i - this.Start), new Inventory.Slot.Range(i + 1, this.Count - (i - this.Start + 1)));
				return 2;
			}

			// Token: 0x06002D91 RID: 11665 RVA: 0x000B5E4C File Offset: 0x000B404C
			public int Index(int offset)
			{
				int num = this.Start + offset;
				return (!this.Contains(num)) ? -1 : num;
			}

			// Token: 0x06002D92 RID: 11666 RVA: 0x000B5E78 File Offset: 0x000B4078
			public int GetOffset(int i)
			{
				if (this.Contains(i))
				{
					return i - this.Start;
				}
				return -1;
			}

			// Token: 0x06002D93 RID: 11667 RVA: 0x000B5E90 File Offset: 0x000B4090
			public override string ToString()
			{
				return string.Format("[{0}:{1}]", this.Start, this.Count);
			}

			// Token: 0x040018CD RID: 6349
			public readonly int Start;

			// Token: 0x040018CE RID: 6350
			public readonly int Count;
		}

		// Token: 0x02000540 RID: 1344
		public struct RangePair
		{
			// Token: 0x06002D94 RID: 11668 RVA: 0x000B5EC0 File Offset: 0x000B40C0
			public RangePair(Inventory.Slot.Range A, Inventory.Slot.Range B)
			{
				this.A = A;
				this.B = B;
			}

			// Token: 0x06002D95 RID: 11669 RVA: 0x000B5ED0 File Offset: 0x000B40D0
			public RangePair(Inventory.Slot.Range AB)
			{
				this.A = AB;
				this.B = AB;
			}

			// Token: 0x040018CF RID: 6351
			public readonly Inventory.Slot.Range A;

			// Token: 0x040018D0 RID: 6352
			public readonly Inventory.Slot.Range B;
		}
	}

	// Token: 0x02000541 RID: 1345
	[Flags]
	public enum SlotFlags
	{
		// Token: 0x040018D2 RID: 6354
		Belt = 1,
		// Token: 0x040018D3 RID: 6355
		Storage = 2,
		// Token: 0x040018D4 RID: 6356
		Equip = 4,
		// Token: 0x040018D5 RID: 6357
		Head = 8,
		// Token: 0x040018D6 RID: 6358
		Chest = 16,
		// Token: 0x040018D7 RID: 6359
		Legs = 32,
		// Token: 0x040018D8 RID: 6360
		Feet = 64,
		// Token: 0x040018D9 RID: 6361
		FuelBasic = 128,
		// Token: 0x040018DA RID: 6362
		Debris = 256,
		// Token: 0x040018DB RID: 6363
		Raw = 512,
		// Token: 0x040018DC RID: 6364
		Cooked = 1024,
		// Token: 0x040018DD RID: 6365
		Safe = -2147483648
	}

	// Token: 0x02000542 RID: 1346
	public struct Transfer
	{
		// Token: 0x040018DE RID: 6366
		public InventoryItem item;

		// Token: 0x040018DF RID: 6367
		public Inventory.Addition addition;
	}

	// Token: 0x02000543 RID: 1347
	public static class Uses
	{
		// Token: 0x02000544 RID: 1348
		public enum Quantifier : byte
		{
			// Token: 0x040018E1 RID: 6369
			Default,
			// Token: 0x040018E2 RID: 6370
			Manual,
			// Token: 0x040018E3 RID: 6371
			Minimum,
			// Token: 0x040018E4 RID: 6372
			Maximum,
			// Token: 0x040018E5 RID: 6373
			StackSize,
			// Token: 0x040018E6 RID: 6374
			Random
		}

		// Token: 0x02000545 RID: 1349
		public struct Quantity
		{
			// Token: 0x06002D96 RID: 11670 RVA: 0x000B5EE0 File Offset: 0x000B40E0
			private Quantity(Inventory.Uses.Quantifier quantifier, byte manualAmount)
			{
				this.Quantifier = quantifier;
				this.manualAmount = manualAmount;
			}

			// Token: 0x17000A13 RID: 2579
			// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000B5F30 File Offset: 0x000B4130
			public int ManualAmount
			{
				get
				{
					if (this.Quantifier == Inventory.Uses.Quantifier.Manual)
					{
						return (int)this.manualAmount;
					}
					return -1;
				}
			}

			// Token: 0x06002D99 RID: 11673 RVA: 0x000B5F48 File Offset: 0x000B4148
			public static Inventory.Uses.Quantity Manual(int amount)
			{
				return new Inventory.Uses.Quantity(Inventory.Uses.Quantifier.Manual, (byte)amount);
			}

			// Token: 0x06002D9A RID: 11674 RVA: 0x000B5F54 File Offset: 0x000B4154
			public int CalculateCount(ItemDataBlock datablock)
			{
				switch (this.Quantifier)
				{
				case Inventory.Uses.Quantifier.Default:
					return datablock._spawnUsesMin + (datablock._spawnUsesMax - datablock._spawnUsesMin) / 2;
				case Inventory.Uses.Quantifier.Manual:
					return (this.manualAmount != 0) ? (((int)this.manualAmount <= datablock._maxUses) ? ((int)this.manualAmount) : datablock._maxUses) : 1;
				case Inventory.Uses.Quantifier.Minimum:
					return datablock._spawnUsesMin;
				case Inventory.Uses.Quantifier.Maximum:
					return datablock._spawnUsesMax;
				case Inventory.Uses.Quantifier.StackSize:
					return datablock._maxUses;
				case Inventory.Uses.Quantifier.Random:
					return UnityEngine.Random.Range(datablock._spawnUsesMin, datablock._spawnUsesMax + 1);
				default:
					throw new NotImplementedException();
				}
			}

			// Token: 0x06002D9B RID: 11675 RVA: 0x000B6004 File Offset: 0x000B4204
			public override string ToString()
			{
				if (this.Quantifier == Inventory.Uses.Quantifier.Manual)
				{
					return this.manualAmount.ToString();
				}
				return this.Quantifier.ToString();
			}

			// Token: 0x06002D9C RID: 11676 RVA: 0x000B603C File Offset: 0x000B423C
			public static bool TryParse(string text, out Inventory.Uses.Quantity uses)
			{
				int num;
				if (int.TryParse(text, out num))
				{
					if (num == 0)
					{
						uses = Inventory.Uses.Quantity.Random;
					}
					else if (num < 0)
					{
						uses = Inventory.Uses.Quantity.Minimum;
					}
					else if (num > 255)
					{
						uses = Inventory.Uses.Quantity.Maximum;
					}
					else
					{
						uses = num;
					}
					return true;
				}
				if (string.Equals(text, "min", StringComparison.InvariantCultureIgnoreCase))
				{
					uses = Inventory.Uses.Quantity.Minimum;
					return true;
				}
				if (string.Equals(text, "max", StringComparison.InvariantCultureIgnoreCase))
				{
					uses = Inventory.Uses.Quantity.Maximum;
					return true;
				}
				bool result;
				try
				{
					switch ((byte)Enum.Parse(typeof(Inventory.Uses.Quantifier), text, true))
					{
					case 0:
						uses = Inventory.Uses.Quantity.Default;
						return true;
					case 2:
						uses = Inventory.Uses.Quantity.Minimum;
						return true;
					case 3:
						uses = Inventory.Uses.Quantity.Maximum;
						return true;
					case 5:
						uses = Inventory.Uses.Quantity.Random;
						return true;
					}
					throw new NotImplementedException();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					uses = Inventory.Uses.Quantity.Default;
					result = false;
				}
				return result;
			}

			// Token: 0x06002D9D RID: 11677 RVA: 0x000B61B0 File Offset: 0x000B43B0
			public static implicit operator Inventory.Uses.Quantity(int amount)
			{
				return Inventory.Uses.Quantity.Manual(amount);
			}

			// Token: 0x040018E7 RID: 6375
			public readonly Inventory.Uses.Quantifier Quantifier;

			// Token: 0x040018E8 RID: 6376
			private readonly byte manualAmount;

			// Token: 0x040018E9 RID: 6377
			public static readonly Inventory.Uses.Quantity Default = new Inventory.Uses.Quantity(Inventory.Uses.Quantifier.Default, 0);

			// Token: 0x040018EA RID: 6378
			public static readonly Inventory.Uses.Quantity Minimum = new Inventory.Uses.Quantity(Inventory.Uses.Quantifier.Minimum, 0);

			// Token: 0x040018EB RID: 6379
			public static readonly Inventory.Uses.Quantity Maximum = new Inventory.Uses.Quantity(Inventory.Uses.Quantifier.Maximum, 0);

			// Token: 0x040018EC RID: 6380
			public static readonly Inventory.Uses.Quantity Random = new Inventory.Uses.Quantity(Inventory.Uses.Quantifier.Random, 0);
		}
	}

	// Token: 0x02000546 RID: 1350
	public struct VacantIterator : IDisposable
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x000B61B8 File Offset: 0x000B43B8
		public VacantIterator(Inventory inventory)
		{
			this.baseEnumerator = inventory.collection.VacantEnumerator;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000B61CC File Offset: 0x000B43CC
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000B61DC File Offset: 0x000B43DC
		public int slot
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B61EC File Offset: 0x000B43EC
		public bool Next()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B61FC File Offset: 0x000B43FC
		public void Dispose()
		{
			this.baseEnumerator.Dispose();
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B620C File Offset: 0x000B440C
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

		// Token: 0x040018ED RID: 6381
		private Inventory.Collection<InventoryItem>.VacantCollection.Enumerator baseEnumerator;
	}

	// Token: 0x02000547 RID: 1351
	private static class Empty
	{
		// Token: 0x040018EE RID: 6382
		public static readonly Inventory.SlotFlags[] SlotFlags = new Inventory.SlotFlags[0];
	}

	// Token: 0x02000548 RID: 1352
	private static class Shuffle
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B6248 File Offset: 0x000B4448
		public static void Array<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = Inventory.Shuffle.r.Next(i);
				if (num != i)
				{
					T t = array[i];
					array[i] = array[num];
					array[num] = t;
				}
			}
		}

		// Token: 0x040018EF RID: 6383
		private static readonly Random r = new Random();
	}

	// Token: 0x02000549 RID: 1353
	public enum SlotOperationResult : sbyte
	{
		// Token: 0x040018F1 RID: 6385
		NoOp,
		// Token: 0x040018F2 RID: 6386
		Success_Stacked,
		// Token: 0x040018F3 RID: 6387
		Success_Combined,
		// Token: 0x040018F4 RID: 6388
		Success_Moved = 4,
		// Token: 0x040018F5 RID: 6389
		Error_OccupiedDestination = -8,
		// Token: 0x040018F6 RID: 6390
		Error_SameSlot,
		// Token: 0x040018F7 RID: 6391
		Error_MissingInventory,
		// Token: 0x040018F8 RID: 6392
		Error_EmptySourceSlot,
		// Token: 0x040018F9 RID: 6393
		Error_EmptyDestinationSlot,
		// Token: 0x040018FA RID: 6394
		Error_SlotRange,
		// Token: 0x040018FB RID: 6395
		Error_NoOpArgs,
		// Token: 0x040018FC RID: 6396
		Error_Failed
	}

	// Token: 0x0200054A RID: 1354
	private enum SlotOperations : byte
	{
		// Token: 0x040018FE RID: 6398
		Stack = 1,
		// Token: 0x040018FF RID: 6399
		Combine,
		// Token: 0x04001900 RID: 6400
		Move = 4
	}

	// Token: 0x0200054B RID: 1355
	private struct SlotOperationsInfo
	{
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B62A4 File Offset: 0x000B44A4
		public SlotOperationsInfo(Inventory.SlotOperations SlotOperations)
		{
			this.SlotOperations = SlotOperations;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B62B0 File Offset: 0x000B44B0
		public override string ToString()
		{
			return this.SlotOperations.ToString();
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B62C4 File Offset: 0x000B44C4
		public override bool Equals(object obj)
		{
			return obj is Inventory.SlotOperationsInfo && this.Equals((Inventory.SlotOperationsInfo)obj);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B62E0 File Offset: 0x000B44E0
		public override int GetHashCode()
		{
			return (int)((byte)(this.SlotOperations & (Inventory.SlotOperations)7)) << 16;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B62F0 File Offset: 0x000B44F0
		public bool Equals(Inventory.SlotOperationsInfo other)
		{
			return (byte)(this.SlotOperations & (Inventory.SlotOperations)7) == (byte)(other.SlotOperations & (Inventory.SlotOperations)7);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000B6308 File Offset: 0x000B4508
		public static implicit operator Inventory.SlotOperationsInfo(Inventory.SlotOperations ops)
		{
			return new Inventory.SlotOperationsInfo(ops);
		}

		// Token: 0x04001901 RID: 6401
		[NonSerialized]
		public readonly Inventory.SlotOperations SlotOperations;
	}
}
