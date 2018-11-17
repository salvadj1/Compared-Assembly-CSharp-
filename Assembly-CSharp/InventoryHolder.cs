using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000663 RID: 1635
public class InventoryHolder : global::IDLocalCharacter
{
	// Token: 0x17000AC4 RID: 2756
	// (get) Token: 0x06003836 RID: 14390 RVA: 0x000C78E8 File Offset: 0x000C5AE8
	public global::Inventory inventory
	{
		get
		{
			if (!this._inventory.cached)
			{
				this._inventory = base.GetLocal<global::Inventory>();
			}
			return this._inventory.value;
		}
	}

	// Token: 0x17000AC5 RID: 2757
	// (get) Token: 0x06003837 RID: 14391 RVA: 0x000C7924 File Offset: 0x000C5B24
	public global::IInventoryItem inputItem
	{
		get
		{
			global::Inventory inventory = this.inventory;
			global::IInventoryItem result;
			if (inventory)
			{
				global::IInventoryItem activeItem = inventory.activeItem;
				result = activeItem;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}

	// Token: 0x17000AC6 RID: 2758
	// (get) Token: 0x06003838 RID: 14392 RVA: 0x000C7954 File Offset: 0x000C5B54
	public global::ItemModFlags modFlags
	{
		get
		{
			if (this.hasItem && this.itemRep)
			{
				return this.itemRep.modFlags;
			}
			global::IHeldItem heldItem = this.inputItem as global::IHeldItem;
			if (!object.ReferenceEquals(heldItem, null))
			{
				return heldItem.modFlags;
			}
			return global::ItemModFlags.Other;
		}
	}

	// Token: 0x17000AC7 RID: 2759
	// (get) Token: 0x06003839 RID: 14393 RVA: 0x000C79A8 File Offset: 0x000C5BA8
	public bool hasItemRepresentation
	{
		get
		{
			return this.hasItem;
		}
	}

	// Token: 0x17000AC8 RID: 2760
	// (get) Token: 0x0600383A RID: 14394 RVA: 0x000C79B0 File Offset: 0x000C5BB0
	public global::ItemRepresentation itemRepresentation
	{
		get
		{
			return this.itemRep;
		}
	}

	// Token: 0x17000AC9 RID: 2761
	// (get) Token: 0x0600383B RID: 14395 RVA: 0x000C79B8 File Offset: 0x000C5BB8
	public string animationGroupName
	{
		get
		{
			return this._animationGroupNameCached;
		}
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x000C79C0 File Offset: 0x000C5BC0
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			this.inventory.RequestFullUpdate();
		}
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x000C79E0 File Offset: 0x000C5BE0
	internal void SetItemRepresentation(global::ItemRepresentation value)
	{
		if (this.itemRep != value)
		{
			this.itemRep = value;
			this.hasItem = this.itemRep;
			if (this.hasItem)
			{
				this._animationGroupNameCached = this.itemRep.worldAnimationGroupName;
				if (this._animationGroupNameCached != null && this._animationGroupNameCached.Length == 1)
				{
					this._animationGroupNameCached = null;
				}
			}
			else
			{
				this._animationGroupNameCached = null;
			}
		}
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x000C7A64 File Offset: 0x000C5C64
	internal void ClearItemRepresentation(global::ItemRepresentation value)
	{
		if (this.hasItem && this.itemRep == value)
		{
			this.itemRep = null;
			this.hasItem = false;
			this._animationGroupNameCached = null;
		}
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x000C7A98 File Offset: 0x000C5C98
	private bool ValidateAntiBeltSpam(ulong timestamp)
	{
		ulong timeInMillis = global::NetCull.timeInMillis;
		if (timeInMillis + 800UL >= this.lastItemUseTime)
		{
			this.lastItemUseTime = timeInMillis;
			return true;
		}
		return false;
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x000C7AC8 File Offset: 0x000C5CC8
	private bool GetPlayerInventory(out global::PlayerInventory inventory)
	{
		inventory = (this.inventory as global::PlayerInventory);
		if (!inventory)
		{
			inventory = null;
			return false;
		}
		inventory = (global::PlayerInventory)this.inventory;
		return inventory;
	}

	// Token: 0x06003841 RID: 14401 RVA: 0x000C7B08 File Offset: 0x000C5D08
	public void InventoryModified()
	{
		if (base.localControlled)
		{
			global::RPOS.LocalInventoryModified();
		}
	}

	// Token: 0x06003842 RID: 14402 RVA: 0x000C7B1C File Offset: 0x000C5D1C
	public bool BeltUse(int beltNum)
	{
		if (base.dead)
		{
			return false;
		}
		global::PlayerInventory playerInventory;
		global::IInventoryItem inventoryItem;
		global::IHeldItem heldItem;
		if (this.GetPlayerInventory(out playerInventory) && playerInventory.GetItem(30 + beltNum, out inventoryItem) && (!(inventoryItem is global::IHeldItem) || ((!(heldItem = (global::IHeldItem)inventoryItem).active) ? heldItem.canActivate : heldItem.canDeactivate)) && this.ValidateAntiBeltSpam(global::NetCull.timeInMillis))
		{
			base.networkView.RPC<int>("DoBeltUse", 0, beltNum);
			return true;
		}
		return false;
	}

	// Token: 0x06003843 RID: 14403 RVA: 0x000C7BAC File Offset: 0x000C5DAC
	[RPC]
	[global::NGCRPCSkip]
	protected void TOSS(BitStream stream, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x000C7BB0 File Offset: 0x000C5DB0
	public bool TossItem(int slot)
	{
		Facepunch.NetworkView networkView = base.networkView;
		if (!networkView || !networkView.isMine)
		{
			return false;
		}
		global::Inventory inventory = this.inventory;
		global::IInventoryItem inventoryItem;
		if (!inventory || !inventory.GetItem(slot, out inventoryItem))
		{
			return false;
		}
		global::NetCull.RPC<byte>(this, "TOSS", 0, global::Inventory.RPCInteger(slot));
		inventory.NULL_SLOT_FIX_ME(slot);
		return true;
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x000C7C1C File Offset: 0x000C5E1C
	[RPC]
	protected void DoBeltUse(int beltNum)
	{
	}

	// Token: 0x06003846 RID: 14406 RVA: 0x000C7C20 File Offset: 0x000C5E20
	public object InvokeInputItemPreFrame(ref global::HumanController.InputSample sample)
	{
		global::IHeldItem heldItem = this.inputItem as global::IHeldItem;
		if (heldItem != null)
		{
			heldItem.ItemPreFrame(ref sample);
		}
		return heldItem;
	}

	// Token: 0x06003847 RID: 14407 RVA: 0x000C7C48 File Offset: 0x000C5E48
	public void InvokeInputItemPostFrame(object item, ref global::HumanController.InputSample sample)
	{
		global::IHeldItem heldItem = item as global::IHeldItem;
		if (heldItem != null)
		{
			heldItem.ItemPostFrame(ref sample);
		}
	}

	// Token: 0x06003848 RID: 14408 RVA: 0x000C7C6C File Offset: 0x000C5E6C
	public void InvokeInputItemPreRender()
	{
		global::IHeldItem heldItem = this.inputItem as global::IHeldItem;
		if (heldItem != null)
		{
			heldItem.PreCameraRender();
		}
	}

	// Token: 0x04001BF6 RID: 7158
	private const string TossItem_RPC = "TOSS";

	// Token: 0x04001BF7 RID: 7159
	[NonSerialized]
	private global::CacheRef<global::Inventory> _inventory;

	// Token: 0x04001BF8 RID: 7160
	[NonSerialized]
	private global::ItemRepresentation itemRep;

	// Token: 0x04001BF9 RID: 7161
	[NonSerialized]
	private string _animationGroupNameCached;

	// Token: 0x04001BFA RID: 7162
	[NonSerialized]
	private ulong lastItemUseTime;

	// Token: 0x04001BFB RID: 7163
	[NonSerialized]
	private bool hasItem;

	// Token: 0x04001BFC RID: 7164
	[NonSerialized]
	private bool isPlayerInventory;
}
