using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class DatablockDictionary
{
	// Token: 0x17000A4C RID: 2636
	// (get) Token: 0x06003461 RID: 13409 RVA: 0x000BF3D4 File Offset: 0x000BD5D4
	public static ItemDataBlock[] All
	{
		get
		{
			return DatablockDictionary._all;
		}
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x000BF3DC File Offset: 0x000BD5DC
	public static void TryInitialize()
	{
		if (!DatablockDictionary.initializedAtLeastOnce)
		{
			DatablockDictionary.Initialize();
		}
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x000BF3F0 File Offset: 0x000BD5F0
	public static void Initialize()
	{
		DatablockDictionary._dataBlocks = new Dictionary<string, int>();
		DatablockDictionary._dataBlocksByUniqueID = new Dictionary<int, int>();
		DatablockDictionary._lootSpawnLists = new Dictionary<string, LootSpawnList>();
		List<ItemDataBlock> list = new List<ItemDataBlock>();
		HashSet<ItemDataBlock> hashSet = new HashSet<ItemDataBlock>();
		foreach (ItemDataBlock itemDataBlock in Bundling.LoadAll<ItemDataBlock>())
		{
			if (hashSet.Add(itemDataBlock))
			{
				int count = list.Count;
				DatablockDictionary._dataBlocks.Add(itemDataBlock.name, count);
				DatablockDictionary._dataBlocksByUniqueID.Add(itemDataBlock.uniqueID, count);
				list.Add(itemDataBlock);
			}
		}
		DatablockDictionary._all = list.ToArray();
		foreach (LootSpawnList lootSpawnList in Bundling.LoadAll<LootSpawnList>())
		{
			DatablockDictionary._lootSpawnLists.Add(lootSpawnList.name, lootSpawnList);
		}
		DatablockDictionary.initializedAtLeastOnce = true;
	}

	// Token: 0x06003464 RID: 13412 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
	public static ItemDataBlock GetByUniqueID(int uniqueID)
	{
		int num;
		if (!DatablockDictionary._dataBlocksByUniqueID.TryGetValue(uniqueID, out num))
		{
			return null;
		}
		return DatablockDictionary._all[num];
	}

	// Token: 0x06003465 RID: 13413 RVA: 0x000BF4FC File Offset: 0x000BD6FC
	public static ItemDataBlock GetByName(string name)
	{
		int num;
		if (!DatablockDictionary._dataBlocks.TryGetValue(name, out num))
		{
			return null;
		}
		return DatablockDictionary._all[num];
	}

	// Token: 0x06003466 RID: 13414 RVA: 0x000BF524 File Offset: 0x000BD724
	public static LootSpawnList GetLootSpawnListByName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		LootSpawnList result;
		if (!DatablockDictionary._lootSpawnLists.TryGetValue(name, out result))
		{
			Debug.LogError("Theres no loot spawn list with name " + name);
		}
		return result;
	}

	// Token: 0x06003467 RID: 13415 RVA: 0x000BF564 File Offset: 0x000BD764
	public static TArmorModel GetArmorModelByUniqueID<TArmorModel>(int uniqueID) where TArmorModel : ArmorModel, new()
	{
		ArmorDataBlock armorDataBlock = DatablockDictionary.GetByUniqueID(uniqueID) as ArmorDataBlock;
		if (!armorDataBlock)
		{
			return (TArmorModel)((object)null);
		}
		return armorDataBlock.GetArmorModel<TArmorModel>();
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x000BF598 File Offset: 0x000BD798
	public static ArmorModel GetArmorModelByUniqueID(int uniqueID, ArmorModelSlot slot)
	{
		ArmorDataBlock armorDataBlock = DatablockDictionary.GetByUniqueID(uniqueID) as ArmorDataBlock;
		if (!armorDataBlock)
		{
			return null;
		}
		return armorDataBlock.GetArmorModel(slot);
	}

	// Token: 0x04001A1C RID: 6684
	private const int expectedDBListLength = 14;

	// Token: 0x04001A1D RID: 6685
	private static Dictionary<string, int> _dataBlocks;

	// Token: 0x04001A1E RID: 6686
	private static Dictionary<int, int> _dataBlocksByUniqueID;

	// Token: 0x04001A1F RID: 6687
	private static ItemDataBlock[] _all;

	// Token: 0x04001A20 RID: 6688
	public static Dictionary<string, LootSpawnList> _lootSpawnLists;

	// Token: 0x04001A21 RID: 6689
	private static bool initializedAtLeastOnce;
}
