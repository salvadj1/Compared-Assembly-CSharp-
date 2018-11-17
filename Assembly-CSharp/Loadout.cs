using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B0 RID: 1712
public sealed class Loadout : ScriptableObject
{
	// Token: 0x06003A67 RID: 14951 RVA: 0x000CD140 File Offset: 0x000CB340
	private static global::Loadout.Entry[] LoadEntryArray(global::Loadout.Entry[] array, global::Inventory.Slot.Kind kind)
	{
		array = (array ?? global::Loadout.Empty.EntryArray);
		for (int i = 0; i < array.Length; i++)
		{
			global::Loadout.Entry entry = array[i];
			entry.inferredSlotKind = kind;
			entry.inferredSlotOfKind = i;
		}
		return array;
	}

	// Token: 0x06003A68 RID: 14952 RVA: 0x000CD184 File Offset: 0x000CB384
	private global::Loadout.Entry[][] GetEntryArrays()
	{
		return new global::Loadout.Entry[][]
		{
			global::Loadout.LoadEntryArray(this._inventory, global::Inventory.Slot.Kind.Default),
			global::Loadout.LoadEntryArray(this._belt, global::Inventory.Slot.Kind.Belt),
			global::Loadout.LoadEntryArray(this._wearable, global::Inventory.Slot.Kind.Armor)
		};
	}

	// Token: 0x06003A69 RID: 14953 RVA: 0x000CD1BC File Offset: 0x000CB3BC
	private static IEnumerable<global::Inventory.Addition> EnumerateAdditions(global::Loadout.Entry[][] arrays)
	{
		foreach (global::Loadout.Entry array in arrays)
		{
			foreach (global::Loadout.Entry entry in array)
			{
				global::Inventory.Addition current;
				if (entry.GetInventoryAddition(out current))
				{
					yield return current;
				}
			}
		}
		yield break;
	}

	// Token: 0x06003A6A RID: 14954 RVA: 0x000CD1E8 File Offset: 0x000CB3E8
	private static IEnumerable<global::Loadout.Entry> EnumerateRequired(global::Loadout.Entry[][] arrays)
	{
		foreach (global::Loadout.Entry array in arrays)
		{
			foreach (global::Loadout.Entry entry in array)
			{
				if (entry.minimumRequirement)
				{
					yield return entry;
				}
			}
		}
		yield break;
	}

	// Token: 0x06003A6B RID: 14955 RVA: 0x000CD214 File Offset: 0x000CB414
	private void GetAdditionArray(ref global::Inventory.Addition[] array, bool forceUpdate)
	{
		if (forceUpdate || array == null)
		{
			array = new List<global::Inventory.Addition>(global::Loadout.EnumerateAdditions(this.GetEntryArrays())).ToArray();
		}
	}

	// Token: 0x06003A6C RID: 14956 RVA: 0x000CD248 File Offset: 0x000CB448
	private void GetMinimumRequirementArray(ref global::Loadout.Entry[] array, bool forceUpdate)
	{
		if (forceUpdate || array == null)
		{
			array = new List<global::Loadout.Entry>(global::Loadout.EnumerateRequired(this.GetEntryArrays())).ToArray();
		}
	}

	// Token: 0x17000B5B RID: 2907
	// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000CD27C File Offset: 0x000CB47C
	private global::Inventory.Addition[] emptyInventoryAdditions
	{
		get
		{
			this.GetAdditionArray(ref this._blankInventoryLoadout, false);
			return this._blankInventoryLoadout;
		}
	}

	// Token: 0x17000B5C RID: 2908
	// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000CD294 File Offset: 0x000CB494
	private global::Loadout.Entry[] minimumRequirements
	{
		get
		{
			this.GetMinimumRequirementArray(ref this._minimumRequirements, false);
			return this._minimumRequirements;
		}
	}

	// Token: 0x17000B5D RID: 2909
	// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000CD2AC File Offset: 0x000CB4AC
	public global::BlueprintDataBlock[] defaultBlueprints
	{
		get
		{
			return this._defaultBlueprints ?? global::Loadout.Empty.BlueprintArray;
		}
	}

	// Token: 0x04001C99 RID: 7321
	[SerializeField]
	private global::Loadout.Entry[] _inventory;

	// Token: 0x04001C9A RID: 7322
	[SerializeField]
	private global::Loadout.Entry[] _belt;

	// Token: 0x04001C9B RID: 7323
	[SerializeField]
	private global::Loadout.Entry[] _wearable;

	// Token: 0x04001C9C RID: 7324
	[SerializeField]
	private global::BlueprintDataBlock[] _defaultBlueprints;

	// Token: 0x04001C9D RID: 7325
	[NonSerialized]
	private global::Inventory.Addition[] _blankInventoryLoadout;

	// Token: 0x04001C9E RID: 7326
	[NonSerialized]
	private global::Loadout.Entry[] _minimumRequirements;

	// Token: 0x020006B1 RID: 1713
	[Serializable]
	private class Entry
	{
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000CD2C8 File Offset: 0x000CB4C8
		public bool allowed
		{
			get
			{
				return this.enabled && this.item && (!this.item.IsSplittable() || this._useCount > 0);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000CD310 File Offset: 0x000CB510
		public bool forEmptyInventories
		{
			get
			{
				return !this._minimumRequirement && this.allowed;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000CD328 File Offset: 0x000CB528
		public bool minimumRequirement
		{
			get
			{
				return this._minimumRequirement && this.allowed;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000CD340 File Offset: 0x000CB540
		public int useCount
		{
			get
			{
				return (!this.allowed) ? 0 : ((this.item._maxUses >= this._useCount) ? ((int)((byte)this._useCount)) : this.item._maxUses);
			}
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000CD38C File Offset: 0x000CB58C
		public bool GetInventoryAddition(out global::Inventory.Addition addition)
		{
			if (this.allowed)
			{
				addition = default(global::Inventory.Addition);
				global::Inventory.Addition addition2 = addition;
				addition2.Ident = (global::Datablock.Ident)this.item;
				addition2.SlotPreference = global::Inventory.Slot.Preference.Define(this.inferredSlotKind, this.inferredSlotOfKind);
				addition2.UsesQuantity = this.useCount;
				addition = addition2;
				return true;
			}
			addition = default(global::Inventory.Addition);
			return false;
		}

		// Token: 0x04001C9F RID: 7327
		[SerializeField]
		private bool enabled;

		// Token: 0x04001CA0 RID: 7328
		public global::ItemDataBlock item;

		// Token: 0x04001CA1 RID: 7329
		[SerializeField]
		private int _useCount;

		// Token: 0x04001CA2 RID: 7330
		[SerializeField]
		private bool _minimumRequirement;

		// Token: 0x04001CA3 RID: 7331
		[NonSerialized]
		internal global::Inventory.Slot.Kind inferredSlotKind;

		// Token: 0x04001CA4 RID: 7332
		[NonSerialized]
		internal int inferredSlotOfKind;
	}

	// Token: 0x020006B2 RID: 1714
	private static class Empty
	{
		// Token: 0x04001CA5 RID: 7333
		public static readonly global::Loadout.Entry[] EntryArray = new global::Loadout.Entry[0];

		// Token: 0x04001CA6 RID: 7334
		public static readonly global::BlueprintDataBlock[] BlueprintArray = new global::BlueprintDataBlock[0];
	}
}
