using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x02000660 RID: 1632
public class DatablockDictionary
{
	// Token: 0x17000AC2 RID: 2754
	// (get) Token: 0x06003829 RID: 14377 RVA: 0x000C7630 File Offset: 0x000C5830
	public static global::ItemDataBlock[] All
	{
		get
		{
			return global::DatablockDictionary._all;
		}
	}

	// Token: 0x0600382A RID: 14378 RVA: 0x000C7638 File Offset: 0x000C5838
	public static void TryInitialize()
	{
		if (!global::DatablockDictionary.initializedAtLeastOnce)
		{
			global::DatablockDictionary.Initialize();
		}
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x000C764C File Offset: 0x000C584C
	public static void Initialize()
	{
		global::DatablockDictionary._dataBlocks = new Dictionary<string, int>();
		global::DatablockDictionary._dataBlocksByUniqueID = new Dictionary<int, int>();
		global::DatablockDictionary._lootSpawnLists = new Dictionary<string, global::LootSpawnList>();
		List<global::ItemDataBlock> list = new List<global::ItemDataBlock>();
		HashSet<global::ItemDataBlock> hashSet = new HashSet<global::ItemDataBlock>();
		foreach (global::ItemDataBlock itemDataBlock in Facepunch.Bundling.LoadAll<global::ItemDataBlock>())
		{
			if (hashSet.Add(itemDataBlock))
			{
				int count = list.Count;
				global::DatablockDictionary._dataBlocks.Add(itemDataBlock.name, count);
				global::DatablockDictionary._dataBlocksByUniqueID.Add(itemDataBlock.uniqueID, count);
				list.Add(itemDataBlock);
			}
		}
		global::DatablockDictionary._all = list.ToArray();
		foreach (global::LootSpawnList lootSpawnList in Facepunch.Bundling.LoadAll<global::LootSpawnList>())
		{
			global::DatablockDictionary._lootSpawnLists.Add(lootSpawnList.name, lootSpawnList);
		}
		global::DatablockDictionary.initializedAtLeastOnce = true;
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x000C7730 File Offset: 0x000C5930
	public static global::ItemDataBlock GetByUniqueID(int uniqueID)
	{
		int num;
		if (!global::DatablockDictionary._dataBlocksByUniqueID.TryGetValue(uniqueID, out num))
		{
			return null;
		}
		return global::DatablockDictionary._all[num];
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x000C7758 File Offset: 0x000C5958
	public static global::ItemDataBlock GetByName(string name)
	{
		int num;
		if (!global::DatablockDictionary._dataBlocks.TryGetValue(name, out num))
		{
			return null;
		}
		return global::DatablockDictionary._all[num];
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x000C7780 File Offset: 0x000C5980
	public static global::LootSpawnList GetLootSpawnListByName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		global::LootSpawnList result;
		if (!global::DatablockDictionary._lootSpawnLists.TryGetValue(name, out result))
		{
			Debug.LogError("Theres no loot spawn list with name " + name);
		}
		return result;
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x000C77C0 File Offset: 0x000C59C0
	public static TArmorModel GetArmorModelByUniqueID<TArmorModel>(int uniqueID) where TArmorModel : global::ArmorModel, new()
	{
		global::ArmorDataBlock armorDataBlock = global::DatablockDictionary.GetByUniqueID(uniqueID) as global::ArmorDataBlock;
		if (!armorDataBlock)
		{
			return (TArmorModel)((object)null);
		}
		return armorDataBlock.GetArmorModel<TArmorModel>();
	}

	// Token: 0x06003830 RID: 14384 RVA: 0x000C77F4 File Offset: 0x000C59F4
	public static global::ArmorModel GetArmorModelByUniqueID(int uniqueID, global::ArmorModelSlot slot)
	{
		global::ArmorDataBlock armorDataBlock = global::DatablockDictionary.GetByUniqueID(uniqueID) as global::ArmorDataBlock;
		if (!armorDataBlock)
		{
			return null;
		}
		return armorDataBlock.GetArmorModel(slot);
	}

	// Token: 0x04001BED RID: 7149
	private const int expectedDBListLength = 14;

	// Token: 0x04001BEE RID: 7150
	private static Dictionary<string, int> _dataBlocks;

	// Token: 0x04001BEF RID: 7151
	private static Dictionary<int, int> _dataBlocksByUniqueID;

	// Token: 0x04001BF0 RID: 7152
	private static global::ItemDataBlock[] _all;

	// Token: 0x04001BF1 RID: 7153
	public static Dictionary<string, global::LootSpawnList> _lootSpawnLists;

	// Token: 0x04001BF2 RID: 7154
	private static bool initializedAtLeastOnce;
}
