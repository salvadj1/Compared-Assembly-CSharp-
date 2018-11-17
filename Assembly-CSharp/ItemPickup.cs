using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000722 RID: 1826
[global::NGCAutoAddScript]
[RequireComponent(typeof(global::Inventory))]
public class ItemPickup : global::RigidObj, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06003CAA RID: 15530 RVA: 0x000D8F60 File Offset: 0x000D7160
	public ItemPickup() : base(global::RigidObj.FeatureFlags.StreamInitialVelocity)
	{
	}

	// Token: 0x06003CAB RID: 15531 RVA: 0x000D8F6C File Offset: 0x000D716C
	bool global::IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		global::ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint);
		return true;
	}

	// Token: 0x06003CAC RID: 15532 RVA: 0x000D8F78 File Offset: 0x000D7178
	private void StoreItemInfo(global::ItemDataBlock datablock, int uses)
	{
		global::ItemPickup.PickupInfo value;
		value.datablock = datablock;
		value.amount = uses;
		this.info = new global::ItemPickup.PickupInfo?(value);
		value.datablock.ConfigureItemPickup(this, uses);
	}

	// Token: 0x06003CAD RID: 15533 RVA: 0x000D8FB0 File Offset: 0x000D71B0
	[RPC]
	protected void PKIS(int itemName)
	{
		this.StoreItemInfo(global::DatablockDictionary.GetByUniqueID(itemName), 1);
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x000D8FC0 File Offset: 0x000D71C0
	[RPC]
	protected void PKIF(int itemName, byte itemAmount)
	{
		this.StoreItemInfo(global::DatablockDictionary.GetByUniqueID(itemName), (int)itemAmount);
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x000D8FD0 File Offset: 0x000D71D0
	public string ContextText(global::Controllable localControllable)
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

	// Token: 0x06003CB0 RID: 15536 RVA: 0x000D9070 File Offset: 0x000D7270
	protected override void OnDone()
	{
	}

	// Token: 0x06003CB1 RID: 15537 RVA: 0x000D9074 File Offset: 0x000D7274
	protected override void OnHide()
	{
		if (base.renderer)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x06003CB2 RID: 15538 RVA: 0x000D9094 File Offset: 0x000D7294
	protected override void OnShow()
	{
		if (base.renderer)
		{
			base.renderer.enabled = true;
		}
	}

	// Token: 0x04001EE4 RID: 7908
	private const string ItemInfoOne_RPC = "PKIS";

	// Token: 0x04001EE5 RID: 7909
	private const string ItemInfo_RPC = "PKIF";

	// Token: 0x04001EE6 RID: 7910
	[NonSerialized]
	private global::ItemPickup.PickupInfo? info;

	// Token: 0x04001EE7 RID: 7911
	[NonSerialized]
	private global::ItemPickup.PickupInfo? lastInfo;

	// Token: 0x04001EE8 RID: 7912
	[NonSerialized]
	private string lastString;

	// Token: 0x02000723 RID: 1827
	private struct PickupInfo : IEquatable<global::ItemPickup.PickupInfo>
	{
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000D90B4 File Offset: 0x000D72B4
		public bool Equals(global::ItemPickup.PickupInfo other)
		{
			return this.datablock == other.datablock && this.amount == other.amount;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000D90E0 File Offset: 0x000D72E0
		public override int GetHashCode()
		{
			return (!this.datablock) ? this.amount : (this.datablock.GetHashCode() ^ this.amount);
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000D9110 File Offset: 0x000D7310
		public override bool Equals(object obj)
		{
			return obj is global::ItemPickup.PickupInfo && this.Equals((global::ItemPickup.PickupInfo)obj);
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000D912C File Offset: 0x000D732C
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

		// Token: 0x04001EE9 RID: 7913
		public global::ItemDataBlock datablock;

		// Token: 0x04001EEA RID: 7914
		public int amount;
	}
}
