using System;
using InventoryExtensions;
using uLink;
using UnityEngine;

// Token: 0x0200068D RID: 1677
public abstract class InventoryItem
{
	// Token: 0x0600397C RID: 14716 RVA: 0x000CAD58 File Offset: 0x000C8F58
	internal InventoryItem(global::ItemDataBlock datablock)
	{
		this.maxUses = datablock._maxUses;
		this.datablockUniqueID = datablock.uniqueID;
		this.iface = (this as global::IInventoryItem);
	}

	// Token: 0x17000B19 RID: 2841
	// (get) Token: 0x0600397D RID: 14717 RVA: 0x000CAD90 File Offset: 0x000C8F90
	// (set) Token: 0x0600397E RID: 14718 RVA: 0x000CAD98 File Offset: 0x000C8F98
	public float maxcondition { get; private set; }

	// Token: 0x17000B1A RID: 2842
	// (get) Token: 0x0600397F RID: 14719 RVA: 0x000CADA4 File Offset: 0x000C8FA4
	// (set) Token: 0x06003980 RID: 14720 RVA: 0x000CADAC File Offset: 0x000C8FAC
	public float condition { get; private set; }

	// Token: 0x17000B1B RID: 2843
	// (get) Token: 0x06003981 RID: 14721 RVA: 0x000CADB8 File Offset: 0x000C8FB8
	// (set) Token: 0x06003982 RID: 14722 RVA: 0x000CADC0 File Offset: 0x000C8FC0
	public int slot { get; private set; }

	// Token: 0x17000B1C RID: 2844
	// (get) Token: 0x06003983 RID: 14723 RVA: 0x000CADCC File Offset: 0x000C8FCC
	// (set) Token: 0x06003984 RID: 14724 RVA: 0x000CADD4 File Offset: 0x000C8FD4
	public int uses { get; private set; }

	// Token: 0x17000B1D RID: 2845
	// (get) Token: 0x06003985 RID: 14725 RVA: 0x000CADE0 File Offset: 0x000C8FE0
	// (set) Token: 0x06003986 RID: 14726 RVA: 0x000CADE8 File Offset: 0x000C8FE8
	public global::Inventory inventory { get; private set; }

	// Token: 0x17000B1E RID: 2846
	// (get) Token: 0x06003987 RID: 14727 RVA: 0x000CADF4 File Offset: 0x000C8FF4
	public bool dirty
	{
		get
		{
			return this.inventory && this.inventory.IsSlotDirty(this.slot);
		}
	}

	// Token: 0x17000B1F RID: 2847
	// (get) Token: 0x06003988 RID: 14728 RVA: 0x000CAE28 File Offset: 0x000C9028
	// (set) Token: 0x06003989 RID: 14729 RVA: 0x000CAE30 File Offset: 0x000C9030
	public float lastUseTime { get; set; }

	// Token: 0x17000B20 RID: 2848
	// (get) Token: 0x0600398A RID: 14730
	public abstract string toolTip { get; }

	// Token: 0x17000B21 RID: 2849
	// (get) Token: 0x0600398B RID: 14731 RVA: 0x000CAE3C File Offset: 0x000C903C
	public bool isInLocalInventory
	{
		get
		{
			global::Inventory inventory = this.inventory;
			global::Character character;
			return inventory && (character = (inventory.idMain as global::Character)) && character.localPlayerControlled;
		}
	}

	// Token: 0x17000B22 RID: 2850
	// (get) Token: 0x0600398C RID: 14732 RVA: 0x000CAE7C File Offset: 0x000C907C
	public IDMain idMain
	{
		get
		{
			global::Inventory inventory = this.inventory;
			return (!inventory) ? null : inventory.idMain;
		}
	}

	// Token: 0x17000B23 RID: 2851
	// (get) Token: 0x0600398D RID: 14733 RVA: 0x000CAEA8 File Offset: 0x000C90A8
	public global::Character character
	{
		get
		{
			global::Inventory inventory = this.inventory;
			return (!inventory) ? null : (inventory.idMain as global::Character);
		}
	}

	// Token: 0x17000B24 RID: 2852
	// (get) Token: 0x0600398E RID: 14734 RVA: 0x000CAED8 File Offset: 0x000C90D8
	public global::Controller controller
	{
		get
		{
			global::Inventory inventory = this.inventory;
			global::Character character;
			return (!inventory || !(character = (inventory.idMain as global::Character))) ? null : character.controller;
		}
	}

	// Token: 0x17000B25 RID: 2853
	// (get) Token: 0x0600398F RID: 14735 RVA: 0x000CAF1C File Offset: 0x000C911C
	public global::Controllable controllable
	{
		get
		{
			global::Inventory inventory = this.inventory;
			global::Character character;
			return (!inventory || !(character = (inventory.idMain as global::Character))) ? null : character.controllable;
		}
	}

	// Token: 0x17000B26 RID: 2854
	// (get) Token: 0x06003990 RID: 14736 RVA: 0x000CAF60 File Offset: 0x000C9160
	public bool active
	{
		get
		{
			global::Inventory inventory = this.inventory;
			return inventory && inventory.activeItem == this;
		}
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x000CAF8C File Offset: 0x000C918C
	public int AddUses(int count)
	{
		int uses;
		if (count <= 0 || (uses = this.uses) == this.maxUses)
		{
			return 0;
		}
		int uses2;
		if ((uses2 = uses + count) >= this.maxUses)
		{
			this.uses = this.maxUses;
			this.MarkDirty();
			return this.maxUses - uses;
		}
		this.uses = uses2;
		this.MarkDirty();
		return count;
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x000CAFF0 File Offset: 0x000C91F0
	public void SetUses(int count)
	{
		int uses = this.uses;
		if (count < 0 || count > this.maxUses)
		{
			count = this.maxUses;
		}
		if (count != uses)
		{
			this.uses = count;
			this.MarkDirty();
		}
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x000CB034 File Offset: 0x000C9234
	public bool Consume(ref int numWant)
	{
		int uses = this.uses;
		if (uses == 0)
		{
			return true;
		}
		if (numWant == 0)
		{
			return false;
		}
		if (uses <= numWant)
		{
			numWant -= uses;
			this.uses = 0;
			this.MarkDirty();
			return true;
		}
		this.uses = uses - numWant;
		numWant = 0;
		this.MarkDirty();
		return false;
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x000CB08C File Offset: 0x000C928C
	public void SetCondition(float newcondition)
	{
		float condition = this.condition;
		this.condition = Mathf.Clamp(newcondition, 0f, this.maxcondition);
		this.ConditionChanged(condition);
		this.MarkDirty();
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x000CB0C8 File Offset: 0x000C92C8
	public void SetMaxCondition(float newmaxcondition)
	{
		float maxcondition = this.maxcondition;
		this.maxcondition = Mathf.Clamp(newmaxcondition, 0.01f, 1f);
		this.MaxConditionChanged(maxcondition);
		this.MarkDirty();
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x000CB100 File Offset: 0x000C9300
	public float GetConditionForBreak()
	{
		return 0f;
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x000CB108 File Offset: 0x000C9308
	public virtual void ConditionChanged(float oldCondition)
	{
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x000CB10C File Offset: 0x000C930C
	public virtual bool CanMoveToSlot(global::Inventory toinv, int toslot)
	{
		return true;
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x000CB110 File Offset: 0x000C9310
	public virtual void MaxConditionChanged(float oldCondition)
	{
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x000CB114 File Offset: 0x000C9314
	public virtual string GetConditionString()
	{
		if (!this.datablock.doesLoseCondition)
		{
			return string.Empty;
		}
		if (this.condition > 1f)
		{
			return "Artifact";
		}
		if (this.condition >= 0.8f)
		{
			return "Perfect";
		}
		if (this.condition >= 0.6f)
		{
			return "Quality";
		}
		if (this.condition >= 0.5f)
		{
			return string.Empty;
		}
		if (this.condition >= 0.4f)
		{
			return "Shoddy";
		}
		if ((double)this.condition > 0.0)
		{
			return "Bad";
		}
		if (this.IsBroken())
		{
			return "Broken";
		}
		return "ERROR";
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x000CB1D8 File Offset: 0x000C93D8
	public float GetConditionPercent()
	{
		return this.condition / this.maxcondition;
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x000CB1E8 File Offset: 0x000C93E8
	public bool IsDamaged()
	{
		return this.maxcondition - this.condition > 0.001f;
	}

	// Token: 0x0600399D RID: 14749 RVA: 0x000CB200 File Offset: 0x000C9400
	public bool IsBroken()
	{
		return this.condition <= this.GetConditionForBreak();
	}

	// Token: 0x0600399E RID: 14750 RVA: 0x000CB214 File Offset: 0x000C9414
	public void BreakIntoPieces()
	{
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x000CB218 File Offset: 0x000C9418
	public bool TryConditionLoss(float probability, float percentLoss)
	{
		return false;
	}

	// Token: 0x17000B27 RID: 2855
	// (get) Token: 0x060039A0 RID: 14752
	protected abstract global::ItemDataBlock __infrastructure_db { get; }

	// Token: 0x17000B28 RID: 2856
	// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000CB21C File Offset: 0x000C941C
	public global::ItemDataBlock datablock
	{
		get
		{
			return this.__infrastructure_db;
		}
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x000CB224 File Offset: 0x000C9424
	public bool MarkDirty()
	{
		global::Inventory inventory = this.inventory;
		return inventory && inventory.MarkSlotDirty(this.slot);
	}

	// Token: 0x060039A3 RID: 14755
	protected abstract void OnBitStreamWrite(BitStream stream);

	// Token: 0x060039A4 RID: 14756
	protected abstract void OnBitStreamRead(BitStream stream);

	// Token: 0x060039A5 RID: 14757
	public abstract void OnMovedTo(global::Inventory inv, int slot);

	// Token: 0x060039A6 RID: 14758 RVA: 0x000CB254 File Offset: 0x000C9454
	public virtual void OnAddedTo(global::Inventory inv, int slot)
	{
		this.inventory = inv;
		this.slot = slot;
	}

	// Token: 0x060039A7 RID: 14759
	public abstract global::InventoryItem.MergeResult TryStack(global::IInventoryItem other);

	// Token: 0x060039A8 RID: 14760
	public abstract global::InventoryItem.MergeResult TryCombine(global::IInventoryItem other);

	// Token: 0x060039A9 RID: 14761
	public abstract global::InventoryItem.MenuItemResult OnMenuOption(global::InventoryItem.MenuItem option);

	// Token: 0x060039AA RID: 14762 RVA: 0x000CB264 File Offset: 0x000C9464
	public void Serialize(BitStream stream)
	{
		this.OnBitStreamWrite(stream);
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x000CB270 File Offset: 0x000C9470
	public void Deserialize(BitStream stream)
	{
		this.OnBitStreamRead(stream);
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x000CB27C File Offset: 0x000C947C
	protected static void SerializeSharedProperties(BitStream stream, global::InventoryItem item, global::ItemDataBlock db)
	{
		stream.WriteInvInt(item.uses);
		if (item.datablock.DoesLoseCondition())
		{
			stream.WriteSingle(item.condition);
			stream.WriteSingle(item.maxcondition);
		}
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x000CB2C0 File Offset: 0x000C94C0
	protected static void DeserializeSharedProperties(BitStream stream, global::InventoryItem item, global::ItemDataBlock db)
	{
		item.uses = stream.ReadInvInt();
		if (item.datablock.DoesLoseCondition())
		{
			item.condition = stream.ReadSingle();
			item.maxcondition = stream.ReadSingle();
		}
	}

	// Token: 0x04001C47 RID: 7239
	public const int MAX_SUPPORTED_ITEM_MODS = 5;

	// Token: 0x04001C48 RID: 7240
	public readonly global::IInventoryItem iface;

	// Token: 0x04001C49 RID: 7241
	public readonly int maxUses;

	// Token: 0x04001C4A RID: 7242
	public readonly int datablockUniqueID;

	// Token: 0x0200068E RID: 1678
	public enum MergeResult
	{
		// Token: 0x04001C52 RID: 7250
		Failed,
		// Token: 0x04001C53 RID: 7251
		Merged,
		// Token: 0x04001C54 RID: 7252
		Combined
	}

	// Token: 0x0200068F RID: 1679
	public enum ItemEvent
	{
		// Token: 0x04001C56 RID: 7254
		None,
		// Token: 0x04001C57 RID: 7255
		Equipped,
		// Token: 0x04001C58 RID: 7256
		UnEquipped,
		// Token: 0x04001C59 RID: 7257
		Combined,
		// Token: 0x04001C5A RID: 7258
		Used
	}

	// Token: 0x02000690 RID: 1680
	public enum MenuItem : byte
	{
		// Token: 0x04001C5C RID: 7260
		Info = 1,
		// Token: 0x04001C5D RID: 7261
		Status,
		// Token: 0x04001C5E RID: 7262
		Use,
		// Token: 0x04001C5F RID: 7263
		Study,
		// Token: 0x04001C60 RID: 7264
		Split,
		// Token: 0x04001C61 RID: 7265
		Eat,
		// Token: 0x04001C62 RID: 7266
		Drink,
		// Token: 0x04001C63 RID: 7267
		Consume,
		// Token: 0x04001C64 RID: 7268
		Unload
	}

	// Token: 0x02000691 RID: 1681
	public enum MenuItemResult : byte
	{
		// Token: 0x04001C66 RID: 7270
		DoneOnServer = 1,
		// Token: 0x04001C67 RID: 7271
		DoneOnServerNotYetClient,
		// Token: 0x04001C68 RID: 7272
		DoneOnClient,
		// Token: 0x04001C69 RID: 7273
		Complete,
		// Token: 0x04001C6A RID: 7274
		Unhandled = 0
	}
}
