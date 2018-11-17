using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200065E RID: 1630
[NGCAutoAddScript]
[RequireComponent(typeof(Inventory))]
public class ItemPickup : RigidObj, IContextRequestable, IContextRequestableQuick, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060038B6 RID: 14518 RVA: 0x000D0580 File Offset: 0x000CE780
	public ItemPickup() : base(RigidObj.FeatureFlags.StreamInitialVelocity)
	{
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x000D058C File Offset: 0x000CE78C
	bool IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x000D0598 File Offset: 0x000CE798
	private void StoreItemInfo(ItemDataBlock datablock, int uses)
	{
		ItemPickup.PickupInfo value;
		value.datablock = datablock;
		value.amount = uses;
		this.info = new ItemPickup.PickupInfo?(value);
		value.datablock.ConfigureItemPickup(this, uses);
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x000D05D0 File Offset: 0x000CE7D0
	[RPC]
	protected void PKIS(int itemName)
	{
		this.StoreItemInfo(DatablockDictionary.GetByUniqueID(itemName), 1);
	}

	// Token: 0x060038BA RID: 14522 RVA: 0x000D05E0 File Offset: 0x000CE7E0
	[RPC]
	protected void PKIF(int itemName, byte itemAmount)
	{
		this.StoreItemInfo(DatablockDictionary.GetByUniqueID(itemName), (int)itemAmount);
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x000D05F0 File Offset: 0x000CE7F0
	public string ContextText(Controllable localControllable)
	{
		if (!base.renderer.enabled)
		{
			return string.Empty;
		}
		if (this.info == null)
		{
			return "Loading...";
		}
		if (this.lastInfo == null || !this.lastInfo.Value.Equals(this.info.Value))
		{
			this.lastInfo = this.info;
			this.lastString = string.Format("Take '{0}'", this.info.Value);
		}
		return this.lastString;
	}

	// Token: 0x060038BC RID: 14524 RVA: 0x000D0690 File Offset: 0x000CE890
	protected override void OnDone()
	{
	}

	// Token: 0x060038BD RID: 14525 RVA: 0x000D0694 File Offset: 0x000CE894
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x000D06B4 File Offset: 0x000CE8B4
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x04001CEC RID: 7404
	private const string ItemInfoOne_RPC = "PKIS";

	// Token: 0x04001CED RID: 7405
	private const string ItemInfo_RPC = "PKIF";

	// Token: 0x04001CEE RID: 7406
	[NonSerialized]
	private ItemPickup.PickupInfo? info;

	// Token: 0x04001CEF RID: 7407
	[NonSerialized]
	private ItemPickup.PickupInfo? lastInfo;

	// Token: 0x04001CF0 RID: 7408
	[NonSerialized]
	private string lastString;

	// Token: 0x0200065F RID: 1631
	private struct PickupInfo : IEquatable<ItemPickup.PickupInfo>
	{
		// Token: 0x060038BF RID: 14527 RVA: 0x000D06D4 File Offset: 0x000CE8D4
		public bool Equals(ItemPickup.PickupInfo other)
		{
			return this.datablock == other.datablock && this.amount == other.amount;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000D0700 File Offset: 0x000CE900
		public override int GetHashCode()
		{
			return (!this.datablock) ? this.amount : (this.datablock.GetHashCode() ^ this.amount);
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000D0730 File Offset: 0x000CE930
		public override bool Equals(object obj)
		{
			return obj is ItemPickup.PickupInfo && this.Equals((ItemPickup.PickupInfo)obj);
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000D074C File Offset: 0x000CE94C
		public override string ToString()
		{
			if (this.datablock)
			{
				if (this.amount > 1 && this.datablock.IsSplittable())
				{
					return string.Format("{0} x{1}", this.datablock.name, this.amount);
				}
				return this.datablock.name;
			}
			else
			{
				if (this.amount > 1)
				{
					return string.Format("null x{0}", this.amount);
				}
				return "null";
			}
		}

		// Token: 0x04001CF1 RID: 7409
		public ItemDataBlock datablock;

		// Token: 0x04001CF2 RID: 7410
		public int amount;
	}
}
