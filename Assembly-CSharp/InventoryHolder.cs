using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class InventoryHolder : IDLocalCharacter
{
	// Token: 0x17000A4E RID: 2638
	// (get) Token: 0x0600346E RID: 13422 RVA: 0x000BF68C File Offset: 0x000BD88C
	public Inventory inventory
	{
		get
		{
			if (!this._inventory.cached)
			{
				this._inventory = base.GetLocal<Inventory>();
			}
			return this._inventory.value;
		}
	}

	// Token: 0x17000A4F RID: 2639
	// (get) Token: 0x0600346F RID: 13423 RVA: 0x000BF6C8 File Offset: 0x000BD8C8
	public IInventoryItem inputItem
	{
		get
		{
			Inventory inventory = this.inventory;
			IInventoryItem result;
			if (inventory)
			{
				IInventoryItem activeItem = inventory.activeItem;
				result = activeItem;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}

	// Token: 0x17000A50 RID: 2640
	// (get) Token: 0x06003470 RID: 13424 RVA: 0x000BF6F8 File Offset: 0x000BD8F8
	public ItemModFlags modFlags
	{
		get
		{
			if (this.hasItem && this.itemRep)
			{
				return this.itemRep.modFlags;
			}
			IHeldItem heldItem = this.inputItem as IHeldItem;
			if (!object.ReferenceEquals(heldItem, null))
			{
				return heldItem.modFlags;
			}
			return ItemModFlags.Other;
		}
	}

	// Token: 0x17000A51 RID: 2641
	// (get) Token: 0x06003471 RID: 13425 RVA: 0x000BF74C File Offset: 0x000BD94C
	public bool hasItemRepresentation
	{
		get
		{
			return this.hasItem;
		}
	}

	// Token: 0x17000A52 RID: 2642
	// (get) Token: 0x06003472 RID: 13426 RVA: 0x000BF754 File Offset: 0x000BD954
	public ItemRepresentation itemRepresentation
	{
		get
		{
			return this.itemRep;
		}
	}

	// Token: 0x17000A53 RID: 2643
	// (get) Token: 0x06003473 RID: 13427 RVA: 0x000BF75C File Offset: 0x000BD95C
	public string animationGroupName
	{
		get
		{
			return this._animationGroupNameCached;
		}
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x000BF764 File Offset: 0x000BD964
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			this.inventory.RequestFullUpdate();
		}
	}

	// Token: 0x06003475 RID: 13429 RVA: 0x000BF784 File Offset: 0x000BD984
	internal void SetItemRepresentation(ItemRepresentation value)
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

	// Token: 0x06003476 RID: 13430 RVA: 0x000BF808 File Offset: 0x000BDA08
	internal void ClearItemRepresentation(ItemRepresentation value)
	{
		if (this.hasItem && this.itemRep == value)
		{
			this.itemRep = null;
			this.hasItem = false;
			this._animationGroupNameCached = null;
		}
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x000BF83C File Offset: 0x000BDA3C
	private bool ValidateAntiBeltSpam(ulong timestamp)
	{
		ulong timeInMillis = NetCull.timeInMillis;
		if (timeInMillis + 800UL >= this.lastItemUseTime)
		{
			this.lastItemUseTime = timeInMillis;
			return true;
		}
		return false;
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x000BF86C File Offset: 0x000BDA6C
	private bool GetPlayerInventory(out PlayerInventory inventory)
	{
		inventory = (this.inventory as PlayerInventory);
		if (!inventory)
		{
			inventory = null;
			return false;
		}
		inventory = (PlayerInventory)this.inventory;
		return inventory;
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x000BF8AC File Offset: 0x000BDAAC
	public void InventoryModified()
	{
		if (base.localControlled)
		{
			RPOS.LocalInventoryModified();
		}
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x000BF8C0 File Offset: 0x000BDAC0
	public bool BeltUse(int beltNum)
	{
		if (base.dead)
		{
			return false;
		}
		PlayerInventory playerInventory;
		IInventoryItem inventoryItem;
		IHeldItem heldItem;
		if (this.GetPlayerInventory(out playerInventory) && playerInventory.GetItem(30 + beltNum, out inventoryItem) && (!(inventoryItem is IHeldItem) || ((!(heldItem = (IHeldItem)inventoryItem).active) ? heldItem.canActivate : heldItem.canDeactivate)) && this.ValidateAntiBeltSpam(NetCull.timeInMillis))
		{
			base.networkView.RPC<int>("DoBeltUse", 0, beltNum);
			return true;
		}
		return false;
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x000BF950 File Offset: 0x000BDB50
	[NGCRPCSkip]
	[RPC]
	protected void TOSS(BitStream stream, NetworkMessageInfo info)
	{
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x000BF954 File Offset: 0x000BDB54
	public bool TossItem(int slot)
	{
		NetworkView networkView = base.networkView;
		if (!networkView || !networkView.isMine)
		{
			return false;
		}
		Inventory inventory = this.inventory;
		IInventoryItem inventoryItem;
		if (!inventory || !inventory.GetItem(slot, out inventoryItem))
		{
			return false;
		}
		NetCull.RPC<byte>(this, "TOSS", 0, Inventory.RPCInteger(slot));
		inventory.NULL_SLOT_FIX_ME(slot);
		return true;
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x000BF9C0 File Offset: 0x000BDBC0
	[RPC]
	protected void DoBeltUse(int beltNum)
	{
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x000BF9C4 File Offset: 0x000BDBC4
	public object InvokeInputItemPreFrame(ref HumanController.InputSample sample)
	{
		IHeldItem heldItem = this.inputItem as IHeldItem;
		if (heldItem != null)
		{
			heldItem.ItemPreFrame(ref sample);
		}
		return heldItem;
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x000BF9EC File Offset: 0x000BDBEC
	public void InvokeInputItemPostFrame(object item, ref HumanController.InputSample sample)
	{
		IHeldItem heldItem = item as IHeldItem;
		if (heldItem != null)
		{
			heldItem.ItemPostFrame(ref sample);
		}
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x000BFA10 File Offset: 0x000BDC10
	public void InvokeInputItemPreRender()
	{
		IHeldItem heldItem = this.inputItem as IHeldItem;
		if (heldItem != null)
		{
			heldItem.PreCameraRender();
		}
	}

	// Token: 0x04001A25 RID: 6693
	private const string TossItem_RPC = "TOSS";

	// Token: 0x04001A26 RID: 6694
	[NonSerialized]
	private CacheRef<Inventory> _inventory;

	// Token: 0x04001A27 RID: 6695
	[NonSerialized]
	private ItemRepresentation itemRep;

	// Token: 0x04001A28 RID: 6696
	[NonSerialized]
	private string _animationGroupNameCached;

	// Token: 0x04001A29 RID: 6697
	[NonSerialized]
	private ulong lastItemUseTime;

	// Token: 0x04001A2A RID: 6698
	[NonSerialized]
	private bool hasItem;

	// Token: 0x04001A2B RID: 6699
	[NonSerialized]
	private bool isPlayerInventory;
}
