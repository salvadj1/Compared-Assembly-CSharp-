using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005F2 RID: 1522
public sealed class Loadout : ScriptableObject
{
	// Token: 0x0600369F RID: 13983 RVA: 0x000C4EE4 File Offset: 0x000C30E4
	private static Loadout.Entry[] LoadEntryArray(Loadout.Entry[] array, Inventory.Slot.Kind kind)
	{
		array = (array ?? Loadout.Empty.EntryArray);
		for (int i = 0; i < array.Length; i++)
		{
			Loadout.Entry entry = array[i];
			entry.inferredSlotKind = kind;
			entry.inferredSlotOfKind = i;
		}
		return array;
	}

	// Token: 0x060036A0 RID: 13984 RVA: 0x000C4F28 File Offset: 0x000C3128
	private Loadout.Entry[][] GetEntryArrays()
	{
		return new Loadout.Entry[][]
		{
			Loadout.LoadEntryArray(this._inventory, Inventory.Slot.Kind.Default),
			Loadout.LoadEntryArray(this._belt, Inventory.Slot.Kind.Belt),
			Loadout.LoadEntryArray(this._wearable, Inventory.Slot.Kind.Armor)
		};
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x000C4F60 File Offset: 0x000C3160
	private static IEnumerable<Inventory.Addition> EnumerateAdditions(Loadout.Entry[][] arrays)
	{
		foreach (Loadout.Entry array in arrays)
		{
			foreach (Loadout.Entry entry in array)
			{
				Inventory.Addition current;
				if (entry.GetInventoryAddition(out current))
				{
					yield return current;
				}
			}
		}
		yield break;
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x000C4F8C File Offset: 0x000C318C
	private static IEnumerable<Loadout.Entry> EnumerateRequired(Loadout.Entry[][] arrays)
	{
		foreach (Loadout.Entry array in arrays)
		{
			foreach (Loadout.Entry entry in array)
			{
				if (entry.minimumRequirement)
				{
					yield return entry;
				}
			}
		}
		yield break;
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x000C4FB8 File Offset: 0x000C31B8
	private void GetAdditionArray(ref Inventory.Addition[] array, bool forceUpdate)
	{
		if (forceUpdate || array == null)
		{
			array = new List<Inventory.Addition>(Loadout.EnumerateAdditions(this.GetEntryArrays())).ToArray();
		}
	}

	// Token: 0x060036A4 RID: 13988 RVA: 0x000C4FEC File Offset: 0x000C31EC
	private void GetMinimumRequirementArray(ref Loadout.Entry[] array, bool forceUpdate)
	{
		if (forceUpdate || array == null)
		{
			array = new List<Loadout.Entry>(Loadout.EnumerateRequired(this.GetEntryArrays())).ToArray();
		}
	}

	// Token: 0x17000AE5 RID: 2789
	// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000C5020 File Offset: 0x000C3220
	private Inventory.Addition[] emptyInventoryAdditions
	{
		get
		{
			this.GetAdditionArray(ref this._blankInventoryLoadout, false);
			return this._blankInventoryLoadout;
		}
	}

	// Token: 0x17000AE6 RID: 2790
	// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000C5038 File Offset: 0x000C3238
	private Loadout.Entry[] minimumRequirements
	{
		get
		{
			this.GetMinimumRequirementArray(ref this._minimumRequirements, false);
			return this._minimumRequirements;
		}
	}

	// Token: 0x17000AE7 RID: 2791
	// (get) Token: 0x060036A7 RID: 13991 RVA: 0x000C5050 File Offset: 0x000C3250
	public BlueprintDataBlock[] defaultBlueprints
	{
		get
		{
			return this._defaultBlueprints ?? Loadout.Empty.BlueprintArray;
		}
	}

	// Token: 0x04001AC8 RID: 6856
	[SerializeField]
	private Loadout.Entry[] _inventory;

	// Token: 0x04001AC9 RID: 6857
	[SerializeField]
	private Loadout.Entry[] _belt;

	// Token: 0x04001ACA RID: 6858
	[SerializeField]
	private Loadout.Entry[] _wearable;

	// Token: 0x04001ACB RID: 6859
	[SerializeField]
	private BlueprintDataBlock[] _defaultBlueprints;

	// Token: 0x04001ACC RID: 6860
	[NonSerialized]
	private Inventory.Addition[] _blankInventoryLoadout;

	// Token: 0x04001ACD RID: 6861
	[NonSerialized]
	private Loadout.Entry[] _minimumRequirements;

	// Token: 0x020005F3 RID: 1523
	[Serializable]
	private class Entry
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000C506C File Offset: 0x000C326C
		public bool allowed
		{
			get
			{
				return this.enabled && this.item && (!this.item.IsSplittable() || this._useCount > 0);
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000C50B4 File Offset: 0x000C32B4
		public bool forEmptyInventories
		{
			get
			{
				return !this._minimumRequirement && this.allowed;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000C50CC File Offset: 0x000C32CC
		public bool minimumRequirement
		{
			get
			{
				return this._minimumRequirement && this.allowed;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x000C50E4 File Offset: 0x000C32E4
		public int useCount
		{
			get
			{
				return (!this.allowed) ? 0 : ((this.item._maxUses >= this._useCount) ? ((int)((byte)this._useCount)) : this.item._maxUses);
			}
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000C5130 File Offset: 0x000C3330
		public bool GetInventoryAddition(out Inventory.Addition addition)
		{
			if (this.allowed)
			{
				addition = default(Inventory.Addition);
				Inventory.Addition addition2 = addition;
				addition2.Ident = (Datablock.Ident)this.item;
				addition2.SlotPreference = Inventory.Slot.Preference.Define(this.inferredSlotKind, this.inferredSlotOfKind);
				addition2.UsesQuantity = this.useCount;
				addition = addition2;
				return true;
			}
			addition = default(Inventory.Addition);
			return false;
		}

		// Token: 0x04001ACE RID: 6862
		[SerializeField]
		private bool enabled;

		// Token: 0x04001ACF RID: 6863
		public ItemDataBlock item;

		// Token: 0x04001AD0 RID: 6864
		[SerializeField]
		private int _useCount;

		// Token: 0x04001AD1 RID: 6865
		[SerializeField]
		private bool _minimumRequirement;

		// Token: 0x04001AD2 RID: 6866
		[NonSerialized]
		internal Inventory.Slot.Kind inferredSlotKind;

		// Token: 0x04001AD3 RID: 6867
		[NonSerialized]
		internal int inferredSlotOfKind;
	}

	// Token: 0x020005F4 RID: 1524
	private static class Empty
	{
		// Token: 0x04001AD4 RID: 6868
		public static readonly Loadout.Entry[] EntryArray = new Loadout.Entry[0];

		// Token: 0x04001AD5 RID: 6869
		public static readonly BlueprintDataBlock[] BlueprintArray = new BlueprintDataBlock[0];
	}
}
