using System;
using InventoryExtensions;
using uLink;
using UnityEngine;

// Token: 0x020005CF RID: 1487
public abstract class InventoryItem
{
	// Token: 0x060035B4 RID: 13748 RVA: 0x000C2AFC File Offset: 0x000C0CFC
	internal InventoryItem(ItemDataBlock datablock)
	{
		this.maxUses = datablock._maxUses;
		this.datablockUniqueID = datablock.uniqueID;
		this.iface = (this as IInventoryItem);
	}

	// Token: 0x17000AA3 RID: 2723
	// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000C2B34 File Offset: 0x000C0D34
	// (set) Token: 0x060035B6 RID: 13750 RVA: 0x000C2B3C File Offset: 0x000C0D3C
	public float maxcondition { get; private set; }

	// Token: 0x17000AA4 RID: 2724
	// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000C2B48 File Offset: 0x000C0D48
	// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000C2B50 File Offset: 0x000C0D50
	public float condition { get; private set; }

	// Token: 0x17000AA5 RID: 2725
	// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000C2B5C File Offset: 0x000C0D5C
	// (set) Token: 0x060035BA RID: 13754 RVA: 0x000C2B64 File Offset: 0x000C0D64
	public int slot { get; private set; }

	// Token: 0x17000AA6 RID: 2726
	// (get) Token: 0x060035BB RID: 13755 RVA: 0x000C2B70 File Offset: 0x000C0D70
	// (set) Token: 0x060035BC RID: 13756 RVA: 0x000C2B78 File Offset: 0x000C0D78
	public int uses { get; private set; }

	// Token: 0x17000AA7 RID: 2727
	// (get) Token: 0x060035BD RID: 13757 RVA: 0x000C2B84 File Offset: 0x000C0D84
	// (set) Token: 0x060035BE RID: 13758 RVA: 0x000C2B8C File Offset: 0x000C0D8C
	public Inventory inventory { get; private set; }

	// Token: 0x17000AA8 RID: 2728
	// (get) Token: 0x060035BF RID: 13759 RVA: 0x000C2B98 File Offset: 0x000C0D98
	public bool dirty
	{
		get
		{
			return this.inventory && this.inventory.IsSlotDirty(this.slot);
		}
	}

	// Token: 0x17000AA9 RID: 2729
	// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000C2BCC File Offset: 0x000C0DCC
	// (set) Token: 0x060035C1 RID: 13761 RVA: 0x000C2BD4 File Offset: 0x000C0DD4
	public float lastUseTime { get; set; }

	// Token: 0x17000AAA RID: 2730
	// (get) Token: 0x060035C2 RID: 13762
	public abstract string toolTip { get; }

	// Token: 0x17000AAB RID: 2731
	// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000C2BE0 File Offset: 0x000C0DE0
	public bool isInLocalInventory
	{
		get
		{
			Inventory inventory = this.inventory;
			Character character;
			return inventory && (character = (inventory.idMain as Character)) && character.localPlayerControlled;
		}
	}

	// Token: 0x17000AAC RID: 2732
	// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000C2C20 File Offset: 0x000C0E20
	public IDMain idMain
	{
		get
		{
			Inventory inventory = this.inventory;
			return (!inventory) ? null : inventory.idMain;
		}
	}

	// Token: 0x17000AAD RID: 2733
	// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000C2C4C File Offset: 0x000C0E4C
	public Character character
	{
		get
		{
			Inventory inventory = this.inventory;
			return (!inventory) ? null : (inventory.idMain as Character);
		}
	}

	// Token: 0x17000AAE RID: 2734
	// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000C2C7C File Offset: 0x000C0E7C
	public Controller controller
	{
		get
		{
			Inventory inventory = this.inventory;
			Character character;
			return (!inventory || !(character = (inventory.idMain as Character))) ? null : character.controller;
		}
	}

	// Token: 0x17000AAF RID: 2735
	// (get) Token: 0x060035C7 RID: 13767 RVA: 0x000C2CC0 File Offset: 0x000C0EC0
	public Controllable controllable
	{
		get
		{
			Inventory inventory = this.inventory;
			Character character;
			return (!inventory || !(character = (inventory.idMain as Character))) ? null : character.controllable;
		}
	}

	// Token: 0x17000AB0 RID: 2736
	// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000C2D04 File Offset: 0x000C0F04
	public bool active
	{
		get
		{
			Inventory inventory = this.inventory;
			return inventory && inventory.activeItem == this;
		}
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x000C2D30 File Offset: 0x000C0F30
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

	// Token: 0x060035CA RID: 13770 RVA: 0x000C2D94 File Offset: 0x000C0F94
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

	// Token: 0x060035CB RID: 13771 RVA: 0x000C2DD8 File Offset: 0x000C0FD8
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

	// Token: 0x060035CC RID: 13772 RVA: 0x000C2E30 File Offset: 0x000C1030
	public void SetCondition(float newcondition)
	{
		float condition = this.condition;
		this.condition = Mathf.Clamp(newcondition, 0f, this.maxcondition);
		this.ConditionChanged(condition);
		this.MarkDirty();
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x000C2E6C File Offset: 0x000C106C
	public void SetMaxCondition(float newmaxcondition)
	{
		float maxcondition = this.maxcondition;
		this.maxcondition = Mathf.Clamp(newmaxcondition, 0.01f, 1f);
		this.MaxConditionChanged(maxcondition);
		this.MarkDirty();
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x000C2EA4 File Offset: 0x000C10A4
	public float GetConditionForBreak()
	{
		return 0f;
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x000C2EAC File Offset: 0x000C10AC
	public virtual void ConditionChanged(float oldCondition)
	{
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x000C2EB0 File Offset: 0x000C10B0
	public virtual bool CanMoveToSlot(Inventory toinv, int toslot)
	{
		return true;
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x000C2EB4 File Offset: 0x000C10B4
	public virtual void MaxConditionChanged(float oldCondition)
	{
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x000C2EB8 File Offset: 0x000C10B8
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

	// Token: 0x060035D3 RID: 13779 RVA: 0x000C2F7C File Offset: 0x000C117C
	public float GetConditionPercent()
	{
		return this.condition / this.maxcondition;
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x000C2F8C File Offset: 0x000C118C
	public bool IsDamaged()
	{
		return this.maxcondition - this.condition > 0.001f;
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x000C2FA4 File Offset: 0x000C11A4
	public bool IsBroken()
	{
		return this.condition <= this.GetConditionForBreak();
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x000C2FB8 File Offset: 0x000C11B8
	public void BreakIntoPieces()
	{
	}

	// Token: 0x060035D7 RID: 13783 RVA: 0x000C2FBC File Offset: 0x000C11BC
	public bool TryConditionLoss(float probability, float percentLoss)
	{
		return false;
	}

	// Token: 0x17000AB1 RID: 2737
	// (get) Token: 0x060035D8 RID: 13784
	protected abstract ItemDataBlock __infrastructure_db { get; }

	// Token: 0x17000AB2 RID: 2738
	// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000C2FC0 File Offset: 0x000C11C0
	public ItemDataBlock datablock
	{
		get
		{
			return this.__infrastructure_db;
		}
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x000C2FC8 File Offset: 0x000C11C8
	public bool MarkDirty()
	{
		Inventory inventory = this.inventory;
		return inventory && inventory.MarkSlotDirty(this.slot);
	}

	// Token: 0x060035DB RID: 13787
	protected abstract void OnBitStreamWrite(BitStream stream);

	// Token: 0x060035DC RID: 13788
	protected abstract void OnBitStreamRead(BitStream stream);

	// Token: 0x060035DD RID: 13789
	public abstract void OnMovedTo(Inventory inv, int slot);

	// Token: 0x060035DE RID: 13790 RVA: 0x000C2FF8 File Offset: 0x000C11F8
	public virtual void OnAddedTo(Inventory inv, int slot)
	{
		this.inventory = inv;
		this.slot = slot;
	}

	// Token: 0x060035DF RID: 13791
	public abstract InventoryItem.MergeResult TryStack(IInventoryItem other);

	// Token: 0x060035E0 RID: 13792
	public abstract InventoryItem.MergeResult TryCombine(IInventoryItem other);

	// Token: 0x060035E1 RID: 13793
	public abstract InventoryItem.MenuItemResult OnMenuOption(InventoryItem.MenuItem option);

	// Token: 0x060035E2 RID: 13794 RVA: 0x000C3008 File Offset: 0x000C1208
	public void Serialize(BitStream stream)
	{
		this.OnBitStreamWrite(stream);
	}

	// Token: 0x060035E3 RID: 13795 RVA: 0x000C3014 File Offset: 0x000C1214
	public void Deserialize(BitStream stream)
	{
		this.OnBitStreamRead(stream);
	}

	// Token: 0x060035E4 RID: 13796 RVA: 0x000C3020 File Offset: 0x000C1220
	protected static void SerializeSharedProperties(BitStream stream, InventoryItem item, ItemDataBlock db)
	{
		stream.WriteInvInt(item.uses);
		if (item.datablock.DoesLoseCondition())
		{
			stream.WriteSingle(item.condition);
			stream.WriteSingle(item.maxcondition);
		}
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x000C3064 File Offset: 0x000C1264
	protected static void DeserializeSharedProperties(BitStream stream, InventoryItem item, ItemDataBlock db)
	{
		item.uses = stream.ReadInvInt();
		if (item.datablock.DoesLoseCondition())
		{
			item.condition = stream.ReadSingle();
			item.maxcondition = stream.ReadSingle();
		}
	}

	// Token: 0x04001A76 RID: 6774
	public const int MAX_SUPPORTED_ITEM_MODS = 5;

	// Token: 0x04001A77 RID: 6775
	public readonly IInventoryItem iface;

	// Token: 0x04001A78 RID: 6776
	public readonly int maxUses;

	// Token: 0x04001A79 RID: 6777
	public readonly int datablockUniqueID;

	// Token: 0x020005D0 RID: 1488
	public enum MergeResult
	{
		// Token: 0x04001A81 RID: 6785
		Failed,
		// Token: 0x04001A82 RID: 6786
		Merged,
		// Token: 0x04001A83 RID: 6787
		Combined
	}

	// Token: 0x020005D1 RID: 1489
	public enum ItemEvent
	{
		// Token: 0x04001A85 RID: 6789
		None,
		// Token: 0x04001A86 RID: 6790
		Equipped,
		// Token: 0x04001A87 RID: 6791
		UnEquipped,
		// Token: 0x04001A88 RID: 6792
		Combined,
		// Token: 0x04001A89 RID: 6793
		Used
	}

	// Token: 0x020005D2 RID: 1490
	public enum MenuItem : byte
	{
		// Token: 0x04001A8B RID: 6795
		Info = 1,
		// Token: 0x04001A8C RID: 6796
		Status,
		// Token: 0x04001A8D RID: 6797
		Use,
		// Token: 0x04001A8E RID: 6798
		Study,
		// Token: 0x04001A8F RID: 6799
		Split,
		// Token: 0x04001A90 RID: 6800
		Eat,
		// Token: 0x04001A91 RID: 6801
		Drink,
		// Token: 0x04001A92 RID: 6802
		Consume,
		// Token: 0x04001A93 RID: 6803
		Unload
	}

	// Token: 0x020005D3 RID: 1491
	public enum MenuItemResult : byte
	{
		// Token: 0x04001A95 RID: 6805
		DoneOnServer = 1,
		// Token: 0x04001A96 RID: 6806
		DoneOnServerNotYetClient,
		// Token: 0x04001A97 RID: 6807
		DoneOnClient,
		// Token: 0x04001A98 RID: 6808
		Complete,
		// Token: 0x04001A99 RID: 6809
		Unhandled = 0
	}
}
