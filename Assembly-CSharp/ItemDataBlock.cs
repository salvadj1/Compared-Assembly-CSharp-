using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000578 RID: 1400
public class ItemDataBlock : Datablock, IComparable<ItemDataBlock>
{
	// Token: 0x060030F1 RID: 12529 RVA: 0x000BB054 File Offset: 0x000B9254
	int IComparable<ItemDataBlock>.CompareTo(ItemDataBlock other)
	{
		return this.CompareTo(other);
	}

	// Token: 0x060030F2 RID: 12530 RVA: 0x000BB060 File Offset: 0x000B9260
	protected virtual IInventoryItem ConstructItem()
	{
		return new ItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060030F3 RID: 12531 RVA: 0x000BB068 File Offset: 0x000B9268
	public IInventoryItem CreateItem()
	{
		IInventoryItem inventoryItem = this.ConstructItem();
		this.InstallData(inventoryItem);
		return inventoryItem;
	}

	// Token: 0x060030F4 RID: 12532 RVA: 0x000BB084 File Offset: 0x000B9284
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<int>((int)this._itemFlags, new object[0]);
		stream.Write<int>(this._maxUses, new object[0]);
		stream.Write<bool>(this._splittable, new object[0]);
		stream.Write<byte>((byte)this.transientMode, new object[0]);
		stream.Write<bool>(this.isResearchable, new object[0]);
		stream.Write<bool>(this.isResearchable, new object[0]);
		stream.Write<bool>(this.isRecycleable, new object[0]);
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x000BB118 File Offset: 0x000B9318
	public virtual void InstallData(IInventoryItem item)
	{
		item.SetUses(1);
		item.SetMaxCondition(1f);
		item.SetCondition(1f);
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x000BB138 File Offset: 0x000B9338
	protected virtual void PreInstallJsonProperties(IInventoryItem item)
	{
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x000BB13C File Offset: 0x000B933C
	protected virtual void PostInstallJsonProperties(IInventoryItem item)
	{
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x000BB140 File Offset: 0x000B9340
	public static bool LoadIconOrUnknown<TTex>(string iconPath, ref TTex tex) where TTex : Texture
	{
		return tex || ItemDataBlock.LoadIconOrUnknownForced<TTex>(iconPath, out tex);
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x000BB164 File Offset: 0x000B9364
	public static bool LoadIconOrUnknownForced<TTex>(string iconPath, out TTex tex) where TTex : Texture
	{
		return Bundling.Load<TTex>(iconPath, out tex) || Bundling.Load<TTex>("content/item/tex/unknown", out tex);
	}

	// Token: 0x17000A30 RID: 2608
	// (get) Token: 0x060030FA RID: 12538 RVA: 0x000BB180 File Offset: 0x000B9380
	public bool doesNotSave
	{
		get
		{
			return (this.transientMode & ItemDataBlock.TransientMode.DoesNotSave) == ItemDataBlock.TransientMode.DoesNotSave;
		}
	}

	// Token: 0x17000A31 RID: 2609
	// (get) Token: 0x060030FB RID: 12539 RVA: 0x000BB190 File Offset: 0x000B9390
	public bool untransferable
	{
		get
		{
			return (this.transientMode & ItemDataBlock.TransientMode.Untransferable) == ItemDataBlock.TransientMode.Untransferable;
		}
	}

	// Token: 0x17000A32 RID: 2610
	// (get) Token: 0x060030FC RID: 12540 RVA: 0x000BB1A0 File Offset: 0x000B93A0
	public bool saves
	{
		get
		{
			return (this.transientMode & ItemDataBlock.TransientMode.DoesNotSave) != ItemDataBlock.TransientMode.DoesNotSave;
		}
	}

	// Token: 0x17000A33 RID: 2611
	// (get) Token: 0x060030FD RID: 12541 RVA: 0x000BB1B0 File Offset: 0x000B93B0
	public bool transferable
	{
		get
		{
			return (this.transientMode & ItemDataBlock.TransientMode.Untransferable) != ItemDataBlock.TransientMode.Untransferable;
		}
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x000BB1C0 File Offset: 0x000B93C0
	public virtual byte GetMaxEligableSlots()
	{
		return 0;
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x000BB1C4 File Offset: 0x000B93C4
	public int GetRandomSpawnUses()
	{
		return Random.Range(this._spawnUsesMin, this._spawnUsesMax + 1);
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x000BB1DC File Offset: 0x000B93DC
	public virtual bool IsSplittable()
	{
		return this._splittable;
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x000BB1E4 File Offset: 0x000B93E4
	public bool DoesLoseCondition()
	{
		return this.doesLoseCondition;
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x000BB1EC File Offset: 0x000B93EC
	public int GetMinUsesForDisplay()
	{
		return this._minUsesForDisplay;
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x000BB1F4 File Offset: 0x000B93F4
	public virtual string GetItemDescription()
	{
		if (this.itemDescriptionOverride.Length > 0)
		{
			return this.itemDescriptionOverride;
		}
		return "No item description available";
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x000BB214 File Offset: 0x000B9414
	public virtual int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		if (this._splittable && item.uses > 1 && item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Split;
		}
		return offset;
	}

	// Token: 0x06003105 RID: 12549 RVA: 0x000BB250 File Offset: 0x000B9450
	public virtual InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option == InventoryItem.MenuItem.Info)
		{
			RPOS.OpenInfoWindow(this);
			return InventoryItem.MenuItemResult.DoneOnClient;
		}
		if (option != InventoryItem.MenuItem.Split)
		{
			return InventoryItem.MenuItemResult.Unhandled;
		}
		item.inventory.SplitStack(item.slot);
		return InventoryItem.MenuItemResult.Complete;
	}

	// Token: 0x06003106 RID: 12550 RVA: 0x000BB290 File Offset: 0x000B9490
	public Texture GetIconTexture()
	{
		if (!this.iconTex && !Bundling.Load<Texture>(this.icon, out this.iconTex))
		{
			Bundling.Load<Texture>("content/item/tex/unknown", out this.iconTex);
		}
		return this.iconTex;
	}

	// Token: 0x06003107 RID: 12551 RVA: 0x000BB2D0 File Offset: 0x000B94D0
	public ItemDataBlock.CombineRecipe GetMatchingRecipe(ItemDataBlock db)
	{
		if (this.Combinations == null || this.Combinations.Length == 0)
		{
			return null;
		}
		foreach (ItemDataBlock.CombineRecipe combineRecipe in this.Combinations)
		{
			if (combineRecipe.droppedOnType == db)
			{
				return combineRecipe;
			}
		}
		return null;
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x000BB32C File Offset: 0x000B952C
	public void ConfigureItemPickup(ItemPickup pickup, int amount)
	{
	}

	// Token: 0x06003109 RID: 12553 RVA: 0x000BB330 File Offset: 0x000B9530
	public virtual void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem item)
	{
		infoWindow.AddItemTitle(this, item, 0f);
		infoWindow.AddConditionInfo(item);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x0600310A RID: 12554 RVA: 0x000BB35C File Offset: 0x000B955C
	public virtual void OnItemEvent(InventoryItem.ItemEvent itemEvent)
	{
		switch (itemEvent)
		{
		case InventoryItem.ItemEvent.Equipped:
			if (this.equippedSound)
			{
				this.equippedSound.Play(1f);
			}
			break;
		case InventoryItem.ItemEvent.UnEquipped:
			if (this.unEquippedSound)
			{
				this.unEquippedSound.Play(1f);
			}
			break;
		case InventoryItem.ItemEvent.Combined:
			if (this.combinedSound)
			{
				this.combinedSound.Play(1f);
			}
			break;
		case InventoryItem.ItemEvent.Used:
			if (this.UsedSound)
			{
				this.UsedSound.Play(1f);
			}
			break;
		}
	}

	// Token: 0x04001993 RID: 6547
	public const string kUnknownIconPath = "content/item/tex/unknown";

	// Token: 0x04001994 RID: 6548
	public string icon;

	// Token: 0x04001995 RID: 6549
	[HideInInspector]
	[NonSerialized]
	public Texture iconTex;

	// Token: 0x04001996 RID: 6550
	public int _maxUses = 1;

	// Token: 0x04001997 RID: 6551
	public int _spawnUsesMin = 1;

	// Token: 0x04001998 RID: 6552
	public int _spawnUsesMax = 1;

	// Token: 0x04001999 RID: 6553
	public int _minUsesForDisplay = 1;

	// Token: 0x0400199A RID: 6554
	public float _maxCondition = 1f;

	// Token: 0x0400199B RID: 6555
	public bool _splittable;

	// Token: 0x0400199C RID: 6556
	[HideInInspector]
	public Inventory.SlotFlags _itemFlags;

	// Token: 0x0400199D RID: 6557
	public bool isResearchable = true;

	// Token: 0x0400199E RID: 6558
	public bool isRepairable = true;

	// Token: 0x0400199F RID: 6559
	public bool isRecycleable = true;

	// Token: 0x040019A0 RID: 6560
	public bool doesLoseCondition;

	// Token: 0x040019A1 RID: 6561
	public ItemDataBlock.ItemCategory category = ItemDataBlock.ItemCategory.Misc;

	// Token: 0x040019A2 RID: 6562
	public string itemDescriptionOverride = string.Empty;

	// Token: 0x040019A3 RID: 6563
	public AudioClip equippedSound;

	// Token: 0x040019A4 RID: 6564
	public AudioClip unEquippedSound;

	// Token: 0x040019A5 RID: 6565
	public AudioClip combinedSound;

	// Token: 0x040019A6 RID: 6566
	public AudioClip UsedSound;

	// Token: 0x040019A7 RID: 6567
	public ItemDataBlock.CombineRecipe[] Combinations;

	// Token: 0x040019A8 RID: 6568
	public ItemDataBlock.TransientMode transientMode;

	// Token: 0x02000579 RID: 1401
	private sealed class ITEM_TYPE : InventoryItem<ItemDataBlock>, IInventoryItem
	{
		// Token: 0x0600310B RID: 12555 RVA: 0x000BB41C File Offset: 0x000B961C
		public ITEM_TYPE(ItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000BB428 File Offset: 0x000B9628
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000BB430 File Offset: 0x000B9630
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000BB438 File Offset: 0x000B9638
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000BB440 File Offset: 0x000B9640
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000BB448 File Offset: 0x000B9648
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000BB454 File Offset: 0x000B9654
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000BB460 File Offset: 0x000B9660
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000BB46C File Offset: 0x000B966C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000BB478 File Offset: 0x000B9678
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000BB484 File Offset: 0x000B9684
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000BB490 File Offset: 0x000B9690
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000BB49C File Offset: 0x000B969C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000BB4A8 File Offset: 0x000B96A8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000BB4B0 File Offset: 0x000B96B0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000BB4B8 File Offset: 0x000B96B8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000BB4C0 File Offset: 0x000B96C0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000BB4C8 File Offset: 0x000B96C8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000BB4D0 File Offset: 0x000B96D0
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000BB4D8 File Offset: 0x000B96D8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000BB4E0 File Offset: 0x000B96E0
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000BB4E8 File Offset: 0x000B96E8
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000BB4F4 File Offset: 0x000B96F4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000BB4FC File Offset: 0x000B96FC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000BB504 File Offset: 0x000B9704
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000BB50C File Offset: 0x000B970C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000BB514 File Offset: 0x000B9714
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000BB51C File Offset: 0x000B971C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000BB524 File Offset: 0x000B9724
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200057A RID: 1402
	[Serializable]
	public class CombineRecipe
	{
		// Token: 0x040019A9 RID: 6569
		public ItemDataBlock droppedOnType;

		// Token: 0x040019AA RID: 6570
		public ItemDataBlock resultItem;

		// Token: 0x040019AB RID: 6571
		public int amountToLose = 1;

		// Token: 0x040019AC RID: 6572
		public int amountToLoseOther = 1;

		// Token: 0x040019AD RID: 6573
		public int amountToGive = 1;
	}

	// Token: 0x0200057B RID: 1403
	[Serializable]
	public enum ItemCategory
	{
		// Token: 0x040019AF RID: 6575
		Survival,
		// Token: 0x040019B0 RID: 6576
		Weapons,
		// Token: 0x040019B1 RID: 6577
		Ammo,
		// Token: 0x040019B2 RID: 6578
		Misc,
		// Token: 0x040019B3 RID: 6579
		Medical,
		// Token: 0x040019B4 RID: 6580
		Armor,
		// Token: 0x040019B5 RID: 6581
		Blueprint,
		// Token: 0x040019B6 RID: 6582
		Food,
		// Token: 0x040019B7 RID: 6583
		Tools,
		// Token: 0x040019B8 RID: 6584
		Mods,
		// Token: 0x040019B9 RID: 6585
		Parts,
		// Token: 0x040019BA RID: 6586
		Resource
	}

	// Token: 0x0200057C RID: 1404
	public enum TransientMode
	{
		// Token: 0x040019BC RID: 6588
		Full,
		// Token: 0x040019BD RID: 6589
		DoesNotSave,
		// Token: 0x040019BE RID: 6590
		Untransferable,
		// Token: 0x040019BF RID: 6591
		None
	}
}
