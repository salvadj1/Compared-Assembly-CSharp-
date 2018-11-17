using System;
using UnityEngine;

// Token: 0x020006B5 RID: 1717
public class LootSpawnList : ScriptableObject
{
	// Token: 0x06003A88 RID: 14984 RVA: 0x000CD70C File Offset: 0x000CB90C
	public void PopulateInventory(global::Inventory inven)
	{
		global::LootSpawnList.RecursiveInventoryPopulateArgs recursiveInventoryPopulateArgs;
		recursiveInventoryPopulateArgs.inventory = inven;
		recursiveInventoryPopulateArgs.spawnCount = 0;
		recursiveInventoryPopulateArgs.inventoryExausted = inven.noVacantSlots;
		if (!recursiveInventoryPopulateArgs.inventoryExausted)
		{
			this.PopulateInventory_Recurse(ref recursiveInventoryPopulateArgs);
		}
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x000CD74C File Offset: 0x000CB94C
	private void PopulateInventory_Recurse(ref global::LootSpawnList.RecursiveInventoryPopulateArgs args)
	{
		if (this.maxPackagesToSpawn > this.LootPackages.Length)
		{
			this.maxPackagesToSpawn = this.LootPackages.Length;
		}
		int num;
		if (this.spawnOneOfEach)
		{
			num = this.LootPackages.Length;
		}
		else
		{
			num = Random.Range(this.minPackagesToSpawn, this.maxPackagesToSpawn);
		}
		int num2 = 0;
		while (!args.inventoryExausted && num2 < num)
		{
			global::LootSpawnList.LootWeightedEntry lootWeightedEntry;
			if (this.spawnOneOfEach)
			{
				lootWeightedEntry = this.LootPackages[num2];
			}
			else
			{
				lootWeightedEntry = (global::WeightSelection.RandomPickEntry(this.LootPackages) as global::LootSpawnList.LootWeightedEntry);
			}
			if (lootWeightedEntry == null)
			{
				Debug.Log("Massive fuckup...");
				return;
			}
			Object obj = lootWeightedEntry.obj;
			if (obj)
			{
				if (obj is global::ItemDataBlock)
				{
					global::ItemDataBlock datablock = obj as global::ItemDataBlock;
					if (!object.ReferenceEquals(args.inventory.AddItem(datablock, global::Inventory.Slot.Preference.Define(args.spawnCount, false, global::Inventory.Slot.KindFlags.Default | global::Inventory.Slot.KindFlags.Belt), Random.Range(lootWeightedEntry.amountMin, lootWeightedEntry.amountMax + 1)), null))
					{
						args.spawnCount++;
						if (args.inventory.noVacantSlots)
						{
							args.inventoryExausted = true;
						}
					}
				}
				else if (obj is global::LootSpawnList)
				{
					((global::LootSpawnList)obj).PopulateInventory_Recurse(ref args);
				}
			}
			num2++;
		}
	}

	// Token: 0x04001CBC RID: 7356
	public global::LootSpawnList.LootWeightedEntry[] LootPackages;

	// Token: 0x04001CBD RID: 7357
	public int minPackagesToSpawn = 1;

	// Token: 0x04001CBE RID: 7358
	public int maxPackagesToSpawn = 1;

	// Token: 0x04001CBF RID: 7359
	public bool noDuplicates;

	// Token: 0x04001CC0 RID: 7360
	public bool spawnOneOfEach;

	// Token: 0x020006B6 RID: 1718
	[Serializable]
	public class LootWeightedEntry : global::WeightSelection.WeightedEntry
	{
		// Token: 0x04001CC1 RID: 7361
		public int amountMin;

		// Token: 0x04001CC2 RID: 7362
		public int amountMax = 1;
	}

	// Token: 0x020006B7 RID: 1719
	private struct RecursiveInventoryPopulateArgs
	{
		// Token: 0x04001CC3 RID: 7363
		public global::Inventory inventory;

		// Token: 0x04001CC4 RID: 7364
		public int spawnCount;

		// Token: 0x04001CC5 RID: 7365
		public bool inventoryExausted;
	}
}
