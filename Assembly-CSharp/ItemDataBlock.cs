using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000636 RID: 1590
public class ItemDataBlock : global::Datablock, IComparable<global::ItemDataBlock>
{
	// Token: 0x060034B9 RID: 13497 RVA: 0x000C32B0 File Offset: 0x000C14B0
	int IComparable<global::ItemDataBlock>.CompareTo(global::ItemDataBlock other)
	{
		return this.CompareTo(other);
	}

	// Token: 0x060034BA RID: 13498 RVA: 0x000C32BC File Offset: 0x000C14BC
	protected virtual global::IInventoryItem ConstructItem()
	{
		return new global::ItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060034BB RID: 13499 RVA: 0x000C32C4 File Offset: 0x000C14C4
	public global::IInventoryItem CreateItem()
	{
		global::IInventoryItem inventoryItem = this.ConstructItem();
		this.InstallData(inventoryItem);
		return inventoryItem;
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x000C32E0 File Offset: 0x000C14E0
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

	// Token: 0x060034BD RID: 13501 RVA: 0x000C3374 File Offset: 0x000C1574
	public virtual void InstallData(global::IInventoryItem item)
	{
		item.SetUses(1);
		item.SetMaxCondition(1f);
		item.SetCondition(1f);
	}

	// Token: 0x060034BE RID: 13502 RVA: 0x000C3394 File Offset: 0x000C1594
	protected virtual void PreInstallJsonProperties(global::IInventoryItem item)
	{
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x000C3398 File Offset: 0x000C1598
	protected virtual void PostInstallJsonProperties(global::IInventoryItem item)
	{
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x000C339C File Offset: 0x000C159C
	public static bool LoadIconOrUnknown<TTex>(string iconPath, ref TTex tex) where TTex : Texture
	{
		return tex || global::ItemDataBlock.LoadIconOrUnknownForced<TTex>(iconPath, out tex);
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x000C33C0 File Offset: 0x000C15C0
	public static bool LoadIconOrUnknownForced<TTex>(string iconPath, out TTex tex) where TTex : Texture
	{
		return Facepunch.Bundling.Load<TTex>(iconPath, out tex) || Facepunch.Bundling.Load<TTex>("content/item/tex/unknown", out tex);
	}

	// Token: 0x17000AA6 RID: 2726
	// (get) Token: 0x060034C2 RID: 13506 RVA: 0x000C33DC File Offset: 0x000C15DC
	public bool doesNotSave
	{
		get
		{
			return (this.transientMode & global::ItemDataBlock.TransientMode.DoesNotSave) == global::ItemDataBlock.TransientMode.DoesNotSave;
		}
	}

	// Token: 0x17000AA7 RID: 2727
	// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000C33EC File Offset: 0x000C15EC
	public bool untransferable
	{
		get
		{
			return (this.transientMode & global::ItemDataBlock.TransientMode.Untransferable) == global::ItemDataBlock.TransientMode.Untransferable;
		}
	}

	// Token: 0x17000AA8 RID: 2728
	// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000C33FC File Offset: 0x000C15FC
	public bool saves
	{
		get
		{
			return (this.transientMode & global::ItemDataBlock.TransientMode.DoesNotSave) != global::ItemDataBlock.TransientMode.DoesNotSave;
		}
	}

	// Token: 0x17000AA9 RID: 2729
	// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000C340C File Offset: 0x000C160C
	public bool transferable
	{
		get
		{
			return (this.transientMode & global::ItemDataBlock.TransientMode.Untransferable) != global::ItemDataBlock.TransientMode.Untransferable;
		}
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x000C341C File Offset: 0x000C161C
	public virtual byte GetMaxEligableSlots()
	{
		return 0;
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x000C3420 File Offset: 0x000C1620
	public int GetRandomSpawnUses()
	{
		return Random.Range(this._spawnUsesMin, this._spawnUsesMax + 1);
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x000C3438 File Offset: 0x000C1638
	public virtual bool IsSplittable()
	{
		return this._splittable;
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x000C3440 File Offset: 0x000C1640
	public bool DoesLoseCondition()
	{
		return this.doesLoseCondition;
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x000C3448 File Offset: 0x000C1648
	public int GetMinUsesForDisplay()
	{
		return this._minUsesForDisplay;
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x000C3450 File Offset: 0x000C1650
	public virtual string GetItemDescription()
	{
		if (this.itemDescriptionOverride.Length > 0)
		{
			return this.itemDescriptionOverride;
		}
		return "No item description available";
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x000C3470 File Offset: 0x000C1670
	public virtual int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		if (this._splittable && item.uses > 1 && item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Split;
		}
		return offset;
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x000C34AC File Offset: 0x000C16AC
	public virtual global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option == global::InventoryItem.MenuItem.Info)
		{
			global::RPOS.OpenInfoWindow(this);
			return global::InventoryItem.MenuItemResult.DoneOnClient;
		}
		if (option != global::InventoryItem.MenuItem.Split)
		{
			return global::InventoryItem.MenuItemResult.Unhandled;
		}
		item.inventory.SplitStack(item.slot);
		return global::InventoryItem.MenuItemResult.Complete;
	}

	// Token: 0x060034CE RID: 13518 RVA: 0x000C34EC File Offset: 0x000C16EC
	public Texture GetIconTexture()
	{
		if (!this.iconTex && !Facepunch.Bundling.Load<Texture>(this.icon, out this.iconTex))
		{
			Facepunch.Bundling.Load<Texture>("content/item/tex/unknown", out this.iconTex);
		}
		return this.iconTex;
	}

	// Token: 0x060034CF RID: 13519 RVA: 0x000C352C File Offset: 0x000C172C
	public global::ItemDataBlock.CombineRecipe GetMatchingRecipe(global::ItemDataBlock db)
	{
		if (this.Combinations == null || this.Combinations.Length == 0)
		{
			return null;
		}
		foreach (global::ItemDataBlock.CombineRecipe combineRecipe in this.Combinations)
		{
			if (combineRecipe.droppedOnType == db)
			{
				return combineRecipe;
			}
		}
		return null;
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x000C3588 File Offset: 0x000C1788
	public void ConfigureItemPickup(global::ItemPickup pickup, int amount)
	{
	}

	// Token: 0x060034D1 RID: 13521 RVA: 0x000C358C File Offset: 0x000C178C
	public virtual void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem item)
	{
		infoWindow.AddItemTitle(this, item, 0f);
		infoWindow.AddConditionInfo(item);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x060034D2 RID: 13522 RVA: 0x000C35B8 File Offset: 0x000C17B8
	public virtual void OnItemEvent(global::InventoryItem.ItemEvent itemEvent)
	{
		switch (itemEvent)
		{
		case global::InventoryItem.ItemEvent.Equipped:
			if (this.equippedSound)
			{
				this.equippedSound.Play(1f);
			}
			break;
		case global::InventoryItem.ItemEvent.UnEquipped:
			if (this.unEquippedSound)
			{
				this.unEquippedSound.Play(1f);
			}
			break;
		case global::InventoryItem.ItemEvent.Combined:
			if (this.combinedSound)
			{
				this.combinedSound.Play(1f);
			}
			break;
		case global::InventoryItem.ItemEvent.Used:
			if (this.UsedSound)
			{
				this.UsedSound.Play(1f);
			}
			break;
		}
	}

	// Token: 0x04001B64 RID: 7012
	public const string kUnknownIconPath = "content/item/tex/unknown";

	// Token: 0x04001B65 RID: 7013
	public string icon;

	// Token: 0x04001B66 RID: 7014
	[HideInInspector]
	[NonSerialized]
	public Texture iconTex;

	// Token: 0x04001B67 RID: 7015
	public int _maxUses = 1;

	// Token: 0x04001B68 RID: 7016
	public int _spawnUsesMin = 1;

	// Token: 0x04001B69 RID: 7017
	public int _spawnUsesMax = 1;

	// Token: 0x04001B6A RID: 7018
	public int _minUsesForDisplay = 1;

	// Token: 0x04001B6B RID: 7019
	public float _maxCondition = 1f;

	// Token: 0x04001B6C RID: 7020
	public bool _splittable;

	// Token: 0x04001B6D RID: 7021
	[HideInInspector]
	public global::Inventory.SlotFlags _itemFlags;

	// Token: 0x04001B6E RID: 7022
	public bool isResearchable = true;

	// Token: 0x04001B6F RID: 7023
	public bool isRepairable = true;

	// Token: 0x04001B70 RID: 7024
	public bool isRecycleable = true;

	// Token: 0x04001B71 RID: 7025
	public bool doesLoseCondition;

	// Token: 0x04001B72 RID: 7026
	public global::ItemDataBlock.ItemCategory category = global::ItemDataBlock.ItemCategory.Misc;

	// Token: 0x04001B73 RID: 7027
	public string itemDescriptionOverride = string.Empty;

	// Token: 0x04001B74 RID: 7028
	public AudioClip equippedSound;

	// Token: 0x04001B75 RID: 7029
	public AudioClip unEquippedSound;

	// Token: 0x04001B76 RID: 7030
	public AudioClip combinedSound;

	// Token: 0x04001B77 RID: 7031
	public AudioClip UsedSound;

	// Token: 0x04001B78 RID: 7032
	public global::ItemDataBlock.CombineRecipe[] Combinations;

	// Token: 0x04001B79 RID: 7033
	public global::ItemDataBlock.TransientMode transientMode;

	// Token: 0x02000637 RID: 1591
	private sealed class ITEM_TYPE : global::InventoryItem<global::ItemDataBlock>, global::IInventoryItem
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x000C3678 File Offset: 0x000C1878
		public ITEM_TYPE(global::ItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000C3684 File Offset: 0x000C1884
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000C368C File Offset: 0x000C188C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000C3694 File Offset: 0x000C1894
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000C369C File Offset: 0x000C189C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000C36A4 File Offset: 0x000C18A4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000C36B0 File Offset: 0x000C18B0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000C36BC File Offset: 0x000C18BC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000C36C8 File Offset: 0x000C18C8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000C36D4 File Offset: 0x000C18D4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000C36E0 File Offset: 0x000C18E0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000C36EC File Offset: 0x000C18EC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000C36F8 File Offset: 0x000C18F8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000C3704 File Offset: 0x000C1904
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000C370C File Offset: 0x000C190C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000C3714 File Offset: 0x000C1914
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000C371C File Offset: 0x000C191C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000C3724 File Offset: 0x000C1924
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000C372C File Offset: 0x000C192C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000C3734 File Offset: 0x000C1934
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000C373C File Offset: 0x000C193C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000C3744 File Offset: 0x000C1944
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000C3750 File Offset: 0x000C1950
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000C3758 File Offset: 0x000C1958
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000C3760 File Offset: 0x000C1960
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000C3768 File Offset: 0x000C1968
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000C3770 File Offset: 0x000C1970
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000C3778 File Offset: 0x000C1978
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000C3780 File Offset: 0x000C1980
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x02000638 RID: 1592
	[Serializable]
	public class CombineRecipe
	{
		// Token: 0x04001B7A RID: 7034
		public global::ItemDataBlock droppedOnType;

		// Token: 0x04001B7B RID: 7035
		public global::ItemDataBlock resultItem;

		// Token: 0x04001B7C RID: 7036
		public int amountToLose = 1;

		// Token: 0x04001B7D RID: 7037
		public int amountToLoseOther = 1;

		// Token: 0x04001B7E RID: 7038
		public int amountToGive = 1;
	}

	// Token: 0x02000639 RID: 1593
	[Serializable]
	public enum ItemCategory
	{
		// Token: 0x04001B80 RID: 7040
		Survival,
		// Token: 0x04001B81 RID: 7041
		Weapons,
		// Token: 0x04001B82 RID: 7042
		Ammo,
		// Token: 0x04001B83 RID: 7043
		Misc,
		// Token: 0x04001B84 RID: 7044
		Medical,
		// Token: 0x04001B85 RID: 7045
		Armor,
		// Token: 0x04001B86 RID: 7046
		Blueprint,
		// Token: 0x04001B87 RID: 7047
		Food,
		// Token: 0x04001B88 RID: 7048
		Tools,
		// Token: 0x04001B89 RID: 7049
		Mods,
		// Token: 0x04001B8A RID: 7050
		Parts,
		// Token: 0x04001B8B RID: 7051
		Resource
	}

	// Token: 0x0200063A RID: 1594
	public enum TransientMode
	{
		// Token: 0x04001B8D RID: 7053
		Full,
		// Token: 0x04001B8E RID: 7054
		DoesNotSave,
		// Token: 0x04001B8F RID: 7055
		Untransferable,
		// Token: 0x04001B90 RID: 7056
		None
	}
}
